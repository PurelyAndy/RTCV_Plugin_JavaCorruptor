using System.ComponentModel;
using System.Windows.Forms;

namespace Java_Corruptor.UI.Components.EngineControls
{
    partial class NukerEngineControl : JavaEngineControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NukerEngineControl));
            this.label19 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pgVoid = new System.Windows.Forms.TabPage();
            this.cbSkipInit = new System.Windows.Forms.CheckBox();
            this.cbSkipClinit = new System.Windows.Forms.CheckBox();
            this.cbVoid = new System.Windows.Forms.CheckBox();
            this.pgBool = new System.Windows.Forms.TabPage();
            this.cbBoolRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.cbFalse = new System.Windows.Forms.CheckBox();
            this.cbTrue = new System.Windows.Forms.CheckBox();
            this.cbBool = new System.Windows.Forms.CheckBox();
            this.pgChar = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCharacters = new System.Windows.Forms.TextBox();
            this.cbCharRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.cbChar = new System.Windows.Forms.CheckBox();
            this.pgDouble = new System.Windows.Forms.TabPage();
            this.cbDoubleRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbDoubleMaximum = new System.Windows.Forms.Label();
            this.tbDoubleMaximum = new System.Windows.Forms.TrackBar();
            this.cbDouble = new System.Windows.Forms.CheckBox();
            this.tbDoubleMinimum = new System.Windows.Forms.TrackBar();
            this.lbDoubleMinimum = new System.Windows.Forms.Label();
            this.pgFloat = new System.Windows.Forms.TabPage();
            this.cbFloatRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbFloatMaximum = new System.Windows.Forms.Label();
            this.tbFloatMaximum = new System.Windows.Forms.TrackBar();
            this.tbFloatMinimum = new System.Windows.Forms.TrackBar();
            this.lbFloatMinimum = new System.Windows.Forms.Label();
            this.cbFloat = new System.Windows.Forms.CheckBox();
            this.pgLong = new System.Windows.Forms.TabPage();
            this.cbLongRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbLongMaximum = new System.Windows.Forms.Label();
            this.tbLongMaximum = new System.Windows.Forms.TrackBar();
            this.tbLongMinimum = new System.Windows.Forms.TrackBar();
            this.lbLongMinimum = new System.Windows.Forms.Label();
            this.cbLong = new System.Windows.Forms.CheckBox();
            this.pgInt = new System.Windows.Forms.TabPage();
            this.cbIntRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbIntMaximum = new System.Windows.Forms.Label();
            this.tbIntMaximum = new System.Windows.Forms.TrackBar();
            this.tbIntMinimum = new System.Windows.Forms.TrackBar();
            this.lbIntMinimum = new System.Windows.Forms.Label();
            this.cbInt = new System.Windows.Forms.CheckBox();
            this.pgShort = new System.Windows.Forms.TabPage();
            this.cbShortRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbShortMaximum = new System.Windows.Forms.Label();
            this.tbShortMaximum = new System.Windows.Forms.TrackBar();
            this.tbShortMinimum = new System.Windows.Forms.TrackBar();
            this.lbShortMinimum = new System.Windows.Forms.Label();
            this.cbShort = new System.Windows.Forms.CheckBox();
            this.pgByte = new System.Windows.Forms.TabPage();
            this.cbByteRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.lbByteMaximum = new System.Windows.Forms.Label();
            this.tbByteMaximum = new System.Windows.Forms.TrackBar();
            this.tbByteMinimum = new System.Windows.Forms.TrackBar();
            this.lbByteMinimum = new System.Windows.Forms.Label();
            this.cbByte = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pgString = new System.Windows.Forms.TabPage();
            this.cbString = new System.Windows.Forms.CheckBox();
            this.rbCharset = new System.Windows.Forms.RadioButton();
            this.rbOnePerLine = new System.Windows.Forms.RadioButton();
            this.cbStringRuntimeRandom = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStringText = new System.Windows.Forms.TextBox();
            this.engineGroupBox.SuspendLayout();
            this.pgVoid.SuspendLayout();
            this.pgBool.SuspendLayout();
            this.pgChar.SuspendLayout();
            this.pgDouble.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoubleMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoubleMinimum)).BeginInit();
            this.pgFloat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFloatMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFloatMinimum)).BeginInit();
            this.pgLong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLongMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLongMinimum)).BeginInit();
            this.pgInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntMinimum)).BeginInit();
            this.pgShort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbShortMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbShortMinimum)).BeginInit();
            this.pgByte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbByteMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbByteMinimum)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.pgString.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineGroupBox
            // 
            this.engineGroupBox.Controls.Add(this.btnHelp);
            this.engineGroupBox.Controls.Add(this.tabControl1);
            this.engineGroupBox.Controls.Add(this.label19);
            this.engineGroupBox.Controls.SetChildIndex(this.label19, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.tabControl1, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.btnHelp, 0);
            this.engineGroupBox.Controls.SetChildIndex(this.placeholderComboBox, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(169, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 12);
            this.label19.TabIndex = 170;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(417, 12);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(19, 19);
            this.btnHelp.TabIndex = 215;
            this.btnHelp.Tag = "color:dark1";
            this.toolTip1.SetToolTip(this.btnHelp, "what the hell?");
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // pgVoid
            // 
            this.pgVoid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgVoid.Controls.Add(this.cbSkipInit);
            this.pgVoid.Controls.Add(this.cbSkipClinit);
            this.pgVoid.Controls.Add(this.cbVoid);
            this.pgVoid.Location = new System.Drawing.Point(4, 22);
            this.pgVoid.Name = "pgVoid";
            this.pgVoid.Size = new System.Drawing.Size(422, 119);
            this.pgVoid.TabIndex = 7;
            this.pgVoid.Tag = "color:dark2";
            this.pgVoid.Text = "Void";
            // 
            // cbSkipInit
            // 
            this.cbSkipInit.AutoSize = true;
            this.cbSkipInit.Checked = true;
            this.cbSkipInit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipInit.ForeColor = System.Drawing.Color.White;
            this.cbSkipInit.Location = new System.Drawing.Point(119, 39);
            this.cbSkipInit.Name = "cbSkipInit";
            this.cbSkipInit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSkipInit.Size = new System.Drawing.Size(184, 17);
            this.cbSkipInit.TabIndex = 170;
            this.cbSkipInit.Text = "Skip class initializers/constructors";
            this.cbSkipInit.UseVisualStyleBackColor = true;
            // 
            // cbSkipClinit
            // 
            this.cbSkipClinit.AutoSize = true;
            this.cbSkipClinit.Checked = true;
            this.cbSkipClinit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipClinit.ForeColor = System.Drawing.Color.White;
            this.cbSkipClinit.Location = new System.Drawing.Point(119, 62);
            this.cbSkipClinit.Name = "cbSkipClinit";
            this.cbSkipClinit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSkipClinit.Size = new System.Drawing.Size(149, 17);
            this.cbSkipClinit.TabIndex = 169;
            this.cbSkipClinit.Text = "Skip static class initializers";
            this.cbSkipClinit.UseVisualStyleBackColor = true;
            // 
            // cbVoid
            // 
            this.cbVoid.AutoSize = true;
            this.cbVoid.ForeColor = System.Drawing.Color.White;
            this.cbVoid.Location = new System.Drawing.Point(3, 3);
            this.cbVoid.Name = "cbVoid";
            this.cbVoid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbVoid.Size = new System.Drawing.Size(65, 17);
            this.cbVoid.TabIndex = 168;
            this.cbVoid.Text = "Enabled";
            this.cbVoid.UseVisualStyleBackColor = true;
            // 
            // pgBool
            // 
            this.pgBool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgBool.Controls.Add(this.cbBoolRuntimeRandom);
            this.pgBool.Controls.Add(this.cbFalse);
            this.pgBool.Controls.Add(this.cbTrue);
            this.pgBool.Controls.Add(this.cbBool);
            this.pgBool.Location = new System.Drawing.Point(4, 22);
            this.pgBool.Name = "pgBool";
            this.pgBool.Padding = new System.Windows.Forms.Padding(3);
            this.pgBool.Size = new System.Drawing.Size(422, 119);
            this.pgBool.TabIndex = 9;
            this.pgBool.Tag = "color:dark2";
            this.pgBool.Text = "Bool";
            // 
            // cbBoolRuntimeRandom
            // 
            this.cbBoolRuntimeRandom.AutoSize = true;
            this.cbBoolRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbBoolRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbBoolRuntimeRandom.Name = "cbBoolRuntimeRandom";
            this.cbBoolRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbBoolRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbBoolRuntimeRandom.TabIndex = 189;
            this.cbBoolRuntimeRandom.Text = "Runtime Randomization";
            this.cbBoolRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // cbFalse
            // 
            this.cbFalse.AutoSize = true;
            this.cbFalse.Checked = true;
            this.cbFalse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFalse.ForeColor = System.Drawing.Color.White;
            this.cbFalse.Location = new System.Drawing.Point(233, 51);
            this.cbFalse.Name = "cbFalse";
            this.cbFalse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFalse.Size = new System.Drawing.Size(51, 17);
            this.cbFalse.TabIndex = 172;
            this.cbFalse.Text = "False";
            this.cbFalse.UseVisualStyleBackColor = true;
            this.cbFalse.CheckedChanged += new System.EventHandler(this.cbFalse_CheckedChanged);
            // 
            // cbTrue
            // 
            this.cbTrue.AutoSize = true;
            this.cbTrue.Checked = true;
            this.cbTrue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTrue.ForeColor = System.Drawing.Color.White;
            this.cbTrue.Location = new System.Drawing.Point(179, 51);
            this.cbTrue.Name = "cbTrue";
            this.cbTrue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbTrue.Size = new System.Drawing.Size(48, 17);
            this.cbTrue.TabIndex = 171;
            this.cbTrue.Text = "True";
            this.cbTrue.UseVisualStyleBackColor = true;
            this.cbTrue.CheckedChanged += new System.EventHandler(this.cbTrue_CheckedChanged);
            // 
            // cbBool
            // 
            this.cbBool.AutoSize = true;
            this.cbBool.ForeColor = System.Drawing.Color.White;
            this.cbBool.Location = new System.Drawing.Point(3, 3);
            this.cbBool.Name = "cbBool";
            this.cbBool.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbBool.Size = new System.Drawing.Size(65, 17);
            this.cbBool.TabIndex = 170;
            this.cbBool.Text = "Enabled";
            this.cbBool.UseVisualStyleBackColor = true;
            // 
            // pgChar
            // 
            this.pgChar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgChar.Controls.Add(this.label1);
            this.pgChar.Controls.Add(this.tbCharacters);
            this.pgChar.Controls.Add(this.cbCharRuntimeRandom);
            this.pgChar.Controls.Add(this.cbChar);
            this.pgChar.Location = new System.Drawing.Point(4, 22);
            this.pgChar.Name = "pgChar";
            this.pgChar.Padding = new System.Windows.Forms.Padding(3);
            this.pgChar.Size = new System.Drawing.Size(422, 119);
            this.pgChar.TabIndex = 1;
            this.pgChar.Tag = "color:dark2";
            this.pgChar.Text = "Char";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(182, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 197;
            // 
            // tbCharacters
            // 
            this.tbCharacters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbCharacters.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbCharacters.ForeColor = System.Drawing.Color.White;
            this.tbCharacters.Location = new System.Drawing.Point(3, 26);
            this.tbCharacters.Multiline = true;
            this.tbCharacters.Name = "tbCharacters";
            this.tbCharacters.Size = new System.Drawing.Size(416, 90);
            this.tbCharacters.TabIndex = 190;
            this.tbCharacters.Tag = "color:normal";
            // 
            // cbCharRuntimeRandom
            // 
            this.cbCharRuntimeRandom.AutoSize = true;
            this.cbCharRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbCharRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbCharRuntimeRandom.Name = "cbCharRuntimeRandom";
            this.cbCharRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbCharRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbCharRuntimeRandom.TabIndex = 188;
            this.cbCharRuntimeRandom.Text = "Runtime Randomization";
            this.cbCharRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // cbChar
            // 
            this.cbChar.AutoSize = true;
            this.cbChar.ForeColor = System.Drawing.Color.White;
            this.cbChar.Location = new System.Drawing.Point(3, 3);
            this.cbChar.Name = "cbChar";
            this.cbChar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbChar.Size = new System.Drawing.Size(65, 17);
            this.cbChar.TabIndex = 167;
            this.cbChar.Text = "Enabled";
            this.cbChar.UseVisualStyleBackColor = true;
            // 
            // pgDouble
            // 
            this.pgDouble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgDouble.Controls.Add(this.cbDoubleRuntimeRandom);
            this.pgDouble.Controls.Add(this.lbDoubleMaximum);
            this.pgDouble.Controls.Add(this.tbDoubleMaximum);
            this.pgDouble.Controls.Add(this.cbDouble);
            this.pgDouble.Controls.Add(this.tbDoubleMinimum);
            this.pgDouble.Controls.Add(this.lbDoubleMinimum);
            this.pgDouble.Location = new System.Drawing.Point(4, 22);
            this.pgDouble.Name = "pgDouble";
            this.pgDouble.Size = new System.Drawing.Size(422, 119);
            this.pgDouble.TabIndex = 6;
            this.pgDouble.Tag = "color:dark2";
            this.pgDouble.Text = "Double";
            // 
            // cbDoubleRuntimeRandom
            // 
            this.cbDoubleRuntimeRandom.AutoSize = true;
            this.cbDoubleRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbDoubleRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbDoubleRuntimeRandom.Name = "cbDoubleRuntimeRandom";
            this.cbDoubleRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbDoubleRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbDoubleRuntimeRandom.TabIndex = 188;
            this.cbDoubleRuntimeRandom.Text = "Runtime Randomization";
            this.cbDoubleRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbDoubleMaximum
            // 
            this.lbDoubleMaximum.AutoSize = true;
            this.lbDoubleMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbDoubleMaximum.ForeColor = System.Drawing.Color.White;
            this.lbDoubleMaximum.Location = new System.Drawing.Point(3, 23);
            this.lbDoubleMaximum.Name = "lbDoubleMaximum";
            this.lbDoubleMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbDoubleMaximum.TabIndex = 166;
            // 
            // tbDoubleMaximum
            // 
            this.tbDoubleMaximum.AutoSize = false;
            this.tbDoubleMaximum.Location = new System.Drawing.Point(3, 39);
            this.tbDoubleMaximum.Maximum = 350000;
            this.tbDoubleMaximum.Name = "tbDoubleMaximum";
            this.tbDoubleMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbDoubleMaximum.TabIndex = 165;
            this.tbDoubleMaximum.TickFrequency = 10000;
            this.tbDoubleMaximum.Value = 3000;
            this.tbDoubleMaximum.Scroll += new System.EventHandler(this.UpdateFloatTrackbar);
            // 
            // cbDouble
            // 
            this.cbDouble.AutoSize = true;
            this.cbDouble.ForeColor = System.Drawing.Color.White;
            this.cbDouble.Location = new System.Drawing.Point(3, 3);
            this.cbDouble.Name = "cbDouble";
            this.cbDouble.Size = new System.Drawing.Size(65, 17);
            this.cbDouble.TabIndex = 159;
            this.cbDouble.Text = "Enabled";
            this.cbDouble.UseVisualStyleBackColor = true;
            // 
            // tbDoubleMinimum
            // 
            this.tbDoubleMinimum.AutoSize = false;
            this.tbDoubleMinimum.Location = new System.Drawing.Point(3, 86);
            this.tbDoubleMinimum.Maximum = 0;
            this.tbDoubleMinimum.Minimum = -350000;
            this.tbDoubleMinimum.Name = "tbDoubleMinimum";
            this.tbDoubleMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbDoubleMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbDoubleMinimum.TabIndex = 163;
            this.tbDoubleMinimum.TickFrequency = 10000;
            this.tbDoubleMinimum.Value = -3000;
            this.tbDoubleMinimum.Scroll += new System.EventHandler(this.UpdateFloatTrackbar);
            // 
            // lbDoubleMinimum
            // 
            this.lbDoubleMinimum.AutoSize = true;
            this.lbDoubleMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbDoubleMinimum.ForeColor = System.Drawing.Color.White;
            this.lbDoubleMinimum.Location = new System.Drawing.Point(3, 70);
            this.lbDoubleMinimum.Name = "lbDoubleMinimum";
            this.lbDoubleMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbDoubleMinimum.TabIndex = 164;
            // 
            // pgFloat
            // 
            this.pgFloat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgFloat.Controls.Add(this.cbFloatRuntimeRandom);
            this.pgFloat.Controls.Add(this.lbFloatMaximum);
            this.pgFloat.Controls.Add(this.tbFloatMaximum);
            this.pgFloat.Controls.Add(this.tbFloatMinimum);
            this.pgFloat.Controls.Add(this.lbFloatMinimum);
            this.pgFloat.Controls.Add(this.cbFloat);
            this.pgFloat.Location = new System.Drawing.Point(4, 22);
            this.pgFloat.Name = "pgFloat";
            this.pgFloat.Size = new System.Drawing.Size(422, 119);
            this.pgFloat.TabIndex = 5;
            this.pgFloat.Tag = "color:dark2";
            this.pgFloat.Text = "Float";
            // 
            // cbFloatRuntimeRandom
            // 
            this.cbFloatRuntimeRandom.AutoSize = true;
            this.cbFloatRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbFloatRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbFloatRuntimeRandom.Name = "cbFloatRuntimeRandom";
            this.cbFloatRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbFloatRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbFloatRuntimeRandom.TabIndex = 188;
            this.cbFloatRuntimeRandom.Text = "Runtime Randomization";
            this.cbFloatRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbFloatMaximum
            // 
            this.lbFloatMaximum.AutoSize = true;
            this.lbFloatMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbFloatMaximum.ForeColor = System.Drawing.Color.White;
            this.lbFloatMaximum.Location = new System.Drawing.Point(3, 23);
            this.lbFloatMaximum.Name = "lbFloatMaximum";
            this.lbFloatMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbFloatMaximum.TabIndex = 170;
            // 
            // tbFloatMaximum
            // 
            this.tbFloatMaximum.AutoSize = false;
            this.tbFloatMaximum.Location = new System.Drawing.Point(3, 39);
            this.tbFloatMaximum.Maximum = 350000;
            this.tbFloatMaximum.Name = "tbFloatMaximum";
            this.tbFloatMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbFloatMaximum.TabIndex = 169;
            this.tbFloatMaximum.TickFrequency = 10000;
            this.tbFloatMaximum.Value = 3000;
            this.tbFloatMaximum.Scroll += new System.EventHandler(this.UpdateFloatTrackbar);
            // 
            // tbFloatMinimum
            // 
            this.tbFloatMinimum.AutoSize = false;
            this.tbFloatMinimum.Location = new System.Drawing.Point(3, 86);
            this.tbFloatMinimum.Maximum = 0;
            this.tbFloatMinimum.Minimum = -350000;
            this.tbFloatMinimum.Name = "tbFloatMinimum";
            this.tbFloatMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbFloatMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbFloatMinimum.TabIndex = 167;
            this.tbFloatMinimum.TickFrequency = 10000;
            this.tbFloatMinimum.Value = -3000;
            this.tbFloatMinimum.Scroll += new System.EventHandler(this.UpdateFloatTrackbar);
            // 
            // lbFloatMinimum
            // 
            this.lbFloatMinimum.AutoSize = true;
            this.lbFloatMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbFloatMinimum.ForeColor = System.Drawing.Color.White;
            this.lbFloatMinimum.Location = new System.Drawing.Point(3, 70);
            this.lbFloatMinimum.Name = "lbFloatMinimum";
            this.lbFloatMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbFloatMinimum.TabIndex = 168;
            // 
            // cbFloat
            // 
            this.cbFloat.AutoSize = true;
            this.cbFloat.ForeColor = System.Drawing.Color.White;
            this.cbFloat.Location = new System.Drawing.Point(3, 3);
            this.cbFloat.Name = "cbFloat";
            this.cbFloat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbFloat.Size = new System.Drawing.Size(65, 17);
            this.cbFloat.TabIndex = 160;
            this.cbFloat.Text = "Enabled";
            this.cbFloat.UseVisualStyleBackColor = true;
            // 
            // pgLong
            // 
            this.pgLong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgLong.Controls.Add(this.cbLongRuntimeRandom);
            this.pgLong.Controls.Add(this.lbLongMaximum);
            this.pgLong.Controls.Add(this.tbLongMaximum);
            this.pgLong.Controls.Add(this.tbLongMinimum);
            this.pgLong.Controls.Add(this.lbLongMinimum);
            this.pgLong.Controls.Add(this.cbLong);
            this.pgLong.Location = new System.Drawing.Point(4, 22);
            this.pgLong.Name = "pgLong";
            this.pgLong.Size = new System.Drawing.Size(422, 119);
            this.pgLong.TabIndex = 4;
            this.pgLong.Tag = "color:dark2";
            this.pgLong.Text = "Long";
            // 
            // cbLongRuntimeRandom
            // 
            this.cbLongRuntimeRandom.AutoSize = true;
            this.cbLongRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbLongRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbLongRuntimeRandom.Name = "cbLongRuntimeRandom";
            this.cbLongRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLongRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbLongRuntimeRandom.TabIndex = 188;
            this.cbLongRuntimeRandom.Text = "Runtime Randomization";
            this.cbLongRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbLongMaximum
            // 
            this.lbLongMaximum.AutoSize = true;
            this.lbLongMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbLongMaximum.ForeColor = System.Drawing.Color.White;
            this.lbLongMaximum.Location = new System.Drawing.Point(3, 23);
            this.lbLongMaximum.Name = "lbLongMaximum";
            this.lbLongMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbLongMaximum.TabIndex = 174;
            // 
            // tbLongMaximum
            // 
            this.tbLongMaximum.AutoSize = false;
            this.tbLongMaximum.Location = new System.Drawing.Point(3, 39);
            this.tbLongMaximum.Maximum = 350;
            this.tbLongMaximum.Name = "tbLongMaximum";
            this.tbLongMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbLongMaximum.TabIndex = 173;
            this.tbLongMaximum.TickFrequency = 10;
            this.tbLongMaximum.Value = 3;
            this.tbLongMaximum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // tbLongMinimum
            // 
            this.tbLongMinimum.AutoSize = false;
            this.tbLongMinimum.Location = new System.Drawing.Point(3, 86);
            this.tbLongMinimum.Maximum = 0;
            this.tbLongMinimum.Minimum = -350;
            this.tbLongMinimum.Name = "tbLongMinimum";
            this.tbLongMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbLongMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbLongMinimum.TabIndex = 171;
            this.tbLongMinimum.TickFrequency = 10;
            this.tbLongMinimum.Value = -3;
            this.tbLongMinimum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // lbLongMinimum
            // 
            this.lbLongMinimum.AutoSize = true;
            this.lbLongMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbLongMinimum.ForeColor = System.Drawing.Color.White;
            this.lbLongMinimum.Location = new System.Drawing.Point(3, 70);
            this.lbLongMinimum.Name = "lbLongMinimum";
            this.lbLongMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbLongMinimum.TabIndex = 172;
            // 
            // cbLong
            // 
            this.cbLong.AutoSize = true;
            this.cbLong.ForeColor = System.Drawing.Color.White;
            this.cbLong.Location = new System.Drawing.Point(3, 3);
            this.cbLong.Name = "cbLong";
            this.cbLong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbLong.Size = new System.Drawing.Size(65, 17);
            this.cbLong.TabIndex = 162;
            this.cbLong.Text = "Enabled";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // pgInt
            // 
            this.pgInt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgInt.Controls.Add(this.cbIntRuntimeRandom);
            this.pgInt.Controls.Add(this.lbIntMaximum);
            this.pgInt.Controls.Add(this.tbIntMaximum);
            this.pgInt.Controls.Add(this.tbIntMinimum);
            this.pgInt.Controls.Add(this.lbIntMinimum);
            this.pgInt.Controls.Add(this.cbInt);
            this.pgInt.Location = new System.Drawing.Point(4, 22);
            this.pgInt.Name = "pgInt";
            this.pgInt.Size = new System.Drawing.Size(422, 119);
            this.pgInt.TabIndex = 3;
            this.pgInt.Tag = "color:dark2";
            this.pgInt.Text = "Int";
            // 
            // cbIntRuntimeRandom
            // 
            this.cbIntRuntimeRandom.AutoSize = true;
            this.cbIntRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbIntRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbIntRuntimeRandom.Name = "cbIntRuntimeRandom";
            this.cbIntRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbIntRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbIntRuntimeRandom.TabIndex = 188;
            this.cbIntRuntimeRandom.Text = "Runtime Randomization";
            this.cbIntRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbIntMaximum
            // 
            this.lbIntMaximum.AutoSize = true;
            this.lbIntMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbIntMaximum.ForeColor = System.Drawing.Color.White;
            this.lbIntMaximum.Location = new System.Drawing.Point(3, 23);
            this.lbIntMaximum.Name = "lbIntMaximum";
            this.lbIntMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbIntMaximum.TabIndex = 178;
            // 
            // tbIntMaximum
            // 
            this.tbIntMaximum.AutoSize = false;
            this.tbIntMaximum.Location = new System.Drawing.Point(3, 39);
            this.tbIntMaximum.Maximum = 350;
            this.tbIntMaximum.Name = "tbIntMaximum";
            this.tbIntMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbIntMaximum.TabIndex = 177;
            this.tbIntMaximum.TickFrequency = 10;
            this.tbIntMaximum.Value = 3;
            this.tbIntMaximum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // tbIntMinimum
            // 
            this.tbIntMinimum.AutoSize = false;
            this.tbIntMinimum.Location = new System.Drawing.Point(3, 86);
            this.tbIntMinimum.Maximum = 0;
            this.tbIntMinimum.Minimum = -350;
            this.tbIntMinimum.Name = "tbIntMinimum";
            this.tbIntMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbIntMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbIntMinimum.TabIndex = 175;
            this.tbIntMinimum.TickFrequency = 10;
            this.tbIntMinimum.Value = -3;
            this.tbIntMinimum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // lbIntMinimum
            // 
            this.lbIntMinimum.AutoSize = true;
            this.lbIntMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbIntMinimum.ForeColor = System.Drawing.Color.White;
            this.lbIntMinimum.Location = new System.Drawing.Point(3, 70);
            this.lbIntMinimum.Name = "lbIntMinimum";
            this.lbIntMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbIntMinimum.TabIndex = 176;
            // 
            // cbInt
            // 
            this.cbInt.AutoSize = true;
            this.cbInt.ForeColor = System.Drawing.Color.White;
            this.cbInt.Location = new System.Drawing.Point(3, 3);
            this.cbInt.Name = "cbInt";
            this.cbInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbInt.Size = new System.Drawing.Size(65, 17);
            this.cbInt.TabIndex = 161;
            this.cbInt.Text = "Enabled";
            this.cbInt.UseVisualStyleBackColor = true;
            // 
            // pgShort
            // 
            this.pgShort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgShort.Controls.Add(this.cbShortRuntimeRandom);
            this.pgShort.Controls.Add(this.lbShortMaximum);
            this.pgShort.Controls.Add(this.tbShortMaximum);
            this.pgShort.Controls.Add(this.tbShortMinimum);
            this.pgShort.Controls.Add(this.lbShortMinimum);
            this.pgShort.Controls.Add(this.cbShort);
            this.pgShort.Location = new System.Drawing.Point(4, 22);
            this.pgShort.Name = "pgShort";
            this.pgShort.Size = new System.Drawing.Size(422, 119);
            this.pgShort.TabIndex = 2;
            this.pgShort.Tag = "color:dark2";
            this.pgShort.Text = "Short";
            // 
            // cbShortRuntimeRandom
            // 
            this.cbShortRuntimeRandom.AutoSize = true;
            this.cbShortRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbShortRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbShortRuntimeRandom.Name = "cbShortRuntimeRandom";
            this.cbShortRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbShortRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbShortRuntimeRandom.TabIndex = 188;
            this.cbShortRuntimeRandom.Text = "Runtime Randomization";
            this.cbShortRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbShortMaximum
            // 
            this.lbShortMaximum.AutoSize = true;
            this.lbShortMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbShortMaximum.ForeColor = System.Drawing.Color.White;
            this.lbShortMaximum.Location = new System.Drawing.Point(3, 23);
            this.lbShortMaximum.Name = "lbShortMaximum";
            this.lbShortMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbShortMaximum.TabIndex = 182;
            // 
            // tbShortMaximum
            // 
            this.tbShortMaximum.AutoSize = false;
            this.tbShortMaximum.Location = new System.Drawing.Point(3, 39);
            this.tbShortMaximum.Maximum = 350;
            this.tbShortMaximum.Name = "tbShortMaximum";
            this.tbShortMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbShortMaximum.TabIndex = 181;
            this.tbShortMaximum.TickFrequency = 10;
            this.tbShortMaximum.Value = 3;
            this.tbShortMaximum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // tbShortMinimum
            // 
            this.tbShortMinimum.AutoSize = false;
            this.tbShortMinimum.Location = new System.Drawing.Point(3, 86);
            this.tbShortMinimum.Maximum = 0;
            this.tbShortMinimum.Minimum = -350;
            this.tbShortMinimum.Name = "tbShortMinimum";
            this.tbShortMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbShortMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbShortMinimum.TabIndex = 179;
            this.tbShortMinimum.TickFrequency = 10;
            this.tbShortMinimum.Value = -3;
            this.tbShortMinimum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // lbShortMinimum
            // 
            this.lbShortMinimum.AutoSize = true;
            this.lbShortMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbShortMinimum.ForeColor = System.Drawing.Color.White;
            this.lbShortMinimum.Location = new System.Drawing.Point(3, 70);
            this.lbShortMinimum.Name = "lbShortMinimum";
            this.lbShortMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbShortMinimum.TabIndex = 180;
            // 
            // cbShort
            // 
            this.cbShort.AutoSize = true;
            this.cbShort.ForeColor = System.Drawing.Color.White;
            this.cbShort.Location = new System.Drawing.Point(3, 3);
            this.cbShort.Name = "cbShort";
            this.cbShort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbShort.Size = new System.Drawing.Size(65, 17);
            this.cbShort.TabIndex = 166;
            this.cbShort.Text = "Enabled";
            this.cbShort.UseVisualStyleBackColor = true;
            // 
            // pgByte
            // 
            this.pgByte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgByte.Controls.Add(this.cbByteRuntimeRandom);
            this.pgByte.Controls.Add(this.lbByteMaximum);
            this.pgByte.Controls.Add(this.tbByteMaximum);
            this.pgByte.Controls.Add(this.tbByteMinimum);
            this.pgByte.Controls.Add(this.lbByteMinimum);
            this.pgByte.Controls.Add(this.cbByte);
            this.pgByte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgByte.Location = new System.Drawing.Point(4, 22);
            this.pgByte.Name = "pgByte";
            this.pgByte.Padding = new System.Windows.Forms.Padding(3);
            this.pgByte.Size = new System.Drawing.Size(422, 119);
            this.pgByte.TabIndex = 0;
            this.pgByte.Tag = "color:dark2";
            this.pgByte.Text = "Byte";
            // 
            // cbByteRuntimeRandom
            // 
            this.cbByteRuntimeRandom.AutoSize = true;
            this.cbByteRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbByteRuntimeRandom.Location = new System.Drawing.Point(281, 3);
            this.cbByteRuntimeRandom.Name = "cbByteRuntimeRandom";
            this.cbByteRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbByteRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbByteRuntimeRandom.TabIndex = 187;
            this.cbByteRuntimeRandom.Text = "Runtime Randomization";
            this.cbByteRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // lbByteMaximum
            // 
            this.lbByteMaximum.AutoSize = true;
            this.lbByteMaximum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbByteMaximum.ForeColor = System.Drawing.Color.White;
            this.lbByteMaximum.Location = new System.Drawing.Point(3, 20);
            this.lbByteMaximum.Name = "lbByteMaximum";
            this.lbByteMaximum.Size = new System.Drawing.Size(0, 13);
            this.lbByteMaximum.TabIndex = 186;
            // 
            // tbByteMaximum
            // 
            this.tbByteMaximum.AutoSize = false;
            this.tbByteMaximum.Location = new System.Drawing.Point(3, 36);
            this.tbByteMaximum.Maximum = 127;
            this.tbByteMaximum.Name = "tbByteMaximum";
            this.tbByteMaximum.Size = new System.Drawing.Size(416, 30);
            this.tbByteMaximum.TabIndex = 185;
            this.tbByteMaximum.TickFrequency = 10;
            this.tbByteMaximum.Value = 127;
            this.tbByteMaximum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // tbByteMinimum
            // 
            this.tbByteMinimum.AutoSize = false;
            this.tbByteMinimum.Location = new System.Drawing.Point(3, 83);
            this.tbByteMinimum.Maximum = 0;
            this.tbByteMinimum.Minimum = -128;
            this.tbByteMinimum.Name = "tbByteMinimum";
            this.tbByteMinimum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbByteMinimum.Size = new System.Drawing.Size(416, 30);
            this.tbByteMinimum.TabIndex = 183;
            this.tbByteMinimum.TickFrequency = 10;
            this.tbByteMinimum.Value = -128;
            this.tbByteMinimum.Scroll += new System.EventHandler(this.UpdateTrackbar);
            // 
            // lbByteMinimum
            // 
            this.lbByteMinimum.AutoSize = true;
            this.lbByteMinimum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbByteMinimum.ForeColor = System.Drawing.Color.White;
            this.lbByteMinimum.Location = new System.Drawing.Point(3, 67);
            this.lbByteMinimum.Name = "lbByteMinimum";
            this.lbByteMinimum.Size = new System.Drawing.Size(0, 13);
            this.lbByteMinimum.TabIndex = 184;
            // 
            // cbByte
            // 
            this.cbByte.AutoSize = true;
            this.cbByte.ForeColor = System.Drawing.Color.White;
            this.cbByte.Location = new System.Drawing.Point(3, 3);
            this.cbByte.Name = "cbByte";
            this.cbByte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbByte.Size = new System.Drawing.Size(65, 17);
            this.cbByte.TabIndex = 165;
            this.cbByte.Text = "Enabled";
            this.cbByte.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pgByte);
            this.tabControl1.Controls.Add(this.pgShort);
            this.tabControl1.Controls.Add(this.pgInt);
            this.tabControl1.Controls.Add(this.pgLong);
            this.tabControl1.Controls.Add(this.pgFloat);
            this.tabControl1.Controls.Add(this.pgDouble);
            this.tabControl1.Controls.Add(this.pgChar);
            this.tabControl1.Controls.Add(this.pgBool);
            this.tabControl1.Controls.Add(this.pgVoid);
            this.tabControl1.Controls.Add(this.pgString);
            this.tabControl1.Location = new System.Drawing.Point(6, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 145);
            this.tabControl1.TabIndex = 171;
            this.tabControl1.Tag = "color:dark2";
            // 
            // pgString
            // 
            this.pgString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pgString.Controls.Add(this.cbString);
            this.pgString.Controls.Add(this.rbCharset);
            this.pgString.Controls.Add(this.rbOnePerLine);
            this.pgString.Controls.Add(this.cbStringRuntimeRandom);
            this.pgString.Controls.Add(this.label2);
            this.pgString.Controls.Add(this.tbStringText);
            this.pgString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgString.Location = new System.Drawing.Point(4, 22);
            this.pgString.Name = "pgString";
            this.pgString.Padding = new System.Windows.Forms.Padding(3);
            this.pgString.Size = new System.Drawing.Size(422, 119);
            this.pgString.TabIndex = 0;
            this.pgString.Tag = "color:dark2";
            this.pgString.Text = "String";
            // 
            // cbString
            // 
            this.cbString.AutoSize = true;
            this.cbString.ForeColor = System.Drawing.Color.White;
            this.cbString.Location = new System.Drawing.Point(3, 2);
            this.cbString.Name = "cbString";
            this.cbString.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbString.Size = new System.Drawing.Size(99, 17);
            this.cbString.TabIndex = 199;
            this.cbString.Text = "Strings enabled";
            this.cbString.UseVisualStyleBackColor = true;
            // 
            // rbCharset
            // 
            this.rbCharset.AutoSize = true;
            this.rbCharset.Checked = true;
            this.rbCharset.ForeColor = System.Drawing.Color.White;
            this.rbCharset.Location = new System.Drawing.Point(103, 1);
            this.rbCharset.Name = "rbCharset";
            this.rbCharset.Size = new System.Drawing.Size(76, 17);
            this.rbCharset.TabIndex = 202;
            this.rbCharset.TabStop = true;
            this.rbCharset.Text = "Charset (5)";
            this.rbCharset.UseVisualStyleBackColor = true;
            // 
            // rbOnePerLine
            // 
            this.rbOnePerLine.AutoSize = true;
            this.rbOnePerLine.ForeColor = System.Drawing.Color.White;
            this.rbOnePerLine.Location = new System.Drawing.Point(204, 1);
            this.rbOnePerLine.Name = "rbOnePerLine";
            this.rbOnePerLine.Size = new System.Drawing.Size(82, 17);
            this.rbOnePerLine.TabIndex = 201;
            this.rbOnePerLine.Text = "One per line";
            this.rbOnePerLine.UseVisualStyleBackColor = true;
            // 
            // cbStringRuntimeRandom
            // 
            this.cbStringRuntimeRandom.AutoSize = true;
            this.cbStringRuntimeRandom.Checked = true;
            this.cbStringRuntimeRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStringRuntimeRandom.ForeColor = System.Drawing.Color.White;
            this.cbStringRuntimeRandom.Location = new System.Drawing.Point(281, 2);
            this.cbStringRuntimeRandom.Name = "cbStringRuntimeRandom";
            this.cbStringRuntimeRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbStringRuntimeRandom.Size = new System.Drawing.Size(138, 17);
            this.cbStringRuntimeRandom.TabIndex = 200;
            this.cbStringRuntimeRandom.Text = "Runtime Randomization";
            this.cbStringRuntimeRandom.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(356, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 204;
            // 
            // tbStringText
            // 
            this.tbStringText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.tbStringText.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbStringText.ForeColor = System.Drawing.Color.White;
            this.tbStringText.Location = new System.Drawing.Point(3, 25);
            this.tbStringText.Multiline = true;
            this.tbStringText.Name = "tbStringText";
            this.tbStringText.Size = new System.Drawing.Size(413, 93);
            this.tbStringText.TabIndex = 203;
            this.tbStringText.Tag = "color:normal";
            this.tbStringText.Text = "abcdefghijklmnop0123456789";
            // 
            // NukerEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "NukerEngineControl";
            this.engineGroupBox.ResumeLayout(false);
            this.engineGroupBox.PerformLayout();
            this.pgVoid.ResumeLayout(false);
            this.pgVoid.PerformLayout();
            this.pgBool.ResumeLayout(false);
            this.pgBool.PerformLayout();
            this.pgChar.ResumeLayout(false);
            this.pgChar.PerformLayout();
            this.pgDouble.ResumeLayout(false);
            this.pgDouble.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoubleMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoubleMinimum)).EndInit();
            this.pgFloat.ResumeLayout(false);
            this.pgFloat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFloatMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFloatMinimum)).EndInit();
            this.pgLong.ResumeLayout(false);
            this.pgLong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLongMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLongMinimum)).EndInit();
            this.pgInt.ResumeLayout(false);
            this.pgInt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntMinimum)).EndInit();
            this.pgShort.ResumeLayout(false);
            this.pgShort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbShortMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbShortMinimum)).EndInit();
            this.pgByte.ResumeLayout(false);
            this.pgByte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbByteMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbByteMinimum)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.pgString.ResumeLayout(false);
            this.pgString.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabPage pgString;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        public System.Windows.Forms.CheckBox cbByte;
        public System.Windows.Forms.CheckBox cbShort;
        public System.Windows.Forms.CheckBox cbChar;
        public System.Windows.Forms.CheckBox cbVoid;

        private System.Windows.Forms.Label lbDoubleMinimum;
        public System.Windows.Forms.TrackBar tbDoubleMinimum;
        public System.Windows.Forms.CheckBox cbLong;
        public System.Windows.Forms.CheckBox cbInt;
        public System.Windows.Forms.CheckBox cbFloat;
        public System.Windows.Forms.CheckBox cbDouble;

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pgByte;
        private System.Windows.Forms.TabPage pgChar;
        private System.Windows.Forms.TabPage pgShort;
        private System.Windows.Forms.TabPage pgInt;
        private System.Windows.Forms.TabPage pgLong;
        private System.Windows.Forms.TabPage pgFloat;
        private System.Windows.Forms.TabPage pgDouble;
        private System.Windows.Forms.TabPage pgVoid;
        private System.Windows.Forms.Label lbDoubleMaximum;
        public System.Windows.Forms.TrackBar tbDoubleMaximum;
        private System.Windows.Forms.Label lbFloatMaximum;
        public System.Windows.Forms.TrackBar tbFloatMaximum;
        public System.Windows.Forms.TrackBar tbFloatMinimum;
        private System.Windows.Forms.Label lbFloatMinimum;
        private System.Windows.Forms.Label lbLongMaximum;
        public System.Windows.Forms.TrackBar tbLongMaximum;
        public System.Windows.Forms.TrackBar tbLongMinimum;
        private System.Windows.Forms.Label lbLongMinimum;
        private System.Windows.Forms.Label lbIntMaximum;
        public System.Windows.Forms.TrackBar tbIntMaximum;
        public System.Windows.Forms.TrackBar tbIntMinimum;
        private System.Windows.Forms.Label lbIntMinimum;
        private System.Windows.Forms.Label lbShortMaximum;
        public System.Windows.Forms.TrackBar tbShortMaximum;
        public System.Windows.Forms.TrackBar tbShortMinimum;
        private System.Windows.Forms.Label lbShortMinimum;
        private System.Windows.Forms.Label lbByteMaximum;
        public System.Windows.Forms.TrackBar tbByteMaximum;
        public System.Windows.Forms.TrackBar tbByteMinimum;
        private System.Windows.Forms.Label lbByteMinimum;
        public System.Windows.Forms.CheckBox cbByteRuntimeRandom;
        public System.Windows.Forms.CheckBox cbShortRuntimeRandom;
        public System.Windows.Forms.CheckBox cbIntRuntimeRandom;
        public System.Windows.Forms.CheckBox cbLongRuntimeRandom;
        public System.Windows.Forms.CheckBox cbFloatRuntimeRandom;
        public System.Windows.Forms.CheckBox cbDoubleRuntimeRandom;
        public System.Windows.Forms.CheckBox cbCharRuntimeRandom;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CheckBox cbSkipClinit;
        public System.Windows.Forms.CheckBox cbSkipInit;
        public System.Windows.Forms.TextBox tbCharacters;
        public System.Windows.Forms.CheckBox cbStringRuntimeRandom;
        private System.Windows.Forms.RadioButton rbCharset;
        private System.Windows.Forms.RadioButton rbOnePerLine;
        public System.Windows.Forms.CheckBox cbString;
        public System.Windows.Forms.TextBox tbStringText;
        private System.Windows.Forms.TabPage pgBool;
        public System.Windows.Forms.CheckBox cbBool;
        public System.Windows.Forms.CheckBox cbTrue;
        public System.Windows.Forms.CheckBox cbFalse;
        public System.Windows.Forms.CheckBox cbBoolRuntimeRandom;
    }
}
