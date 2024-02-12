package andy.engines;

import andy.Main;
import org.objectweb.asm.Opcodes;
import org.objectweb.asm.tree.*;

import java.util.ArrayList;
import java.util.List;

public class FunctionEngine extends AbstractEngine {
    private final List<String> limiters = new ArrayList<>();
    private final List<String> values = new ArrayList<>();

    public FunctionEngine(String[] args) {
        int i = 6;
        for (; !args[i].equals(":"); i++)
            limiters.add(args[i]);
        i++;
        for (; !args[i].equals(":"); i++)
            values.add(args[i]);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) {
        InsnList list = new InsnList();
        if (insn.getOpcode() != Opcodes.INVOKESTATIC) {
            list.add(insn);
            return list;
        }
        MethodInsnNode methodInsnNode = (MethodInsnNode) insn;
        if (!methodInsnNode.owner.equals("java/lang/Math") || !methodInsnNode.desc.equals("(D)D")) {
            list.add(insn);
            return list;
        }

        if (limiters.contains(methodInsnNode.name)) {
            String newMethod = values.get(Main.valueRandom.nextInt(values.size()));
            if (newMethod.equals("POP,random()")) {
                list.add(new InsnNode(Opcodes.POP));
                list.add(new MethodInsnNode(Opcodes.INVOKESTATIC, "java/lang/Math", "random", "()D", false));
            } else {
                methodInsnNode.name = newMethod;
                list.add(methodInsnNode);
            }
        } else {
            list.add(methodInsnNode);
        }
        return list;
    }
}
