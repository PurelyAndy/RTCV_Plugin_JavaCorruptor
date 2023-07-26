using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Java_Corruptor;

namespace Java_Corruptor.UI.Engines
{
    [Designer(typeof(Engine))]
    public partial class VectorEngine : Engine
    {
        public VectorEngine()
        {
            InitializeComponent();

            Opcodes[] opcodes = (Opcodes[])Enum.GetValues(typeof(Opcodes));

            foreach (Opcodes op in opcodes)
            {
                cbVectorLimiterList.Items.Add(op);
                cbVectorValueList.Items.Add(op);
            }
        }
    }
}
