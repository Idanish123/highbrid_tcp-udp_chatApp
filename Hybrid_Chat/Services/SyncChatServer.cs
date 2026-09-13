using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hybrid_Chat.Models;

namespace Hybrid_Chat.Services
{
    public class SyncChatServer
    {
        private TcpListener? _tcpListener;
        private UdpClient? _udpListener;
        private CancellationTokenSource? _cts;
        private readonly ConcurrentDictionary<string, ClientSession> _activeClients = new();
        private X509Certificate2? _serverCertificate;
        private Task? _heartbeatTask;
        private readonly TimeSpan _keepAliveInterval = TimeSpan.FromSeconds(10);
        private readonly TimeSpan _clientTimeout = TimeSpan.FromSeconds(30);
        
        private int _tcpPort;
        private int _udpPort;
        private bool _isRunning;

        // Thread-safe UI communication channels
        public event Action<string>? OnLogEvent;
        public event Action<List<ClientSession>>? OnClientListUpdated;
        public event Action<string, string>? OnClientTypingReceived;

        public bool IsRunning => _isRunning;
        public int ActiveCount => _activeClients.Count;

        public void Start(int tcpPort, int udpPort)
        {
            if (_isRunning) return;

            _tcpPort = tcpPort;
            _udpPort = udpPort;
            _cts = new CancellationTokenSource();
            _isRunning = true;

            InitializeCertificate();

            // Start TCP Engine
            _tcpListener = new TcpListener(IPAddress.Any, _tcpPort);
            _tcpListener.Start();
            _tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            Task.Run(() => AcceptTcpClientsAsync(_cts.Token));
            Log($"[TCP] Server socket listening on port {_tcpPort} successfully.");

            // Start UDP Presence/Telemetry Engine
            _udpListener = new UdpClient(_udpPort);
            Task.Run(() => ListenUdpTelemetryAsync(_cts.Token));
            Log($"[UDP] Engine bound and receiving on port {_udpPort}.");

            _heartbeatTask = Task.Run(() => MonitorClientHeartbeatsAsync(_cts.Token));
            Log("[SYSTEM] Hybrid Server fully active with TLS and heartbeat guard.");
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cts?.Cancel();

            // Close listeners
            _tcpListener?.Stop();
            _udpListener?.Close();

            // Safely close all client connections
            foreach (var session in _activeClients.Values.ToList())
            {
                if (_activeClients.TryRemove(session.SessionId, out var closedSession))
                {
                    try
                    {
                        closedSession.Writer?.Dispose();
                        closedSession.SecureStream?.Dispose();
                        closedSession.Connection?.Close();
                    }
                    catch { }
                }
            }

            Log("[SYSTEM] Server listener stopped gracefully. Disconnected all sessions.");
            TriggerClientUpdate();
        }

        private async Task AcceptTcpClientsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var tcpClient = await _tcpListener!.AcceptTcpClientAsync(token);
                    var remoteIp = tcpClient.Client.RemoteEndPoint?.ToString() ?? "Unknown";
                    Log($"[TCP] Core handshake requested from {remoteIp}");

