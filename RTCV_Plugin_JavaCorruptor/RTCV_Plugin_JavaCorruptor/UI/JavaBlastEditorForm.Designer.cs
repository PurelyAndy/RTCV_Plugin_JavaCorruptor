using System.Windows.Forms;
using RTCV.UI;
using RTCV.UI.Components;

namespace Java_Corruptor.UI
{
	partial class JavaBlastEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaBlastEditorForm));
            this.dgvBlastEditor = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbFilterColumn = new System.Windows.Forms.ComboBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMethod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNote = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbLocked = new System.Windows.Forms.CheckBox();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnDisable50 = new System.Windows.Forms.Button();
            this.btnInvertDisabled = new System.Windows.Forms.Button();
            this.btnRemoveDisabled = new System.Windows.Forms.Button();
            this.btnSanitizeTool = new System.Windows.Forms.Button();
            this.btnDisableEverything = new System.Windows.Forms.Button();
            this.btnEnableEverything = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnDuplicateSelected = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnAddStashToStockpile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbShiftBlastlayer = new System.Windows.Forms.ComboBox();
            this.btnShiftBlastLayerDown = new System.Windows.Forms.Button();
            this.btnShiftBlastLayerUp = new System.Windows.Forms.Button();
            this.pnMemoryTargetting = new System.Windows.Forms.Panel();
            this.lbBlastLayerSize = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadCorrupt = new System.Windows.Forms.Button();
            this.btnCorrupt = new System.Windows.Forms.Button();
            this.btnSendToStash = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStripEx1 = new System.Windows.Forms.MenuStrip();
            this.blastLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFileblToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileblToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToFileblToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importBlastlayerblToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importBlastlayerFromCorruptedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dontShowBlastlayerNameInTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceRomFromGHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceRomFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sanitizeDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rasterizeVMDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.upDownReplaces = new RTCV.UI.Components.NumericUpDownHexFix();
            this.upDownIndex = new RTCV.UI.Components.NumericUpDownHexFix();
            this.updownShiftBlastLayerAmount = new RTCV.UI.Components.NumericUpDownHexFix();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlastEditor)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnMemoryTargetting.SuspendLayout();
            this.menuStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownReplaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updownShiftBlastLayerAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBlastEditor
            // 
            this.dgvBlastEditor.AllowUserToAddRows = false;
            this.dgvBlastEditor.AllowUserToResizeRows = false;
            this.dgvBlastEditor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBlastEditor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvBlastEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBlastEditor.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvBlastEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlastEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlastEditor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvBlastEditor.Location = new System.Drawing.Point(0, 0);
            this.dgvBlastEditor.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBlastEditor.Name = "dgvBlastEditor";
            this.dgvBlastEditor.RowHeadersVisible = false;
            this.dgvBlastEditor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvBlastEditor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBlastEditor.Size = new System.Drawing.Size(662, 356);
            this.dgvBlastEditor.TabIndex = 0;
            this.dgvBlastEditor.Tag = "color:normal";
            this.dgvBlastEditor.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnBlastEditorCellFormatting);
            this.dgvBlastEditor.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnBlastEditorCellMouseClick);
            this.dgvBlastEditor.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnBlastEditorCellMouseDoubleClick);
            this.dgvBlastEditor.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.OnBlastEditorCellValidating);
            this.dgvBlastEditor.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnBlastEditorCellValueChanged);
            this.dgvBlastEditor.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnBlastEditorColumnHeaderMouseClick);
            this.dgvBlastEditor.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnBlastEditorDataError);
            this.dgvBlastEditor.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnBlastEditorRowsAdded);
            this.dgvBlastEditor.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.OnBlastEditorRowsRemoved);
            this.dgvBlastEditor.SelectionChanged += new System.EventHandler(this.OnBlastEditorSelectionChange);
            this.dgvBlastEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnBlastEditorMouseClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbFilterColumn);
            this.panel2.Controls.Add(this.tbFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(662, 21);
            this.panel2.TabIndex = 148;
            this.panel2.Tag = "color:light1";
            // 
            // cbFilterColumn
            // 
            this.cbFilterColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilterColumn.BackColor = System.Drawing.Color.White;
            this.cbFilterColumn.ForeColor = System.Drawing.Color.Black;
            this.cbFilterColumn.FormattingEnabled = true;
            this.cbFilterColumn.Location = new System.Drawing.Point(462, -1);
            this.cbFilterColumn.Name = "cbFilterColumn";
            this.cbFilterColumn.Size = new System.Drawing.Size(100, 21);
            this.cbFilterColumn.TabIndex = 149;
            this.cbFilterColumn.SelectedValueChanged += new System.EventHandler(this.OnFilterTextChanged);
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(562, -1);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(100, 22);
            this.tbFilter.TabIndex = 7;
            this.tbFilter.TextChanged += new System.EventHandler(this.OnFilterTextChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelBottom.Controls.Add(this.splitContainer1);
            this.panelBottom.Controls.Add(this.label5);
            this.panelBottom.Controls.Add(this.panel5);
            this.panelBottom.Controls.Add(this.label2);
            this.panelBottom.Controls.Add(this.btnNote);
            this.panelBottom.Controls.Add(this.panel4);
            this.panelBottom.ForeColor = System.Drawing.Color.White;
            this.panelBottom.Location = new System.Drawing.Point(0, 21);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(662, 136);
            this.panelBottom.TabIndex = 149;
            this.panelBottom.Tag = "color:normal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Data";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(148, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(484, 98);
            this.panel5.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Value";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(267, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 149;
            this.label10.Text = "Replaces";
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbValue.ForeColor = System.Drawing.Color.White;
            this.tbValue.Location = new System.Drawing.Point(3, 20);
            this.tbValue.MaxLength = 16348;
            this.tbValue.Multiline = true;
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(154, 74);
            this.tbValue.TabIndex = 0;
            this.tbValue.Tag = "color:dark1";
            this.tbValue.Validated += new System.EventHandler(this.OnValueValidated);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(211, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Index";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Method";
            // 
            // tbMethod
            // 
            this.tbMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMethod.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMethod.ForeColor = System.Drawing.Color.White;
            this.tbMethod.Location = new System.Drawing.Point(6, 18);
            this.tbMethod.MaxLength = 16348;
            this.tbMethod.Name = "tbMethod";
            this.tbMethod.Size = new System.Drawing.Size(312, 20);
            this.tbMethod.TabIndex = 0;
            this.tbMethod.Tag = "color:dark1";
            this.tbMethod.Validated += new System.EventHandler(this.OnMethodValidated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Settings";
            // 
            // btnNote
            // 
            this.btnNote.BackColor = System.Drawing.Color.Gray;
            this.btnNote.FlatAppearance.BorderSize = 0;
            this.btnNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNote.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnNote.ForeColor = System.Drawing.Color.White;
            this.btnNote.Location = new System.Drawing.Point(20, 76);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(118, 35);
            this.btnNote.TabIndex = 142;
            this.btnNote.TabStop = false;
            this.btnNote.Tag = "color:light1";
            this.btnNote.Text = "Open Note Editor";
            this.btnNote.UseVisualStyleBackColor = false;
            this.btnNote.Click += new System.EventHandler(this.OpenNoteEditor);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.cbLocked);
            this.panel4.Controls.Add(this.cbEnabled);
            this.panel4.Location = new System.Drawing.Point(20, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(118, 42);
            this.panel4.TabIndex = 0;
            // 
            // cbLocked
            // 
            this.cbLocked.AutoSize = true;
            this.cbLocked.Location = new System.Drawing.Point(3, 21);
            this.cbLocked.Name = "cbLocked";
            this.cbLocked.Size = new System.Drawing.Size(62, 17);
            this.cbLocked.TabIndex = 1;
            this.cbLocked.Text = "Locked";
            this.cbLocked.UseVisualStyleBackColor = true;
            this.cbLocked.Validated += new System.EventHandler(this.OnLockedValidated);
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(3, 4);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(68, 17);
            this.cbEnabled.TabIndex = 0;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            this.cbEnabled.Validated += new System.EventHandler(this.OnEnabledValidated);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelSidebar.Controls.Add(this.btnDisable50);
            this.panelSidebar.Controls.Add(this.btnInvertDisabled);
            this.panelSidebar.Controls.Add(this.btnRemoveDisabled);
            this.panelSidebar.Controls.Add(this.btnSanitizeTool);
            this.panelSidebar.Controls.Add(this.btnDisableEverything);
            this.panelSidebar.Controls.Add(this.btnEnableEverything);
            this.panelSidebar.Controls.Add(this.btnRemoveSelected);
            this.panelSidebar.Controls.Add(this.btnDuplicateSelected);
            this.panelSidebar.Controls.Add(this.btnAddRow);
            this.panelSidebar.Controls.Add(this.btnAddStashToStockpile);
            this.panelSidebar.Controls.Add(this.panel1);
            this.panelSidebar.Controls.Add(this.pnMemoryTargetting);
            this.panelSidebar.Controls.Add(this.btnHelp);
            this.panelSidebar.Controls.Add(this.label3);
            this.panelSidebar.Controls.Add(this.btnLoadCorrupt);
            this.panelSidebar.Controls.Add(this.btnCorrupt);
            this.panelSidebar.Controls.Add(this.btnSendToStash);
            this.panelSidebar.Controls.Add(this.label4);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSidebar.Location = new System.Drawing.Point(662, 24);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(159, 517);
            this.panelSidebar.TabIndex = 146;
            this.panelSidebar.Tag = "color:dark1";
            // 
            // btnDisable50
            // 
            this.btnDisable50.BackColor = System.Drawing.Color.Gray;
            this.btnDisable50.FlatAppearance.BorderSize = 0;
            this.btnDisable50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisable50.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnDisable50.ForeColor = System.Drawing.Color.White;
            this.btnDisable50.Location = new System.Drawing.Point(14, 144);
            this.btnDisable50.Name = "btnDisable50";
            this.btnDisable50.Size = new System.Drawing.Size(135, 23);
            this.btnDisable50.TabIndex = 114;
            this.btnDisable50.TabStop = false;
            this.btnDisable50.Tag = "color:light1";
            this.btnDisable50.Text = "Disable 50%";
            this.btnDisable50.UseVisualStyleBackColor = false;
            this.btnDisable50.Click += new System.EventHandler(this.Disable50);
            // 
            // btnInvertDisabled
            // 
            this.btnInvertDisabled.BackColor = System.Drawing.Color.Gray;
            this.btnInvertDisabled.FlatAppearance.BorderSize = 0;
            this.btnInvertDisabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvertDisabled.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnInvertDisabled.ForeColor = System.Drawing.Color.White;
            this.btnInvertDisabled.Location = new System.Drawing.Point(14, 168);
            this.btnInvertDisabled.Name = "btnInvertDisabled";
            this.btnInvertDisabled.Size = new System.Drawing.Size(135, 23);
            this.btnInvertDisabled.TabIndex = 116;
            this.btnInvertDisabled.TabStop = false;
            this.btnInvertDisabled.Tag = "color:light1";
            this.btnInvertDisabled.Text = "Invert Disabled";
            this.btnInvertDisabled.UseVisualStyleBackColor = false;
            this.btnInvertDisabled.Click += new System.EventHandler(this.InvertDisabled);
            // 
            // btnRemoveDisabled
            // 
            this.btnRemoveDisabled.BackColor = System.Drawing.Color.Gray;
            this.btnRemoveDisabled.FlatAppearance.BorderSize = 0;
            this.btnRemoveDisabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveDisabled.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRemoveDisabled.ForeColor = System.Drawing.Color.White;
            this.btnRemoveDisabled.Location = new System.Drawing.Point(14, 192);
            this.btnRemoveDisabled.Name = "btnRemoveDisabled";
            this.btnRemoveDisabled.Size = new System.Drawing.Size(135, 23);
            this.btnRemoveDisabled.TabIndex = 115;
            this.btnRemoveDisabled.TabStop = false;
            this.btnRemoveDisabled.Tag = "color:light1";
            this.btnRemoveDisabled.Text = "Remove Disabled";
            this.btnRemoveDisabled.UseVisualStyleBackColor = false;
            this.btnRemoveDisabled.Click += new System.EventHandler(this.RemoveDisabled);
            // 
            // btnSanitizeTool
            // 
            this.btnSanitizeTool.BackColor = System.Drawing.Color.Gray;
            this.btnSanitizeTool.FlatAppearance.BorderSize = 0;
            this.btnSanitizeTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSanitizeTool.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSanitizeTool.ForeColor = System.Drawing.Color.White;
            this.btnSanitizeTool.Location = new System.Drawing.Point(15, 222);
            this.btnSanitizeTool.Name = "btnSanitizeTool";
            this.btnSanitizeTool.Size = new System.Drawing.Size(135, 23);
            this.btnSanitizeTool.TabIndex = 178;
            this.btnSanitizeTool.TabStop = false;
            this.btnSanitizeTool.Tag = "color:light1";
            this.btnSanitizeTool.Text = "Sanitize Tool";
            this.btnSanitizeTool.UseVisualStyleBackColor = false;
            this.btnSanitizeTool.Click += new System.EventHandler(this.OpenSanitizeTool);
            // 
            // btnDisableEverything
            // 
            this.btnDisableEverything.BackColor = System.Drawing.Color.Gray;
            this.btnDisableEverything.FlatAppearance.BorderSize = 0;
            this.btnDisableEverything.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableEverything.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnDisableEverything.ForeColor = System.Drawing.Color.White;
            this.btnDisableEverything.Location = new System.Drawing.Point(14, 252);
            this.btnDisableEverything.Name = "btnDisableEverything";
            this.btnDisableEverything.Size = new System.Drawing.Size(135, 23);
            this.btnDisableEverything.TabIndex = 128;
            this.btnDisableEverything.TabStop = false;
            this.btnDisableEverything.Tag = "color:light1";
            this.btnDisableEverything.Text = "Disable Everything";
            this.btnDisableEverything.UseVisualStyleBackColor = false;
            this.btnDisableEverything.Click += new System.EventHandler(this.DisableEverything);
            // 
            // btnEnableEverything
            // 
            this.btnEnableEverything.BackColor = System.Drawing.Color.Gray;
            this.btnEnableEverything.FlatAppearance.BorderSize = 0;
            this.btnEnableEverything.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnableEverything.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnEnableEverything.ForeColor = System.Drawing.Color.White;
            this.btnEnableEverything.Location = new System.Drawing.Point(14, 276);
            this.btnEnableEverything.Name = "btnEnableEverything";
            this.btnEnableEverything.Size = new System.Drawing.Size(135, 23);
            this.btnEnableEverything.TabIndex = 129;
            this.btnEnableEverything.TabStop = false;
            this.btnEnableEverything.Tag = "color:light1";
            this.btnEnableEverything.Text = "Enable Everything";
            this.btnEnableEverything.UseVisualStyleBackColor = false;
            this.btnEnableEverything.Click += new System.EventHandler(this.EnableEverything);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.BackColor = System.Drawing.Color.Gray;
            this.btnRemoveSelected.FlatAppearance.BorderSize = 0;
            this.btnRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelected.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRemoveSelected.ForeColor = System.Drawing.Color.White;
            this.btnRemoveSelected.Location = new System.Drawing.Point(14, 307);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(135, 23);
            this.btnRemoveSelected.TabIndex = 139;
            this.btnRemoveSelected.TabStop = false;
            this.btnRemoveSelected.Tag = "color:light1";
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = false;
            this.btnRemoveSelected.Click += new System.EventHandler(this.RemoveSelected);
            // 
            // btnDuplicateSelected
            // 
            this.btnDuplicateSelected.BackColor = System.Drawing.Color.Gray;
            this.btnDuplicateSelected.FlatAppearance.BorderSize = 0;
            this.btnDuplicateSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuplicateSelected.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnDuplicateSelected.ForeColor = System.Drawing.Color.White;
            this.btnDuplicateSelected.Location = new System.Drawing.Point(14, 337);
            this.btnDuplicateSelected.Name = "btnDuplicateSelected";
            this.btnDuplicateSelected.Size = new System.Drawing.Size(135, 23);
            this.btnDuplicateSelected.TabIndex = 130;
            this.btnDuplicateSelected.TabStop = false;
            this.btnDuplicateSelected.Tag = "color:light1";
            this.btnDuplicateSelected.Text = "Duplicate Selected";
            this.btnDuplicateSelected.UseVisualStyleBackColor = false;
            this.btnDuplicateSelected.Click += new System.EventHandler(this.DuplicateSelected);
            // 
            // btnAddRow
            // 
            this.btnAddRow.BackColor = System.Drawing.Color.Gray;
            this.btnAddRow.FlatAppearance.BorderSize = 0;
            this.btnAddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRow.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnAddRow.ForeColor = System.Drawing.Color.White;
            this.btnAddRow.Location = new System.Drawing.Point(14, 361);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(135, 23);
            this.btnAddRow.TabIndex = 177;
            this.btnAddRow.TabStop = false;
            this.btnAddRow.Tag = "color:light1";
            this.btnAddRow.Text = "Add New Row";
            this.btnAddRow.UseVisualStyleBackColor = false;
            this.btnAddRow.Click += new System.EventHandler(this.AddRow);
            // 
            // btnAddStashToStockpile
            // 
            this.btnAddStashToStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnAddStashToStockpile.FlatAppearance.BorderSize = 0;
            this.btnAddStashToStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStashToStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAddStashToStockpile.ForeColor = System.Drawing.Color.White;
            this.btnAddStashToStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStashToStockpile.Image")));
            this.btnAddStashToStockpile.Location = new System.Drawing.Point(14, 392);
            this.btnAddStashToStockpile.Name = "btnAddStashToStockpile";
            this.btnAddStashToStockpile.Size = new System.Drawing.Size(135, 23);
            this.btnAddStashToStockpile.TabIndex = 150;
            this.btnAddStashToStockpile.TabStop = false;
            this.btnAddStashToStockpile.Tag = "color:light1";
            this.btnAddStashToStockpile.Text = " To Stockpile";
            this.btnAddStashToStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddStashToStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddStashToStockpile.UseVisualStyleBackColor = false;
            this.btnAddStashToStockpile.Click += new System.EventHandler(this.AddStashToStockpile);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.cbShiftBlastlayer);
            this.panel1.Controls.Add(this.btnShiftBlastLayerDown);
            this.panel1.Controls.Add(this.btnShiftBlastLayerUp);
            this.panel1.Controls.Add(this.updownShiftBlastLayerAmount);
            this.panel1.Location = new System.Drawing.Point(14, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 60);
            this.panel1.TabIndex = 137;
            this.panel1.Tag = "color:light2";
            // 
            // cbShiftBlastlayer
            // 
            this.cbShiftBlastlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbShiftBlastlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShiftBlastlayer.ForeColor = System.Drawing.Color.White;
            this.cbShiftBlastlayer.FormattingEnabled = true;
            this.cbShiftBlastlayer.Location = new System.Drawing.Point(11, 7);
            this.cbShiftBlastlayer.Name = "cbShiftBlastlayer";
            this.cbShiftBlastlayer.Size = new System.Drawing.Size(114, 21);
            this.cbShiftBlastlayer.TabIndex = 148;
            this.cbShiftBlastlayer.Tag = "color:dark1";
            // 
            // btnShiftBlastLayerDown
            // 
            this.btnShiftBlastLayerDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShiftBlastLayerDown.FlatAppearance.BorderSize = 0;
            this.btnShiftBlastLayerDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftBlastLayerDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnShiftBlastLayerDown.ForeColor = System.Drawing.Color.Black;
            this.btnShiftBlastLayerDown.Location = new System.Drawing.Point(11, 34);
            this.btnShiftBlastLayerDown.Name = "btnShiftBlastLayerDown";
            this.btnShiftBlastLayerDown.Size = new System.Drawing.Size(21, 21);
            this.btnShiftBlastLayerDown.TabIndex = 147;
            this.btnShiftBlastLayerDown.TabStop = false;
            this.btnShiftBlastLayerDown.Tag = "color:light1";
            this.btnShiftBlastLayerDown.Text = "◀";
            this.btnShiftBlastLayerDown.UseVisualStyleBackColor = false;
            this.btnShiftBlastLayerDown.Click += new System.EventHandler(this.ShiftBlastLayerDown);
            // 
            // btnShiftBlastLayerUp
            // 
            this.btnShiftBlastLayerUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShiftBlastLayerUp.FlatAppearance.BorderSize = 0;
            this.btnShiftBlastLayerUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftBlastLayerUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnShiftBlastLayerUp.ForeColor = System.Drawing.Color.Black;
            this.btnShiftBlastLayerUp.Location = new System.Drawing.Point(103, 33);
            this.btnShiftBlastLayerUp.Name = "btnShiftBlastLayerUp";
            this.btnShiftBlastLayerUp.Size = new System.Drawing.Size(22, 22);
            this.btnShiftBlastLayerUp.TabIndex = 146;
            this.btnShiftBlastLayerUp.TabStop = false;
            this.btnShiftBlastLayerUp.Tag = "color:light1";
            this.btnShiftBlastLayerUp.Text = "▶";
            this.btnShiftBlastLayerUp.UseVisualStyleBackColor = false;
            this.btnShiftBlastLayerUp.Click += new System.EventHandler(this.ShiftBlastLayerUp);
            // 
            // pnMemoryTargetting
            // 
            this.pnMemoryTargetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMemoryTargetting.BackColor = System.Drawing.Color.Gray;
            this.pnMemoryTargetting.Controls.Add(this.lbBlastLayerSize);
            this.pnMemoryTargetting.Location = new System.Drawing.Point(14, 26);
            this.pnMemoryTargetting.Name = "pnMemoryTargetting";
            this.pnMemoryTargetting.Size = new System.Drawing.Size(135, 24);
            this.pnMemoryTargetting.TabIndex = 134;
            this.pnMemoryTargetting.Tag = "color:light2";
            // 
            // lbBlastLayerSize
            // 
            this.lbBlastLayerSize.ForeColor = System.Drawing.Color.White;
            this.lbBlastLayerSize.Location = new System.Drawing.Point(5, 5);
            this.lbBlastLayerSize.Name = "lbBlastLayerSize";
            this.lbBlastLayerSize.Size = new System.Drawing.Size(120, 19);
            this.lbBlastLayerSize.TabIndex = 132;
            this.lbBlastLayerSize.Text = "Layer size:";
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(127, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(27, 17);
            this.btnHelp.TabIndex = 176;
            this.btnHelp.TabStop = false;
            this.btnHelp.Tag = "color:dark1";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.ShowHelp);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 135;
            this.label3.Text = "BlastLayer Info";
            // 
            // btnLoadCorrupt
            // 
            this.btnLoadCorrupt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadCorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLoadCorrupt.FlatAppearance.BorderSize = 0;
            this.btnLoadCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadCorrupt.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnLoadCorrupt.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnLoadCorrupt.Location = new System.Drawing.Point(14, 426);
            this.btnLoadCorrupt.Name = "btnLoadCorrupt";
            this.btnLoadCorrupt.Size = new System.Drawing.Size(135, 25);
            this.btnLoadCorrupt.TabIndex = 14;
            this.btnLoadCorrupt.TabStop = false;
            this.btnLoadCorrupt.Tag = "color:dark2";
            this.btnLoadCorrupt.Text = "Load + Corrupt";
            this.btnLoadCorrupt.UseVisualStyleBackColor = false;
            this.btnLoadCorrupt.Click += new System.EventHandler(this.LoadCorrupt);
            // 
            // btnCorrupt
            // 
            this.btnCorrupt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCorrupt.FlatAppearance.BorderSize = 0;
            this.btnCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorrupt.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnCorrupt.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnCorrupt.Location = new System.Drawing.Point(14, 452);
            this.btnCorrupt.Name = "btnCorrupt";
            this.btnCorrupt.Size = new System.Drawing.Size(135, 25);
            this.btnCorrupt.TabIndex = 13;
            this.btnCorrupt.TabStop = false;
            this.btnCorrupt.Tag = "color:dark2";
            this.btnCorrupt.Text = "Apply Corruption";
            this.btnCorrupt.UseVisualStyleBackColor = false;
            this.btnCorrupt.Click += new System.EventHandler(this.Corrupt);
            // 
            // btnSendToStash
            // 
            this.btnSendToStash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendToStash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSendToStash.FlatAppearance.BorderSize = 0;
            this.btnSendToStash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToStash.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSendToStash.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnSendToStash.Location = new System.Drawing.Point(14, 478);
            this.btnSendToStash.Name = "btnSendToStash";
            this.btnSendToStash.Size = new System.Drawing.Size(135, 25);
            this.btnSendToStash.TabIndex = 12;
            this.btnSendToStash.TabStop = false;
            this.btnSendToStash.Tag = "color:dark2";
            this.btnSendToStash.Text = "Send To Stash";
            this.btnSendToStash.UseVisualStyleBackColor = false;
            this.btnSendToStash.Click += new System.EventHandler(this.SendToStash);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 136;
            this.label4.Text = "Shift Selected Rows";
            // 
            // menuStripEx1
            // 
            this.menuStripEx1.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.menuStripEx1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blastLayerToolStripMenuItem,
            this.rOMToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStripEx1.Location = new System.Drawing.Point(0, 0);
            this.menuStripEx1.Name = "menuStripEx1";
            this.menuStripEx1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStripEx1.Size = new System.Drawing.Size(821, 24);
            this.menuStripEx1.TabIndex = 145;
            this.menuStripEx1.Tag = "";
            this.menuStripEx1.Text = "menuStripEx1";
            // 
            // blastLayerToolStripMenuItem
            // 
            this.blastLayerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadFromFileblToolStripMenuItem,
            this.saveToFileblToolStripMenuItem,
            this.saveAsToFileblToolStripMenuItem,
            this.importBlastlayerblToolStripMenuItem,
            this.exportToCSVToolStripMenuItem,
            this.importBlastlayerFromCorruptedFileToolStripMenuItem,
            this.dontShowBlastlayerNameInTitleToolStripMenuItem});
            this.blastLayerToolStripMenuItem.Name = "blastLayerToolStripMenuItem";
            this.blastLayerToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.blastLayerToolStripMenuItem.Tag = "";
            this.blastLayerToolStripMenuItem.Text = "BlastLayer";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewBlastLayer);
            // 
            // loadFromFileblToolStripMenuItem
            // 
            this.loadFromFileblToolStripMenuItem.Name = "loadFromFileblToolStripMenuItem";
            this.loadFromFileblToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.loadFromFileblToolStripMenuItem.Text = "&Load From File (.jbl)";
            this.loadFromFileblToolStripMenuItem.Click += new System.EventHandler(this.LoadBlastLayerFromFile);
            // 
            // saveToFileblToolStripMenuItem
            // 
            this.saveToFileblToolStripMenuItem.Name = "saveToFileblToolStripMenuItem";
            this.saveToFileblToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveToFileblToolStripMenuItem.Text = "&Save to File (.jbl)";
            this.saveToFileblToolStripMenuItem.Click += new System.EventHandler(this.SaveBlastLayerToFile);
            // 
            // saveAsToFileblToolStripMenuItem
            // 
            this.saveAsToFileblToolStripMenuItem.Name = "saveAsToFileblToolStripMenuItem";
            this.saveAsToFileblToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveAsToFileblToolStripMenuItem.Text = "&Save As to File (.jbl)";
            this.saveAsToFileblToolStripMenuItem.Click += new System.EventHandler(this.SaveAsBlastLayerToFile);
            // 
            // importBlastlayerblToolStripMenuItem
            // 
            this.importBlastlayerblToolStripMenuItem.Name = "importBlastlayerblToolStripMenuItem";
            this.importBlastlayerblToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.importBlastlayerblToolStripMenuItem.Text = "&Import Blastlayer (.jbl)";
            this.importBlastlayerblToolStripMenuItem.Click += new System.EventHandler(this.ImportBlastLayer);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.exportToCSVToolStripMenuItem.Text = "&Export to CSV";
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.ExportBlastLayerToCSV);
            // 
            // importBlastlayerFromCorruptedFileToolStripMenuItem
            // 
            this.importBlastlayerFromCorruptedFileToolStripMenuItem.Name = "importBlastlayerFromCorruptedFileToolStripMenuItem";
            this.importBlastlayerFromCorruptedFileToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.importBlastlayerFromCorruptedFileToolStripMenuItem.Text = "Import Blastlayer From Corrupted &File";
            this.importBlastlayerFromCorruptedFileToolStripMenuItem.Click += new System.EventHandler(this.importBlastlayerFromCorruptedFileToolStripMenuItem_Click);
            // 
            // dontShowBlastlayerNameInTitleToolStripMenuItem
            // 
            this.dontShowBlastlayerNameInTitleToolStripMenuItem.CheckOnClick = true;
            this.dontShowBlastlayerNameInTitleToolStripMenuItem.Name = "dontShowBlastlayerNameInTitleToolStripMenuItem";
            this.dontShowBlastlayerNameInTitleToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.dontShowBlastlayerNameInTitleToolStripMenuItem.Text = "Don\'t Show Blastlayer Name in Title";
            this.dontShowBlastlayerNameInTitleToolStripMenuItem.Click += new System.EventHandler(this.showBlastlayerNameInTitleToolStripMenuItem_Click);
            // 
            // rOMToolStripMenuItem
            // 
            this.rOMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceRomFromGHToolStripMenuItem,
            this.replaceRomFromFileToolStripMenuItem});
            this.rOMToolStripMenuItem.Name = "rOMToolStripMenuItem";
            this.rOMToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.rOMToolStripMenuItem.Tag = "";
            this.rOMToolStripMenuItem.Text = "JAR";
            // 
            // replaceRomFromGHToolStripMenuItem
            // 
            this.replaceRomFromGHToolStripMenuItem.Name = "replaceRomFromGHToolStripMenuItem";
            this.replaceRomFromGHToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.replaceRomFromGHToolStripMenuItem.Text = "Replace JAR from GH";
            this.replaceRomFromGHToolStripMenuItem.Click += new System.EventHandler(this.ReplaceRomFromGlitchHarvester);
            // 
            // replaceRomFromFileToolStripMenuItem
            // 
            this.replaceRomFromFileToolStripMenuItem.Name = "replaceRomFromFileToolStripMenuItem";
            this.replaceRomFromFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.replaceRomFromFileToolStripMenuItem.Text = "Replace JAR from File";
            this.replaceRomFromFileToolStripMenuItem.Click += new System.EventHandler(this.ReplaceRomFromFile);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sanitizeDuplicatesToolStripMenuItem,
            this.rasterizeVMDsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Tag = "";
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // sanitizeDuplicatesToolStripMenuItem
            // 
            this.sanitizeDuplicatesToolStripMenuItem.Name = "sanitizeDuplicatesToolStripMenuItem";
            this.sanitizeDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.sanitizeDuplicatesToolStripMenuItem.Text = "Sanitize Duplicates";
            this.sanitizeDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.SanitizeDuplicates);
            // 
            // rasterizeVMDsToolStripMenuItem
            // 
            this.rasterizeVMDsToolStripMenuItem.Name = "rasterizeVMDsToolStripMenuItem";
            this.rasterizeVMDsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.rasterizeVMDsToolStripMenuItem.Text = "Rasterize VMDs";
            this.rasterizeVMDsToolStripMenuItem.Click += new System.EventHandler(this.RasterizeVMDs);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(148, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.tbValue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.upDownReplaces);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.tbMethod);
            this.splitContainer1.Panel2.Controls.Add(this.upDownIndex);
            this.splitContainer1.Size = new System.Drawing.Size(484, 98);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 143;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvBlastEditor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Panel2.Controls.Add(this.panelBottom);
            this.splitContainer2.Size = new System.Drawing.Size(662, 517);
            this.splitContainer2.SplitterDistance = 356;
            this.splitContainer2.TabIndex = 150;
            // 
            // upDownReplaces
            // 
            this.upDownReplaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.upDownReplaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.upDownReplaces.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.upDownReplaces.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.upDownReplaces.ForeColor = System.Drawing.Color.White;
            this.upDownReplaces.Location = new System.Drawing.Point(252, 73);
            this.upDownReplaces.Name = "upDownReplaces";
            this.upDownReplaces.Size = new System.Drawing.Size(65, 22);
            this.upDownReplaces.TabIndex = 148;
            this.upDownReplaces.Tag = "color:dark1";
            this.upDownReplaces.Validated += new System.EventHandler(this.OnReplacesValidated);
            // 
            // upDownIndex
            // 
            this.upDownIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.upDownIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.upDownIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.upDownIndex.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.upDownIndex.ForeColor = System.Drawing.Color.White;
            this.upDownIndex.Location = new System.Drawing.Point(181, 73);
            this.upDownIndex.Name = "upDownIndex";
            this.upDownIndex.Size = new System.Drawing.Size(65, 22);
            this.upDownIndex.TabIndex = 9;
            this.upDownIndex.Tag = "color:dark1";
            this.upDownIndex.Validated += new System.EventHandler(this.OnIndexValidated);
            // 
            // updownShiftBlastLayerAmount
            // 
            this.updownShiftBlastLayerAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updownShiftBlastLayerAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updownShiftBlastLayerAmount.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.updownShiftBlastLayerAmount.ForeColor = System.Drawing.Color.White;
            this.updownShiftBlastLayerAmount.Hexadecimal = true;
            this.updownShiftBlastLayerAmount.Location = new System.Drawing.Point(38, 33);
            this.updownShiftBlastLayerAmount.Name = "updownShiftBlastLayerAmount";
            this.updownShiftBlastLayerAmount.Size = new System.Drawing.Size(59, 22);
            this.updownShiftBlastLayerAmount.TabIndex = 145;
            this.updownShiftBlastLayerAmount.Tag = "color:dark1";
            // 
            // JavaBlastEditorForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 541);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.menuStripEx1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(837, 559);
            this.Name = "JavaBlastEditorForm";
            this.Text = "Blast Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnFormDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFormDragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlastEditor)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnMemoryTargetting.ResumeLayout(false);
            this.menuStripEx1.ResumeLayout(false);
            this.menuStripEx1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownReplaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updownShiftBlastLayerAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private System.Windows.Forms.Label label10;

		#endregion

		public System.Windows.Forms.DataGridView dgvBlastEditor;
		public System.Windows.Forms.Panel panelSidebar;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnLoadCorrupt;
		private System.Windows.Forms.Button btnRemoveSelected;
		private System.Windows.Forms.Button btnCorrupt;
		private System.Windows.Forms.Button btnSendToStash;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbShiftBlastlayer;
		private System.Windows.Forms.Button btnShiftBlastLayerDown;
		private System.Windows.Forms.Button btnShiftBlastLayerUp;
		private NumericUpDownHexFix updownShiftBlastLayerAmount;
		private System.Windows.Forms.Button btnRemoveDisabled;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnDisable50;
		private System.Windows.Forms.Button btnInvertDisabled;
		private System.Windows.Forms.Panel pnMemoryTargetting;
		private System.Windows.Forms.Label lbBlastLayerSize;
		private System.Windows.Forms.Button btnDisableEverything;
		private System.Windows.Forms.Button btnEnableEverything;
		private System.Windows.Forms.Button btnDuplicateSelected;
		private MenuStrip menuStripEx1;
		private System.Windows.Forms.ToolStripMenuItem rOMToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runRomWithoutBlastlayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceRomFromGHToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceRomFromFileToolStripMenuItem;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStripMenuItem blastLayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFromFileblToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToFileblToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToFileblToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importBlastlayerblToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sanitizeDuplicatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rasterizeVMDsToolStripMenuItem;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.CheckBox cbLocked;
		private System.Windows.Forms.CheckBox cbEnabled;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbMethod;
		private System.Windows.Forms.CheckBox cbBigEndian;
		private System.Windows.Forms.Label label9;
		private RTCV.UI.Components.NumericUpDownHexFix upDownIndex;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbValue;
		private RTCV.UI.Components.NumericUpDownHexFix upDownReplaces;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbFilterColumn;
		private System.Windows.Forms.TextBox tbFilter;
		private System.Windows.Forms.Button btnNote;
		private Button btnAddRow;
        private ToolStripMenuItem importBlastlayerFromCorruptedFileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private Button btnSanitizeTool;
        public Button btnAddStashToStockpile;
        private ToolStripMenuItem dontShowBlastlayerNameInTitleToolStripMenuItem;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
    }
}
