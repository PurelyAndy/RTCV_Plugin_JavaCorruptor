using Java_Corruptor.UI.Engines;

namespace Java_Corruptor.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NLog;
    using RTCV.NetCore;
    using RTCV.Common;
    using RTCV.UI;
    using static RTCV.CorruptCore.RtcCore;
    using RTCV.Vanguard;
    using System.IO;
    using System.Text.RegularExpressions;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System.Diagnostics;
    using SlimDX.XACT3;
    using SlimDX.Direct3D9;

    public partial class PluginForm : Form
    {
        private UI.Engines.Engine _engine;
        private ComboBox _comboBox;

        private CommonOpenFileDialog cofdOutputFolder = new CommonOpenFileDialog()
        {
            IsFolderPicker = true
        };
        public Java_Corruptor plugin;

        public volatile bool HideOnClose = true;

        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public PluginForm(Java_Corruptor _plugin)
        {
            plugin = _plugin;

            InitializeComponent();
            FormClosing += new FormClosingEventHandler(PluginForm_FormClosing);

            _engine = vectorEngine1;
            _comboBox = vectorEngine1.placeholderComboBox;
            _comboBox.SelectedIndex = 0;
            vectorEngine1.Visible = true;

            vectorEngine1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            arithmeticEngine1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            functionEngine1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);

            Text = Java_Corruptor.CamelCase(nameof(Java_Corruptor).Replace("_", " ")) + $" - Version {plugin.Version}"; //automatic window title
        }

        private void PluginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HideOnClose)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnSelectInputJar_Click(object sender, EventArgs e)
        {
            ofdInputJar.ShowDialog();

            if (ofdInputJar.FileName != "")
            {
                tbInputJar.Text = ofdInputJar.FileName;
                Settings1.Default.InputJarPath = ofdInputJar.FileName;
                Settings1.Default.Save();
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            cofdOutputFolder.ShowDialog();

            if (cofdOutputFolder.FileName != "")
            {
                tbOutputFolder.Text = cofdOutputFolder.FileName;
                Settings1.Default.OutputFolderPath = cofdOutputFolder.FileName;
                Settings1.Default.Save();
            }
        }

        private void UpdateEngine(object sender, EventArgs e)
        {
            vectorEngine1.Visible = false;
            arithmeticEngine1.Visible = false;
            functionEngine1.Visible = false;

            ComboBox box = (ComboBox)sender;
            if (box.SelectedIndex == -1)
                return;

            switch (box.SelectedItem.ToString())
            {
                case "Vector Engine":
                    vectorEngine1.Visible = true;
                    _engine = vectorEngine1;
                    break;
                case "Arithmetic Engine":
                    arithmeticEngine1.Visible = true;
                    _engine = arithmeticEngine1;
                    break;
                case "Function Engine":
                    functionEngine1.Visible = true;
                    _engine = functionEngine1;
                    break;
            }


            _comboBox = _engine.placeholderComboBox;
            _comboBox.Text = box.SelectedItem.ToString();
            _engine.BringToFront();
        }

        private void PluginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCorrupt_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pnCorruptionEngine.Controls.Count.ToString());
            string outputFileName = tbInputJar.Text.Split('\\').Last() + "_corrupted_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jar";
            string outputFilePath = Path.Combine(tbOutputFolder.Text, outputFileName);
            string arguments = $"\"{tbInputJar.Text}\" \"{outputFilePath}\" {multiTB_InstructionSeed.Value} {multiTB_ValueSeed.Value} {_engine.placeholderComboBox.SelectedIndex} {(double)S.GET<GeneralParametersForm>().multiTB_Intensity.Value / S.GET<GeneralParametersForm>().multiTB_Intensity.Maximum}";
            switch (_engine.placeholderComboBox.SelectedIndex)
            {
                case 0:
                    arguments += $" {(int)vectorEngine1.cbVectorLimiterList.SelectedItem} {(int)vectorEngine1.cbVectorValueList.SelectedItem}";
                    break;
                case 1:
                    arguments += $" {arithmeticEngine1.tbMaximum.Value / 1000f} {arithmeticEngine1.tbMinimum.Value / 1000f}";
                    foreach (int i in arithmeticEngine1.lbeLimiters.SelectedIndices)
                        arguments += $" {i * 4}";
                    arguments += $" :";
                    foreach (int i in arithmeticEngine1.lbeOperations.SelectedIndices)
                        arguments += $" {i * 4}";
                    arguments += $" : ";
                    arguments +=
                        (arithmeticEngine1.cbInt.Checked ? 1 : 0)
                        | (arithmeticEngine1.cbLong.Checked ? 2 : 0)
                        | (arithmeticEngine1.cbFloat.Checked ? 4 : 0)
                        | (arithmeticEngine1.cbDouble.Checked ? 8 : 0);
                    
                    break;
                case 2:
                    foreach (string s in functionEngine1.lbLimiterFunctions.SelectedItems)
                        arguments += $" {s}";
                    arguments += $" :";
                    foreach (string s in functionEngine1.lbValueFunctions.SelectedItems)
                        arguments += $" {s}";
                    arguments += $" :";
                    break;
            }
            Process process = new Process
            {
                StartInfo =
                {
                    FileName = $"{Directory.GetCurrentDirectory()}\\RTC\\PLUGINS\\JavaCorruptor_packed.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //redirect the output
            process.OutputDataReceived += (s, args) => MessageBox.Show(args.Data);
            process.ErrorDataReceived += (s, args) => MessageBox.Show(args.Data);
            MessageBox.Show(arguments);
            //pipe the output to the console
            MessageBox.Show(Directory.GetCurrentDirectory());
            process.Start();
            process.WaitForExit();
            DialogResult result = MessageBox.Show("Done! Open output folder?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                Process.Start("explorer.exe", tbOutputFolder.Text);
        }
    }
}
