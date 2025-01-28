using System;
using System.Collections.Generic;
using System.Dynamic;
using Java_Corruptor.BlastClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using ObjectWeb.Asm.Tree;
using RTCV.CorruptCore;

namespace Java_Corruptor.JsonConverters;

public class JavaBlastUnitConverter : JsonConverter
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JavaBlastUnit unit = (JavaBlastUnit)value;
        writer.WriteStartObject();
        
        writer.WritePropertyName("Method");
        writer.WriteValue(unit!.Method);
        
        writer.WritePropertyName("Instructions");
        
        writer.WriteStartArray();
        AsmParser parser = new();
        MethodNode method = AsmUtilities.FindMethod(unit.Method);
        parser.RegisterLabelsFrom(method.Instructions);
        foreach (AbstractInsnNode insn in unit.Instructions)
            writer.WriteValue(parser.InsnToString(insn));
        writer.WriteEndArray();
        
        writer.WritePropertyName("Index");
        writer.WriteValue(unit.Index);
        writer.WritePropertyName("Replaces");
        writer.WriteValue(unit.Replaces);
        writer.WritePropertyName("IsEnabled");
        writer.WriteValue(unit.IsEnabled);
        writer.WritePropertyName("IsLocked");
        writer.WriteValue(unit.IsLocked);
        writer.WritePropertyName("Note");
        writer.WriteValue(unit.Note);
        writer.WritePropertyName("Engine");
        writer.WriteValue(unit.Engine);
        writer.WritePropertyName("EngineSettings");
        writer.WriteStartObject();
        foreach (KeyValuePair<string, object> kvp in unit.EngineSettings)
        {
            writer.WritePropertyName(kvp.Key);
            if (kvp.Value is Array arr)
            {
                writer.WriteStartArray();
                foreach (object o in arr)
                    writer.WriteValue(o);
                writer.WriteEndArray();
            }
            else
                writer.WriteValue(kvp.Value);
        }
        
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        JArray jArray = (JArray)jObject["Instructions"]!;
        AsmParser parser = new();


        string method = jObject["Method"]!.Value<string>();
        MethodNode methodNode = AsmUtilities.FindMethod(method);
        parser.RegisterLabelsFrom(methodNode.Instructions);
        List<AbstractInsnNode> instructions = new();
        foreach (JToken token in jArray)
            instructions.Add(parser.ParseInsn(token.Value<string>()));
        int index = jObject["Index"]!.Value<int>(); ;
        int replaces = jObject["Replaces"]!.Value<int>(); ;
        bool isEnabled = jObject["IsEnabled"]!.Value<bool>(); ;
        bool isLocked = jObject["IsLocked"]!.Value<bool>(); ;
        string note = jObject["Note"]!.Value<string>(); ;
        string engine = jObject["Engine"]!.Value<string>(); ;
        ExpandoObject engineSettings = jObject["EngineSettings"]!.ToObject<ExpandoObject>(); ;
        
        return new JavaBlastUnit(instructions, index, replaces, method, note, isEnabled, isLocked, engine, engineSettings);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<string, List<BlastUnit>>);
    }
}