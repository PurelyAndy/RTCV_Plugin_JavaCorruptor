using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Java_Corruptor.UI.Engines
{
    [Designer(typeof(Engine))]
    public partial class ArithmeticEngine : Engine
    {
        public ArithmeticEngine()
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
    }
}
