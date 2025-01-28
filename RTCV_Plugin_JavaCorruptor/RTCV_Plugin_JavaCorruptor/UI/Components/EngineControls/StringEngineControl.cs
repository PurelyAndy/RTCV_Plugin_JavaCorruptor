using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

public partial class StringEngineControl
{
    public StringEngineControl()
    { 
        InitializeComponent();
        cbMode.SelectedIndex = 0;
    }

    private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbRuntimeRandom.Checked)
            tbCharacters.Enabled = cbMode.SelectedIndex is 0 or 1;
        else
        {
            tbCharacters.Enabled = cbMode.SelectedIndex is 0 or 3;
            cbRuntimeRandom.Enabled = cbMode.SelectedIndex is 0 or 3;
            if (!cbRuntimeRandom.Enabled)
                cbRuntimeRandom.Checked = false;
        }
    }

    private void cbRuntimeRandom_CheckedChanged(object sender, EventArgs e)
    {
        int i = cbMode.SelectedIndex;
        if (cbRuntimeRandom.Checked)
        {
            cbMode.Items.Clear();
            cbMode.Items.AddRange([
                "Nightmare",
                "One per line",
            ]);
            cbMode.SelectedIndex = i == 3 ? 1 : 0;
        }
        else
        {
            cbMode.Items.Clear();
            cbMode.Items.AddRange([
                "Nightmare",
                "Swap",
                "Cluster",
                "One per line",
            ]);
            cbMode.SelectedIndex = i == 1 ? 3 : 0;
        }
    }

    private void tbPercentage_Scroll(object sender, EventArgs e) =>
        lbPercentage.Text = $"Percentage: {tbPercentage.Value}";

    private double _percentage;
    private string _characters;
    private int _mode;
    private bool _onlySpaces, _runtimeRandom;


    public override void Prepare()
    {
        _engineSettings = null;
        _percentage = tbPercentage.Value / 100d;
        _characters = tbCharacters.Text;
        _onlySpaces = cbOnlySpaces.Checked;
        _mode = cbMode.SelectedIndex;
        _runtimeRandom = cbRuntimeRandom.Checked;
    }
    public override void UpdateUI()
    {
        tbPercentage.Value = (int)(_percentage * 100);
        tbCharacters.Text = _characters;
        cbOnlySpaces.Checked = _onlySpaces;
        cbMode.SelectedIndex = _mode;
        cbRuntimeRandom.Checked = _runtimeRandom;
    }

    public override List<AbstractInsnNode> DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        List<AbstractInsnNode> list = [];

        if (insn is not LdcInsnNode ldcInsn)
            return list;

        if (ldcInsn.Cst is not string cst)
            return list;

        if (_onlySpaces && !cst.Contains(" "))
            return list;

        if (_runtimeRandom)
        {
            if (_mode == 0)
            {
                // Nightmare mode: Replace random characters in the string with random characters from the charset
                list.Add(new TypeInsnNode(Opcodes.New, "java/lang/StringBuilder"));
                list.Add(new InsnNode(Opcodes.Dup));
                list.Add(new MethodInsnNode(Opcodes.Invokespecial, "java/lang/StringBuilder", "<init>", "()V", false));
                foreach (char t in cst)
                {
                    if (JavaGeneralParametersForm.Random.NextDouble() < _percentage)
                    {
                        list.Add(new LdcInsnNode(_characters));
                        list.Add(new MethodInsnNode(Opcodes.Invokestatic, "java/lang/Math", "random", "()D", false));
                        list.Add(new LdcInsnNode((double)_characters.Length));
                        list.Add(new InsnNode(Opcodes.Dmul));
                        list.Add(new InsnNode(Opcodes.D2I));
                        list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/String", "charAt", "(I)C", false));
                        list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/StringBuilder", "append", "(C)Ljava/lang/StringBuilder;", false));
                    }
                    else
                    {
                        list.Add(new LdcInsnNode(t));
                        list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/StringBuilder", "append", "(C)Ljava/lang/StringBuilder;", false));
                    }
                }
                list.Add(new MethodInsnNode(Opcodes.Invokevirtual, "java/lang/StringBuilder", "toString", "()Ljava/lang/String;", false));
            }
            else //if (_mode == 1)
            {
                // One per line mode: Pick a random string from each line of the given text
                //return (new String[]{"string", "test", "apple"})[(int)java.lang.Math.random() * 3];

                string[] lines = _characters.Split('\n').Select(s => s.Replace("\r", "")).ToArray();
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
        }
        else
        {
            if (_mode == 0)
            {
                // Nightmare mode: Replace random characters in the string with random characters from the charset
                StringBuilder sb = new();

                foreach (char c in cst)
                    if (JavaGeneralParametersForm.Random.NextDouble() < _percentage)
                        sb.Append(_characters[JavaGeneralParametersForm.Random.Next(_characters.Length)]);
                    else
                        sb.Append(c);

                ldcInsn.Cst = sb.ToString();
                list.Add(ldcInsn.Clone());
            }
            else if (_mode == 1)
            {
                // Swap mode: Swap random characters in the string with each other
                StringBuilder sb = new(cst);

                for (int i = 0; i < cst.Length; i++)
                    if (JavaGeneralParametersForm.Random.NextDouble() < _percentage / 2) // divide by 2 because each swap changes 2 characters
                    {
                        int j = JavaGeneralParametersForm.Random.Next(cst.Length);
                        (sb[i], sb[j]) = (sb[j], sb[i]); //this is such a cool feature
                    }

                ldcInsn.Cst = sb.ToString();
                list.Add(ldcInsn.Clone());
            }
            else if (_mode == 2)
            {
                // Cluster mode: Swap mode, but only with adjacent characters
                StringBuilder sb = new(cst);

                for (int i = 0; i < cst.Length; i++)
                    if (JavaGeneralParametersForm.Random.NextDouble() < _percentage / 2) // divide by 2 because each swap changes 2 characters
                    {
                        int r = JavaGeneralParametersForm.Random.Next(2);
                        int j = (r == 0 ? -1 : 1) + i;
                        if (j < 0)
                            j += 2;
                        if (j >= cst.Length)
                            j -= 2;
                        if (j < 0)
                            continue;

                        (sb[i], sb[j]) = (sb[j], sb[i]);
                    }

                ldcInsn.Cst = sb.ToString();
                list.Add(ldcInsn.Clone());
            }
            else if (_mode == 3)
            {
                // One per line mode: Pick a random string from each line of the given text

                string[] split = _characters.Split('\n').Select(s => s.Replace("\r", "")).ToArray();
                ldcInsn.Cst = split.Length > 0 ? split[JavaGeneralParametersForm.Random.Next(split.Length)] : "";
                list.Add(ldcInsn.Clone());
            }
        }

        replaces = 1;
        return list;
    }
    
    public override ExpandoObject EngineSettings
    {
        get
        {
            if (_engineSettings is not null)
                return _engineSettings;
            dynamic settings = new ExpandoObject();
            settings.Percentage = _percentage;
            settings.Characters = _characters;
            settings.OnlySpaces = _onlySpaces;
            settings.Mode = _mode;
            settings.RuntimeRandom = _runtimeRandom;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            _percentage = settings.Percentage;
            _characters = settings.Characters;
            _onlySpaces = settings.OnlySpaces;
            _mode = (int)settings.Mode;
            if (((IDictionary<string, object>)settings).ContainsKey("RuntimeRandom"))
                _runtimeRandom = settings.RuntimeRandom;
        }
    }
}