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
        string method = "";
        List<AbstractInsnNode> instructions = new();
        int index = -1;
        int replaces = -1;
        bool isEnabled = true;
        bool isLocked = false;
        string note = null;
        string engine = null;
        ExpandoObject engineSettings = new();

        /*
        reader.Read();
        while (reader.TokenType != JsonToken.EndObject)
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                string propName = (string)reader.Value;
                reader.Read();
                switch (propName)
                {
                    case "Method":
                        method = (string)reader.Value;
                        break;
                    case "Instructions":
                        reader.Read();
                        AsmParser.ClearLabels();
                        MethodNode methodNode = AsmUtilities.FindMethod(method);
                        AsmParser.RegisterLabelsFrom(methodNode.Instructions);
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            instructions.Add(AsmParser.ParseInsn(reader.Value.ToString()));
                            reader.Read();
                        }

                        break;
                    case "Index":
                        index = (int)reader.Value!;
                        break;
                    case "Replaces":
                        replaces = (int)reader.Value!;
                        break;
                    case "IsEnabled":
                        isEnabled = (bool)reader.Value!;
                        break;
                    case "IsLocked":
                        isLocked = (bool)reader.Value!;
                        break;
                    case "Note":
                        note = (string)reader.Value;
                        break;
                } 
            }
        }*/

        // i don't know if i can trust the order of the properties to be consistent. specifically, Method needs to come before Instructions
        // TODO: profile this, if it's too slow, use the above code instead if it's faster
        JObject jObject = JObject.Load(reader);
        
        method = jObject["Method"]!.Value<string>();
        
        JArray jArray = (JArray)jObject["Instructions"]!;
        AsmParser parser = new();
        MethodNode methodNode = AsmUtilities.FindMethod(method);
        parser.RegisterLabelsFrom(methodNode.Instructions);
        foreach (JToken token in jArray)
            instructions.Add(parser.ParseInsn(token.Value<string>()));
        
        index = jObject["Index"]!.Value<int>();
        replaces = jObject["Replaces"]!.Value<int>();
        isEnabled = jObject["IsEnabled"]!.Value<bool>();
        isLocked = jObject["IsLocked"]!.Value<bool>();
        note = jObject["Note"]!.Value<string>();
        engine = jObject["Engine"]!.Value<string>();
        engineSettings = jObject["EngineSettings"]!.ToObject<ExpandoObject>();
        
        return new JavaBlastUnit(instructions, index, replaces, method, note, isEnabled, isLocked, engine, engineSettings);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<string, List<BlastUnit>>);
    }
}