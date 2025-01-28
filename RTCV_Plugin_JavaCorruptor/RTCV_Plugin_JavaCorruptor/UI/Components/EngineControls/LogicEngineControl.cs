using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ObjectWeb.Asm.Tree;
using static ObjectWeb.Asm.Opcodes;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class LogicEngineControl
{
    private int _mode;
    private bool[] _find = new bool[6];
    private byte[] _replace;
    private bool _int, _long, _float, _double, _boolean;

    private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbeReplace.Enabled = cbMode.SelectedIndex == 1;
    }

    public LogicEngineControl()
    {
        InitializeComponent();
        cbMode.SelectedIndex = 0;
        lbeFind.SetSelected(0, true);
        lbeFind.SetSelected(1, true);
        lbeFind.SetSelected(2, true);
        lbeFind.SetSelected(3, true);
        lbeFind.SetSelected(4, true);
        lbeFind.SetSelected(5, true);
    }

    public override void Prepare()
    {
        base.Prepare();
        _mode = cbMode.SelectedIndex;
        for (int i = 0; i < 6; i++)
            _find[i] = lbeFind.GetSelected(i);
        _replace = new byte[lbeReplace.SelectedIndices.Count];
        for (int i = 0; i < lbeReplace.SelectedIndices.Count; i++)
            _replace[i] = (byte)lbeReplace.SelectedIndices[i];
        _int = cbInt.Checked;
        _long = cbLong.Checked;
        _float = cbFloat.Checked;
        _double = cbDouble.Checked;
        _boolean = cbBoolean.Checked;
    }
    public override void UpdateUI()
    {
        cbMode.SelectedIndex = _mode;
        for (int i = 0; i < _find.Length; i++)
            lbeFind.SetSelected(i, _find[i]);
        foreach (byte value in _replace)
            lbeReplace.SetSelected(Array.IndexOf(lbeReplace.Items.Cast<byte>().ToArray(), value), true);
        cbInt.Checked = _int;
        cbLong.Checked = _long;
        cbFloat.Checked = _float;
        cbDouble.Checked = _double;
        cbBoolean.Checked = _boolean;
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            settings.Mode = _mode;
            settings.Find = _find;
            settings.Replace = _replace;
            settings.Int = _int;
            settings.Long = _long;
            settings.Float = _float;
            settings.Double = _double;
            settings.Boolean = _boolean;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            _mode = settings.Mode;
            if (settings.Find is Array)
                _find = settings.Find;
            else
                _find = ((IEnumerable<object>)settings.Find).Select(x => (bool)x).ToArray();
            if (settings.LimiterFunctions is Array)
                _replace = settings.Replace;
            else
                _replace = ((IEnumerable<object>)settings.Replace).Select(x => (byte)x).ToArray();
            _find = settings.Find;
            _replace = settings.Replace;
            _int = settings.Int;
            _long = settings.Long;
            _float = settings.Float;
            _double = settings.Double;
            _boolean = settings.Boolean;
        }
    }

    public override List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        List<AbstractInsnNode> list = [];

        int opcode = insn.Opcode;

        if (_int && opcode is >= If_Icmpeq and <= If_Icmple)
        {
            if (!_find[opcode - If_Icmpeq])
                return list;

            replaces = 1;
            JumpInsnNode node = (JumpInsnNode)insn;
            if (_mode == 0)
                list.Add(new JumpInsnNode(GetOpposite(opcode), node.Label));
            else
                list.Add(new JumpInsnNode(GetRandom(opcode - If_Icmpeq) + If_Icmpeq, node.Label));
        }
        else if (_long && opcode == Lcmp)
        {
            JumpInsnNode node = (JumpInsnNode)insn.Next;
            opcode = insn.Next.Opcode;
            if (!_find[opcode - Ifeq])
                return list;

            replaces = 2;
            list.Add(insn);
            if (_mode == 0)
                list.Add(new JumpInsnNode(GetOpposite(opcode), node.Label));
            else
                list.Add(new JumpInsnNode(GetRandom(opcode - Ifeq) + Ifeq, node.Label));

        }
        else if (_float && opcode is Fcmpg or Fcmpl)
        {
            JumpInsnNode node = (JumpInsnNode)insn.Next;
            int jumpOpcode = insn.Next.Opcode;
            if (!_find[jumpOpcode - Ifeq])
                return list;

            replaces = 2;
            if (_mode == 0)
            {
                list.Add(new InsnNode(opcode == Fcmpg ? Fcmpl : Fcmpg));
                list.Add(new JumpInsnNode(GetOpposite(jumpOpcode), node.Label));
            }
            else
            {
                int newOpcode = GetRandom(jumpOpcode - Ifeq) + Ifeq;

                if (newOpcode is Ifge or Ifgt)
                    list.Add(new InsnNode(Fcmpg));
                else
                    list.Add(new InsnNode(Fcmpl));

                list.Add(new JumpInsnNode(newOpcode, node.Label));
            }
        }
        else if (_double && opcode is Dcmpg or Dcmpl)
        {
            JumpInsnNode node = (JumpInsnNode)insn.Next;
            int jumpOpcode = insn.Next.Opcode;
            if (!_find[jumpOpcode - Ifeq])
                return list;

            replaces = 2;
            if (_mode == 0)
            {
                list.Add(new InsnNode(opcode == Dcmpg ? Dcmpl : Dcmpg));
                list.Add(new JumpInsnNode(GetOpposite(jumpOpcode), node.Label));
            }
            else
            {
                int newOpcode = GetRandom(jumpOpcode - Ifeq) + Ifeq;

                if (newOpcode is Ifge or Ifgt)
                    list.Add(new InsnNode(Dcmpg));
                else
                    list.Add(new InsnNode(Dcmpl));

                list.Add(new JumpInsnNode(newOpcode, node.Label));
            }
        }
        else if (_boolean && opcode is Ifne or Ifeq)
        {
            if (!_find[opcode - Ifeq])
                return list;

            AbstractInsnNode node = insn.Previous;
            replaces = 1;
            switch (node.Opcode)
            {
                case >= Invokevirtual and <= Invokeinterface when ((MethodInsnNode)node).Desc.EndsWith("Z"):
                case Getfield or Getstatic when ((FieldInsnNode)node).Desc.EndsWith("Z"):
                case Iload or Istore:
                    list.Add(new JumpInsnNode(GetOpposite(opcode), ((JumpInsnNode)insn).Label));
                    break;
            }
        }

        return list;
    }

    private static int GetOpposite(int opcode)
    {
        return opcode + (opcode % 2 == 1 ? 1 : -1);
    }

    /// <summary>
    /// Returns a random opcode different to the given one
    /// </summary>
    /// <param name="opcode">The opcode to avoid returning</param>
    /// <returns>An opcode that isn't <c>opcode</c></returns>
    private int GetRandom(int opcode)
    {
        int op;
        if (_replace.Length > 1)
        {
            int rand = JavaGeneralParametersForm.Random.Next(_replace.Length - 1);
            op = opcode >= rand ? _replace[rand + 1] : _replace[rand];
        }
        else
            op = _replace[0];

        return op;
    }
}