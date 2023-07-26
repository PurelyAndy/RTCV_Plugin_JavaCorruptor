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
    public partial class Engine : UserControl
    {
        public event EventHandler EngineChanged;
        public Engine()
        {
            InitializeComponent();
        }

        private void placeholderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EngineChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
