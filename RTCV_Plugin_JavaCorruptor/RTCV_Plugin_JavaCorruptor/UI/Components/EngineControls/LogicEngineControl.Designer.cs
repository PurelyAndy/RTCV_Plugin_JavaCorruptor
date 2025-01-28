using System.ComponentModel;
using System.Windows.Forms;
using RTCV.UI.Components.Controls;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class LogicEngineControl : JavaEngineControl
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
            this.cbLong = new System.Windows.Forms.CheckBox();
            this.cbInt = new System.Windows.Forms.CheckBox();
            this.cbFloat = new System.Windows.Forms.CheckBox();
            this.cbDouble = new System.Windows.Forms.CheckBox();
            this.lbeFind = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBoolean = new System.Windows.Forms.CheckBox();
            this.lbeReplace = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.engineGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.label4);
            this.engineGroupBox.Controls.Add(this.cbMode);
            this.engineGroupBox.Controls.Add(this.lbeReplace);
            this.engineGroupBox.Controls.Add(this.label1);
            this.engineGroupBox.Controls.Add(this.cbBoolean);
            this.engineGroupBox.Controls.Add(this.label3);
            this.engineGroupBox.Controls.Add(this.cbDouble);
            this.engineGroupBox.Controls.Add(this.lbeFind);
            this.engineGroupBox.Controls.Add(this.label2);
            this.engineGroupBox.Controls.Add(this.cbLong);
            this.engineGroupBox.Controls.Add(this.cbInt);
            this.engineGroupBox.Controls.Add(this.cbFloat);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbFloat, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbInt, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbLong, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label2, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeFind, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbDouble, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label3, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbBoolean, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.lbeReplace, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.cbMode, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label4, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Corrupts comparisons";
            // 
            // cbLong
            // 
            this.cbLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLong.AutoSize = true;
            this.cbLong.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbLong.ForeColor = System.Drawing.Color.White;
            this.cbLong.Location = new System.Drawing.Point(55, 161);
            this.cbLong.Name = "cbLong";
            this.cbLong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbLong.Size = new System.Drawing.Size(52, 17);
            this.cbLong.TabIndex = 169;
            this.cbLong.Text = "Long";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // cbInt
            // 
            this.cbInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInt.AutoSize = true;
            this.cbInt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbInt.ForeColor = System.Drawing.Color.White;
            this.cbInt.Location = new System.Drawing.Point(9, 161);
            this.cbInt.Name = "cbInt";
            this.cbInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbInt.Size = new System.Drawing.Size(40, 17);
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
            this.cbFloat.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbFloat.ForeColor = System.Drawing.Color.White;
            this.cbFloat.Location = new System.Drawing.Point(55, 140);
            this.cbFloat.Name = "cbFloat";
            this.cbFloat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFloat.Size = new System.Drawing.Size(52, 17);
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
            this.cbDouble.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDouble.ForeColor = System.Drawing.Color.White;
            this.cbDouble.Location = new System.Drawing.Point(113, 140);
            this.cbDouble.Name = "cbDouble";
            this.cbDouble.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDouble.Size = new System.Drawing.Size(64, 17);
            this.cbDouble.TabIndex = 166;
            this.cbDouble.Text = "Double";
            this.cbDouble.UseVisualStyleBackColor = true;
            // 
            // lbeFind
            // 
            this.lbeFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeFind.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeFind.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeFind.ForeColor = System.Drawing.Color.White;
            this.lbeFind.FormattingEnabled = true;
            this.lbeFind.Items.AddRange(new object[] {
            "==",
            "!=",
            "<",
            ">=",
            ">",
            "<="});
            this.lbeFind.Location = new System.Drawing.Point(6, 46);
            this.lbeFind.Name = "lbeFind";
            this.lbeFind.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeFind.Size = new System.Drawing.Size(23, 78);
            this.lbeFind.TabIndex = 173;
            this.lbeFind.Tag = "color:dark2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 172;
            this.label2.Text = "Find";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 174;
            this.label3.Text = "Types";
            // 
            // cbBoolean
            // 
            this.cbBoolean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBoolean.AutoSize = true;
            this.cbBoolean.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.cbBoolean.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbBoolean.ForeColor = System.Drawing.Color.White;
            this.cbBoolean.Location = new System.Drawing.Point(113, 161);
            this.cbBoolean.Name = "cbBoolean";
            this.cbBoolean.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbBoolean.Size = new System.Drawing.Size(68, 17);
            this.cbBoolean.TabIndex = 175;
            this.cbBoolean.Text = "Boolean";
            this.cbBoolean.UseVisualStyleBackColor = true;
            // 
            // lbeReplace
            // 
            this.lbeReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbeReplace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbeReplace.Enabled = false;
            this.lbeReplace.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbeReplace.ForeColor = System.Drawing.Color.White;
            this.lbeReplace.FormattingEnabled = true;
            this.lbeReplace.Items.AddRange(new object[] {
            "==",
            "!=",
            "<",
            ">=",
            ">",
            "<="});
            this.lbeReplace.Location = new System.Drawing.Point(47, 46);
            this.lbeReplace.Name = "lbeReplace";
            this.lbeReplace.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbeReplace.Size = new System.Drawing.Size(23, 78);
            this.lbeReplace.TabIndex = 177;
            this.lbeReplace.Tag = "color:dark2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(44, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 176;
            this.label1.Text = "Replace";
            // 
            // cbMode
            // 
            this.cbMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMode.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbMode.ForeColor = System.Drawing.Color.White;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.IntegralHeight = false;
            this.cbMode.Items.AddRange(new object[] {
            "Invert",
            "Find & replace"});
            this.cbMode.Location = new System.Drawing.Point(101, 103);
            this.cbMode.MaxDropDownItems = 15;
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(125, 21);
            this.cbMode.TabIndex = 178;
            this.cbMode.Tag = "color:normal";
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(98, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 179;
            this.label4.Text = "Mode";
            // 
            // BranchEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label19);
            this.Name = "BranchEngineControl";
            this.Controls.SetChildIndex(this.engineGroupBox, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label19;
        public CheckBox cbLong;
        public CheckBox cbInt;
        public CheckBox cbFloat;
        public CheckBox cbDouble;
        public ListBoxExtended lbeFind;
        private Label label2;
        private Label label3;
        public CheckBox cbBoolean;
        public ListBoxExtended lbeReplace;
        private Label label1;
        private Label label4;
        public ComboBox cbMode;
    }
}
