using Newtonsoft.Json;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public partial class SanitizerEngineControl : JavaEngineControl
    {
        public string JBLPath;
        public string TempPath = $@"{Directory.GetCurrentDirectory()}\RTC\WORKING\TEMP\sanitizing.jbl";
        private string _jarPath;
        private string _serializedBlastLayer;
        private JavaBlastLayer _originalBlastLayer;
        public JavaBlastLayer CurrentBlastLayer;
        public JavaBlastLayer CurrentDisabledBlastLayer;
        public enum Selection
        {
            Yes,
            No,
            TryAgain,
            Nothing
        }
        public Selection Chosen = Selection.Nothing;

        public SanitizerEngineControl()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            lbSelectedAnswer.Text = "Selected: Yes";
            Chosen = Selection.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            lbSelectedAnswer.Text = "Selected: No";
            Chosen = Selection.No;
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            lbSelectedAnswer.Text = "Selected: Try Again";
            Chosen = Selection.TryAgain;
        }

        private void btnLoadJBL_Click(object sender, EventArgs e)
        {
            ofdLoadJBL.ShowDialog();

            if (ofdLoadJBL.FileName != string.Empty)
            {
                btnEndAndSave.Enabled = false;
                btnStartRestart.Enabled = true;
                JBLPath = ofdLoadJBL.FileName;
                //_jarPath = ofdLoadJBL.FileName.Substring(0, ofdLoadJBL.FileName.Length - 3) + "jar";
                lbLoadedJBL.Text = "Loaded: " + JBLPath;

                _serializedBlastLayer = File.ReadAllText(JBLPath);
                _originalBlastLayer = JsonConvert.DeserializeObject<JavaBlastLayer>(_serializedBlastLayer);
                CurrentBlastLayer = JsonConvert.DeserializeObject<JavaBlastLayer>(_serializedBlastLayer);

                UpdateCount(false);
                btnStartRestart.Text = "Start";
            }
        }

        private void btnStartRestart_Click(object sender, EventArgs e)
        {
            CurrentBlastLayer = JsonConvert.DeserializeObject<JavaBlastLayer>(_serializedBlastLayer);

            CurrentDisabledBlastLayer = new JavaBlastLayer();

            /* int half = CurrentBlastLayer.layer.Count / 2;
            for (int i = 0; i < half; i++)
            {
                var item = CurrentBlastLayer.layer.ElementAt(i);
                CurrentDisabledBlastLayer.layer.Add(item.Key, item.Value);
                CurrentBlastLayer.layer.Remove(item.Key);
            } */

            Dictionary<string, List<bool>> list = GetShuffledList(CurrentBlastLayer.layer);
            foreach (var item in list)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i])
                    {
                        var unit = CurrentBlastLayer.layer[item.Key][i];

                        if (!CurrentDisabledBlastLayer.layer.ContainsKey(item.Key))
                            CurrentDisabledBlastLayer.layer.Add(item.Key, new List<JavaBlastUnit>());

                        CurrentDisabledBlastLayer.layer[item.Key].Add(unit);

                        CurrentBlastLayer.layer[item.Key].Remove(unit);
                        item.Value.RemoveAt(i);
                        i--;

                        if (CurrentBlastLayer.layer[item.Key].Count == 0)
                            CurrentBlastLayer.layer.Remove(item.Key);
                    }
                }
            }

            btnStartRestart.Text = "Restart";
            lbSelectedAnswer.Text = "Press Corrupt to start.";
            Chosen = Selection.Nothing;
            btnEndAndSave.Enabled = true;
            btnYes.Enabled = btnNo.Enabled = btnTryAgain.Enabled = false;

            UpdateCount();
        }

        public void UpdateCount(bool updateDisabled = true)
        {
            lbCurrentUnits.Text = "Enabled Units: " + CurrentBlastLayer.layer.Aggregate(0, (acc, item) => acc + item.Value.Count);
            if (updateDisabled)
                lbTotalUnits.Text = "Disabled Units: " + CurrentDisabledBlastLayer.layer.Aggregate(0, (acc, item) => acc + item.Value.Count);
        }

        public void ApplyAndWriteTemp()
        {
            bool doneSanitizing = false;

            switch (Chosen)
            {
                case Selection.Yes:
                    {
                        if (CurrentBlastLayer.layer.Count == 1 && CurrentBlastLayer.layer.FirstOrDefault().Value.Count == 1)
                        {
                            doneSanitizing = true;
                            break;
                        }

                        CurrentDisabledBlastLayer.layer.Clear();

                        Dictionary<string, List<bool>> list = GetShuffledList(CurrentBlastLayer.layer);
                        foreach (var item in list)
                        {
                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                if (item.Value[i])
                                {
                                    var unit = CurrentBlastLayer.layer[item.Key][i];

                                    if (!CurrentDisabledBlastLayer.layer.ContainsKey(item.Key))
                                        CurrentDisabledBlastLayer.layer.Add(item.Key, new List<JavaBlastUnit>());

                                    CurrentDisabledBlastLayer.layer[item.Key].Add(unit);

                                    CurrentBlastLayer.layer[item.Key].Remove(unit);
                                    item.Value.RemoveAt(i);
                                    i--;

                                    if (CurrentBlastLayer.layer[item.Key].Count == 0)
                                        CurrentBlastLayer.layer.Remove(item.Key);
                                }
                            }
                        }
                        UpdateCount();
                        break;
                    }
                case Selection.No:
                    {
                        if (CurrentDisabledBlastLayer.layer.Count == 1 && CurrentDisabledBlastLayer.layer.FirstOrDefault().Value.Count == 1)
                        {
                            CurrentBlastLayer.layer = CurrentDisabledBlastLayer.layer;
                            doneSanitizing = true;
                            break;
                        }

                        CurrentBlastLayer.layer.Clear();

                        Dictionary<string, List<bool>> list = GetShuffledList(CurrentDisabledBlastLayer.layer);
                        foreach (var item in list)
                        {
                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                if (item.Value[i])
                                {
                                    var unit = CurrentDisabledBlastLayer.layer[item.Key][i];

                                    if (!CurrentBlastLayer.layer.ContainsKey(item.Key))
                                        CurrentBlastLayer.layer.Add(item.Key, new List<JavaBlastUnit>());

                                    CurrentBlastLayer.layer[item.Key].Add(unit);

                                    CurrentDisabledBlastLayer.layer[item.Key].Remove(unit);
                                    item.Value.RemoveAt(i);
                                    i--;

                                    if (CurrentDisabledBlastLayer.layer[item.Key].Count == 0)
                                        CurrentDisabledBlastLayer.layer.Remove(item.Key);
                                }
                            }
                        }
                        UpdateCount();
                        break;
                    }
                case Selection.TryAgain:
                    {
                        Dictionary<string, List<JavaBlastUnit>> combined = new Dictionary<string, List<JavaBlastUnit>>();
                        foreach (var item in CurrentBlastLayer.layer)
                            if (combined.ContainsKey(item.Key))
                                combined[item.Key].AddRange(item.Value);
                            else
                                combined.Add(item.Key, item.Value);
                        foreach (var item in CurrentDisabledBlastLayer.layer)
                            if (combined.ContainsKey(item.Key))
                                combined[item.Key].AddRange(item.Value);
                            else
                                combined.Add(item.Key, item.Value);

                        CurrentBlastLayer.layer.Clear();
                        CurrentDisabledBlastLayer.layer.Clear();

                        Dictionary<string, List<bool>> list = GetShuffledList(combined);
                        foreach (var item in list)
                        {
                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                var unit = combined[item.Key][i];

                                if (item.Value[i])
                                {
                                    if (!CurrentBlastLayer.layer.ContainsKey(item.Key))
                                        CurrentBlastLayer.layer.Add(item.Key, new List<JavaBlastUnit>());

                                    CurrentBlastLayer.layer[item.Key].Add(unit);
                                }
                                else
                                {
                                    if (!CurrentDisabledBlastLayer.layer.ContainsKey(item.Key))
                                        CurrentDisabledBlastLayer.layer.Add(item.Key, new List<JavaBlastUnit>());

                                    CurrentDisabledBlastLayer.layer[item.Key].Add(unit);
                                }
                            }
                        }
                        UpdateCount();
                        break;
                    }
                case Selection.Nothing:
                    btnYes.Enabled = btnNo.Enabled = btnTryAgain.Enabled = true;
                    lbSelectedAnswer.Text = "Selected: Nothing (Please select an option)";
                    break;
            }

            File.WriteAllText(TempPath, JsonConvert.SerializeObject(CurrentBlastLayer));

            if (doneSanitizing)
                WriteFinalJBL();
        }

        public void WriteFinalJBL()
        {
            File.Copy(TempPath, JBLPath + "_sanitized.jbl");
            btnEndAndSave.Enabled = btnYes.Enabled = btnNo.Enabled = btnTryAgain.Enabled = false;
            MessageBox.Show("Wrote final JBL to the same folder as the original.");
        }

        /* private List<bool> GetShuffledList(int count)
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < count; i++)
                if (i % 2 == 0)
                    list.Add(true);
                else
                    list.Add(false);

            for (int i = 0; i < list.Count; i++)
            {
                int r = _random.Next(list.Count);
                (list[i], list[r]) = (list[r], list[i]);
            }

            int @true = 0;
            int @false = 0;
            for (int i = 0; i < list.Count; i++)
                if (list[i])
                    @true++;
                else
                    @false++;

            return list;
        } */

        private Dictionary<string, List<bool>> GetShuffledList(Dictionary<string, List<JavaBlastUnit>> units)
        {
            Dictionary<string, List<bool>> list = new Dictionary<string, List<bool>>();
            int evenOdd = 0;
            foreach (var item in units)
            {
                list.Add(item.Key, new List<bool>());
                foreach (var _ in item.Value)
                {
                    if (evenOdd++ % 2 == 0)
                        list[item.Key].Add(true);
                    else
                        list[item.Key].Add(false);
                }

                for (int i = 0; i < list[item.Key].Count; i++)
                {
                    int r = JAVA_CORRUPTOR.Random.Next(list[item.Key].Count);
                    (list[item.Key][i], list[item.Key][r]) = (list[item.Key][r], list[item.Key][i]);
                }
            }

            return list;
        }

        private void btnEndAndSave_Click(object sender, EventArgs e) => WriteFinalJBL();

        public override string GetArguments()
        {
            ApplyAndWriteTemp(); // TODO: This is the wrong place for this,
                                 // and I don't feel like moving it right now.
            return $"\"{TempPath}\"";
        }
    }
}
