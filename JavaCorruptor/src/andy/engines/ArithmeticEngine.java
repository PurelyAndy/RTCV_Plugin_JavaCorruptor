package andy.engines;

import andy.Main;
import org.objectweb.asm.tree.*;

import java.util.ArrayList;
import java.util.List;

public class ArithmeticEngine extends AbstractEngine {
    private final float max, min;
    private final List<Integer> limiters = new ArrayList<>();
    private final List<Integer> operations = new ArrayList<>();
    private final int types;

    public ArithmeticEngine(String[] args) {
        max = Float.parseFloat(args[6]);
        min = Float.parseFloat(args[7]);

        int i = 8;
        for (; !args[i].equals(":"); i++)
            limiters.add(Integer.parseInt(args[i]));
        i++;
        for (; !args[i].equals(":"); i++)
            operations.add(Integer.parseInt(args[i]));
        i++;

        types = Integer.parseInt(args[i]);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) {
        // TODO: so much
        InsnList list = new InsnList();
        list.add(insn);

        int opcode = insn.getOpcode();
        if (opcode < 0x60 || opcode > 0x6f)
            return list;
        if (!limiters.contains(opcode - 0x60 - ((opcode - 0x60) % 4))) //from 0x60-0x6f, each grouping of 4 is the I,L,F, and D instructions of each operation.
            return list;

        float f = Main.valueRandom.nextFloat();
        float value = f > 0 ? f * max : f * -min;
        int operation = (operations.get(Main.valueRandom.nextInt(operations.size()))) + (opcode % 4) + 0x60;

        //TODO: this could probably be a loop or something, but it's 3am and i'm too tired to think
        switch ((opcode) % 4) {
            case 0:
                if ((types & 1) == 1) {
                    list.add(new LdcInsnNode((int) value));
                    list.add(new InsnNode(operation));
                }
                return list;
            case 1:
                if ((types & 2) == 2) {
                    list.add(new LdcInsnNode((long) value));
                    list.add(new InsnNode(operation));
                }
                return list;
            case 2:
                if ((types & 4) == 4) {
                    list.add(new LdcInsnNode(value));
                    list.add(new InsnNode(operation));
                }
                return list;
            case 3:
                if ((types & 8) == 8) {
                    list.add(new LdcInsnNode((double) value));
                    list.add(new InsnNode(operation));
                }
                return list;
        }

        return list;
    }
}
