namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    partial class SanitizerEngineControl : JavaEngineControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label19 = new System.Windows.Forms.Label();
            this.lbCurrentUnits = new System.Windows.Forms.Label();
            this.lbLoadedJBL = new System.Windows.Forms.Label();
            this.btnLoadJBL = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.btnStartRestart = new System.Windows.Forms.Button();
            this.lbTotalUnits = new System.Windows.Forms.Label();
            this.lbSelectedAnswer = new System.Windows.Forms.Label();
            this.ofdLoadJBL = new System.Windows.Forms.OpenFileDialog();
            this.btnEndAndSave = new System.Windows.Forms.Button();
            this.engineGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.btnEndAndSave);
            this.engineGroupBox.Controls.Add(this.lbSelectedAnswer);
            this.engineGroupBox.Controls.Add(this.lbTotalUnits);
            this.engineGroupBox.Controls.Add(this.btnStartRestart);
            this.engineGroupBox.Controls.Add(this.btnTryAgain);
            this.engineGroupBox.Controls.Add(this.btnNo);
            this.engineGroupBox.Controls.Add(this.btnYes);
            this.engineGroupBox.Controls.Add(this.btnLoadJBL);
            this.engineGroupBox.Controls.Add(this.lbLoadedJBL);
            this.engineGroupBox.Controls.Add(this.lbCurrentUnits);
            this.engineGroupBox.Controls.Add(this.label19);
            this.engineGroupBox.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbCurrentUnits, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbLoadedJBL, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnLoadJBL, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnYes, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnNo, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnTryAgain, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnStartRestart, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbTotalUnits, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbSelectedAnswer, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnEndAndSave, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(181, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Sanitization UI. Not really an engine.";
            // 
            // lbCurrentUnits
            // 
            this.lbCurrentUnits.AutoSize = true;
            this.lbCurrentUnits.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbCurrentUnits.ForeColor = System.Drawing.Color.White;
            this.lbCurrentUnits.Location = new System.Drawing.Point(3, 56);
            this.lbCurrentUnits.Name = "lbCurrentUnits";
            this.lbCurrentUnits.Size = new System.Drawing.Size(91, 13);
            this.lbCurrentUnits.TabIndex = 159;
            this.lbCurrentUnits.Text = "Enabled Units: 0";
            // 
            // lbLoadedJBL
            // 
            this.lbLoadedJBL.AutoSize = true;
            this.lbLoadedJBL.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbLoadedJBL.ForeColor = System.Drawing.Color.White;
            this.lbLoadedJBL.Location = new System.Drawing.Point(77, 37);
            this.lbLoadedJBL.Name = "lbLoadedJBL";
            this.lbLoadedJBL.Size = new System.Drawing.Size(48, 13);
            this.lbLoadedJBL.TabIndex = 160;
            this.lbLoadedJBL.Text = "Loaded:";
            // 
            // btnLoadJBL
            // 
            this.btnLoadJBL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadJBL.BackColor = System.Drawing.Color.Gray;
            this.btnLoadJBL.FlatAppearance.BorderSize = 0;
            this.btnLoadJBL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadJBL.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLoadJBL.ForeColor = System.Drawing.Color.White;
            this.btnLoadJBL.Location = new System.Drawing.Point(6, 33);
            this.btnLoadJBL.Name = "btnLoadJBL";
            this.btnLoadJBL.Size = new System.Drawing.Size(65, 20);
            this.btnLoadJBL.TabIndex = 163;
            this.btnLoadJBL.TabStop = false;
            this.btnLoadJBL.Tag = "color:light1";
            this.btnLoadJBL.Text = "Load JBL";
            this.btnLoadJBL.UseVisualStyleBackColor = false;
            this.btnLoadJBL.Click += new System.EventHandler(this.btnLoadJBL_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.BackColor = System.Drawing.Color.Gray;
            this.btnYes.FlatAppearance.BorderSize = 0;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(156, 118);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(65, 24);
            this.btnYes.TabIndex = 165;
            this.btnYes.TabStop = false;
            this.btnYes.Tag = "color:light1";
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.BackColor = System.Drawing.Color.Gray;
            this.btnNo.FlatAppearance.BorderSize = 0;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(227, 118);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(65, 24);
            this.btnNo.TabIndex = 166;
            this.btnNo.TabStop = false;
            this.btnNo.Tag = "color:light1";
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTryAgain.BackColor = System.Drawing.Color.Gray;
            this.btnTryAgain.FlatAppearance.BorderSize = 0;
            this.btnTryAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTryAgain.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnTryAgain.ForeColor = System.Drawing.Color.White;
            this.btnTryAgain.Location = new System.Drawing.Point(298, 118);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(65, 24);
            this.btnTryAgain.TabIndex = 167;
            this.btnTryAgain.TabStop = false;
            this.btnTryAgain.Tag = "color:light1";
            this.btnTryAgain.Text = "Try Again";
            this.btnTryAgain.UseVisualStyleBackColor = false;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // btnStartRestart
            // 
            this.btnStartRestart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartRestart.BackColor = System.Drawing.Color.Gray;
            this.btnStartRestart.Enabled = false;
            this.btnStartRestart.FlatAppearance.BorderSize = 0;
            this.btnStartRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartRestart.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnStartRestart.ForeColor = System.Drawing.Color.White;
            this.btnStartRestart.Location = new System.Drawing.Point(6, 118);
            this.btnStartRestart.Name = "btnStartRestart";
            this.btnStartRestart.Size = new System.Drawing.Size(65, 24);
            this.btnStartRestart.TabIndex = 168;
            this.btnStartRestart.TabStop = false;
            this.btnStartRestart.Tag = "color:light1";
            this.btnStartRestart.Text = "Start";
            this.btnStartRestart.UseVisualStyleBackColor = false;
            this.btnStartRestart.Click += new System.EventHandler(this.btnStartRestart_Click);
            // 
            // lbTotalUnits
            // 
            this.lbTotalUnits.AutoSize = true;
            this.lbTotalUnits.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTotalUnits.ForeColor = System.Drawing.Color.White;
            this.lbTotalUnits.Location = new System.Drawing.Point(3, 70);
            this.lbTotalUnits.Name = "lbTotalUnits";
            this.lbTotalUnits.Size = new System.Drawing.Size(94, 13);
            this.lbTotalUnits.TabIndex = 169;
            this.lbTotalUnits.Text = "Disabled Units: 0";
            // 
            // lbSelectedAnswer
            // 
            this.lbSelectedAnswer.AutoSize = true;
            this.lbSelectedAnswer.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbSelectedAnswer.ForeColor = System.Drawing.Color.White;
            this.lbSelectedAnswer.Location = new System.Drawing.Point(153, 102);
            this.lbSelectedAnswer.Name = "lbSelectedAnswer";
            this.lbSelectedAnswer.Size = new System.Drawing.Size(61, 13);
            this.lbSelectedAnswer.TabIndex = 170;
            this.lbSelectedAnswer.Text = "Selected: ?";
            // 
            // ofdLoadJBL
            // 
            this.ofdLoadJBL.Filter = "Java blast layers|*.jbl";
            // 
            // btnEndAndSave
            // 
            this.btnEndAndSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndAndSave.BackColor = System.Drawing.Color.Gray;
            this.btnEndAndSave.Enabled = false;
            this.btnEndAndSave.FlatAppearance.BorderSize = 0;
            this.btnEndAndSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndAndSave.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEndAndSave.ForeColor = System.Drawing.Color.White;
            this.btnEndAndSave.Location = new System.Drawing.Point(77, 118);
            this.btnEndAndSave.Name = "btnEndAndSave";
            this.btnEndAndSave.Size = new System.Drawing.Size(73, 24);
            this.btnEndAndSave.TabIndex = 171;
            this.btnEndAndSave.TabStop = false;
            this.btnEndAndSave.Tag = "color:light1";
            this.btnEndAndSave.Text = "End & Save";
            this.btnEndAndSave.UseMnemonic = false;
            this.btnEndAndSave.UseVisualStyleBackColor = false;
            this.btnEndAndSave.Click += new System.EventHandler(this.btnEndAndSave_Click);
            // 
            // SanitizerEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SanitizerEngineControl";
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbCurrentUnits;
        private System.Windows.Forms.Label lbLoadedJBL;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnLoadJBL;
        private System.Windows.Forms.Button btnStartRestart;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label lbSelectedAnswer;
        private System.Windows.Forms.Label lbTotalUnits;
        private System.Windows.Forms.OpenFileDialog ofdLoadJBL;
        private System.Windows.Forms.Button btnEndAndSave;
    }
}
