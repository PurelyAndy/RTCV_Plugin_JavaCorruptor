using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using Newtonsoft.Json;
using NLog;
using ObjectWeb.Asm.Tree;
using RTCV.Common;
using RTCV.NetCore;

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

    public virtual InsnList DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces) { return new(); }

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

    public virtual void Corrupt(ClassNode classNode)
    {
        for (int index = 0; index < classNode.Methods.Count; index++)
        {
            MethodNode methodNode = classNode.Methods[index];
            List<AbstractInsnNode> insnList = [];
            //InsnList insnList = new();
            AsmParser parser = new();
            parser.RegisterLabelsFrom(methodNode.Instructions);

            //AbstractInsnNode insnNode = methodNode.Instructions.First;
            AbstractInsnNode[] methodNodeInstructions = methodNode.Instructions.ToArray();
            for (int i = 0; i < methodNodeInstructions.Length; i++)
            {
                AbstractInsnNode insnNode = methodNodeInstructions[i];
                if (insnNode is null)
                    break;
                //methodNode.Instructions.Remove(insnNode);
                //AbstractInsnNode insnNode = methodNodeInstructions[i];
                if (JavaCorruptionEngineForm.Intensity < JavaGeneralParametersForm.Random.NextDouble())
                {
                    insnList.Add(insnNode);
                    continue;
                }

                int replaces = -1;
                InsnList result = DoCorrupt(insnNode, parser, ref replaces);

                if (replaces == -1 || result is null || result.Size == 0 || (result.Size == 1 && result.First == insnNode))
                {
                    insnList.Add(insnNode);
                    continue;
                }
                
                List<AbstractInsnNode> copy = result.ToList();
                insnList.AddRange(result);
                result.RemoveAll(true);
                /*foreach (AbstractInsnNode insn in result)
                    insnList.Add(insn);*/
                string key = classNode.Name + "." + methodNode.Name + methodNode.Desc;
                JavaBlastUnit unit = new(copy, i, replaces, key, engine: GetType().Name,
                    engineSettings: EngineSettings);
                
                if (!JavaCorruptionEngineForm.BlastLayerCollection.ContainsKey(key))
                    JavaCorruptionEngineForm.BlastLayerCollection.Add(key, JsonConvert.DeserializeObject<SerializedInsnBlastLayer>(JsonConvert.SerializeObject(new JavaBlastLayer(unit))));
                else
                    JavaCorruptionEngineForm.BlastLayerCollection[key].Layer.Add(JsonConvert.DeserializeObject<SerializedInsnBlastUnit>(JsonConvert.SerializeObject(unit)));
                

                i += replaces - 1;
            }

            methodNode.Instructions.RemoveAll(true);
            foreach (AbstractInsnNode insn in insnList)
            {
                methodNode.Instructions.Add(insn);
            }
            //methodNode.Instructions = insnList;
        }
    }
}