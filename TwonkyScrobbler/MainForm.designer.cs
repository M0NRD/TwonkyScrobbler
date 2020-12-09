namespace TwonkyScrobbler
{
    partial class Scrobbler
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scrobbler));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.titleText = new System.Windows.Forms.Label();
            this.artistText = new System.Windows.Forms.Label();
            this.albumText = new System.Windows.Forms.Label();
            this.playListButton = new System.Windows.Forms.Button();
            this.optionsButton = new System.Windows.Forms.Button();
            this.supportButton = new System.Windows.Forms.Button();
            this.artistLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeText = new System.Windows.Forms.Label();
            this.urlLabel = new System.Windows.Forms.Label();
            this.urlText = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusText = new System.Windows.Forms.Label();
            this.logButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // titleText
            // 
            this.titleText.AutoSize = true;
            this.titleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleText.ForeColor = System.Drawing.Color.Blue;
            this.titleText.Location = new System.Drawing.Point(45, 23);
            this.titleText.Name = "titleText";
            this.titleText.Size = new System.Drawing.Size(19, 13);
            this.titleText.TabIndex = 0;
            this.titleText.Text = "...";
            // 
            // artistText
            // 
            this.artistText.AutoSize = true;
            this.artistText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artistText.ForeColor = System.Drawing.Color.Blue;
            this.artistText.Location = new System.Drawing.Point(45, 10);
            this.artistText.Name = "artistText";
            this.artistText.Size = new System.Drawing.Size(19, 13);
            this.artistText.TabIndex = 1;
            this.artistText.Text = "...";
            // 
            // albumText
            // 
            this.albumText.AutoSize = true;
            this.albumText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumText.ForeColor = System.Drawing.Color.Blue;
            this.albumText.Location = new System.Drawing.Point(45, 36);
            this.albumText.Name = "albumText";
            this.albumText.Size = new System.Drawing.Size(19, 13);
            this.albumText.TabIndex = 2;
            this.albumText.Text = "...";
            // 
            // playListButton
            // 
            this.playListButton.Location = new System.Drawing.Point(393, 134);
            this.playListButton.Name = "playListButton";
            this.playListButton.Size = new System.Drawing.Size(90, 22);
            this.playListButton.TabIndex = 3;
            this.playListButton.Text = "Server RSS";
            this.playListButton.UseVisualStyleBackColor = true;
            this.playListButton.Click += new System.EventHandler(this.playListButton_Click);
            // 
            // optionsButton
            // 
            this.optionsButton.Location = new System.Drawing.Point(3, 134);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(90, 23);
            this.optionsButton.TabIndex = 4;
            this.optionsButton.Text = "Options";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // supportButton
            // 
            this.supportButton.Location = new System.Drawing.Point(393, 105);
            this.supportButton.Name = "supportButton";
            this.supportButton.Size = new System.Drawing.Size(90, 23);
            this.supportButton.TabIndex = 5;
            this.supportButton.Text = "TwonkyVision";
            this.supportButton.UseVisualStyleBackColor = true;
            this.supportButton.Click += new System.EventHandler(this.supportButton_Click);
            // 
            // artistLabel
            // 
            this.artistLabel.AutoSize = true;
            this.artistLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artistLabel.ForeColor = System.Drawing.Color.Black;
            this.artistLabel.Location = new System.Drawing.Point(3, 10);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(36, 13);
            this.artistLabel.TabIndex = 6;
            this.artistLabel.Text = "Artist";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(3, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(32, 13);
            this.titleLabel.TabIndex = 7;
            this.titleLabel.Text = "Title";
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumLabel.ForeColor = System.Drawing.Color.Black;
            this.albumLabel.Location = new System.Drawing.Point(3, 36);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(41, 13);
            this.albumLabel.TabIndex = 8;
            this.albumLabel.Text = "Album";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::TwonkyScrobbler.Properties.Resources.logo;
            this.pictureBox1.InitialImage = global::TwonkyScrobbler.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(393, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 90);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.Black;
            this.usernameLabel.Location = new System.Drawing.Point(6, 83);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(63, 13);
            this.usernameLabel.TabIndex = 10;
            this.usernameLabel.Text = "Username";
            // 
            // usernameText
            // 
            this.usernameText.AutoSize = true;
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.Location = new System.Drawing.Point(66, 83);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(16, 13);
            this.usernameText.TabIndex = 11;
            this.usernameText.Text = "...";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Black;
            this.timeLabel.Location = new System.Drawing.Point(3, 49);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(34, 13);
            this.timeLabel.TabIndex = 12;
            this.timeLabel.Text = "Time";
            // 
            // timeText
            // 
            this.timeText.AutoSize = true;
            this.timeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeText.ForeColor = System.Drawing.Color.Blue;
            this.timeText.Location = new System.Drawing.Point(45, 49);
            this.timeText.Name = "timeText";
            this.timeText.Size = new System.Drawing.Size(19, 13);
            this.timeText.TabIndex = 13;
            this.timeText.Text = "...";
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlLabel.ForeColor = System.Drawing.Color.Black;
            this.urlLabel.Location = new System.Drawing.Point(6, 96);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(52, 13);
            this.urlLabel.TabIndex = 14;
            this.urlLabel.Text = "RSS Url";
            // 
            // urlText
            // 
            this.urlText.AutoSize = true;
            this.urlText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlText.LinkColor = System.Drawing.Color.Blue;
            this.urlText.Location = new System.Drawing.Point(66, 96);
            this.urlText.Name = "urlText";
            this.urlText.Size = new System.Drawing.Size(16, 13);
            this.urlText.TabIndex = 15;
            this.urlText.TabStop = true;
            this.urlText.Text = "...";
            this.urlText.VisitedLinkColor = System.Drawing.Color.Blue;
            this.urlText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.urlText_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.artistLabel);
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Controls.Add(this.albumLabel);
            this.panel1.Controls.Add(this.timeText);
            this.panel1.Controls.Add(this.timeLabel);
            this.panel1.Controls.Add(this.artistText);
            this.panel1.Controls.Add(this.titleText);
            this.panel1.Controls.Add(this.albumText);
            this.panel1.Location = new System.Drawing.Point(3, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 71);
            this.panel1.TabIndex = 16;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Black;
            this.statusLabel.Location = new System.Drawing.Point(7, 110);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(43, 13);
            this.statusLabel.TabIndex = 17;
            this.statusLabel.Text = "Status";
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.Location = new System.Drawing.Point(66, 110);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(45, 13);
            this.statusText.TabIndex = 18;
            this.statusText.Text = "Inactive";
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(99, 134);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(90, 23);
            this.logButton.TabIndex = 19;
            this.logButton.Text = "View Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // Scrobbler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(489, 163);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.urlText);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.supportButton);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.playListButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 166);
            this.Name = "Scrobbler";
            this.ShowInTaskbar = false;
            this.Text = "TwonkyMedia Scrobbler";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label titleText;
        private System.Windows.Forms.Label artistText;
        private System.Windows.Forms.Label albumText;
        private System.Windows.Forms.Button playListButton;
        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.Button supportButton;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label usernameText;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label timeText;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.LinkLabel urlText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Button logButton;
    }
}

