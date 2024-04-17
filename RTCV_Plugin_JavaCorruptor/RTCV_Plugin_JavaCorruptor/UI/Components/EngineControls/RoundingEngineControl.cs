using System;
using System.Dynamic;
using System.Linq;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class RoundingEngineControl
{
    //TODO: although this is a double, the math doesn't work out for decimal amounts of places. 
    // to elaborate here's an example. (2.56 * (10 ^ 2)) / (10 ^ 1) = 2.6. (2.56 * (10 ^ 2)) / (10 ^ 2) = 2.56.
    // but what if we do (2.56 * (10 ^ 1.5)) / (10 ^ 1.5)? you might expect it to be 2.58, since that's halfway between 2.5 and 2.6, but it's not because that's not how math works.
    // i would like it to be 2.58, but i'm not sure what the proper equation is.
    // i'm going to leave it as is for now because maybe some interesting pattern comes out of it anyway.
    private double _places;
    private bool _constants, _mathOperations, _variableLoads, _fieldLoads, _returnValues;
    private byte _operations;
    private bool _int, _long, _float, _double;

    public RoundingEngineControl()
    {
        InitializeComponent();
    }

    public override void Prepare()
    {
        base.Prepare();
        _places = Math.Pow(10, tbDecimalPlaces.Value / 1000d);
        _constants = lbeKinds.SelectedIndices.Contains(0);
        _mathOperations = lbeKinds.SelectedIndices.Contains(1);
        _variableLoads = lbeKinds.SelectedIndices.Contains(2);
        _fieldLoads = lbeKinds.SelectedIndices.Contains(3);
        _returnValues = lbeKinds.SelectedIndices.Contains(4);
        for (int i = 0; i < lbeOperations.SelectedIndices.Count; i++)
            _operations += (byte)Math.Pow(2, lbeOperations.SelectedIndices[i]);
        _int = cbInt.Checked;
        _long = cbLong.Checked;
        _float = cbFloat.Checked;
        _double = cbDouble.Checked;
    }
    public override void UpdateUI()
    {
        tbDecimalPlaces.Value = (int)Math.Log10(_places) * 1000;
        lbDecimalPlaces.Text = $"Round to {tbDecimalPlaces.Value / 1000d} decimal places";
        lbeKinds.SetSelected(0, _constants);
        lbeKinds.SetSelected(1, _mathOperations);
        lbeKinds.SetSelected(2, _variableLoads);
        lbeKinds.SetSelected(3, _fieldLoads);
        lbeKinds.SetSelected(4, _returnValues);
        for (int i = 0; i < 4; i++)
            lbeOperations.SetSelected(i, (_operations & (int)Math.Pow(2, i)) == (int)Math.Pow(2, i));
        cbInt.Checked = _int;
        cbLong.Checked = _long;
        cbFloat.Checked = _float;
        cbDouble.Checked = _double;
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            settings.DecimalPlaces = _places;
            settings.Constants = _constants;
            settings.MathOperations = _mathOperations;
            settings.VariableLoads = _variableLoads;
            settings.FieldLoads = _fieldLoads;
            settings.ReturnValues = _returnValues;
            settings.Operations = _operations;
            settings.Int = _int;
            settings.Long = _long;
            settings.Float = _float;
            settings.Double = _double;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            _places = settings.DecimalPlaces;
            _constants = settings.Constants;
            _mathOperations = settings.MathOperations;
            _variableLoads = settings.VariableLoads;
            _fieldLoads = settings.FieldLoads;
            _returnValues = settings.ReturnValues;
            _operations = settings.Operations;
            _int = settings.Int;
            _long = settings.Long;
            _float = settings.Float;
            _double = settings.Double;
        }
    }

    private void tbRounding_Scroll(object sender, EventArgs e)
    {
        lbDecimalPlaces.Text = $"Round to {tbDecimalPlaces.Value / 1000d} decimal places";
    }

    public override InsnList DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        InsnList list = new();

        int opcode = insn.Opcode;

        if (insn is LdcInsnNode && _constants)
            RoundConstants(insn, list);
        else if (insn.Opcode is >= 0x60 and <= 0x6f && _mathOperations)
            RoundMathOperations(insn, list, opcode);
        else if (opcode is >= 0x15 and <= 0x18 && _variableLoads)
            RoundVariableLoads(insn, list, opcode);
        else if (opcode is >= 0x1a and <= 0x29 && _variableLoads)
            RoundVariableLoadsConst(insn, list, opcode);
        else if (insn.Opcode is Opcodes.Getfield or Opcodes.Getstatic && _fieldLoads)
            RoundFieldLoads(insn, list);
        else if (insn.Opcode is Opcodes.Invokestatic or Opcodes.Invokevirtual or Opcodes.Invokespecial && _returnValues)
            RoundMethodReturnValues(insn, list);
        else
            return list;

        replaces = 1;
        return list;
    }

    private void RoundMethodReturnValues(AbstractInsnNode insn, InsnList list)
    {
        list.Add(insn);
        MethodInsnNode node = (MethodInsnNode)insn;
        string desc = node.Desc;
        if (desc.EndsWith(")I") && _int)
        {
            list.Add(new InsnNode(Opcodes.I2D));
            AddRoundingInstructions(list);
            list.Add(new InsnNode(Opcodes.D2I));
        }
        else if (desc.EndsWith(")J") && cbLong.Checked)
        {
            list.Add(new InsnNode(Opcodes.L2D));
            AddRoundingInstructions(list);
            list.Add(new InsnNode(Opcodes.D2L));
        }
        else if (desc.EndsWith(")F") && _float)
        {
            list.Add(new InsnNode(Opcodes.F2D));
            AddRoundingInstructions(list);
            list.Add(new InsnNode(Opcodes.D2F));
        }
        else if (desc.EndsWith(")D") && _double)
            AddRoundingInstructions(list);
    }

    private void RoundFieldLoads(AbstractInsnNode insn, InsnList list)
    {
        list.Add(insn);
        FieldInsnNode node = (FieldInsnNode)insn;
        string desc = node.Desc;
        switch (desc)
        {
            case "I" when _int:
                list.Add(new InsnNode(Opcodes.I2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2I));
                break;
            case "J" when _long:
                list.Add(new InsnNode(Opcodes.L2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2L));
                break;
            case "F" when _float:
                list.Add(new InsnNode(Opcodes.F2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2F));
                break;
            case "D" when _double:
                AddRoundingInstructions(list);
                break;
        }
    }

    private void RoundVariableLoadsConst(AbstractInsnNode insn, InsnList list, int opcode)
    {
        list.Add(insn);
        if (opcode <= 0x1d)
        {
            if (_int)
            {
                list.Add(new InsnNode(Opcodes.I2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2I));
            }
        }
        else if (opcode <= 0x21)
        {
            if (_long)
            {
                list.Add(new InsnNode(Opcodes.L2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2L));
            }
        }
        else if (opcode <= 0x25)
        {
            if (_float)
            {
                list.Add(new InsnNode(Opcodes.F2D));
                AddRoundingInstructions(list);
                list.Add(new InsnNode(Opcodes.D2F));
            }
        }
        else if (opcode <= 0x29)
        {
            if (_double)
                AddRoundingInstructions(list);
        }
    }

    private void RoundVariableLoads(AbstractInsnNode insn, InsnList list, int opcode)
    {
        list.Add(insn);
        switch (opcode)
        {
            case Opcodes.Iload:
                if (_int)
                {
                    list.Add(new InsnNode(Opcodes.I2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2I));
                }

                break;
            case Opcodes.Lload:
                if (_long)
                {
                    list.Add(new InsnNode(Opcodes.L2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2L));
                }

                break;
            case Opcodes.Fload:
                if (_float)
                {
                    list.Add(new InsnNode(Opcodes.F2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2F));
                }

                break;
            case Opcodes.Dload:
                if (_double)
                    AddRoundingInstructions(list);
                break;
        }
    }

    private void RoundMathOperations(AbstractInsnNode insn, InsnList list, int opcode)
    {
        list.Add(insn);
        int operation = (int)Math.Pow(2, (opcode - 0x60) / 4);
        if ((_operations & operation) != operation)
            return;

        switch (opcode % 4)
        {
            case 0:
                if (_int)
                {
                    list.Add(new InsnNode(Opcodes.I2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2I));
                }

                break;
            case 1:
                if (_long)
                {
                    list.Add(new InsnNode(Opcodes.L2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2L));
                }

                break;
            case 2:
                if (_float)
                {
                    list.Add(new InsnNode(Opcodes.F2D));
                    AddRoundingInstructions(list);
                    list.Add(new InsnNode(Opcodes.D2F));
                }

                break;
            case 3:
                if (_double)
                    AddRoundingInstructions(list);
                break;
        }
    }

    private void RoundConstants(AbstractInsnNode insn, InsnList list)
    {
        LdcInsnNode node = (LdcInsnNode)insn;
        switch (node.Cst)
        {
            case int i when _int:
                list.Add(new LdcInsnNode(i));
                break;
            case long l when _long:
                list.Add(new LdcInsnNode(l));
                break;
            case float f when _float:
                list.Add(new LdcInsnNode((float)(Math.Round(f * _places) / _places)));
                break;
            case double d when _double:
                list.Add(new LdcInsnNode(Math.Round(d * _places) / _places));
                break;
            default:
                list.Add(insn);
                break;
        }
    }

    private void AddRoundingInstructions(InsnList list)
    {
        list.Add(new LdcInsnNode(_places));
        list.Add(new InsnNode(Opcodes.Dmul));
        list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "round", "(D)J", false));
        list.Add(new InsnNode(Opcodes.L2D));
        list.Add(new LdcInsnNode(_places));
        list.Add(new InsnNode(Opcodes.Ddiv));
    }
}