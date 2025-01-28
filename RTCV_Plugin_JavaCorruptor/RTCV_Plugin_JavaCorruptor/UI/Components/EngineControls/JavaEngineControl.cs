using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using NLog;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class JavaEngineControl : UserControl
{
    protected AsmParser Parser = new();
    protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public event EventHandler EngineChanged;
    public JavaEngineControl()
    {
        InitializeComponent();
    }
    public void DisableComboBox()
    {
        placeholderComboBox.Enabled = false;
    }
    
    private void placeholderComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        EngineChanged?.Invoke(this, EventArgs.Empty);
    }

    public virtual List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces) { return new(); }

    public virtual void Prepare()
    {
        _engineSettings = null;
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected ExpandoObject _engineSettings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ExpandoObject EngineSettings
    {
        get;
        set;
    }
    public virtual void UpdateUI() { }

    public virtual bool Corrupt(ClassNode classNode)
    {
        bool modified = false;
        for (int index = 0; index < classNode.Methods.Count; index++)
        {
            MethodNode methodNode = classNode.Methods[index];
            if (CorruptionOptions.UseDomains && CorruptionOptions.FilterMethods.Count > 0 && CorruptionOptions.FilterMethods.All(m => m != methodNode.Name + methodNode.Desc))
                continue;
            List<AbstractInsnNode> insnList = [];
            //InsnList insnList = new();
            AsmParser parser = new();
            parser.RegisterLabelsFrom(methodNode.Instructions);

            //for (int i = 0; i < methodNode.Instructions.Size; i++)
            int i = 0;
            for (AbstractInsnNode insnNode = methodNode.Instructions.First; insnNode is not null; insnNode = insnNode.Next, i++)
            {
                /*AbstractInsnNode insnNode = methodNodeInstructions[i];
                if (insnNode is null)
                    break;*/
                //methodNode.Instructions.Remove(insnNode);
                //AbstractInsnNode insnNode = methodNodeInstructions[i];
                if (JavaCorruptionEngineForm.Intensity < JavaGeneralParametersForm.Random.NextDouble())
                {
                    insnList.Add(insnNode);
                    continue;
                }

                int replaces = -1;
                List<AbstractInsnNode> result = DoCorrupt(insnNode, parser, ref replaces);

                if (replaces == -1 || result is null || result.Count == 0 || (result.Count == 1 && result.First() == insnNode))
                {
                    insnList.Add(insnNode);
                    continue;
                }

                insnList.AddRange(result);
                /*foreach (AbstractInsnNode insn in result)
                    insnList.Add(insn);*/
                string key = classNode.Name + "." + methodNode.Name + methodNode.Desc;
                JavaBlastUnit unit = new(result, i, replaces, key, engine: GetType().Name,
                    engineSettings: EngineSettings);
                
                if (!JavaCorruptionEngineForm.BlastLayerCollection.ContainsKey(key))
                    JavaCorruptionEngineForm.BlastLayerCollection.Add(key, new JavaBlastLayer(unit));
                else
                    JavaCorruptionEngineForm.BlastLayerCollection.Add(unit);

                i += replaces - 1;
                while (replaces - 1 > 0)
                {
                    insnNode = insnNode.Next;
                    replaces--;
                }
                modified = true;
            }

            if (modified)
            {
                methodNode.Instructions.RemoveAll(true);
                foreach (AbstractInsnNode insn in insnList)
                {
                    methodNode.Instructions.Add(insn);
                }
            }
            //methodNode.Instructions = insnList;
        }

        return modified;
    }
}