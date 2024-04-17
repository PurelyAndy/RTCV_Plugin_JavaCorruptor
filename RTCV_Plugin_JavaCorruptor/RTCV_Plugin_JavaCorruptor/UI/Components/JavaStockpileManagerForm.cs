using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using RTCV.Common;
using RTCV.Common.Objects;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.UI.Forms;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI.Components;

public partial class JavaStockpileManagerForm : ComponentForm, IBlockable
{
    private new void HandleMouseDown(object s, MouseEventArgs e) => typeof(ComponentForm).GetMethod("HandleMouseDown", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new object[] { s, e });
    private new void HandleFormClosing(object s, FormClosingEventArgs e) => typeof(ComponentForm).GetMethod("HandleFormClosing", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new object[] { s, e });

    private Color? _originalSaveButtonColor;
    private bool _unsavedEdits;
    private bool _shouldCompressStockpile = true;
    private bool _shouldIncludeReferencedFiles;
    public bool UnsavedEdits
    {
        get => _unsavedEdits;
        set
        {
            _unsavedEdits = value;

            if (_unsavedEdits && btnSaveStockpile.Enabled)
            {
                _originalSaveButtonColor ??= btnSaveStockpile.BackColor;

                btnSaveStockpile.BackColor = Color.Tomato;
            }
            else
            {
                if (_originalSaveButtonColor != null)
                {
                    btnSaveStockpile.BackColor = _originalSaveButtonColor.Value;
                }
            }
        }
    }
        
        

    public JavaStockpileManagerForm()
    {
        InitializeComponent();

        dgvStockpile.RowsAdded += (_, _) =>
        {
            RefreshNoteIcons();
        };
    }

