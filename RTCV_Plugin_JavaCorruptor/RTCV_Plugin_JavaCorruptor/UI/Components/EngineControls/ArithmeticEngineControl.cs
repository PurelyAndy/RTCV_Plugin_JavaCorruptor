using ObjectWeb.Asm.Tree;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using static ObjectWeb.Asm.Opcodes;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class ArithmeticEngineControl
{
    private byte[] _limiters, _operations;
    private byte _instructions;
    private double _minimum, _maximum;
    private bool _int, _long, _float, _double;
    private bool _runtimeRandom;
    private bool _skipArrayAccess;
    private bool _noneSelectedWarningShown;
    public ArithmeticEngineControl()
    {
        InitializeComponent();
        lbeInstructions.SetSelected(1, true);
        lbeInstructions.SetSelected(3, true);
        lbeInstructions.SetSelected(4, true);
        lbeLimiters.SetSelected(0, true);
        lbeLimiters.SetSelected(1, true);
        lbeLimiters.SetSelected(2, true);
        lbeOperations.SetSelected(0, true);
        lbeOperations.SetSelected(1, true);
    }
    private double RandomValue => _minimum + JavaGeneralParametersForm.Random.NextDouble() * (_maximum - _minimum);

    private int GetRandomOperation(int opcode)
    {
        return _operations[JavaGeneralParametersForm.Random.Next(_operations.Length)] + (opcode % 4) + 0x60;
    }

    public override void Prepare()
    {
        base.Prepare();
        _limiters = (from int i in lbeLimiters.SelectedIndices select (byte)(i * 4)).ToArray();
        _operations = (from int i in lbeOperations.SelectedIndices select (byte)(i * 4)).ToArray();
        _minimum = tbMinimum.Value / 1000d;
        _maximum = tbMaximum.Value / 1000d;
        _int = cbInt.Checked;
        _long = cbLong.Checked;
        _float = cbFloat.Checked;
        _double = cbDouble.Checked;
        _runtimeRandom = cbRuntimeRandom.Checked;
        _instructions = 0;
        foreach (int i in lbeInstructions.SelectedIndices)
            _instructions |= (byte)Math.Pow(2, i);
        _skipArrayAccess = cbSkipArrayAccess.Checked;
        _noneSelectedWarningShown = false;
    }

    public override void UpdateUI()
    {
        foreach (byte limiter in _limiters)
            lbeLimiters.SetSelected(limiter / 4, true);
        foreach (byte operation in _operations)
            lbeOperations.SetSelected(operation / 4, true);
        
        tbMinimum.Value = (int)(_minimum * 1000);
        lbMinimum.Text = "Minimum: " + _minimum;
        tbMaximum.Value = (int)(_maximum * 1000);
        lbMaximum.Text = "Maximum: " + _maximum;
        cbInt.Checked = _int;
        cbLong.Checked = _long;
        cbFloat.Checked = _float;
        cbDouble.Checked = _double;
        cbRuntimeRandom.Checked = _runtimeRandom;
        lbeInstructions.ClearSelected();
        for (int i = 0; i < 8; i++)
            lbeInstructions.SetSelected(i, (_instructions & (byte)Math.Pow(2, i)) != 0);
        cbSkipArrayAccess.Checked = _skipArrayAccess;
    }

    private void tbMaximum_Scroll(object sender, EventArgs e)
    {
        lbMaximum.Text = "Maximum: " + (float)tbMaximum.Value / 1000;
        if (tbMinimum.Value > tbMaximum.Value)
        {
            tbMinimum.Value = tbMaximum.Value;
            tbMinimum_Scroll(null, null);
        }
    }

    private void tbMinimum_Scroll(object sender, EventArgs e)
    {
        lbMinimum.Text = "Minimum: " + (float)tbMinimum.Value / 1000;
        if (tbMaximum.Value < tbMinimum.Value)
        {
            tbMaximum.Value = tbMinimum.Value;
            tbMaximum_Scroll(null, null);
        }
    }

    private void lbeInstructions_SelectedIndexChanged(object sender, EventArgs e) =>
        lbeLimiters.Enabled = lbeInstructions.SelectedIndices.Contains(1);

    public override List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        List<AbstractInsnNode> list = [];

        if (_operations.Length == 0)
        {
            Logger.Warn("No operations selected");
            return list;
        }

        int opcode = insn.Opcode;

        if (insn is LdcInsnNode && (_instructions & 1) == 1)
            CorruptLdcConstants(insn, list, opcode);
        if (opcode is >= Iconst_M1 and <= Dconst_1 && (_instructions & 1) == 1)
            CorruptXConst_NConstants(insn, list, opcode);
        else if (opcode is >= Iadd and <= Ddiv && (_instructions & 2) == 2)
            CorruptMathOperations(insn, list, opcode);
        else if (opcode is >= Iload and <= Dload && (_instructions & 4) == 4)
            CorruptVariableLoads(insn, list, opcode);
        else if (opcode is >= Iaload and <= Daload && (_instructions & 4) == 4 && !_skipArrayAccess)
            CorruptArrayLoads(insn, list, opcode);
        else if (opcode is >= 0x1a and <= 0x29 && (_instructions & 4) == 4)  // possibly redundant, i think asm converts these to the long form (which would be why they don't appear in the Opcodes class)
            CorruptVariableLoadsConst(insn, list, opcode);
        else if (opcode is Getfield or Getstatic && (_instructions & 8) == 8)
            CorruptFieldLoads(insn, list);
        else if (opcode is Invokestatic or Invokevirtual or Invokespecial && (_instructions & 16) == 16)
            CorruptMethodCalls(insn, list);
        else if (opcode is >= Istore and <= Dstore && (_instructions & 32) == 32)
            CorruptVariableStores(insn, list, opcode);
        else if (opcode is >= Iastore and <= Dastore && (_instructions & 32) == 32 && !_skipArrayAccess)
            CorruptArrayStores(insn, list, opcode);
        else if (opcode is >= 0x3b and <= 0x4a && (_instructions & 32) == 32)  // possibly redundant, i think asm converts these to the long form (which would be why they don't appear in the Opcodes class)
            CorruptVariableStoresConst(insn, list, opcode);
        else if (opcode is Putfield or Putstatic && (_instructions & 64) == 64)
            CorruptFieldStores(insn, list);
        else if (opcode is >= Ireturn and <= Dreturn && (_instructions & 128) == 128)
            CorruptReturns(insn, list, opcode);
        else
            return list;

        if (list.Count == 0)
            return list;
        if (list.Count == 1 && list.First() == insn)
            return list;

        replaces = 1;
        return list;
    }

    private void AddArithmeticInstructions(List<AbstractInsnNode> list, int opcode = -1)
    {
        if (_runtimeRandom)
        {
            list.Add(new MethodInsnNode(Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(_maximum - _minimum));
            list.Add(new InsnNode(Dmul));
            list.Add(new LdcInsnNode(_minimum));
            list.Add(new InsnNode(Dadd));
            int t = opcode % 4;
            if (t == 0 && _int)
                list.Add(new InsnNode(D2I));
            else if (t == 1 && _long)
                list.Add(new InsnNode(D2L));
            else if (t == 2 && _float)
                list.Add(new InsnNode(D2F));
            else if (t != 3 || !_double)
            {
                list.Clear();
                return;
            }
            list.Add(new InsnNode(GetRandomOperation(opcode)));
        }
        else
        {
            double value = RandomValue;
            int t = opcode % 4;
            if (t == 0 && _int)
                list.Add(new LdcInsnNode((int)Math.Round(value)));
            else if (t == 1 && _long)
                list.Add(new LdcInsnNode((long)Math.Round(value)));
            else if (t == 2 && _float)
                list.Add(new LdcInsnNode((float)value));
            else if (t == 3 && _double)
                list.Add(new LdcInsnNode((double)value));
            else
                return;
            list.Add(new InsnNode(GetRandomOperation(opcode)));
        }
    }

    private void CorruptLdcConstants(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        LdcInsnNode node = (LdcInsnNode)insn;
        object t = node.Cst;
        list.Add(insn);
        if (_runtimeRandom)
        {
            list.Add(new MethodInsnNode(Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(_maximum - _minimum));
            list.Add(new InsnNode(Dmul));
            list.Add(new LdcInsnNode(_minimum));
            list.Add(new InsnNode(Dadd));
            if (t is int && _int)
            {
                list.Add(new InsnNode(D2I));
                list.Add(new InsnNode(GetRandomOperation(0)));
            }
            else if (t is long && _long)
            {
                list.Add(new InsnNode(D2L));
                list.Add(new InsnNode(GetRandomOperation(1)));
            }
            else if (t is float && _float)
            {
                list.Add(new InsnNode(D2F));
                list.Add(new InsnNode(GetRandomOperation(2)));
            }
            else if (t is double && _double)
                list.Add(new InsnNode(GetRandomOperation(3)));
            else
                list.Clear();
        }
        else
        {
            double value = RandomValue;
            if (t is int && _int)
            {
                list.Add(new LdcInsnNode((int)Math.Round(value)));
                list.Add(new InsnNode(GetRandomOperation(0)));
            }
            else if (t is long && _long)
            {
                list.Add(new LdcInsnNode((long)Math.Round(value)));
                list.Add(new InsnNode(GetRandomOperation(1)));
            }
            else if (t is float && _float)
            {
                list.Add(new LdcInsnNode((float)value));
                list.Add(new InsnNode(GetRandomOperation(2)));
            }
            else if (t is double && _double)
            {
                list.Add(new LdcInsnNode((double)value));
                list.Add(new InsnNode(GetRandomOperation(3)));
            }
            else
                return;
        }
    }
    
    private void CorruptXConst_NConstants(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        if (opcode is >= Iconst_M1 and <= Iconst_5)
            opcode = 0;
        else if (opcode is Lconst_0 or Lconst_1)
            opcode = 1;
        else if (opcode is >= Fconst_0 and <= Fconst_2)
            opcode = 2;
        else if (opcode is Dconst_0 or <= Dconst_1)
            opcode = 3;
        else
            return;
        list.Add(insn);
        AddArithmeticInstructions(list, opcode);
    }

    private void CorruptMathOperations(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        if (_limiters.Length == 0 || _operations.Length == 0)
        {
            if (!_noneSelectedWarningShown)
            {
                Logger.Warn("No limiter math or no value math selected.");
                _noneSelectedWarningShown = true;
            }
            return;
        }

        if (!_limiters.Contains((byte)(opcode - 0x60 - ((opcode - 0x60) % 4)))) //from 0x60-0x6f, each grouping of 4 is the I,L,F, and D instructions of each operation.
            return;

        list.Add(insn);
        AddArithmeticInstructions(list, opcode);
    }

    private void CorruptVariableLoads(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        list.Add(insn);
        AddArithmeticInstructions(list, opcode - Iload); // ILOAD is not a multiple of 4
    }

    private void CorruptArrayLoads(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        list.Add(insn);
        AddArithmeticInstructions(list, opcode - Iaload); // IALOAD is not a multiple of 4
    }

    private void CorruptVariableLoadsConst(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        list.Add(insn);
        AddArithmeticInstructions(list, (opcode - 0x1a) / 4); // 0x1a is ILOAD_0, 0x1b is ILOAD_1, etc until ILOAD_3
    }

    private void CorruptFieldLoads(AbstractInsnNode insn, List<AbstractInsnNode> list)
    {
        FieldInsnNode node = (FieldInsnNode)insn;
        string desc = node.Desc;
        if (desc.Length != 1)
            return;
        
        if (desc is "I" && _int)
            AddArithmeticInstructions(list, 0);
        else if (desc is "J" && _long)
            AddArithmeticInstructions(list, 1);
        else if (desc is "F" && _float)
            AddArithmeticInstructions(list, 2);
        else if (desc is "D" && _double)
            AddArithmeticInstructions(list, 3);
        else
            return;
        
        list.Insert(0, insn);
    }

    private void CorruptMethodCalls(AbstractInsnNode insn, List<AbstractInsnNode> list)
    {
        MethodInsnNode node = (MethodInsnNode)insn;
        string desc = node.Desc;
        if (desc.Length != 3)
            return;
        
        if (desc.EndsWith(")I") && _int)
            AddArithmeticInstructions(list, 0);
        else if (desc.EndsWith(")J") && _long)
            AddArithmeticInstructions(list, 1);
        else if (desc.EndsWith(")F") && _float)
            AddArithmeticInstructions(list, 2);
        else if (desc.EndsWith(")D") && _double)
            AddArithmeticInstructions(list, 3);
        else
            return;
        
        list.Insert(0, insn);
    }

    private void CorruptVariableStores(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        AddArithmeticInstructions(list, opcode - Istore); // ISTORE is not a multiple of 4
        if (list.Count > 0)
            list.Add(insn.Clone());
    }

    private void CorruptArrayStores(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        AddArithmeticInstructions(list, opcode - Iastore); // IASTORE is not a multiple of 4
        if (list.Count > 0)
            list.Add(insn.Clone());
    }

    private void CorruptVariableStoresConst(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        AddArithmeticInstructions(list, (opcode - 0x3b) / 4); // 0x3b is ISTORE_0, 0x3c is ISTORE_1, etc until ISTORE_3
        if (list.Count > 0)
            list.Add(insn.Clone());
    }

    private void CorruptFieldStores(AbstractInsnNode insn, List<AbstractInsnNode> list)
    {
        FieldInsnNode node = (FieldInsnNode)insn;
        string desc = node.Desc;
        if (desc.Length != 1)
            return;
        
        if (desc is "I" && _int)
            AddArithmeticInstructions(list, 0);
        else if (desc is "J" && _long)
            AddArithmeticInstructions(list, 1);
        else if (desc is "F" && _float)
            AddArithmeticInstructions(list, 2);
        else if (desc is "D" && _double)
            AddArithmeticInstructions(list, 3);
        else
            return;

        if (list.Count > 0)
            list.Add(insn.Clone());
    }

    private void CorruptReturns(AbstractInsnNode insn, List<AbstractInsnNode> list, int opcode)
    {
        AddArithmeticInstructions(list, opcode - Ireturn); // IRETURN is not a multiple of 4
        if (list.Count > 0)
            list.Add(insn.Clone());
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            settings.Limiters = _limiters;
            settings.Operations = _operations;
            settings.Minimum = _minimum;
            settings.Maximum = _maximum;
            settings.Int = _int;
            settings.Long = _long;
            settings.Float = _float;
            settings.Double = _double;
            settings.RuntimeRandom = _runtimeRandom;
            settings.Instructions = _instructions;
            settings.SkipArrayAccess = _skipArrayAccess;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            if (((IDictionary<string, object>)settings).ContainsKey("SkipArrayAccess"))
            {
                if (settings.Limiters is Array)
                    _limiters = settings.Limiters;
                else
                    _limiters = ((IEnumerable<object>)settings.Limiters).Select(x => (byte)(long)x).ToArray();
                if (settings.Operations is Array)
                    _operations = settings.Operations;
                else
                    _operations = ((IEnumerable<object>)settings.Operations).Select(x => (byte)(long)x).ToArray();
                _instructions = (byte)settings.Instructions;
                _skipArrayAccess = settings.SkipArrayAccess;
            }
            _minimum = settings.Minimum;
            _maximum = settings.Maximum;
            _int = settings.Int;
            _long = settings.Long;
            _float = settings.Float;
            _double = settings.Double;
            _runtimeRandom = settings.RuntimeRandom;
        }
    }
}