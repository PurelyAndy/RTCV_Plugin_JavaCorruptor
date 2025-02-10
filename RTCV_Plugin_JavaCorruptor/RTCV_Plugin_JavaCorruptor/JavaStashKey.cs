using System;
using System.Collections.Generic;
using System.IO;
using Ceras;
using Java_Corruptor.BlastClasses;
using RTCV.CorruptCore;
using RTCV.NetCore;

namespace Java_Corruptor;

[Serializable]
[MemberConfig(TargetMember.AllPublic)]
internal class JavaStashKey : ICloneable, INote
{
    private string _alias;

    public string JarFilename { get; set; }
    public string JarShortFilename { get; set; }
    public string GameName { get; set; }
    public string Note { get; set; }
    public string Key { get; set; }
    public string ParentKey { get; set; }
    public LaunchScript LaunchScript { get; set; }
    public SerializedInsnBlastLayerCollection BlastLayer { get; set; }

    public string Alias
    {
        get => _alias ?? Key;
        set => _alias = value;
    }

    public JavaStashKey() => StashKeyConstructor(RtcCore.GetRandomKey(), null, new());

    public JavaStashKey(string key, string parentkey, SerializedInsnBlastLayerCollection blastlayer) => StashKeyConstructor(key, parentkey, blastlayer);
    public JavaStashKey(string key, string parentkey, SerializedInsnBlastLayerCollection blastlayer, LaunchScript launchScript) => StashKeyConstructor(key, parentkey, blastlayer, launchScript);

    private void StashKeyConstructor(string key, string parentkey, SerializedInsnBlastLayerCollection blastlayer, LaunchScript launchScript = null)
    {
        Key = key;
        ParentKey = parentkey;
        BlastLayer = blastlayer;

        JarFilename = (string)AllSpec.VanguardSpec?[VSPEC.OPENROMFILENAME] ?? "ERROR";
        JarShortFilename = Path.GetFileName(JarFilename);
        GameName = Path.GetFileNameWithoutExtension(JarFilename);
        LaunchScript = launchScript ?? CorruptionOptions.LaunchScript;
    }

    public override string ToString()
    {
        return Alias;
    }
    
    /// <summary>
    /// Can be called from UI Side
    /// </summary>
    public bool Run()
    {
        JavaStockpileManagerUISide.CurrentStashkey = this;
        return JavaStockpileManagerUISide.ApplyStashkey(this);
    }

    public object Clone()
    {
        //object sk = ObjectCopierCeras.Clone(this); Ceras doesn't like to deserialize it, it raises a very confusing exception that I can't fix.
        SerializedInsnBlastLayerCollection blc = new();
        foreach (SerializedInsnBlastUnit unit in (IList<SerializedInsnBlastUnit>)BlastLayer) {
            blc.Add((SerializedInsnBlastUnit)unit.Clone());
        }
        JavaStashKey sk = new(Key, ParentKey, blc, LaunchScript)
        {
            JarFilename = JarFilename,
            JarShortFilename = JarShortFilename,
            GameName = GameName,
            Note = Note,
            Key = RtcCore.GetRandomKey(),
            Alias = null,
        };
        return sk;
    }
}