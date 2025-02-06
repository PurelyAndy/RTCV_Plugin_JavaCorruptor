using System;
using System.Windows.Forms;

namespace Java_Corruptor.UI
{
    partial class LaunchGeneratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchGeneratorForm));
            this.flpPrograms = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddProgram = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flpPrograms
            // 
            this.flpPrograms.AutoScroll = true;
            this.flpPrograms.Location = new System.Drawing.Point(6, 33);
            this.flpPrograms.Name = "flpPrograms";
            this.flpPrograms.Size = new System.Drawing.Size(479, 218);
            this.flpPrograms.TabIndex = 0;
            // 
            // btnAddProgram
            // 
            this.btnAddProgram.BackColor = System.Drawing.Color.Gray;
            this.btnAddProgram.FlatAppearance.BorderSize = 0;
            this.btnAddProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProgram.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnAddProgram.ForeColor = System.Drawing.Color.White;
            this.btnAddProgram.Location = new System.Drawing.Point(6, 6);
            this.btnAddProgram.Name = "btnAddProgram";
            this.btnAddProgram.Size = new System.Drawing.Size(108, 21);
            this.btnAddProgram.TabIndex = 185;
            this.btnAddProgram.TabStop = false;
            this.btnAddProgram.Tag = "color:light1";
            this.btnAddProgram.Text = "Add New Program";
            this.btnAddProgram.UseVisualStyleBackColor = false;
            this.btnAddProgram.Click += new System.EventHandler(this.btnAddProgram_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(120, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 21);
            this.btnSave.TabIndex = 186;
            this.btnSave.TabStop = false;
            this.btnSave.Tag = "color:light1";
            this.btnSave.Text = "Save Launch Script";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LaunchGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(491, 257);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddProgram);
            this.Controls.Add(this.flpPrograms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LaunchGeneratorForm";
            this.Tag = "color:dark1";
            this.Text = "LaunchGeneratorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LaunchGeneratorForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flpPrograms;
        public Button btnAddProgram;
        public Button btnSave;
    }
}