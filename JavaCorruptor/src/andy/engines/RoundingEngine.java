package andy.engines;

import org.objectweb.asm.Opcodes;
import org.objectweb.asm.tree.*;

/**
 * Created by Andy on 5/29/17.
 * I miss C# :(
 * Everything is so much easier in C#,
 */
public class RoundingEngine extends AbstractEngine {
    private final double places;
    private final byte types;
    private final byte kinds;
    private final byte operations;

    public RoundingEngine(String[] args) {
        places = Double.parseDouble(args[6]);
        types = Byte.parseByte(args[7]);
        kinds = Byte.parseByte(args[8]);
        operations = Byte.parseByte(args[9]);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) {
        InsnList list = new InsnList();

        int opcode = insn.getOpcode();

        if (insn instanceof LdcInsnNode && (kinds & 1) == 1)
            roundConstants(insn, list);
        else if (insn.getOpcode() >= 0x60 && insn.getOpcode() <= 0x6f && (kinds & 2) == 2)
            roundMathOperations(insn, list, opcode);
        else if (opcode >= 0x15 && opcode <= 0x18 && (kinds & 4) == 4)
            roundVariableLoads(insn, list, opcode);
        else if (opcode >= 0x1a && opcode <= 0x29 && (kinds & 4) == 4)
            roundVariableLoadsConst(insn, list, opcode);
        else if ((insn.getOpcode() == Opcodes.GETFIELD || insn.getOpcode() == Opcodes.GETSTATIC) && (kinds & 8) == 8)
            roundFieldLoads(insn, list);
        else if ((insn.getOpcode() == Opcodes.INVOKESTATIC || insn.getOpcode() == Opcodes.INVOKEVIRTUAL || insn.getOpcode() == Opcodes.INVOKESPECIAL) && (kinds & 16) == 16)
            roundMethodReturnValues(insn, list);
        else
            list.add(insn);

        return list;
    }

    private void roundMethodReturnValues(AbstractInsnNode insn, InsnList list) {
        list.add(insn);
        MethodInsnNode node = (MethodInsnNode) insn;
        String desc = node.desc;
        if (desc.endsWith(")I") && (types & 1) == 1) {
            list.add(new InsnNode(Opcodes.I2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2I));
        } else if (desc.endsWith(")J") && (types & 2) == 2) {
            list.add(new InsnNode(Opcodes.L2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2L));
        } else if (desc.endsWith(")F") && (types & 4) == 4) {
            list.add(new InsnNode(Opcodes.F2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2F));
        } else if (desc.endsWith(")D") && (types & 8) == 8)
            AddRoundingInstructions(list);
    }

    private void roundFieldLoads(AbstractInsnNode insn, InsnList list) {
        list.add(insn);
        FieldInsnNode node = (FieldInsnNode) insn;
        String desc = node.desc;
        if (desc.equals("I") && (types & 1) == 1) {
            list.add(new InsnNode(Opcodes.I2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2I));
        } else if (desc.equals("J") && (types & 2) == 2) {
            list.add(new InsnNode(Opcodes.L2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2L));
        } else if (desc.equals("F") && (types & 4) == 4) {
            list.add(new InsnNode(Opcodes.F2D));
            AddRoundingInstructions(list);
            list.add(new InsnNode(Opcodes.D2F));
        } else if (desc.equals("D") && (types & 8) == 8)
            AddRoundingInstructions(list);
    }

    private void roundVariableLoadsConst(AbstractInsnNode insn, InsnList list, int opcode) {
        list.add(insn);
        if (opcode <= 0x1d) {
            if ((types & 1) == 1) {
                list.add(new InsnNode(Opcodes.I2D));
                AddRoundingInstructions(list);
                list.add(new InsnNode(Opcodes.D2I));
            }
        } else if (opcode <= 0x21) {
            if ((types & 2) == 2) {
                list.add(new InsnNode(Opcodes.L2D));
                AddRoundingInstructions(list);
                list.add(new InsnNode(Opcodes.D2L));
            }
        } else if (opcode <= 0x25) {
            if ((types & 4) == 4) {
                list.add(new InsnNode(Opcodes.F2D));
                AddRoundingInstructions(list);
                list.add(new InsnNode(Opcodes.D2F));
            }
        } else if (opcode <= 0x29) {
            if ((types & 8) == 8)
                AddRoundingInstructions(list);
        }
    }

    private void roundVariableLoads(AbstractInsnNode insn, InsnList list, int opcode) {
        list.add(insn);
        switch (opcode) {
            case Opcodes.ILOAD:
                if ((types & 1) == 1) {
                    list.add(new InsnNode(Opcodes.I2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2I));
                }
                break;
            case Opcodes.LLOAD:
                if ((types & 2) == 2) {
                    list.add(new InsnNode(Opcodes.L2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2L));
                }
                break;
            case Opcodes.FLOAD:
                if ((types & 4) == 4) {
                    list.add(new InsnNode(Opcodes.F2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2F));
                }
                break;
            case Opcodes.DLOAD:
                if ((types & 8) == 8)
                    AddRoundingInstructions(list);
                break;
        }
    }

    private void roundMathOperations(AbstractInsnNode insn, InsnList list, int opcode) {
        list.add(insn);
        int operation = (int)Math.pow(2, (opcode - 0x60) / 4);
        if ((operations & operation) != operation)
            return;

        switch (opcode % 4) {
            case 0:
                if ((types & 1) == 1) {
                    list.add(new InsnNode(Opcodes.I2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2I));
                }
                break;
            case 1:
                if ((types & 2) == 2) {
                    list.add(new InsnNode(Opcodes.L2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2L));
                }
                break;
            case 2:
                if ((types & 4) == 4) {
                    list.add(new InsnNode(Opcodes.F2D));
                    AddRoundingInstructions(list);
                    list.add(new InsnNode(Opcodes.D2F));
                }
                break;
            case 3:
                if ((types & 8) == 8)
                    AddRoundingInstructions(list);
                break;
        }
    }

    private void roundConstants(AbstractInsnNode insn, InsnList list) {
        LdcInsnNode node = (LdcInsnNode) insn;
        if (node.cst instanceof Integer && (types & 1) == 1) {
            int i = (int)node.cst;
            list.add(new LdcInsnNode(i));
        } else if (node.cst instanceof Long && (types & 2) == 2) {
            long l = (long)node.cst;
            list.add(new LdcInsnNode(l));
        } else if (node.cst instanceof Float && (types & 4) == 4) {
            float f = (float)node.cst;
            list.add(new LdcInsnNode((float)(Math.round(f * places) / places)));
        } else if (node.cst instanceof Double && (types & 8) == 8) {
            double d = (double)node.cst;
            list.add(new LdcInsnNode(Math.round(d * places) / places));
        } else {
            list.add(insn);
        }
    }

    private void AddRoundingInstructions(InsnList list) {
        list.add(new LdcInsnNode(places));
        list.add(new InsnNode(Opcodes.DMUL));
        list.add(new MethodInsnNode(Opcodes.INVOKESTATIC, "java/lang/Math", "round", "(D)J", false));
        list.add(new InsnNode(Opcodes.L2D));
        list.add(new LdcInsnNode(places));
        list.add(new InsnNode(Opcodes.DDIV));
    }
}