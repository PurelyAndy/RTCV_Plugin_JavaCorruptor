using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using static ObjectWeb.Asm.Tree.AbstractInsnNode;

namespace Java_Corruptor;

public class AsmParser
{
    public readonly BidirectionalDictionary<LabelNode, string> LabelNames = new();

    private static readonly List<string> _precalculatedLabels;
    static AsmParser()
    {
        _precalculatedLabels = [];
        for (int i = 0; i < 2400; i++) //2400 is approximately the largest number of labels 
        {
            const int divisor = 26;
            int index = i;
            int length = 0;
            for (int temp = index; temp >= 0; temp = temp / divisor - 1)
                length++;

            char[] arr = new char[length];

            for (int j = length - 1; j >= 0; j--)
            {
                arr[j] = (char)('A' + index % 26);
                index = index / divisor - 1;
            }

            _precalculatedLabels.Add(new(arr));
        }
    }

    /// <summary>
    /// Gets the name for a label.
    /// </summary>
    /// <param name="index">The index of the label.</param>
    /// <returns>The name of the label.</returns>
    private static string GetNameForLabel(int index)
    {
        if (index < _precalculatedLabels.Count)
            return _precalculatedLabels[index];
        
        const int divisor = 26;

        int length = 0;
        for (int temp = index; temp >= 0; temp = temp / divisor - 1)
            length++;

        char[] arr = new char[length];

        for (int i = length - 1; i >= 0; i--)
        {
            arr[i] = (char)('A' + index % 26);
            index = index / divisor - 1;
        }

        return new(arr);
    }

    /// <summary>
    /// Registers all labels in the given instruction list.
    /// </summary>
    /// <param name="insns">The instruction list to register labels from.</param>
    public void RegisterLabelsFrom(InsnList insns)
    {
        //AbstractInsnNode[] a = insns.ToArray();
        //foreach (AbstractInsnNode insn in a)
        int i = 0;
        for (AbstractInsnNode insn = insns.First; insn is not null; insn = insn.Next, i++)
        {
            if (insn.Type == Label_Insn)
            {
                LabelNames.Add((LabelNode)insn, GetNameForLabel(i)); // insns.IndexOf() exists, but has an issue with cache invalidation i think
            }
        }
    }
    /// <summary>
    /// Registers all labels in the given instruction list.
    /// </summary>
    /// <param name="insns">The instruction list to register labels from.</param>
    public void RegisterLabelsFrom(AbstractInsnNode[] insns)
    {
        foreach (AbstractInsnNode insn in insns)
        {
            if (insn is null) continue;
            if (insn.Type == Label_Insn)
            {
                LabelNames.Add((LabelNode)insn, GetNameForLabel(Array.IndexOf(insns, insn))); // insns.IndexOf() exists, but has an issue with cache invalidation i think
            }
        }
    }

    /// <summary>
    /// Registers a label and returns the name given to it.
    /// </summary>
    /// <param name="label">The label to register.</param>
    /// <returns>The name given to the label.</returns>
    public string RegisterLabel(LabelNode label)
    {
        string nameForLabel = GetNameForLabel(LabelNames.Count);
        LabelNames.Add(label, nameForLabel);
        return nameForLabel;
    }

    /// <summary>
    /// Registers a label with a given name.
    /// </summary>
    /// <param name="label">The label to register.</param>
    /// <param name="name">The name to give to the label.</param>
    /// <param name="replace">Whether to replace the label if it already exists.</param>
    public void RegisterLabel(LabelNode label, string name, bool replace)
    {
        if (replace)
            LabelNames[label] = name;
        else
            LabelNames.Add(label, name);
    }

    /// <summary>
    /// Removes all labels from the label map.
    /// </summary>
    public void ClearLabels()
    {
        LabelNames.Clear();
    }

    /// <summary>
    /// Tries to get the name of a label.
    /// </summary>
    /// <param name="labelNode">The label to get the name of.</param>
    /// <returns>The name of the label, or "?" if it isn't registered.</returns>
    private string GetNameOfLabel(LabelNode labelNode)
    {
        return LabelNames.TryGetValue(labelNode, out string name) ? name : "?";
    }

