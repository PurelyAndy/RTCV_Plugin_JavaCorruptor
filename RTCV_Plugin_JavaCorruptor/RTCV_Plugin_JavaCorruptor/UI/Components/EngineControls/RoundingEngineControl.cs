using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JAVACORRUPTOR;

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public partial class RoundingEngineControl : JavaEngineControl
    {
        public RoundingEngineControl()
        {
            InitializeComponent();
        }

        private void tbRounding_Scroll(object sender, EventArgs e)
        {
            lbDecimalPlaces.Text = $"Round to {tbDecimalPlaces.Value / 1000} decimal places";
        }


        public override string GetArguments()
        {
            string args = $"{tbDecimalPlaces.Value / 1000}";
            args += $@" {(cbInt.Checked ? 1 : 0)
                      | (cbLong.Checked ? 2 : 0)
                     | (cbFloat.Checked ? 4 : 0)
                    | (cbDouble.Checked ? 8 : 0)}";

            byte kinds = 0;
            for (int i = 0; i < lbeKinds.SelectedIndices.Count; i++)
                kinds += (byte)Math.Pow(2, lbeKinds.SelectedIndices[i]);
            args += $" {kinds}";

            byte operations = 0;
            for (int i = 0; i < lbeOperations.SelectedIndices.Count; i++)
                operations += (byte)Math.Pow(2, lbeOperations.SelectedIndices[i]);
            args += $" {operations}";

            return args;
        }
    }
}
