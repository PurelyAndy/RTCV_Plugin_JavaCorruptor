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
    public partial class JavaVectorEngineControl : JavaEngineControl
    {
        public JavaVectorEngineControl()
        {
            InitializeComponent();

            Opcodes[] opcodes = (Opcodes[])Enum.GetValues(typeof(Opcodes));

            foreach (Opcodes op in opcodes)
            {
                cbVectorLimiterList.Items.Add(op);
                cbVectorValueList.Items.Add(op);
            }
        }

        public override string GetArguments()
            => $"{(int)cbVectorLimiterList.SelectedItem} {(int)cbVectorValueList.SelectedItem}";
    }
}
