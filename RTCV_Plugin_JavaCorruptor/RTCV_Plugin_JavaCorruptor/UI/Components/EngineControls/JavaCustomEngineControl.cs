using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public partial class JavaCustomEngineControl : JavaEngineControl
    {
        public string Path = "";
        public JavaCustomEngineControl()
        {
            InitializeComponent();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                Path = saveFileDialog1.FileName;

                string text = "[FIND]\n";
                text += tbFind.Text + "\n";
                text += "[REPLACE]\n";
                text += tbReplace.Text;

                System.IO.File.WriteAllText(Path, text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Path != "") {
                string text = "[FIND]\n";
                text += tbFind.Text + "\n";
                text += "[REPLACE]\n";
                text += tbReplace.Text;

                System.IO.File.WriteAllText(Path, text);
            } else {
                btnSaveAs_Click(sender, e);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                Path = openFileDialog1.FileName;

                string text = System.IO.File.ReadAllText(Path);
                
                string find = text.Substring(text.IndexOf("[FIND]") + 6, text.IndexOf("[REPLACE]") - text.IndexOf("[FIND]") - 6);
                string replace = text.Substring(text.IndexOf("[REPLACE]") + 9);

                tbFind.Text = find;
                tbReplace.Text = replace;
            }
        }

        public override string GetArguments() => $"\"{Path}\"";
    }
}
