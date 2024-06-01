using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.CorruptCore;
using static ObjectWeb.Asm.Tree.AbstractInsnNode;

namespace Java_Corruptor;

public class AsmParser
{
    public readonly BidirectionalDictionary<LabelNode, string> LabelNames = new();

    private static readonly List<string> _precalculatedLabels;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    static AsmParser()
    {
        _logger.Info("Precalculating label names...");
        _precalculatedLabels = [];
        for (int i = 0; i < 100000; i++) // 100000 is mostly arbitrary, this doesn't take long anyway. some libraries have *really* long methods.
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
        _logger.Info("Precalculated label names.");
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
        
        _logger.Debug($"Label index {index} is out of range for precalculated labels, calculating manually.");
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
    /// <returns>The name of the label, or if it isn't registered, registers it and returns the name.</returns>
    ///// <returns>The name of the label, or "?" if it isn't registered.</returns>
    private string GetNameOfLabel(LabelNode labelNode)
    {
        //return LabelNames.TryGetValue(labelNode, out string name) ? name : "?";
        if (LabelNames.TryGetValue(labelNode, out string name))
            return name;

        string nameForLabel = RtcCore.RND.Next().ToString();//GetNameForLabel(LabelNames.Count);
        LabelNames.Add(labelNode, nameForLabel);
        return nameForLabel;
    }

    /// <summary>
    /// Tries to get the label with a given name.
    /// </summary>
    /// <param name="name">The name of the label to find.</param>
    /// <returns>The label, or a new label if it doesn't exist.</returns>
    private LabelNode GetLabelWithName(string name)
    {
        LabelNames.TryGetValue(name, out LabelNode labelNode);
        if (labelNode != null)
            return labelNode;
        
        labelNode = new();
        LabelNames.Add(labelNode, name);
        return labelNode;
    }

    /// <summary>
    /// Converts an instruction to a string.
    /// </summary>
    /// <param name="insn">The instruction to convert.</param>
    /// <returns>The string representation of the instruction.</returns>
    public string InsnToString(AbstractInsnNode insn)
    {
        if (!AsmUtilities.Opcodes.TryGetValue(insn.Opcode, out string op)) // Shouldn't happen, but did once
        {
            _logger.Error($"Unknown opcode encountered: {insn.Opcode}, insn.Type: {insn.Type}");
            MessageBox.Show($"UNKNOWN OPCODE ENCOUNTERED: {insn.Opcode}\nINSN TYPE: {insn.Type}\nPlease report this to @purely_andy.");
            return "UNKNOWN OPCODE " + insn.Opcode;
        }
        string opcode = insn.Opcode != -1 ? op : "";
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
    /// Validates a string that represents an instruction. Only checks for syntax errors.
    /// </summary>
    /// <param name="insn">The instruction to validate.</param>
    /// <param name="message">The details of in what way the instruction is invalid, or null if it is valid. If the instruction is invalid for multiple reasons, the first reason encountered will be returned.</param>
    /// <returns>Whether the instruction is valid.</returns>
    public bool ValidateInsn(string insn, out string message)
    {
        message = null;
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
            if (AsmUtilities.Opcodes.TryGetValue(split[0], out opcode))
                type = AsmUtilities.Types[opcode];
            else
            {
                message = "Unknown opcode: " + split[0];
                return false;
            }
        }
        
        switch (type)
        {
            case Line_Insn:
                if (split.Length < 3)
                {
                    message = $"Line instruction {insn} is missing a line number.";
                    return false;
                }
                if (split.Length < 2)
                {
                    message = $"Line instruction {insn} is missing a label and a line number.";
                    return false;
                }
                if (split.Length > 3)
                {
                    message = $"Line instruction {insn} has too many arguments.";
                    return false;
                }
                if (int.TryParse(split[2], out _))
                    return true;

                message = $"Line instruction {insn} has an invalid line number: {split[2]}";
                return false;
            case Insn:
                if (split.Length > 1)
                {
                    message = $"Instruction {insn} has too many arguments.";
                    return false;
                }
                return true;
            case Int_Insn:
                if (split.Length < 2)
                {
                    message = $"Integer instruction {insn} is missing an operand.";
                    return false;
                }
                if (int.TryParse(split[1], out _))
                    return true;
                
                message = $"Integer instruction {insn} has an invalid integer: {split[1]}";
                return false;
            case Var_Insn:
                if (split.Length < 2)
                {
                    message = $"Variable instruction {insn} is missing a variable index.";
                    return false;
                }
                if (int.TryParse(split[1], out _))
                    return true;
                message = $"Variable instruction {insn} has an invalid variable index: {split[1]}";
                return false;
            case Type_Insn:
                switch (split.Length)
                {
                    case < 2:
                        message = $"Type instruction {insn} is missing a descriptor.";
                        return false;
                    case > 2:
                        message = $"Type instruction {insn} has too many arguments.";
                        return true;
                    default:
                        return true;
                }

            case Iinc_Insn:
                if (split.Length < 3)
                    message = $"Iinc instruction {insn} requires an amount to increment by.";
                else if (split.Length < 2)
                    message = $"Iinc instruction {insn} requires a variable index and an amount to increment by.";
                else if (split.Length > 3)
                    message = $"Iinc instruction {insn} has too many arguments.";
                else if (!int.TryParse(split[1], out _))
                    message = $"Iinc instruction {insn} has an invalid variable index: {split[1]}";
                else if (!int.TryParse(split[2], out _))
                    message = $"Iinc instruction {insn} has an invalid increment amount: {split[2]}";
                else
                    return true;
                return false;
            case Multianewarray_Insn:
                if (split.Length < 3)
                {
                    message = $"Multianewarray instruction {insn} requires a dimension count.";
                    return false;
                }
                if (split.Length < 2)
                {
                    message = $"Multianewarray instruction {insn} requires a descriptor and a dimension count.";
                    return false;
                }
                if (split.Length > 3)
                {
                    message = $"Multianewarray instruction {insn} has too many arguments.";
                    return false;
                }
                if (int.TryParse(split[2], out _))
                    return true;
                message = $"Multianewarray instruction {insn} has an invalid dimension count: {split[2]}";
                return false;
            case Field_Insn:
                if (split.Length < 2)
                {
                    message = $"Field instruction {insn} requires an identifier in the format owner.name desc";
                    return false;
                }
                if (split.Length > 2)
                {
                    message = $"Field instruction {insn} has too many arguments.";
                    return false;
                }
                string[] fieldSplit = split[1].Split('.');
                if (fieldSplit.Length == 3)
                    return true;
                message = $"Field instruction {insn} has a malformed identifier {split[1]} - expected: owner.name desc";
                return false;
            case Method_Insn:
                if (split.Length < 2)
                {
                    message = $"Method instruction {insn} requires an identifier in the format owner.name(args)type";
                    return false;
                }
                if (split.Length > 2)
                {
                    message = $"Method instruction {insn} has too many arguments.";
                    return false;
                }
                string[] methodSplit = split[1].Split('.');
                if (methodSplit.Length < 2)
                {
                    message = $"Method instruction {insn} requires an identifier in the format owner.name(args)type";
                    return false;
                }
                if (methodSplit[1].StartsWith("<init>") || methodSplit[1].StartsWith("<clinit>"))
                {
                    int index = methodSplit[1].IndexOf('>');
                    if (methodSplit[1].Length - (index + 1) < 3)
                    {
                        message = $"Method instruction {insn} has an invalid method descriptor: {methodSplit[1][(index + 1)..]}";
                        return false;
                    }
                    return true;
                }
                //return new MethodInsnNode(opcode, methodSplit[0], methodSplit[1][..methodSplit[1].IndexOf('(')], methodSplit[1][methodSplit[1].IndexOf('(')..], opcode == Opcodes.Invokeinterface);
                int ind = methodSplit[1].IndexOf('(');
                if (methodSplit[1].Length - ind < 3)
                {
                    message = $"Method instruction {insn} has an invalid method descriptor: {methodSplit[1][ind..]}";
                    return false;
                }
                return true;
            case Ldc_Insn:
                if (split.Length < 2)
                {
                    message = $"Ldc instruction {insn} requires a constant.";
                    return false;
                }
                if (split[1].StartsWith("\""))
                {
                    if (split[1].EndsWith("\"")) return true;
                    message = $"Ldc instruction {insn} has an unterminated string constant.";
                    return false;
                }
                if (split[1].EndsWith("L"))
                {
                    if (split.Length > 2)
                    {
                        message = $"Ldc instruction {insn} has too many arguments.";
                        return false;
                    }
                    if (long.TryParse(split[1][..^1], out _))
                        return true;
                    message = $"Ldc instruction {insn} has an invalid long constant: {split[1][..^1]}";
                    return false;
                }
                if (split[1].EndsWith("F"))
                {
                    if (split.Length > 2)
                    {
                        message = $"Ldc instruction {insn} has too many arguments.";
                        return false;
                    }
                    if (float.TryParse(split[1][..^1], out _))
                        return true;
                    message = $"Ldc instruction {insn} has an invalid float constant: {split[1][..^1]}";
                    return false;
                }
                if (split[1].EndsWith("D"))
                {
                    if (split.Length > 2)
                    {
                        message = $"Ldc instruction {insn} has too many arguments.";
                        return false;
                    }
                    if (double.TryParse(split[1][..^1], out _))
                        return true;
                    message = $"Ldc instruction {insn} has an invalid double constant: {split[1][..^1]}";
                    return false;
                }
                if (split[1].EndsWith("I"))
                {
                    if (split.Length > 2)
                    {
                        message = $"Ldc instruction {insn} has too many arguments.";
                        return false;
                    }
                    if (int.TryParse(split[1][..^1], out _))
                        return true;
                    message = $"Ldc instruction {insn} has an invalid integer constant: {split[1][..^1]}";
                    return false;
                }

                if (split[1].StartsWith("handle"))
                {
                    if (split.Length < 2)
                    {
                        message = $"Handle ldc instruction {insn} requires a handle tag and an identifier.";
                        return false;
                    }
                    if (!AsmUtilities.Tags.TryGetValue(split[1][7..], out int tag))
                    {
                        message = $"Handle ldc instruction {insn} has an invalid handle tag: {split[1][7..]}";
                        return false;
                    }
                    if (split.Length < 3)
                    {
                        if (tag > Opcodes.H_Putstatic)
                            message = $"Handle ldc instruction {insn} requires an identifier.";
                        else
                            message = $"Handle ldc instruction {insn} requires an owner/name and a descriptor.";
                        return false;
                    }

                    if (tag is >= Opcodes.H_Getfield and <= Opcodes.H_Putstatic)
                    {
                        if (split.Length < 4)
                        {
                            message = $"Handle ldc instruction {insn} requires a descriptor.";
                            return false;
                        }
                        if (split.Length > 4)
                        {
                            message = $"Handle ldc instruction {insn} has too many arguments.";
                            return false;
                        }

                        return true;
                    }
                    
                    if (split.Length > 3)
                    {
                        message = $"Handle ldc instruction {insn} has too many arguments.";
                        return false;
                    }
                    if (split[2].LastIndexOf('.') == -1 || split[2].IndexOf('(') == -1)
                    {
                        message = $"Handle ldc instruction {insn} has an invalid identifier: {split[2]}";
                        return false;
                    }

                    return true;
                }
                
                if (split.Length > 2)
                {
                    message = $"Ldc instruction {insn} has too many arguments.";
                    return false;
                }

                if (bool.TryParse(split[1], out _) || int.TryParse(split[1], out _))
                    return true;
                
                message = $"Ldc instruction {insn} has an invalid constant: {split[1]}";
                return false;
            case Jump_Insn:
                if (split.Length == 2)
                    return true;
                if (split.Length > 2)
                {
                    message = $"Jump instruction {insn} has too many arguments.";
                    return false;
                }
                message = $"Jump instruction {insn} requires a label.";
                return false;
            case Tableswitch_Insn: //BUG this needs to be implemented
                switch (split.Length)
                {
                    case < 2:
                        message = $"Tableswitch instruction {insn} requires a range.";
                        return false;
                    case < 3:
                        message = $"Tableswitch instruction {insn} requires a max.";
                        return false;
                    case > 3:
                        message = $"Tableswitch instruction {insn} has too many arguments.";
                        return false;
                    default:
                        return true;
                }
            case Lookupswitch_Insn:
                string[] splitting = insn.Split('=');
                if (splitting.Length < 2)
                {
                    message = $"Lookupswitch instruction {insn} is malformed. It should look something like: LOOKUPSWITCH mapping[val=LBL1, val2=LBL2] default[LBL3]";
                    return false;
                }
                if (splitting[0].LastIndexOf('[') == -1)
                {
                    message = $"Lookupswitch instruction {insn} is malformed. It should look something like: LOOKUPSWITCH mapping[val=LBL1, val2=LBL2] default[LBL3]";
                    return false;
                }
                for (int i = 1; i < splitting.Length; i++)
                {
                    if (splitting[i].LastIndexOf(' ') == -1 || splitting[i].IndexOf(',') == -1)
                    {
                        message = $"Lookupswitch instruction {insn} is malformed. It should look something like: LOOKUPSWITCH mapping[val=LBL1, val2=LBL2] default[LBL3]";
                        return false;
                    }
                }

                string lastElement = splitting[^1];
                if (lastElement.LastIndexOf('[') == -1 || lastElement.LastIndexOf(']') == -1)
                {
                    message = $"Lookupswitch instruction {insn} is malformed. It should look something like: LOOKUPSWITCH mapping[val=LBL1, val2=LBL2] default[LBL3]";
                    return false;
                }
                
                return true;
            case Label_Insn:
                if (split.Length > 1)
                {
                    message = $"Label instruction {insn} has too many arguments.";
                    return false;
                }
                return true; // If it got detected as a label, it's valid
            case Frame_Insn:
            //return new FrameNode(opcode, 0, new Object[0], 0, new Object[0]);
            default:
                message = "Unknown instruction type " + type + " from opcode " + opcode;
                return false;
        }
    }

    /// <summary>
    /// Parses a known-good instruction from a string.
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
            if (AsmUtilities.Opcodes.TryGetValue(split[0], out opcode))
                type = AsmUtilities.Types[opcode];
            else
                throw new InvalidInstructionException("Unknown opcode: " + split[0]);
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
                string[] fieldSplit = split[1].Split('.');
                return new FieldInsnNode(opcode, fieldSplit[0], fieldSplit[1], split[2]);
            case Method_Insn:
                string[] methodSplit = split[1].Split('.');
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
                    return new LdcInsnNode(combined[1..^1]); // BUG: is this supposed to be [1..^1] or [..^1]?
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
                return new TableSwitchInsnNode(int.Parse(split[1]), // BUG: implement this
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
                throw new InvalidInstructionException("Unknown instruction type " + type + " from opcode " + opcode);
        }
    }
}