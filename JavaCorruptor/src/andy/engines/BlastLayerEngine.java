package andy.engines;

import andy.BlastLayer;
import andy.BlastUnit;
import andy.Main;
import com.google.gson.Gson;
import org.objectweb.asm.tree.*;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Does nothing. Exists only to fill a slot in the engine list.
 */
public class BlastLayerEngine extends AbstractEngine {
    public BlastLayerEngine(String[] args) throws IOException {
        String bl = new String(Files.readAllBytes(Paths.get(args[6])));
        Main.blastLayer = new Gson().fromJson(bl, BlastLayer.class);
    }

    @Override
    public void corrupt(ClassNode classNode) {
        classNode.methods.forEach(methodNode -> {
            List<BlastUnit> unitList = Main.blastLayer.layer.get(classNode.name + "." + methodNode.name + methodNode.desc);
            if (unitList == null)
                return;
            Map<AbstractInsnNode, InsnList> indexedAnchorsAndNewInsns = new HashMap<>();
            unitList.forEach(unit -> indexedAnchorsAndNewInsns.put(methodNode.instructions.get(unit.index), unit.instructions));
            indexedAnchorsAndNewInsns.forEach((key, value) -> {
                methodNode.instructions.insert(key, value);
                methodNode.instructions.remove(key);
            });
        });
    }
    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) {
        InsnList list = new InsnList();
        list.add(insn);
        return list;
    }
}