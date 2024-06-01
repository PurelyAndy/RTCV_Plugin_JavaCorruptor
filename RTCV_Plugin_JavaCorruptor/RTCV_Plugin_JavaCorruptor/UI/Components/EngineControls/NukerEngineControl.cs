using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using Newtonsoft.Json;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class NukerEngineControl
{
    private int _charsetAmount = 5;

    public NukerEngineControl()
    {
        InitializeComponent();
    }

    public override void Corrupt(ClassNode classNode)
    {
        foreach (MethodNode methodNode in classNode.Methods)
        {
            if (JavaCorruptionEngineForm.Intensity < JavaGeneralParametersForm.Random.NextDouble())
                continue;
            
            AsmParser parser = new();
            parser.RegisterLabelsFrom(methodNode.Instructions);

            AbstractInsnNode insnNode = methodNode.Instructions.First;
            if (insnNode is null)
            {
                continue;
            }

            List<AbstractInsnNode> result = DoCorrupt(methodNode.Name, methodNode.Desc);
            int replaces = methodNode.Instructions.ToArray().Length - 1;

            if (result is null || result.Count == 0 || replaces == 0 || (result.Count == 1 && result.First() == insnNode))
            {
                //if (Array.IndexOf(methodNode.Instructions.ToArray(), insnNode) == -1)
                //    methodNode.Instructions.InsertBefore(methodNode.Instructions.First, insnNode);
                continue;
            }

            List<AbstractInsnNode> copy = result.ToList();
            string key = classNode.Name + "." + methodNode.Name + methodNode.Desc;
            JavaBlastUnit unit = new(copy, 0, replaces, key, engine: GetType().Name, engineSettings: EngineSettings);
            if (!JavaCorruptionEngineForm.BlastLayerCollection.ContainsKey(key))
                JavaCorruptionEngineForm.BlastLayerCollection.Add(key, JsonConvert.DeserializeObject<SerializedInsnBlastLayer>(JsonConvert.SerializeObject(new JavaBlastLayer(unit))));
            else
                JavaCorruptionEngineForm.BlastLayerCollection.Add(JsonConvert.DeserializeObject<SerializedInsnBlastUnit>(JsonConvert.SerializeObject(unit)));
            
            methodNode.Instructions.Clear();
            foreach (AbstractInsnNode i in result)
                methodNode.Instructions.Add(i);
            
            for (int i = 0; i < methodNode.TryCatchBlocks.Count; i++)
            {
                TryCatchBlockNode tcb = methodNode.TryCatchBlocks[i];
                if (tcb.Start == null || tcb.End == null || tcb.Handler == null)
                    continue;
                if (methodNode.Instructions.Contains(tcb.Start) && methodNode.Instructions.Contains(tcb.End) && methodNode.Instructions.Contains(tcb.Handler))
                    continue;
                methodNode.TryCatchBlocks.Remove(tcb);
                i--;
            }
            methodNode.LocalVariables.Clear();
        }
    }

    private bool _byte, _short, _int, _long, _float, _double, _char, _bool, _void, _string;
    private bool _byteRuntimeRandom, _shortRuntimeRandom, _intRuntimeRandom, _longRuntimeRandom, _floatRuntimeRandom, _doubleRuntimeRandom, _charRuntimeRandom, _boolRuntimeRandom, _stringRuntimeRandom;
    private double _byteMinimum, _byteMaximum, _shortMinimum, _shortMaximum, _intMinimum, _intMaximum, _longMinimum, _longMaximum, _floatMinimum, _floatMaximum, _doubleMinimum, _doubleMaximum;

    private void rbCharset_CheckedChanged(object sender, EventArgs e)
    {
        if (!rbCharset.Checked)
            return;

        // feeling lazy right now
        Form inputForm = new()
        {
            Width = 400,
            Height = 400,
            Text = "Close the window when you're done",
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            StartPosition = FormStartPosition.CenterScreen,
        };

        System.Windows.Forms.Label label = new()
        {
            Text = $"Enter the number of characters to use ({_charsetAmount})",
            Location = new(10, 10),
            AutoSize = true,
        };
        inputForm.Controls.Add(label);

        TrackBar trackBar = new()
        {
            Minimum = 1,
            Maximum = 300,
            Location = new(10, 30),
            Width = 380,
            TickFrequency = 10,
            Value = 5,
        };
        trackBar.Scroll += (_, _) =>
            label.Text = $"Enter the number of characters to use ({_charsetAmount = trackBar.Value})";
        inputForm.Controls.Add(trackBar);

        inputForm.ShowDialog();

        rbCharset.Text = $"Charset ({_charsetAmount})";
    }

    private string _characters;
    private bool _true, _false;
    private string _stringText;
    private bool _onePerLine;
    private bool _skipInit, _skipClinit;

    public override void Prepare()
    {
        base.Prepare();
        _byte = cbByte.Checked;
        _short = cbShort.Checked;
        _int = cbInt.Checked;
        _long = cbLong.Checked;
        _float = cbFloat.Checked;
        _double = cbDouble.Checked;
        _char = cbChar.Checked;
        _bool = cbBool.Checked;
        _void = cbVoid.Checked;
        _string = cbString.Checked;
        _byteRuntimeRandom = cbByteRuntimeRandom.Checked;
        _shortRuntimeRandom = cbShortRuntimeRandom.Checked;
        _intRuntimeRandom = cbIntRuntimeRandom.Checked;
        _longRuntimeRandom = cbLongRuntimeRandom.Checked;
        _floatRuntimeRandom = cbFloatRuntimeRandom.Checked;
        _doubleRuntimeRandom = cbDoubleRuntimeRandom.Checked;
        _charRuntimeRandom = cbCharRuntimeRandom.Checked;
        _boolRuntimeRandom = cbBoolRuntimeRandom.Checked;
        _stringRuntimeRandom = cbStringRuntimeRandom.Checked;
        _byteMinimum = tbByteMinimum.Value;
        _byteMaximum = tbByteMaximum.Value;
        _shortMinimum = tbShortMinimum.Value;
        _shortMaximum = tbShortMaximum.Value;
        _intMinimum = tbIntMinimum.Value;
        _intMaximum = tbIntMaximum.Value;
        _longMinimum = tbLongMinimum.Value;
        _longMaximum = tbLongMaximum.Value;
        _floatMinimum = tbFloatMinimum.Value;
        _floatMaximum = tbFloatMaximum.Value;
        _doubleMinimum = tbDoubleMinimum.Value;
        _doubleMaximum = tbDoubleMaximum.Value;
        _characters = tbCharacters.Text;
        _true = cbTrue.Checked;
        _false = cbFalse.Checked;
        _stringText = tbStringText.Text;
        _onePerLine = rbOnePerLine.Checked;
        _skipInit = cbSkipInit.Checked;
        _skipClinit = cbSkipClinit.Checked;
    }
    public override void UpdateUI()
    {
        cbByte.Checked = _byte;
        cbShort.Checked = _short;
        cbInt.Checked = _int;
        cbLong.Checked = _long;
        cbFloat.Checked = _float;
        cbDouble.Checked = _double;
        cbChar.Checked = _char;
        cbBool.Checked = _bool;
        cbVoid.Checked = _void;
        cbString.Checked = _string;
        cbByteRuntimeRandom.Checked = _byteRuntimeRandom;
        cbShortRuntimeRandom.Checked = _shortRuntimeRandom;
        cbIntRuntimeRandom.Checked = _intRuntimeRandom;
        cbLongRuntimeRandom.Checked = _longRuntimeRandom;
        cbFloatRuntimeRandom.Checked = _floatRuntimeRandom;
        cbDoubleRuntimeRandom.Checked = _doubleRuntimeRandom;
        cbCharRuntimeRandom.Checked = _charRuntimeRandom;
        cbBoolRuntimeRandom.Checked = _boolRuntimeRandom;
        cbStringRuntimeRandom.Checked = _stringRuntimeRandom;
        tbByteMinimum.Value = (int)_byteMinimum;
        tbByteMaximum.Value = (int)_byteMaximum;
        tbShortMinimum.Value = (int)_shortMinimum;
        tbShortMaximum.Value = (int)_shortMaximum;
        tbIntMinimum.Value = (int)_intMinimum;
        tbIntMaximum.Value = (int)_intMaximum;
        tbLongMinimum.Value = (int)_longMinimum;
        tbLongMaximum.Value = (int)_longMaximum;
        tbFloatMinimum.Value = (int)_floatMinimum;
        tbFloatMaximum.Value = (int)_floatMaximum;
        tbDoubleMinimum.Value = (int)_doubleMinimum;
        tbDoubleMaximum.Value = (int)_doubleMaximum;
        tbCharacters.Text = _characters;
        cbTrue.Checked = _true;
        cbFalse.Checked = _false;
        tbStringText.Text = _stringText;
        rbOnePerLine.Checked = _onePerLine;
        cbSkipInit.Checked = _skipInit;
        cbSkipClinit.Checked = _skipClinit;
    }

    private List<AbstractInsnNode> DoCorrupt(string method, string descriptor)
    {
        List< AbstractInsnNode> list = new();
        
        if (descriptor.Contains(")["))
            return null;
            
        if (_byte && descriptor.EndsWith("B"))
            list.AddRange(GetNumberReplacement(5, _byteRuntimeRandom, _byteMinimum, _byteMaximum));
        else if (_short && descriptor.EndsWith("S"))
            list.AddRange(GetNumberReplacement(7, _shortRuntimeRandom, _shortMinimum, _shortMaximum));
        else if (_int && descriptor.EndsWith("I"))
            list.AddRange(GetNumberReplacement(0, _intRuntimeRandom, _intMinimum, _intMaximum));
        else if (_long && descriptor.EndsWith("J"))
            list.AddRange(GetNumberReplacement(1, _longRuntimeRandom, _longMinimum, _longMaximum));
        else if (_float && descriptor.EndsWith("F"))
            list.AddRange(GetNumberReplacement(2, _floatRuntimeRandom, _floatMinimum / 1000d, _floatMaximum / 1000d));
        else if (_double && descriptor.EndsWith("D"))
            list.AddRange(GetNumberReplacement(3, _doubleRuntimeRandom, _doubleMinimum / 1000d, _doubleMaximum / 1000d));
        else if (_char && descriptor.EndsWith("C"))
            list.AddRange(GetCharReplacement());
        else if (_bool && descriptor.EndsWith("Z"))
            list.AddRange(GetBoolReplacement());
        else if (_void && descriptor.EndsWith("V"))
        {
            if (_skipClinit && method == "<clinit>")
                return null;
            if (_skipInit && method == "<init>")
                return null;
            list.Add(new InsnNode(Opcodes.Return));
        }
        else if (_string && descriptor.EndsWith("Ljava/lang/String;"))
            list.AddRange(GetStringReplacement());
        else
            return null;

        return list;
    }

    private IEnumerable<AbstractInsnNode> GetStringReplacement()
    {
        List<AbstractInsnNode> list = new();
        if (_onePerLine)
        {
            if (_stringRuntimeRandom)
            {
                //return (new String[]{"string", "test", "apple"})[(int)java.lang.Math.random() * 3];
        
                string[] lines = _stringText.Split('\n').Select(s => s.Replace("\r", "")).ToArray();
                list.Add(new LdcInsnNode(lines.Length));
                list.Add(new TypeInsnNode(Opcodes.Anewarray, "java/lang/String"));
                for (int i = 0; i < lines.Length; i++)
                {
                    list.Add(new InsnNode(Opcodes.Dup));
                    list.Add(new LdcInsnNode(i));
                    list.Add(new LdcInsnNode(lines[i]));
                    list.Add(new InsnNode(Opcodes.Aastore));
                }
                list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
                list.Add(new LdcInsnNode((double)lines.Length));
                list.Add(new InsnNode(Opcodes.Dmul));
                list.Add(new InsnNode(Opcodes.D2I));
                list.Add(new InsnNode(Opcodes.Aaload));
            }
            else
            {
                string[] lines = _stringText.Split('\n').Select(s => s.Replace("\r", "")).ToArray();
                list.Add(lines.Length > 0
                    ? new LdcInsnNode(lines[JavaGeneralParametersForm.Random.Next(lines.Length)])
                    : new("")
                );
            }
        }
        else //_charset
        {
            int amount = _charsetAmount;
            if (_stringRuntimeRandom)
            {
                list.Add(new TypeInsnNode(Opcodes.New, "java/lang/StringBuilder"));
                list.Add(new InsnNode(Opcodes.Dup));
                list.Add(new MethodInsnNode(Opcodes.Invokespecial, "java/lang/StringBuilder", "<init>", "()V", false));
                for (int i = 0; i < amount; i++)
                {
                    list.Add(new LdcInsnNode(_stringText));
                    list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
                    list.Add(new LdcInsnNode((double)_stringText.Length));
                    list.Add(new InsnNode(Opcodes.Dmul));
                    list.Add(new InsnNode(Opcodes.D2I));
                    list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/String", "charAt", "(I)C", false));
                    list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/StringBuilder", "append", "(C)Ljava/lang/StringBuilder;", false));
                }
                list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/StringBuilder", "toString", "()Ljava/lang/String;", false));
            }
            else
            {
                string str = "";
                for (int i = 0; i < amount; i++)
                    str += _stringText[JavaGeneralParametersForm.Random.Next(0, _stringText.Length)];
                list.Add(new LdcInsnNode(str));
            }
        }
        
        list.Add(new InsnNode(Opcodes.Areturn));
        return list;
    }

    private IEnumerable<AbstractInsnNode> GetBoolReplacement()
    {
        List<AbstractInsnNode> list = new();
        if (_boolRuntimeRandom)
        {
            // this is not possible to write in java syntax, but it's basically just getting a random 0 or 1, then returning it because booleans are returned with Ireturn as either 0 or 1
            // you could interpret it like: return (boolean)((int) (Math.random() + 0.5));
            list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(0.5d));
            list.Add(new InsnNode(Opcodes.Dadd));
            list.Add(new InsnNode(Opcodes.D2I));
        }
        else
        {
            /*if (@true && @false)
                list.Add(new LdcInsnNode(JavaGeneralParametersForm.Random.NextDouble() > 0.5));
            else if (@true)
                list.Add(new LdcInsnNode(true));
            else
                list.Add(new LdcInsnNode(false));*/
            list.Add(_true switch
            {
                true when _false => new LdcInsnNode(JavaGeneralParametersForm.Random.NextDouble() > 0.5),
                true => new(true),
                _ => new(false),
            });
        }

        list.Add(new InsnNode(Opcodes.Ireturn));
        return list;
    }

    private IEnumerable<AbstractInsnNode> GetCharReplacement()
    {
        List<AbstractInsnNode> list = new();
        string charset = _characters;
        
        if (_charRuntimeRandom)
        {
            // return charset.charAt((int)(Math.random() * charset.length()));
            list.Add(new LdcInsnNode(charset));
            list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(charset));
            list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/String", "length", "()I", false));
            list.Add(new InsnNode(Opcodes.I2D));
            list.Add(new InsnNode(Opcodes.Dmul));
            list.Add(new InsnNode(Opcodes.D2I));
            list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/String", "charAt", "(I)C", false));
            list.Add(new InsnNode(Opcodes.Ireturn));
        }
        else
        {
            list.Add(new LdcInsnNode(charset[JavaGeneralParametersForm.Random.Next(0, charset.Length)]));
            list.Add(new InsnNode(Opcodes.Ireturn));
        }

        return list;
    }
    //ILFDABCS
    private IEnumerable<AbstractInsnNode> GetNumberReplacement(int type, bool runtimeRandom, double min, double max)
    {
        List<AbstractInsnNode> list = new();

        if (runtimeRandom)
        {
            // return (int)(Math.random() * (max - min) + min);
            list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
            list.Add(new LdcInsnNode(max - min));
            list.Add(new InsnNode(Opcodes.Dmul));
            list.Add(new LdcInsnNode(min));
            list.Add(new InsnNode(Opcodes.Dadd));
            switch (type)
            {
                case >= 0 and <= 2:
                    list.Add(new InsnNode(Opcodes.D2I + type));
                    list.Add(new InsnNode(Opcodes.Ireturn + type));
                    break;
                case 5 or 7:
                    list.Add(new InsnNode(Opcodes.D2I));
                    list.Add(new InsnNode(Opcodes.F2L + type)); // I2B or I2S
                    list.Add(new InsnNode(Opcodes.Ireturn));
                    break;
                default:
                    list.Add(new InsnNode(Opcodes.Dreturn));
                    break;
            }
        }
        else
        {   
            switch (type)
            {
                case 0:
                    list.Add(new LdcInsnNode((int)(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min)));
                    list.Add(new InsnNode(Opcodes.Ireturn));
                    break;
                case 1:
                    list.Add(new LdcInsnNode((long)(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min)));
                    list.Add(new InsnNode(Opcodes.Lreturn));
                    break;
                case 2:
                    list.Add(new LdcInsnNode((float)(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min)));
                    list.Add(new InsnNode(Opcodes.Freturn));
                    break;
                case 3:
                    list.Add(new LdcInsnNode(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min));
                    list.Add(new InsnNode(Opcodes.Dreturn));
                    break;
                case 5: //TODO: i think these 2 can be included with case 0, but not 100% sure
                    list.Add(new LdcInsnNode((byte)(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min)));
                    list.Add(new InsnNode(Opcodes.Ireturn));
                    break;
                case 7:
                    list.Add(new LdcInsnNode((short)(JavaGeneralParametersForm.Random.NextDouble() * (max - min) + min)));
                    list.Add(new InsnNode(Opcodes.Ireturn));
                    break;
            }
        }

        return list;
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            if (_byte)
            {
                settings.Byte = true;
                settings.ByteRuntimeRandom = _byteRuntimeRandom;
                settings.ByteMinimum = _byteMinimum;
                settings.ByteMaximum = _byteMaximum;
            }
            if (_short)
            {
                settings.Short = true;
                settings.ShortRuntimeRandom = _shortRuntimeRandom;
                settings.ShortMinimum = _shortMinimum;
                settings.ShortMaximum = _shortMaximum;
            }
            if (_int)
            {
                settings.Int = true;
                settings.IntRuntimeRandom = _intRuntimeRandom;
                settings.IntMinimum = _intMinimum;
                settings.IntMaximum = _intMaximum;
            }
            if (_long)
            {
                settings.Long = true;
                settings.LongRuntimeRandom = _longRuntimeRandom;
                settings.LongMinimum = _longMinimum;
                settings.LongMaximum = _longMaximum;
            }
            if (_float)
            {
                settings.Float = true;
                settings.FloatRuntimeRandom = _floatRuntimeRandom;
                settings.FloatMinimum = _floatMinimum;
                settings.FloatMaximum = _floatMaximum;
            }
            if (_double)
            {
                settings.Double = true;
                settings.DoubleRuntimeRandom = _doubleRuntimeRandom;
                settings.DoubleMinimum = _doubleMinimum;
                settings.DoubleMaximum = _doubleMaximum;
            }
            if (_char)
            {
                settings.Char = true;
                settings.CharRuntimeRandom = _charRuntimeRandom;
                settings.CharCharacters = _characters;
            }
            if (_bool)
            {
                settings.Bool = true;
                settings.BoolRuntimeRandom = _boolRuntimeRandom;
                settings.BoolTrue = _true;
                settings.BoolFalse = _false;
            }
            if (_void)
            {
                settings.Void = true;
                settings.SkipClinit = _skipClinit;
                settings.SkipInit = _skipInit;
            }
            if (_string)
            {
                settings.String = true;
                settings.StringRuntimeRandom = _stringRuntimeRandom;
                settings.StringOnePerLine = _onePerLine;
                settings.StringText = _stringText;
                settings.StringCharsetAmount = _charsetAmount;
            }
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            IDictionary<string, object> dict = settings;
            
            if (dict.ContainsKey("Byte") && settings.Byte)
            {
                _byte = true;
                _byteRuntimeRandom = settings.ByteRuntimeRandom;
                _byteMinimum = settings.ByteMinimum;
                _byteMaximum = settings.ByteMaximum;
            }
            if (dict.ContainsKey("Short") && settings.Short)
            {
                _short = true;
                _shortRuntimeRandom = settings.ShortRuntimeRandom;
                _shortMinimum = settings.ShortMinimum;
                _shortMaximum = settings.ShortMaximum;
            }
            if (dict.ContainsKey("Int") && settings.Int)
            {
                _int = true;
                _intRuntimeRandom = settings.IntRuntimeRandom;
                _intMinimum = settings.IntMinimum;
                _intMaximum = settings.IntMaximum;
            }
            if (dict.ContainsKey("Long") && settings.Long)
            {
                _long = true;
                _longRuntimeRandom = settings.LongRuntimeRandom;
                _longMinimum = settings.LongMinimum;
                _longMaximum = settings.LongMaximum;
            }
            if (dict.ContainsKey("Float") && settings.Float)
            {
                _float = true;
                _floatRuntimeRandom = settings.FloatRuntimeRandom;
                _floatMinimum = settings.FloatMinimum;
                _floatMaximum = settings.FloatMaximum;
            }
            if (dict.ContainsKey("Double") && settings.Double)
            {
                _double = true;
                _doubleRuntimeRandom = settings.DoubleRuntimeRandom;
                _doubleMinimum = settings.DoubleMinimum;
                _doubleMaximum = settings.DoubleMaximum;
            }
            if (dict.ContainsKey("Char") && settings.Char)
            {
                _char = true;
                _charRuntimeRandom = settings.CharRuntimeRandom;
                _characters = settings.CharCharacters;
            }
            if (dict.ContainsKey("Bool") && settings.Bool)
            {
                _bool = true;
                _boolRuntimeRandom = settings.BoolRuntimeRandom;
                _true = settings.BoolTrue;
                _false = settings.BoolFalse;
            }
            if (dict.ContainsKey("Void") && settings.Void)
            {
                _void = true;
                _skipClinit = settings.SkipClinit;
                _skipInit = settings.SkipInit;
            }
            if (dict.ContainsKey("String") && settings.String)
            {
                _string = true;
                _stringRuntimeRandom = settings.StringRuntimeRandom;
                _onePerLine = settings.StringOnePerLine;
                _stringText = settings.StringText;
                _charsetAmount = settings.StringCharsetAmount;
            }
        }
    }

    private void UpdateTrackbar(object sender, EventArgs e)
    {
        // We can do this because the trackbar names are tb{Type}Minimum/Maximum, and the labels are lb{Type}Minimum/Maximum
        string associatedLabelName = "l" + ((TrackBar)sender).Name[1..];
        System.Windows.Forms.Label associatedLabel = (System.Windows.Forms.Label)Controls.Find(associatedLabelName, true)[0];
        bool max = associatedLabelName.EndsWith("Maximum");
        if (max)
            associatedLabel.Text = "Maximum: " + ((TrackBar)sender).Value.ToString();
        else
            associatedLabel.Text = "Minimum: " + ((TrackBar)sender).Value.ToString();
    }

    private void UpdateFloatTrackbar(object sender, EventArgs e)
    {
        string associatedLabelName = "l" + ((TrackBar)sender).Name[1..];
        System.Windows.Forms.Label associatedLabel = (System.Windows.Forms.Label)Controls.Find(associatedLabelName, true)[0];
        bool max = associatedLabelName.EndsWith("Maximum");
        if (max)
            associatedLabel.Text = "Maximum: " + ((double)((TrackBar)sender).Value / 1000).ToString(CultureInfo.CurrentCulture);
        else
            associatedLabel.Text = "Minimum: " + ((double)((TrackBar)sender).Value / 1000).ToString(CultureInfo.CurrentCulture);
    }

    private void cbFalse_CheckedChanged(object sender, EventArgs e)
    {
        if (!cbFalse.Checked && !cbTrue.Checked)
            cbTrue.Checked = true;
        if (!cbFalse.Checked || !cbTrue.Checked)
        {
            cbBoolRuntimeRandom.Checked = false;
            cbBoolRuntimeRandom.Enabled = false;
        }
        else
            cbBoolRuntimeRandom.Enabled = true;
    }

    private void cbTrue_CheckedChanged(object sender, EventArgs e)
    {
        if (!cbFalse.Checked && !cbTrue.Checked)
            cbFalse.Checked = true;
        if (!cbFalse.Checked || !cbTrue.Checked)
        {
            cbBoolRuntimeRandom.Checked = false;
            cbBoolRuntimeRandom.Enabled = false;
        }
        else
            cbBoolRuntimeRandom.Enabled = true;
    }
}