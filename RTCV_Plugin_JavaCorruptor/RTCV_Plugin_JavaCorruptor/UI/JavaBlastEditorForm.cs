/*
 * The DataGridView is bound to the blastlayer
 * All validation is done within the dgv
 * The boxes at the bottom are unbound and manipulate the selected rows in the dgv, and thus, the validation is handled by the dgv
 * No maxmimum is set in the numericupdowns at the bottom as the dgv validates
 */

/*
Applies in all cases & should be editable
 * bool IsEnabled
 * bool IsLocked
 *
 * string Method
 * long Index
 * int Replaces
 * string Value
 * 
 * string Note
*/

using System.Diagnostics;
using System.IO.Compression;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.UI.Components;
using Java_Corruptor.UI.Components.EngineControls;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.UI;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using RTCV.CorruptCore;
using RTCV.CorruptCore.Extensions;
using RTCV.NetCore;
using RTCV.Common;
using RTCV.UI.Components;
using global::Java_Corruptor;
using SlimDX.XACT3;

#pragma warning disable CA2213 //Component designer classes generate their own Dispose method
public partial class JavaBlastEditorForm : ColorizedForm
{
    private const int ButtonFillWeight = 20;
    private const int CheckBoxFillWeight = 25;
    private const int ComboBoxFillWeight = 40;
    private const int TextBoxFillWeight = 30;
    private const int NumericUpDownFillWeight = 35;

    //private string[] _methods = null;
    private List<string> VisibleColumns { get; set; }
    private string _currentBlastLayerFile = "";
    private bool _batchOperation;
    private ContextMenuStrip _headerStrip;
    private ContextMenuStrip _cms;
    private Dictionary<string, Control> _property2ControlDict;
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private enum BuProperty
    {
        isEnabled,
        isLocked,
        Method,
        Index,
        Replaces,
        ValueString,
        Note,
    }
    //We gotta cache this stuff outside of the scope of InitializeDGV
    //    private object actionTimeValues =

    public JavaBlastEditorForm()
    {
        try
        {
            InitializeComponent();

            dgvBlastEditor.AutoGenerateColumns = false;

            //On today's episode of "why is the designer overriding these values every time I build"
            upDownReplaces.Maximum = 16348; //Textbox doesn't like more than ~20k
            upDownIndex.Maximum = int.MaxValue;
            dontShowBlastlayerNameInTitleToolStripMenuItem.Checked = Params.IsParamSet("DONT_SHOW_BLASTLAYER_NAME_IN_EDITOR");
        }
        catch (Exception ex)
        {
            if (CloudDebug.ShowErrorDialog(ex, true) == DialogResult.Abort)
            {
                throw new AbortEverythingException();
            }
        }
    }

