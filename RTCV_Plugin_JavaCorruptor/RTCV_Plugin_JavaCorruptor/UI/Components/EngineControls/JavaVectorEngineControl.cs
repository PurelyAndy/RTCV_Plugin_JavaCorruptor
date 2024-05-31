using System;
using System.Dynamic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using NLog;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class JavaVectorEngineControl
{
    private byte[] _limiters, _values;

    public JavaVectorEngineControl()
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
    }
    public override void UpdateUI()
    {
        foreach (byte limiter in _limiters)
            
            lbLimiters.SetSelected(Array.IndexOf(lbLimiters.Items.Cast<OpcodesWithNoOperands>().ToArray(), (OpcodesWithNoOperands)limiter), true);
        foreach (byte value in _values)
            lbValues.SetSelected(Array.IndexOf(lbValues.Items.Cast<OpcodesWithNoOperands>().ToArray(), (OpcodesWithNoOperands)value), true);
    }

    public override InsnList DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        InsnList list = new();

        if (_values.Length == 0 || _limiters.Length == 0)
        {
            Logger.Warn("No limiters or no operations selected.");
            return list;
        }
        
        if (Array.IndexOf(_limiters, (byte)insn.Opcode) != -1)
        {
            list.Add(new InsnNode(_values[JavaGeneralParametersForm.Random.Next(_values.Length)]));
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