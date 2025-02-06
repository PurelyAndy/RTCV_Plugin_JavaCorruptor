using System.ComponentModel;
using System.Windows.Forms;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class BasicEngineControl : JavaEngineControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbValues = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.lbVectorEngineValueText1 = new System.Windows.Forms.Label();
            this.pnLimiterList = new System.Windows.Forms.Panel();
            this.lbLimiters = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.lbVectorEngineLimiterText1 = new System.Windows.Forms.Label();
            this.engineGroupBox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnLimiterList.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.label1);
            this.engineGroupBox.Controls.Add(this.panel2);
            this.engineGroupBox.Controls.Add(this.pnLimiterList);
            this.engineGroupBox.Controls.SetChildIndex(this.pnLimiterList, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.panel2, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(251, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Replaces one zero-operand instruction with another";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(170, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 154;
            this.label1.Text = "Replaces instructions with other instructions";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel2.Controls.Add(this.lbValues);
            this.panel2.Controls.Add(this.lbVectorEngineValueText1);
            this.panel2.Location = new System.Drawing.Point(221, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 145);
            this.panel2.TabIndex = 156;
            this.panel2.Tag = "color:dark2";
            // 
            // lbValues
            // 
            this.lbValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.lbValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbValues.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbValues.ForeColor = System.Drawing.Color.White;
            this.lbValues.FormattingEnabled = true;
            this.lbValues.Location = new System.Drawing.Point(3, 25);
            this.lbValues.Name = "lbValues";
            this.lbValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbValues.Size = new System.Drawing.Size(210, 117);
            this.lbValues.TabIndex = 139;
            this.lbValues.Tag = "color:dark3";
            this.lbValues.SelectedIndexChanged += new System.EventHandler(this.lbValues_SelectedIndexChanged);
            // 
            // lbVectorEngineValueText1
            // 
            this.lbVectorEngineValueText1.AutoSize = true;
            this.lbVectorEngineValueText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineValueText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineValueText1.Location = new System.Drawing.Point(3, 7);
            this.lbVectorEngineValueText1.Name = "lbVectorEngineValueText1";
            this.lbVectorEngineValueText1.Size = new System.Drawing.Size(82, 13);
            this.lbVectorEngineValueText1.TabIndex = 138;
            this.lbVectorEngineValueText1.Text = "Replace with...";
            // 
            // pnLimiterList
            // 
            this.pnLimiterList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnLimiterList.Controls.Add(this.lbLimiters);
            this.pnLimiterList.Controls.Add(this.lbVectorEngineLimiterText1);
            this.pnLimiterList.Location = new System.Drawing.Point(5, 33);
            this.pnLimiterList.Name = "pnLimiterList";
            this.pnLimiterList.Size = new System.Drawing.Size(207, 145);
            this.pnLimiterList.TabIndex = 155;
            this.pnLimiterList.Tag = "color:dark2";
            // 
            // lbLimiters
            // 
            this.lbLimiters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLimiters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.lbLimiters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLimiters.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbLimiters.ForeColor = System.Drawing.Color.White;
            this.lbLimiters.FormattingEnabled = true;
            this.lbLimiters.Location = new System.Drawing.Point(3, 25);
            this.lbLimiters.Name = "lbLimiters";
            this.lbLimiters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbLimiters.Size = new System.Drawing.Size(201, 117);
            this.lbLimiters.TabIndex = 146;
            this.lbLimiters.Tag = "color:dark3";
            this.lbLimiters.SelectedIndexChanged += new System.EventHandler(this.lbLimiters_SelectedIndexChanged);
            // 
            // lbVectorEngineLimiterText1
            // 
            this.lbVectorEngineLimiterText1.AutoSize = true;
            this.lbVectorEngineLimiterText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineLimiterText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineLimiterText1.Location = new System.Drawing.Point(2, 7);
            this.lbVectorEngineLimiterText1.Name = "lbVectorEngineLimiterText1";
            this.lbVectorEngineLimiterText1.Size = new System.Drawing.Size(39, 13);
            this.lbVectorEngineLimiterText1.TabIndex = 141;
            this.lbVectorEngineLimiterText1.Text = "Find...";
            // 
            // BasicEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.label19);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "BasicEngineControl";
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.engineGroupBox, 0);
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnLimiterList.ResumeLayout(false);
            this.pnLimiterList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label19;
        private Label label1;
        private Panel panel2;
        public RTCV.UI.Components.Controls.ListBoxExtended lbValues;
        private Label lbVectorEngineValueText1;
        private Panel pnLimiterList;
        public RTCV.UI.Components.Controls.ListBoxExtended lbLimiters;
        private Label lbVectorEngineLimiterText1;
    }
}