    public void HandleCellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e == null || e.RowIndex == -1)
        {
            return;
        }

        try
        {
            dgvStockpile.Enabled = false;
            btnStockpileUP.Enabled = false;
            btnStockpileDOWN.Enabled = false;

            DataGridView senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                JavaStashKey sk = (JavaStashKey)senderGrid.Rows[e.RowIndex].Cells["Item"].Value;
                S.SET(new NoteEditorForm(sk, senderGrid.Rows[e.RowIndex].Cells["Note"]));
                S.GET<NoteEditorForm>().Show();

                return;
            }

            if (dgvStockpile.SelectedRows.Count == 0)
            {
                return;
            }

            JavaStockpileManagerUISide.CurrentStashkey = GetSelectedStashKey();

            List<JavaStashKey> keys = dgvStockpile.Rows.Cast<DataGridViewRow>().Select(x => (JavaStashKey)x.Cells[0].Value).ToList();
            if (!JavaStockpileManagerUISide.CheckAndFixMissingReference(JavaStockpileManagerUISide.CurrentStashkey, false, keys))
            {
                return;
            }

            if (!S.GET<JavaGlitchHarvesterBlastForm>().LoadOnSelect)
            {
                return;
            }

            // Merge Execution
            if (dgvStockpile.SelectedRows.Count > 1)
            {
                List<JavaStashKey> sks = new();

                foreach (DataGridViewRow row in dgvStockpile.SelectedRows)
                {
                    sks.Add((JavaStashKey)row.Cells[0].Value);
                }

                //Removing this check makes Merge behave properly in all cases:
                //Shift+select uses the topmost savestate of the selection
                //Ctrl+select uses the savestate from the first item that was selected
                //Using the 'Merge' button follows the rules above to determine which savestate to use

                //if (IsControlDown())
                //{
                //    sks.Reverse();
                //}

                sks.Reverse();

                JavaStockpileManagerUISide.MergeStashkeys(sks);

                /*if (Render.RenderAtLoad && S.GET<JavaGlitchHarvesterBlastForm>().loadBeforeOperation)
                {
                    Render.StartRender();
                }*/

                S.GET<JavaStashHistoryForm>().RefreshStashHistory();
                return;
            }

            S.GET<JavaGlitchHarvesterBlastForm>().OneTimeExecute(); // Calls ApplyStashkey
            //JavaStockpileManagerUISide.ApplyStashkey(JavaStockpileManagerUISide.CurrentStashkey); //TODO: see 2nd todo in JavaStockpileManagerUISide.LoadState();
        }
        finally
        {
            dgvStockpile.Enabled = true;
            btnStockpileUP.Enabled = true;
            btnStockpileDOWN.Enabled = true;
            S.GET<JavaStashHistoryForm>().btnAddStashToStockpile.Enabled = true;
        }

        S.GET<JavaGlitchHarvesterBlastForm>().RedrawActionUI();
    }
    private static bool IsControlDown()
    {
        return (ModifierKeys & Keys.Control) != 0;
    }

    private void HandleStockpileMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
            return;
        
        Point locate = new((sender as System.Windows.Forms.Control).Location.X + e.Location.X, (sender as System.Windows.Forms.Control).Location.Y + e.Location.Y);

        ContextMenuStrip columnsMenu = new();

        SerializedInsnBlastLayerCollection bl = null;

        if (dgvStockpile.SelectedRows.Count == 1)
            bl = GetSelectedStashKey().BlastLayer;

        if (bl != null)
            columnsMenu.Items.Add($"Layer Size: {bl.Layer?.Layer.Count ?? 0}", null).Enabled = false;


        ((ToolStripMenuItem)columnsMenu.Items.Add("Open Selected Item in Blast Editor", null, (ob, ev) =>
        {
            if (S.GET<JavaBlastEditorForm>() != null)
            {
                JavaStashKey sk = GetSelectedStashKey();
                JavaBlastEditorForm.OpenBlastEditor((JavaStashKey)sk.Clone());
            }
        })).Enabled = (dgvStockpile.SelectedRows.Count == 1);

        ((ToolStripMenuItem)columnsMenu.Items.Add("Sanitize", null, (ob, ev) =>
        {
            if (S.GET<BlastEditorForm>() != null)
            {
                JavaStashKey sk = GetSelectedStashKey();
                JavaSanitizeToolForm.OpenSanitizeTool((JavaStashKey)sk.Clone(),false);
            }
        })).Enabled = (dgvStockpile.SelectedRows.Count == 1);

        columnsMenu.Items.Add(new ToolStripSeparator());

        /*((ToolStripMenuItem)columnsMenu.Items.Add("Manual Inject", null, (ob, ev) => TODO: manual inject would be nice, but it's probably not entirely possible
            {
                JavaStashKey sk = GetSelectedStashKey();
                JavaStashKey newSk = (JavaStashKey)sk.Clone();

                bool IsCorrupted = JavaStockpileManagerUISide.ApplyStashkey(newSk, false, false);

                if (JavaStockpileManagerUISide.CurrentStashkey != null)
                    S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = IsCorrupted;
            })).Enabled = (dgvStockpile.SelectedRows.Count == 1);*/

        columnsMenu.Items.Add(new ToolStripSeparator());

        ((ToolStripMenuItem)columnsMenu.Items.Add("Rename selected item", null, (ob, ev) =>
        {
            if (dgvStockpile.SelectedRows.Count != 0)
            {
                if (RenameStashKey(GetSelectedStashKey()))
                {
                    JavaStockpileManagerUISide.StockpileChanged();
                    dgvStockpile.Refresh();
                    UnsavedEdits = true;
                }

                //lbStockpile.RefreshItemsReal();   
            }
        })).Enabled = (dgvStockpile.SelectedRows.Count == 1);

        /*((ToolStripMenuItem)columnsMenu.Items.Add("Merge Selected Stashkeys", null, (ob, ev) => TODO: merge stashkeys
            {
                List<JavaStashKey> sks = new List<JavaStashKey>();
                foreach (DataGridViewRow row in dgvStockpile.SelectedRows)
                {
                    sks.Add((JavaStashKey)row.Cells[0].Value);
                }

                JavaStockpileManagerUISide.MergeStashkeys(sks);
                S.GET<JavaStashHistoryForm>().RefreshStashHistory();
            })).Enabled = (dgvStockpile.SelectedRows.Count > 1);*/




        ((ToolStripMenuItem)columnsMenu.Items.Add("Replace associated JAR", null, (ob, ev) =>
        {
            List<JavaStashKey> sks = new();
            foreach (DataGridViewRow row in dgvStockpile.SelectedRows)
            {
                sks.Add((JavaStashKey)row.Cells[0].Value);
            }

            OpenFileDialog ofd = new()
            {
                DefaultExt = "*",
                Title = "Select Replacement File",
                Filter = "Java Executables|*.jar",
                RestoreDirectory = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string oldFilename = sks.First().JarFilename;
                foreach (JavaStashKey sk in sks.Where(x => x.JarFilename == oldFilename))
                {
                    sk.JarFilename = filename;
                    sk.JarShortFilename = Path.GetFileName(sk.JarFilename);
                }
            }
        })).Enabled = (dgvStockpile.SelectedRows.Count >= 1);

        columnsMenu.Items.Add(new ToolStripSeparator());

        ((ToolStripMenuItem)columnsMenu.Items.Add($"Duplicate selected item{(dgvStockpile.SelectedRows.Count > 1 ? "s" : "")}", null, (ob, ev) =>
        {
            DuplicateSelected();
        })).Enabled = (dgvStockpile.SelectedRows.Count > 0);

        ((ToolStripMenuItem)columnsMenu.Items.Add($"Remove selected item{(dgvStockpile.SelectedRows.Count > 1 ? "s" : "")}", null, (ob, ev) =>
        {
            RemoveSelected();
                    
        })).Enabled = dgvStockpile.SelectedRows.Count > 0;

        columnsMenu.Show(this, locate);
    }

    public void RefreshNoteIcons()
    {
        foreach (DataGridViewRow dataRow in dgvStockpile.Rows)
        {
            JavaStashKey sk = (JavaStashKey)dataRow.Cells["Item"].Value;
            if (sk == null)
            {
                continue;
            }

            dataRow.Cells["Note"].Value = string.IsNullOrWhiteSpace(sk.Note) ? "" : "📝";
        }
    }

    internal static bool RenameStashKey(JavaStashKey sk)
    {
        string value = sk.Alias;

        if (value == null)
            value = "";

        if (InputBox.ShowDialog("Renaming Stashkey", "Enter the new Stash name:", ref value) == DialogResult.OK && !string.IsNullOrWhiteSpace(value))
        {
            sk.Alias = value.Trim();
            return true;
        }

        return false;
    }


    private void RenamedSelected(object sender, EventArgs e)
    {
        if (!btnRenameSelected.Visible)
        {
            return;
        }

        if (dgvStockpile.SelectedRows.Count != 0)
        {
            if (RenameStashKey(GetSelectedStashKey()))
            {
                JavaStockpileManagerUISide.StockpileChanged();
                dgvStockpile.Refresh();
                UnsavedEdits = true;
            }

            //lbStockpile.RefreshItemsReal();
        }
    }

    private void RemoveSelectedStockpile(object sender, EventArgs e) => RemoveSelected();
    public void RemoveSelected()
    {
        if (ModifierKeys == Keys.Control || (dgvStockpile.SelectedRows.Count != 0 && (MessageBox.Show("Are you sure you want to remove the selected stockpile entries?", "Delete Stockpile Entry?", MessageBoxButtons.YesNo) == DialogResult.Yes)))
        {
            foreach (DataGridViewRow row in dgvStockpile.SelectedRows)
            {
                dgvStockpile.Rows.Remove(row);
            }
            JavaStockpileManagerUISide.StockpileChanged();
            UnsavedEdits = true;
        }
    }
    public void DuplicateSelected()
    {
        List<JavaStashKey> sks = new();
        foreach (DataGridViewRow row in dgvStockpile.SelectedRows)
        {
            sks.Add((JavaStashKey)((JavaStashKey)row.Cells[0].Value).Clone());
            sks.Last().Alias = (row.Cells[0].Value as JavaStashKey)?.Alias ?? sks.Last().Alias;
        }
        foreach (JavaStashKey sk in sks)
        {
            JavaStockpileManagerUISide.StashHistory.Add(sk);
            dgvStockpile.ClearSelection();
            JavaStockpileManagerUISide.CurrentStashkey = JavaStockpileManagerUISide.StashHistory[S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex];
        }
        JavaStockpileManagerUISide.StockpileChanged();
        UnsavedEdits = true;
    }
    private void ClearStockpile(object sender, EventArgs e) => ClearStockpile();
    public void ClearStockpile(bool force = false)
    {
        if (force || MessageBox.Show("Are you sure you want to clear the stockpile?", "Clearing stockpile", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            dgvStockpile.Rows.Clear();

            JavaStockpileManagerUISide.ClearCurrentStockpile();

            btnSaveStockpile.Enabled = false;
            UnsavedEdits = false;
        }
    }

    public async void LoadStockpile(string filename)
    {
        if (UnsavedEdits && MessageBox.Show("You have unsaved edits in your stockpile.\n\nAre you sure you want to load a new one without saving?",
                "Unsaved edits in Stockpile", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            return;
        }

        CanvasForm cgForm = CanvasForm.GetExtraForm("Custom Grid");
        try
        {
            //We do this here and invoke because our unlock runs at the end of the awaited method, but there's a chance an error occurs
            //Thus, we want this to happen within the try block
            UICore.SetHotkeyTimer(false);

            S.GET<SaveProgressForm>().Dock = DockStyle.Fill;
            cgForm?.OpenSubForm(S.GET<SaveProgressForm>());

            JavaStockpileManagerUISide.ClearCurrentStockpile();
            dgvStockpile.Rows.Clear();

            //S.GET<JavaStockpilePlayerForm>().dgvStockpile.Rows.Clear(); if i ever make a JavaStockpilePlayerForm
            OperationResults<JavaStockpile> r = await Task.Run(() => JavaStockpile.Load(filename));
            if (r.Failed)
                MessageBox.Show($"Loading the stockpile failed!\n{r.GetErrorsFormatted()}");
            else
            {
                JavaStockpile sks = r.Result;
                //Update the current stockpile to this one
                JavaStockpileManagerUISide.SetCurrentStockpile(sks);

                foreach (JavaStashKey key in sks.StashKeys)
                {
                    dgvStockpile?.Rows.Add(key, key.GameName, key.Note);
                }

                btnSaveStockpile.Enabled = true;
                RefreshNoteIcons();

                if (r.HasWarnings())
                {
                    MessageBox.Show("The stockpile gave the following warnings:\n" +
                                    $"{r.GetWarningsFormatted()}");
                }
            }

            dgvStockpile.ClearSelection();
            JavaStockpileManagerUISide.StockpileChanged();

            UnsavedEdits = false;
        }
        finally
        {
            cgForm?.CloseSubForm();
            UICore.SetHotkeyTimer(true);
            UICore.UnlockInterface();
        }
    }

    internal JavaStashKey GetSelectedStashKey() => (dgvStockpile.SelectedRows[0].Cells[0].Value as JavaStashKey);

    private async void ImportStockpile(string filename)
    {
        //temporary code

        CanvasForm cgForm = CanvasForm.GetExtraForm("Custom Grid");
        try {
            UICore.SetHotkeyTimer(false);
            UICore.LockInterface(false, true);
            S.GET<SaveProgressForm>().Dock = DockStyle.Fill;
            cgForm?.OpenSubForm(S.GET<SaveProgressForm>());

        OperationResults<JavaStockpile> r = await Task.Run(() => JavaStockpile.Import(filename));
            if (!r.Failed)
        {
            JavaStockpile sks = r.Result;
                RtcCore.OnProgressBarUpdate(sks, new ProgressBarEventArgs("Populating UI", 95));
            foreach (JavaStashKey key in sks.StashKeys)
            {
                dgvStockpile?.Rows.Add(key, key.GameName, key.Note);
            }

            UnsavedEdits = true;
                RtcCore.OnProgressBarUpdate(sks, new ProgressBarEventArgs("Done", 100));
        }
        RefreshNoteIcons();
        } finally {
            cgForm?.CloseSubForm();
            UICore.UnlockInterface();
            UICore.SetHotkeyTimer(true);
            RefreshNoteIcons();

        }
    }

    private async void SaveStockpile(JavaStockpile sks, string path)
    {
        //logger.Trace("Entering SaveStockpile {0}\n{1}", Thread.CurrentThread.ManagedThreadId, Environment.StackTrace);
        //temporary code
        CanvasForm cgForm = CanvasForm.GetExtraForm("Custom Grid");
        try {
            UICore.SetHotkeyTimer(false);
            UICore.LockInterface(false, true);
            S.GET<SaveProgressForm>().Dock = DockStyle.Fill;
            cgForm?.OpenSubForm(S.GET<SaveProgressForm>());

            bool r = await Task.Run(() => JavaStockpile.Save(sks, path, _shouldIncludeReferencedFiles, _shouldCompressStockpile));
            
        if (r)
        {
            JavaStockpileManagerUISide.SetCurrentStockpile(sks);
            sendCurrentStockpileToSKS();
            UnsavedEdits = false;
            btnSaveStockpile.Enabled = true;
        }
        } finally {
            cgForm?.CloseSubForm();
            UICore.UnlockInterface();
            UICore.SetHotkeyTimer(true);
        }
    }

    private void LoadStockpile(object sender, MouseEventArgs e)
    {
        //logger.Trace("Entering LoadStockpile {0}", Thread.CurrentThread.ManagedThreadId);
        //RtcCore.CheckForProblematicProcesses(); // not commented by me

        //Point locate = new Point(((Control)sender).Location.X + e.Location.X, ((Control)sender).Location.Y + e.Location.Y);

        //ContextMenuStrip loadMenuItems = new ContextMenuStrip();
        //loadMenuItems.Items.Add("Load Stockpile", null, (ob, ev) =>
        {
            string filename;
            OpenFileDialog ofd = new()
            {
                DefaultExt = "jsks",
                Title = "Open Stockpile File",
                Filter = "JSKS files|*.jsks",
                RestoreDirectory = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            else
            {
                return;
            }

            LoadStockpile(filename);
        }//);

        //loadMenuItems.Show(this, locate);
    }

    public void SaveStockpileAs(object sender, EventArgs e)
    {
        if (dgvStockpile.Rows.Count == 0)
        {
            MessageBox.Show("You cannot save the Stockpile because it is empty");
            return;
        }

        UICore.SetHotkeyTimer(false);
        string path;
        SaveFileDialog saveFileDialog1 = new()
        {
            DefaultExt = "jsks",
            Title = "Save Stockpile File",
            Filter = "JSKS files|*.jsks",
            RestoreDirectory = true,
        };

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            path = saveFileDialog1.FileName;
        }
        else
        {
            return;
        }

        JavaStockpile sks = new(dgvStockpile);
        SaveStockpile(sks, path);
    }

    private void SaveStockpile(object sender, EventArgs e)
    {
        JavaStockpile sks = new(dgvStockpile);
        SaveStockpile(sks, JavaStockpileManagerUISide.GetCurrentStockpilePath());
    }

    private void sendCurrentStockpileToSKS()
    {
        foreach (DataGridViewRow dataRow in dgvStockpile.Rows)
        {
            JavaStashKey sk = (JavaStashKey)dataRow.Cells["Item"].Value;
        }
    }

    private void MoveSelectedStockpileUp(object sender, EventArgs e)
    {
        DataGridViewRow[] selectedRows = dgvStockpile.SelectedRows.Cast<DataGridViewRow>().ToArray();
        foreach (DataGridViewRow row in selectedRows)
        {
            int pos = row.Index;
            dgvStockpile.Rows.RemoveAt(pos);

            if (pos == 0)
            {
                dgvStockpile.Rows.Add(row);
            }
            else
            {
                int newpos = pos - 1;
                dgvStockpile.Rows.Insert(newpos, row);
            }
        }
        dgvStockpile.ClearSelection();
        foreach (DataGridViewRow row in selectedRows) //I don't know. Blame DGV
        {
            row.Selected = true;
        }

        UnsavedEdits = true;

        JavaStockpileManagerUISide.StockpileChanged();
    }

    private void MoveSelectedStockpileDown(object sender, EventArgs e)
    {
        DataGridViewRow[] selectedRows = dgvStockpile.SelectedRows.Cast<DataGridViewRow>().ToArray();
        foreach (DataGridViewRow row in selectedRows)
        {
            int pos = row.Index;
            int count = dgvStockpile.Rows.Count;
            dgvStockpile.Rows.RemoveAt(pos);

            if (pos == count - 1)
            {
                int newpos = 0;
                dgvStockpile.Rows.Insert(newpos, row);
            }
            else
            {
                int newpos = pos + 1;
                dgvStockpile.Rows.Insert(newpos, row);
            }
        }
        dgvStockpile.ClearSelection();
        foreach (DataGridViewRow row in selectedRows) //I don't know. Blame DGV
        {
            row.Selected = true;
        }

        UnsavedEdits = true;

        JavaStockpileManagerUISide.StockpileChanged();
    }

    private void HandleDragDrop(object sender, DragEventArgs e)
    {
        bool alreadyLoadedAStockpile = false;

        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

        if (files == null)
            return;

        foreach (string f in files)
        {
            if (f.Contains(".jbl"))
            {
                SerializedInsnBlastLayerCollection bl;
                
                bl = JavaBlastTools.LoadBlastLayerFromFile(f);
                JavaStockpileManagerUISide.Import(bl);
                AddStashToStockpile(false,f);
            }
            else if (f.Contains(".sks"))
            {
                if (!alreadyLoadedAStockpile)
                {
                    LoadStockpile(f);
                    alreadyLoadedAStockpile = true;
                }
                else
                {
                    ImportStockpile(f);
                }
            }
        }
    }

    private bool AddStashToStockpile(bool askForName = true, string itemName = null)
    {
        string namee;
        string value = "";
            
        JavaStashKey sk = JavaStockpileManagerUISide.CurrentStashkey;

        if (askForName)
            if (InputBox.ShowDialog("Renaming Stashkey", "Enter the new Stash name:", ref value) == DialogResult.OK)
                namee = value.Trim();
            else
                return false;
        else
        {
            namee = JavaStockpileManagerUISide.CurrentStashkey.Alias;

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                if(itemName.Contains("\\"))
                {
                    //assume it's a full path
                    FileInfo fi = new(itemName);
                    namee = Path.GetFileNameWithoutExtension(fi.Name);
                }
                else
                    namee = itemName;
            }
        }

        JavaStockpileManagerUISide.CurrentStashkey.Alias = 
            string.IsNullOrWhiteSpace(namee) ? JavaStockpileManagerUISide.CurrentStashkey.Key : namee;

        DataGridViewRow dataRow = dgvStockpile.Rows[dgvStockpile.Rows.Add()];
        dataRow.Cells["Item"].Value = sk;
        dataRow.Cells["GameName"].Value = sk.GameName;

        RefreshNoteIcons();

        JavaStockpileManagerUISide.StashHistory.Remove(sk);

        int nRowIndex = dgvStockpile.Rows.Count - 1;

        dgvStockpile.ClearSelection();
        dgvStockpile.Rows[nRowIndex].Selected = true;

        JavaStockpileManagerUISide.StockpileChanged();

        UnsavedEdits = true;

        return true;
    }

    private void HandleDragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Link;
    }

    private void ImportStockpile(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new()
        {
            DefaultExt = "*",
            Title = "Select stockpile to import",
            Filter = "Any file|*.jsks",
            RestoreDirectory = true,
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            ImportStockpile(ofd.FileName);
        }
    }

    private void StockpileUp(object sender, EventArgs e)
    {
        if (dgvStockpile.SelectedRows.Count == 0)
        {
            return;
        }

        int currentSelectedIndex = dgvStockpile.SelectedRows[0].Index;

        if (currentSelectedIndex == 0)
        {
            dgvStockpile.ClearSelection();
            dgvStockpile.Rows[dgvStockpile.Rows.Count - 1].Selected = true;
        }
        else
        {
            dgvStockpile.ClearSelection();
            dgvStockpile.Rows[currentSelectedIndex - 1].Selected = true;
        }

        HandleCellClick(dgvStockpile, null);
    }

    private void StockpileDown(object sender, EventArgs e)
    {
        if (dgvStockpile.SelectedRows.Count == 0)
        {
            return;
        }

        int currentSelectedIndex = dgvStockpile.SelectedRows[0].Index;

        if (currentSelectedIndex == dgvStockpile.Rows.Count - 1)
        {
            dgvStockpile.ClearSelection();
            dgvStockpile.Rows[0].Selected = true;
        }
        else
        {
            dgvStockpile.ClearSelection();
            dgvStockpile.Rows[currentSelectedIndex + 1].Selected = true;
        }

        HandleCellClick(dgvStockpile, null);
    }

    private void OnFormLoad(object sender, EventArgs e)
    {
        dgvStockpile.AllowDrop = true;
        dgvStockpile.DragDrop += HandleDragDrop;
        dgvStockpile.DragEnter += HandleDragEnter;
    }

    private void HandleGlitchHarvesterSettingsMouseDown(object sender, MouseEventArgs e)
    {
        Point locate = e.GetMouseLocation(sender);
        ContextMenuStrip ghSettingsMenu = new();

        ghSettingsMenu.Items.Add(new ToolStripLabel("Stockpile Manager settings")
        {
            Font = new("Segoe UI", 12),
        });

        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Stockpile items: " + dgvStockpile.Rows.Cast<DataGridViewRow>().Count() , null, (ob, ev) =>
        {

        })).Enabled = false;

        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Compress Stockpiles", null, (ob, ev) =>
        {
            _shouldCompressStockpile = !_shouldCompressStockpile;
        })).Checked = _shouldCompressStockpile;

        ((ToolStripMenuItem)ghSettingsMenu.Items.Add("Include referenced files", null, (ob, ev) =>
        {
            _shouldIncludeReferencedFiles = !_shouldIncludeReferencedFiles;
        })).Checked = _shouldIncludeReferencedFiles;

        ghSettingsMenu.Items.Add(new ToolStripSeparator());

        (ghSettingsMenu.Items.Add("Show Item Name", null,
                (ob, ev) => { dgvStockpile.Columns["Item"].Visible ^= true; }) as ToolStripMenuItem).Checked =
            dgvStockpile.Columns["Item"].Visible;
        (ghSettingsMenu.Items.Add("Show Game Name", null,
                (ob, ev) => { dgvStockpile.Columns["GameName"].Visible ^= true; }) as ToolStripMenuItem)
            .Checked =
            dgvStockpile.Columns["GameName"].Visible;
        (ghSettingsMenu.Items.Add("Show Note", null, (ob, ev) => { dgvStockpile.Columns["Note"].Visible ^= true; })
            as ToolStripMenuItem).Checked = dgvStockpile.Columns["Note"].Visible;

        ghSettingsMenu.Show(this, locate);
    }

    private void dgvStockpile_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        throw new NotImplementedException();
    }
}