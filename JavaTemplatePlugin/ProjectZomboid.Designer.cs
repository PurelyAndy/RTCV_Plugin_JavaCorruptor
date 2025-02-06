namespace JavaTemplatePlugin
{
    partial class ProjectZomboid
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
            this.cbNoVerify = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbInfo);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 224);
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
            this.lbInfo.Size = new System.Drawing.Size(276, 224);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "== Corrupt Project Zomboid==\r\nClick \'Browse Target\' and select ProjectZomboid64.b" +
    "at, or drag and drop it into the box. Then, click \"Load targets into RTCV\" on th" +
    "e right.\r\n";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel2.Controls.Add(this.cbNoVerify);
            this.panel2.Location = new System.Drawing.Point(12, 242);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 26);
            this.panel2.TabIndex = 43;
            this.panel2.Tag = "color:normal";
            // 
            // cbNoVerify
            // 
            this.cbNoVerify.AutoSize = true;
            this.cbNoVerify.Checked = true;
            this.cbNoVerify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNoVerify.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbNoVerify.ForeColor = System.Drawing.Color.White;
            this.cbNoVerify.Location = new System.Drawing.Point(3, 5);
            this.cbNoVerify.Name = "cbNoVerify";
            this.cbNoVerify.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbNoVerify.Size = new System.Drawing.Size(142, 17);
            this.cbNoVerify.TabIndex = 170;
            this.cbNoVerify.Text = "Launch with -noverify?";
            this.cbNoVerify.UseVisualStyleBackColor = true;
            // 
            // ProjectZomboid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjectZomboid";
            this.Tag = "color:dark1";
            this.Text = "FileStubTemplateJava";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.CheckBox cbNoVerify;
    }
}