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
            this.btnOpenDisassembler = new System.Windows.Forms.Button();
            this.btnMoreSettings = new System.Windows.Forms.Button();
            this.ofdProgram = new System.Windows.Forms.OpenFileDialog();
            this.btnCreateScript = new System.Windows.Forms.Button();
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
            this.tbIntensity.Name = "tbIntensity";
            this.tbIntensity.Size = new System.Drawing.Size(268, 30);
            this.tbIntensity.TabIndex = 22;
            this.tbIntensity.Tag = "color:dark1";
            this.tbIntensity.TickFrequency = 10000;
            this.tbIntensity.Value = 7071;
            this.tbIntensity.Scroll += new System.EventHandler(this.tbIntensity_Scroll);
            // 
            // lbIntensity
            // 
            this.lbIntensity.AutoSize = true;
            this.lbIntensity.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.lbIntensity.ForeColor = System.Drawing.Color.White;
            this.lbIntensity.Location = new System.Drawing.Point(5, 1);
            this.lbIntensity.Name = "lbIntensity";
            this.lbIntensity.Size = new System.Drawing.Size(104, 17);
            this.lbIntensity.TabIndex = 23;
            this.lbIntensity.Text = "Intensity: 0.500%";
            // 
            // cbUseLastSeed
            // 
            this.cbUseLastSeed.AutoSize = true;
            this.cbUseLastSeed.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbUseLastSeed.ForeColor = System.Drawing.Color.White;
            this.cbUseLastSeed.Location = new System.Drawing.Point(8, 74);
            this.cbUseLastSeed.Name = "cbUseLastSeed";
            this.cbUseLastSeed.Size = new System.Drawing.Size(108, 17);
            this.cbUseLastSeed.TabIndex = 212;
            this.cbUseLastSeed.Text = "Use last seed (0)";
            this.cbUseLastSeed.UseVisualStyleBackColor = true;
            // 
            // btnOptionsInfo
            // 
            this.btnOptionsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptionsInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnOptionsInfo.FlatAppearance.BorderSize = 0;
            this.btnOptionsInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptionsInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOptionsInfo.ForeColor = System.Drawing.Color.White;
            this.btnOptionsInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnOptionsInfo.Image")));
            this.btnOptionsInfo.Location = new System.Drawing.Point(253, 1);
            this.btnOptionsInfo.Name = "btnOptionsInfo";
            this.btnOptionsInfo.Size = new System.Drawing.Size(19, 19);
            this.btnOptionsInfo.TabIndex = 214;
            this.btnOptionsInfo.Tag = "color:dark1";
            this.btnOptionsInfo.UseVisualStyleBackColor = false;
            this.btnOptionsInfo.Click += new System.EventHandler(this.btnGeneralParamsInfo_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOutput.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbOutput.ForeColor = System.Drawing.Color.White;
            this.tbOutput.Location = new System.Drawing.Point(2, 130);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(268, 68);
            this.tbOutput.TabIndex = 220;
            this.tbOutput.Tag = "color:dark2";
            this.tbOutput.Text = "Program output will show up here";
            // 
            // cbPostCorruptAction
            // 
            this.cbPostCorruptAction.AutoSize = true;
            this.cbPostCorruptAction.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPostCorruptAction.ForeColor = System.Drawing.Color.White;
            this.cbPostCorruptAction.Location = new System.Drawing.Point(8, 90);
            this.cbPostCorruptAction.Margin = new System.Windows.Forms.Padding(0);
            this.cbPostCorruptAction.Name = "cbPostCorruptAction";
            this.cbPostCorruptAction.Size = new System.Drawing.Size(196, 17);
            this.cbPostCorruptAction.TabIndex = 219;
            this.cbPostCorruptAction.Text = "Launch software after corrupting";
            this.cbPostCorruptAction.UseVisualStyleBackColor = true;
            this.cbPostCorruptAction.CheckedChanged += new System.EventHandler(this.cbPostCorruptAction_CheckedChanged);
            // 
            // btnSelectProgram
            // 
            this.btnSelectProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSelectProgram.FlatAppearance.BorderSize = 0;
            this.btnSelectProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectProgram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectProgram.ForeColor = System.Drawing.Color.White;
            this.btnSelectProgram.Location = new System.Drawing.Point(208, 89);
            this.btnSelectProgram.Name = "btnSelectProgram";
            this.btnSelectProgram.Size = new System.Drawing.Size(40, 17);
            this.btnSelectProgram.TabIndex = 218;
            this.btnSelectProgram.Tag = "color:dark2";
            this.btnSelectProgram.Text = "Select";
            this.btnSelectProgram.UseVisualStyleBackColor = false;
            this.btnSelectProgram.Click += new System.EventHandler(this.btnSelectProgram_Click);
            // 
            // tbProgram
            // 
            this.tbProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbProgram.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbProgram.ForeColor = System.Drawing.Color.White;
            this.tbProgram.Location = new System.Drawing.Point(2, 107);
            this.tbProgram.Name = "tbProgram";
            this.tbProgram.Size = new System.Drawing.Size(210, 22);
            this.tbProgram.TabIndex = 217;
            this.tbProgram.Tag = "color:normal";
            this.tbProgram.TextChanged += new System.EventHandler(this.tbProgram_TextChanged);
            this.tbProgram.LostFocus += new System.EventHandler(this.tbProgram_LostFocus);
            // 
            // btnOpenDisassembler
            // 
            this.btnOpenDisassembler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenDisassembler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnOpenDisassembler.FlatAppearance.BorderSize = 0;
            this.btnOpenDisassembler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDisassembler.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpenDisassembler.ForeColor = System.Drawing.Color.White;
            this.btnOpenDisassembler.Location = new System.Drawing.Point(8, 50);
            this.btnOpenDisassembler.Name = "btnOpenDisassembler";
            this.btnOpenDisassembler.Size = new System.Drawing.Size(116, 23);
            this.btnOpenDisassembler.TabIndex = 216;
            this.btnOpenDisassembler.Tag = "color:dark2";
            this.btnOpenDisassembler.Text = "Open Disassembler";
            this.btnOpenDisassembler.UseVisualStyleBackColor = false;
            this.btnOpenDisassembler.Click += new System.EventHandler(this.btnOpenDisassembler_Click);
            // 
            // btnMoreSettings
            // 
            this.btnMoreSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoreSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnMoreSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMoreSettings.FlatAppearance.BorderSize = 0;
            this.btnMoreSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoreSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMoreSettings.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnMoreSettings.Location = new System.Drawing.Point(128, 50);
            this.btnMoreSettings.Name = "btnMoreSettings";
            this.btnMoreSettings.Size = new System.Drawing.Size(132, 23);
            this.btnMoreSettings.TabIndex = 216;
            this.btnMoreSettings.Tag = "color:dark2";
            this.btnMoreSettings.Text = "Settings && Domains";
            this.btnMoreSettings.UseVisualStyleBackColor = false;
            this.btnMoreSettings.Click += new System.EventHandler(this.btnMoreSettings_Click);
            // 
            // ofdProgram
            // 
            this.ofdProgram.FileName = "openFileDialog1";
            // 
            // btnCreateScript
            // 
            this.btnCreateScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCreateScript.FlatAppearance.BorderSize = 0;
            this.btnCreateScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateScript.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCreateScript.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.btnCreateScript.Location = new System.Drawing.Point(213, 107);
            this.btnCreateScript.Name = "btnCreateScript";
            this.btnCreateScript.Size = new System.Drawing.Size(57, 22);
            this.btnCreateScript.TabIndex = 221;
            this.btnCreateScript.Tag = "color:dark2";
            this.btnCreateScript.Text = "Create";
            this.btnCreateScript.UseVisualStyleBackColor = false;
            this.btnCreateScript.Click += new System.EventHandler(this.btnCreateScript_Click);
            // 
            // JavaGeneralParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(272, 200);
            this.Controls.Add(this.btnCreateScript);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.cbPostCorruptAction);
            this.Controls.Add(this.btnSelectProgram);
            this.Controls.Add(this.tbProgram);
            this.Controls.Add(this.btnOpenDisassembler);
            this.Controls.Add(this.btnMoreSettings);
            this.Controls.Add(this.btnOptionsInfo);
            this.Controls.Add(this.cbUseLastSeed);
            this.Controls.Add(this.lbIntensity);
            this.Controls.Add(this.tbIntensity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(272, 200);
            this.Name = "JavaGeneralParametersForm";
            this.Tag = "color:dark1";
            this.Text = "General Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public System.Windows.Forms.RichTextBox tbOutput;
		private System.Windows.Forms.CheckBox cbPostCorruptAction;
		private System.Windows.Forms.Button btnSelectProgram;
		private System.Windows.Forms.Button btnOpenDisassembler;
		private System.Windows.Forms.Button btnMoreSettings;
		private System.Windows.Forms.Button btnOptionsInfo;
		public System.Windows.Forms.TrackBar tbIntensity;
		private System.Windows.Forms.Label lbIntensity;
		public System.Windows.Forms.CheckBox cbUseLastSeed;
		private OpenFileDialog ofdProgram;

        #endregion

        private Button btnCreateScript;
        public TextBox tbProgram;
    }
}
