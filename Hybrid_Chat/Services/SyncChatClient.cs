using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hybrid_Chat.Models;

namespace Hybrid_Chat.Services
{
    public class SyncChatClient
    {
        private TcpClient? _tcpClient;
        private UdpClient? _udpClient;
        private SslStream? _sslStream;
        private Stream? _stream;
        private StreamWriter? _writer;
        private StreamReader? _reader;
        private CancellationTokenSource? _cts;
        private string _username = string.Empty;
        private string _serverIp = "127.0.0.1";
        private int _tcpPort;
        private int _udpPort;
        private bool _isConnected;
        private DateTime _lastServerKeepAlive = DateTime.UtcNow;
        private readonly TimeSpan _keepAliveInterval = TimeSpan.FromSeconds(10);
        private readonly TimeSpan _keepAliveTimeout = TimeSpan.FromSeconds(30);
        private readonly object _sendLock = new();

        public event Action<string>? OnLogEvent;
        public event Action<NetworkPacket>? OnPacketReceived;
        public event Action<bool>? OnConnectionStateChanged;

        public bool IsConnected => _isConnected;
        public string Username => _username;

        public async Task<bool> ConnectAsync(string ip, int tcpPort, int udpPort, string username)
        {
            if (_isConnected) return true;

            _serverIp = ip;
            _tcpPort = tcpPort;
            _udpPort = udpPort;
            _username = username;
            _cts = new CancellationTokenSource();

            try
            {
                Log($"[CLIENT] Attempting connection to TCP server {_serverIp}:{_tcpPort}...");
                _tcpClient = new TcpClient();
                _tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                _tcpClient.Client.ReceiveTimeout = 30000;
                _tcpClient.Client.SendTimeout = 30000;

                // Resolve connection inside a Task
                await _tcpClient.ConnectAsync(_serverIp, _tcpPort);
                
                _sslStream = new SslStream(_tcpClient.GetStream(), false, ValidateServerCertificate);
                var clientOptions = new SslClientAuthenticationOptions
                {
                    TargetHost = "Hybrid_ChatServer",
                    EnabledSslProtocols = SslProtocols.Tls12,
                    CertificateRevocationCheckMode = X509RevocationMode.NoCheck,
                    RemoteCertificateValidationCallback = ValidateServerCertificate
                };
                try
                {
                    await _sslStream.AuthenticateAsClientAsync(clientOptions);
                    Log("[TLS] Client authentication successful");
                }
                catch (AuthenticationException authEx)
                {
                    Log($"[TLS ERROR] Client auth failed: {authEx.Message}");
                    Log($"[TLS ERROR] Inner: {authEx.InnerException?.Message}");
                    throw;
                }

                _stream = _sslStream;
                _writer = new StreamWriter(_stream, Encoding.UTF8) { AutoFlush = true };
                _reader = new StreamReader(_stream, Encoding.UTF8);

                // Immediately initialize handshake JOIN frame
                var handshake = new NetworkPacket
                {
                    Type = PacketType.JOIN,
                    Sender = _username,
                    Payload = "Initial connection shake"
                };
                await _writer.WriteLineAsync(handshake.Serialize());

                // Set up low latency UDP Broadcaster
                _udpClient = new UdpClient();

                _isConnected = true;
                _lastServerKeepAlive = DateTime.UtcNow;
                OnConnectionStateChanged?.Invoke(true);
                Log("[CLIENT] Registration accepted. Initiating live listeners.");

                // Spawn TCP Reader Loop
                _ = Task.Run(() => ReadTcpMessagesAsync(_cts.Token));
                _ = Task.Run(() => HeartbeatLoopAsync(_cts.Token));

                return true;
            }
            catch (Exception ex)
            {
                Log($"[CONNECTION ERROR] Handshake failed: {ex.Message}");
                Cleanup();
                return false;
            }
        }

