namespace JAVACORRUPTOR.UI
{
    partial class JRESelectorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb32Bit = new System.Windows.Forms.ListBox();
            this.lb64Bit = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select any one of these. Prefer versions containing \"1.8.0\", but it";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "probably doesn\'t matter too much.";
            // 
            // lb32Bit
            // 
            this.lb32Bit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lb32Bit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb32Bit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lb32Bit.ForeColor = System.Drawing.Color.White;
            this.lb32Bit.FormattingEnabled = true;
            this.lb32Bit.Location = new System.Drawing.Point(12, 154);
            this.lb32Bit.Name = "lb32Bit";
            this.lb32Bit.Size = new System.Drawing.Size(338, 52);
            this.lb32Bit.TabIndex = 2;
            this.lb32Bit.Tag = "color:dark2";
            this.lb32Bit.SelectedIndexChanged += new System.EventHandler(this.lb32Bit_SelectedIndexChanged);
            // 
            // lb64Bit
            // 
            this.lb64Bit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lb64Bit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb64Bit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lb64Bit.ForeColor = System.Drawing.Color.White;
            this.lb64Bit.FormattingEnabled = true;
            this.lb64Bit.Location = new System.Drawing.Point(12, 74);
            this.lb64Bit.Name = "lb64Bit";
            this.lb64Bit.Size = new System.Drawing.Size(338, 52);
            this.lb64Bit.TabIndex = 3;
            this.lb64Bit.Tag = "color:dark2";
            this.lb64Bit.SelectedIndexChanged += new System.EventHandler(this.lb64Bit_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "32-bit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "64-bit";
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.Gray;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(12, 216);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 6;
            this.btnContinue.Tag = "color:light1";
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // JRESelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(362, 246);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb64Bit);
            this.Controls.Add(this.lb32Bit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "JRESelectorForm";
            this.Tag = "color:dark1";
            this.Text = "JRESelectorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox lb32Bit;
        public System.Windows.Forms.ListBox lb64Bit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnContinue;
    }
}