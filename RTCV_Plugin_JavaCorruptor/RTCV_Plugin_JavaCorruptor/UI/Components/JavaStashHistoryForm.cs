using System.IO;
using System.IO.Compression;
using System.Reflection;
using Java_Corruptor;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.UI;
using Java_Corruptor.UI.Components;
using RTCV.UI;

namespace Java_Corruptor.UI.Components;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.Common;
using RTCV.UI.Modular;

public partial class JavaStashHistoryForm : ComponentForm, IBlockable
{
    private new void HandleMouseDown(object s, MouseEventArgs e) => typeof(ComponentForm).GetMethod("HandleMouseDown", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this,
        [s, e]);
    private new void HandleFormClosing(object s, FormClosingEventArgs e) => typeof(ComponentForm).GetMethod("HandleFormClosing", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this,
        [s, e]);

    public bool DontLoadSelectedStash { get; set; } = false;

    public JavaStashHistoryForm()
    {
        InitializeComponent();

        lbStashHistory.DataSource = JavaStockpileManagerUISide.StashHistory;
    }

    private void OnDragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        foreach (var f in files)
        {
            if (f.Contains(".jbl"))
            {
                //JavaBlastLayer temp = BlastTools.LoadBlastLayerFromFile(f);
                SerializedInsnBlastLayerCollection bl;

                bl = JavaBlastTools.LoadBlastLayerFromFile(f);
                JavaStockpileManagerUISide.Import(bl);
                S.GET<JavaStashHistoryForm>().RefreshStashHistory();
            }
        }
    }

    private void OnDragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Link;
    }

    public void AddStashToStockpileButtonClick(object sender, EventArgs e) => AddStashToStockpileFromUI();
    public bool AddStashToStockpileFromUI()
    {
        if (JavaStockpileManagerUISide.CurrentStashkey != null && JavaStockpileManagerUISide.CurrentStashkey.Alias != JavaStockpileManagerUISide.CurrentStashkey.Key)
        {
            return AddStashToStockpile(false);
        }
        else
        {
            return AddStashToStockpile(true);
        }
            
    }

    public bool AddStashToStockpile(bool askForName = true, string itemName = null)
    {
        if (lbStashHistory.Items.Count == 0 || lbStashHistory.SelectedIndex == -1)
        {
            MessageBox.Show("Can't add the Stash to the Stockpile because none is selected in the Stash History");
            return false;
        }

        string Name = "";
        string value = "";

        JavaStashKey sk = (JavaStashKey)lbStashHistory.SelectedItem;
        JavaStockpileManagerUISide.CurrentStashkey = sk;

        if (askForName)
        {
            if (RTCV.UI.Forms.InputBox.ShowDialog("Renaming Stashkey", "Enter the new Stash name:", ref value) == DialogResult.OK)
            {
                Name = value.Trim();
            }
            else
            {
                return false;
            }
        }
        else
        {
            Name = JavaStockpileManagerUISide.CurrentStashkey.Alias;

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                if(itemName.Contains("\\"))
                {
                    //assume it's a full path
                    var fi = new System.IO.FileInfo(itemName);
                    Name = System.IO.Path.GetFileNameWithoutExtension(fi.Name);
                }
                else
                    Name = itemName;
            }
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            JavaStockpileManagerUISide.CurrentStashkey.Alias = JavaStockpileManagerUISide.CurrentStashkey.Key;
        }
        else
        {
            JavaStockpileManagerUISide.CurrentStashkey.Alias = Name;
        }

        DataGridViewRow dataRow = S.GET<JavaStockpileManagerForm>().dgvStockpile.Rows[S.GET<JavaStockpileManagerForm>().dgvStockpile.Rows.Add()];
        dataRow.Cells["Item"].Value = sk;
        dataRow.Cells["GameName"].Value = sk.GameName;

        S.GET<JavaStockpileManagerForm>().RefreshNoteIcons();

        JavaStockpileManagerUISide.StashHistory.Remove(sk);

        RefreshStashHistory();

        DontLoadSelectedStash = true;
        lbStashHistory.ClearSelected();
        DontLoadSelectedStash = false;

        int nRowIndex = S.GET<JavaStockpileManagerForm>().dgvStockpile.Rows.Count - 1;

        S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
        S.GET<JavaStockpileManagerForm>().dgvStockpile.Rows[nRowIndex].Selected = true;

        JavaStockpileManagerUISide.StockpileChanged();

        S.GET<JavaStockpileManagerForm>().UnsavedEdits = true;

        //Ensure it is redrawn to prevent weird issues such as the merge button not returning into the corrupt button
        S.GET<JavaGlitchHarvesterBlastForm>().RedrawActionUI();

        return true;
    }

    public void RefreshStashHistory(object sender = null, EventArgs e = null)
    {
        DontLoadSelectedStash = true;
        var lastSelect = lbStashHistory.SelectedIndex;

        DontLoadSelectedStash = true;
        lbStashHistory.DataSource = null;
        lbStashHistory.SelectedIndex = -1;

        DontLoadSelectedStash = true;
        //lbStashHistory.BeginUpdate();
        lbStashHistory.DataSource = JavaStockpileManagerUISide.StashHistory;
        //lbStashHistory.EndUpdate();

        DontLoadSelectedStash = true;
        if (lastSelect < lbStashHistory.Items.Count)
        {
            lbStashHistory.SelectedIndex = lastSelect;
        }

        DontLoadSelectedStash = false;
    }

    public void RemoveFirstStashHistoryItem()
    {
        DontLoadSelectedStash = true;
        lbStashHistory.DataSource = null;
        lbStashHistory.SelectedIndex = -1;

        DontLoadSelectedStash = true;
        //lbStashHistory.BeginUpdate();
        JavaStockpileManagerUISide.RemoveFirstStashItem();
        lbStashHistory.DataSource = JavaStockpileManagerUISide.StashHistory;
        DontLoadSelectedStash = false;
    }

    private void HandleStashHistoryMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
            return;
        
        Point locate = new(((Control)sender).Location.X + e.Location.X, ((Control)sender).Location.Y + e.Location.Y);

        ContextMenuStrip columnsMenu = new();

        SerializedInsnBlastLayerCollection bl = null;

        if(lbStashHistory.SelectedIndex != -1)
            bl = JavaStockpileManagerUISide.StashHistory[lbStashHistory.SelectedIndex].BlastLayer;

        if(bl != null)
            columnsMenu.Items.Add($"Layer Size: {bl.Layer?.Layer.Count ?? 0}", null).Enabled = false;

        ((ToolStripMenuItem)columnsMenu.Items.Add("Open Selected Item in Blast Editor", null, new((ob, ev) =>
        {
            if (S.GET<BlastEditorForm>() != null)
            {
                JavaStashKey sk = JavaStockpileManagerUISide.StashHistory[lbStashHistory.SelectedIndex];
                JavaBlastEditorForm.OpenBlastEditor((JavaStashKey)sk.Clone());
            }
        }))).Enabled = lbStashHistory.SelectedIndex != -1;

        ((ToolStripMenuItem)columnsMenu.Items.Add("Sanitize", null, (_, _) =>
        {
            if (S.GET<BlastEditorForm>() != null)
            {
                JavaStashKey sk = JavaStockpileManagerUISide.StashHistory[lbStashHistory.SelectedIndex];
                JavaSanitizeToolForm.OpenSanitizeTool((JavaStashKey)sk.Clone(), false);
            }
        })).Enabled = lbStashHistory.SelectedIndex != -1;

        columnsMenu.Items.Add(new ToolStripSeparator());

        ((ToolStripMenuItem)columnsMenu.Items.Add("Rename selected item", null, (_, _) =>
        {
            JavaStashKey sk = JavaStockpileManagerUISide.StashHistory[lbStashHistory.SelectedIndex];
            JavaStockpileManagerForm.RenameStashKey(sk);
            RefreshStashHistory();
        })).Enabled = lbStashHistory.SelectedIndex != -1;

        columnsMenu.Items.Add(new ToolStripSeparator());

        ((ToolStripMenuItem)columnsMenu.Items.Add("Merge Selected Stashkeys", null, (_, _) =>
        {
            List<JavaStashKey> sks = [];
            foreach (JavaStashKey sk in lbStashHistory.SelectedItems)
            {
                sks.Add(sk);
            }

            JavaStockpileManagerUISide.MergeStashkeys(sks);

            RefreshStashHistory();
        })).Enabled = (lbStashHistory.SelectedIndex != -1 && lbStashHistory.SelectedItems.Count > 1);

        columnsMenu.Show(this, locate);
    }

    public void RefreshStashHistorySelectLast()
    {
        RefreshStashHistory();
        DontLoadSelectedStash = true;
        lbStashHistory.ClearSelected();
        DontLoadSelectedStash = true;
        lbStashHistory.SelectedIndex = lbStashHistory.Items.Count - 1;
    }

    public void HandleStashHistorySelectionChange(object sender, EventArgs e)
    {
        try
        {
            lbStashHistory.Enabled = false;
            btnStashUP.Enabled = false;
            btnStashDOWN.Enabled = false;
            btnAddStashToStockpile.Enabled = false;

            if (DontLoadSelectedStash || lbStashHistory.SelectedIndex == -1)
            {
                DontLoadSelectedStash = false;
                return;
            }

            S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
            S.GET<StockpilePlayerForm>().dgvStockpile.ClearSelection();

            var blastForm = S.GET<JavaGlitchHarvesterBlastForm>();

            if (S.GET<JavaGlitchHarvesterBlastForm>().MergeMode)
            {
                blastForm.ghMode = GlitchHarvesterMode.CORRUPT;
                S.GET<JavaStockpileManagerForm>().btnRenameSelected.Visible = true;
                S.GET<JavaStockpileManagerForm>().btnRemoveSelectedStockpile.Text = "  Remove Item";

                blastForm.btnCorrupt.Text = blastForm.ghMode switch
                {
                    GlitchHarvesterMode.CORRUPT => "Corrupt",
                    GlitchHarvesterMode.INJECT => "Inject",
                    GlitchHarvesterMode.ORIGINAL => "Original",
                    _ => blastForm.btnCorrupt.Text,
                };
            }

            JavaStockpileManagerUISide.CurrentStashkey = JavaStockpileManagerUISide.StashHistory[lbStashHistory.SelectedIndex];

            if (!blastForm.LoadOnSelect)
            {
                return;
            }

            blastForm.OneTimeExecute();
        }
        finally
        {
            lbStashHistory.Enabled = true;
            btnStashUP.Enabled = true;
            btnStashDOWN.Enabled = true;
            btnAddStashToStockpile.Enabled = true;
            //((Control)sender).Focus();
            S.GET<JavaGlitchHarvesterBlastForm>().RedrawActionUI();
        }
    }

    private void ClearSelectedSKs(object sender, MouseEventArgs e)
    {
        DontLoadSelectedStash = true;
        lbStashHistory.ClearSelected();
        DontLoadSelectedStash = true;
        S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
        S.GET<JavaGlitchHarvesterBlastForm>().RedrawActionUI();
    }

    private void ClearStashHistory(object sender, EventArgs e)
    {
        if (MessageBox.Show("Are you sure you want to clear the stash?", "Clear stash?", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            JavaStockpileManagerUISide.StashHistory.Clear();
            RefreshStashHistory();

            //Force clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public void MoveSelectedStashUp(object sender, EventArgs e)
    {
        if (lbStashHistory.SelectedIndex == -1)
        {
            return;
        }

        if (lbStashHistory.SelectedIndex == 0)
        {
            lbStashHistory.ClearSelected();
            lbStashHistory.SelectedIndex = lbStashHistory.Items.Count - 1;
        }
        else
        {
            int newPos = lbStashHistory.SelectedIndex - 1;
            lbStashHistory.ClearSelected();
            lbStashHistory.SelectedIndex = newPos;
        }
    }

    public void MoveSelectedStashDown(object sender, EventArgs e)
    {
        if (lbStashHistory.SelectedIndex == -1)
        {
            return;
        }

        if (lbStashHistory.SelectedIndex == lbStashHistory.Items.Count - 1)
        {
            lbStashHistory.ClearSelected();
            lbStashHistory.SelectedIndex = 0;
        }
        else
        {
            int newPos = lbStashHistory.SelectedIndex + 1;
            lbStashHistory.ClearSelected();
            lbStashHistory.SelectedIndex = newPos;
        }
    }
}