        public async Task DisconnectAsync()
        {
            if (!_isConnected) return;
            
            try
            {
                var leavePacket = new NetworkPacket
                { 
                    Type = PacketType.LEAVE, 
                    Sender = _username, 
                    Payload = "Departed manually"
                };
                if (_writer != null)
                {
                    await _writer.WriteLineAsync(leavePacket.Serialize());
                }
            }
            catch { }
            
            Cleanup();
            Log("[CLIENT] Logged off successfully.");
        }

        public async Task SendMessageAsync(string text)
        {
            if (!_isConnected || _writer == null) return;

            try
            {
                var msgPacket = new NetworkPacket
                {
                    Type = PacketType.MSG,
                    Sender = _username,
                    Payload = text
                };
                await SendPacketAsync(msgPacket);
            }
            catch (Exception ex)
            {
                Log($"[SEND ERROR] Message failed: {ex.Message}");
            }
        }

        private async Task SendPacketAsync(NetworkPacket packet)
        {
            if (!_isConnected || _writer == null) return;

            try
            {
                Task sendTask;
                lock (_sendLock)
                {
                    sendTask = _writer.WriteLineAsync(packet.Serialize());
                }
                await sendTask;
            }
            catch (Exception ex)
            {
                Log($"[SEND ERROR] Heartbeat/message failed: {ex.Message}");
                Cleanup();
            }
        }

        /// <summary>
        /// Emits an unreliable UDP message announcing typing status
        /// </summary>
        public void SendTypingTelemetry(bool isTyping)
        {
            if (!_isConnected || _udpClient == null) return;

            try
            {
                string payload = $"TYPING|{_username}|{(isTyping ? "1" : "0")}";
                byte[] data = Encoding.UTF8.GetBytes(payload);
                _udpClient.Send(data, data.Length, _serverIp, _udpPort);
            }
            catch
            {
                // UDP is fire and forget, silences network errors gracefully
            }
        }

        private async Task ReadTcpMessagesAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _tcpClient != null && _tcpClient.Connected)
                {
                    string? rawPayload = await _reader!.ReadLineAsync(token);
                    if (rawPayload == null)
                    {
                        Log("[CLIENT] Remote host closed TCP connection cleanly.");
                        break;
                    }

                    var packet = NetworkPacket.Deserialize(rawPayload);
                    if (packet.Type == PacketType.PING)
                    {
                        await SendPacketAsync(new NetworkPacket
                        {
                            Type = PacketType.PONG,
                            Sender = _username,
                            Payload = "alive"
                        });
                        continue;
                    }

                    if (packet.Type == PacketType.PONG)
                    {
                        _lastServerKeepAlive = DateTime.UtcNow;
                        continue;
                    }

                    if (packet.Type == PacketType.SYS && packet.Payload.StartsWith("REJECT"))
                    {
                        Log("[SYSTEM REJECTION] Port Authority refused registration. Username taken.");
                        break;
                    }

                    OnPacketReceived?.Invoke(packet);
                }
            }
            catch (Exception)
            {
                // Server session ended or thread aborted
            }
            finally
            {
                if (_isConnected)
                {
                    _ = Task.Run(() => Cleanup());
                }
            }
        }

        private async Task HeartbeatLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _isConnected)
            {
                try
                {
                    await Task.Delay(_keepAliveInterval, token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (!_isConnected) break;

                await SendPacketAsync(new NetworkPacket
                {
                    Type = PacketType.PING,
                    Sender = _username,
                    Payload = "keepalive"
                });

                if (DateTime.UtcNow - _lastServerKeepAlive > _keepAliveTimeout)
                {
                    Log("[CLIENT] Server keepalive timeout exceeded. Disconnecting.");
                    Cleanup();
                    break;
                }
            }
        }

        private void Cleanup()
        {
            _isConnected = false;
            _cts?.Cancel();
            _tcpClient?.Close();
            _udpClient?.Close();
            _sslStream?.Dispose();
            _stream = null;
            _writer = null;
            _reader = null;
            
            OnConnectionStateChanged?.Invoke(false);
        }

        private bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate? certificate,
            System.Security.Cryptography.X509Certificates.X509Chain? chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // Accept self-signed certificates for local demonstrations and encrypted traffic.
            return true;
        }

        private void Log(string message)
        {
            OnLogEvent?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}