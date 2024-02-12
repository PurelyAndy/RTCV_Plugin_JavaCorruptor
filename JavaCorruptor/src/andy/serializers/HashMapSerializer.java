package andy.serializers;

import andy.AsmUtilities;
import andy.BlastUnit;
import com.google.gson.TypeAdapter;
import org.objectweb.asm.tree.InsnList;
import andy.AsmParser;
import org.objectweb.asm.tree.MethodNode;

import java.util.ArrayList;
import java.util.HashMap;

public class HashMapSerializer extends TypeAdapter<HashMap<String, ArrayList<BlastUnit>>> {
    @Override
    public void write(com.google.gson.stream.JsonWriter out, HashMap<String, ArrayList<BlastUnit>> value) throws java.io.IOException {
        out.beginObject();
        for (String key : value.keySet()) {
            out.name(key);
            out.beginArray();
            for (BlastUnit unit : value.get(key)) {
                out.beginObject();
                out.name("index").value(unit.index);
                out.name("instructions");
                out.beginArray();
                AsmParser.clearLabels();
                MethodNode method = AsmUtilities.findMethod(key);
                AsmParser.registerLabelsFrom(method.instructions);
                for (int i = 0; i < unit.instructions.size(); i++) {
                    out.value(AsmParser.insnToString(unit.instructions.get(i)));
                }
                out.endArray();
                out.endObject();
            }
            out.endArray();
        }
        out.endObject();
    }

    @Override
    public HashMap<String, ArrayList<BlastUnit>> read(com.google.gson.stream.JsonReader in) throws java.io.IOException {
        HashMap<String, ArrayList<BlastUnit>> map = new HashMap<>();
        in.beginObject();
        while (in.hasNext()) {
            String key = in.nextName();
            ArrayList<BlastUnit> list = new ArrayList<>();
            in.beginArray();
            while (in.hasNext()) {
                in.beginObject();
                int index = -1;
                InsnList instructions = new InsnList();
                while (in.hasNext()) {
                    String name = in.nextName();
                    if (name.equals("index")) {
                        index = in.nextInt();
                    } else if (name.equals("instructions")) {
                        in.beginArray();
                        AsmParser.clearLabels();
                        MethodNode method = AsmUtilities.findMethod(key);
                        AsmParser.registerLabelsFrom(method.instructions);
                        while (in.hasNext()) {
                            instructions.add(AsmParser.parseInsn(in.nextString()));
                        }
                        in.endArray();
                    }
                }
                in.endObject();
                list.add(new BlastUnit(instructions, index));
            }
            in.endArray();
            map.put(key, list);
        }
        in.endObject();
        return map;
    }
}
