using System;
using System.Collections.Generic; 
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hybrid_Chat.Models;
using Hybrid_Chat.Services;

namespace Hybrid_Chat.GUI
{
    public partial class FormMain : Form
    {
        private readonly SyncChatServer _server;
        private readonly SyncChatClient _client;
        
        // Helper state variables
        private bool _isUserTyping = false;
        private readonly System.Windows.Forms.Timer _typingStopTimer;
        private readonly System.Windows.Forms.Timer _typingIndicatorRefreshTimer;
        private readonly Dictionary<string, DateTime> _remoteTypingStates = new();

        public FormMain()
        {
            InitializeComponent();
            
            _server = new SyncChatServer();
            _client = new SyncChatClient();

            // Initialize Typing Status Timer
            _typingStopTimer = new System.Windows.Forms.Timer();
            _typingStopTimer.Interval = 1500; // Reset after 1.5 seconds of non-typing
            _typingStopTimer.Tick += TypingStopTimer_Tick;

            _typingIndicatorRefreshTimer = new System.Windows.Forms.Timer();
            _typingIndicatorRefreshTimer.Interval = 500;
            _typingIndicatorRefreshTimer.Tick += TypingIndicatorRefreshTimer_Tick;
            _typingIndicatorRefreshTimer.Start();

            // Wire up Server events
            _server.OnLogEvent += Server_OnLogEvent;
            _server.OnClientListUpdated += Server_OnClientListUpdated;
            _server.OnClientTypingReceived += Server_OnClientTypingReceived;

            // Wire up Client events
            _client.OnLogEvent += Client_OnLogEvent;
            _client.OnPacketReceived += Client_OnPacketReceived;
            _client.OnConnectionStateChanged += Client_OnConnectionStateChanged;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AppendServerLog("[SYSTEM ENGINE] UI Thread Ready. Server and Client stacks loaded.");
            AppendClientLog("[CLIENT STATUS] Ready to establish virtual socket stream.");
            
            // Default configuration profiles
            txtServerPort.Text = "11000";
            txtClientIP.Text = "127.0.0.1";
            txtClientPort.Text = "11000";
            txtUsername.Text = "Developer_" + new Random().Next(100, 999);
            
            UpdateClientUIStates(false);
        }

        #region Server Side Controls Handler

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                int tcpPort = int.Parse(txtServerPort.Text.Trim());
                int udpPort = tcpPort + 1; // Always bind UDP on TCP + 1

                _server.Start(tcpPort, udpPort);
                
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
                lblServerStatus.Text = "Server status: ONLINE (Listening)";
                lblServerStatus.ForeColor = Color.Teal;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not initialize listeners: {ex.Message}", "Socket Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            _server.Stop();
            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
            lblServerStatus.Text = "Server status: OFFLINE";
            lblServerStatus.ForeColor = Color.IndianRed;
            dgvConnectedClients.Rows.Clear();
        }

