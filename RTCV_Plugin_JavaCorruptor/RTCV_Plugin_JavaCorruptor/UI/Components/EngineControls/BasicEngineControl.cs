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

    public BasicEngineControl()
    {
        InitializeComponent();

        OpcodesWithNoOperands[] opcodes = (OpcodesWithNoOperands[])Enum.GetValues(typeof(OpcodesWithNoOperands));

        foreach (OpcodesWithNoOperands op in opcodes)
        {
            lbLimiters.Items.Add(op);
            lbValues.Items.Add(op);
        }
    }

    public override void Prepare()
    {
        base.Prepare();
        
        _limiters = lbLimiters.SelectedItems.Cast<OpcodesWithNoOperands>().Select(i => (byte) i).ToArray();
        _values = lbValues.SelectedItems.Cast<OpcodesWithNoOperands>().Select(i => (byte) i).ToArray();
        _noneSelectedWarningShown = false;
        _sameSelectedWarningShown = false;
    }
    public override void UpdateUI()
    {
        foreach (byte limiter in _limiters)
            lbLimiters.SetSelected(Array.IndexOf(lbLimiters.Items.Cast<OpcodesWithNoOperands>().ToArray(), (OpcodesWithNoOperands)limiter), true);
        foreach (byte value in _values)
            lbValues.SetSelected(Array.IndexOf(lbValues.Items.Cast<OpcodesWithNoOperands>().ToArray(), (OpcodesWithNoOperands)value), true);
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