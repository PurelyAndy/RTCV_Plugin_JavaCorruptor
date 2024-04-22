using System.ComponentModel;
using System.Windows.Forms;
using RTCV.UI.Components.Controls;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class RoundingEngineControl : JavaEngineControl
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
            this.lbeOperations = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLong = new System.Windows.Forms.CheckBox();
            this.cbInt = new System.Windows.Forms.CheckBox();
            this.cbFloat = new System.Windows.Forms.CheckBox();
            this.cbDouble = new System.Windows.Forms.CheckBox();
            this.lbDecimalPlaces = new System.Windows.Forms.Label();
            this.tbDecimalPlaces = new System.Windows.Forms.TrackBar();
            this.lbeKinds = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.engineGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDecimalPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.label3);
            this.engineGroupBox.Controls.Add(this.cbDouble);
            this.engineGroupBox.Controls.Add(this.lbeKinds);
            this.engineGroupBox.Controls.Add(this.label2);
            this.engineGroupBox.Controls.Add(this.lbDecimalPlaces);
            this.engineGroupBox.Controls.Add(this.tbDecimalPlaces);
            this.engineGroupBox.Controls.Add(this.cbLong);
            this.engineGroupBox.Controls.Add(this.cbInt);
            this.engineGroupBox.Controls.Add(this.cbFloat);
            this.engineGroupBox.Controls.Add(this.lbeOperations);
            this.engineGroupBox.Controls.Add(this.label1);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeOperations, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbFloat, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbInt, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbLong, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tbDecimalPlaces, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbDecimalPlaces, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label2, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeKinds, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbDouble, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label3, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Rounds various numbers";
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
            this.lbeOperations.Location = new System.Drawing.Point(352, 126);
            this.lbeOperations.Name = "lbeOperations";
            this.lbeOperations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeOperations.Size = new System.Drawing.Size(84, 52);
            this.lbeOperations.TabIndex = 165;
            this.lbeOperations.Tag = "color:dark2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(308, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 164;
            this.label1.Text = "Which math operations";
            // 
            // cbLong
            // 
            this.cbLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLong.AutoSize = true;
            this.cbLong.ForeColor = System.Drawing.Color.White;
            this.cbLong.Location = new System.Drawing.Point(63, 129);
            this.cbLong.Name = "cbLong";
            this.cbLong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbLong.Size = new System.Drawing.Size(50, 17);
            this.cbLong.TabIndex = 169;
            this.cbLong.Text = "Long";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // cbInt
            // 
            this.cbInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInt.AutoSize = true;
            this.cbInt.ForeColor = System.Drawing.Color.White;
            this.cbInt.Location = new System.Drawing.Point(10, 152);
            this.cbInt.Name = "cbInt";
            this.cbInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbInt.Size = new System.Drawing.Size(38, 17);
            this.cbInt.TabIndex = 168;
            this.cbInt.Text = "Int";
            this.cbInt.UseVisualStyleBackColor = true;
            // 
            // cbFloat
            // 
            this.cbFloat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFloat.AutoSize = true;
            this.cbFloat.Checked = true;
            this.cbFloat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFloat.ForeColor = System.Drawing.Color.White;
            this.cbFloat.Location = new System.Drawing.Point(10, 129);
            this.cbFloat.Name = "cbFloat";
            this.cbFloat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFloat.Size = new System.Drawing.Size(49, 17);
            this.cbFloat.TabIndex = 167;
            this.cbFloat.Text = "Float";
            this.cbFloat.UseVisualStyleBackColor = true;
            // 
            // cbDouble
            // 
            this.cbDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDouble.AutoSize = true;
            this.cbDouble.Checked = true;
            this.cbDouble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDouble.ForeColor = System.Drawing.Color.White;
            this.cbDouble.Location = new System.Drawing.Point(53, 152);
            this.cbDouble.Name = "cbDouble";
            this.cbDouble.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDouble.Size = new System.Drawing.Size(60, 17);
            this.cbDouble.TabIndex = 166;
            this.cbDouble.Text = "Double";
            this.cbDouble.UseVisualStyleBackColor = true;
            // 
            // lbDecimalPlaces
            // 
            this.lbDecimalPlaces.AutoSize = true;
            this.lbDecimalPlaces.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbDecimalPlaces.ForeColor = System.Drawing.Color.White;
            this.lbDecimalPlaces.Location = new System.Drawing.Point(6, 43);
            this.lbDecimalPlaces.Name = "lbDecimalPlaces";
            this.lbDecimalPlaces.Size = new System.Drawing.Size(163, 13);
            this.lbDecimalPlaces.TabIndex = 171;
            this.lbDecimalPlaces.Text = "Round to 3.000 decimal places";
            // 
            // tbDecimalPlaces
            // 
            this.tbDecimalPlaces.AutoSize = false;
            this.tbDecimalPlaces.Location = new System.Drawing.Point(3, 59);
            this.tbDecimalPlaces.Maximum = 10000;
            this.tbDecimalPlaces.Minimum = 10;
            this.tbDecimalPlaces.Name = "tbDecimalPlaces";
            this.tbDecimalPlaces.Size = new System.Drawing.Size(179, 32);
            this.tbDecimalPlaces.TabIndex = 170;
            this.tbDecimalPlaces.TickFrequency = 1000;
            this.tbDecimalPlaces.Value = 3000;
            this.tbDecimalPlaces.Scroll += new System.EventHandler(this.tbRounding_Scroll);
            // 
            // lbeKinds
            // 
            this.lbeKinds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbeKinds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeKinds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeKinds.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeKinds.ForeColor = System.Drawing.Color.White;
            this.lbeKinds.FormattingEnabled = true;
            this.lbeKinds.Items.AddRange(new object[] {
            "Constants",
            "Math operations",
            "Variable loads",
            "Field loads",
            "Return values"});
            this.lbeKinds.Location = new System.Drawing.Point(345, 31);
            this.lbeKinds.Name = "lbeKinds";
            this.lbeKinds.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeKinds.Size = new System.Drawing.Size(91, 65);
            this.lbeKinds.TabIndex = 173;
            this.lbeKinds.Tag = "color:dark2";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(401, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 172;
            this.label2.Text = "Kinds";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 174;
            this.label3.Text = "Types";
            // 
            // RoundingEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label19);
            this.Name = "RoundingEngineControl";
            this.Controls.SetChildIndex(this.engineGroupBox, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDecimalPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label19;
        public ListBoxExtended lbeOperations;
        private Label label1;
        public CheckBox cbLong;
        public CheckBox cbInt;
        public CheckBox cbFloat;
        public CheckBox cbDouble;
        public ListBoxExtended lbeKinds;
        private Label label2;
        private Label lbDecimalPlaces;
        public TrackBar tbDecimalPlaces;
        private Label label3;
    }
}
