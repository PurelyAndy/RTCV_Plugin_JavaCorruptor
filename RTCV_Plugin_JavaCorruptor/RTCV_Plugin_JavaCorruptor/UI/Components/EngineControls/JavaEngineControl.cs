using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAVACORRUPTOR.UI.Components.EngineControls
{
    public abstract partial class JavaEngineControl : UserControl
    {
        public event EventHandler EngineChanged;
        public JavaEngineControl()
        {
            InitializeComponent();
        }

        private void placeholderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EngineChanged?.Invoke(this, EventArgs.Empty);
        }

        public abstract string GetArguments();
    }
}
