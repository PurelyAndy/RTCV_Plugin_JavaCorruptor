using System.Drawing;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.UI.Components;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.Common;
using RTCV.UI;

namespace Java_Corruptor.UI;

public partial class JavaSanitizeToolForm : RTCV.UI.Modular.ColorizedForm
{
    private JavaFastSanitizer _sanitizer;
    private int _originalSize;

    public JavaSanitizeToolForm()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            if (CloudDebug.ShowErrorDialog(ex, true) == DialogResult.Abort)
            {
                throw new AbortEverythingException();
            }
        }
        Colors.SetRTCColor(Color.FromArgb(0xA0, 0xA0, 0xFF), this);
    }

    internal static void OpenSanitizeTool(JavaStashKey sk = null, bool lockUI = true)
    {
        //This is the main entry point of the code


        //makes sure it gets a fresh instance of the sanitize tool, can't use two at the same time.
        if (!S.ISNULL<JavaSanitizeToolForm>() && S.GET<JavaSanitizeToolForm>().IsDisposed)
        {
            S.GET<JavaSanitizeToolForm>()?.Close();
        }
        JavaSanitizeToolForm stf = new();
        S.SET(stf);

        SerializedInsnBlastLayerCollection bl = sk?.BlastLayer;

        //if no blastlayer or stockpile was provided, get out
        if (sk == null || bl == null)
        {
            return;
        }

        if (bl.Layer.Layer.All(x => x.IsLocked))
        {
            MessageBox.Show("Sanitize Tool cannot sanitize BlastLayers that don't have any units.");
            return;
        }

        if (bl.Layer.Layer.Count(x => !x.IsLocked) == 1)
        {
            MessageBox.Show("Sanitize Tool cannot sanitize BlastLayers that only have one unit.");
            return;
        }

        //backup the blastlayer, prepare the tool and launch.

        SerializedInsnBlastLayer clone = new(bl.Layer.Layer.Where(x => !x.IsLocked).ToList());
        stf._originalSize = clone.Layer.Count;
        stf.lbOriginalLayerSize.Text = $"Original Layer size: {clone.Layer.Count}";
        stf.lbSteps.DisplayMember = "Text";
        stf.lbSteps.ValueMember = "Value";

        stf._sanitizer = new(sk, clone);
        stf.UpdateSanitizeProgress();
        stf.lbSteps.Items.Add(new { Text = $"[{stf._sanitizer.OriginalLayer.Layer.Count} Units]", Value = "" });

        if (lockUI)
        {
            stf.ShowDialog();
        }
        else
        {
            stf.Show();
        }
    }

    public async void Reroll(object sender, EventArgs e)
    {
        pnBlastLayerSanitization.Visible = false;
        Refresh();

        await _sanitizer.Disable50();
        await _sanitizer.LoadCorrupt();
        UpdateSanitizeProgress();
        lbSteps.Items.RemoveAt(lbSteps.Items.Count - 1);
        lbSteps.Items.Add(new { Text = $"[{_sanitizer.NumCurUnits} Units]", Value = "" });
        pnBlastLayerSanitization.Visible = true;
    }

    public async void YesEffect(object sender, EventArgs e)
    {
        pnBlastLayerSanitization.Visible = false;
        Refresh();

        _sanitizer.Yes();
        await _sanitizer.Disable50();
        await _sanitizer.LoadCorrupt();

        UpdateSanitizeProgress();
        lbSteps.Items.Add(new { Text = $"[{_sanitizer.NumCurUnits} Units]", Value = "" });

        if (_sanitizer.NumCurUnits == 1)
        {
            lbSanitizationText.Text = "1 Unit remaining, sanitization complete.";
            btnYesEffect.Visible = false;
            btnNoEffect.Visible = false;
            btnReroll.Visible = false;
        }

        pnBlastLayerSanitization.Visible = true;
    }

    public async void NoEffect(object sender, EventArgs e)
    {
        pnBlastLayerSanitization.Visible = false;
        Refresh();

        _sanitizer.No();

        await _sanitizer.Disable50();
        await _sanitizer.LoadCorrupt();

        UpdateSanitizeProgress();
        lbSteps.Items.Add(new { Text = $"[{_sanitizer.NumCurUnits} Units]", Value = "" });

        if (_sanitizer.NumCurUnits == 1)
        {
            lbSanitizationText.Text = "1 Unit remaining, sanitization complete.";
            btnYesEffect.Visible = false;
            btnNoEffect.Visible = false;
            btnReroll.Visible = false;
        }

        pnBlastLayerSanitization.Visible = true;
    }

    private async void ReplayCorruption(object sender, EventArgs e)
    {
        pnBlastLayerSanitization.Visible = false;
        Refresh();
        await _sanitizer.Replay();
        pnBlastLayerSanitization.Visible = true;
    }

    public void LeaveAndKeepChanges(object sender, EventArgs e)
    {
        bool success = JavaBlastEditorForm.OpenBlastEditor(_sanitizer.GetFinalStashKey());

        if (success)
            Close();
    }

    public void LeaveAndSubtractChanges(object sender, EventArgs e)
    {
        JavaStashKey sk = _sanitizer.GetStashKeyMinusChanges();

        bool success = JavaBlastEditorForm.OpenBlastEditor(sk);
        if (success)
            Close();
    }

    private void LeaveWithoutChanges(object sender, EventArgs e)
    {
        //Open blast editor with original blast layer/savestate
        bool success = JavaBlastEditorForm.OpenBlastEditor(_sanitizer.GetOriginalStashKey());

        if (success)
            Close();
    }

    private async void GoBackToPreviousState(object sender, EventArgs e)
    {
        if (_sanitizer.stateStack.Count < 1 || _sanitizer.shownStack.Count < 1) return;

        pnBlastLayerSanitization.Visible = false;
        Refresh();

        _sanitizer.Undo();
        UpdateSanitizeProgress();
        lbSteps.Items.RemoveAt(lbSteps.Items.Count - 1);

        await _sanitizer.LoadCorrupt();

        lbSanitizationText.Text = "Is the effect you are looking for still present?";
        btnYesEffect.Visible = true;
        btnNoEffect.Visible = true;
        btnReroll.Visible = true;

        if (lbSteps.Items.Count == 1)
        {
            lbWorkingPleaseWait.Visible = false;
            pnBlastLayerSanitization.Visible = false;
            btnStartSanitizing.Visible = true;
        }
        else
        {
            pnBlastLayerSanitization.Visible = true;
        }
    }

    public void UpdateSanitizeProgress()
    {
        int originalRemainder = _originalSize;
        int originalMaxsteps = 0;
        while (originalRemainder > 1)
        {
            originalRemainder /= 2;
            originalMaxsteps++;
        }

        int currentSize = _sanitizer.NumCurUnits;

        int currentRemainder = currentSize;
        int currentMaxsteps = 0;
        while (currentRemainder > 1)
        {
            currentRemainder /= 2;
            currentMaxsteps++;
        }

        lbCurrentLayerSize.Text = $"Current Layer size: {currentSize}";
        pbProgress.Maximum = originalMaxsteps;
        pbProgress.Value = originalMaxsteps - currentMaxsteps;
    }

    public async void StartSanitizing(object sender, EventArgs e)
    {
        btnStartSanitizing.Visible = false;

        await _sanitizer.Disable50();
        await _sanitizer.LoadCorrupt();

        pnBlastLayerSanitization.Visible = true;
        lbWorkingPleaseWait.Visible = true;
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.UserClosing)
        {
            return;
        }

        Form frm = (sender as Form);
        Button check = (frm?.ActiveControl as Button);

        if (check == null && lbSteps.Items.Count > 1)
        {
            DialogResult dr = MessageBox.Show("Would you like to restore the Original BlastLayer in the Blest Editor?", "Leaving Sanitize Tool", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (dr)
            {
                case DialogResult.Yes:
                    //S.GET<JavaBlastEditorForm>().LoadBlastlayer(_originalBlastLayer);
                    JavaBlastEditorForm.OpenBlastEditor(_sanitizer.GetOriginalStashKey());
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                default:
                    e.Cancel = true;
                    break;
            }
        }
    }

    private void AddToStockpile(object sender, EventArgs e)
    {
        JavaStashKey oldSk = (JavaStashKey)_sanitizer.GetFinalStashKey().Clone();
        JavaStashKey newSk = new(RtcCore.GetRandomKey(), oldSk.ParentKey, null)
        {
            JarFilename = oldSk.JarFilename,
            GameName = oldSk.GameName,
            BlastLayer = (SerializedInsnBlastLayerCollection)oldSk.BlastLayer.Clone(),
        };
        JavaStockpileManagerUISide.StashHistory.Add(newSk);

        S.GET<JavaStashHistoryForm>().RefreshStashHistory();
        S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
        S.GET<JavaStashHistoryForm>().lbStashHistory.ClearSelected();

        S.GET<JavaStashHistoryForm>().DontLoadSelectedStash = true;
        S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex = S.GET<JavaStashHistoryForm>().lbStashHistory.Items.Count - 1;
        JavaStockpileManagerUISide.CurrentStashkey = JavaStockpileManagerUISide.StashHistory[S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex];


        S.GET<JavaStashHistoryForm>().AddStashToStockpileFromUI();

        //bool res = S.GET<JavaStashHistoryForm>().AddStashToStockpileFromUI();

        //if (res)
        //{
        //    this.Close();
        //}
        if (cbCloseOnSend.Checked)
        {
            Close();
        }
    }
    private void AddToStash(object sender, EventArgs e)
    {
        JavaStashKey oldSk = (JavaStashKey)_sanitizer.GetFinalStashKey().Clone();
        JavaStashKey newSk = new(RtcCore.GetRandomKey(), oldSk.ParentKey, null)
        {
            JarFilename = oldSk.JarFilename,
            GameName = oldSk.GameName,
            BlastLayer = (SerializedInsnBlastLayerCollection)oldSk.BlastLayer.Clone(),
        };
        JavaStockpileManagerUISide.StashHistory.Add(newSk);

        S.GET<JavaStashHistoryForm>().RefreshStashHistory();

        //S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
        //S.GET<JavaStashHistoryForm>().lbStashHistory.ClearSelected();

        //S.GET<JavaStashHistoryForm>().DontLoadSelectedStash = true;
        //S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex = S.GET<JavaStashHistoryForm>().lbStashHistory.Items.Count - 1;
        //JavaStockpileManagerUISide.CurrentStashkey = JavaStockpileManagerUISide.StashHistory[S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex];
        //S.GET<JavaStashHistoryForm>().AddStashToStockpileFromUI();

        //S.GET<JavaBlastEditorForm>().SendToStash(null, null);
        //this.Close();
        if (cbCloseOnSend.Checked)
        {
            Close();
        }

    }
    private void LeaveWithNoChanges(object sender, EventArgs e)
    {
        Close();
    }

    private void cbCloseOnSend_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCloseOnSend.Checked)
            Params.SetParam("SANITIZETOOL_AUTOCLOSE");
        else
            Params.RemoveParam("SANITIZETOOL_AUTOCLOSE");
    }

    private void SanitizeToolForm_Load(object sender, EventArgs e)
    {
        if (Params.IsParamSet("SANITIZETOOL_AUTOCLOSE"))
            cbCloseOnSend.Checked = true;
    }
}

