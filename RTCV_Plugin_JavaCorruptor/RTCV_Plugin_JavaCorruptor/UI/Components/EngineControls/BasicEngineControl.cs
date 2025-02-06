using System;
using System.Dynamic;
using System.Linq;
using System.Collections.Generic;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class BasicEngineControl
{
    private byte[] _limiters, _values;
    private bool _noneSelectedWarningShown, _sameSelectedWarningShown;
    private bool _skipHandling;

    public BasicEngineControl()
    {
        InitializeComponent();

        NoOperandOpcodes[] opcodes = (NoOperandOpcodes[])Enum.GetValues(typeof(NoOperandOpcodes));

        foreach (NoOperandOpcodes op in opcodes)
        {
            lbLimiters.Items.Add(op);
            lbValues.Items.Add(op);
        }
        lbLimiters.Items.Remove(NoOperandOpcodes.nop);
    }

    public override void Prepare()
    {
        base.Prepare();
        
        _limiters = lbLimiters.SelectedItems.Cast<NoOperandOpcodes>().Select(i => (byte) i).ToArray();
        _values = lbValues.SelectedItems.Cast<NoOperandOpcodes>().Select(i => (byte) i).ToArray();
        _noneSelectedWarningShown = false;
        _sameSelectedWarningShown = false;
    }
    public override void UpdateUI()
    {
        foreach (byte limiter in _limiters)
            lbLimiters.SetSelected(Array.IndexOf(lbLimiters.Items.Cast<NoOperandOpcodes>().ToArray(), (NoOperandOpcodes)limiter), true);
        foreach (byte value in _values)
            lbValues.SetSelected(Array.IndexOf(lbValues.Items.Cast<NoOperandOpcodes>().ToArray(), (NoOperandOpcodes)value), true);
    }

    private void lbLimiters_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_skipHandling)
            return;
        
        HashSet<NoOperandOpcodes> set;
        // if there are any selected limiters, get the set of opcodes that perform the same type of operation as the selected limiter
        if (lbLimiters.SelectedItems.Count > 0)
        {
            NoOperandOpcodes opcode = (NoOperandOpcodes)lbLimiters.SelectedItems[0];
            set = OpcodeGroups.AllOpcodeGroups.First(s => s.Contains(opcode));
        }
        else if (lbValues.SelectedItems.Count <= 0) // if nothing is selected at all, reset both lists
        {
            lbLimiters.Items.Clear();
            lbValues.Items.Clear();
            NoOperandOpcodes[] opcodes = (NoOperandOpcodes[])Enum.GetValues(typeof(NoOperandOpcodes));
            foreach (NoOperandOpcodes op in opcodes)
            {
                lbLimiters.Items.Add(op);
                lbValues.Items.Add(op);
            }
            lbLimiters.Items.Remove(NoOperandOpcodes.nop);
            return;
        }
        else // if there are no selected limiters, but there are selected values, update the lists based on the selected values
        {
            lbValues_SelectedIndexChanged(null, EventArgs.Empty);
            return;
        }
        
        List<NoOperandOpcodes> limiters = lbLimiters.SelectedItems.Cast<NoOperandOpcodes>().ToList();
        List<NoOperandOpcodes> values = lbValues.SelectedItems.Cast<NoOperandOpcodes>().ToList();
        lbLimiters.Items.Clear();
        lbValues.Items.Clear();
        
        if (OpcodeGroups.Unary.Contains(limiters[0]))
            lbValues.Items.Add(NoOperandOpcodes.nop);

        foreach (var opcode in set)
        {
            lbLimiters.Items.Add(opcode);
            lbValues.Items.Add(opcode);
        }
        if (lbLimiters.Items.Count == 0)
        {
            // i'm 90% sure this is unreachable, but if somehow the set is empty, ensure that there's a way for the user
            // to deselect everything, which will reset the lists
            lbLimiters.Items.Add(NoOperandOpcodes.iconst_0);
            lbValues.Items.Add(NoOperandOpcodes.iconst_0);
            return;
        }

        try
        {
            _skipHandling = true;
            foreach (var l in limiters)
                lbLimiters.SetSelected(Array.IndexOf(lbLimiters.Items.Cast<NoOperandOpcodes>().ToArray(), l),
                    true);
            foreach (var v in values)
                lbValues.SetSelected(Array.IndexOf(lbValues.Items.Cast<NoOperandOpcodes>().ToArray(), v), true);
        }
        finally 
        {
            _skipHandling = false;
        }
    }

    private void lbValues_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_skipHandling)
            return;
        
        HashSet<NoOperandOpcodes> set = [];
        if (lbValues.SelectedItems.Count > 0) // if there are any values selected,
        {
            NoOperandOpcodes opcode = (NoOperandOpcodes)lbValues.SelectedItems[0];
            // if the selected value is not nop, or there are no selected limiters
            if (opcode != NoOperandOpcodes.nop || lbLimiters.SelectedItems.Count <= 0)
            {
                // if the selected value is a nop, and it's the only selected value
                if (opcode == NoOperandOpcodes.nop && lbValues.SelectedItems.Count == 1)
                {
                    set = OpcodeGroups.Unary; // we want to show all possible unary operations
                }
                else if (opcode == NoOperandOpcodes.nop)  // if it's a nop, and there are multiple selected values,
                                                          // show the set of opcodes that perform the same type of operation to the selected value that is not nop
                                                          // nop is guaranteed to be the first selected item if it's present
                    set = OpcodeGroups.AllOpcodeGroups.First(s => s.Contains((NoOperandOpcodes)lbValues.SelectedItems[1]));
                else // if the selected value is not nop, show the set of opcodes that perform the same type of operation as the selected value(s)
                    set = OpcodeGroups.AllOpcodeGroups.First(s => s.Contains(opcode));
            }
            else
            {
                return;
            }
        }
        else if (lbLimiters.SelectedItems.Count <= 0) // if nothing is selected at all, reset both lists
        {
            lbLimiters.Items.Clear();
            lbValues.Items.Clear();
            NoOperandOpcodes[] opcodes = (NoOperandOpcodes[])Enum.GetValues(typeof(NoOperandOpcodes));
            foreach (NoOperandOpcodes op in opcodes)
            {
                lbLimiters.Items.Add(op);
                lbValues.Items.Add(op);
            }
            lbLimiters.Items.Remove(NoOperandOpcodes.nop);
            return;
        }
        else
        {
            // we don't have to update anything in this case. we only update the lists in the value selection event because of the nop special cases.
            return;
        }
        
        List<NoOperandOpcodes> limiters = lbLimiters.SelectedItems.Cast<NoOperandOpcodes>().ToList();
        List<NoOperandOpcodes> values = lbValues.SelectedItems.Cast<NoOperandOpcodes>().ToList();
        lbLimiters.Items.Clear();
        lbValues.Items.Clear();
        
        if (OpcodeGroups.Unary.Contains(values[0]) || values[0] == NoOperandOpcodes.nop)
            lbValues.Items.Add(NoOperandOpcodes.nop);

        foreach (var opcode in set)
        {
            lbLimiters.Items.Add(opcode);
            lbValues.Items.Add(opcode);
        }
        if (lbLimiters.Items.Count == 0)
        {
            // i'm 90% sure this is unreachable, but if somehow the set is empty, ensure that there's a way for the user
            // to deselect everything, which will reset the lists
            lbLimiters.Items.Add(NoOperandOpcodes.iconst_0);
            lbValues.Items.Add(NoOperandOpcodes.iconst_0);
            return;
        }
        
        try
        {
            _skipHandling = true;
            foreach (var l in limiters)
                lbLimiters.SetSelected(Array.IndexOf(lbLimiters.Items.Cast<NoOperandOpcodes>().ToArray(), l), true);
            foreach (var v in values)
                lbValues.SetSelected(Array.IndexOf(lbValues.Items.Cast<NoOperandOpcodes>().ToArray(), v), true);
        }
        finally
        {
            _skipHandling = false;
        }
    }

    public override List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        List<AbstractInsnNode> list = [];

        if (_limiters.Length == 0 || _values.Length == 0)
        {
            if (_noneSelectedWarningShown) return list;
            Logger.Warn("No limiters or no operations selected.");
            _noneSelectedWarningShown = true;
            return list;
        }
        if (_limiters.Length == 1 && _values.Length == 1 && _limiters[0] == _values[0])
        {
            if (_sameSelectedWarningShown) return list;
            Logger.Warn("Limiter and operation are the same");
            _sameSelectedWarningShown = true;
            return list;
        }

        int index = Array.IndexOf(_limiters, (byte)insn.Opcode);
        int idx = Array.IndexOf(_values, (byte)insn.Opcode);
        if (index != -1)
        {
            int op;
            if (_values.Length > 1)
            {
                int rand = JavaGeneralParametersForm.Random.Next(_values.Length - 1);
                op = rand >= idx ? _values[rand + 1] : _values[rand];
            }
            else
                op = _values[0];
            if (insn.Opcode == op)
                return list;
            list.Add(new InsnNode(op));
        }
        else
            return list;

        replaces = 1;
        return list;
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            settings.Limiters = _limiters;
            settings.Values = _values;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            if (settings.Limiters is Array)
                _limiters = settings.Limiters;
            else
                _limiters = ((IEnumerable<object>)settings.Limiters).Select(x => (byte)(long)x).ToArray();
            if (settings.Values is Array)
                _values = settings.Values;
            else
                _values = ((IEnumerable<object>)settings.Values).Select(x => (byte)(long)x).ToArray();
        }
    }
}