    private void OnFormDragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        foreach (string f in files)
        {
            if (!f.Contains(".jbl"))
                continue;
            SerializedInsnBlastLayerCollection temp = JavaBlastTools.LoadBlastLayerFromFile(f);
            ImportBlastLayer(temp.Layer);
        }
    }

    private void OnFormDragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Link;
    }

    internal static bool OpenBlastEditor(JavaStashKey sk = null, bool silent = false)
    {
        if (S.GET<JavaBlastEditorForm>().Visible)
        {
            silent = false;
        }

        //S.GET<JavaBlastEditorForm>().Close();
        JavaBlastEditorForm bef = new();
        
        S.SET(bef);

        sk ??= new();

        //If the blastlayer is big, prompt them before opening it. Let's go with 500 for now.

        switch (sk.BlastLayer.Layer.Layer.Count)
        {
            //TODO
            //to-do WHAT
            case > 500 when (DialogResult.Yes == MessageBox.Show($"You're trying to open a BlastLayer of size {sk.BlastLayer.Layer.Layer.Count}. This could take a while. Are you sure you want to continue?", "Opening a large BlastLayer", MessageBoxButtons.YesNo)):
                bef.LoadStashKey(sk, silent);
                return true;
            case <= 500:
                bef.LoadStashKey(sk, silent);
                return true;
            default:
                return false;
        }
    }

    private void OnFormLoad(object sender, EventArgs e)
    {
        //_methods = MemoryDomains.MemoryInterfaces?.Keys?.Concat(MemoryDomains.VmdPool.Values.Select(it => it.ToString())).ToArray();

        dgvBlastEditor.AllowUserToOrderColumns = true;
        SetDisplayOrder();
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e) => SaveDisplayOrder();

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
        //Clean up
        _bs = null;
        _bs2 = null;
        CurrentSk = null;
        _originalSk = null;
        //Force cleanup
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Dispose();
    }

    private void SetDisplayOrder()
    {
        if (!Params.IsParamSet("BLASTEDITOR_COLUMN_ORDER"))
        {
            return;
        }
        //Names split with commas
        string s = Params.ReadParam("BLASTEDITOR_COLUMN_ORDER");
        string[] order = s.Split(',');

        //Use a foreach and keep track in-case the number of entries changes
        int i = 0;
        foreach (string c in order)
        {
            if (dgvBlastEditor.Columns.Cast<DataGridViewColumn>().All(x => x.Name != c))
                continue;
            
            dgvBlastEditor.Columns[c]!.DisplayIndex = i;
            i++;
        }
    }

    private void SaveDisplayOrder()
    {
        IOrderedEnumerable<DataGridViewColumn> cols = dgvBlastEditor.Columns.Cast<DataGridViewColumn>().OrderBy(x => x.DisplayIndex);
        StringBuilder sb = new();
        foreach (DataGridViewColumn c in cols)
        {
            sb.Append(c.Name + ",");
        }

        Params.SetParam("BLASTEDITOR_COLUMN_ORDER", sb.ToString());
    }

    private void OnBlastEditorMouseClick(object sender, MouseEventArgs e)
    {
        //Exit edit mode if you click away from a cell
        DataGridView.HitTestInfo ht = dgvBlastEditor.HitTest(e.X, e.Y);

        if (ht.Type != DataGridViewHitTestType.Cell)
        {
            dgvBlastEditor.EndEdit();
        }
    }

    private void OnBlastEditorCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        // Note handling
        if (e != null && e.RowIndex != -1 &&
            e.ColumnIndex == dgvBlastEditor.Columns[BuProperty.Note.ToString()]?.Index &&
            dgvBlastEditor.Rows[e.RowIndex].DataBoundItem is SerializedInsnBlastUnit bu)
        {
            S.SET(new NoteEditorForm(bu, dgvBlastEditor[e.ColumnIndex, e.RowIndex]));
            S.GET<NoteEditorForm>().Show();
        }

        if (e.Button == MouseButtons.Left)
        {
            if (e.RowIndex != -1) 
                return;
            
            dgvBlastEditor.EndEdit();
            dgvBlastEditor.ClearSelection();
        }
        else if (e.Button == MouseButtons.Right)
        {
            //End the edit if they're right clicking somewhere else
            if (dgvBlastEditor.CurrentCell != null && dgvBlastEditor.CurrentCell.ColumnIndex != e.ColumnIndex)
            {
                dgvBlastEditor.EndEdit();
            }

            _cms = new();

            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            
            PopulateGenericContextMenu();
            
            if (dgvBlastEditor.Columns[e.ColumnIndex] == dgvBlastEditor.Columns[BuProperty.ValueString.ToString()])
            {
                _cms.Items.Add(new ToolStripSeparator());
                PopulateValueStringContextMenu(dgvBlastEditor[e.ColumnIndex, e.RowIndex]);
            }
            _cms.Show(dgvBlastEditor, dgvBlastEditor.PointToClient(Cursor.Position));
        }
    }
    
    private void PopulateValueStringContextMenu(DataGridViewCell cell)
    {
        ((ToolStripMenuItem)_cms.Items.Add("Copy Value", null, (_, _) =>
        {
            if (cell.Value is not string value)
            {
                return;
            }
            Clipboard.SetText(value);
        })).Enabled = true;
    }

    private void OnBlastEditorCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            dgvBlastEditor.BeginEdit(false);
        }
    }

    private void PopulateGenericContextMenu()
    {
        ((ToolStripMenuItem)_cms.Items.Add("Re-roll Selected Row(s)", null, (_, _) =>
        {
            //generate temporary blastlayer for batch processing
            List<SerializedInsnBlastUnit> layer = [];
            foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows)
            {
                SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)row.DataBoundItem;
                layer.Add(bu);
            }
            SerializedInsnBlastLayer tempBl = new(layer);

            SerializedInsnBlastLayer rerolledBl = (SerializedInsnBlastLayer)tempBl.Clone();
            JavaBlastTools.LoadClassesFromJar(CurrentSk.JarFilename);
            rerolledBl.ReRoll();

            //update JavaBlastUnit with new data
            for (int i = 0; i < rerolledBl.Layer.Count; i++)
            {
                SerializedInsnBlastUnit bu = tempBl.Layer[i];
                SerializedInsnBlastUnit newBu = rerolledBl.Layer[i];

                bu.Instructions = newBu.Instructions;
                bu.Replaces = newBu.Replaces;
            }

            dgvBlastEditor.Refresh();
            UpdateBottom();
        })).Enabled = true;

        ((ToolStripMenuItem)_cms.Items.Add("Modify Re-roll Settings", null, (_, _) =>
        {
            SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)dgvBlastEditor.SelectedRows[0].DataBoundItem;

            if (bu.Engine == "JavaVectorEngineControl")
                bu.Engine = "BasicEngineControl";
            Type engineType = Type.GetType("Java_Corruptor.UI.Components.EngineControls." + bu.Engine);
            bool engineWasNull = false;
            if (engineType is null)
            {
                engineWasNull = true;
                bu.Engine = "ArithmeticEngineControl";
            }
            JavaEngineControl engine = (JavaEngineControl)Activator.CreateInstance(engineType);
            if (engineWasNull)
                bu.EngineSettings = engine.EngineSettings;

            engine.DisableComboBox();
            
            ColorizedForm f = new();
            engine.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            engine.Location = new(0, 0);
            f.Text = "Re-roll Settings";
            f.ClientSize = new(442, 217);
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            f.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            f.Tag = "color:dark1";
            f.Controls.Add(engine);
            Button ok = new();
            ok.FlatStyle = FlatStyle.Flat;
            ok.FlatAppearance.BorderSize = 0;
            ok.Location = new(390, 190);
            ok.Size = new(35, 24);
            ok.Text = "OK";
            ok.Tag = "color:dark2";
            ok.ForeColor = System.Drawing.Color.White;
            ok.Click += (_, _) => { f.DialogResult = DialogResult.OK; f.Close(); };
            f.Controls.Add(ok);
            f.AcceptButton = ok;
            Button cancel = new();
            cancel.FlatStyle = FlatStyle.Flat;
            cancel.FlatAppearance.BorderSize = 0;
            cancel.Location = new(334, 190);
            cancel.Size = new(50, 24);
            cancel.Text = "Cancel";
            cancel.Tag = "color:dark2";
            cancel.ForeColor = System.Drawing.Color.White;
            cancel.Click += (_, _) => { f.DialogResult = DialogResult.Cancel; f.Close(); };
            f.Controls.Add(cancel);
            f.CancelButton = cancel;
            
            engine.EngineSettings = bu.EngineSettings;
            engine.UpdateUI();
            
            if (f.ShowDialog() == DialogResult.OK)
            {
                engine.Prepare();
                bu.EngineSettings = engine.EngineSettings;
            }

            dgvBlastEditor.Refresh();
            UpdateBottom();
        })).Enabled = dgvBlastEditor.SelectedRows.Count == 1;
        
        ((ToolStripMenuItem)_cms.Items.Add("Disassemble this unit's method", null, (_, _) =>
        {
            SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)dgvBlastEditor.SelectedRows[0].DataBoundItem;
            
            if (S.GET<DisassemblerForm>().IsDisposed)
            {
                S.SET(new DisassemblerForm());
            }
            S.GET<DisassemblerForm>().Show();
            S.GET<DisassemblerForm>().BringToFront();
            S.GET<DisassemblerForm>().OpenMethod(bu.Method, bu.Index, bu.Replaces);
        })).Enabled = true;

        //TODO: break down support
        /*((ToolStripMenuItem)cms.Items.Add("Break Down Selected Unit(s)", null, (_, _) =>
        {
            BreakDownUnits(true);
        })).Enabled = dgvBlastEditor.SelectedRows.Count > 0;*/
    }
        
    //TODO: break down support
    public void BreakDownUnits(bool breakSelected = false)
    {
        /*List<DataGridViewRow> targetRows;

        if (breakSelected)
        {
            targetRows = dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().ToList();
        }
        else
        {
            targetRows = dgvBlastEditor.Rows.Cast<DataGridViewRow>().ToList();
        }

        //Important we ToArray() this or else the ienumerable will become invalidated
        var blastUnits = targetRows.Select(x => (SerializedInsnBlastUnit)x.DataBoundItem).ToArray();

        dgvBlastEditor.DataSource = null;
        batchOperation = true;

        foreach (var bu in blastUnits)
        {
            SerializedInsnBlastUnit[] brokenUnits = bu.GetBreakdown();

            if (brokenUnits == null || brokenUnits.Length < 2)
            {
                continue;
            }

            foreach (JavaBlastUnit unit in brokenUnits)
            {
                bs.Add(unit);
            }
        }

        bs = new BindingSource { DataSource = new SortableBindingList<JavaBlastUnit>(currentSK.BlastLayer) };
        _siblc = CurrentSk!.BlastLayer;
        batchOperation = false;
        dgvBlastEditor.DataSource = bs;
        updateMaximum(this, dgvBlastEditor.Rows.Cast<DataGridViewRow>().ToList());
        dgvBlastEditor.Refresh();
        UpdateBottom();*/
    }

    private void PopulateMethodContextMenu(DataGridViewCell cell)
    {
    }

    private void OnBlastEditorCellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        DataGridViewColumn changedColumn = dgvBlastEditor.Columns[e.ColumnIndex];
        
        //If the Domain or SourceDomain changed update the Maximum Value
        //TODO: this should instead update the maximum index based on the size of the method
        /*if (changedColumn.Name == BuProperty.Domain.ToString())
        {
            updateMaximum(this, dgvBlastEditor.Rows[e.RowIndex].Cells[BuProperty.Address.ToString()] as DataGridViewNumericUpDownCell, dgvBlastEditor.Rows[e.RowIndex].Cells[BuProperty.Domain.ToString()].Value.ToString());
        }*/
        
        // check if the cell changed is ValueString
        if (changedColumn.Name == BuProperty.ValueString.ToString())
        {
            // get the value of the cell
            string value = dgvBlastEditor.Rows[e.RowIndex].Cells[BuProperty.ValueString.ToString()].Value.ToString();
            // get the blastunit
            SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)dgvBlastEditor.Rows[e.RowIndex].DataBoundItem;
            // set the value of the blastunit
            bu.ValueString = value;
        }
        
        UpdateBottom();
    }

    private void OnBlastEditorCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        DataGridViewColumn changedColumn = dgvBlastEditor.Columns[e.ColumnIndex];
        
        if (changedColumn.Name == BuProperty.Method.ToString())
        {
            DataGridViewRow row = dgvBlastEditor.Rows[e.RowIndex];
            SerializedInsnBlastUnit sibu = (SerializedInsnBlastUnit)row.DataBoundItem;
            string oldValue = row.Cells[BuProperty.Method.ToString()].Value.ToString();
            string newValue = (string)e.FormattedValue;
            if (oldValue == newValue)
            {
                _siblc.RepairMap();
            }
            
            int index = _siblc.IndexOf(sibu);
            _siblc.Remove(sibu);
            sibu.Method = newValue;
            _siblc.Insert(index, sibu);
            row.Cells[BuProperty.Method.ToString()].Value = newValue;
        }
    }

    private void OnValueValidated(object sender, EventArgs e)
    {
        string value = tbValue.Text;
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Where(x => (x.DataBoundItem as SerializedInsnBlastUnit)?.IsLocked == false))
        {
            row.Cells[BuProperty.ValueString.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnIndexValidated(object sender, EventArgs e)
    {
        decimal value = upDownIndex.Value;
        if (value > int.MaxValue)
        {
            value = int.MaxValue;
        }

        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Where(x => (x.DataBoundItem as SerializedInsnBlastUnit)?.IsLocked == false))
        {
            row.Cells[BuProperty.Index.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnReplacesValidated(object sender, EventArgs e)
    {
        decimal value = upDownReplaces.Value;
        if (value > int.MaxValue)
        {
            value = int.MaxValue;
        }

        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Where(x => (x.DataBoundItem as SerializedInsnBlastUnit)?.IsLocked == false))
        {
            row.Cells[BuProperty.Replaces.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnLockedValidated(object sender, EventArgs e)
    {
        bool value = cbLocked.Checked;
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows)
        {
            row.Cells[BuProperty.isLocked.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnEnabledValidated(object sender, EventArgs e)
    {
        bool value = cbEnabled.Checked;
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Where(x => (x.DataBoundItem as SerializedInsnBlastUnit)?.IsLocked == false))
        {
            row.Cells[BuProperty.isEnabled.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnMethodValidated(object sender, EventArgs e)
    {
        string value = tbMethod.Text;

        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Where(x => (x.DataBoundItem as SerializedInsnBlastUnit)?.IsLocked == false))
        {
            SerializedInsnBlastUnit sibu = (SerializedInsnBlastUnit)row.DataBoundItem;
            int index = _siblc.IndexOf(sibu);
            _siblc.Remove(sibu);
            sibu.Method = value;
            _siblc.Insert(index, sibu);
            row.Cells[BuProperty.Method.ToString()].Value = value;
        }

        UpdateBottom();
    }

    private void OnBlastEditorColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            _headerStrip = new();
            _headerStrip.Items.Add("Select columns to show", null, (_, _) =>
            {
                ColumnSelector cs = new();
                cs.LoadColumnSelector(dgvBlastEditor.Columns);
            });

            _headerStrip.Show(MousePosition);
        }

        RefreshAllNoteIcons();
    }

    //TODO: this should instead update the maximum index based on the size of the method
    /*private static void updateMaximum(JavaBlastEditorForm bef, List<DataGridViewRow> rows)
    {
        foreach (DataGridViewRow row in rows)
        {
            var bu = row.DataBoundItem as JavaBlastUnit;
            var domain = bu.Domain;
            var sourceDomain = bu.SourceDomain;

            if (domain != null && bef.DomainToMemInterfaceDict.ContainsKey(bu.Domain ?? ""))
            {
                (row.Cells[BuProperty.Address.ToString()] as DataGridViewNumericUpDownCell).Maximum = DomainToMemInterfaceDict[domain].Size - 1;
            }

            if (sourceDomain != null && bef.DomainToMemInterfaceDict.ContainsKey(bu.SourceDomain ?? ""))
            {
                (row.Cells[BuProperty.SourceAddress.ToString()] as DataGridViewNumericUpDownCell).Maximum = DomainToMemInterfaceDict[sourceDomain].Size - 1;
            }
        }
    }*/

    //TODO: this should instead update the maximum index based on the size of the method
    /*private static void updateMaximum(JavaBlastEditorForm bef, DataGridViewNumericUpDownCell cell, string domain)
    {
        if (DomainToMemInterfaceDict.ContainsKey(domain))
        {
            cell.Maximum = bef.DomainToMemInterfaceDict[domain].Size - 1;
        }
        else
        {
            cell.Maximum = int.MaxValue;
        }
    }*/

    private void UpdateBottom()
    {
        if (dgvBlastEditor.SelectedRows.Count <= 0)
            return;
        
        DataGridViewRow lastRow = dgvBlastEditor.SelectedRows[^1];
        SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)lastRow.DataBoundItem;

        //TODO: this should instead update the maximum index based on the size of the method
        /*if (DomainToMemInterfaceDict.ContainsKey(bu.Method ?? string.Empty))
            {
                upDownIndex.Maximum = DomainToMemInterfaceDict[bu.Method].Size - 1;
            }
            else
            {
                upDownIndex.Maximum = int.MaxValue;
            }

            if (DomainToMemInterfaceDict.ContainsKey(bu.SourceDomain ?? string.Empty))
            {
                upDownSourceAddress.Maximum = DomainToMemInterfaceDict[bu.SourceDomain].Size - 1;
            }
            else
            {
                upDownSourceAddress.Maximum = int.MaxValue;
            }*/

        tbMethod.Text = bu.Method;
        cbEnabled.Checked = bu.IsEnabled;
        cbLocked.Checked = bu.IsLocked;

        upDownIndex.Value = bu.Index;
        upDownReplaces.Value = bu.Replaces;
        tbValue.Text = bu.ValueString;
    }

    private void OnBlastEditorSelectionChange(object sender, EventArgs e)
    {
        UpdateBottom();

        List<DataGridViewRow> col = [];
        //For some reason DataGridViewRowCollection and DataGridViewSelectedRowCollection aren't directly compatible???
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows)
        {
            col.Add(row);
        }

        //Rather than setting all these values at load, we set it on the fly
        //updateMaximum(this, col); TODO: this should instead update the maximum index based on the size of the method
    }

    private void OnBlastEditorCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        //Bug in DGV. If you don't read the value back, it goes into edit mode on first click if you read the selectedrow within SelectionChanged. Why? No idea.
        _ = dgvBlastEditor.Rows[e.RowIndex].Cells[0].Value;
    }

    private void OnFilterTextChanged(object sender, EventArgs e)
    {
        if (tbFilter.Text.Length == 0)
        {
            dgvBlastEditor.DataSource = _bs;
            _bs2 = null;
            RefreshAllNoteIcons();
            return;
        }

        string value = ((ComboBoxItem<string>)cbFilterColumn?.SelectedItem)?.Value;
        if (value == null)
        {
            return;
        }

        _bs2 = new();
        _bs2.DataSource = CurrentSk.BlastLayer.Layer.Layer.Where(x => //RTC FIX: don't error if the filter text is longer than the property's value, also change behavior to search the whole string rather than just matching from the beginning
        {
            object o = x?.GetType().GetProperty(value)?.GetValue(x);
            return o != null && o.ToString().IndexOf(tbFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }).ToList();
        
        dgvBlastEditor.DataSource = _bs2;
        RefreshAllNoteIcons();
    }

    private void InitializeBottom()
    {
        _property2ControlDict = new();
        tbMethod.BindingContext = [];

        _property2ControlDict.Add(BuProperty.Index.ToString(), upDownIndex);
        _property2ControlDict.Add(BuProperty.Method.ToString(), tbMethod);
        _property2ControlDict.Add(BuProperty.isEnabled.ToString(), cbEnabled);
        _property2ControlDict.Add(BuProperty.isLocked.ToString(), cbLocked);
        _property2ControlDict.Add(BuProperty.Note.ToString(), btnNote);
        _property2ControlDict.Add(BuProperty.Replaces.ToString(), upDownReplaces);
        _property2ControlDict.Add(BuProperty.ValueString.ToString(), tbValue);
    }

    private void InitializeDGV()
    {
        VisibleColumns = [];

        DataGridViewColumn enabled = CreateColumn(BuProperty.isEnabled.ToString(), BuProperty.isEnabled.ToString(), "Enabled",
            new DataGridViewCheckBoxColumn());
        enabled.SortMode = DataGridViewColumnSortMode.Automatic;
        dgvBlastEditor.Columns.Add(enabled);

        DataGridViewColumn locked = CreateColumn(BuProperty.isLocked.ToString(), BuProperty.isLocked.ToString(), "Locked",
            new DataGridViewCheckBoxColumn());
        locked.SortMode = DataGridViewColumnSortMode.Automatic;
        dgvBlastEditor.Columns.Add(locked);

        DataGridViewTextBoxColumn method = CreateColumn(BuProperty.Method.ToString(), BuProperty.Method.ToString(), "Method", new DataGridViewTextBoxColumn()) as DataGridViewTextBoxColumn;//new DataGridViewComboBoxColumn()) as DataGridViewComboBoxColumn;
        //TODO: make methods a combobox rather than a textbox (maybe)
        //method.DataSource = _methods;
        method!.SortMode = DataGridViewColumnSortMode.Automatic;
        dgvBlastEditor.Columns.Add(method);

        DataGridViewNumericUpDownColumn index = (DataGridViewNumericUpDownColumn)CreateColumn(BuProperty.Index.ToString(), BuProperty.Index.ToString(), "Index", new DataGridViewNumericUpDownColumn());
        index.Hexadecimal = false;
        index.SortMode = DataGridViewColumnSortMode.Automatic;
        index.Increment = 1;
        index.Maximum = int.MaxValue;
        dgvBlastEditor.Columns.Add(index);

        DataGridViewNumericUpDownColumn replaces = (DataGridViewNumericUpDownColumn)CreateColumn(BuProperty.Replaces.ToString(), BuProperty.Replaces.ToString(), "Replaces", new DataGridViewNumericUpDownColumn());
        replaces.Minimum = 0;
        replaces.Maximum = int.MaxValue;
        replaces.SortMode = DataGridViewColumnSortMode.Automatic;
        dgvBlastEditor.Columns.Add(replaces);

        DataGridViewColumn valuestring = CreateColumn(BuProperty.ValueString.ToString(), BuProperty.ValueString.ToString(), "Value", new DataGridViewTextBoxColumn());
        valuestring.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        valuestring.SortMode = DataGridViewColumnSortMode.Automatic;
        ((DataGridViewTextBoxColumn)valuestring).MaxInputLength = 16348; //textbox doesn't like larger than ~20k
        dgvBlastEditor.Columns.Add(valuestring);

        dgvBlastEditor.Columns.Add(CreateColumn("", BuProperty.Note.ToString(), "Note", new DataGridViewButtonColumn()));

        if (Params.IsParamSet("BLASTEDITOR_VISIBLECOLUMNS"))
        {
            string str = Params.ReadParam("BLASTEDITOR_VISIBLECOLUMNS");
            string[] columns = str.Split(',');
            foreach (string column in columns)
            {
                VisibleColumns.Add(column);
            }
        }
        else
        {
            VisibleColumns.Add(BuProperty.isEnabled.ToString());
            VisibleColumns.Add(BuProperty.isLocked.ToString());
            VisibleColumns.Add(BuProperty.Method.ToString());
            VisibleColumns.Add(BuProperty.Index.ToString());
            VisibleColumns.Add(BuProperty.Replaces.ToString());
            VisibleColumns.Add(BuProperty.ValueString.ToString());
            VisibleColumns.Add(BuProperty.Note.ToString());
        }

        RefreshVisibleColumns();

        PopulateFilterCombobox();
        PopulateShiftCombobox();
    }

    private void PopulateFilterCombobox()
    {
        cbFilterColumn.SelectedItem = null;
        cbFilterColumn.Items.Clear();

        //Populate the filter ComboBox
        cbFilterColumn.DisplayMember = "Name";
        cbFilterColumn.ValueMember = "Value";
        foreach (DataGridViewColumn column in dgvBlastEditor.Columns)
        {
            //Exclude button and checkbox
            if (column is not (DataGridViewCheckBoxColumn or DataGridViewButtonColumn))// && column.Visible)
            {
                cbFilterColumn.Items.Add(new ComboBoxItem<string>(column.HeaderText, column.Name));
            }
        }
        cbFilterColumn.SelectedIndex = 0;
    }

    private void PopulateShiftCombobox()
    {
        cbShiftBlastlayer.SelectedItem = null;
        cbShiftBlastlayer.Items.Clear();

        //Populate the filter ComboBox
        cbShiftBlastlayer.DisplayMember = "Name";
        cbShiftBlastlayer.ValueMember = "Value";

        cbShiftBlastlayer.Items.Add(new ComboBoxItem<string>(BuProperty.Index.ToString(), BuProperty.Index.ToString()));
        cbShiftBlastlayer.Items.Add(new ComboBoxItem<string>(BuProperty.Replaces.ToString(), BuProperty.Replaces.ToString()));
        cbShiftBlastlayer.SelectedIndex = 0;
    }

    public void RefreshVisibleColumns()
    {
        foreach (DataGridViewColumn column in dgvBlastEditor.Columns)
            column.Visible = VisibleColumns.Contains(column.Name);
        dgvBlastEditor.Refresh();
    }

    private static DataGridViewColumn CreateColumn(string dataPropertyName, string columnName, string displayName,
        DataGridViewColumn column, int fillWeight = -1)
    {
        if (fillWeight == -1)
        {
            switch (column)
            {
                case DataGridViewButtonColumn s:
                    s.FillWeight = ButtonFillWeight;
                    break;
                case DataGridViewCheckBoxColumn s:
                    s.FillWeight = CheckBoxFillWeight;
                    break;
                case DataGridViewComboBoxColumn s:
                    s.FillWeight = ComboBoxFillWeight;
                    break;
                case DataGridViewTextBoxColumn s:
                    s.FillWeight = TextBoxFillWeight;
                    break;
                case DataGridViewNumericUpDownColumn s:
                    s.FillWeight = NumericUpDownFillWeight;
                    break;
            }
        }
        else
        {
            column.FillWeight = fillWeight;
        }

        column.DataPropertyName = dataPropertyName;
        column.Name = columnName;

        column.HeaderText = displayName;

        return column;
    }

    /*
    private DataGridViewColumn CreateColumnUnbound(string columnName, string displayName,
        DataGridViewColumn column, int fillWeight = -1)
    {
        return CreateColumn(String.Empty, columnName, displayName, column, fillWeight);
    }
    */

    private JavaStashKey _originalSk;

    private JavaStashKey _currentSk;

    private JavaStashKey CurrentSk
    {
        get => _currentSk;
        set
        {
            _currentSk = value;
            SetTitle(value?.Alias ?? "Unnamed");
        }
    }

    private BindingSource _bs;
    private BindingSource _bs2;
    private SerializedInsnBlastLayerCollection _siblc;

    private void LoadStashKey(JavaStashKey sk, bool silent = false)
    {
        _originalSk = sk;
        CurrentSk = sk.Clone() as JavaStashKey;

        _bs = new() { DataSource = new SortableBindingList<SerializedInsnBlastUnit>(CurrentSk!.BlastLayer) };
        _siblc = CurrentSk!.BlastLayer;

        _bs.CurrentChanged += (_, e) =>
        {
            if (!_batchOperation)
                return;
            if (e is HandledEventArgs h)
                h.Handled = true;
        };


        dgvBlastEditor.DataSource = _bs;
        InitializeDGV();
        InitializeBottom();

        if (silent)
            return;
        
        Show();
        BringToFront();
        RefreshAllNoteIcons();
    }

    private void OnBlastEditorDataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        MessageBox.Show(e.Exception + "\nRow:" + e.RowIndex + "\nColumn" + e.ColumnIndex + "\n" + e.Context + "\n" + dgvBlastEditor[e.ColumnIndex, e.RowIndex].Value);
    }

    public void Disable50(object sender, EventArgs e)
    {
        foreach (SerializedInsnBlastUnit bu in CurrentSk.BlastLayer.Layer.Layer.Where(x => x.IsLocked == false))
            bu.IsEnabled = true;

        List<SerializedInsnBlastUnit> unlocked = CurrentSk.BlastLayer.Layer.Layer.Where(x => !x.IsLocked).ToList();
        foreach (SerializedInsnBlastUnit bu in unlocked
                     .OrderBy(_ => RtcCore.RND.Next())
                     .Take(unlocked.Count / 2))
        {
            bu.IsEnabled = false;
        }
        dgvBlastEditor.Refresh();
    }

    private void InvertDisabled(object sender, EventArgs e)
    {
        foreach (SerializedInsnBlastUnit bu in CurrentSk.BlastLayer.Layer.Layer.Where(x => !x.IsLocked))
            bu.IsEnabled = !bu.IsEnabled;
        dgvBlastEditor.Refresh();
    }

    public void RemoveDisabled(object sender, EventArgs e)
    {
        List<SerializedInsnBlastUnit> buToRemove = [];

        dgvBlastEditor.SuspendLayout();
        _batchOperation = true;
        object oldBS = dgvBlastEditor.DataSource;
        dgvBlastEditor.DataSource = null;
        foreach (SerializedInsnBlastUnit bu in CurrentSk.BlastLayer.Layer.Layer.
                     Where(x =>
                         !x.IsLocked &&
                         !x.IsEnabled))
        {
            buToRemove.Add(bu);
        }

        foreach (SerializedInsnBlastUnit bu in buToRemove)
        {
            _bs.Remove(bu);
            if (_bs2 != null && _bs2.Contains(bu))
                _bs2.Remove(bu);
        }
        _batchOperation = false;
        dgvBlastEditor.DataSource = oldBS;
        RefreshAllNoteIcons();
        dgvBlastEditor.ResumeLayout();
    }

    private void DisableEverything(object sender, EventArgs e)
    {
        foreach (SerializedInsnBlastUnit bu in CurrentSk.BlastLayer.Layer.Layer.
                     Where(x =>
                         x.IsLocked == false))
        {
            bu.IsEnabled = false;
        }
        dgvBlastEditor.Refresh();
    }

    private void EnableEverything(object sender, EventArgs e)
    {
        foreach (SerializedInsnBlastUnit bu in CurrentSk.BlastLayer.Layer.Layer.
                     Where(x =>
                         x.IsLocked == false))
        {
            bu.IsEnabled = true;
        }
        dgvBlastEditor.Refresh();
    }

    public void RemoveSelected(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows)
        {
            if (((SerializedInsnBlastUnit)row.DataBoundItem).IsLocked)
                continue;
            
            SerializedInsnBlastUnit bu = (SerializedInsnBlastUnit)row.DataBoundItem;
            
            _bs.Remove(bu!);
            //Todo replace how this works
            if (_bs2 != null && _bs2.Contains(bu))
                _bs2.Remove(bu); //RTC FIX: it originally said _bs.Remove (in the original-original, bs.Remove since they're renamed here)
        }
    }

    public void DuplicateSelected(object sender, EventArgs e)
    {
        if (dgvBlastEditor.SelectedRows.Count == 0)
            return;

        DataGridViewRow[] reversed = dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>().Reverse()?.ToArray();
        foreach (DataGridViewRow row in reversed)
        {
            if (((SerializedInsnBlastUnit)row.DataBoundItem).IsLocked)
                continue;
            
            SerializedInsnBlastUnit bu = ((SerializedInsnBlastUnit)row.DataBoundItem).Clone() as SerializedInsnBlastUnit;
            _bs.Add(bu);
        }
        RefreshAllNoteIcons();
    }

    private void SendToStash(object sender, EventArgs e)
    {
        /*if (CurrentSk.ParentKey == null)
        {
            MessageBox.Show("There's no savestate associated with this Stashkey!\nAssociate one in the menu to send this to the stash.");
            return;
        }*/
        JavaStashKey newSk = (JavaStashKey)CurrentSk.Clone();

        JavaStockpileManagerUISide.StashHistory.Add(newSk);

        S.GET<JavaStashHistoryForm>().RefreshStashHistory();
        S.GET<JavaStockpileManagerForm>().dgvStockpile.ClearSelection();
        S.GET<JavaStashHistoryForm>().lbStashHistory.ClearSelected();

        S.GET<JavaStashHistoryForm>().DontLoadSelectedStash = true;
        S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex = S.GET<JavaStashHistoryForm>().lbStashHistory.Items.Count - 1;
        JavaStockpileManagerUISide.CurrentStashkey = JavaStockpileManagerUISide.StashHistory[S.GET<JavaStashHistoryForm>().lbStashHistory.SelectedIndex];
    }

    private void OpenNoteEditor(object sender, EventArgs e)
    {
        if (dgvBlastEditor.SelectedRows.Count == 0)
            return;

        SerializedInsnBlastLayer temp = new();
        List<DataGridViewCell> cellList = [];
        foreach (DataGridViewRow row in dgvBlastEditor.SelectedRows)
        {
            if (row.DataBoundItem is not SerializedInsnBlastUnit bu) 
                continue;
            temp.Layer.Add(bu);
            cellList.Add(row.Cells[BuProperty.Note.ToString()]);
        }

        S.SET(new NoteEditorForm(temp, cellList));
        S.GET<NoteEditorForm>().Show();
    }

    private void SanitizeDuplicates(object sender, EventArgs e)
    {
        dgvBlastEditor.ClearSelection();

        dgvBlastEditor.DataSource = null;
        _batchOperation = true;
        /*currentSK.BlastLayer.SanitizeDuplicates();*/
        foreach (SerializedInsnBlastLayer bl in CurrentSk.BlastLayer.Values)
            bl.SanitizeDuplicates();
        
        _bs = new() { DataSource = new SortableBindingList<SerializedInsnBlastUnit>(CurrentSk.BlastLayer) };
        _siblc = CurrentSk!.BlastLayer;
        _batchOperation = false;
        dgvBlastEditor.DataSource = _bs;
        dgvBlastEditor.Refresh();
        UpdateBottom();
    }

    //TODO: if i ever make methods into domains, this will need to be re-implemented
    /*public void RasterizeVMDs()
    {
        dgvBlastEditor.ClearSelection();

        dgvBlastEditor.DataSource = null;
        batchOperation = true;
        currentSK.BlastLayer.RasterizeVMDs();
        bs = new BindingSource { DataSource = new SortableBindingList<SerializedInsnBlastUnit>(currentSK.BlastLayer) };
        _siblc = CurrentSk!.BlastLayer;

        batchOperation = false;
        dgvBlastEditor.DataSource = bs;
        //updateMaximum(this, dgvBlastEditor.Rows.Cast<DataGridViewRow>().ToList()); TODO: this should instead update the maximum index based on the size of the method
        dgvBlastEditor.Refresh();
        UpdateBottom();
    }*/


    private void ReplaceRomFromGlitchHarvester(object sender, EventArgs e)
    {
        JavaStashKey temp = JavaStockpileManagerUISide.CurrentSavestateStashKey;

        if (temp == null)
        {
            MessageBox.Show("There is no savestate selected in the Glitch Harvester, or the current selected box is empty");
            return;
        }
        CurrentSk.ParentKey = null;
        CurrentSk.JarFilename = temp.JarFilename;
        CurrentSk.GameName = temp.GameName;
    }

    private void ReplaceRomFromFile(object sender, EventArgs e)
    {
        OpenFileDialog openRomDialog = new()
        {
            Title = "Open JAR File",
            Filter = "JAR files|*.jar",
            RestoreDirectory = true,
        };

        if (openRomDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        string filename = openRomDialog.FileName;

        LocalNetCoreRouter.Route(Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.LoadROM, filename, true);

        JavaStashKey temp = new(RtcCore.GetRandomKey(), CurrentSk.ParentKey, CurrentSk.BlastLayer);

        // We have to null this as to properly create a stashkey, we need to use it in the constructor,
        // but then the user needs to provide a savestate
        CurrentSk.ParentKey = null;

        CurrentSk.JarFilename = temp.JarFilename;
        CurrentSk.GameName = temp.GameName;
    }

    private void ReplaceLaunchScriptFromFile(object sender, EventArgs e)
    {
        OpenFileDialog openRomDialog = new()
        {
            Title = "Open Launch Script File",
            Filter = "Java Launch Scripts|*.jls",
            RestoreDirectory = true,
        };

        if (openRomDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        
        string filename = openRomDialog.FileName;
        
        CurrentSk.LaunchScript = JsonHelper.Deserialize<LaunchScript>(File.ReadAllText(filename));
    }

    private void SaveBlastLayerToFile(object sender, EventArgs e)
    {
        //If there's no blastlayer file already set, don't quicksave
        if (_currentBlastLayerFile.Length == 0)
            JavaBlastTools.SaveBlastLayerToFile(CurrentSk.BlastLayer);
        else
            JavaBlastTools.SaveBlastLayerToFile(CurrentSk.BlastLayer, _currentBlastLayerFile);

        _currentBlastLayerFile = JavaBlastTools.LastBlastLayerSavePath;
    }

    private void SaveAsBlastLayerToFile(object sender, EventArgs e)
    {
        JavaBlastTools.SaveBlastLayerToFile(CurrentSk.BlastLayer);
        _currentBlastLayerFile = JavaBlastTools.LastBlastLayerSavePath;
    }

    private void ImportBlastLayer(object sender, EventArgs e)
    {
        SerializedInsnBlastLayerCollection temp = JavaBlastTools.LoadBlastLayerFromFile();
        if (temp is null)
            return;
        ImportBlastLayer(temp.Layer);
    }

    private void LoadBlastLayerFromFile(object sender, EventArgs e)
    {
        SerializedInsnBlastLayerCollection temp = JavaBlastTools.LoadBlastLayerFromFile();
        if (temp != null)
        {
            LoadBlastlayer(temp.Layer);
        }
    }

    private void LoadBlastlayer(SerializedInsnBlastLayer bl, bool import = false)
    {
        if (bl == null)
        {
            _logger.Trace("LoadBlastLayer had an empty bl");
            return;
        }

        SynchronizedCollection<SerializedInsnBlastUnit> l = bl.Layer;
        if (l == null)
            return;

        if (import)
            foreach (SerializedInsnBlastUnit bu in l)
                _bs.Add(bu);
        else
        {
            CurrentSk.BlastLayer = new(l);
            _bs = new() { DataSource = new SortableBindingList<SerializedInsnBlastUnit>(CurrentSk.BlastLayer) };
            _siblc = CurrentSk!.BlastLayer;
            dgvBlastEditor.DataSource = _bs;
        }
        dgvBlastEditor.ResetBindings();
        RefreshAllNoteIcons();
        dgvBlastEditor.Refresh();
    }

    public void ImportBlastLayer(SerializedInsnBlastLayer bl)
    {
        LoadBlastlayer(bl, true);
    }

    private static string GenerateCSV(DataGridView dgv)
    {
        StringBuilder sb = new();
        IEnumerable<DataGridViewColumn> headers = dgv.Columns.Cast<DataGridViewColumn>();

        sb.AppendLine(string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));
        foreach (DataGridViewRow row in dgv.Rows)
        {
            IEnumerable<DataGridViewCell> cells = row.Cells.Cast<DataGridViewCell>();
            sb.AppendLine(string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
        }
        return sb.ToString();
    }

    public void ExportToCSV(string filename = null)
    {
        if (CurrentSk.BlastLayer.Layer.Layer.Count == 0)
        {
            MessageBox.Show("Can't save because the provided blastlayer is empty.");
            return;
        }

        if (filename == null)
        {
            SaveFileDialog saveCsvDialog = new()
            {
                DefaultExt = "csv",
                Title = "Export to csv",
                Filter = "csv files|*.csv",
                RestoreDirectory = true,
            };

            if (saveCsvDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filename = saveCsvDialog.FileName;
        }

        File.WriteAllText(filename, GenerateCSV(dgvBlastEditor), Encoding.UTF8);
    }
        

    public void LoadCorrupt(object sender, EventArgs e)
    {

        JavaStashKey newSk = (JavaStashKey)CurrentSk.Clone();
        S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = newSk.Run();
    }

    private void Corrupt(object sender, EventArgs e)
    {
        JavaStashKey newSk = (JavaStashKey)CurrentSk.Clone();
        S.GET<JavaGlitchHarvesterBlastForm>().IsCorruptionApplied = JavaStockpileManagerUISide.ApplyStashkey(newSk, false);
    }

    private static void RefreshNoteIcons(DataGridViewRowCollection rows)
    {
        foreach (DataGridViewRow row in rows)
        {
            DataGridViewCell buttonCell = row.Cells[BuProperty.Note.ToString()];
            buttonCell.Value = string.IsNullOrWhiteSpace((row.DataBoundItem as SerializedInsnBlastUnit)?.Note) ? string.Empty : "📝";
        }
    }

    private void RefreshAllNoteIcons()
    {
        RefreshNoteIcons(dgvBlastEditor.Rows);
    }


    private void ShiftBlastLayerDown(object sender, EventArgs e)
    {
        decimal amount = updownShiftBlastLayerAmount.Value;
        string column = ((ComboBoxItem<string>)cbShiftBlastlayer?.SelectedItem)?.Value;

        if (column == null)
        {
            return;
        }

        List<DataGridViewRow> rows = dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>()
            .Where(item => ((SerializedInsnBlastUnit)item.DataBoundItem).IsLocked == false)
            .ToList();
        ShiftBlastLayer(amount, column, rows, true);
    }

    private void ShiftBlastLayerUp(object sender, EventArgs e)
    {
        decimal amount = updownShiftBlastLayerAmount.Value;
        string column = ((ComboBoxItem<string>)cbShiftBlastlayer?.SelectedItem)?.Value;

        if (column == null)
        {
            return;
        }

        List<DataGridViewRow> rows = dgvBlastEditor.SelectedRows.Cast<DataGridViewRow>()
            .Where((item => ((SerializedInsnBlastUnit)item.DataBoundItem).IsLocked == false))
            .ToList();
        ShiftBlastLayer(amount, column, rows, false);
    }

    private void ShiftBlastLayer(decimal amount, string column, List<DataGridViewRow> rows, bool shiftDown)
    {
        foreach (DataGridViewRow row in rows)
        {
            DataGridViewCell cell = row.Cells[column];

            //Can't use a switch statement because tostring is evaluated at runtime
            if (cell is DataGridViewNumericUpDownCell u)
                if (shiftDown)
                    if (Convert.ToInt64(u.Value) - amount >= 0)
                        u.Value = Convert.ToInt64(u.Value) - amount;
                    else
                        u.Value = 0;
                else if (Convert.ToInt64(u.Value) + amount <= u.Maximum)
                    u.Value = Convert.ToInt64(u.Value) + amount;
                else
                    u.Value = u.Maximum;
            else
                throw new NotImplementedException("Invalid column type.");
        }
        dgvBlastEditor.Refresh();
        UpdateBottom();
    }

    private static string GetShiftedHexString(string value, decimal amount, int precision)
    {
        //Convert the string we have into a byte array
        byte[] valueBytes = value.ToByteArrayPadLeft(precision);
        if (valueBytes == null)
        {
            return value;
        }

        valueBytes.AddValueToByteArrayUnchecked(new(amount), true);
        return BitConverter.ToString(valueBytes).Replace("-", string.Empty);
    }

    private void ShowHelp(object sender, EventArgs e)
    {
        ProcessStartInfo startInfo = new("https://corrupt.wiki/corruptors/rtc-real-time-corruptor/blast-editor.html");
        Process.Start(startInfo);
    }

    private void AddRow(object sender, EventArgs e)
    {
        SerializedInsnBlastUnit bu = new([], 0, 1, "my/package/MyClass.myMethod(IFLmy/package.Class;)V");
        _bs.Add(bu);
    }

    private void UpdateLayerSize()
    {
        lbBlastLayerSize.Text = "Size: " + CurrentSk.BlastLayer.Layer.Layer.Count;
    }

    private void OpenSanitizeTool(object sender, EventArgs e)
    {
        OpenSanitizeTool(false);
    }

    private void OpenSanitizeTool(bool lockUI = true)
    {
        if (CurrentSk?.BlastLayer?.Layer == null)
            return;
        JavaSanitizeToolForm.OpenSanitizeTool(CurrentSk, lockUI);
    }

    internal JavaStashKey[] GetStashKeys()
    {
        return [CurrentSk, _originalSk];
    }

    /// <summary>
    /// Diffs two JARs and extracts the differences into a blast layer.
    /// </summary>
    /// <param name="filename">The filename of the corrupted JAR.</param>
    private void ImportBlastLayerFromCorruptedFile(string filename = null)
    {
        MessageBox.Show("This feature might be re-implemented later");
        return;
        // ReSharper disable HeuristicUnreachableCode
#pragma warning disable CS0162 // Unreachable code detected
        if (filename == null)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = "*",
                Title = "Open Corrupted File",
                Filter = "Any file|*.*",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filename = openFileDialog.FileName;
        }

        string suggestion;
        string uncorruptedFilename;
        if (DialogResult.Yes == MessageBox.Show("Is the currently loaded JAR the uncorrupted version of the file?", "Corrupted File", MessageBoxButtons.YesNo))
        {
            suggestion = "You need to select the uncorrupted file manually rather than using the currently loaded one.";
            uncorruptedFilename = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
            CurrentSk.JarFilename = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
        }
        else
        {
            suggestion = "You probably selected the wrong file to act as the uncorrupted one.";
            MessageBox.Show("Please select the uncorrupted version of the file.");
            OpenFileDialog openRomDialog = new()
            {
                Title = "Open JAR File",
                Filter = "JAR files|*.jar",
                RestoreDirectory = true,
            };

            if (openRomDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            uncorruptedFilename = openRomDialog.FileName;
            CurrentSk.JarFilename = openRomDialog.FileName;
        }


        SerializedInsnBlastLayer layer = new();
        using FileStream uncorruptedFs = new(uncorruptedFilename, FileMode.Open, FileAccess.Read);
        using ZipArchive uncorruptedArchive = new(uncorruptedFs, ZipArchiveMode.Read);
        using FileStream corruptFs = new(filename, FileMode.Open, FileAccess.Read);
        using ZipArchive corruptArchive = new(corruptFs, ZipArchiveMode.Read);
        foreach (ZipArchiveEntry entry in uncorruptedArchive.Entries)
        {
            if (entry.FullName.EndsWith(".class"))
            {
                ZipArchiveEntry corruptEntry = corruptArchive.GetEntry(entry.FullName);
                if (corruptEntry == null)
                {
                    MessageBox.Show($"The corrupted JAR does not contain the file {entry.FullName}. {suggestion}");
                    return;
                }
                
                using Stream corruptStream = corruptEntry.Open();
                using Stream uncorruptedStream = entry.Open();
                byte[] uncorruptedBytes = new byte[entry.Length];
                byte[] corruptBytes = new byte[corruptEntry.Length];
                
                int bytesRead = 0;
                do
                    bytesRead += uncorruptedStream.Read(uncorruptedBytes, bytesRead, uncorruptedBytes.Length - bytesRead);
                while (bytesRead < uncorruptedBytes.Length);
                
                bytesRead = 0;
                do
                    bytesRead += corruptStream.Read(corruptBytes, bytesRead, corruptBytes.Length - bytesRead);
                while (bytesRead < corruptBytes.Length);
                
                if (uncorruptedBytes.SequenceEqual(corruptBytes))
                    continue;
                
                ClassReader originalReader = new((sbyte[])(Array)uncorruptedBytes);
                ClassReader corruptedReader = new((sbyte[])(Array)corruptBytes);
                ClassNode originalClass = new();
                ClassNode corruptedClass = new();
                originalReader.Accept(originalClass, 0);
                corruptedReader.Accept(corruptedClass, 0);
                if (originalClass.Methods.Count != corruptedClass.Methods.Count)
                {
                    MessageBox.Show($"The class {entry.FullName} has a different number of methods between the corrupted and uncorrupted JARs, meaning your uncorrupted file is not the right one. {suggestion}");
                    return;
                }
                
                for (int i = 0; i < originalClass.Methods.Count; i++)
                {
                    MethodNode originalMethod = originalClass.Methods[i];
                    MethodNode corruptedMethod = corruptedClass.Methods[i];
                    if (originalMethod.Name != corruptedMethod.Name || originalMethod.Desc != corruptedMethod.Desc)
                    {
                        MessageBox.Show($"Found a method mismatch: uncorrupted method #{i} is {originalMethod.Name}{originalMethod.Desc}, and corrupted method #{i} is {corruptedMethod.Name}{corruptedMethod.Desc}. {suggestion}");
                        return;
                    }
                    
                    AsmParser originalParser = new();
                    AsmParser corruptedParser = new();
                    originalParser.RegisterLabelsFrom(originalMethod.Instructions);
                    corruptedParser.RegisterLabelsFrom(corruptedMethod.Instructions);
                    int index = 0;
                    int foundAtIndex = 0;
                    int replaces = 0;
                    List<string> corruption = [];
                    AbstractInsnNode corruptInsn = corruptedMethod.Instructions.First;
                    AbstractInsnNode firstCorruptInsn = originalMethod.Instructions.First;
                    for (AbstractInsnNode origInsn = originalMethod.Instructions.First;
                         origInsn is not null && corruptInsn is not null;
                         origInsn = origInsn.Next, corruptInsn = corruptInsn.Next, index++)
                    {
                        string origInsnStr = originalParser.InsnToString(origInsn);
                        string corruptInsnStr = corruptedParser.InsnToString(corruptInsn);
                        
                        if (origInsnStr == corruptInsnStr)
                        {
                            if (replaces == 0)
                                continue;

                            layer.Layer.Add(new(corruption, foundAtIndex, replaces,
                                originalClass.Name + "." + originalMethod.Name + originalMethod.Desc));
                            corruption = [];
                            replaces = 0;
                            foundAtIndex = 0;
                            continue;
                        }
        
                        corruption.Add(corruptInsnStr);
                        if (replaces < 1)
                        {
                            firstCorruptInsn = origInsn;
                        }
                        replaces++;
                        foundAtIndex = index;
                    }
                }
            }
        }

        ImportBlastLayer(layer);
#pragma warning restore CS0162 // Unreachable code detected
        // ReSharper enable HeuristicUnreachableCode
    }

    private void importBlastlayerFromCorruptedFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ImportBlastLayerFromCorruptedFile();
    }

    private void showBlastlayerNameInTitleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dontShowBlastlayerNameInTitleToolStripMenuItem.Checked)
            Params.SetParam("DONT_SHOW_BLASTLAYER_NAME_IN_EDITOR");
        else
            Params.RemoveParam("DONT_SHOW_BLASTLAYER_NAME_IN_EDITOR");

        SetTitle(CurrentSk?.Alias ?? "Unsaved");
    }

    private void SetTitle(string name)
    {
        Text = dontShowBlastlayerNameInTitleToolStripMenuItem.Checked
            ? "Blast Editor"
            : "Blast Editor - " + name;
    }

    private void NewBlastLayer(object sender, EventArgs e)
    {
        _bs.Clear();
        dgvBlastEditor.ResetBindings();
        RefreshAllNoteIcons();
        dgvBlastEditor.Refresh();
    }

    public bool AddStashToStockpile()
    {
        SendToStash(null, null);
        return S.GET<JavaStashHistoryForm>().AddStashToStockpileFromUI();
    }

    private void AddStashToStockpile(object sender, EventArgs e) => AddStashToStockpile();
    private void BreakDownAllBlastUnits(object sender, EventArgs e) => BreakDownUnits();
    private void OnBlastEditorRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => UpdateLayerSize();
    private void OnBlastEditorRowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => UpdateLayerSize();
    private void ExportBlastLayerToCSV(object sender, EventArgs e) => ExportToCSV();
    
    private void RasterizeVMDs(object sender, EventArgs e)
    {
        //TODO: if i ever make methods/classes into domains, this will need to be re-implemented
        MessageBox.Show("This feature may be re-implemented later if I decide to turn methods/classes into domains. For now, it's useless.");
        //RasterizeVMDs();
    }
}