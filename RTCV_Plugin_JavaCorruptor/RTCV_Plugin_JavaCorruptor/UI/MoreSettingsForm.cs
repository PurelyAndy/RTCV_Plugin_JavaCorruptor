using ObjectWeb.Asm;
using RTCV.NetCore;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Java_Corruptor.UI
{
    public partial class MoreSettingsForm : ColorizedForm
    {
        public MoreSettingsForm()
        {
            InitializeComponent();
            switch (CorruptionOptions.MethodCompute)
            {
                case 1:
                    rbComputeMaxStack.Checked = true; break;
                case 2:
                    rbComputeStackFrames.Checked = true; break;
            }
            lbThreads.Text = $"Threads: {CorruptionOptions.Threads}";
            tbThreads.Value = CorruptionOptions.Threads;
        }

        private void btnComputeInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("""
                            The JVM requires a method to contain stack frames and the maximum size of its stack to verify its integrity.
                            Most corruptions do not require stack frames to be recalculated, but if you make a blast layer with branches, you will have to either select "all stack frames" here, or launch your program with the -noverify JVM argument.
                            """, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rbComputeStackFrames_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComputeStackFrames.Checked)
            {
                CorruptionOptions.MethodCompute = ClassWriter.Compute_Frames;
                Params.SetParam("JAVA_METHOD_COMPUTE", CorruptionOptions.MethodCompute.ToString());
            }
        }

        private void rbComputeMaxStack_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComputeMaxStack.Checked)
            {
                CorruptionOptions.MethodCompute = ClassWriter.Compute_Maxs;
                Params.SetParam("JAVA_METHOD_COMPUTE", CorruptionOptions.MethodCompute.ToString());
            }
        }

        private void cbCompressJar_CheckedChanged(object sender, EventArgs e)
        {
            CorruptionOptions.CompressJar = cbCompressJar.Checked;
            Params.SetParam("JAVA_COMPRESS_JAR", CorruptionOptions.CompressJar.ToString());
        }

        private void tbThreads_Scroll(object sender, EventArgs e)
        {
            CorruptionOptions.Threads = tbThreads.Value;
            lbThreads.Text = $"Threads: {CorruptionOptions.Threads}";
            Params.SetParam("JAVA_CORRUPTION_THREADS", CorruptionOptions.Threads.ToString());
        }

        private void cbSelectDomains_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectDomains.Checked)
            {
                tlpDomains.Enabled = true;
                flpClasses.Enabled = true;
                flpMethods.Enabled = true;
            }
            else
            {
                tlpDomains.Enabled = false;
                flpClasses.Enabled = false;
                flpMethods.Enabled = false;
            }
            CorruptionOptions.UseDomains = cbSelectDomains.Checked;
        }

        private void MoreSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            e.Cancel = true;
            Hide();
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            AddEntry(flpClasses);
            CorruptionOptions.FilterClasses.Add("");
        }

        private void btnAddMethod_Click(object sender, EventArgs e)
        {
            AddEntry(flpMethods);
            CorruptionOptions.FilterMethods.Add("");
        }

        private FlowLayoutPanel AddEntry(FlowLayoutPanel flp)
        {
            FlowLayoutPanel pn = new();
            TextBox tb = new();
            Button btnRemove = new();

            pn.Width = flp.Width - 25;
            pn.Height = 30;
            pn.FlowDirection = FlowDirection.LeftToRight;
            btnRemove.Text = "X";
            btnRemove.ForeColor = Color.OrangeRed;
            btnRemove.Font = new Font("Consolas", 10);
            tb.Width = pn.Width - 40;
            btnRemove.Width = 24;
            btnRemove.Height = 24;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 0;

            btnRemove.Click += (s, e) => {
                flp.Controls.Remove(pn);
                if (flp == flpClasses)
                    CorruptionOptions.FilterClasses.Remove(tb.Text);
                else
                    CorruptionOptions.FilterMethods.Remove(tb.Text);
            };
            tb.TextChanged += (s, e) => {
                if (flp == flpClasses)
                    CorruptionOptions.FilterClasses[flp.Controls.IndexOf(pn)] = tb.Text;
                else
                    CorruptionOptions.FilterMethods[flp.Controls.IndexOf(pn)] = tb.Text;
            };

            pn.Controls.Add(tb);
            pn.Controls.Add(btnRemove);
            flp.Controls.Add(pn);
            return pn;
        }
    }
}
