using JAVACORRUPTOR.UI.Components.EngineControls;
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
using RTCV.CorruptCore;
using RTCV.UI.Components.EngineConfig.EngineControls;
using RTCV.Common.CustomExtensions;

namespace JAVACORRUPTOR.UI
{

    public partial class PluginForm : Form
    {
        private JavaEngineControl _engine;
        private ComboBox _comboBox;
        private bool _hasAskedToOpenFolder;

        private CommonOpenFileDialog cofdOutputFolder = new CommonOpenFileDialog()
        {
            IsFolderPicker = true
        };
        public JAVA_CORRUPTOR plugin;

        public volatile bool HideOnClose = true;

        Logger logger = LogManager.GetCurrentClassLogger();

        public PluginForm(JAVA_CORRUPTOR _plugin)
        {
            plugin = _plugin;

            InitializeComponent();
            FormClosing += new FormClosingEventHandler(PluginForm_FormClosing);

            _engine = javaVectorEngineControl1;
            _comboBox = javaVectorEngineControl1.placeholderComboBox;
            _comboBox.SelectedIndex = 0;
            javaVectorEngineControl1.Visible = true;

            javaVectorEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            arithmeticEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            functionEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            javaCustomEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            sanitizerEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            stringEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);
            roundingEngineControl1.placeholderComboBox.SelectedIndexChanged += new EventHandler(UpdateEngine);

            Text = JAVA_CORRUPTOR.CamelCase(nameof(JAVA_CORRUPTOR).Replace("_", " ")) + $" - Version {plugin.Version}"; //automatic window title
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
                tbInputJar.Text = ofdInputJar.FileName;
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            cofdOutputFolder.ShowDialog();

            if (cofdOutputFolder.FileName != "")
                tbOutputFolder.Text = cofdOutputFolder.FileName;
        }

        private void UpdateEngine(object sender, EventArgs e)
        {
            javaVectorEngineControl1.Visible = false;
            arithmeticEngineControl1.Visible = false;
            functionEngineControl1.Visible = false;
            javaCustomEngineControl1.Visible = false;
            sanitizerEngineControl1.Visible = false;
            stringEngineControl1.Visible = false;
            roundingEngineControl1.Visible = false;

            ComboBox box = (ComboBox)sender;
            if (box.SelectedIndex == -1)
                return;

            switch (box.SelectedItem.ToString())
            {
                case "Vector Engine":
                    javaVectorEngineControl1.Visible = true;
                    _engine = javaVectorEngineControl1;
                    break;
                case "Arithmetic Engine":
                    arithmeticEngineControl1.Visible = true;
                    _engine = arithmeticEngineControl1;
                    break;
                case "Function Engine":
                    functionEngineControl1.Visible = true;
                    _engine = functionEngineControl1;
                    break;
                case "Custom Engine":
                    javaCustomEngineControl1.Visible = true;
                    _engine = javaCustomEngineControl1;
                    break;
                case "Sanitizer":
                    sanitizerEngineControl1.Visible = true;
                    _engine = sanitizerEngineControl1;
                    break;
                case "String Engine":
                    stringEngineControl1.Visible = true;
                    _engine = stringEngineControl1;
                    break;
                case "Rounding Engine":
                    roundingEngineControl1.Visible = true;
                    _engine = roundingEngineControl1;
                    break;
            }


            _comboBox = _engine.placeholderComboBox;
            _comboBox.Text = box.SelectedItem.ToString();
            _engine.BringToFront();
        }

