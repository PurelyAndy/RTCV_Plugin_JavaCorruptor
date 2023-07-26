namespace Java_Corruptor.UI
{

    partial class PluginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginForm));
            this.btnCorrupt = new System.Windows.Forms.Button();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.btnSelectInputJar = new System.Windows.Forms.Button();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.tbInputJar = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ofdInputJar = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnCorruptionEngine = new System.Windows.Forms.Panel();
            this.multiTB_ValueSeed = new RTCV.UI.Components.Controls.MultiTrackBar();
            this.multiTB_InstructionSeed = new RTCV.UI.Components.Controls.MultiTrackBar();
            this.vectorEngine1 = new UI.Engines.VectorEngine();
            this.arithmeticEngine1 = new UI.Engines.ArithmeticEngine();
            this.functionEngine1 = new UI.Engines.FunctionEngine();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnCorruptionEngine.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCorrupt
            // 
            this.btnCorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnCorrupt.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCorrupt.FlatAppearance.BorderSize = 0;
            this.btnCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorrupt.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCorrupt.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnCorrupt.Image = ((System.Drawing.Image)(resources.GetObject("btnCorrupt.Image")));
            this.btnCorrupt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorrupt.Location = new System.Drawing.Point(12, 12);
            this.btnCorrupt.Name = "btnCorrupt";
            this.btnCorrupt.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnCorrupt.Size = new System.Drawing.Size(115, 51);
            this.btnCorrupt.TabIndex = 138;
            this.btnCorrupt.TabStop = false;
            this.btnCorrupt.Tag = "color:dark3";
            this.btnCorrupt.Text = "  Corrupt";
            this.btnCorrupt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorrupt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCorrupt.UseVisualStyleBackColor = false;
            this.btnCorrupt.Click += new System.EventHandler(this.btnCorrupt_Click);
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnSelectOutputFolder.FlatAppearance.BorderSize = 0;
            this.btnSelectOutputFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectOutputFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnSelectOutputFolder.ForeColor = System.Drawing.Color.White;
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(495, 41);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(143, 22);
            this.btnSelectOutputFolder.TabIndex = 200;
            this.btnSelectOutputFolder.TabStop = false;
            this.btnSelectOutputFolder.Tag = "color:dark2";
            this.btnSelectOutputFolder.Text = "Select Output Folder";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = false;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // btnSelectInputJar
            // 
            this.btnSelectInputJar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnSelectInputJar.FlatAppearance.BorderSize = 0;
            this.btnSelectInputJar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectInputJar.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnSelectInputJar.ForeColor = System.Drawing.Color.White;
            this.btnSelectInputJar.Location = new System.Drawing.Point(495, 13);
            this.btnSelectInputJar.Name = "btnSelectInputJar";
            this.btnSelectInputJar.Size = new System.Drawing.Size(143, 22);
            this.btnSelectInputJar.TabIndex = 201;
            this.btnSelectInputJar.TabStop = false;
            this.btnSelectInputJar.Tag = "color:dark2";
            this.btnSelectInputJar.Text = "Select Input Jar";
            this.btnSelectInputJar.UseVisualStyleBackColor = false;
            this.btnSelectInputJar.Click += new System.EventHandler(this.btnSelectInputJar_Click);
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbOutputFolder.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbOutputFolder.ForeColor = System.Drawing.Color.White;
            this.tbOutputFolder.Location = new System.Drawing.Point(133, 41);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(356, 22);
            this.tbOutputFolder.TabIndex = 202;
            this.tbOutputFolder.Tag = "color:normal";
            // 
            // tbInputJar
            // 
            this.tbInputJar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputJar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbInputJar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbInputJar.ForeColor = System.Drawing.Color.White;
            this.tbInputJar.Location = new System.Drawing.Point(133, 13);
            this.tbInputJar.Name = "tbInputJar";
            this.tbInputJar.Size = new System.Drawing.Size(356, 22);
            this.tbInputJar.TabIndex = 203;
            this.tbInputJar.Tag = "color:normal";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 175);
            this.panel1.TabIndex = 204;
            this.panel1.Tag = "color:dark3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 122;
            this.label1.Text = "Seeds";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.multiTB_ValueSeed);
            this.panel2.Controls.Add(this.multiTB_InstructionSeed);
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 151);
            this.panel2.TabIndex = 0;
            this.panel2.Tag = "color:dark1";
            // 
            // ofdInputJar
            // 
            this.ofdInputJar.FileName = "openFileDialog1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.pnCorruptionEngine);
            this.panel3.Location = new System.Drawing.Point(263, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(375, 175);
            this.panel3.TabIndex = 205;
            this.panel3.Tag = "color:dark3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(5, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 122;
            this.label2.Text = "Corruption Engine";
            // 
            // pnCorruptionEngine
            // 
            this.pnCorruptionEngine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnCorruptionEngine.Controls.Add(this.vectorEngine1);
            this.pnCorruptionEngine.Controls.Add(this.arithmeticEngine1);
            this.pnCorruptionEngine.Controls.Add(this.functionEngine1);
            this.pnCorruptionEngine.Location = new System.Drawing.Point(0, 24);
            this.pnCorruptionEngine.Name = "pnCorruptionEngine";
            this.pnCorruptionEngine.Size = new System.Drawing.Size(375, 151);
            this.pnCorruptionEngine.TabIndex = 0;
            this.pnCorruptionEngine.Tag = "color:dark1";
            // 
            // multiTB_ValueSeed
            // 
            this.multiTB_ValueSeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.multiTB_ValueSeed.Checked = false;
            this.multiTB_ValueSeed.DisplayCheckbox = false;
            this.multiTB_ValueSeed.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.multiTB_ValueSeed.Hexadecimal = false;
            this.multiTB_ValueSeed.Label = "Value Seed";
            this.multiTB_ValueSeed.Location = new System.Drawing.Point(3, 74);
            this.multiTB_ValueSeed.Maximum = ((long)(100000000));
            this.multiTB_ValueSeed.Minimum = ((long)(0));
            this.multiTB_ValueSeed.Name = "multiTB_ValueSeed";
            this.multiTB_ValueSeed.Size = new System.Drawing.Size(239, 60);
            this.multiTB_ValueSeed.TabIndex = 1;
            this.multiTB_ValueSeed.Tag = "color:dark1";
            this.multiTB_ValueSeed.UncapNumericBox = false;
            this.multiTB_ValueSeed.Value = ((long)(0));
            // 
            // multiTB_InstructionSeed
            // 
            this.multiTB_InstructionSeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.multiTB_InstructionSeed.Checked = false;
            this.multiTB_InstructionSeed.DisplayCheckbox = false;
            this.multiTB_InstructionSeed.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.multiTB_InstructionSeed.Hexadecimal = false;
            this.multiTB_InstructionSeed.Label = "Instruction Seed";
            this.multiTB_InstructionSeed.Location = new System.Drawing.Point(3, 8);
            this.multiTB_InstructionSeed.Maximum = ((long)(100000000));
            this.multiTB_InstructionSeed.Minimum = ((long)(0));
            this.multiTB_InstructionSeed.Name = "multiTB_InstructionSeed";
            this.multiTB_InstructionSeed.Size = new System.Drawing.Size(239, 60);
            this.multiTB_InstructionSeed.TabIndex = 0;
            this.multiTB_InstructionSeed.Tag = "color:dark1";
            this.multiTB_InstructionSeed.UncapNumericBox = false;
            this.multiTB_InstructionSeed.Value = ((long)(0));
            // 
            // vectorEngine1
            // 
            this.vectorEngine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vectorEngine1.Location = new System.Drawing.Point(3, -3);
            this.vectorEngine1.Name = "vectorEngine1";
            this.vectorEngine1.Size = new System.Drawing.Size(369, 151);
            this.vectorEngine1.TabIndex = 0;
            this.vectorEngine1.Tag = "color:dark1";
            this.vectorEngine1.Visible = false;
            // 
            // arithmeticEngine1
            // 
            this.arithmeticEngine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.arithmeticEngine1.Location = new System.Drawing.Point(3, -3);
            this.arithmeticEngine1.Name = "arithmeticEngine1";
            this.arithmeticEngine1.Size = new System.Drawing.Size(369, 151);
            this.arithmeticEngine1.TabIndex = 0;
            this.arithmeticEngine1.Tag = "color:dark1";
            this.arithmeticEngine1.Visible = false;
            // 
            // functionEngine1
            // 
            this.functionEngine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.functionEngine1.Location = new System.Drawing.Point(3, -3);
            this.functionEngine1.Name = "functionEngine1";
            this.functionEngine1.Size = new System.Drawing.Size(369, 151);
            this.functionEngine1.TabIndex = 0;
            this.functionEngine1.Tag = "color:dark1";
            this.functionEngine1.Visible = false;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(650, 256);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbInputJar);
            this.Controls.Add(this.tbOutputFolder);
            this.Controls.Add(this.btnSelectInputJar);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.btnCorrupt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "color:dark2";
            this.Text = "Plugin Form";
            this.Load += new System.EventHandler(this.PluginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnCorruptionEngine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public System.Windows.Forms.Button btnCorrupt;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Button btnSelectInputJar;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.TextBox tbInputJar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog ofdInputJar;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label1;
        private RTCV.UI.Components.Controls.MultiTrackBar multiTB_InstructionSeed;
        private RTCV.UI.Components.Controls.MultiTrackBar multiTB_ValueSeed;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnCorruptionEngine;
        private UI.Engines.VectorEngine vectorEngine1;
        private UI.Engines.ArithmeticEngine arithmeticEngine1;
        private UI.Engines.FunctionEngine functionEngine1;
    }
}
