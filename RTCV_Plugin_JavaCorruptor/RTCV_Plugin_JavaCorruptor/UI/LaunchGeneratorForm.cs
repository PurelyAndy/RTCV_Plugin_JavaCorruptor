using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Java_Corruptor.UI.Components;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI
{
    public partial class LaunchGeneratorForm : ColorizedForm
    {
        public LaunchGeneratorForm()
        {
            InitializeComponent();
        }

        private void LaunchGeneratorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            e.Cancel = true;
            Hide();
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            CorruptionOptions.LaunchScript.Stages.Add(new());
            FlowLayoutPanel pn = new();
            TextBox tbProgram = new();
            TextBox tbArgs = new();
            CheckBox cbShowOutput = new();
            Button btnRemove = new();

            pn.Width = flpPrograms.Width - 30;
            pn.Height = 30;
            pn.FlowDirection = FlowDirection.LeftToRight;
            btnRemove.Text = "X";
            btnRemove.ForeColor = Color.OrangeRed;
            btnRemove.Font = new Font("Consolas", 10);
            btnRemove.Width = 30;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 0;
            cbShowOutput.Text = "Show Output";
            cbShowOutput.ForeColor = Color.White;
            cbShowOutput.Font = new("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point);
            tbProgram.Width = pn.Width / 3;
            tbProgram.BackColor = Color.FromArgb(96, 96, 96);
            tbProgram.ForeColor = Color.White;
            tbProgram.Tag = "color:normal";
            tbArgs.Width = pn.Width - tbProgram.Width - btnRemove.Width - cbShowOutput.Width - 40;
            tbArgs.BackColor = Color.FromArgb(96, 96, 96);
            tbArgs.ForeColor = Color.White;
            tbArgs.Tag = "color:normal";

            btnRemove.Click += (_, _) => {
                CorruptionOptions.LaunchScript.Stages.RemoveAt(flpPrograms.Controls.IndexOf(pn));
                flpPrograms.Controls.Remove(pn);
            };
            tbProgram.TextChanged += (_, _) => {
                int index = flpPrograms.Controls.IndexOf(pn);
                CorruptionOptions.LaunchScript.Stages[index].Program = tbProgram.Text;
            };
            tbArgs.TextChanged += (_, _) => {
                int index = flpPrograms.Controls.IndexOf(pn);
                CorruptionOptions.LaunchScript.Stages[index].Arguments = tbArgs.Text;
            };
            cbShowOutput.CheckedChanged += (_, _) =>
            {
                int index = flpPrograms.Controls.IndexOf(pn);
                CorruptionOptions.LaunchScript.Stages[index].ShowOutput = cbShowOutput.Checked;
            };

            pn.Controls.Add(tbProgram);
            pn.Controls.Add(tbArgs);
            pn.Controls.Add(cbShowOutput);
            pn.Controls.Add(btnRemove);

            flpPrograms.Controls.Add(pn);
            Recolor();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string script = JsonHelper.Serialize(CorruptionOptions.LaunchScript);
            SaveFileDialog dialog = new()
            {
                Title = "Save Java Launch Script",
                DefaultExt = "jls",
                Filter = "Java Launch Script|*.jls"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using var stream = dialog.OpenFile();
                using var writer = new StreamWriter(stream);
                writer.Write(script);
            }
            S.GET<JavaGeneralParametersForm>().tbProgram.Text = dialog.FileName;
        }

        internal void LoadScript(string path)
        {
            string text = File.ReadAllText(path);
            CorruptionOptions.LaunchScript = JsonHelper.Deserialize<LaunchScript>(text);
            flpPrograms.Controls.Clear();
            foreach (LaunchScript.ScriptStage stage in CorruptionOptions.LaunchScript.Stages)
            {
                FlowLayoutPanel pn = new();
                TextBox tbProgram = new();
                TextBox tbArgs = new();
                CheckBox cbShowOutput = new();
                Button btnRemove = new();

                pn.Width = flpPrograms.Width - 30;
                pn.Height = 30;
                pn.FlowDirection = FlowDirection.LeftToRight;
                btnRemove.Text = "X";
                btnRemove.ForeColor = Color.OrangeRed;
                btnRemove.Font = new Font("Consolas", 10);
                btnRemove.Width = 30;
                btnRemove.FlatStyle = FlatStyle.Flat;
                btnRemove.FlatAppearance.BorderSize = 0;
                cbShowOutput.Text = "Show Output";
                cbShowOutput.ForeColor = Color.White;
                cbShowOutput.Font = new("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point);
                cbShowOutput.Checked = stage.ShowOutput;
                tbProgram.Width = pn.Width / 3;
                tbProgram.BackColor = Color.FromArgb(96, 96, 96);
                tbProgram.ForeColor = Color.White;
                tbProgram.Tag = "color:normal";
                tbProgram.Text = stage.Program;
                tbArgs.Width = pn.Width - tbProgram.Width - btnRemove.Width - cbShowOutput.Width - 40;
                tbArgs.BackColor = Color.FromArgb(96, 96, 96);
                tbArgs.ForeColor = Color.White;
                tbArgs.Tag = "color:normal";
                tbArgs.Text = stage.Arguments;

                btnRemove.Click += (_, _) => {
                    CorruptionOptions.LaunchScript.Stages.RemoveAt(flpPrograms.Controls.IndexOf(pn));
                    flpPrograms.Controls.Remove(pn);
                };
                tbProgram.TextChanged += (_, _) => {
                    int index = flpPrograms.Controls.IndexOf(pn);
                    CorruptionOptions.LaunchScript.Stages[index].Program = tbProgram.Text;
                };
                tbArgs.TextChanged += (_, _) => {
                    int index = flpPrograms.Controls.IndexOf(pn);
                    CorruptionOptions.LaunchScript.Stages[index].Arguments = tbArgs.Text;
                };
                cbShowOutput.CheckedChanged += (_, _) =>
                {
                    int index = flpPrograms.Controls.IndexOf(pn);
                    CorruptionOptions.LaunchScript.Stages[index].ShowOutput = cbShowOutput.Checked;
                };

                pn.Controls.Add(tbProgram);
                pn.Controls.Add(tbArgs);
                pn.Controls.Add(cbShowOutput);
                pn.Controls.Add(btnRemove);

                flpPrograms.Controls.Add(pn);
            }
            Recolor();
        }
    }
}
