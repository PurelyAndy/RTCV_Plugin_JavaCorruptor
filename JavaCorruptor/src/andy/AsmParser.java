// This class heavily references Recaf's disassembler/assembler, so I think I have to put this here.
/*
The MIT License (MIT)

Copyright (c) 2017-2021 Matthew Coley

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

package andy;

import com.google.common.collect.HashBiMap;
import org.objectweb.asm.Handle;
import org.objectweb.asm.Opcodes;
import org.objectweb.asm.Type;
import org.objectweb.asm.tree.*;

import static andy.AsmUtilities.tags;
import static org.objectweb.asm.tree.AbstractInsnNode.*;

public class AsmParser {
    private static final HashBiMap<LabelNode, String> labelNames = HashBiMap.create();

    /**
     * Gets the name for a label.
     * @param index The index of the label.
     * @return The name of the label.
     */
    private static String getNameForLabel(int index) {
        final String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        final int base = alphabet.length();
        StringBuilder sb = new StringBuilder();
        while (index >= 0) {
            sb.append(alphabet.charAt(index % base));
            index = index / base - 1;
        }
        return sb.reverse().toString();
    }

    /**
     * Registers all labels in the given instruction list.
     * @param insns The instruction list to register labels from.
     */
    public static void registerLabelsFrom(InsnList insns) {
        for (AbstractInsnNode insn : insns.toArray()) {
            if (insn.getType() == LABEL) {
                labelNames.put((LabelNode) insn, getNameForLabel(insns.indexOf(insn)));
            }
        }
    }

    /**
     * Registers a label and returns the name given to it.
     * @param label The label to register.
     * @return The name given to the label.
     */
    public static String registerLabel(LabelNode label) {
        String nameForLabel = getNameForLabel(labelNames.size());
        labelNames.put(label, nameForLabel);
        return nameForLabel;
    }

    /**
     * Registers a label with a given name.
     * @param label The label to register.
     * @param name The name to give to the label.
     * @param replace Whether to replace the label if it already exists.
     */
    public static void registerLabel(LabelNode label, String name, boolean replace) {
        if (replace)
            labelNames.forcePut(label, name);
        else
            labelNames.put(label, name);
    }

    /**
     * Removes all labels from the label map.
     */
    public static void clearLabels() {
        labelNames.clear();
    }

    /**
     * Tries to get the name of a label.
     * @param labelNode The label to get the name of.
     * @return The name of the label, or "?" if it isn't registered.
     */
    private static String getNameOfLabel(LabelNode labelNode) {
        return labelNames.getOrDefault(labelNode, "?");
    }

    /**
     * Tries to get the label with a given name.
     * @param name The name of the label to find.
     * @return The label, or a new label if it doesn't exist.
     */
    private static LabelNode getLabelWithName(String name) {
        LabelNode labelNode = labelNames.inverse().get(name);
        if (labelNode == null) {
            labelNode = new LabelNode();
            labelNames.put(labelNode, name);
        }
        return labelNode;
    }

    /**
     * Converts an instruction to a string.
     * @param insn The instruction to convert.
     * @return The string representation of the instruction.
     */
    public static String insnToString(AbstractInsnNode insn) {
        String opcode = AsmUtilities.opcodes.get(insn.getOpcode());
        String args = "";
        switch (insn.getType()) {
            case INSN:
                break;
            case INT_INSN:
                args = Integer.toString(((IntInsnNode) insn).operand);
                break;
            case VAR_INSN:
                args = Integer.toString(((VarInsnNode) insn).var); //TODO: variable names
                break;
            case TYPE_INSN:
                args = ((TypeInsnNode) insn).desc;
                break;
            case FIELD_INSN:
                args = ((FieldInsnNode) insn).owner + "." + ((FieldInsnNode) insn).name + " " + ((FieldInsnNode) insn).desc;
                break;
            case METHOD_INSN:
                args = ((MethodInsnNode) insn).owner + "." + ((MethodInsnNode) insn).name + ((MethodInsnNode) insn).desc;
                break;
            case JUMP_INSN:
                args = getNameOfLabel(((JumpInsnNode) insn).label);
                break;
            case LABEL:
                args = getNameOfLabel((LabelNode) insn) + ":";
                break;
            case LDC_INSN:
                LdcInsnNode ldcInsn = (LdcInsnNode) insn;

                if (ldcInsn.cst instanceof String)
                    args = "\"" + ldcInsn.cst + "\"";
                else if (ldcInsn.cst instanceof Long)
                    args = ldcInsn.cst + "L";
                else if (ldcInsn.cst instanceof Float)
                    args = ldcInsn.cst + "F";
                else if (ldcInsn.cst instanceof Double)
                    args = ldcInsn.cst + "D";
                else if (ldcInsn.cst instanceof Handle) {
                    Handle handle = (Handle) ldcInsn.cst;

                    int htag = handle.getTag();
                    String tag = tags.get(htag);
                    args = "handle[" + tag + " " + handle.getOwner() + "." + handle.getName();
                    if (htag >= Opcodes.H_GETFIELD && htag <= Opcodes.H_PUTSTATIC)
                        args += " ";
                    args += handle.getDesc() + "]";
                } else
                    args = ldcInsn.cst.toString();
                break;
            case IINC_INSN:
                args = ((IincInsnNode) insn).var + " " + ((IincInsnNode) insn).incr; //TODO: variable names
                break;
            case TABLESWITCH_INSN:
                TableSwitchInsnNode tableSwitchInsn = (TableSwitchInsnNode) insn;
                args = "range[" + tableSwitchInsn.min + ":" + tableSwitchInsn.max + "] labels[";
                for (int i = 0; i < tableSwitchInsn.labels.size(); i++) {
                    args += getNameOfLabel(tableSwitchInsn.labels.get(i));
                    if (i < tableSwitchInsn.labels.size() - 1)
                        args += ", ";
                }
                args += "] default[" + getNameOfLabel(tableSwitchInsn.dflt) + "]";
                break;
            case LOOKUPSWITCH_INSN:
                LookupSwitchInsnNode lookupSwitchInsn = (LookupSwitchInsnNode) insn;
                args = "mapping[";
                for (int i = 0; i < lookupSwitchInsn.keys.size(); i++) {
                    args += lookupSwitchInsn.keys.get(i) + "=" + getNameOfLabel(lookupSwitchInsn.labels.get(i));
                    if (i < lookupSwitchInsn.keys.size() - 1)
                        args += ", ";
                }
                args += "] default[" + getNameOfLabel(lookupSwitchInsn.dflt) + "]"; //TODO: labels
                break;
            case MULTIANEWARRAY_INSN:
                args = ((MultiANewArrayInsnNode) insn).desc + " " + ((MultiANewArrayInsnNode) insn).dims;
                break;
            case LINE:
                args = "LINE " + getNameOfLabel(((LineNumberNode) insn).start) + " " + ((LineNumberNode) insn).line;
                break;
            case INVOKE_DYNAMIC_INSN:
                InvokeDynamicInsnNode indyInsn = (InvokeDynamicInsnNode) insn;
                args = indyInsn.name + " " + indyInsn.desc + " handle[";

                Handle handle = indyInsn.bsm;
                int htag = handle.getTag();
                String tag = tags.get(htag);
                args += tag + " " + handle.getOwner() + "." + handle.getName();
                if (htag >= Opcodes.H_GETFIELD && htag <= Opcodes.H_PUTSTATIC)
                    args += " ";
                args += handle.getDesc() + "] args[";

                for (int i = 0; i < indyInsn.bsmArgs.length; i++) {
                    Object arg = indyInsn.bsmArgs[i];
                    if (arg instanceof String)
                        args += "\"" + arg + "\"";
                    else if (arg instanceof Type || arg instanceof Integer)
                        args += arg;
                    else if (arg instanceof Long)
                        args += arg + "L";
                    else if (arg instanceof Float)
                        args += arg + "F";
                    else if (arg instanceof Double)
                        args += arg + "D";
                    else if (arg instanceof Handle) {
                        handle = (Handle) arg;
                        htag = handle.getTag();
                        tag = tags.get(htag);
                        args += "handle[" + tag + " " + handle.getOwner() + "." + handle.getName();
                        if (htag >= Opcodes.H_GETFIELD && htag <= Opcodes.H_PUTSTATIC)
                            args += " ";
                        args += handle.getDesc() + "]";
                    }
                    if (i < indyInsn.bsmArgs.length - 1)
                        args += ", ";
                }
                args += "]";
                break;
            case FRAME:
                //Probably unnecessary?
                break;
            default:
                throw new IllegalStateException("Unknown instruction type: " + insn.getType());
        }

        return (opcode + " " + args).trim();
    }

    /**
     * Parses an instruction from a string.
     * @param insn The string to parse.
     * @return The instruction.
     */
    public static AbstractInsnNode parseInsn(String insn) {
    	String[] split = insn.split(" ");
        int opcode;
        int type;
        if (split[0].endsWith(":")) {
            opcode = 0;
            type = LABEL;
        } else {
            opcode = AsmUtilities.opcodes.inverse().get(split[0].toUpperCase());
            type = AsmUtilities.types.get(opcode);
        }

    	switch (type) {
    		case LINE:
                return new LineNumberNode(Integer.parseInt(split[2]), getLabelWithName(split[1]));
			case INSN:
				return new InsnNode(opcode);
			case INT_INSN:
				return new IntInsnNode(opcode, Integer.parseInt(split[1]));
			case VAR_INSN:
				return new VarInsnNode(opcode, Integer.parseInt(split[1]));
			case TYPE_INSN:
				return new TypeInsnNode(opcode, split[1]);
			case IINC_INSN:
				return new IincInsnNode(Integer.parseInt(split[1]), Integer.parseInt(split[2]));
			case MULTIANEWARRAY_INSN:
				return new MultiANewArrayInsnNode(split[1], Integer.parseInt(split[2]));
			case FIELD_INSN:
				String[] fieldSplit = split[1].split("\\.");
				return new FieldInsnNode(opcode, fieldSplit[0], fieldSplit[1], split[2]);
			case METHOD_INSN:
				String[] methodSplit = split[1].split("\\.");
				return new MethodInsnNode(opcode, methodSplit[0], methodSplit[1].substring(0, methodSplit[1].indexOf('(')), methodSplit[1].substring(methodSplit[1].indexOf('(')), opcode == Opcodes.INVOKEINTERFACE);
			case LDC_INSN:
				if (split[1].endsWith("\""))
					return new LdcInsnNode(split[1].substring(1, split[1].length() - 1));
				else if (split[1].endsWith("L"))
					return new LdcInsnNode(Long.parseLong(split[1].substring(0, split[1].length() - 1)));
				else if (split[1].endsWith("F"))
					return new LdcInsnNode(Float.parseFloat(split[1].substring(0, split[1].length() - 1)));
				else if (split[1].endsWith("D"))
					return new LdcInsnNode(Double.parseDouble(split[1].substring(0, split[1].length() - 1)));
				else if (split[1].endsWith("I")) {
					return new LdcInsnNode(Integer.parseInt(split[1].substring(0, split[1].length() - 1)));
				}
				else if (split[1].startsWith("handle")) {
					//i miss c#'s ranges...
					int tag = tags.inverse().get(split[1].substring(7));
					String owner = split[2].substring(0, split[2].lastIndexOf('.'));

					if (tag >= Opcodes.H_GETFIELD && tag <= Opcodes.H_PUTSTATIC) {
						String name = split[2].substring(split[2].lastIndexOf('.') + 1);
						return new LdcInsnNode(new Handle(tag, owner, name, split[3].substring(0, split[3].length() - 1)));
					}
					else {
						String name = split[2].substring(split[2].lastIndexOf('.') + 1, split[2].indexOf('('));
						String descriptor = split[2].substring(split[2].indexOf('('), split[2].length() - 1);
						return new LdcInsnNode(new Handle(tag, owner, name, descriptor));
					}
				}
				else
					return new LdcInsnNode(Integer.parseInt(split[1]));
			case JUMP_INSN:
                return new JumpInsnNode(opcode, getLabelWithName(split[1]));
			case TABLESWITCH_INSN:
				return new TableSwitchInsnNode(Integer.parseInt(split[1]),
						Integer.parseInt(split[2]),
						new LabelNode(),
						new LabelNode[Integer.parseInt(split[2]) - Integer.parseInt(split[1]) + 1]
				);
			case LOOKUPSWITCH_INSN:
                String[] splitting = insn.split("=");
                int[] keys = new int[splitting.length - 1];
                LabelNode[] labels = new LabelNode[splitting.length - 1];

                keys[0] = Integer.parseInt(splitting[0].substring(splitting[0].lastIndexOf('[') + 1));
                for (int i = 1; i < keys.length; i++) {
                    keys[i] = Integer.parseInt(splitting[i].substring(splitting[i].lastIndexOf(' ') + 1));
                    labels[i - 1] = getLabelWithName(splitting[i].substring(0, splitting[i].indexOf(',')));
                }
                String lastElement = splitting[splitting.length - 1];

                labels[labels.length - 1] = getLabelWithName(lastElement.substring(0, lastElement.indexOf(']')));
                LabelNode defaultLabel = getLabelWithName(lastElement.substring(lastElement.lastIndexOf('[') + 1, lastElement.lastIndexOf(']')));

				return new LookupSwitchInsnNode(defaultLabel, keys, labels);
			case LABEL:
				return getLabelWithName(split[0].substring(0, split[0].length() - 1));
			case FRAME:
				//return new FrameNode(opcode, 0, new Object[0], 0, new Object[0]);
			default:
				throw new IllegalStateException("Unknown instruction type: " + type);
		}
	}
}