        private void Server_OnLogEvent(string logLine)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Server_OnLogEvent(logLine)));
                return;
            }
            AppendServerLog(logLine);
        }

        private void Server_OnClientListUpdated(List<ClientSession> sessions)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Server_OnClientListUpdated(sessions)));
                return;
            }

            dgvConnectedClients.Rows.Clear();
            foreach (var session in sessions)
            {
                dgvConnectedClients.Rows.Add(
                    session.SessionId, 
                    session.Username, 
                    session.RemoteEndpoint, 
                    session.JoinTime.ToString("HH:mm:ss")
                );
            }
            lblActiveClientsCount.Text = $"Connected Nodes: {sessions.Count}";
        }

        private void Server_OnClientTypingReceived(string username, string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Server_OnClientTypingReceived(username, status)));
                return;
            }

            if (string.IsNullOrEmpty(status))
            {
                lblTypingIndicator.Text = "Nobody is typing";
                lblTypingIndicator.ForeColor = Color.Gray;
            }
            else
            {
                lblTypingIndicator.Text = $"{username} {status}";
                lblTypingIndicator.ForeColor = Color.DarkCyan;
            }
        }

        private void AppendServerLog(string text)
        {
            rtbServerLog.AppendText(text + Environment.NewLine);
            rtbServerLog.SelectionStart = rtbServerLog.TextLength;
            rtbServerLog.ScrollToCaret();
        }

        #endregion

        #region Client Side Controls Handler

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string ip = txtClientIP.Text.Trim();
            int tcpPort = int.Parse(txtClientPort.Text.Trim());
            int udpPort = tcpPort + 1;
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a valid username.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConnect.Enabled = false;
            bool success = await _client.ConnectAsync(ip, tcpPort, udpPort, username);
            if (!success)
            {
                btnConnect.Enabled = true;
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await _client.DisconnectAsync();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendChatMessage();
        }

        private async Task SendChatMessage()
        {
            string msg = txtMessageInput.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;

            await _client.SendMessageAsync(msg);
            txtMessageInput.Clear();
            
            // Explicitly notify that we stopped typing
            _typingStopTimer.Stop();
            TypingStopTimer_Tick(this, EventArgs.Empty);
        }

        private void Client_OnLogEvent(string logLine)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Client_OnLogEvent(logLine)));
                return;
            }
            AppendClientLog(logLine);
        }

        private void Client_OnPacketReceived(NetworkPacket packet)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Client_OnPacketReceived(packet)));
                return;
            }

            // Visual formatting according to HCI messaging standard standards
            string timestamp = packet.Timestamp.ToString("HH:mm:ss");
            if (packet.Type == PacketType.SYS)
            {
                rtbChatFeed.SelectionColor = Color.DarkGoldenrod;
                rtbChatFeed.AppendText($"[{timestamp}] SYSTEM ALERT: {packet.Payload}{Environment.NewLine}");
            }
            else if (packet.Type == PacketType.MSG)
            {
                bool isMe = packet.Sender.Equals(_client.Username, StringComparison.OrdinalIgnoreCase);
                
                rtbChatFeed.SelectionFont = new Font(rtbChatFeed.Font, FontStyle.Bold);
                rtbChatFeed.SelectionColor = isMe ? Color.DodgerBlue : Color.MediumSlateBlue;
                rtbChatFeed.AppendText($"[{timestamp}] {packet.Sender}: ");
                
                rtbChatFeed.SelectionFont = new Font(rtbChatFeed.Font, FontStyle.Regular);
                rtbChatFeed.SelectionColor = Color.Black;
                rtbChatFeed.AppendText($"{packet.Payload}{Environment.NewLine}");
            }
            else if (packet.Type == PacketType.TYPING)
            {
                bool isTyping = packet.Payload == "1";
                UpdateRemoteTypingState(packet.Sender, isTyping);
            }
            
            rtbChatFeed.SelectionStart = rtbChatFeed.TextLength;
            rtbChatFeed.ScrollToCaret();
        }

        private void Client_OnConnectionStateChanged(bool connected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Client_OnConnectionStateChanged(connected)));
                return;
            }

            if (!connected)
            {
                _remoteTypingStates.Clear();
                RefreshTypingIndicator();
            }

            UpdateClientUIStates(connected);
        }

        private void UpdateClientUIStates(bool isConnected)
        {
            btnConnect.Enabled = !isConnected;
            btnDisconnect.Enabled = isConnected;
            btnSend.Enabled = isConnected;
            txtMessageInput.ReadOnly = !isConnected;
            txtUsername.ReadOnly = isConnected;
            txtClientIP.ReadOnly = isConnected;
            txtClientPort.ReadOnly = isConnected;

            lblConnectionStatus.Text = isConnected 
                ? $"Connected to Node: {_client.Username}" 
                : "Status: Disconnected";
            lblConnectionStatus.ForeColor = isConnected ? Color.Green : Color.Red;
        }

        private void AppendClientLog(string text)
        {
            rtbClientConsole.AppendText(text + Environment.NewLine);
            rtbClientConsole.SelectionStart = rtbClientConsole.TextLength;
            rtbClientConsole.ScrollToCaret();
        }

        private void UpdateRemoteTypingState(string username, bool isTyping)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Equals(_client.Username, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (isTyping)
            {
                _remoteTypingStates[username] = DateTime.UtcNow;
            }
            else
            {
                _remoteTypingStates.Remove(username);
            }

            RefreshTypingIndicator();
        }

        private void RefreshTypingIndicator()
        {
            if (_remoteTypingStates.Count == 0)
            {
                lblTypingIndicator.Text = "Nobody is typing";
                lblTypingIndicator.ForeColor = Color.Gray;
                return;
            }

            var activeNames = string.Join(", ", _remoteTypingStates.Keys);
            lblTypingIndicator.Text = _remoteTypingStates.Count == 1
                ? $"{activeNames} is typing..."
                : $"{activeNames} are typing...";
            lblTypingIndicator.ForeColor = Color.DarkCyan;
        }

        private void TypingIndicatorRefreshTimer_Tick(object? sender, EventArgs e)
        {
            var now = DateTime.UtcNow;
            var staleUsers = new List<string>();
            foreach (var item in _remoteTypingStates)
            {
                if ((now - item.Value).TotalMilliseconds > 2000)
                {
                    staleUsers.Add(item.Key);
                }
            }

            foreach (var user in staleUsers)
            {
                _remoteTypingStates.Remove(user);
            }

            if (staleUsers.Count > 0)
            {
                RefreshTypingIndicator();
            }
        }

        private void txtMessageInput_TextChanged(object sender, EventArgs e)
        {
            if (!_client.IsConnected) return;

            if (!_isUserTyping)
            {
                _isUserTyping = true;
                _client.SendTypingTelemetry(true);
            }

            // Reset the timeout timer
            _typingStopTimer.Stop();
            _typingStopTimer.Start();
        }

        private void TypingStopTimer_Tick(object? sender, EventArgs e)
        {
            _typingStopTimer.Stop();
            if (_isUserTyping)
            {
                _isUserTyping = false;
                _client.SendTypingTelemetry(false);
            }
        }

        private void txtMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Stop Windows notification ding sounds
                btnSend.PerformClick();
            }
        }

        #endregion
    }
}