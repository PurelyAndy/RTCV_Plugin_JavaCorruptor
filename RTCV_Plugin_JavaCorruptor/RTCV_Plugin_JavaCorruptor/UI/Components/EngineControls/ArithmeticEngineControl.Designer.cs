using System.ComponentModel;
using System.Windows.Forms;
using RTCV.UI.Components.Controls;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class ArithmeticEngineControl : JavaEngineControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label13 = new System.Windows.Forms.Label();
            this.cbDouble = new System.Windows.Forms.CheckBox();
            this.cbFloat = new System.Windows.Forms.CheckBox();
            this.cbInt = new System.Windows.Forms.CheckBox();
            this.cbLong = new System.Windows.Forms.CheckBox();
            this.tbMaximum = new System.Windows.Forms.TrackBar();
            this.lbMaximum = new System.Windows.Forms.Label();
            this.lbMinimum = new System.Windows.Forms.Label();
            this.tbMinimum = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lbeLimiters = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.lbeOperations = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.cbRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbeInstructions = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.cbSkipArrayAccess = new System.Windows.Forms.CheckBox();
            this.btnPresets = new System.Windows.Forms.Button();
            this.engineGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinimum)).BeginInit();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.btnPresets);
            this.engineGroupBox.Controls.Add(this.cbSkipArrayAccess);
            this.engineGroupBox.Controls.Add(this.lbeInstructions);
            this.engineGroupBox.Controls.Add(this.label2);
            this.engineGroupBox.Controls.Add(this.cbRuntimeRandom);
            this.engineGroupBox.Controls.Add(this.lbeOperations);
            this.engineGroupBox.Controls.Add(this.lbeLimiters);
            this.engineGroupBox.Controls.Add(this.label1);
            this.engineGroupBox.Controls.Add(this.lbMinimum);
            this.engineGroupBox.Controls.Add(this.tbMinimum);
            this.engineGroupBox.Controls.Add(this.lbMaximum);
            this.engineGroupBox.Controls.Add(this.tbMaximum);
            this.engineGroupBox.Controls.Add(this.cbLong);
            this.engineGroupBox.Controls.Add(this.cbInt);
            this.engineGroupBox.Controls.Add(this.cbFloat);
            this.engineGroupBox.Controls.Add(this.label19);
            this.engineGroupBox.Controls.Add(this.cbDouble);
            this.engineGroupBox.Controls.Add(this.label13);
            this.engineGroupBox.Controls.SetChildIndex(this.label13, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbDouble, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbFloat, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbInt, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbLong, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tbMaximum, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbMaximum, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tbMinimum, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbMinimum, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeLimiters, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeOperations, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbRuntimeRandom, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label2, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeInstructions, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbSkipArrayAccess, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnPresets, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(245, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Adds extra math operations to various bits of code";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(341, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 150;
            this.label13.Text = "Limiter operations";
            // 
            // cbDouble
            // 
            this.cbDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDouble.AutoSize = true;
            this.cbDouble.Checked = true;
            this.cbDouble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDouble.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDouble.ForeColor = System.Drawing.Color.White;
            this.cbDouble.Location = new System.Drawing.Point(64, 138);
            this.cbDouble.Name = "cbDouble";
            this.cbDouble.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDouble.Size = new System.Drawing.Size(64, 17);
            this.cbDouble.TabIndex = 151;
            this.cbDouble.Text = "Double";
            this.cbDouble.UseVisualStyleBackColor = true;
            // 
            // cbFloat
            // 
            this.cbFloat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFloat.AutoSize = true;
            this.cbFloat.Checked = true;
            this.cbFloat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFloat.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbFloat.ForeColor = System.Drawing.Color.White;
            this.cbFloat.Location = new System.Drawing.Point(6, 138);
            this.cbFloat.Name = "cbFloat";
            this.cbFloat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFloat.Size = new System.Drawing.Size(52, 17);
            this.cbFloat.TabIndex = 152;
            this.cbFloat.Text = "Float";
            this.cbFloat.UseVisualStyleBackColor = true;
            // 
            // cbInt
            // 
            this.cbInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInt.AutoSize = true;
            this.cbInt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbInt.ForeColor = System.Drawing.Color.White;
            this.cbInt.Location = new System.Drawing.Point(134, 138);
            this.cbInt.Name = "cbInt";
            this.cbInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbInt.Size = new System.Drawing.Size(40, 17);
            this.cbInt.TabIndex = 153;
            this.cbInt.Text = "Int";
            this.cbInt.UseVisualStyleBackColor = true;
            // 
            // cbLong
            // 
            this.cbLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLong.AutoSize = true;
            this.cbLong.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbLong.ForeColor = System.Drawing.Color.White;
            this.cbLong.Location = new System.Drawing.Point(180, 138);
            this.cbLong.Name = "cbLong";
            this.cbLong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbLong.Size = new System.Drawing.Size(52, 17);
            this.cbLong.TabIndex = 154;
            this.cbLong.Text = "Long";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // tbMaximum
            // 
            this.tbMaximum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaximum.AutoSize = false;
            this.tbMaximum.Location = new System.Drawing.Point(1, 46);
            this.tbMaximum.Maximum = 10000;
            this.tbMaximum.Minimum = -10000;
            this.tbMaximum.Name = "tbMaximum";
            this.tbMaximum.Size = new System.Drawing.Size(251, 30);
            this.tbMaximum.TabIndex = 155;
            this.tbMaximum.TickFrequency = 1000;
            this.tbMaximum.Value = 2000;
            this.tbMaximum.Scroll += new System.EventHandler(this.tbMaximum_Scroll);
            // 
            // lbMaximum
            // 
            this.lbMaximum.AutoSize = true;
            this.lbMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMaximum.ForeColor = System.Drawing.Color.White;
            this.lbMaximum.Location = new System.Drawing.Point(6, 30);
            this.lbMaximum.Name = "lbMaximum";
            this.lbMaximum.Size = new System.Drawing.Size(68, 13);
            this.lbMaximum.TabIndex = 156;
            this.lbMaximum.Text = "Maximum: 2";
            // 
            // lbMinimum
            // 
            this.lbMinimum.AutoSize = true;
            this.lbMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMinimum.ForeColor = System.Drawing.Color.White;
            this.lbMinimum.Location = new System.Drawing.Point(6, 82);
            this.lbMinimum.Name = "lbMinimum";
            this.lbMinimum.Size = new System.Drawing.Size(71, 13);
            this.lbMinimum.TabIndex = 158;
            this.lbMinimum.Text = "Minimum: -2";
            // 
            // tbMinimum
            // 
            this.tbMinimum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMinimum.AutoSize = false;
            this.tbMinimum.Location = new System.Drawing.Point(1, 98);
            this.tbMinimum.Maximum = 10000;
            this.tbMinimum.Minimum = -10000;
            this.tbMinimum.Name = "tbMinimum";
            this.tbMinimum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbMinimum.Size = new System.Drawing.Size(251, 30);
            this.tbMinimum.TabIndex = 157;
            this.tbMinimum.TickFrequency = 1000;
            this.tbMinimum.Value = -2000;
            this.tbMinimum.Scroll += new System.EventHandler(this.tbMinimum_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(341, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 160;
            this.label1.Text = "Value operations";
            // 
            // lbeLimiters
            // 
            this.lbeLimiters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbeLimiters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeLimiters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeLimiters.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeLimiters.ForeColor = System.Drawing.Color.White;
            this.lbeLimiters.FormattingEnabled = true;
            this.lbeLimiters.Items.AddRange(new object[] {
            "Addition",
            "Subtraction",
            "Multiplication",
            "Division"});
            this.lbeLimiters.Location = new System.Drawing.Point(344, 46);
            this.lbeLimiters.Name = "lbeLimiters";
            this.lbeLimiters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeLimiters.Size = new System.Drawing.Size(84, 52);
            this.lbeLimiters.TabIndex = 161;
            this.lbeLimiters.Tag = "color:dark2";
            // 
            // lbeOperations
            // 
            this.lbeOperations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbeOperations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeOperations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeOperations.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeOperations.ForeColor = System.Drawing.Color.White;
            this.lbeOperations.FormattingEnabled = true;
            this.lbeOperations.Items.AddRange(new object[] {
            "Addition",
            "Subtraction",
            "Multiplication",
            "Division"});
            this.lbeOperations.Location = new System.Drawing.Point(344, 117);
            this.lbeOperations.Name = "lbeOperations";
            this.lbeOperations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeOperations.Size = new System.Drawing.Size(84, 52);
            this.lbeOperations.TabIndex = 163;
            this.lbeOperations.Tag = "color:dark2";
            // 
            // cbRuntimeRandom
            // 
            this.cbRuntimeRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRuntimeRandom.AutoSize = true;
            this.cbRuntimeRandom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbRuntimeRandom.Location = new System.Drawing.Point(6, 161);
            this.cbRuntimeRandom.Name = "cbRuntimeRandom";
            this.cbRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRuntimeRandom.Size = new System.Drawing.Size(169, 17);
            this.cbRuntimeRandom.TabIndex = 164;
            this.cbRuntimeRandom.Text = "Randomize value at runtime";
            this.cbRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(244, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 174;
            this.label2.Text = "Instructions";
            // 
            // lbeInstructions
            // 
            this.lbeInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbeInstructions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeInstructions.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeInstructions.ForeColor = System.Drawing.Color.White;
            this.lbeInstructions.FormattingEnabled = true;
            this.lbeInstructions.Items.AddRange(new object[] {
            "Constants",
            "Math operations",
            "Variable loads",
            "Field loads",
            "Method calls",
            "Variable stores",
            "Field stores",
            "Before return"});
            this.lbeInstructions.Location = new System.Drawing.Point(247, 46);
            this.lbeInstructions.Name = "lbeInstructions";
            this.lbeInstructions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeInstructions.Size = new System.Drawing.Size(91, 104);
            this.lbeInstructions.TabIndex = 176;
            this.lbeInstructions.Tag = "color:dark2";
            this.lbeInstructions.SelectedIndexChanged += new System.EventHandler(this.lbeInstructions_SelectedIndexChanged);
            // 
            // cbSkipArrayAccess
            // 
            this.cbSkipArrayAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSkipArrayAccess.AutoSize = true;
            this.cbSkipArrayAccess.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSkipArrayAccess.ForeColor = System.Drawing.Color.White;
            this.cbSkipArrayAccess.Location = new System.Drawing.Point(180, 161);
            this.cbSkipArrayAccess.Name = "cbSkipArrayAccess";
            this.cbSkipArrayAccess.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSkipArrayAccess.Size = new System.Drawing.Size(122, 17);
            this.cbSkipArrayAccess.TabIndex = 177;
            this.cbSkipArrayAccess.Text = "Skip array accesses";
            this.cbSkipArrayAccess.UseVisualStyleBackColor = true;
            // 
            // btnPresets
            // 
            this.btnPresets.BackColor = System.Drawing.Color.Transparent;
            this.btnPresets.Location = new System.Drawing.Point(417, 12);
            this.btnPresets.Name = "btnPresets";
            this.btnPresets.Size = new System.Drawing.Size(20, 20);
            this.btnPresets.TabIndex = 178;
            this.btnPresets.UseVisualStyleBackColor = false;
            // 
            // ArithmeticEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ArithmeticEngineControl";
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinimum)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private Label label19;
        private Label label13;
        private Label lbMinimum;
        private Label lbMaximum;
        private Label label1;
        public ListBoxExtended lbeOperations;
        public ListBoxExtended lbeLimiters;
        public CheckBox cbFloat;
        public CheckBox cbDouble;
        public CheckBox cbLong;
        public CheckBox cbInt;
        public TrackBar tbMinimum;
        public TrackBar tbMaximum;
        public CheckBox cbRuntimeRandom;
        private Label label2;
        public ListBoxExtended lbeInstructions;
        public CheckBox cbSkipArrayAccess;
        private Button btnPresets;
    }
}
