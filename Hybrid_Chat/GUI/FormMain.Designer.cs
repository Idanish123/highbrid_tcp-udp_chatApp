namespace Hybrid_Chat.GUI
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlServerHeader = new System.Windows.Forms.Panel();
            this.lblServerTitle = new System.Windows.Forms.Label();
            this.gbServerControl = new System.Windows.Forms.GroupBox();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.gbServerActiveNodes = new System.Windows.Forms.GroupBox();
            this.dgvConnectedClients = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblActiveClientsCount = new System.Windows.Forms.Label();
            this.gbServerConsole = new System.Windows.Forms.GroupBox();
            this.rtbServerLog = new System.Windows.Forms.RichTextBox();
            this.pnlClientHeader = new System.Windows.Forms.Panel();
            this.lblClientTitle = new System.Windows.Forms.Label();
            this.gbClientConfig = new System.Windows.Forms.GroupBox();
            this.lblClientIP = new System.Windows.Forms.Label();
            this.txtClientIP = new System.Windows.Forms.TextBox();
            this.lblClientPort = new System.Windows.Forms.Label();
            this.txtClientPort = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.gbChatRoom = new System.Windows.Forms.GroupBox();
            this.rtbChatFeed = new System.Windows.Forms.RichTextBox();
            this.lblTypingIndicator = new System.Windows.Forms.Label();
            this.txtMessageInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.gbClientLogs = new System.Windows.Forms.GroupBox();
            this.rtbClientConsole = new System.Windows.Forms.RichTextBox();
            this.pnlMainSplit = new System.Windows.Forms.SplitContainer();
            this.gbServerControl.SuspendLayout();
            this.gbServerActiveNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnectedClients)).BeginInit();
            this.gbServerConsole.SuspendLayout();
            this.gbClientConfig.SuspendLayout();
            this.gbChatRoom.SuspendLayout();
            this.gbClientLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMainSplit)).BeginInit();
            this.pnlMainSplit.Panel1.SuspendLayout();
            this.pnlMainSplit.Panel2.SuspendLayout();
            this.pnlMainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainSplit
            // 
            this.pnlMainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainSplit.Location = new System.Drawing.Point(0, 0);
            this.pnlMainSplit.Name = "pnlMainSplit";
            // 
            // pnlMainSplit.Panel1
            // 
            this.pnlMainSplit.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.pnlMainSplit.Panel1.Controls.Add(this.pnlServerHeader);
            this.pnlMainSplit.Panel1.Controls.Add(this.gbServerControl);
            this.pnlMainSplit.Panel1.Controls.Add(this.gbServerActiveNodes);
            this.pnlMainSplit.Panel1.Controls.Add(this.gbServerConsole);
            // 
            // pnlMainSplit.Panel2
            // 
            this.pnlMainSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.pnlMainSplit.Panel2.Controls.Add(this.pnlClientHeader);
            this.pnlMainSplit.Panel2.Controls.Add(this.gbClientConfig);
            this.pnlMainSplit.Panel2.Controls.Add(this.gbChatRoom);
            this.pnlMainSplit.Panel2.Controls.Add(this.gbClientLogs);
            this.pnlMainSplit.Size = new System.Drawing.Size(1000, 620);
            this.pnlMainSplit.SplitterDistance = 480;
            this.pnlMainSplit.TabIndex = 0;
            // 
            // pnlServerHeader
            // 
            this.pnlServerHeader.BackColor = System.Drawing.Color.Teal;
            this.pnlServerHeader.Location = new System.Drawing.Point(12, 12);
            this.pnlServerHeader.Size = new System.Drawing.Size(456, 40);
            this.pnlServerHeader.Controls.Add(this.lblServerTitle);
            this.pnlServerHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblServerTitle
            // 
            this.lblServerTitle.Text = "📡 NETWORK SERVER HUB (TCP & UDP SOCKETS)";
            this.lblServerTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblServerTitle.ForeColor = System.Drawing.Color.White;
            this.lblServerTitle.Location = new System.Drawing.Point(10, 10);
            this.lblServerTitle.Size = new System.Drawing.Size(400, 20);
            // 
            // gbServerControl
            // 
            this.gbServerControl.Text = "Engine Configuration";
            this.gbServerControl.Location = new System.Drawing.Point(12, 60);
            this.gbServerControl.Size = new System.Drawing.Size(456, 85);
            this.gbServerControl.Controls.Add(this.lblServerPort); 
            this.gbServerControl.Controls.Add(this.txtServerPort);
            this.gbServerControl.Controls.Add(this.btnStartServer);
            this.gbServerControl.Controls.Add(this.btnStopServer);
            this.gbServerControl.Controls.Add(this.lblServerStatus);
            this.gbServerControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblServerPort
            // 
            this.lblServerPort.Text = "TCP Port:";
            this.lblServerPort.Location = new System.Drawing.Point(15, 25);
            this.lblServerPort.Size = new System.Drawing.Size(60, 20);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(75, 22);
            this.txtServerPort.Size = new System.Drawing.Size(80, 23);
            // 
            // btnStartServer
            // 
            this.btnStartServer.Text = "▶ Start Server";
            this.btnStartServer.Location = new System.Drawing.Point(170, 21);
            this.btnStartServer.Size = new System.Drawing.Size(130, 26);
            this.btnStartServer.BackColor = System.Drawing.Color.Teal;
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.Text = "■ Stop";
            this.btnStopServer.Location = new System.Drawing.Point(308, 21);
            this.btnStopServer.Size = new System.Drawing.Size(130, 26);
            this.btnStopServer.BackColor = System.Drawing.Color.IndianRed;
            this.btnStopServer.ForeColor = System.Drawing.Color.White;
            this.btnStopServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopServer.Enabled = false;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.Text = "Server status: OFFLINE";
            this.lblServerStatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lblServerStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblServerStatus.Location = new System.Drawing.Point(15, 55);
            this.lblServerStatus.Size = new System.Drawing.Size(300, 20);
            // 
            // gbServerActiveNodes
            // 
            this.gbServerActiveNodes.Text = "Active Client Sessions (TCP Streaming Nodes)";
            this.gbServerActiveNodes.Location = new System.Drawing.Point(12, 155);
            this.gbServerActiveNodes.Size = new System.Drawing.Size(456, 210);
            this.gbServerActiveNodes.Controls.Add(this.dgvConnectedClients);
            this.gbServerActiveNodes.Controls.Add(this.lblActiveClientsCount);
            this.gbServerActiveNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // dgvConnectedClients
            // 
            this.dgvConnectedClients.AllowUserToAddRows = false;
            this.dgvConnectedClients.AllowUserToDeleteRows = false;
            this.dgvConnectedClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConnectedClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colUser,
            this.colIp,
            this.colTime});
            this.dgvConnectedClients.Location = new System.Drawing.Point(10, 22);
            this.dgvConnectedClients.Size = new System.Drawing.Size(436, 155);
            this.dgvConnectedClients.ReadOnly = true;
            this.dgvConnectedClients.RowHeadersVisible = false;
            this.dgvConnectedClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConnectedClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.Width = 60;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "Username";
            // 
            // colIp
            // 
            this.colIp.HeaderText = "Remote Port/IP";
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Logged In";
            // 
            // lblActiveClientsCount
            // 
            this.lblActiveClientsCount.Text = "Connected Nodes: 0";
            this.lblActiveClientsCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblActiveClientsCount.Location = new System.Drawing.Point(10, 185);
            this.lblActiveClientsCount.Size = new System.Drawing.Size(200, 20);
            this.lblActiveClientsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // gbServerConsole
            // 
            this.gbServerConsole.Text = "Live Realtime Sockets Server Activity Monitor";
            this.gbServerConsole.Location = new System.Drawing.Point(12, 375);
            this.gbServerConsole.Size = new System.Drawing.Size(456, 230);
            this.gbServerConsole.Controls.Add(this.rtbServerLog);
            this.gbServerConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right))));
            // 
            // rtbServerLog
            // 
            this.rtbServerLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rtbServerLog.ForeColor = System.Drawing.Color.Lime;
            this.rtbServerLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.rtbServerLog.Location = new System.Drawing.Point(10, 22);
            this.rtbServerLog.Size = new System.Drawing.Size(436, 195);
            this.rtbServerLog.ReadOnly = true;
            this.rtbServerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // pnlClientHeader
            // 
            this.pnlClientHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlClientHeader.Location = new System.Drawing.Point(12, 12);
            this.pnlClientHeader.Size = new System.Drawing.Size(480, 40);
            this.pnlClientHeader.Controls.Add(this.lblClientTitle);
            this.pnlClientHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblClientTitle
            // 
            this.lblClientTitle.Text = "💻 CLIENT SUITE PLATFORM (SYNC HCI LAYOUT)";
            this.lblClientTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblClientTitle.ForeColor = System.Drawing.Color.White;
            this.lblClientTitle.Location = new System.Drawing.Point(10, 10);
            this.lblClientTitle.Size = new System.Drawing.Size(400, 20);
            // 
            // gbClientConfig
            // 
            this.gbClientConfig.Text = "Connect Parameter Protocol";
            this.gbClientConfig.Location = new System.Drawing.Point(12, 60);
            this.gbClientConfig.Size = new System.Drawing.Size(480, 85);
            this.gbClientConfig.Controls.Add(this.lblClientIP);
            this.gbClientConfig.Controls.Add(this.txtClientIP);
            this.gbClientConfig.Controls.Add(this.lblClientPort);
            this.gbClientConfig.Controls.Add(this.txtClientPort);
            this.gbClientConfig.Controls.Add(this.lblUsername);
            this.gbClientConfig.Controls.Add(this.txtUsername);
            this.gbClientConfig.Controls.Add(this.btnConnect);
            this.gbClientConfig.Controls.Add(this.btnDisconnect);
            this.gbClientConfig.Controls.Add(this.lblConnectionStatus);
            this.gbClientConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblClientIP
            // 
            this.lblClientIP.Text = "IP Address:";
            this.lblClientIP.Location = new System.Drawing.Point(10, 25);
            this.lblClientIP.Size = new System.Drawing.Size(70, 20);
            // 
            // txtClientIP
            // 
            this.txtClientIP.Location = new System.Drawing.Point(80, 22);
            this.txtClientIP.Size = new System.Drawing.Size(80, 23);
            // 
            // lblClientPort
            // 
            this.lblClientPort.Text = "Port:";
            this.lblClientPort.Location = new System.Drawing.Point(165, 25);
            this.lblClientPort.Size = new System.Drawing.Size(35, 20);
            // 
            // txtClientPort
            // 
            this.txtClientPort.Location = new System.Drawing.Point(200, 22);
            this.txtClientPort.Size = new System.Drawing.Size(55, 23);
            // 
            // lblUsername
            // 
            this.lblUsername.Text = "Alias:";
            this.lblUsername.Location = new System.Drawing.Point(260, 25);
            this.lblUsername.Size = new System.Drawing.Size(40, 20);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(300, 22);
            this.txtUsername.Size = new System.Drawing.Size(75, 23);
            // 
            // btnConnect
            // 
            this.btnConnect.Text = "Connect";
            this.btnConnect.Location = new System.Drawing.Point(380, 18);
            this.btnConnect.Size = new System.Drawing.Size(90, 28);
            this.btnConnect.BackColor = System.Drawing.Color.SlateBlue;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Text = "Exit";
            this.btnDisconnect.Location = new System.Drawing.Point(380, 50);
            this.btnDisconnect.Size = new System.Drawing.Size(90, 28);
            this.btnDisconnect.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Text = "Status: Disconnected";
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.Location = new System.Drawing.Point(10, 55);
            this.lblConnectionStatus.Size = new System.Drawing.Size(350, 20);
            // 
            // gbChatRoom
            // 
            this.gbChatRoom.Text = "Synchronized Chat Room Area";
            this.gbChatRoom.Location = new System.Drawing.Point(12, 155);
            this.gbChatRoom.Size = new System.Drawing.Size(480, 310);
            this.gbChatRoom.Controls.Add(this.rtbChatFeed);
            this.gbChatRoom.Controls.Add(this.lblTypingIndicator); 
            this.gbChatRoom.Controls.Add(this.txtMessageInput);
            this.gbChatRoom.Controls.Add(this.btnSend);
            this.gbChatRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // rtbChatFeed
            // 
            this.rtbChatFeed.BackColor = System.Drawing.Color.White;
            this.rtbChatFeed.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rtbChatFeed.Location = new System.Drawing.Point(10, 22);
            this.rtbChatFeed.Size = new System.Drawing.Size(460, 210);
            this.rtbChatFeed.ReadOnly = true;
            this.rtbChatFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblTypingIndicator
            // 
            this.lblTypingIndicator.Text = "Nobody is typing";
            this.lblTypingIndicator.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblTypingIndicator.ForeColor = System.Drawing.Color.Gray;
            this.lblTypingIndicator.Location = new System.Drawing.Point(10, 240);
            this.lblTypingIndicator.Size = new System.Drawing.Size(350, 15);
            this.lblTypingIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // txtMessageInput
            // 
            this.txtMessageInput.Location = new System.Drawing.Point(10, 260);
            this.txtMessageInput.Size = new System.Drawing.Size(370, 40);
            this.txtMessageInput.Multiline = false;
            this.txtMessageInput.TextChanged += new System.EventHandler(this.txtMessageInput_TextChanged);
            this.txtMessageInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessageInput_KeyDown);
            this.txtMessageInput.ReadOnly = true;
            this.txtMessageInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnSend
            // 
            this.btnSend.Text = "📧 Send";
            this.btnSend.Location = new System.Drawing.Point(390, 259);
            this.btnSend.Size = new System.Drawing.Size(80, 26);
            this.btnSend.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Enabled = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // gbClientLogs
            // 
            this.gbClientLogs.Text = "Client Log Stream Activity Logs";
            this.gbClientLogs.Location = new System.Drawing.Point(12, 475);
            this.gbClientLogs.Size = new System.Drawing.Size(480, 130);
            this.gbClientLogs.Controls.Add(this.rtbClientConsole);
            this.gbClientLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // rtbClientConsole
            // 
            this.rtbClientConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.rtbClientConsole.ForeColor = System.Drawing.Color.MediumPurple;
            this.rtbClientConsole.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.rtbClientConsole.Location = new System.Drawing.Point(10, 20);
            this.rtbClientConsole.Size = new System.Drawing.Size(460, 95);
            this.rtbClientConsole.ReadOnly = true;
            this.rtbClientConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.pnlMainSplit);
            this.Name = "FormMain";
            this.Text = "Hybrid_Chat Engine (TCP/UDP Synchronized Socket System)";
            this.Load += new System.EventHandler(this.FormMain_Load); 
            this.gbServerControl.ResumeLayout(false);
            this.gbServerControl.PerformLayout();
            this.gbServerActiveNodes.ResumeLayout(false);
            this.gbServerActiveNodes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnectedClients)).EndInit();
            this.gbServerConsole.ResumeLayout(false);
            this.gbClientConfig.ResumeLayout(false);
            this.gbClientConfig.PerformLayout();
            this.gbChatRoom.ResumeLayout(false);
            this.gbChatRoom.PerformLayout();
            this.gbClientLogs.ResumeLayout(false);
            this.pnlMainSplit.Panel1.ResumeLayout(false);
            this.pnlMainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMainSplit)).EndInit();
            this.pnlMainSplit.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer pnlMainSplit;
        private System.Windows.Forms.Panel pnlServerHeader;
        private System.Windows.Forms.Label lblServerTitle;
        private System.Windows.Forms.GroupBox gbServerControl;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.GroupBox gbServerActiveNodes;
        private System.Windows.Forms.DataGridView dgvConnectedClients;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.Label lblActiveClientsCount;
        private System.Windows.Forms.GroupBox gbServerConsole;
        private System.Windows.Forms.RichTextBox rtbServerLog;
        
        private System.Windows.Forms.Panel pnlClientHeader;
        private System.Windows.Forms.Label lblClientTitle;
        private System.Windows.Forms.GroupBox gbClientConfig;
        private System.Windows.Forms.Label lblClientIP;
        private System.Windows.Forms.TextBox txtClientIP;
        private System.Windows.Forms.Label lblClientPort;
        private System.Windows.Forms.TextBox txtClientPort;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.GroupBox gbChatRoom;
        private System.Windows.Forms.RichTextBox rtbChatFeed;
        private System.Windows.Forms.Label lblTypingIndicator;
        private System.Windows.Forms.TextBox txtMessageInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox gbClientLogs;
        private System.Windows.Forms.RichTextBox rtbClientConsole;
    }
}