internal class JavaFastSanitizer
{
    private JavaStashKey internalSK;
    public SerializedInsnBlastLayer OriginalLayer { get; private set; }


    public Stack<List<SerializedInsnBlastUnit>> stateStack = new();    //This is the stack of winning layers

    public Stack<List<SerializedInsnBlastUnit>> shownStack = new();    //This is the history stack of shown halves
    public Stack<List<SerializedInsnBlastUnit>> otherStack = new();    //This is the history stack of other halves

    public List<SerializedInsnBlastUnit> shownHalf;                                           //Work list of units for those which are shown (enabled)
    public List<SerializedInsnBlastUnit> otherHalf;                                           //Work list of units for those which aren't shown (disabled)

    Random rand = new();
    public int NumCurUnits => stateStack.Peek().Count;
    public JavaFastSanitizer(JavaStashKey originalStashkey, SerializedInsnBlastLayer blClone)
    {
        //Create StashKey clone
        internalSK = new(RtcCore.GetRandomKey(), originalStashkey.ParentKey, null)
        {
            JarFilename = originalStashkey.JarFilename,
            GameName = originalStashkey.GameName,
            BlastLayer = new(blClone),
        };
        OriginalLayer = blClone;
        shownHalf = [..OriginalLayer.Layer];
        otherHalf = [..OriginalLayer.Layer];
        stateStack.Push([..OriginalLayer.Layer]);
    }

