using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Ceras;
using Newtonsoft.Json;
using RTCV.CorruptCore;

namespace Java_Corruptor.BlastClasses;

[MemberConfig(TargetMember.All)]
[Serializable]
[XmlInclude(typeof(SerializedInsnBlastUnit))]
public class SerializedInsnBlastLayer : ICloneable, INote
{
    public List<SerializedInsnBlastUnit> Layer = new();
    
    public SerializedInsnBlastLayer()
    {
    }

    public SerializedInsnBlastLayer(SerializedInsnBlastUnit bu)
    {
        Layer.Add(bu);
    }

    public SerializedInsnBlastLayer(List<SerializedInsnBlastUnit> layer)
    {
        Layer = layer;
    }

    public object Clone()
    {
        SerializedInsnBlastLayer newLayer = new();
        foreach (SerializedInsnBlastUnit bu in Layer)
        {
            newLayer.Layer.Add((SerializedInsnBlastUnit)bu.Clone());
        }
        newLayer.Note = Note;
        return newLayer;
    }

    public SerializedInsnBlastLayer GetBackup()
    {
        List<SerializedInsnBlastUnit> backupLayer = new();

        backupLayer.AddRange(Layer.Select(it => it.GetBackup()).Where(it => it != null));

        return new(backupLayer);
    }

    /// <summary>
    /// You MUST load the classes from the appropriate jar file before calling this.
    /// Runs the engines used to create the units in this layer again, replacing the units' corruptions with the result.
    /// </summary>
    public void ReRoll()
    {
        foreach (SerializedInsnBlastUnit bu in Layer.Where(x => x.IsLocked == false))
        {
            bu.ReRoll();
        }
    }

    private const string Shared = "[DIFFERENT]";

    [JsonIgnore]
    public string Note
    {
        get
        {
            return Layer.All(x => x.Note == Layer.First().Note)
                ? Layer.FirstOrDefault()?.Note
                : Shared;
        }
        set
        {
            if (value == Shared)
            {
                return;
            }
            foreach (SerializedInsnBlastUnit bu in Layer)
            {
                bu.Note = value;
            }
        }
    }

    public void SanitizeDuplicates()
    {
        /*
        Layer = Layer.GroupBy(x => new { x.Address, x.Domain })
          .Where(g => g.Count() > 1)
          .Select(y => y.First())
          .ToList();
          */

        List<SerializedInsnBlastUnit> bul = new(Layer.ToArray().Reverse());
        List<ValueTuple<string, long>> usedIndexes = new();

        foreach (SerializedInsnBlastUnit bu in bul)
        {
            if (!usedIndexes.Contains(new(bu.Method, bu.Index)) && !bu.IsLocked)
            {
                usedIndexes.Add(new(bu.Method, bu.Index));
            }
            else if (!bu.IsLocked)
            {
                Layer.Remove(bu);
            }
        }
    }
}