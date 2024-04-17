using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using Ceras;
using Java_Corruptor.JsonConverters;
using Newtonsoft.Json;
using ObjectWeb.Asm.Tree;
using RTCV.CorruptCore;

namespace Java_Corruptor.BlastClasses;

[Serializable]
[MemberConfig(TargetMember.All)]
[JsonConverter(typeof(JavaBlastUnitConverter))]
public class JavaBlastUnit : INote
{
    public object Clone()
    {
        return ObjectCopierCeras.Clone(this);
    }
    
    [Category("Data")]
    [Description("The method that this unit affects")]
    [DisplayName("Method")]
    public string Method { get; set; }

    [Category("Data")]
    [Description("The instructions this unit adds")]
    [DisplayName("Instructions")]
    public List<AbstractInsnNode> Instructions { get; set; }

    [Category("Data")]
    [Description("The index of the point within the method that this unit will take place at")]
    [DisplayName("Index")]
    public int Index { get; set; }

    [Category("Data")]
    [Description("The number of instructions to remove after the index")]
    [DisplayName("Replaced")]
    public int Replaces { get; set; }

    [JsonIgnore] [NonSerialized] [Exclude] private string _engine;
    [Category("Data")]
    [Description("The engine that this unit was created with")]
    [DisplayName("Engine")]
    public string Engine
    {
        get => _engine;
        set
        {
            if (_engine == value)
                return;
            _engine = value;
            EngineSettings = new ExpandoObject();
        }
    }

    [Category("Data")]
    [Description("The settings for this unit's engine")]
    [DisplayName("EngineSettings")]
    public dynamic EngineSettings { get; set; }

    [Category("Settings")]
    [Description("Whether or not the BlastUnit will apply if the stashkey is run")]
    [DisplayName("Enabled")]
    public bool IsEnabled { get; set; }

    [Category("Settings")]
    [Description("Whether or not this unit will be affected by batch operations (disable 50, invert, etc)")]
    [DisplayName("Locked")]
    public bool IsLocked { get; set; }

    [Category("Misc")]
    [Description("Note associated with this unit")]
    public string Note { get; set; }


    /// <param name="instructions">The instructions this unit adds</param>
    /// <param name="index">The index of the instruction that this unit will take place before within the method</param>
    /// <param name="replaces">The number of instructions to remove after the index</param>
    /// <param name="method">The method that this unit affects</param>
    /// <param name="note">The text for the unit's note</param>
    /// <param name="isEnabled">Will the unit be applied?</param>
    /// <param name="isLocked">May the unit be modified?</param>
    /// <param name="engine">The engine that this unit was created with</param>
    /// <param name="engineSettings">The settings that this unit was created with</param>
    public JavaBlastUnit(List<AbstractInsnNode> instructions, int index, int replaces, string method, string note = null, bool isEnabled = true, bool isLocked = false, string engine = null, ExpandoObject engineSettings = null)
    {
        Instructions = instructions;
        Index = index;
        Replaces = replaces;
        Method = method;
        Note = note;
        IsEnabled = isEnabled;
        IsLocked = isLocked;
        Engine = engine;
        EngineSettings = engineSettings ?? new();
    }
    
    //TODO: reroll support maybe
    
    public JavaBlastUnit GetBackup()
    {
        //TODO
        //There's a todo here but I didn't leave a note please help someone tell me why there's a todo here oh god I'm the only one working on this code
        // Unrelated TODO: might want to clone the reference types here
        return new(Instructions, Index, Replaces, Method, Note, IsEnabled, IsLocked, Engine, EngineSettings);
    }
}