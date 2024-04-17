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
            this.engineGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinimum)).BeginInit();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
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
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
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
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(253, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Adds an extra operation after performing arithmetic";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(185, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 150;
            this.label13.Text = "Limiters";
            // 
            // cbDouble
            // 
            this.cbDouble.AutoSize = true;
            this.cbDouble.Checked = true;
            this.cbDouble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDouble.ForeColor = System.Drawing.Color.White;
            this.cbDouble.Location = new System.Drawing.Point(69, 134);
            this.cbDouble.Name = "cbDouble";
            this.cbDouble.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDouble.Size = new System.Drawing.Size(60, 17);
            this.cbDouble.TabIndex = 151;
            this.cbDouble.Text = "Double";
            this.cbDouble.UseVisualStyleBackColor = true;
            // 
            // cbFloat
            // 
            this.cbFloat.AutoSize = true;
            this.cbFloat.Checked = true;
            this.cbFloat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFloat.ForeColor = System.Drawing.Color.White;
            this.cbFloat.Location = new System.Drawing.Point(14, 134);
            this.cbFloat.Name = "cbFloat";
            this.cbFloat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFloat.Size = new System.Drawing.Size(49, 17);
            this.cbFloat.TabIndex = 152;
            this.cbFloat.Text = "Float";
            this.cbFloat.UseVisualStyleBackColor = true;
            // 
            // cbInt
            // 
            this.cbInt.AutoSize = true;
            this.cbInt.ForeColor = System.Drawing.Color.White;
            this.cbInt.Location = new System.Drawing.Point(14, 153);
            this.cbInt.Name = "cbInt";
            this.cbInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbInt.Size = new System.Drawing.Size(38, 17);
            this.cbInt.TabIndex = 153;
            this.cbInt.Text = "Int";
            this.cbInt.UseVisualStyleBackColor = true;
            // 
            // cbLong
            // 
            this.cbLong.AutoSize = true;
            this.cbLong.ForeColor = System.Drawing.Color.White;
            this.cbLong.Location = new System.Drawing.Point(69, 153);
            this.cbLong.Name = "cbLong";
            this.cbLong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbLong.Size = new System.Drawing.Size(50, 17);
            this.cbLong.TabIndex = 154;
            this.cbLong.Text = "Long";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // tbMaximum
            // 
            this.tbMaximum.AutoSize = false;
            this.tbMaximum.Location = new System.Drawing.Point(6, 46);
            this.tbMaximum.Maximum = 10000;
            this.tbMaximum.Name = "tbMaximum";
            this.tbMaximum.Size = new System.Drawing.Size(179, 30);
            this.tbMaximum.TabIndex = 155;
            this.tbMaximum.TickFrequency = 1000;
            this.tbMaximum.Value = 3000;
            this.tbMaximum.Scroll += new System.EventHandler(this.tbMaximum_Scroll);
            // 
            // lbMaximum
            // 
            this.lbMaximum.AutoSize = true;
            this.lbMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMaximum.ForeColor = System.Drawing.Color.White;
            this.lbMaximum.Location = new System.Drawing.Point(6, 30);
            this.lbMaximum.Name = "lbMaximum";
            this.lbMaximum.Size = new System.Drawing.Size(89, 13);
            this.lbMaximum.TabIndex = 156;
            this.lbMaximum.Text = "Maximum: 3.000";
            // 
            // lbMinimum
            // 
            this.lbMinimum.AutoSize = true;
            this.lbMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMinimum.ForeColor = System.Drawing.Color.White;
            this.lbMinimum.Location = new System.Drawing.Point(6, 82);
            this.lbMinimum.Name = "lbMinimum";
            this.lbMinimum.Size = new System.Drawing.Size(92, 13);
            this.lbMinimum.TabIndex = 158;
            this.lbMinimum.Text = "Minimum: -3.000";
            // 
            // tbMinimum
            // 
            this.tbMinimum.AutoSize = false;
            this.tbMinimum.Location = new System.Drawing.Point(6, 98);
            this.tbMinimum.Maximum = 0;
            this.tbMinimum.Minimum = -10000;
            this.tbMinimum.Name = "tbMinimum";
            this.tbMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMinimum.Size = new System.Drawing.Size(179, 30);
            this.tbMinimum.TabIndex = 157;
            this.tbMinimum.TickFrequency = 1000;
            this.tbMinimum.Value = -3000;
            this.tbMinimum.Scroll += new System.EventHandler(this.tbMinimum_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(276, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 160;
            this.label1.Text = "Operations";
            // 
            // lbeLimiters
            // 
            this.lbeLimiters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeLimiters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeLimiters.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeLimiters.ForeColor = System.Drawing.Color.White;
            this.lbeLimiters.FormattingEnabled = true;
            this.lbeLimiters.Items.AddRange(new object[] { "Addition", "Subtraction", "Multiplication", "Division" });
            this.lbeLimiters.Location = new System.Drawing.Point(188, 45);
            this.lbeLimiters.Name = "lbeLimiters";
            this.lbeLimiters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeLimiters.Size = new System.Drawing.Size(84, 52);
            this.lbeLimiters.TabIndex = 161;
            this.lbeLimiters.Tag = "color:dark2";
            // 
            // lbeOperations
            // 
            this.lbeOperations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeOperations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeOperations.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeOperations.ForeColor = System.Drawing.Color.White;
            this.lbeOperations.FormattingEnabled = true;
            this.lbeOperations.Items.AddRange(new object[] { "Addition", "Subtraction", "Multiplication", "Division" });
            this.lbeOperations.Location = new System.Drawing.Point(279, 45);
            this.lbeOperations.Name = "lbeOperations";
            this.lbeOperations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeOperations.Size = new System.Drawing.Size(84, 52);
            this.lbeOperations.TabIndex = 163;
            this.lbeOperations.Tag = "color:dark2";
            // 
            // cbRuntimeRandom
            // 
            this.cbRuntimeRandom.AutoSize = true;
            this.cbRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbRuntimeRandom.Location = new System.Drawing.Point(185, 111);
            this.cbRuntimeRandom.Name = "cbRuntimeRandom";
            this.cbRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRuntimeRandom.Size = new System.Drawing.Size(128, 17);
            this.cbRuntimeRandom.TabIndex = 164;
            this.cbRuntimeRandom.Text = "Randomize value at runtime";
            this.cbRuntimeRandom.UseVisualStyleBackColor = true;
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
    }
}
