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
[XmlInclude(typeof(JavaBlastUnit))]
public class JavaBlastLayer : ICloneable, INote
{
    public List<JavaBlastUnit> Layer = new();
    
    public JavaBlastLayer()
    {
    }

    public JavaBlastLayer(JavaBlastUnit bu)
    {
        Layer.Add(bu);
    }

    public JavaBlastLayer(List<JavaBlastUnit> layer)
    {
        Layer = layer;
    }

    public object Clone()
    {
        return ObjectCopierCeras.Clone(this);
    }

    public JavaBlastLayer GetBackup()
    {
        List<JavaBlastUnit> backupLayer = new();

        backupLayer.AddRange(Layer.Select(it => it.GetBackup()).Where(it => it != null));

        return new(backupLayer);
    }

    //TODO: reroll support maybe
    
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
            foreach (JavaBlastUnit bu in Layer)
            {
                bu.Note = value;
            }
        }
    }
}