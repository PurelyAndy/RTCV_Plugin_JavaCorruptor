namespace JavaTemplatePlugin
{
    partial class Minecraft
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.cbNoVerify = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.nmRam = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tbArgs = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmRam)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbInfo);
            this.panel1.Location = new System.Drawing.Point(13, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 63);
            this.panel1.TabIndex = 42;
            this.panel1.Tag = "color:normal";
            // 
            // lbInfo
            // 
            this.lbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInfo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbInfo.ForeColor = System.Drawing.Color.White;
            this.lbInfo.Location = new System.Drawing.Point(0, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(274, 63);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "== Corrupt Minecraft ==\r\nSelect the version, username, and other options below, t" +
    "hen click \"Load targets into RTCV\" on the right.";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbArgs);
            this.panel2.Controls.Add(this.nmRam);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbUsername);
            this.panel2.Controls.Add(this.cbNoVerify);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbVersion);
            this.panel2.Location = new System.Drawing.Point(13, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 183);
            this.panel2.TabIndex = 41;
            this.panel2.Tag = "color:normal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 172;
            this.label3.Text = "Max RAM usage (in GiB):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 171;
            this.label2.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tbUsername.ForeColor = System.Drawing.Color.White;
            this.tbUsername.Location = new System.Drawing.Point(3, 74);
            this.tbUsername.MaxLength = 400;
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(268, 22);
            this.tbUsername.TabIndex = 170;
            this.tbUsername.TabStop = false;
            this.tbUsername.Tag = "color:dark1";
            this.tbUsername.WordWrap = false;
            // 
            // cbNoVerify
            // 
            this.cbNoVerify.AutoSize = true;
            this.cbNoVerify.Checked = true;
            this.cbNoVerify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNoVerify.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbNoVerify.ForeColor = System.Drawing.Color.White;
            this.cbNoVerify.Location = new System.Drawing.Point(3, 42);
            this.cbNoVerify.Name = "cbNoVerify";
            this.cbNoVerify.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbNoVerify.Size = new System.Drawing.Size(142, 17);
            this.cbNoVerify.TabIndex = 169;
            this.cbNoVerify.Text = "Launch with -noverify?";
            this.cbNoVerify.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "Minecraft version:";
            // 
            // cbVersion
            // 
            this.cbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbVersion.ForeColor = System.Drawing.Color.White;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.IntegralHeight = false;
            this.cbVersion.Location = new System.Drawing.Point(3, 19);
            this.cbVersion.MaxDropDownItems = 15;
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(268, 21);
            this.cbVersion.TabIndex = 161;
            this.cbVersion.Tag = "color:dark1";
            // 
            // nmRam
            // 
            this.nmRam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nmRam.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmRam.ForeColor = System.Drawing.Color.White;
            this.nmRam.Location = new System.Drawing.Point(3, 113);
            this.nmRam.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nmRam.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmRam.Name = "nmRam";
            this.nmRam.Size = new System.Drawing.Size(268, 22);
            this.nmRam.TabIndex = 173;
            this.nmRam.Tag = "color:dark1";
            this.nmRam.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 175;
            this.label4.Text = "(Optional) Extra JVM arguments:";
            // 
            // tbArgs
            // 
            this.tbArgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbArgs.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tbArgs.ForeColor = System.Drawing.Color.White;
            this.tbArgs.Location = new System.Drawing.Point(3, 153);
            this.tbArgs.MaxLength = 400;
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(268, 22);
            this.tbArgs.TabIndex = 174;
            this.tbArgs.TabStop = false;
            this.tbArgs.Tag = "color:dark1";
            this.tbArgs.WordWrap = false;
            // 
            // Minecraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Minecraft";
            this.Tag = "color:dark1";
            this.Text = "FileStubTemplateJava";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmRam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbInfo;
        public System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cbNoVerify;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown nmRam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbArgs;
    }
}