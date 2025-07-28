﻿using System.ComponentModel;
using System.Windows.Forms;
using RTCV.UI.Components;

namespace Java_Corruptor.UI.Components
{
    partial class JavaStockpileManagerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaStockpileManagerForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoadStockpile = new System.Windows.Forms.Button();
            this.btnSaveStockpile = new System.Windows.Forms.Button();
            this.btnSaveStockpileAs = new System.Windows.Forms.Button();
            this.btnClearStockpile = new System.Windows.Forms.Button();
            this.btnStockpileDOWN = new System.Windows.Forms.Button();
            this.btnStockpileUP = new System.Windows.Forms.Button();
            this.dgvStockpile = new RTCV.UI.Components.DataGridViewDraggable();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnRenameSelected = new System.Windows.Forms.Button();
            this.btnImportStockpile = new System.Windows.Forms.Button();
            this.btnStockpileMoveSelectedDown = new System.Windows.Forms.Button();
            this.btnStockpileMoveSelectedUp = new System.Windows.Forms.Button();
            this.btnRemoveSelectedStockpile = new System.Windows.Forms.Button();
            this.btnGlitchHarvesterSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadStockpile
            // 
            this.btnLoadStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnLoadStockpile.FlatAppearance.BorderSize = 0;
            this.btnLoadStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLoadStockpile.ForeColor = System.Drawing.Color.White;
            this.btnLoadStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadStockpile.Image")));
            this.btnLoadStockpile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadStockpile.Location = new System.Drawing.Point(124, 3);
            this.btnLoadStockpile.Name = "btnLoadStockpile";
            this.btnLoadStockpile.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnLoadStockpile.Size = new System.Drawing.Size(68, 32);
            this.btnLoadStockpile.TabIndex = 173;
            this.btnLoadStockpile.TabStop = false;
            this.btnLoadStockpile.Tag = "color:light1";
            this.btnLoadStockpile.Text = "  Load";
            this.btnLoadStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadStockpile.UseVisualStyleBackColor = false;
            this.btnLoadStockpile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoadStockpile);
            // 
            // btnSaveStockpile
            // 
            this.btnSaveStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnSaveStockpile.Enabled = false;
            this.btnSaveStockpile.FlatAppearance.BorderSize = 0;
            this.btnSaveStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSaveStockpile.ForeColor = System.Drawing.Color.White;
            this.btnSaveStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveStockpile.Image")));
            this.btnSaveStockpile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStockpile.Location = new System.Drawing.Point(278, 3);
            this.btnSaveStockpile.Name = "btnSaveStockpile";
            this.btnSaveStockpile.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnSaveStockpile.Size = new System.Drawing.Size(66, 32);
            this.btnSaveStockpile.TabIndex = 176;
            this.btnSaveStockpile.TabStop = false;
            this.btnSaveStockpile.Tag = "color:light1";
            this.btnSaveStockpile.Text = "  Save";
            this.btnSaveStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveStockpile.UseVisualStyleBackColor = false;
            this.btnSaveStockpile.Click += new System.EventHandler(this.SaveStockpile);
            // 
            // btnSaveStockpileAs
            // 
            this.btnSaveStockpileAs.BackColor = System.Drawing.Color.Gray;
            this.btnSaveStockpileAs.FlatAppearance.BorderSize = 0;
            this.btnSaveStockpileAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveStockpileAs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSaveStockpileAs.ForeColor = System.Drawing.Color.White;
            this.btnSaveStockpileAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveStockpileAs.Image")));
            this.btnSaveStockpileAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStockpileAs.Location = new System.Drawing.Point(195, 3);
            this.btnSaveStockpileAs.Name = "btnSaveStockpileAs";
            this.btnSaveStockpileAs.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnSaveStockpileAs.Size = new System.Drawing.Size(80, 32);
            this.btnSaveStockpileAs.TabIndex = 172;
            this.btnSaveStockpileAs.TabStop = false;
            this.btnSaveStockpileAs.Tag = "color:light1";
            this.btnSaveStockpileAs.Text = "  Save as";
            this.btnSaveStockpileAs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStockpileAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveStockpileAs.UseVisualStyleBackColor = false;
            this.btnSaveStockpileAs.Click += new System.EventHandler(this.SaveStockpileAs);
            // 
            // btnClearStockpile
            // 
            this.btnClearStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnClearStockpile.FlatAppearance.BorderSize = 0;
            this.btnClearStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearStockpile.ForeColor = System.Drawing.Color.White;
            this.btnClearStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnClearStockpile.Image")));
            this.btnClearStockpile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearStockpile.Location = new System.Drawing.Point(3, 3);
            this.btnClearStockpile.Name = "btnClearStockpile";
            this.btnClearStockpile.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnClearStockpile.Size = new System.Drawing.Size(118, 32);
            this.btnClearStockpile.TabIndex = 177;
            this.btnClearStockpile.TabStop = false;
            this.btnClearStockpile.Tag = "color:light1";
            this.btnClearStockpile.Text = "  Clear stockpile";
            this.btnClearStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearStockpile.UseVisualStyleBackColor = false;
            this.btnClearStockpile.Click += new System.EventHandler(this.ClearStockpile);
            // 
            // btnStockpileDOWN
            // 
            this.btnStockpileDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockpileDOWN.BackColor = System.Drawing.Color.Gray;
            this.btnStockpileDOWN.FlatAppearance.BorderSize = 0;
            this.btnStockpileDOWN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockpileDOWN.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.btnStockpileDOWN.ForeColor = System.Drawing.Color.White;
            this.btnStockpileDOWN.Location = new System.Drawing.Point(417, 3);
            this.btnStockpileDOWN.Name = "btnStockpileDOWN";
            this.btnStockpileDOWN.Size = new System.Drawing.Size(32, 32);
            this.btnStockpileDOWN.TabIndex = 175;
            this.btnStockpileDOWN.TabStop = false;
            this.btnStockpileDOWN.Tag = "color:light1";
            this.btnStockpileDOWN.Text = "▼";
            this.btnStockpileDOWN.UseVisualStyleBackColor = false;
            this.btnStockpileDOWN.Click += new System.EventHandler(this.StockpileDown);
            // 
            // btnStockpileUP
            // 
            this.btnStockpileUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockpileUP.BackColor = System.Drawing.Color.Gray;
            this.btnStockpileUP.FlatAppearance.BorderSize = 0;
            this.btnStockpileUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockpileUP.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.btnStockpileUP.ForeColor = System.Drawing.Color.White;
            this.btnStockpileUP.Location = new System.Drawing.Point(382, 3);
            this.btnStockpileUP.Name = "btnStockpileUP";
            this.btnStockpileUP.Size = new System.Drawing.Size(32, 32);
            this.btnStockpileUP.TabIndex = 174;
            this.btnStockpileUP.TabStop = false;
            this.btnStockpileUP.Tag = "color:light1";
            this.btnStockpileUP.Text = "▲";
            this.btnStockpileUP.UseVisualStyleBackColor = false;
            this.btnStockpileUP.Click += new System.EventHandler(this.StockpileUp);
            // 
            // dgvStockpile
            // 
            this.dgvStockpile.AllowDrop = true;
            this.dgvStockpile.AllowUserToAddRows = false;
            this.dgvStockpile.AllowUserToDeleteRows = false;
            this.dgvStockpile.AllowUserToResizeRows = false;
            this.dgvStockpile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockpile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockpile.BackgroundColor = System.Drawing.Color.Gray;
            this.dgvStockpile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockpile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStockpile.ColumnHeadersHeight = 21;
            this.dgvStockpile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStockpile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Item, this.GameName, this.Note });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockpile.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStockpile.GridColor = System.Drawing.Color.Black;
            this.dgvStockpile.Location = new System.Drawing.Point(3, 38);
            this.dgvStockpile.Name = "dgvStockpile";
            this.dgvStockpile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockpile.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStockpile.RowHeadersVisible = false;
            this.dgvStockpile.RowTemplate.Height = 25;
            this.dgvStockpile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockpile.Size = new System.Drawing.Size(446, 272);
            this.dgvStockpile.TabIndex = 178;
            this.dgvStockpile.Tag = "color:dark1";
            this.dgvStockpile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HandleCellClick);
            this.dgvStockpile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleStockpileMouseDown);
            // 
            // Item
            // 
            this.Item.FillWeight = 145F;
            this.Item.HeaderText = "Item Name";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // GameName
            // 
            this.GameName.FillWeight = 76.73162F;
            this.GameName.HeaderText = "Game";
            this.GameName.Name = "GameName";
            this.GameName.ReadOnly = true;
            // 
            // Note
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.Note.DefaultCellStyle = dataGridViewCellStyle2;
            this.Note.FillWeight = 23.01949F;
            this.Note.HeaderText = "Note";
            this.Note.MinimumWidth = 35;
            this.Note.Name = "Note";
            this.Note.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Note.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Note.Text = "";
            // 
            // btnRenameSelected
            // 
            this.btnRenameSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRenameSelected.BackColor = System.Drawing.Color.Gray;
            this.btnRenameSelected.FlatAppearance.BorderSize = 0;
            this.btnRenameSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenameSelected.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRenameSelected.ForeColor = System.Drawing.Color.White;
            this.btnRenameSelected.Image = ((System.Drawing.Image)(resources.GetObject("btnRenameSelected.Image")));
            this.btnRenameSelected.Location = new System.Drawing.Point(88, 313);
            this.btnRenameSelected.Name = "btnRenameSelected";
            this.btnRenameSelected.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnRenameSelected.Size = new System.Drawing.Size(113, 32);
            this.btnRenameSelected.TabIndex = 183;
            this.btnRenameSelected.TabStop = false;
            this.btnRenameSelected.Tag = "color:light1";
            this.btnRenameSelected.Text = "  Rename Item";
            this.btnRenameSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRenameSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRenameSelected.UseVisualStyleBackColor = false;
            this.btnRenameSelected.Click += new System.EventHandler(this.RenamedSelected);
            // 
            // btnImportStockpile
            // 
            this.btnImportStockpile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnImportStockpile.FlatAppearance.BorderSize = 0;
            this.btnImportStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnImportStockpile.ForeColor = System.Drawing.Color.White;
            this.btnImportStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnImportStockpile.Image")));
            this.btnImportStockpile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportStockpile.Location = new System.Drawing.Point(3, 313);
            this.btnImportStockpile.Name = "btnImportStockpile";
            this.btnImportStockpile.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnImportStockpile.Size = new System.Drawing.Size(82, 32);
            this.btnImportStockpile.TabIndex = 182;
            this.btnImportStockpile.TabStop = false;
            this.btnImportStockpile.Tag = "color:light1";
            this.btnImportStockpile.Text = "  Import";
            this.btnImportStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportStockpile.UseVisualStyleBackColor = false;
            this.btnImportStockpile.Click += new System.EventHandler(this.ImportStockpile);
            // 
            // btnStockpileMoveSelectedDown
            // 
            this.btnStockpileMoveSelectedDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockpileMoveSelectedDown.BackColor = System.Drawing.Color.Gray;
            this.btnStockpileMoveSelectedDown.FlatAppearance.BorderSize = 0;
            this.btnStockpileMoveSelectedDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockpileMoveSelectedDown.Font = new System.Drawing.Font("Segoe UI Symbol", 7F);
            this.btnStockpileMoveSelectedDown.ForeColor = System.Drawing.Color.White;
            this.btnStockpileMoveSelectedDown.Location = new System.Drawing.Point(417, 313);
            this.btnStockpileMoveSelectedDown.Name = "btnStockpileMoveSelectedDown";
            this.btnStockpileMoveSelectedDown.Size = new System.Drawing.Size(32, 32);
            this.btnStockpileMoveSelectedDown.TabIndex = 181;
            this.btnStockpileMoveSelectedDown.TabStop = false;
            this.btnStockpileMoveSelectedDown.Tag = "color:light1";
            this.btnStockpileMoveSelectedDown.Text = "▼▼";
            this.btnStockpileMoveSelectedDown.UseVisualStyleBackColor = false;
            this.btnStockpileMoveSelectedDown.Click += new System.EventHandler(this.MoveSelectedStockpileDown);
            // 
            // btnStockpileMoveSelectedUp
            // 
            this.btnStockpileMoveSelectedUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockpileMoveSelectedUp.BackColor = System.Drawing.Color.Gray;
            this.btnStockpileMoveSelectedUp.FlatAppearance.BorderSize = 0;
            this.btnStockpileMoveSelectedUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockpileMoveSelectedUp.Font = new System.Drawing.Font("Segoe UI Symbol", 7F);
            this.btnStockpileMoveSelectedUp.ForeColor = System.Drawing.Color.White;
            this.btnStockpileMoveSelectedUp.Location = new System.Drawing.Point(382, 313);
            this.btnStockpileMoveSelectedUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnStockpileMoveSelectedUp.Name = "btnStockpileMoveSelectedUp";
            this.btnStockpileMoveSelectedUp.Size = new System.Drawing.Size(32, 32);
            this.btnStockpileMoveSelectedUp.TabIndex = 180;
            this.btnStockpileMoveSelectedUp.TabStop = false;
            this.btnStockpileMoveSelectedUp.Tag = "color:light1";
            this.btnStockpileMoveSelectedUp.Text = "▲▲";
            this.btnStockpileMoveSelectedUp.UseVisualStyleBackColor = false;
            this.btnStockpileMoveSelectedUp.Click += new System.EventHandler(this.MoveSelectedStockpileUp);
            // 
            // btnRemoveSelectedStockpile
            // 
            this.btnRemoveSelectedStockpile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveSelectedStockpile.BackColor = System.Drawing.Color.Gray;
            this.btnRemoveSelectedStockpile.FlatAppearance.BorderSize = 0;
            this.btnRemoveSelectedStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelectedStockpile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRemoveSelectedStockpile.ForeColor = System.Drawing.Color.White;
            this.btnRemoveSelectedStockpile.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveSelectedStockpile.Image")));
            this.btnRemoveSelectedStockpile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveSelectedStockpile.Location = new System.Drawing.Point(204, 313);
            this.btnRemoveSelectedStockpile.Name = "btnRemoveSelectedStockpile";
            this.btnRemoveSelectedStockpile.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnRemoveSelectedStockpile.Size = new System.Drawing.Size(116, 32);
            this.btnRemoveSelectedStockpile.TabIndex = 179;
            this.btnRemoveSelectedStockpile.TabStop = false;
            this.btnRemoveSelectedStockpile.Tag = "color:light1";
            this.btnRemoveSelectedStockpile.Text = "  Remove Item";
            this.btnRemoveSelectedStockpile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveSelectedStockpile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveSelectedStockpile.UseVisualStyleBackColor = false;
            this.btnRemoveSelectedStockpile.Click += new System.EventHandler(this.RemoveSelectedStockpile);
            // 
            // btnGlitchHarvesterSettings
            // 
            this.btnGlitchHarvesterSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGlitchHarvesterSettings.BackColor = System.Drawing.Color.Gray;
            this.btnGlitchHarvesterSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGlitchHarvesterSettings.FlatAppearance.BorderSize = 0;
            this.btnGlitchHarvesterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGlitchHarvesterSettings.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGlitchHarvesterSettings.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnGlitchHarvesterSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnGlitchHarvesterSettings.Image")));
            this.btnGlitchHarvesterSettings.Location = new System.Drawing.Point(347, 3);
            this.btnGlitchHarvesterSettings.Name = "btnGlitchHarvesterSettings";
            this.btnGlitchHarvesterSettings.Size = new System.Drawing.Size(32, 32);
            this.btnGlitchHarvesterSettings.TabIndex = 184;
            this.btnGlitchHarvesterSettings.TabStop = false;
            this.btnGlitchHarvesterSettings.Tag = "color:light1";
            this.btnGlitchHarvesterSettings.UseVisualStyleBackColor = false;
            this.btnGlitchHarvesterSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleGlitchHarvesterSettingsMouseDown);
            // 
            // JavaStockpileManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(452, 348);
            this.Controls.Add(this.btnGlitchHarvesterSettings);
            this.Controls.Add(this.btnRenameSelected);
            this.Controls.Add(this.btnImportStockpile);
            this.Controls.Add(this.btnStockpileMoveSelectedDown);
            this.Controls.Add(this.btnStockpileMoveSelectedUp);
            this.Controls.Add(this.btnRemoveSelectedStockpile);
            this.Controls.Add(this.dgvStockpile);
            this.Controls.Add(this.btnLoadStockpile);
            this.Controls.Add(this.btnSaveStockpile);
            this.Controls.Add(this.btnSaveStockpileAs);
            this.Controls.Add(this.btnClearStockpile);
            this.Controls.Add(this.btnStockpileDOWN);
            this.Controls.Add(this.btnStockpileUP);
            this.MinimumSize = new System.Drawing.Size(464, 152);
            this.Name = "JavaStockpileManagerForm";
            this.Tag = "color:dark1";
            this.Text = "Stockpile Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).EndInit();
            this.ResumeLayout(false);
        }


        #endregion

        public System.Windows.Forms.Button btnGlitchHarvesterSettings;
        private System.Windows.Forms.Button btnLoadStockpile;
        public System.Windows.Forms.Button btnSaveStockpile;
        private System.Windows.Forms.Button btnSaveStockpileAs;
        private System.Windows.Forms.Button btnClearStockpile;
        public System.Windows.Forms.Button btnStockpileDOWN;
        public System.Windows.Forms.Button btnStockpileUP;
        public RTCV.UI.Components.DataGridViewDraggable dgvStockpile;
        private DataGridViewTextBoxColumn Item;
        private DataGridViewTextBoxColumn GameName;
        private DataGridViewButtonColumn Note;
        public System.Windows.Forms.Button btnRenameSelected;
        private System.Windows.Forms.Button btnImportStockpile;
        private System.Windows.Forms.Button btnStockpileMoveSelectedDown;
        private System.Windows.Forms.Button btnStockpileMoveSelectedUp;
        public System.Windows.Forms.Button btnRemoveSelectedStockpile;
    }
}