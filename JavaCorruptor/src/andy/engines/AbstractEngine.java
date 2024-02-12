package andy.engines;

import andy.AsmParser;
import andy.AsmUtilities;
import andy.BlastUnit;
import andy.Main;
import org.objectweb.asm.tree.AbstractInsnNode;
import org.objectweb.asm.tree.ClassNode;
import org.objectweb.asm.tree.InsnList;

import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

public abstract class AbstractEngine {
    protected abstract InsnList doCorrupt(AbstractInsnNode insnNode, int insnIndex);

    public void corrupt(ClassNode classNode) {
        classNode.methods.forEach(methodNode -> {
            final AtomicInteger i = new AtomicInteger();
            final InsnList insnList = new InsnList();
            AsmParser.clearLabels();
            AsmParser.registerLabelsFrom(methodNode.instructions);

            methodNode.instructions.forEach(insnNode -> {
                i.incrementAndGet();
                if (Main.instructionRandom.nextDouble() > Main.intensity) {
                    insnList.add(insnNode);
                    return;
                }

                InsnList result = doCorrupt(insnNode, i.get() - 1);
                if (result.size() == 0 || (result.size() == 1 && result.getFirst() == insnNode)) {
                    insnList.add(result);
                    return;
                }
                InsnList copy = AsmUtilities.copy(result);
                insnList.add(copy);
                BlastUnit unit = new BlastUnit(result, i.get() - 1);
                Main.blastLayer.layer.computeIfAbsent(classNode.name + "." + methodNode.name + methodNode.desc, k -> new ArrayList<>()).add(unit);
            });

            methodNode.instructions = insnList;
        });
    }
}