    internal JavaStashKey GetFinalStashKey()
    {
        List<SerializedInsnBlastUnit> allUnits = new();
        allUnits.AddRange(shownHalf);

        List<SerializedInsnBlastUnit> disabledUnits = new();

        //extra check because of how otherHalf is instanciated. Maybe it should be empty at first?
        disabledUnits.AddRange(otherHalf.Where(x => !shownHalf.Contains(x)));
        //disabledUnits.AddRange(otherHalf);


        foreach (SerializedInsnBlastUnit unit in disabledUnits)
            unit.IsEnabled = false;

        allUnits.AddRange(disabledUnits);

        internalSK.BlastLayer = new(allUnits);

        return internalSK;
    }

    internal void Clean()
    {
        stateStack.Clear();
        shownHalf = null;
        otherHalf = null;
        internalSK = null;
        OriginalLayer = null;
    }

    internal void Undo()
    {
        //uncommit the previous choice
        stateStack.Pop();

        shownHalf = shownStack.Pop();
        otherHalf = otherStack.Pop();
    }

    internal void Yes()
    {
        //commit the choice of the shown half and prepare for next step
        stateStack.Push(shownHalf);
        internalSK.BlastLayer = new(shownHalf);

        //backup the possible choices
        shownStack.Push(shownHalf);
        otherStack.Push(otherHalf);
    }
    internal void No()
    {
        //commit the choice of the other half and prepare for next step
        stateStack.Push(otherHalf);
        internalSK.BlastLayer = new(otherHalf);

        //backup the possible choices
        shownStack.Push(shownHalf);
        otherStack.Push(otherHalf);
    }

