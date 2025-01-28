using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;

namespace Java_Corruptor;

/// <summary>
/// the default cloud debug form is practically useless on AggregateExceptions
/// </summary>
public class BetterCloudDebug : CloudDebug, IColorize
{
    public BetterCloudDebug(Exception ex, bool canContinue = true) : base(ex, canContinue)
    {
        TextBox tb = (TextBox)typeof(CloudDebug).GetRuntimeFields().First(f => f.Name == "tbStackTrace")!.GetValue(this);
        tb.Text = ex.ToString();
        S.RegisterColorizable(this);
        Load += Colorize;
        FormClosed += DeregisterColorizable;
    }

    private void DeregisterColorizable(object sender, FormClosedEventArgs e)
    {
        S.DeregisterColorizable(this);
    }
    private void Colorize(object sender, EventArgs e) => Recolor();
    public void Recolor()
    {
        Colors.SetRTCColor(Colors.GeneralColor, this);
    }
}