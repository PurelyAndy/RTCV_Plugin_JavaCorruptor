using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.Common;
using RTCV.UI;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI.Components;

public partial class JavaGlitchHarvesterBlastForm : ComponentForm, IBlockable
{
    private void HandleMouseDown(object s, MouseEventArgs e) => this.HandleMouseDownP(s, e);
    private void HandleFormClosing(object s, FormClosingEventArgs e) => this.HandleFormClosingP(s, e);

    public bool MergeMode { get; private set; }
    public GlitchHarvesterMode ghMode { get; set; } = GlitchHarvesterMode.CORRUPT; //Current Glitch Harvester mode
    public GlitchHarvesterMode ghModeStore { get; set; } = GlitchHarvesterMode.CORRUPT; //Temporary Variable used for borrowing different corruption methods
    public bool LoadOnSelect { get; set; } = true;
    public bool loadBeforeOperation { get; set; } = true;

    private Color? originalRenderOutputButtonColor = null;

    private bool isCorruptionApplied;
    public bool IsCorruptionApplied
    {
        get => isCorruptionApplied;
        set
        {
            if (value)
            {
                btnBlastToggle.BackColor = Color.FromArgb(224, 128, 128);
                btnBlastToggle.ForeColor = Color.Black;
                btnBlastToggle.Text = "BlastLayer : ON";

                S.GET<StockpilePlayerForm>().btnBlastToggle.BackColor = Color.FromArgb(224, 128, 128);
                S.GET<StockpilePlayerForm>().btnBlastToggle.ForeColor = Color.Black;
                S.GET<StockpilePlayerForm>().btnBlastToggle.Text = "BlastLayer : ON     (Attempts to uncorrupt/recorrupt in real-time)";

                S.GET<SimpleModeForm>().btnBlastToggle.BackColor = Color.FromArgb(224, 128, 128);
                S.GET<SimpleModeForm>().btnBlastToggle.ForeColor = Color.Black;
                S.GET<SimpleModeForm>().btnBlastToggle.Text = "BlastLayer : ON     (Attempts to uncorrupt/recorrupt in real-time)";
            }
            else
            {
                btnBlastToggle.BackColor = S.GET<CoreForm>().btnLogo.BackColor;
                btnBlastToggle.ForeColor = Color.White;
                btnBlastToggle.Text = "BlastLayer : OFF";

                S.GET<StockpilePlayerForm>().btnBlastToggle.BackColor = S.GET<CoreForm>().btnLogo.BackColor;
                S.GET<StockpilePlayerForm>().btnBlastToggle.ForeColor = Color.White;
                S.GET<StockpilePlayerForm>().btnBlastToggle.Text = "BlastLayer : OFF    (Attempts to uncorrupt/recorrupt in real-time)";

                S.GET<SimpleModeForm>().btnBlastToggle.BackColor = S.GET<CoreForm>().btnLogo.BackColor;
                S.GET<SimpleModeForm>().btnBlastToggle.ForeColor = Color.White;
                S.GET<SimpleModeForm>().btnBlastToggle.Text = "BlastLayer : OFF    (Attempts to uncorrupt/recorrupt in real-time)";
            }

            isCorruptionApplied = value;
        }
    }

    public JavaGlitchHarvesterBlastForm()
    {
        InitializeComponent();

        //cbRenderType.SelectedIndex = 0;

        //Registers the drag and drop with the blast edirot form
        AllowDrop = true;
        DragEnter += OnDragEnter;
        DragDrop += OnDragDrop;
    }