    /// <summary>
    /// Tries to get the label with a given name.
    /// </summary>
    /// <param name="name">The name of the label to find.</param>
    /// <returns>The label, or a new label if it doesn't exist.</returns>
    private LabelNode GetLabelWithName(string name)
    {
        LabelNode labelNode = LabelNames[name];
        if (labelNode == null)
        {
            labelNode = new();
            LabelNames.Add(labelNode, name);
        }

        return labelNode;
    }

    /// <summary>
    /// Converts an instruction to a string.
    /// </summary>
    /// <param name="insn">The instruction to convert.</param>
    /// <returns>The string representation of the instruction.</returns>
    public string InsnToString(AbstractInsnNode insn)
    {
        string opcode = insn.Opcode != -1 ? AsmUtilities.Opcodes[insn.Opcode] : "";
        string args = "";
        switch (insn.Type)
        {
            case Insn:
                break;
            case Int_Insn:
                args = ((IntInsnNode)insn).Operand.ToString();
                break;
            case Var_Insn:
                args = ((VarInsnNode)insn).Var.ToString(); //TODO: variable names
                break;
            case Type_Insn:
                args = ((TypeInsnNode)insn).Desc;
                break;
            case Field_Insn:
                args = ((FieldInsnNode)insn).Owner + "." + ((FieldInsnNode)insn).Name + " " +
                       ((FieldInsnNode)insn).Desc;
                break;
            case Method_Insn:
                args = ((MethodInsnNode)insn).Owner + "." + ((MethodInsnNode)insn).Name +
                       ((MethodInsnNode)insn).Desc;
                break;
            case Jump_Insn:
                args = GetNameOfLabel(((JumpInsnNode)insn).Label);
                break;
            case Label_Insn:
                args = GetNameOfLabel((LabelNode)insn) + ":";
                break;
            case Ldc_Insn:
                LdcInsnNode ldcInsn = (LdcInsnNode)insn;

                switch (ldcInsn.Cst)
                {
                    case string:
                        args = "\"" + ldcInsn.Cst + "\"";
                        break;
                    case long:
                        args = ldcInsn.Cst + "L";
                        break;
                    case float:
                        args = ldcInsn.Cst + "F";
                        break;
                    case double:
                        args = ldcInsn.Cst + "D";
                        break;
                    case Handle cst:
                    {
                        int htag = cst.Tag;
                        string tag = AsmUtilities.Tags[htag];
                        args = "handle[" + tag + " " + cst.Owner + "." + cst.Name;
                        if (htag is >= Opcodes.H_Getfield and <= Opcodes.H_Putstatic)
                            args += " ";
                        args += cst.Desc + "]";
                        break;
                    }
                    default:
                        args = ldcInsn.Cst.ToString();
                        break;
                }

                break;
            case Iinc_Insn:
                args = ((IincInsnNode)insn).Var + " " + ((IincInsnNode)insn).Incr; //TODO: variable names
                break;
            case Tableswitch_Insn:
                TableSwitchInsnNode tableSwitchInsn = (TableSwitchInsnNode)insn;
                args = "range[" + tableSwitchInsn.Min + ":" + tableSwitchInsn.Max + "] labels[";
                for (int i = 0; i < tableSwitchInsn.Labels.Count; i++)
                {
                    args += GetNameOfLabel(tableSwitchInsn.Labels[i]);
                    if (i < tableSwitchInsn.Labels.Count - 1)
                        args += ", ";
                }

                args += "] default[" + GetNameOfLabel(tableSwitchInsn.Dflt) + "]";
                break;
            case Lookupswitch_Insn:
                LookupSwitchInsnNode lookupSwitchInsn = (LookupSwitchInsnNode)insn;
                args = "mapping[";
                for (int i = 0; i < lookupSwitchInsn.Keys.Count; i++)
                {
                    args += lookupSwitchInsn.Keys[i] + "=" + GetNameOfLabel(lookupSwitchInsn.Labels[i]);
                    if (i < lookupSwitchInsn.Keys.Count - 1)
                        args += ", ";
                }

                args += "] default[" + GetNameOfLabel(lookupSwitchInsn.Dflt) + "]";
                break;
            case Multianewarray_Insn:
                args = ((MultiANewArrayInsnNode)insn).Desc + " " + ((MultiANewArrayInsnNode)insn).Dims;
                break;
            case Line_Insn:
                args = "LINE " + GetNameOfLabel(((LineNumberNode)insn).Start) + " " + ((LineNumberNode)insn).Line;
                break;
            case Invoke_Dynamic_Insn:
            {
                InvokeDynamicInsnNode indyInsn = (InvokeDynamicInsnNode)insn;
                args = indyInsn.Name + " " + indyInsn.Desc + " handle[";

                Handle handle = indyInsn.Bsm;
                int htag = handle.Tag;
                string tag = AsmUtilities.Tags[htag];
                args += tag + " " + handle.Owner + "." + handle.Name;
                if (htag is >= Opcodes.H_Getfield and <= Opcodes.H_Putstatic)
                    args += " ";
                args += handle.Desc + "] args[";

                for (int i = 0; i < indyInsn.BsmArgs.Length; i++)
                {
                    object arg = indyInsn.BsmArgs[i];
                    switch (arg)
                    {
                        case string:
                            args += "\"" + arg + "\"";
                            break;
                        case JType or int:
                            args += arg;
                            break;
                        case long:
                            args += arg + "L";
                            break;
                        case float:
                            args += arg + "F";
                            break;
                        case double:
                            args += arg + "D";
                            break;
                        case Handle handleArg:
                        {
                            handle = handleArg;
                            htag = handle.Tag;
                            tag = AsmUtilities.Tags[htag];
                            args += "handle[" + tag + " " + handle.Owner + "." + handle.Name;
                            if (htag is >= Opcodes.H_Getfield and <= Opcodes.H_Putstatic)
                                args += " ";
                            args += handle.Desc + "]";
                            break;
                        }
                    }

                    if (i < indyInsn.BsmArgs.Length - 1)
                        args += ", ";
                }

                args += "]";
                break;
            }
            case Frame_Insn:
                //Probably unnecessary?
                break;
            default:
                throw new("Unknown instruction type: " + insn.Type);
        }

        return ((insn.Opcode == -1 ? "" : opcode + " ") + args).Trim();
    }

