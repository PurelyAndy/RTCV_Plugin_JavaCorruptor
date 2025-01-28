using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class FunctionEngineControl
{
    private string[] _limiters, _values;
    private bool _noneSelectedWarningShown, _sameSelectedWarningShown;
    public FunctionEngineControl() => InitializeComponent();

    public override void Prepare()
    {
        base.Prepare();
        _limiters = lbLimiterFunctions.SelectedItems.Cast<string>().ToArray();
        _values = lbValueFunctions.SelectedItems.Cast<string>().ToArray();
        _noneSelectedWarningShown = false;
        _sameSelectedWarningShown = false;
    }

    public override void UpdateUI()
    {
        foreach (string limiter in _limiters)
            lbLimiterFunctions.SetSelected(Array.IndexOf(lbLimiterFunctions.Items.Cast<string>().ToArray(), limiter), true);
        foreach (string value in _values)
            lbValueFunctions.SetSelected(Array.IndexOf(lbValueFunctions.Items.Cast<string>().ToArray(), value), true);
    }

    public override List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        List<AbstractInsnNode> list = [];
        if (_limiters.Length == 0 || _values.Length == 0)
        {
            if (!_noneSelectedWarningShown)
            {
                Logger.Warn("No limiters or no values selected");
                _noneSelectedWarningShown = true;
            }
            return list;
        }
        if (_limiters.Length == 1 && _values.Length == 1 && _limiters[0] == _values[0])
        {
            if (!_sameSelectedWarningShown)
            {
                Logger.Warn("Limiter and value are the same");
                _sameSelectedWarningShown = true;
            }
            return list;
        }
        if (insn.Opcode != Opcodes.Invokestatic)
            return list;

        MethodInsnNode methodInsnNode = (MethodInsnNode)insn;
        if (methodInsnNode.Owner != "java/lang/Math" || methodInsnNode.Desc != "(D)D")
            return list;

        int index = Array.IndexOf(_limiters, methodInsnNode.Name);
        if (index != -1)
        {
            //TODO: leaving this unfinished code here. it's impossible to implement this without being able to store the value in a variable because the SWAP instruction doesn't work with 8-byte types
            /*if (_runtimeRandom)
            {
                // Method method = Math.TYPE.getMethod((new String[]{"func1(D)D", "func2(D)D", "func3(D)D"})[(int)(Math.random() * 3)], Double.TYPE);
                // method.invoke(null, new Object[]{value});
                list.Add(new LdcInsnNode(JType.GetObjectType("java/lang/Math")));                   // java/lang/Math value
                list.Add(new LdcInsnNode(_values.Length));                                          // _values.Length java/lang/Math va;ue
                list.Add(new TypeInsnNode(Opcodes.Anewarray, "java/lang/String"));  //
                for (int i = 0; i < _values.Length; i++)
                {
                    list.Add(new InsnNode(Opcodes.Dup));
                    list.Add(new LdcInsnNode(i));
                    list.Add(new LdcInsnNode(_values[i]));
                    list.Add(new InsnNode(Opcodes.Aastore));
                }
                
                list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
                list.Add(new LdcInsnNode(_values.Length));
                list.Add(new InsnNode(Opcodes.Dmul));
                list.Add(new InsnNode(Opcodes.D2I));
                list.Add(new InsnNode(Opcodes.Aaload));
                
                list.Add(new InsnNode(Opcodes.Iconst_1));
                list.Add(new TypeInsnNode(Opcodes.Anewarray, "java/lang/Class"));
                list.Add(new InsnNode(Opcodes.Dup));
                list.Add(new InsnNode(Opcodes.Iconst_0));
                list.Add(new FieldInsnNode(Opcodes.Getstatic, "java/lang/Double", "TYPE", "Ljava/lang/Class;"));
                list.Add(new InsnNode(Opcodes.Aastore));
                
                list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/Class", "getMethod", "(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;", false));
                list.Add(new InsnNode(Opcodes.Aconst_Null));
                list.Add(new InsnNode(Opcodes.Iconst_1));
                list.Add(new TypeInsnNode(Opcodes.Anewarray, "java/lang/Object"));
                list.Add(new InsnNode(Opcodes.Dup));
                list.Add(new InsnNode(Opcodes.Iconst_0));
                
            }
            else
            {*/
                string newMethod;
                if (_values.Length > 1)
                {
                    int rand = JavaGeneralParametersForm.Random.Next(_values.Length - 1);
                    newMethod = rand >= index ? _values[rand + 1] : _values[rand];
                }
                else
                    newMethod = _values[0];
                if (newMethod == methodInsnNode.Name)
                    return list;
                if (newMethod == "POP,random()")
                {
                    list.Add(new InsnNode(Opcodes.Pop));
                    list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
                }
                else if (newMethod == "round" && methodInsnNode.Name != "round")
                {
                    list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "round", "(D)J", false));
                    list.Add(new InsnNode(Opcodes.L2D));
                }
                else if (methodInsnNode.Name == "round" && newMethod != "round")
                {
                    methodInsnNode.Name = newMethod;
                    list.Add(methodInsnNode.Clone());
                    list.Add(new InsnNode(Opcodes.D2L));
                }
                else
                {
                    methodInsnNode.Name = newMethod;
                    list.Add(methodInsnNode.Clone());
                }
            //}
        }
        else
            list.Add(insn);

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
            settings.LimiterFunctions = _limiters;
            settings.ValueFunctions = _values;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            if (settings.LimiterFunctions is Array)
                _limiters = settings.LimiterFunctions;
            else
                _limiters = ((IEnumerable<object>)settings.LimiterFunctions).Select(x => (string)x).ToArray();
            if (settings.ValueFunctions is Array)
                _values = settings.ValueFunctions;
            else
                _values = ((IEnumerable<object>)settings.ValueFunctions).Select(x => (string)x).ToArray();
        }
    }
}