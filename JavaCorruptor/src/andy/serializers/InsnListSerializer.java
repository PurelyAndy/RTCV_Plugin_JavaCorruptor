package andy.serializers;

import andy.AsmParser;
import com.google.gson.TypeAdapter;
import org.objectweb.asm.tree.InsnList;

import java.io.IOException;

public class InsnListSerializer extends TypeAdapter<InsnList> {
    @Override
    public void write(com.google.gson.stream.JsonWriter out, InsnList value) throws IOException {
        out.beginArray();
        for (int i = 0; i < value.size(); i++) {
            out.value(AsmParser.insnToString(value.get(i)));
        }
        out.endArray();
    }

    @Override
    public InsnList read(com.google.gson.stream.JsonReader in) throws IOException {
        InsnList list = new InsnList();
        in.beginArray();
        while (in.hasNext()) {
            list.add(AsmParser.parseInsn(in.nextString()));
        }
        in.endArray();
        return list;
    }
}