    /// <summary>
    /// Parses an instruction from a string.
    /// </summary>
    /// <param name="insn">The string to parse.</param>
    /// <returns>The instruction.</returns>
    public AbstractInsnNode ParseInsn(string insn)
    {
        string[] split = insn.Split(' ');
        int opcode;
        int type;
        if (split[0].EndsWith(":"))
        {
            opcode = 0;
            type = Label_Insn;
        }
        else
        {
            opcode = AsmUtilities.Opcodes[split[0].ToUpper()];
            type = AsmUtilities.Types[opcode];
        }

        switch (type)
        {
            case Line_Insn:
                return new LineNumberNode(int.Parse(split[2]), GetLabelWithName(split[1]));
            case Insn:
                return new InsnNode(opcode);
            case Int_Insn:
                return new IntInsnNode(opcode, int.Parse(split[1]));
            case Var_Insn:
                return new VarInsnNode(opcode, int.Parse(split[1]));
            case Type_Insn:
                return new TypeInsnNode(opcode, split[1]);
            case Iinc_Insn:
                return new IincInsnNode(int.Parse(split[1]), int.Parse(split[2]));
            case Multianewarray_Insn:
                return new MultiANewArrayInsnNode(split[1], int.Parse(split[2]));
            case Field_Insn:
                string[] fieldSplit = split[1].Split(new[] { "." }, StringSplitOptions.None);
                return new FieldInsnNode(opcode, fieldSplit[0], fieldSplit[1], split[2]);
            case Method_Insn:
                string[] methodSplit = split[1].Split(new[] { "." }, StringSplitOptions.None);
                if (methodSplit[1].StartsWith("<init>") || methodSplit[1].StartsWith("<clinit>"))
                {
                    int index = methodSplit[1].IndexOf('>');
                    string desc = methodSplit[1][(index + 1)..];
                    methodSplit[1] = methodSplit[1][..(index + 1)];
                    return new MethodInsnNode(opcode, methodSplit[0], methodSplit[1], desc, opcode == Opcodes.Invokespecial);
                }
                return new MethodInsnNode(opcode, methodSplit[0], methodSplit[1][..methodSplit[1].IndexOf('(')], methodSplit[1][methodSplit[1].IndexOf('(')..], opcode == Opcodes.Invokeinterface);
            case Ldc_Insn:
                if (split[1].StartsWith("\""))
                {
                    string combined = split[1..].Aggregate((a, b) => a + " " + b);
                    return new LdcInsnNode(combined[1..^1]);
                }
                if (split[1].EndsWith("L"))
                    return new LdcInsnNode(long.Parse(split[1][..^1]));
                if (split[1].EndsWith("F"))
                    return new LdcInsnNode(float.Parse(split[1][..^1]));
                if (split[1].EndsWith("D"))
                    return new LdcInsnNode(double.Parse(split[1][..^1]));
                if (split[1].EndsWith("I"))
                    return new LdcInsnNode(int.Parse(split[1][..^1]));

                if (split[1].StartsWith("handle"))
                {
                    int tag = AsmUtilities.Tags[split[1][7..]];
                    string owner = split[2][..split[2].LastIndexOf('.')];

                    if (tag is >= Opcodes.H_Getfield and <= Opcodes.H_Putstatic)
                    {
                        string name = split[2][(split[2].LastIndexOf('.') + 1)..];
                        return new LdcInsnNode(new Handle(tag, owner, name, split[3][..^1], false));
                    }
                    else
                    {
                        string name = split[2].Substring(split[2].LastIndexOf('.') + 1, split[2].IndexOf('(') - (split[2].LastIndexOf('.') + 1));
                        string descriptor = split[2].Substring(split[2].IndexOf('('), split[2].Length - split[2].IndexOf('(') - 1);
                        return new LdcInsnNode(new Handle(tag, owner, name, descriptor, tag == Opcodes.H_Invokeinterface));
                    }
                }
                
                if (bool.TryParse(split[1], out bool boolValue))
                    return new LdcInsnNode(boolValue);

                return new LdcInsnNode(int.Parse(split[1]));
            case Jump_Insn:
                return new JumpInsnNode(opcode, GetLabelWithName(split[1]));
            case Tableswitch_Insn:
                return new TableSwitchInsnNode(int.Parse(split[1]),
                    int.Parse(split[2]),
                    new(),
                    new LabelNode[int.Parse(split[2]) - int.Parse(split[1]) + 1]
                );
            case Lookupswitch_Insn:
                string[] splitting = insn.Split('=');
                int[] keys = new int[splitting.Length - 1];
                LabelNode[] labels = new LabelNode[splitting.Length - 1];

                keys[0] = int.Parse(splitting[0][(splitting[0].LastIndexOf('[') + 1)..]);
                for (int i = 1; i < keys.Length; i++)
                {
                    keys[i] = int.Parse(splitting[i][(splitting[i].LastIndexOf(' ') + 1)..]);
                    labels[i - 1] = GetLabelWithName(splitting[i][..splitting[i].IndexOf(',')]);
                }

                string lastElement = splitting[^1];

                labels[^1] = GetLabelWithName(lastElement[..lastElement.IndexOf(']')]);
                LabelNode defaultLabel = GetLabelWithName(lastElement.Substring(lastElement.LastIndexOf('[') + 1, lastElement.LastIndexOf(']') - (lastElement.LastIndexOf('[') + 1)));

                return new LookupSwitchInsnNode(defaultLabel, keys, labels);
            case Label_Insn:
                return GetLabelWithName(split[0][..^1]);
            case Frame_Insn:
            //return new FrameNode(opcode, 0, new Object[0], 0, new Object[0]);
            default:
                throw new("Unknown instruction type: " + type);
        }
    }
}