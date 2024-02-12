namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    partial class StringEngineControl : JavaEngineControl
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
            this.lbPercentage = new System.Windows.Forms.Label();
            this.tbPercentage = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.tbCharacters = new System.Windows.Forms.TextBox();
            this.lbMode = new System.Windows.Forms.Label();
            this.engineGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.lbMode);
            this.engineGroupBox.Controls.Add(this.tbCharacters);
            this.engineGroupBox.Controls.Add(this.cbMode);
            this.engineGroupBox.Controls.Add(this.label1);
            this.engineGroupBox.Controls.Add(this.lbPercentage);
            this.engineGroupBox.Controls.Add(this.tbPercentage);
            this.engineGroupBox.Controls.Add(this.label19);
            this.engineGroupBox.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tbPercentage, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbPercentage, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbMode, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tbCharacters, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbMode, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Screws with text";
            // 
            // lbPercentage
            // 
            this.lbPercentage.AutoSize = true;
            this.lbPercentage.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbPercentage.ForeColor = System.Drawing.Color.White;
            this.lbPercentage.Location = new System.Drawing.Point(6, 82);
            this.lbPercentage.Name = "lbPercentage";
            this.lbPercentage.Size = new System.Drawing.Size(82, 13);
            this.lbPercentage.TabIndex = 158;
            this.lbPercentage.Text = "Percentage: 20";
            // 
            // tbPercentage
            // 
            this.tbPercentage.Location = new System.Drawing.Point(3, 98);
            this.tbPercentage.Maximum = 100;
            this.tbPercentage.Name = "tbPercentage";
            this.tbPercentage.Size = new System.Drawing.Size(179, 45);
            this.tbPercentage.TabIndex = 157;
            this.tbPercentage.TickFrequency = 10;
            this.tbPercentage.Value = 20;
            this.tbPercentage.Scroll += new System.EventHandler(this.tbPercentage_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 159;
            this.label1.Text = "Characters";
            // 
            // cbMode
            // 
            this.cbMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMode.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbMode.ForeColor = System.Drawing.Color.White;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.IntegralHeight = false;
            this.cbMode.Items.AddRange(new object[] {
            "Nightmare",
            "Swap"});
            this.cbMode.Location = new System.Drawing.Point(211, 98);
            this.cbMode.MaxDropDownItems = 15;
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(152, 21);
            this.cbMode.TabIndex = 160;
            this.cbMode.Tag = "color:normal";
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // tbCharacters
            // 
            this.tbCharacters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbCharacters.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbCharacters.ForeColor = System.Drawing.Color.White;
            this.tbCharacters.Location = new System.Drawing.Point(9, 47);
            this.tbCharacters.Multiline = true;
            this.tbCharacters.Name = "tbCharacters";
            this.tbCharacters.Size = new System.Drawing.Size(354, 32);
            this.tbCharacters.TabIndex = 161;
            this.tbCharacters.Tag = "color:normal";
            this.tbCharacters.Text = "abcdefghijklmnopqrstuvwxyz1234567890";
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMode.ForeColor = System.Drawing.Color.White;
            this.lbMode.Location = new System.Drawing.Point(207, 82);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(37, 13);
            this.lbMode.TabIndex = 162;
            this.lbMode.Text = "Mode";
            // 
            // StringEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "StringEngineControl";
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbPercentage;
        public System.Windows.Forms.TrackBar tbPercentage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbMode;
        public System.Windows.Forms.ComboBox cbMode;
        public System.Windows.Forms.TextBox tbCharacters;
    }
}
