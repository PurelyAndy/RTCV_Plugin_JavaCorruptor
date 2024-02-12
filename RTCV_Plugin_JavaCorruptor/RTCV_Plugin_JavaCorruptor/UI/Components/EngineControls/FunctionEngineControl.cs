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

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public partial class FunctionEngineControl : JavaEngineControl
    {
        public FunctionEngineControl()
        {
            InitializeComponent();
        }

        public override string GetArguments()
        {
            string args = "";

            foreach (string s in lbLimiterFunctions.SelectedItems)
                args += $" {s}";
            args += $" :";

            foreach (string s in lbValueFunctions.SelectedItems)
                args += $" {s}";
            args += $" :";

            return args;
        }
    }
}
