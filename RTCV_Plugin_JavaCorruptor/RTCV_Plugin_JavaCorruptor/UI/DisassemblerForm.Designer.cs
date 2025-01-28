namespace Java_Corruptor.UI
{
    partial class DisassemblerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisassemblerForm));
            this.tbDisassembly = new System.Windows.Forms.RichTextBox();
            this.tbMethod = new System.Windows.Forms.TextBox();
            this.tbLineNumbers = new System.Windows.Forms.RichTextBox();
            this.btnDisassemble = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDisassembly
            // 
            this.tbDisassembly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDisassembly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbDisassembly.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDisassembly.Font = new System.Drawing.Font("Consolas", 10F);
            this.tbDisassembly.ForeColor = System.Drawing.Color.White;
            this.tbDisassembly.Location = new System.Drawing.Point(37, 40);
            this.tbDisassembly.Margin = new System.Windows.Forms.Padding(0);
            this.tbDisassembly.Name = "tbDisassembly";
            this.tbDisassembly.Size = new System.Drawing.Size(729, 353);
            this.tbDisassembly.TabIndex = 0;
            this.tbDisassembly.Tag = "color:dark2";
            this.tbDisassembly.Text = "";
            this.tbDisassembly.VScroll += new System.EventHandler(this.tbDisassembly_VScroll);
            this.tbDisassembly.TextChanged += new System.EventHandler(this.tbDisassembly_TextChanged);
            // 
            // tbMethod
            // 
            this.tbMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbMethod.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbMethod.ForeColor = System.Drawing.Color.White;
            this.tbMethod.Location = new System.Drawing.Point(12, 12);
            this.tbMethod.Name = "tbMethod";
            this.tbMethod.Size = new System.Drawing.Size(666, 22);
            this.tbMethod.TabIndex = 145;
            this.tbMethod.Tag = "color:normal";
            // 
            // tbLineNumbers
            // 
            this.tbLineNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLineNumbers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbLineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLineNumbers.Font = new System.Drawing.Font("Consolas", 10F);
            this.tbLineNumbers.ForeColor = System.Drawing.Color.White;
            this.tbLineNumbers.Location = new System.Drawing.Point(12, 40);
            this.tbLineNumbers.Margin = new System.Windows.Forms.Padding(0);
            this.tbLineNumbers.Name = "tbLineNumbers";
            this.tbLineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbLineNumbers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbLineNumbers.Size = new System.Drawing.Size(25, 353);
            this.tbLineNumbers.TabIndex = 146;
            this.tbLineNumbers.Tag = "color:dark2";
            this.tbLineNumbers.Text = "";
            // 
            // btnDisassemble
            // 
            this.btnDisassemble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisassemble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnDisassemble.FlatAppearance.BorderSize = 0;
            this.btnDisassemble.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisassemble.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDisassemble.ForeColor = System.Drawing.Color.White;
            this.btnDisassemble.Location = new System.Drawing.Point(684, 12);
            this.btnDisassemble.Name = "btnDisassemble";
            this.btnDisassemble.Size = new System.Drawing.Size(82, 22);
            this.btnDisassemble.TabIndex = 147;
            this.btnDisassemble.Tag = "color:dark2";
            this.btnDisassemble.Text = "Disassemble";
            this.btnDisassemble.UseVisualStyleBackColor = false;
            this.btnDisassemble.Click += new System.EventHandler(this.btnDisassemble_Click);
            // 
            // DisassemblerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(778, 405);
            this.Controls.Add(this.btnDisassemble);
            this.Controls.Add(this.tbLineNumbers);
            this.Controls.Add(this.tbMethod);
            this.Controls.Add(this.tbDisassembly);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisassemblerForm";
            this.Tag = "color:dark1";
            this.Text = "DisassemblerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisassemblerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnDisassemble;

        private System.Windows.Forms.RichTextBox tbLineNumbers;

        #endregion

        private System.Windows.Forms.RichTextBox tbDisassembly;
        private System.Windows.Forms.TextBox tbMethod;
    }
}