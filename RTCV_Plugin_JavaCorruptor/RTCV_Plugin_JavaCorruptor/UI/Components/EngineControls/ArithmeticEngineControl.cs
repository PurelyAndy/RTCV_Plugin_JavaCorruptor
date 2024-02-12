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
    public partial class ArithmeticEngineControl : JavaEngineControl
    {
        public ArithmeticEngineControl()
        {
            InitializeComponent();
        }

        private void tbMaximum_Scroll(object sender, EventArgs e)
        {
            lbMaximum.Text = "Maximum: " + (float)tbMaximum.Value / 1000;
        }

        private void tbMinimum_Scroll(object sender, EventArgs e)
        {
            lbMinimum.Text = "Minimum: " + (float)tbMinimum.Value / 1000;
        }

        public override string GetArguments()
        {
            string args = $"{tbMaximum.Value / 1000f} {tbMinimum.Value / 1000f}";
            foreach (int i in lbeLimiters.SelectedIndices)
                args += $" {i * 4}";
            args += $" :";
            foreach (int i in lbeOperations.SelectedIndices)
                args += $" {i * 4}";
            args += $" : ";
            args +=  (cbInt.Checked ? 1 : 0)
                |   (cbLong.Checked ? 2 : 0)
                |  (cbFloat.Checked ? 4 : 0)
                | (cbDouble.Checked ? 8 : 0);
            return args;
        }
    }
}