        private void PluginForm_Load(object sender, EventArgs e)
        {
            if (File.Exists($@"{Directory.GetCurrentDirectory()}\RTC\PLUGINS\jrechecked"))
                return;
                
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $@"{Directory.GetCurrentDirectory()}\RTC\PLUGINS\JavaCorruptor_packed.exe",
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.OutputDataReceived += (s, args) =>
            {
                if (args.Data == null || !args.Data.Contains("This application requires a Java Runtime Environment."))
                    return;
                MessageBox.Show("Your JAVA_HOME variable is not set, or the Java corruptor isn't recognizing it for some reason. Please select a Java runtime next.");

                string[] x64jres = Directory.GetDirectories("C:\\Program Files\\Java\\");
                string[] x86jres = Directory.GetDirectories("C:\\Program Files (x86)\\Java\\");

                JRESelectorForm jreSelector = new JRESelectorForm();
                jreSelector.lb64Bit.Items.AddRange(x64jres);
                jreSelector.lb32Bit.Items.AddRange(x86jres);
                jreSelector.ShowDialog();
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        private void btnCorrupt_Click(object sender, EventArgs e)
        {
            if (cbRandomizeInstruction.Checked)
                multiTB_InstructionSeed.Value = JAVA_CORRUPTOR.Random.NextLong(multiTB_InstructionSeed.Minimum, multiTB_InstructionSeed.Maximum);
            if (cbRandomizeValue.Checked)
                multiTB_ValueSeed.Value = JAVA_CORRUPTOR.Random.NextLong(multiTB_ValueSeed.Minimum, multiTB_ValueSeed.Maximum);

            string outputFileName = tbInputJar.Text.Split('\\').Last() + "_corrupted_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jar";
            string outputFilePath = Path.Combine(tbOutputFolder.Text, outputFileName);
            string engineArgs = _engine.GetArguments();
            string arguments = $"\"{tbInputJar.Text}\" \"{outputFilePath}\" {multiTB_InstructionSeed.Value} {multiTB_ValueSeed.Value} {_engine.placeholderComboBox.SelectedIndex} {(double)S.GET<GeneralParametersForm>().multiTB_Intensity.Value / S.GET<GeneralParametersForm>().multiTB_Intensity.Maximum} {engineArgs}";
            /*
            switch (_engine.placeholderComboBox.SelectedIndex)
            {
                case 0: // Vector
                    arguments += $" {(int)javaVectorEngineControl1.cbVectorLimiterList.SelectedItem} {(int)javaVectorEngineControl1.cbVectorValueList.SelectedItem}";
                    break;
                case 1: // Arithmetic
                    arguments += $" {arithmeticEngineControl1.tbMaximum.Value / 1000f} {arithmeticEngineControl1.tbMinimum.Value / 1000f}";
                    foreach (int i in arithmeticEngineControl1.lbeLimiters.SelectedIndices)
                        arguments += $" {i * 4}";
                    arguments += $" :";
                    foreach (int i in arithmeticEngineControl1.lbeOperations.SelectedIndices)
                        arguments += $" {i * 4}";
                    arguments += $" : ";
                    arguments +=
                        (arithmeticEngineControl1.cbInt.Checked ? 1 : 0)
                        | (arithmeticEngineControl1.cbLong.Checked ? 2 : 0)
                        | (arithmeticEngineControl1.cbFloat.Checked ? 4 : 0)
                        | (arithmeticEngineControl1.cbDouble.Checked ? 8 : 0);

                    break;
                case 2: // Function
                    foreach (string s in functionEngineControl1.lbLimiterFunctions.SelectedItems)
                        arguments += $" {s}";
                    arguments += $" :";
                    foreach (string s in functionEngineControl1.lbValueFunctions.SelectedItems)
                        arguments += $" {s}";
                    arguments += $" :";
                    break;
                case 3: // Custom
                    arguments += $" \"{javaCustomEngineControl1.Path}\"";
                    break;
                case 4: // Sanitizer
                    sanitizerEngineControl1.ApplyAndWriteTemp();
                    arguments += $" \"{sanitizerEngineControl1.TempPath}\"";
                    break;
                case 5: // String
                    arguments += $" {stringEngineControl1.cbMode.SelectedIndex} {stringEngineControl1.tbCharacters.Text} {stringEngineControl1.tbPercentage.Value / 100f}";
                    break;
                case 6: // Rounding
                    arguments += $" {roundingEngineControl1.tbDecimalPlaces.Value / 1000}";
                    arguments += $@" {(roundingEngineControl1.cbInt.Checked ? 1 : 0)
                                   | (roundingEngineControl1.cbLong.Checked ? 2 : 0)
                                   | (roundingEngineControl1.cbFloat.Checked ? 4 : 0)
                                   | (roundingEngineControl1.cbDouble.Checked ? 8 : 0)}";
                    byte kinds = 0;
                    for (int i = 0; i < roundingEngineControl1.lbeKinds.SelectedIndices.Count; i++)
                        kinds += (byte)Math.Pow(2, roundingEngineControl1.lbeKinds.SelectedIndices[i]);
                    arguments += $" {kinds}";
                    byte operations = 0;
                    for (int i = 0; i < roundingEngineControl1.lbeOperations.SelectedIndices.Count; i++)
                        operations += (byte)Math.Pow(2, roundingEngineControl1.lbeOperations.SelectedIndices[i]);
                    arguments += $" {operations}";
                    break;
            }
            */

            Process process = new Process
            {
                StartInfo =
                {
                    FileName = $"{Directory.GetCurrentDirectory()}\\RTC\\PLUGINS\\JavaCorruptor_packed.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            void Log(object s, DataReceivedEventArgs args)
            {
                if (args.Data != null && args.Data.Length > 0)
                    MessageBox.Show(args.Data);
            }
            process.OutputDataReceived += Log;
            process.ErrorDataReceived += Log;

            if (cbShowDebugStuff.Checked)
            {
                MessageBox.Show(arguments);
                MessageBox.Show(Directory.GetCurrentDirectory());
            }

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (!_hasAskedToOpenFolder)
            {
                _hasAskedToOpenFolder = true;
                DialogResult result = MessageBox.Show("Done! Open output folder?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Process.Start("explorer.exe", tbOutputFolder.Text);
            }
        }

        private void btnSelectJRE_Click(object sender, EventArgs e)
        {
            string[] x64jres = Directory.GetDirectories("C:\\Program Files\\Java\\");
            string[] x86jres = Directory.GetDirectories("C:\\Program Files (x86)\\Java\\");

            JRESelectorForm jreSelector = new JRESelectorForm();
            jreSelector.lb64Bit.Items.AddRange(x64jres);
            jreSelector.lb32Bit.Items.AddRange(x86jres);
            jreSelector.ShowDialog();
        }
    }
}