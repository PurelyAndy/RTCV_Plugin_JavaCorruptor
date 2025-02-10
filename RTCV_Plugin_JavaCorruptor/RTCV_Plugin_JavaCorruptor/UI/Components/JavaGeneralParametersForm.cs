using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RTCV.NetCore;
using RTCV.UI.Modular;
using RTCV.UI;
using System.Threading;
using System.Threading.Tasks;
using RTCV.Common;
using RTCV.CorruptCore;
using System.Linq;

namespace Java_Corruptor.UI.Components;

public partial class JavaGeneralParametersForm : ComponentForm, IBlockable
{
    private void HandleMouseDown(object s, MouseEventArgs e) => this.HandleMouseDownP(s, e);
    private void HandleFormClosing(object s, FormClosingEventArgs e) => this.HandleFormClosingP(s, e);
    public double Intensity
    {
        get => (tbIntensity.Value / 100000d) * (tbIntensity.Value / 100000d);
        private set => tbIntensity.Value = (int)(Math.Sqrt(value) * 100000);
    }

    private static int _seed;
    private static ThreadLocal<Random> _random = new(() => new(_seed));
    internal static Random Random => _random.Value;

    public void ResetRandom()
    {
        if (!cbUseLastSeed.Checked)
            _seed = Environment.TickCount;
        _random = new(() => new(_seed));
        cbUseLastSeed.Text = $"Use last seed ({_seed})";
        cbUseLastSeed.Refresh();
    }

    public JavaGeneralParametersForm()
    {
        InitializeComponent();
        //DEPLORABLE hack to set negative padding. this could be used as an argument for the death sentence.
        try
        {
            Padding pad = new(-5, -5, -5, -5);
            Type iArrangedElement = Type.GetType("System.Windows.Forms.Layout.IArrangedElement, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Type propertyStore = Type.GetType("System.Windows.Forms.PropertyStore, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Type commonProperties = Type.GetType("System.Windows.Forms.Layout.CommonProperties, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            PropertyInfo properties = iArrangedElement.GetProperties()[3];
            MethodInfo setPadding = propertyStore.GetMethod("SetPadding");
            FieldInfo paddingProperty = commonProperties.GetField("_paddingProperty", BindingFlags.NonPublic | BindingFlags.Static);
            int paddingPropertyVal = (int)paddingProperty.GetValue(null);
            object o = properties.GetValue(btnSelectProgram);
            setPadding.Invoke(o, [paddingPropertyVal, pad]);
        }
        catch
        {
            //cry
            btnSelectProgram.Padding = Padding.Empty;
        }
        ResetRandom();
        tbOutput.AutoWordSelection = true;
        tbOutput.AutoWordSelection = false;
    }

    public void UpdateLaunchScript(string path)
    {
        tbProgram.Text = path;
        S.GET<LaunchGeneratorForm>().LoadScript(path);
        cbPostCorruptAction.Checked = true;
    }

    private void tbIntensity_Scroll(object sender, EventArgs e)
    {
        lbIntensity.Text = $"Intensity: {Intensity * 100:F3}%";
    }

    private void btnGeneralParamsInfo_Click(object sender, EventArgs e)
    {
        Process.Start("https://corrupt.wiki/systems/java/java-corruptor-plugin#general-parameters");
    }

    private void btnSelectProgram_Click(object sender, EventArgs e)
    {
        if (ofdProgram.ShowDialog() != DialogResult.OK)
            return;
        tbProgram.Text = ofdProgram.FileName;
        if (!string.IsNullOrEmpty(tbProgram.Text))
        {
            S.GET<LaunchGeneratorForm>().LoadScript(tbProgram.Text);
        }
    }

    private string _oldProgramPath = "";
    private void tbProgram_LostFocus(object sender, EventArgs e)
    {
        if (File.Exists(tbProgram.Text))
        {
            _oldProgramPath = tbProgram.Text;
            S.GET<LaunchGeneratorForm>().LoadScript(tbProgram.Text);
            return;
        }
        MessageBox.Show($"The file \"{tbProgram.Text}\" doesn't exist!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        tbProgram.Text = _oldProgramPath;
    }

    public void RunPostCorruptAction(LaunchScript launchScript)
    {
        if (launchScript == null && !cbPostCorruptAction.Checked || CorruptionOptions.LaunchScript == null)
            return;
        launchScript ??= CorruptionOptions.LaunchScript;
        Task.Run(launchScript.Execute);
    }

    private void cbPostCorruptAction_CheckedChanged(object sender, EventArgs e)
    {
        if (cbPostCorruptAction.Checked && tbProgram.Text == string.Empty)
            btnSelectProgram_Click(sender, e);
    }
    
    private void btnOpenDisassembler_Click(object sender, EventArgs e)
    {
        DisassemblerForm disassemblerForm = S.GET<DisassemblerForm>();
        disassemblerForm.Show();
        disassemblerForm.BringToFront();
    }
    
    private void btnMoreSettings_Click(object sender, EventArgs e)
    {
        if (S.GET<MoreSettingsForm>().IsDisposed)
            S.SET(new MoreSettingsForm());
        S.GET<MoreSettingsForm>().Show();
        S.GET<MoreSettingsForm>().BringToFront();
    }

    private void btnCreateScript_Click(object sender, EventArgs e)
    {
        if (S.GET<LaunchGeneratorForm>().IsDisposed)
            S.SET(new LaunchGeneratorForm());
        S.GET<LaunchGeneratorForm>().Show();
        S.GET<LaunchGeneratorForm>().BringToFront();
    }

    private void tbProgram_TextChanged(object sender, EventArgs e)
    {
    }
}