    internal async Task Disable50()
    {
        //get the latest winning layer
        List<SerializedInsnBlastUnit> lastState = stateStack.Peek();
        shownHalf = new();
        otherHalf = new();

        if (lastState.Count == 1)
        {
            shownHalf = lastState;
            otherHalf = lastState;
            return;
        }

        List<SerializedInsnBlastUnit> randomizedUnits = lastState.OrderBy(_ => rand.Next()).ToList();

        int totalCount = randomizedUnits.Count();
        int shownCount = (totalCount / 2);
        int hiddenCount = totalCount - shownCount;

        shownHalf.AddRange(randomizedUnits.GetRange(0, shownCount));
        otherHalf.AddRange(randomizedUnits.GetRange(shownCount, hiddenCount));


        /*

        int[] allIndices = new int[lastState.Count];
        for (int i = 0; i < lastState.Count; i++)
        {
            allIndices[i] = i;
        }

        //In-place shuffle, optimized with cached lengths
        var shuffleCount = allIndices.Length;
        var shuffleEnd = shuffleCount - 1;
        for (var i = 0; i < shuffleEnd; ++i)
        {
            var r = rand.Next(i, shuffleCount);
            var tmp = allIndices[i];
            allIndices[i] = allIndices[r];
            allIndices[r] = tmp;
        }

        for (int i = 0; i < allIndices.Length - 1; i += 2)
        {
            shownHalf.Add(lastState[allIndices[i]]);
            otherHalf.Add(lastState[allIndices[i + 1]]);
        }

        if (lastState.Count % 2 == 1)
        {
            shownHalf.Add(lastState[allIndices.Length - 1]);
        }

        */

        await Task.Delay(1);
    }

    internal async Task LoadCorrupt()
    {
        internalSK.BlastLayer = new(shownHalf);
        S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = internalSK.Run();

        await Task.Delay(1);
    }

    internal JavaStashKey GetOriginalStashKey()
    {
        internalSK.BlastLayer = new(OriginalLayer);
        return internalSK;
    }

    internal JavaStashKey GetStashKeyMinusChanges()
    {
        List<SerializedInsnBlastUnit> newLayer = new();

        newLayer.AddRange(OriginalLayer.Layer.Where(x => !shownHalf.Contains(x)));

        /*
        //subtracted units are disabled rather than deleted (Not good idea as default behavior
        var disabledUnits = OriginalLayer.Layer.Where(x => shownHalf.Contains(x));
        foreach (var unit in disabledUnits)
            unit.IsEnabled = false;

        newLayer.AddRange(disabledUnits);
        */

        internalSK.BlastLayer = new(newLayer);
        return internalSK;
    }

    internal async Task Replay()
    {
        S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = internalSK.Run();

        await Task.Delay(1);
    }
}