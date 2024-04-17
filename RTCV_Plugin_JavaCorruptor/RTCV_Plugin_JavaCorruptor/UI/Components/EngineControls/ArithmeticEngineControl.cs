using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.Common;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class ArithmeticEngineControl
{
    private byte[] _limiters, _operations;
    private double _minimum, _maximum;
    private bool _int, _long, _float, _double;
    private bool _runtimeRandom;
    public ArithmeticEngineControl()
    {
        InitializeComponent();
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
    }

    private void tbMaximum_Scroll(object sender, EventArgs e)
    {
        lbMaximum.Text = "Maximum: " + (float)tbMaximum.Value / 1000;
    }

    private void tbMinimum_Scroll(object sender, EventArgs e)
    {
        lbMinimum.Text = "Minimum: " + (float)tbMinimum.Value / 1000;
    }

    public override InsnList DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        InsnList list = new() { insn };

        int opcode = insn.Opcode;
        if (opcode is < 0x60 or > 0x6f)
            return list;
        if (!_limiters.Contains((byte)(opcode - 0x60 - ((opcode - 0x60) % 4)))) //from 0x60-0x6f, each grouping of 4 is the I,L,F, and D instructions of each operation.
            return list;

        if (_runtimeRandom)
        {
            list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(_maximum - _minimum));
            list.Add(new InsnNode(Opcodes.Dmul));
            list.Add(new LdcInsnNode(_minimum));
            list.Add(new InsnNode(Opcodes.Dadd));
            int operation = _operations[JavaGeneralParametersForm.Random.Next(_operations.Length)] + (opcode % 4) + 0x60;
            replaces = 1;
            switch (opcode % 4)
            {
                case 0 when _int:
                    list.Add(new InsnNode(Opcodes.D2I)); goto operate;
                case 1 when _long:
                    list.Add(new InsnNode(Opcodes.D2L)); goto operate;
                case 2 when _float:
                    list.Add(new InsnNode(Opcodes.D2F)); goto operate;
                case 3 when _double: operate:
                    list.Add(new InsnNode(operation));
                    return list;
            }

            replaces = -1;
            return list;
        }
        else
        {
            double f = JavaGeneralParametersForm.Random.NextDouble();
            double value = _minimum + f * (_maximum - _minimum);
            int operation = _operations[JavaGeneralParametersForm.Random.Next(_operations.Length)] + (opcode % 4) + 0x60;

            //TODO: this could probably be done better
            replaces = 1;
            switch (opcode % 4)
            {
                case 0 when _int:
                    if (value is > 0 and < 1)
                        value = 1;
                    else if (value is > -1 and < 0)
                        value = -1;
                    list.Add(new LdcInsnNode((int)value)); goto operate;
                case 1 when _long:
                    if (value is > 0 and < 1)
                        value = 1;
                    else if (value is > -1 and < 0)
                        value = -1;
                    list.Add(new LdcInsnNode((long)value)); goto operate;
                case 2 when _float:
                    list.Add(new LdcInsnNode((float)value)); goto operate;
                case 3 when _double:
                    list.Add(new LdcInsnNode((double)value)); goto operate;
               operate:
                    list.Add(new InsnNode(operation));
                    return list;
            }

            replaces = -1;
            return list;
        }
        
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
            if (settings.Operations is Array)
                _operations = settings.Operations;
            else
                _operations = ((IEnumerable<object>)settings.Operations).Select(x => (byte)(long)x).ToArray();
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