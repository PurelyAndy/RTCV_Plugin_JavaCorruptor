using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RTCV.NetCore;
using RTCV.UI.Modular;
using RTCV.UI;
using System.Reflection;

namespace Java_Corruptor.UI.Components;

public partial class JavaGeneralParametersForm : ComponentForm, IBlockable
{
    private new void HandleMouseDown(object s, MouseEventArgs e) => typeof(ComponentForm).GetMethod("HandleMouseDown", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(this, new[] { s, e });
    private new void HandleFormClosing(object s, FormClosingEventArgs e) => typeof(ComponentForm).GetMethod("HandleFormClosing", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(this, new[] { s, e });
    public double Intensity
    {
        get => tbIntensity.Value / 100000d;
        private set => tbIntensity.Value = (int)(value * 100000);
    }

    private int _seed;
    internal static Random Random;
        
    public void ResetRandom()
    {
        if (!cbUseLastSeed.Checked)
            _seed = Environment.TickCount;
        Random = new(_seed);
        cbUseLastSeed.Text = $"Use last seed ({_seed})";
    }

    public JavaGeneralParametersForm()
    {
        InitializeComponent();
        ResetRandom();
    }

    private void tbIntensity_Scroll(object sender, EventArgs e)
    {
        lbIntensity.Text = $"Intensity: {tbIntensity.Value / 1000d}%";
    }

    private void btnGeneralParamsInfo_Click(object sender, EventArgs e)
    {
        MessageBox.Show("""
            The intensity slider determines the chance of an instruction being corrupted. At 0%, no corruptions will be applied. At 100%, every instruction that matches the criteria of the corruption engine will be corrupted.
            The "Use last seed" checkbox will use the same seed for the random number generator every time the program is run. This is useful for debugging, but pretty much useless for normal use.
            The "Launch a program after corrupting" checkbox will run a program after the corruption process is complete. You could write a batch script to overwrite the old jar file and then automatically launch your game, for example. The large box below will show the output of the program.
        """, "General Parameters Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnSelectProgram_Click(object sender, EventArgs e)
    {
        if (ofdProgram.ShowDialog() != DialogResult.OK)
            return;
        tbProgram.Text = ofdProgram.FileName;
    }

    private string _oldProgramPath = "";
    private void tbProgram_TextChanged(object sender, EventArgs e)
    {
        if (File.Exists(tbProgram.Text))
        {
            _oldProgramPath = tbProgram.Text;
            return;
        }
        MessageBox.Show($"The file \"{tbProgram.Text}\" doesn't exist!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        tbProgram.Text = _oldProgramPath;
    }

    public void RunPostCorruptAction()
    {
        if (!cbPostCorruptAction.Checked || tbProgram.Text == string.Empty)
            return;
        Process p = new()
        {
            StartInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = $"/c {tbProgram.Text}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = Path.GetDirectoryName(tbProgram.Text)!,
            },
        };
        p.OutputDataReceived += (_, args) =>
        {
            SyncObjectSingleton.FormExecute(form =>
            {
                form.tbOutput.SelectionStart = form.tbOutput.TextLength;
                form.tbOutput.SelectionLength = 0;

                form.tbOutput.SelectionColor = Color.White;
                form.tbOutput.AppendText(args.Data + Environment.NewLine);
                form.tbOutput.SelectionColor = form.tbOutput.ForeColor;
            }, this);
        };
        p.ErrorDataReceived += (_, args) =>
        {
            SyncObjectSingleton.FormExecute(form =>
            {
                form.tbOutput.SelectionStart = form.tbOutput.TextLength;
                form.tbOutput.SelectionLength = 0;

                form.tbOutput.SelectionColor = Color.FromArgb(0xff, 0x40, 0x40);
                form.tbOutput.AppendText(args.Data + Environment.NewLine);
                form.tbOutput.SelectionColor = form.tbOutput.ForeColor;
            }, this);
        };
        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();
    }

    private void cbPostCorruptAction_CheckedChanged(object sender, EventArgs e)
    {
        if (cbPostCorruptAction.Checked && tbProgram.Text == string.Empty)
            btnSelectProgram_Click(sender, e);
    }
}