using System.ComponentModel;
using System.Windows.Forms;
using RTCV.UI.Components.Controls;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class FunctionEngineControl : JavaEngineControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbValueFunctions = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.lbVectorEngineValueText1 = new System.Windows.Forms.Label();
            this.pnLimiterList = new System.Windows.Forms.Panel();
            this.lbLimiterFunctions = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.lbVectorEngineLimiterText1 = new System.Windows.Forms.Label();
            this.engineGroupBox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnLimiterList.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.label19);
            this.engineGroupBox.Controls.Add(this.panel2);
            this.engineGroupBox.Controls.Add(this.pnLimiterList);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.pnLimiterList, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.panel2, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.label19, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(170, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(158, 13);
            this.label19.TabIndex = 151;
            this.label19.Text = "Switches math functions around";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel2.Controls.Add(this.lbValueFunctions);
            this.panel2.Controls.Add(this.lbVectorEngineValueText1);
            this.panel2.Location = new System.Drawing.Point(221, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 145);
            this.panel2.TabIndex = 153;
            this.panel2.Tag = "color:dark2";
            // 
            // lbValueFunctions
            // 
            this.lbValueFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbValueFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.lbValueFunctions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbValueFunctions.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbValueFunctions.ForeColor = System.Drawing.Color.White;
            this.lbValueFunctions.FormattingEnabled = true;
            this.lbValueFunctions.Items.AddRange(new object[] {
            "POP,random()",
            "abs",
            "acos",
            "asin",
            "atan",
            "cbrt",
            "ceil",
            "cos",
            "cosh",
            "exp",
            "expm1",
            "floor",
            "log",
            "log1p",
            "log10",
            "nextDown",
            "nextUp",
            "rint",
            "round",
            "signum",
            "sin",
            "sinh",
            "sqrt",
            "tan",
            "tanh",
            "toDegrees",
            "toRadians",
            "ulp"});
            this.lbValueFunctions.Location = new System.Drawing.Point(3, 25);
            this.lbValueFunctions.Name = "lbValueFunctions";
            this.lbValueFunctions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbValueFunctions.Size = new System.Drawing.Size(207, 117);
            this.lbValueFunctions.TabIndex = 139;
            this.lbValueFunctions.Tag = "color:dark3";
            // 
            // lbVectorEngineValueText1
            // 
            this.lbVectorEngineValueText1.AutoSize = true;
            this.lbVectorEngineValueText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineValueText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineValueText1.Location = new System.Drawing.Point(3, 7);
            this.lbVectorEngineValueText1.Name = "lbVectorEngineValueText1";
            this.lbVectorEngineValueText1.Size = new System.Drawing.Size(87, 13);
            this.lbVectorEngineValueText1.TabIndex = 138;
            this.lbVectorEngineValueText1.Text = "Value functions";
            // 
            // pnLimiterList
            // 
            this.pnLimiterList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnLimiterList.Controls.Add(this.lbLimiterFunctions);
            this.pnLimiterList.Controls.Add(this.lbVectorEngineLimiterText1);
            this.pnLimiterList.Location = new System.Drawing.Point(5, 33);
            this.pnLimiterList.Name = "pnLimiterList";
            this.pnLimiterList.Size = new System.Drawing.Size(207, 145);
            this.pnLimiterList.TabIndex = 152;
            this.pnLimiterList.Tag = "color:dark2";
            // 
            // lbLimiterFunctions
            // 
            this.lbLimiterFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLimiterFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.lbLimiterFunctions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLimiterFunctions.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbLimiterFunctions.ForeColor = System.Drawing.Color.White;
            this.lbLimiterFunctions.FormattingEnabled = true;
            this.lbLimiterFunctions.Items.AddRange(new object[] {
            "abs",
            "acos",
            "asin",
            "atan",
            "cbrt",
            "ceil",
            "cos",
            "cosh",
            "exp",
            "expm1",
            "floor",
            "log",
            "log1p",
            "log10",
            "nextDown",
            "nextUp",
            "rint",
            "round",
            "signum",
            "sin",
            "sinh",
            "sqrt",
            "tan",
            "tanh",
            "toDegrees",
            "toRadians",
            "ulp"});
            this.lbLimiterFunctions.Location = new System.Drawing.Point(3, 25);
            this.lbLimiterFunctions.Name = "lbLimiterFunctions";
            this.lbLimiterFunctions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbLimiterFunctions.Size = new System.Drawing.Size(196, 117);
            this.lbLimiterFunctions.TabIndex = 146;
            this.lbLimiterFunctions.Tag = "color:dark3";
            // 
            // lbVectorEngineLimiterText1
            // 
            this.lbVectorEngineLimiterText1.AutoSize = true;
            this.lbVectorEngineLimiterText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineLimiterText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineLimiterText1.Location = new System.Drawing.Point(2, 7);
            this.lbVectorEngineLimiterText1.Name = "lbVectorEngineLimiterText1";
            this.lbVectorEngineLimiterText1.Size = new System.Drawing.Size(93, 13);
            this.lbVectorEngineLimiterText1.TabIndex = 141;
            this.lbVectorEngineLimiterText1.Text = "Limiter functions";
            // 
            // FunctionEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FunctionEngineControl";
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnLimiterList.ResumeLayout(false);
            this.pnLimiterList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label19;
        private Panel panel2;
        private Label lbVectorEngineValueText1;
        private System.Windows.Forms.Panel pnLimiterList;
        private Label lbVectorEngineLimiterText1;
        public RTCV.UI.Components.Controls.ListBoxExtended lbValueFunctions;
        public RTCV.UI.Components.Controls.ListBoxExtended lbLimiterFunctions;
    }
}
