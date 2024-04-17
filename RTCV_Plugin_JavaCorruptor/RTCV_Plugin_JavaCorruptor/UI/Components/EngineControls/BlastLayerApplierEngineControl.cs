using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using Newtonsoft.Json;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class BlastLayerApplierEngineControl
{
    public BlastLayerApplierEngineControl()
    {
        InitializeComponent();
    }

    public override void Corrupt(ClassNode classNode)
    {
        foreach (MethodNode method in classNode.Methods)
        {
            string key = classNode.Name + "." + method.Name + method.Desc;

            if (!JavaCorruptionEngineForm.BlastLayerCollection.MappedLayers.ContainsKey(key))
                continue;
                        
            List<JavaBlastUnit> unitList = JsonConvert.DeserializeObject<JavaBlastLayer>(JsonConvert.SerializeObject(JavaCorruptionEngineForm.BlastLayerCollection.MappedLayers[key])).Layer;
            AbstractInsnNode[] insns = method.Instructions.ToArray();
            //IEnumerable<(AbstractInsnNode Anchor, List<AbstractInsnNode> Instructions, int Replaces)> indexedAnchorsAndNewInsns = unitList.Select(unit => unit.IsEnabled ? (Anchor: insns[unit.Index], unit.Instructions, Replaces: unit.Replaces) : (null, new(), 0) );
            // todo: this is dumb, just do a single loop
            foreach (JavaBlastUnit unit in unitList)
            {
                if (!unit.IsEnabled)
                    continue;
                AbstractInsnNode anchor = insns[unit.Index];
                List<AbstractInsnNode> instructions = unit.Instructions;
                int replaces = unit.Replaces;
                
                AbstractInsnNode insnAfterTheOneToRemove = anchor.Next.Next;
                for (int i = 1; i < replaces; i++) // We need to keep item.Key (the anchor) in the list
                    // until the end so that we can insert the new instructions after it
                {
                    method.Instructions.Remove(insnAfterTheOneToRemove.Previous);
                    insnAfterTheOneToRemove = insnAfterTheOneToRemove.Next;
                }

                for (int i = instructions.Count - 1; i >= 0; i--)
                {
                    method.Instructions.Insert(anchor, instructions[i]);
                }
                method.Instructions.Remove(anchor);
            }
            //ensure all of the try catch blocks are still valid
            for (int i = 0; i < method.TryCatchBlocks.Count; i++)
            {
                TryCatchBlockNode tcb = method.TryCatchBlocks[i];
                if (tcb.Start == null || tcb.End == null || tcb.Handler == null)
                    continue;
                if (method.Instructions.Contains(tcb.Start) && method.Instructions.Contains(tcb.End) && method.Instructions.Contains(tcb.Handler))
                    continue;
                method.TryCatchBlocks.Remove(tcb);
                i--;
            }
            //the local variable table might need to be "rebuilt" (or something like that)
            method.LocalVariables.Clear();
        }
    }
}