    private void OnDragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        foreach (string f in files)
        {
            if (f.Contains(".jbl"))
            {
                SerializedInsnBlastLayerCollection bl = JavaBlastTools.LoadBlastLayerFromFile(f);
                JavaStashKey newStashKey = new(RtcCore.GetRandomKey(), null, bl);
                S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = JavaStockpileManagerUISide.ApplyStashkey(newStashKey, false);
            }
        }
    }

    private void OnDragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Link;
    }

    public void OneTimeExecute()
    {
        //Disable autocorrupt
        S.GET<CoreForm>().AutoCorrupt = false;

        if (ghMode == GlitchHarvesterMode.CORRUPT)
        {
            IsCorruptionApplied = JavaStockpileManagerUISide.ApplyStashkey(JavaStockpileManagerUISide.CurrentStashkey, loadBeforeOperation);
        }
    }

    public void RedrawActionUI()
    {
        // Merge tool and ui change
        if (S.GET<JavaStockpileManagerForm>().dgvStockpile.SelectedRows.Count > 1)
        {
            MergeMode = true;
            btnCorrupt.Text = "  Merge";
            S.GET<JavaStockpileManagerForm>().btnRenameSelected.Visible = false;
            S.GET<JavaStockpileManagerForm>().btnRemoveSelectedStockpile.Text = "  Remove Items";
        }
        else
        {
            MergeMode = false;
            S.GET<JavaStockpileManagerForm>().btnRenameSelected.Visible = true;
            S.GET<JavaStockpileManagerForm>().btnRemoveSelectedStockpile.Text = "  Remove Item";

            if (ghMode == GlitchHarvesterMode.CORRUPT)
            {
                btnCorrupt.Text = "   Corrupt";
            }
        }
    }

    public void Corrupt(object sender, EventArgs e)
    {
        if (sender != null)
        {
            if (!(btnCorrupt.Visible || AllSpec.VanguardSpec[VSPEC.REPLACE_MANUALBLAST_WITH_GHCORRUPT] != null && S.GET<CoreForm>().btnManualBlast.Visible))
            {
                return;
            }
        }

        try
        {
            SetBlastButtonVisibility(false);

            //Shut off autocorrupt if it's on.
            //Leave this check here so we don't wastefully update the spec
            if (S.GET<CoreForm>().AutoCorrupt)
            {
                S.GET<CoreForm>().AutoCorrupt = false;
            }

            JavaStashKey psk = JavaStockpileManagerUISide.CurrentSavestateStashKey;

            if (MergeMode)
            {
                List<JavaStashKey> sks = [];

                //Reverse before merging because DataGridView selectedrows is backwards for some odd reason
                IEnumerable<DataGridViewRow> reversed = S.GET<JavaStockpileManagerForm>().dgvStockpile.SelectedRows.Cast<DataGridViewRow>().Reverse();
                foreach (DataGridViewRow row in reversed)
                {
                    sks.Add((JavaStashKey)row.Cells[0].Value);
                }

                IsCorruptionApplied = JavaStockpileManagerUISide.MergeStashkeys(sks);

                S.GET<JavaStashHistoryForm>().RefreshStashHistorySelectLast();
                //lbStashHistory.TopIndex = lbStashHistory.Items.Count - 1;

                return;
            }

            if (ghMode == GlitchHarvesterMode.CORRUPT)
            {
                S.GET<JavaStashHistoryForm>().DontLoadSelectedStash = true;
                IsCorruptionApplied = JavaStockpileManagerUISide.Corrupt(loadBeforeOperation);
                S.GET<JavaStashHistoryForm>().RefreshStashHistorySelectLast();
            }
        }
        finally
        {
            SetBlastButtonVisibility(true);
        }
    }

    private void BlastRawStash()
    {/* TODO: something about this
        LocalNetCoreRouter.Route(NetCore.Endpoints.CorruptCore, NetCore.Commands.Basic.ManualBlast, true);
        SendRawToStash(null, null);*/
    }

    public void btnCorrupt_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            Point locate = e.GetMouseLocation(sender);

            ContextMenuStrip columnsMenu = new();
            columnsMenu.Items.Add("Blast + Send RAW To Stash", null, (_, _) =>
            {
                BlastRawStash();
            });
            columnsMenu.Show(this, locate);
            columnsMenu.Items.Add("Corrupt", null, (_, _) =>
            {
                ghModeStore = ghMode;
                ghMode = GlitchHarvesterMode.CORRUPT;
                S.GET<JavaGlitchHarvesterBlastForm>().Corrupt(sender, e);
                ghMode = ghModeStore;
                RedrawActionUI();

            });
            columnsMenu.Show(this, locate);
        }
    }

    public void BlastLayerToggle(object sender, EventArgs e)
    { //TODO: something about this
        /*if (JavaStockpileManagerUISide.CurrentStashkey?.BlastLayer?.Layer == null || JavaStockpileManagerUISide.CurrentStashkey?.BlastLayer?.Layer.Count == 0)
        {
            IsCorruptionApplied = false;
            return;
        }

        if (!IsCorruptionApplied)
        {
            IsCorruptionApplied = true;

            LocalNetCoreRouter.Route(NetCore.Endpoints.CorruptCore, NetCore.Commands.Remote.SetApplyCorruptBL, true);
        }
        else
        {
            IsCorruptionApplied = false;

            LocalNetCoreRouter.Route(NetCore.Endpoints.CorruptCore, NetCore.Commands.Remote.SetApplyUncorruptBL, true);
            LocalNetCoreRouter.Route(NetCore.Endpoints.CorruptCore, NetCore.Commands.Remote.ClearStepBlastUnits, null, true);
        }*/
    }

    public void RerollSelected(object sender, EventArgs e)
    {
        if (!btnRerollSelected.Visible)
        {
            return;
        }

        try
        {
            SetBlastButtonVisibility(false);

            if (S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex != -1)
            {
                JavaStockpileManagerUISide.CurrentStashkey = (JavaStashKey)JavaStockpileManagerUISide.StashHistory[S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex].Clone();
            }
            else if (S.GET<JavaStockpileManagerForm>().dgvStockpile.SelectedRows.Count != 0 && S.GET<JavaStockpileManagerForm>().GetSelectedStashKey() != null)
            {
                JavaStockpileManagerUISide.CurrentStashkey = (JavaStashKey)S.GET<JavaStockpileManagerForm>().GetSelectedStashKey()?.Clone();
                //StockpileManager_UISide.unsavedEdits = true;
            }
            else
            {
                return;
            }

            if (JavaStockpileManagerUISide.CurrentStashkey != null)
            {
                SerializedInsnBlastLayerCollection currentBl = JavaStockpileManagerUISide.CurrentStashkey.BlastLayer;
                //reroll on Emu Side always
                SerializedInsnBlastLayerCollection newBl = (SerializedInsnBlastLayerCollection)currentBl.Clone();
                
                JavaBlastTools.LoadClassesFromJar(JavaStockpileManagerUISide.CurrentStashkey.JarFilename);
                newBl.ReRoll();
                JavaStockpileManagerUISide.CurrentStashkey.BlastLayer = newBl;

                //JavaStockpileManagerUISide.CurrentStashkey.BlastLayer.Reroll();

                if (JavaStockpileManagerUISide.AddCurrentStashkeyToStash())
                {
                    S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
                    S.GET<JavaStashHistoryForm>()
                        .RefreshStashHistory();
                    S.GET<JavaStashHistoryForm>()
                        .lbStashHistory.ClearSelected();
                    S.GET<JavaStashHistoryForm>()
                        .DontLoadSelectedStash = true;
                    S.GET<JavaStashHistoryForm>()
                        .lbStashHistory.SelectedIndex = S.GET<JavaStashHistoryForm>()
                        .lbStashHistory.Items.Count - 1;
                }

                IsCorruptionApplied = JavaStockpileManagerUISide.ApplyStashkey(JavaStockpileManagerUISide.CurrentStashkey);
            }
        }
        finally
        {
            SetBlastButtonVisibility(true);
        }
    }

    public void SetBlastButtonVisibility(bool visible)
    {
        btnCorrupt.Visible = visible;
        btnRerollSelected.Visible = visible;

        if (AllSpec.VanguardSpec[VSPEC.REPLACE_MANUALBLAST_WITH_GHCORRUPT] != null)
        {
            S.GET<CoreForm>().btnManualBlast.Visible = visible;
        }
    }

    private void OpenGlitchHarvesterSettings(object sender, MouseEventArgs e)
    {
        Point locate = e.GetMouseLocation(sender);
        ContextMenuStrip ghSettingsMenu = new();

        ghSettingsMenu.Items.Add(new ToolStripLabel("Glitch Harvester Mode")
        {
            Font = new("Segoe UI", 12),
        });

        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Corrupt", null, (_, _) =>
        {
            ghMode = GlitchHarvesterMode.CORRUPT;
            RedrawActionUI();
        })).Checked = ghMode == GlitchHarvesterMode.CORRUPT;

        ghSettingsMenu.Items.Add(new ToolStripSeparator());

        ghSettingsMenu.Items.Add(new ToolStripLabel("Behaviors")
        {
            Font = new("Segoe UI", 12)
        });

        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Auto-Load State", null, (_, _) =>
        {
            loadBeforeOperation = loadBeforeOperation ^= true;
            RedrawActionUI();
        })).Checked = loadBeforeOperation;
        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Load on select", null, (_, _) =>
        {
            LoadOnSelect = LoadOnSelect ^= true;
            RedrawActionUI();
        })).Checked = LoadOnSelect;
        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Stash results", null, (_, _) =>
        {
            JavaStockpileManagerUISide.StashAfterOperation = JavaStockpileManagerUISide.StashAfterOperation ^= true;
            RedrawActionUI();
        })).Checked = JavaStockpileManagerUISide.StashAfterOperation;

        ghSettingsMenu.Show(this, locate);
    }

    private void btnNewBlastLayerEditor_Click(object sender, EventArgs e)
    {
        JavaBlastEditorForm.OpenBlastEditor();
    }
}

public enum GlitchHarvesterMode
{
    CORRUPT,
    INJECT,
    ORIGINAL,
    MERGE,
}