                    // Spawn thread to handle actual protocol initialization
                    _ = Task.Run(() => HandleClientLifecycleAsync(tcpClient, token), token);
                }
                catch (Exception)
                {
                    // Listener stopped or cancelled
                    break;
                }
            }
        }

        private async Task HandleClientLifecycleAsync(TcpClient client, CancellationToken token)
        {
            string sessionId = Guid.NewGuid().ToString().Substring(0, 8);
            var session = new ClientSession
            {
                SessionId = sessionId,
                Connection = client,
                RemoteEndpoint = client.Client.RemoteEndPoint?.ToString() ?? "Unknown"
            };

            try
            {
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                client.Client.ReceiveTimeout = 30000;
                client.Client.SendTimeout = 30000;

                var sslStream = new SslStream(client.GetStream(), false);
                var serverOptions = new SslServerAuthenticationOptions
                {
                    ServerCertificate = _serverCertificate!,
                    EnabledSslProtocols = SslProtocols.Tls12,
                    ClientCertificateRequired = false,
                    CertificateRevocationCheckMode = X509RevocationMode.NoCheck
                };
                try
                {
                    await sslStream.AuthenticateAsServerAsync(serverOptions);
                    Log($"[TLS] Server authentication successful for session {sessionId}");
                }
                catch (AuthenticationException authEx)
                {
                    Log($"[TLS ERROR] Authentication failed for {sessionId}: {authEx.Message}");
                    Log($"[TLS ERROR] Inner: {authEx.InnerException?.Message}");
                    throw;
                }

                session.SecureStream = sslStream;
                session.Writer = new StreamWriter(sslStream, Encoding.UTF8) { AutoFlush = true };
                using var reader = new StreamReader(sslStream, Encoding.UTF8);

                // Expect standard handshake JOIN
                string? initialFrame = await reader.ReadLineAsync(token);
                if (string.IsNullOrEmpty(initialFrame))
                {
                    client.Close();
                    return;
                }

                var handshake = NetworkPacket.Deserialize(initialFrame);
                if (handshake.Type == PacketType.JOIN)
                {
                    session.Username = string.IsNullOrWhiteSpace(handshake.Sender) ? $"Guest_{sessionId}" : handshake.Sender;
                    
                    // Enforce uniqueness
                    if (_activeClients.Values.Any(c => c.Username.Equals(session.Username, StringComparison.OrdinalIgnoreCase)))
                    { 
                        var rejectPacket = new NetworkPacket
                        {
                            Type = PacketType.SYS,
                            Sender = "Server",
                            Payload = "REJECT|Username already taken."
                        };
                        await session.Writer.WriteLineAsync(rejectPacket.Serialize());
                        client.Close();
                        return;
                    }

                    session.LastKeepAlive = DateTime.UtcNow;
                    _activeClients.TryAdd(sessionId, session);
                    Log($"[SYSTEM] User Registered: {session.Username} [{session.RemoteEndpoint}]");
                    
                    var joinNotice = new NetworkPacket 
                    { 
                        Type = PacketType.SYS, 
                        Sender = "Server", 
                        Payload = $"{session.Username} joined the synchronized workspace."
                    };
                    await BroadcastPacketAsync(joinNotice);
                    TriggerClientUpdate();
                }
                else
                {
                    client.Close();
                    return;
                }

                // Continuous conversation loop
                while (!token.IsCancellationRequested && client.Connected)
                {
                    string? rawData = await reader.ReadLineAsync(token);
                    if (rawData == null) break; // End of Stream

                    var packet = NetworkPacket.Deserialize(rawData);
                    session.LastKeepAlive = DateTime.UtcNow;

                    if (packet.Type == PacketType.LEAVE)
                    {
                        break;
                    }
                    else if (packet.Type == PacketType.PING)
                    {
                        await SendPacketAsync(session, new NetworkPacket
                        {
                            Type = PacketType.PONG,
                            Sender = "Server",
                            Payload = "alive"
                        });
                    }
                    else if (packet.Type == PacketType.PONG)
                    {
                        // keepalive confirmation; no broadcast needed
                    }
                    else if (packet.Type == PacketType.MSG)
                    {
                        Log($"[CHAT] {packet.Sender}: {packet.Payload}");
                        await BroadcastPacketAsync(packet);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"[ERROR] Error occurred in connection process with ID {sessionId}: {ex.Message}");
            }
            finally
            {
                RemoveSession(sessionId, true);
                client.Close();
            }
        }

        private async Task ListenUdpTelemetryAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await _udpListener!.ReceiveAsync(token);
                    string payload = Encoding.UTF8.GetString(result.Buffer);
                    
                    // Formats: TYPING|UserName|1 (Typing) or TYPING|UserName|0 (Stopped)
                    var parts = payload.Split('|');
                    if (parts.Length == 3 && parts[0] == "TYPING")
                    {
                        string username = parts[1];
                        bool isTyping = parts[2] == "1";
                        
                        // Notify any attached UI listener and relay typing state to connected clients.
                        OnClientTypingReceived?.Invoke(username, isTyping ? "is typing..." : string.Empty);
                        var typingPacket = new NetworkPacket
                        {
                            Type = PacketType.TYPING,
                            Sender = username,
                            Payload = isTyping ? "1" : "0"
                        };
                        await BroadcastPacketAsync(typingPacket);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    break;
                }
            }
        }

        private async Task BroadcastPacketAsync(NetworkPacket packet)
        {
            string data = packet.Serialize();
            foreach (var session in _activeClients.Values.ToList())
            {
                if (session.Connection == null || session.Writer == null || !session.Connection.Connected)
                {
                    continue;
                }

                try
                {
                    Task sendTask;
                    lock (session.SendLock)
                    {
                        sendTask = session.Writer.WriteLineAsync(data);
                    }
                    await sendTask;
                }
                catch
                {
                    // Stale connection cleanup can handle individual client errors
                    RemoveSession(session.SessionId, false);
                }
            }
        }

        private async Task SendPacketAsync(ClientSession session, NetworkPacket packet)
        {
            if (session.Writer == null || session.Connection == null || !session.Connection.Connected)
            {
                return;
            }

            try
            {
                Task sendTask;
                lock (session.SendLock)
                {
                    sendTask = session.Writer.WriteLineAsync(packet.Serialize());
                }
                await sendTask;
            }
            catch
            {
                RemoveSession(session.SessionId, false);
            }
        }

        private async Task MonitorClientHeartbeatsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(_keepAliveInterval, token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                var now = DateTime.UtcNow;
                foreach (var session in _activeClients.Values.ToList())
                {
                    if (now - session.LastKeepAlive > _clientTimeout)
                    {
                        Log($"[TIMEOUT] Client '{session.Username}' inactive for more than {_clientTimeout.TotalSeconds} seconds.");
                        RemoveSession(session.SessionId, true);
                    }
                }

                await BroadcastPacketAsync(new NetworkPacket
                {
                    Type = PacketType.PING,
                    Sender = "Server",
                    Payload = "keepalive"
                });
            }
        }

        private void RemoveSession(string sessionId, bool notify)
        {
            if (!_activeClients.TryRemove(sessionId, out var session))
            {
                return;
            }

            try
            {
                session.Writer?.Dispose();
                session.SecureStream?.Dispose();
                session.Connection?.Close();
            }
            catch { }

            Log($"[SYSTEM] User Disconnected: {session.Username}");
            if (notify)
            {
                var leaveNotice = new NetworkPacket
                {
                    Type = PacketType.SYS,
                    Sender = "Server",
                    Payload = $"{session.Username} has exited the room."
                };
                _ = BroadcastPacketAsync(leaveNotice);
            }

            TriggerClientUpdate();
        }

        private void InitializeCertificate()
        {
            if (_serverCertificate != null)
            {
                return;
            }

            try
            {
                // Generate RSA key with explicit settings to avoid ephemeral key issues
                using var rsa = RSA.Create(2048);
                var request = new CertificateRequest(
                    "CN=Hybrid_ChatServer",
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, false));
                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment, false));
                request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
                    new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));
                request.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(request.PublicKey, false));

                var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddYears(1));
                
                // Export and re-import with explicit machine key store for better compatibility
                using var pubCertOnly = new X509Certificate2(cert.Export(X509ContentType.Cert));
                _serverCertificate = new X509Certificate2(cert.Export(X509ContentType.Pkcs12), (string?)null, X509KeyStorageFlags.DefaultKeySet);
                
                Log("[TLS] Self-signed certificate initialized successfully.");
            }
            catch (Exception ex)
            {
                Log($"[TLS ERROR] Certificate initialization failed: {ex.Message}");
                throw;
            }
        }

        private void Log(string message)
        {
            OnLogEvent?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        private void TriggerClientUpdate()
        {
            OnClientListUpdated?.Invoke(_activeClients.Values.ToList());
        }
    }
}