using System.Windows.Forms;

namespace Java_Corruptor.UI.Components
{
	partial class JavaGeneralParametersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaGeneralParametersForm));
            this.tbIntensity = new System.Windows.Forms.TrackBar();
            this.lbIntensity = new System.Windows.Forms.Label();
            this.cbUseLastSeed = new System.Windows.Forms.CheckBox();
            this.btnOptionsInfo = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.cbPostCorruptAction = new System.Windows.Forms.CheckBox();
            this.btnSelectProgram = new System.Windows.Forms.Button();
            this.tbProgram = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ofdProgram = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // tbIntensity
            // 
            this.tbIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIntensity.AutoSize = false;
            this.tbIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbIntensity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tbIntensity.Location = new System.Drawing.Point(2, 21);
            this.tbIntensity.Maximum = 100000;
            this.tbIntensity.Minimum = 1;
            this.tbIntensity.Name = "tbIntensity";
            this.tbIntensity.Size = new System.Drawing.Size(220, 30);
            this.tbIntensity.TabIndex = 22;
            this.tbIntensity.Tag = "color:dark1";
            this.tbIntensity.TickFrequency = 10000;
            this.tbIntensity.Value = 2000;
            this.tbIntensity.Scroll += new System.EventHandler(this.tbIntensity_Scroll);
            // 
            // lbIntensity
            // 
            this.lbIntensity.AutoSize = true;
            this.lbIntensity.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.lbIntensity.ForeColor = System.Drawing.Color.White;
            this.lbIntensity.Location = new System.Drawing.Point(5, 1);
            this.lbIntensity.Name = "lbIntensity";
            this.lbIntensity.Size = new System.Drawing.Size(80, 17);
            this.lbIntensity.TabIndex = 23;
            this.lbIntensity.Text = "Intensity: 2%";
            // 
            // cbUseLastSeed
            // 
            this.cbUseLastSeed.AutoSize = true;
            this.cbUseLastSeed.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbUseLastSeed.ForeColor = System.Drawing.Color.White;
            this.cbUseLastSeed.Location = new System.Drawing.Point(8, 90);
            this.cbUseLastSeed.Name = "cbUseLastSeed";
            this.cbUseLastSeed.Size = new System.Drawing.Size(108, 17);
            this.cbUseLastSeed.TabIndex = 212;
            this.cbUseLastSeed.Text = "Use last seed (0)";
            this.cbUseLastSeed.UseVisualStyleBackColor = true;
            // 
            // btnOptionsInfo
            // 
            this.btnSelectProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptionsInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnOptionsInfo.FlatAppearance.BorderSize = 0;
            this.btnOptionsInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptionsInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOptionsInfo.ForeColor = System.Drawing.Color.White;
            this.btnOptionsInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnOptionsInfo.Image")));
            this.btnOptionsInfo.Location = new System.Drawing.Point(205, 1);
            this.btnOptionsInfo.Name = "btnOptionsInfo";
            this.btnOptionsInfo.Size = new System.Drawing.Size(19, 19);
            this.btnOptionsInfo.TabIndex = 214;
            this.btnOptionsInfo.Tag = "color:dark1";
            this.btnOptionsInfo.UseVisualStyleBackColor = false;
            this.btnOptionsInfo.Click += new System.EventHandler(this.btnGeneralParamsInfo_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOutput.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbOutput.ForeColor = System.Drawing.Color.White;
            this.tbOutput.Location = new System.Drawing.Point(2, 130);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(220, 68);
            this.tbOutput.TabIndex = 220;
            this.tbOutput.Tag = "color:dark2";
            this.tbOutput.Text = "Program output will show up here";
            // 
            // cbPostCorruptAction
            // 
            this.cbPostCorruptAction.AutoSize = true;
            this.cbPostCorruptAction.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPostCorruptAction.ForeColor = System.Drawing.Color.White;
            this.cbPostCorruptAction.Location = new System.Drawing.Point(8, 74);
            this.cbPostCorruptAction.Margin = new System.Windows.Forms.Padding(0);
            this.cbPostCorruptAction.Name = "cbPostCorruptAction";
            this.cbPostCorruptAction.Size = new System.Drawing.Size(204, 17);
            this.cbPostCorruptAction.TabIndex = 219;
            this.cbPostCorruptAction.Text = "Launch a program after corrupting";
            this.cbPostCorruptAction.UseVisualStyleBackColor = true;
            this.cbPostCorruptAction.CheckedChanged += new System.EventHandler(this.cbPostCorruptAction_CheckedChanged);
            // 
            // btnSelectProgram
            // 
            this.btnSelectProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSelectProgram.FlatAppearance.BorderSize = 0;
            this.btnSelectProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectProgram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectProgram.ForeColor = System.Drawing.Color.White;
            this.btnSelectProgram.Location = new System.Drawing.Point(145, 50);
            this.btnSelectProgram.Name = "btnSelectProgram";
            this.btnSelectProgram.Size = new System.Drawing.Size(67, 23);
            this.btnSelectProgram.TabIndex = 218;
            this.btnSelectProgram.Tag = "color:dark2";
            this.btnSelectProgram.Text = "Select file";
            this.btnSelectProgram.UseVisualStyleBackColor = false;
            this.btnSelectProgram.Click += new System.EventHandler(this.btnSelectProgram_Click);
            // 
            // tbProgram
            // 
            this.tbProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbProgram.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbProgram.ForeColor = System.Drawing.Color.White;
            this.tbProgram.Location = new System.Drawing.Point(2, 107);
            this.tbProgram.Name = "tbProgram";
            this.tbProgram.Size = new System.Drawing.Size(220, 22);
            this.tbProgram.TabIndex = 217;
            this.tbProgram.Tag = "color:normal";
            this.tbProgram.TextChanged += new System.EventHandler(this.tbProgram_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(8, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 216;
            this.button1.Tag = "color:dark2";
            this.button1.Text = "Open Disassembler";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // ofdProgram
            // 
            this.ofdProgram.FileName = "openFileDialog1";
            // 
            // JavaGeneralParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(224, 200);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.cbPostCorruptAction);
            this.Controls.Add(this.btnSelectProgram);
            this.Controls.Add(this.tbProgram);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOptionsInfo);
            this.Controls.Add(this.cbUseLastSeed);
            this.Controls.Add(this.lbIntensity);
            this.Controls.Add(this.tbIntensity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JavaGeneralParametersForm";
            this.Tag = "color:dark1";
            this.Text = "General Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private System.Windows.Forms.RichTextBox tbOutput;
		private System.Windows.Forms.CheckBox cbPostCorruptAction;
		private System.Windows.Forms.Button btnSelectProgram;
		private System.Windows.Forms.TextBox tbProgram;
		private System.Windows.Forms.Button button1;

		private System.Windows.Forms.Button btnOptionsInfo;

		#endregion
		public System.Windows.Forms.TrackBar tbIntensity;
        private System.Windows.Forms.Label lbIntensity;
        public System.Windows.Forms.CheckBox cbUseLastSeed;
        private OpenFileDialog ofdProgram;
	}
}
