package andy.engines;

import org.objectweb.asm.tree.AbstractInsnNode;
import org.objectweb.asm.tree.InsnList;
import org.objectweb.asm.tree.InsnNode;

public class VectorEngine extends AbstractEngine {
    private final int limiter, value;

    public VectorEngine(String[] args) {
        limiter = Integer.parseInt(args[6]);
        value = Integer.parseInt(args[7]);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) { // TODO: so much
        InsnList list = new InsnList();

        if (insn.getOpcode() == limiter)
            list.add(new InsnNode(value));
        else
            list.add(insn);

        return list;
    }
}
