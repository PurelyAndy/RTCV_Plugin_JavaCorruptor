using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Linq;
using System.Windows;
using System.Windows.Forms.Layout;
using Ceras;
using Java_Corruptor.UI.Components.EngineControls;
using Newtonsoft.Json;
using ObjectWeb.Asm.Tree;
using RTCV.CorruptCore;

namespace Java_Corruptor.BlastClasses;

[Serializable]
[MemberConfig(TargetMember.All)]
public class SerializedInsnBlastUnit : INote
{
    public object Clone()
    {
        SerializedInsnBlastUnit u = ObjectCopierCeras.Clone(this);
        u.EngineSettings = EngineSettings;
        return u;
    }
    
    [Category("Data")]
    [Description("The method that this unit affects")]
    [DisplayName("Method")]
    public string Method { get; set; }

    [Category("Value")]
    [Description("The instructions this unit adds")]
    [DisplayName("Instructions")]
    public List<string> Instructions { get; set; }

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
    [DisplayName("Note")]
    public string Note { get; set; }
    
    [JsonIgnore]
    [Category("Value")]
    [Description("Gets and sets Value[] through a string. Used for Textboxes")]
    [DisplayName("ValueString")]
    [Exclude]
    public string ValueString
    {
        get
        {
            if (Instructions == null || Instructions.Count == 0)
            {
                return string.Empty;
            }

            return string.Join("\n", Instructions);
        }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            
            Instructions = new(value.Split('\n'));
            for (int i = 0; i < Instructions.Count; i++)
                Instructions[i] = Instructions[i].Trim();
        }
    }

    /// <param name="instructions">The instructions this unit adds</param>
    /// <param name="index">The index of the instruction that this unit will take place before within the method</param>
    /// <param name="replaces">The number of instructions to remove after the index</param>
    /// <param name="method">The method that this unit affects</param>
    /// <param name="note">The text for the unit's note</param>
    /// <param name="isEnabled">Will the unit be applied?</param>
    /// <param name="isLocked">May the unit be modified?</param>
    /// <param name="engine">The engine that this unit was created with</param>
    /// <param name="engineSettings">The settings that this unit was created with</param>
    public SerializedInsnBlastUnit(List<string> instructions, int index, int replaces, string method, string note = null, bool isEnabled = true, bool isLocked = false, string engine = null, ExpandoObject engineSettings = null)
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
    
    public SerializedInsnBlastUnit()
    {
        Instructions = new();
    }

    /// <summary>
    /// You MUST load the classes from the appropriate jar file before calling this.
    /// Runs the engine used to create this unit again, replacing the corruption with the result.
    /// </summary>
    public void ReRoll()
    {
        if (IsLocked)
            return;

        JavaEngineControl ourEngine = (JavaEngineControl)Activator.CreateInstance(Type.GetType("Java_Corruptor.UI.Components.EngineControls." + Engine)!);
        ourEngine.Prepare();
        ourEngine.EngineSettings = EngineSettings;
        MethodNode methodNode = AsmUtilities.FindMethod(Method);
        if (methodNode is null)
        {
            MessageBox.Show("Method not found: " + Method);
            return;
        }
        
        //AbstractInsnNode[] methodNodeInstructions = methodNode.Instructions.ToArray();
        
        AsmParser parser = new();
        parser.RegisterLabelsFrom(methodNode.Instructions);
        
        AbstractInsnNode insnNode = methodNode.Instructions.First;
        for (int i = 0; i < Index; insnNode = insnNode.Next, i++)
        {
            if (insnNode is null)
            {
                MessageBox.Show("Index out of bounds: " + Index);
                return;
            }
        }
        
        int replaces = -1;
        InsnList result = ourEngine.DoCorrupt(insnNode, parser, ref replaces);
        if (replaces == -1 || result is null || result.Size == 0 || (result.Size == 1 && result.First == insnNode))
        {
            MessageBox.Show("Tried to re-roll but nothing was corrupted. Do the unit's re-roll settings still match the instruction?");
            return;
        }

        Instructions = result.Select(parser.InsnToString).ToList();
        Replaces = replaces;
    }

    public SerializedInsnBlastUnit GetBackup()
    {
        //TODO
        //There's a todo here but I didn't leave a note please help someone tell me why there's a todo here oh god I'm the only one working on this code
        // Unrelated TODO: might want to clone the reference types here
        return new(Instructions, Index, Replaces, Method, Note, IsEnabled, IsLocked, Engine, EngineSettings);
    }
}