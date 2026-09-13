using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hybrid_Chat.Models;
using Hybrid_Chat.Services;

namespace Hybrid_Chat.GUI
{
    public partial class FormClient : Form
    {
        private readonly SyncChatClient _client;
        private bool _isUserTyping = false;
        private readonly System.Windows.Forms.Timer _typingStopTimer;
        private readonly System.Windows.Forms.Timer _typingIndicatorRefreshTimer;
        private readonly System.Collections.Generic.Dictionary<string, DateTime> _remoteTypingStates = new();

        public FormClient()
        {
            InitializeComponent();
            _client = new SyncChatClient();

            _typingStopTimer = new System.Windows.Forms.Timer();
            _typingStopTimer.Interval = 1500;
            _typingStopTimer.Tick += TypingStopTimer_Tick;

            _typingIndicatorRefreshTimer = new System.Windows.Forms.Timer();
            _typingIndicatorRefreshTimer.Interval = 500;
            _typingIndicatorRefreshTimer.Tick += TypingIndicatorRefreshTimer_Tick;
            _typingIndicatorRefreshTimer.Start();

            _client.OnLogEvent += Client_OnLogEvent;
            _client.OnPacketReceived += Client_OnPacketReceived;
            _client.OnConnectionStateChanged += Client_OnConnectionStateChanged;
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            AppendClientLog("[CLIENT STATUS] Ready to establish virtual socket stream.");
            txtClientIP.Text = "127.0.0.1";
            txtClientPort.Text = "11000";
            txtUsername.Text = "Developer_" + new Random().Next(100, 999);
            UpdateClientUIStates(false);
        }

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
            var staleUsers = new System.Collections.Generic.List<string>();
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
                e.SuppressKeyPress = true;
                btnSend.PerformClick();
            }
        }
    }
}
