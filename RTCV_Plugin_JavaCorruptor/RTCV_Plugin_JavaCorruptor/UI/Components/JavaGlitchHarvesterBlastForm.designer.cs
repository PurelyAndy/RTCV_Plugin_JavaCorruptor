namespace Java_Corruptor.UI.Components;

partial class JavaGlitchHarvesterBlastForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaGlitchHarvesterBlastForm));
        this.btnGlitchHarvesterSettings = new System.Windows.Forms.Button();
        this.btnRerollSelected = new System.Windows.Forms.Button();
        this.btnCorrupt = new System.Windows.Forms.Button();
        this.btnBlastToggle = new System.Windows.Forms.Button();
        this.btnSendRaw = new System.Windows.Forms.Button();
        this.btnNewBlastLayerEditor = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // btnGlitchHarvesterSettings
        // 
        this.btnGlitchHarvesterSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnGlitchHarvesterSettings.BackColor = System.Drawing.Color.Gray;
        this.btnGlitchHarvesterSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
        this.btnGlitchHarvesterSettings.FlatAppearance.BorderSize = 0;
        this.btnGlitchHarvesterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnGlitchHarvesterSettings.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.btnGlitchHarvesterSettings.ForeColor = System.Drawing.Color.OrangeRed;
        this.btnGlitchHarvesterSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnGlitchHarvesterSettings.Image")));
        this.btnGlitchHarvesterSettings.Location = new System.Drawing.Point(245, 1);
        this.btnGlitchHarvesterSettings.Margin = new System.Windows.Forms.Padding(0);
        this.btnGlitchHarvesterSettings.Name = "btnGlitchHarvesterSettings";
        this.btnGlitchHarvesterSettings.Size = new System.Drawing.Size(26, 26);
        this.btnGlitchHarvesterSettings.TabIndex = 140;
        this.btnGlitchHarvesterSettings.TabStop = false;
        this.btnGlitchHarvesterSettings.Tag = "color:light1";
        this.btnGlitchHarvesterSettings.UseVisualStyleBackColor = false;
        this.btnGlitchHarvesterSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGlitchHarvesterSettings);
        // 
        // btnRerollSelected
        // 
        this.btnRerollSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnRerollSelected.BackColor = System.Drawing.Color.Gray;
        this.btnRerollSelected.FlatAppearance.BorderSize = 0;
        this.btnRerollSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnRerollSelected.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.btnRerollSelected.ForeColor = System.Drawing.Color.White;
        this.btnRerollSelected.Location = new System.Drawing.Point(180, 29);
        this.btnRerollSelected.Margin = new System.Windows.Forms.Padding(1);
        this.btnRerollSelected.Name = "btnRerollSelected";
        this.btnRerollSelected.Size = new System.Drawing.Size(91, 26);
        this.btnRerollSelected.TabIndex = 133;
        this.btnRerollSelected.TabStop = false;
        this.btnRerollSelected.Tag = "color:light1";
        this.btnRerollSelected.Text = "Reroll Selected";
        this.btnRerollSelected.UseVisualStyleBackColor = false;
        this.btnRerollSelected.Click += new System.EventHandler(this.RerollSelected);
        // 
        // btnCorrupt
        // 
        this.btnCorrupt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
        this.btnCorrupt.FlatAppearance.BorderColor = System.Drawing.Color.Black;
        this.btnCorrupt.FlatAppearance.BorderSize = 0;
        this.btnCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCorrupt.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnCorrupt.ForeColor = System.Drawing.Color.OrangeRed;
        this.btnCorrupt.Image = ((System.Drawing.Image)(resources.GetObject("btnCorrupt.Image")));
        this.btnCorrupt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnCorrupt.Location = new System.Drawing.Point(1, 1);
        this.btnCorrupt.Margin = new System.Windows.Forms.Padding(1);
        this.btnCorrupt.Name = "btnCorrupt";
        this.btnCorrupt.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnCorrupt.Size = new System.Drawing.Size(107, 26);
        this.btnCorrupt.TabIndex = 137;
        this.btnCorrupt.TabStop = false;
        this.btnCorrupt.Tag = "color:dark2";
        this.btnCorrupt.Text = "   Corrupt";
        this.btnCorrupt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.btnCorrupt.UseVisualStyleBackColor = false;
        this.btnCorrupt.Click += new System.EventHandler(this.Corrupt);
        this.btnCorrupt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCorrupt_MouseDown);
        // 
        // btnBlastToggle
        // 
        this.btnBlastToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnBlastToggle.BackColor = System.Drawing.Color.Gray;
        this.btnBlastToggle.FlatAppearance.BorderSize = 0;
        this.btnBlastToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnBlastToggle.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.btnBlastToggle.ForeColor = System.Drawing.Color.White;
        this.btnBlastToggle.Location = new System.Drawing.Point(110, 1);
        this.btnBlastToggle.Margin = new System.Windows.Forms.Padding(0);
        this.btnBlastToggle.Name = "btnBlastToggle";
        this.btnBlastToggle.Size = new System.Drawing.Size(133, 26);
        this.btnBlastToggle.TabIndex = 131;
        this.btnBlastToggle.TabStop = false;
        this.btnBlastToggle.Tag = "color:light1";
        this.btnBlastToggle.Text = "BlastLayer : OFF";
        this.btnBlastToggle.UseVisualStyleBackColor = false;
        this.btnBlastToggle.Click += new System.EventHandler(this.BlastLayerToggle);
        // 
        // btnSendRaw
        // 
        this.btnSendRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSendRaw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
        this.btnSendRaw.FlatAppearance.BorderColor = System.Drawing.Color.Black;
        this.btnSendRaw.FlatAppearance.BorderSize = 0;
        this.btnSendRaw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSendRaw.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnSendRaw.ForeColor = System.Drawing.Color.OrangeRed;
        this.btnSendRaw.Image = ((System.Drawing.Image)(resources.GetObject("btnSendRaw.Image")));
        this.btnSendRaw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnSendRaw.Location = new System.Drawing.Point(1, 29);
        this.btnSendRaw.Margin = new System.Windows.Forms.Padding(1);
        this.btnSendRaw.Name = "btnSendRaw";
        this.btnSendRaw.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnSendRaw.Size = new System.Drawing.Size(107, 26);
        this.btnSendRaw.TabIndex = 139;
        this.btnSendRaw.TabStop = false;
        this.btnSendRaw.Tag = "color:dark2";
        this.btnSendRaw.Text = " Raw to Stash";
        this.btnSendRaw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.btnSendRaw.UseVisualStyleBackColor = false;
        this.btnSendRaw.Click += new System.EventHandler(this.SendRawToStash);
        // 
        // btnNewBlastLayerEditor
        // 
        this.btnNewBlastLayerEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.btnNewBlastLayerEditor.BackColor = System.Drawing.Color.Gray;
        this.btnNewBlastLayerEditor.FlatAppearance.BorderSize = 0;
        this.btnNewBlastLayerEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNewBlastLayerEditor.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.btnNewBlastLayerEditor.ForeColor = System.Drawing.Color.White;
        this.btnNewBlastLayerEditor.Location = new System.Drawing.Point(110, 29);
        this.btnNewBlastLayerEditor.Margin = new System.Windows.Forms.Padding(1);
        this.btnNewBlastLayerEditor.Name = "btnNewBlastLayerEditor";
        this.btnNewBlastLayerEditor.Size = new System.Drawing.Size(68, 26);
        this.btnNewBlastLayerEditor.TabIndex = 141;
        this.btnNewBlastLayerEditor.TabStop = false;
        this.btnNewBlastLayerEditor.Tag = "color:light1";
        this.btnNewBlastLayerEditor.Text = "New Layer";
        this.btnNewBlastLayerEditor.UseVisualStyleBackColor = false;
        this.btnNewBlastLayerEditor.Click += new System.EventHandler(this.btnNewBlastLayerEditor_Click);
        // 
        // JavaGlitchHarvesterBlastForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.ClientSize = new System.Drawing.Size(272, 56);
        this.Controls.Add(this.btnNewBlastLayerEditor);
        this.Controls.Add(this.btnBlastToggle);
        this.Controls.Add(this.btnGlitchHarvesterSettings);
        this.Controls.Add(this.btnRerollSelected);
        this.Controls.Add(this.btnCorrupt);
        this.Controls.Add(this.btnSendRaw);
        this.DoubleBuffered = true;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "JavaGlitchHarvesterBlastForm";
        this.Tag = "color:dark1";
        this.Text = "Blast Tools";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
        this.ResumeLayout(false);
    }

    #endregion

    public System.Windows.Forms.Button btnNewBlastLayerEditor;
    public System.Windows.Forms.Button btnCorrupt;
    public System.Windows.Forms.Button btnSendRaw;
    public System.Windows.Forms.Button btnRerollSelected;
    public System.Windows.Forms.Button btnBlastToggle;
    public System.Windows.Forms.Button btnGlitchHarvesterSettings;
}