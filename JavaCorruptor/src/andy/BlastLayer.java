package andy;

import andy.serializers.HashMapSerializer;
import com.google.gson.annotations.JsonAdapter;

import java.util.ArrayList;
import java.util.HashMap;

public class BlastLayer {
    @JsonAdapter(HashMapSerializer.class)
    public HashMap<String, ArrayList<BlastUnit>> layer = new HashMap<>();
}
