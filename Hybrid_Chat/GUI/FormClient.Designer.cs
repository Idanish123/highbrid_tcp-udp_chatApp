namespace Hybrid_Chat.GUI
{
    partial class FormClient
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
            this.pnlClientHeader.SuspendLayout();
            this.gbClientConfig.SuspendLayout();
            this.gbChatRoom.SuspendLayout();
            this.gbClientLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlClientHeader
            // 
            this.pnlClientHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlClientHeader.Controls.Add(this.lblClientTitle);
            this.pnlClientHeader.Location = new System.Drawing.Point(12, 12);
            this.pnlClientHeader.Name = "pnlClientHeader";
            this.pnlClientHeader.Size = new System.Drawing.Size(760, 40);
            this.pnlClientHeader.TabIndex = 0;
            this.pnlClientHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblClientTitle
            // 
            this.lblClientTitle.AutoSize = true;
            this.lblClientTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblClientTitle.ForeColor = System.Drawing.Color.White;
            this.lblClientTitle.Location = new System.Drawing.Point(10, 10);
            this.lblClientTitle.Name = "lblClientTitle";
            this.lblClientTitle.Size = new System.Drawing.Size(384, 17);
            this.lblClientTitle.TabIndex = 0;
            this.lblClientTitle.Text = "💻 CLIENT SUITE PLATFORM (SYNC HCI LAYOUT - CLIENT ONLY MODE)";
            // 
            // gbClientConfig
            // 
            this.gbClientConfig.Controls.Add(this.lblClientIP);
            this.gbClientConfig.Controls.Add(this.txtClientIP);
            this.gbClientConfig.Controls.Add(this.lblClientPort);
            this.gbClientConfig.Controls.Add(this.txtClientPort);
            this.gbClientConfig.Controls.Add(this.lblUsername);
            this.gbClientConfig.Controls.Add(this.txtUsername);
            this.gbClientConfig.Controls.Add(this.btnConnect);
            this.gbClientConfig.Controls.Add(this.btnDisconnect);
            this.gbClientConfig.Controls.Add(this.lblConnectionStatus);
            this.gbClientConfig.Location = new System.Drawing.Point(12, 60);
            this.gbClientConfig.Name = "gbClientConfig";
            this.gbClientConfig.Size = new System.Drawing.Size(760, 90);
            this.gbClientConfig.TabIndex = 1;
            this.gbClientConfig.TabStop = false;
            this.gbClientConfig.Text = "Connect Parameter Protocol";
            this.gbClientConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblClientIP
            // 
            this.lblClientIP.AutoSize = true;
            this.lblClientIP.Location = new System.Drawing.Point(10, 25);
            this.lblClientIP.Name = "lblClientIP";
            this.lblClientIP.Size = new System.Drawing.Size(64, 15);
            this.lblClientIP.TabIndex = 0;
            this.lblClientIP.Text = "IP Address:";
            // 
            // txtClientIP
            // 
            this.txtClientIP.Location = new System.Drawing.Point(80, 22);
            this.txtClientIP.Name = "txtClientIP";
            this.txtClientIP.Size = new System.Drawing.Size(120, 23);
            this.txtClientIP.TabIndex = 1;
            // 
            // lblClientPort
            // 
            this.lblClientPort.AutoSize = true;
            this.lblClientPort.Location = new System.Drawing.Point(210, 25);
            this.lblClientPort.Name = "lblClientPort";
            this.lblClientPort.Size = new System.Drawing.Size(30, 15);
            this.lblClientPort.TabIndex = 2;
            this.lblClientPort.Text = "Port:";
            // 
            // txtClientPort
            // 
            this.txtClientPort.Location = new System.Drawing.Point(245, 22);
            this.txtClientPort.Name = "txtClientPort";
            this.txtClientPort.Size = new System.Drawing.Size(60, 23);
            this.txtClientPort.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(315, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(35, 15);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Alias:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(355, 22);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 23);
            this.txtUsername.TabIndex = 5;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.SlateBlue;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(470, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(110, 28);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.Location = new System.Drawing.Point(590, 18);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(110, 28);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(10, 55);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(133, 15);
            this.lblConnectionStatus.TabIndex = 8;
            this.lblConnectionStatus.Text = "Status: Disconnected";
            // 
            // gbChatRoom
            // 
            this.gbChatRoom.Controls.Add(this.rtbChatFeed);
            this.gbChatRoom.Controls.Add(this.lblTypingIndicator);
            this.gbChatRoom.Controls.Add(this.txtMessageInput);
            this.gbChatRoom.Controls.Add(this.btnSend);
            this.gbChatRoom.Location = new System.Drawing.Point(12, 160);
            this.gbChatRoom.Name = "gbChatRoom";
            this.gbChatRoom.Size = new System.Drawing.Size(760, 320);
            this.gbChatRoom.TabIndex = 2;
            this.gbChatRoom.TabStop = false;
            this.gbChatRoom.Text = "Synchronized Chat Room Area";
            this.gbChatRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // rtbChatFeed
            // 
            this.rtbChatFeed.BackColor = System.Drawing.Color.White;
            this.rtbChatFeed.Location = new System.Drawing.Point(10, 22);
            this.rtbChatFeed.Name = "rtbChatFeed";
            this.rtbChatFeed.ReadOnly = true;
            this.rtbChatFeed.Size = new System.Drawing.Size(740, 210);
            this.rtbChatFeed.TabIndex = 0;
            this.rtbChatFeed.Text = string.Empty;
            this.rtbChatFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblTypingIndicator
            // 
            this.lblTypingIndicator.AutoSize = true;
            this.lblTypingIndicator.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblTypingIndicator.ForeColor = System.Drawing.Color.Gray;
            this.lblTypingIndicator.Location = new System.Drawing.Point(10, 240);
            this.lblTypingIndicator.Name = "lblTypingIndicator";
            this.lblTypingIndicator.Size = new System.Drawing.Size(98, 13);
            this.lblTypingIndicator.TabIndex = 1;
            this.lblTypingIndicator.Text = "Nobody is typing";
            this.lblTypingIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // txtMessageInput
            // 
            this.txtMessageInput.Location = new System.Drawing.Point(10, 260);
            this.txtMessageInput.Multiline = false;
            this.txtMessageInput.Name = "txtMessageInput";
            this.txtMessageInput.ReadOnly = true;
            this.txtMessageInput.Size = new System.Drawing.Size(660, 23);
            this.txtMessageInput.TabIndex = 2;
            this.txtMessageInput.TextChanged += new System.EventHandler(this.txtMessageInput_TextChanged);
            this.txtMessageInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessageInput_KeyDown);
            this.txtMessageInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(680, 259);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(70, 26);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "📧 Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Enabled = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // gbClientLogs
            // 
            this.gbClientLogs.Controls.Add(this.rtbClientConsole);
            this.gbClientLogs.Location = new System.Drawing.Point(12, 490);
            this.gbClientLogs.Name = "gbClientLogs";
            this.gbClientLogs.Size = new System.Drawing.Size(760, 120);
            this.gbClientLogs.TabIndex = 3;
            this.gbClientLogs.TabStop = false;
            this.gbClientLogs.Text = "Client Log Stream Activity Logs";
            this.gbClientLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // rtbClientConsole
            // 
            this.rtbClientConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.rtbClientConsole.ForeColor = System.Drawing.Color.MediumPurple;
            this.rtbClientConsole.Location = new System.Drawing.Point(10, 20);
            this.rtbClientConsole.Name = "rtbClientConsole";
            this.rtbClientConsole.ReadOnly = true;
            this.rtbClientConsole.Size = new System.Drawing.Size(740, 90);
            this.rtbClientConsole.TabIndex = 0;
            this.rtbClientConsole.Text = string.Empty;
            this.rtbClientConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.gbClientLogs);
            this.Controls.Add(this.gbChatRoom);
            this.Controls.Add(this.gbClientConfig);
            this.Controls.Add(this.pnlClientHeader);
            this.Name = "FormClient";
            this.Text = "Hybrid_Chat Client";
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.pnlClientHeader.ResumeLayout(false);
            this.pnlClientHeader.PerformLayout();
            this.gbClientConfig.ResumeLayout(false);
            this.gbClientConfig.PerformLayout();
            this.gbChatRoom.ResumeLayout(false);
            this.gbChatRoom.PerformLayout();
            this.gbClientLogs.ResumeLayout(false);
            this.ResumeLayout(false);
        }

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
