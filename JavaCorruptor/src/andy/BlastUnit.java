package andy;

import andy.serializers.InsnListSerializer;
import com.google.gson.annotations.JsonAdapter;
import org.objectweb.asm.tree.InsnList;

public class BlastUnit {
    @JsonAdapter(InsnListSerializer.class)
    public InsnList instructions;
    public int index;

    public BlastUnit(InsnList instructions, int index) {
        this.instructions = instructions;
        this.index = index;
    }
}
