using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public partial class StringEngineControl : JavaEngineControl
    {
        public StringEngineControl()
        {
            InitializeComponent();
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMode.SelectedIndex == 0)
                tbCharacters.Enabled = true;
            else
                tbCharacters.Enabled = false;
        }

        private void tbPercentage_Scroll(object sender, EventArgs e)
        {
            lbPercentage.Text = $"Percentage: {tbPercentage.Value}";
        }


        public override string GetArguments() => $"{cbMode.SelectedIndex} {tbCharacters.Text} {tbPercentage.Value / 100f}\";";
    }
}
