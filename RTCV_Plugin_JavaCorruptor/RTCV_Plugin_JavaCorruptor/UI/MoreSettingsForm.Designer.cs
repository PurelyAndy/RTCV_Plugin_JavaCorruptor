namespace Java_Corruptor.UI
{
    partial class MoreSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoreSettingsForm));
            this.rbComputeMaxStack = new System.Windows.Forms.RadioButton();
            this.rbComputeStackFrames = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnComputeInfo = new System.Windows.Forms.Button();
            this.cbCompressJar = new System.Windows.Forms.CheckBox();
            this.tbThreads = new System.Windows.Forms.TrackBar();
            this.lbThreads = new System.Windows.Forms.Label();
            this.cbSelectDomains = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tlpDomains = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flpClasses = new System.Windows.Forms.FlowLayoutPanel();
            this.flpMethods = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.btnAddMethod = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreads)).BeginInit();
            this.tlpDomains.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbComputeMaxStack
            // 
            this.rbComputeMaxStack.AutoSize = true;
            this.rbComputeMaxStack.Location = new System.Drawing.Point(6, 41);
            this.rbComputeMaxStack.Name = "rbComputeMaxStack";
            this.rbComputeMaxStack.Size = new System.Drawing.Size(193, 17);
            this.rbComputeMaxStack.TabIndex = 0;
            this.rbComputeMaxStack.TabStop = true;
            this.rbComputeMaxStack.Text = "Max stack size (faster, may break)";
            this.rbComputeMaxStack.UseVisualStyleBackColor = true;
            this.rbComputeMaxStack.CheckedChanged += new System.EventHandler(this.rbComputeMaxStack_CheckedChanged);
            // 
            // rbComputeStackFrames
            // 
            this.rbComputeStackFrames.AutoSize = true;
            this.rbComputeStackFrames.Location = new System.Drawing.Point(6, 18);
            this.rbComputeStackFrames.Name = "rbComputeStackFrames";
            this.rbComputeStackFrames.Size = new System.Drawing.Size(147, 17);
            this.rbComputeStackFrames.TabIndex = 1;
            this.rbComputeStackFrames.TabStop = true;
            this.rbComputeStackFrames.Text = "All stack frames (slower)";
            this.rbComputeStackFrames.UseVisualStyleBackColor = true;
            this.rbComputeStackFrames.CheckedChanged += new System.EventHandler(this.rbComputeStackFrames_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnComputeInfo);
            this.groupBox1.Controls.Add(this.rbComputeMaxStack);
            this.groupBox1.Controls.Add(this.rbComputeStackFrames);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "When corrupting, compute";
            // 
            // btnComputeInfo
            // 
            this.btnComputeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComputeInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnComputeInfo.FlatAppearance.BorderSize = 0;
            this.btnComputeInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComputeInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnComputeInfo.ForeColor = System.Drawing.Color.White;
            this.btnComputeInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnComputeInfo.Image")));
            this.btnComputeInfo.Location = new System.Drawing.Point(178, 10);
            this.btnComputeInfo.Name = "btnComputeInfo";
            this.btnComputeInfo.Size = new System.Drawing.Size(19, 19);
            this.btnComputeInfo.TabIndex = 215;
            this.btnComputeInfo.Tag = "color:dark1";
            this.btnComputeInfo.UseVisualStyleBackColor = false;
            this.btnComputeInfo.Click += new System.EventHandler(this.btnComputeInfo_Click);
            // 
            // cbCompressJar
            // 
            this.cbCompressJar.AutoSize = true;
            this.cbCompressJar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbCompressJar.ForeColor = System.Drawing.Color.White;
            this.cbCompressJar.Location = new System.Drawing.Point(12, 83);
            this.cbCompressJar.Margin = new System.Windows.Forms.Padding(0);
            this.cbCompressJar.Name = "cbCompressJar";
            this.cbCompressJar.Size = new System.Drawing.Size(194, 17);
            this.cbCompressJar.TabIndex = 220;
            this.cbCompressJar.Text = "Compress corrupted JAR (slower)";
            this.cbCompressJar.UseVisualStyleBackColor = true;
            this.cbCompressJar.CheckedChanged += new System.EventHandler(this.cbCompressJar_CheckedChanged);
            // 
            // tbThreads
            // 
            this.tbThreads.Location = new System.Drawing.Point(4, 120);
            this.tbThreads.Maximum = 32;
            this.tbThreads.Minimum = 1;
            this.tbThreads.Name = "tbThreads";
            this.tbThreads.Size = new System.Drawing.Size(219, 45);
            this.tbThreads.TabIndex = 221;
            this.tbThreads.Value = 4;
            this.tbThreads.Scroll += new System.EventHandler(this.tbThreads_Scroll);
            // 
            // lbThreads
            // 
            this.lbThreads.AutoSize = true;
            this.lbThreads.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbThreads.ForeColor = System.Drawing.Color.White;
            this.lbThreads.Location = new System.Drawing.Point(9, 104);
            this.lbThreads.Name = "lbThreads";
            this.lbThreads.Size = new System.Drawing.Size(59, 13);
            this.lbThreads.TabIndex = 222;
            this.lbThreads.Text = "Threads: 4";
            // 
            // cbSelectDomains
            // 
            this.cbSelectDomains.AutoSize = true;
            this.cbSelectDomains.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectDomains.ForeColor = System.Drawing.Color.White;
            this.cbSelectDomains.Location = new System.Drawing.Point(12, 158);
            this.cbSelectDomains.Margin = new System.Windows.Forms.Padding(0);
            this.cbSelectDomains.Name = "cbSelectDomains";
            this.cbSelectDomains.Size = new System.Drawing.Size(193, 17);
            this.cbSelectDomains.TabIndex = 223;
            this.cbSelectDomains.Text = "Limit domain to classes/methods";
            this.cbSelectDomains.UseVisualStyleBackColor = true;
            this.cbSelectDomains.CheckedChanged += new System.EventHandler(this.cbSelectDomains_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 227;
            this.label2.Text = "Methods:";
            // 
            // tlpDomains
            // 
            this.tlpDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpDomains.ColumnCount = 1;
            this.tlpDomains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDomains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDomains.Controls.Add(this.panel1, 0, 0);
            this.tlpDomains.Controls.Add(this.panel2, 0, 1);
            this.tlpDomains.Enabled = false;
            this.tlpDomains.Location = new System.Drawing.Point(12, 178);
            this.tlpDomains.Name = "tlpDomains";
            this.tlpDomains.RowCount = 2;
            this.tlpDomains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDomains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDomains.Size = new System.Drawing.Size(200, 252);
            this.tlpDomains.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddClass);
            this.panel1.Controls.Add(this.flpClasses);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 120);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 229;
            this.label1.Text = "Classes:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAddMethod);
            this.panel2.Controls.Add(this.flpMethods);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 120);
            this.panel2.TabIndex = 1;
            // 
            // flpClasses
            // 
            this.flpClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpClasses.AutoScroll = true;
            this.flpClasses.Enabled = false;
            this.flpClasses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClasses.Location = new System.Drawing.Point(3, 21);
            this.flpClasses.Name = "flpClasses";
            this.flpClasses.Size = new System.Drawing.Size(187, 96);
            this.flpClasses.TabIndex = 230;
            this.flpClasses.WrapContents = false;
            // 
            // flpMethods
            // 
            this.flpMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMethods.AutoScroll = true;
            this.flpMethods.Enabled = false;
            this.flpMethods.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMethods.Location = new System.Drawing.Point(4, 21);
            this.flpMethods.Name = "flpMethods";
            this.flpMethods.Size = new System.Drawing.Size(187, 96);
            this.flpMethods.TabIndex = 231;
            this.flpMethods.WrapContents = false;
            // 
            // btnAddClass
            // 
            this.btnAddClass.BackColor = System.Drawing.Color.Gray;
            this.btnAddClass.FlatAppearance.BorderSize = 0;
            this.btnAddClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddClass.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnAddClass.ForeColor = System.Drawing.Color.White;
            this.btnAddClass.Location = new System.Drawing.Point(56, 0);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(38, 21);
            this.btnAddClass.TabIndex = 184;
            this.btnAddClass.TabStop = false;
            this.btnAddClass.Tag = "color:light1";
            this.btnAddClass.Text = "Add";
            this.btnAddClass.UseVisualStyleBackColor = false;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // btnAddMethod
            // 
            this.btnAddMethod.BackColor = System.Drawing.Color.Gray;
            this.btnAddMethod.FlatAppearance.BorderSize = 0;
            this.btnAddMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMethod.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnAddMethod.ForeColor = System.Drawing.Color.White;
            this.btnAddMethod.Location = new System.Drawing.Point(65, 0);
            this.btnAddMethod.Name = "btnAddMethod";
            this.btnAddMethod.Size = new System.Drawing.Size(42, 21);
            this.btnAddMethod.TabIndex = 231;
            this.btnAddMethod.TabStop = false;
            this.btnAddMethod.Tag = "color:light1";
            this.btnAddMethod.Text = "Add";
            this.btnAddMethod.UseVisualStyleBackColor = false;
            this.btnAddMethod.Click += new System.EventHandler(this.btnAddMethod_Click);
            // 
            // MoreSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(224, 442);
            this.Controls.Add(this.tlpDomains);
            this.Controls.Add(this.cbSelectDomains);
            this.Controls.Add(this.lbThreads);
            this.Controls.Add(this.tbThreads);
            this.Controls.Add(this.cbCompressJar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MoreSettingsForm";
            this.Tag = "color:dark1";
            this.Text = "MoreSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoreSettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreads)).EndInit();
            this.tlpDomains.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbComputeMaxStack;
        private System.Windows.Forms.RadioButton rbComputeStackFrames;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnComputeInfo;
        private System.Windows.Forms.CheckBox cbCompressJar;
        private System.Windows.Forms.TrackBar tbThreads;
        private System.Windows.Forms.Label lbThreads;
        private System.Windows.Forms.CheckBox cbSelectDomains;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tlpDomains;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flpMethods;
        public System.Windows.Forms.Button btnAddClass;
        public System.Windows.Forms.Button btnAddMethod;
    }
}