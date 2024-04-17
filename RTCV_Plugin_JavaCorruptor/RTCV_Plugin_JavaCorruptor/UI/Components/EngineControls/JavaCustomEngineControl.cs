using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ObjectWeb.Asm.Tree;

namespace Java_Corruptor.UI.Components.EngineControls;

// TODO: regex shouldn't have to be put into <>, just interpret the instructions as regex and let the user escape characters as needed.
// This will allow for <> to be used for dynamic values in the replace instructions, like calling a method with a matched value as an argument, or doing logic.
public partial class JavaCustomEngineControl
{
    private string _path = "";
    private string[] _findInstructions;
    private string _findList, _replaceList, _findText, _replaceText;
    public JavaCustomEngineControl()
    {
        InitializeComponent();
    }

    private void btnSaveAs_Click(object sender, EventArgs e)
    {
        if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            return;
        
        _path = saveFileDialog1.FileName;

        string text = "[FIND]\n";
        text += tbFind.Text + "\n";
        text += "[REPLACE]\n";
        text += tbReplace.Text;

        File.WriteAllText(_path, text);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (_path != "") {
            string text = "[FIND]\r\n";
            text += tbFind.Text + "\r\n";
            text += "[REPLACE]\r\n";
            text += tbReplace.Text;

            File.WriteAllText(_path, text);
        } else {
            btnSaveAs_Click(sender, e);
        }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) {
            _path = openFileDialog1.FileName;
            string fileText = File.ReadAllText(_path);
            
            var texts = SeparateEngineText(fileText);
            tbFind.Text = texts.Find;
            tbReplace.Text = texts.Replace;
        }
    }

    private (string Find, string Replace) SeparateEngineText(string fileText)
    {
        string text = Regex.Replace(fileText, "([^\r])\n", "$1\r\n");
            
        // WinForms textboxes use \r\n for newlines, and will ignore \n on its own, so we need to replace \n with \r\n
        Regex blockComments = new(@"/\*(.|\n)*?\*/");
        Regex lineComments = new(@"//.*");
                
        Dictionary<int, (int, string)> comments = new();

        foreach (Match match in blockComments.Matches(text))
            comments.Add(match.Index, (match.Length, match.Value));

        foreach (Match match in lineComments.Matches(text))
            comments.Add(match.Index, (match.Length, match.Value));

        comments = comments.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        text = blockComments.Replace(text, "");
        text = lineComments.Replace(text, "");

        int findIndex = text.IndexOf("[FIND]\r\n", StringComparison.Ordinal);
        int replaceIndex = text.IndexOf("\r\n[REPLACE]\r\n", StringComparison.Ordinal);

        string find = text.Substring(findIndex + 8, replaceIndex - findIndex - 8); // 8 is the length of [FIND]\r\n
        string replace = text[(replaceIndex + 13)..]; // 13 is the length of \r\n[REPLACE]\r\n

        int offset = 0;
        foreach (var comment in comments) // Adds the comments back in at the correct positions
        {
            int position = comment.Key;
            int length = comment.Value.Item1;
            string value = comment.Value.Item2;
                    
            if (position > findIndex && position < replaceIndex)
                find = find.Insert(position - findIndex - 8 - offset, value);
            if (position > replaceIndex)
                replace = replace.Insert(position - replaceIndex - 13 - offset, value);
                
            offset += length;
        }
                
        return (find, replace);
    }

    public override void Prepare()
    {
        base.Prepare();
        _findText = tbFind.Text;
        _replaceText = tbReplace.Text;
        string code = "[FIND]\n";
        code += _findText + "\n";
        code += "[REPLACE]\n";
        code += _replaceText;
        
        code = Regex.Replace(code, "(?s)/\\*.*?\\*/", ""); // removes block comments
        code = Regex.Replace(code, "//.*", ""); // removes line comments
            
        int index = code.IndexOf("[FIND]", StringComparison.Ordinal);
            
        string find = code.Substring(index + 6, code.IndexOf("[REPLACE]", StringComparison.Ordinal) - index - 6).Trim();
        find = Regex.Replace(find, @"^\s*$[\r\n]*", "", RegexOptions.Multiline);
        find = find.Trim();
        find = Regex.Replace(find, @"([^\r])\n", "$1\r\n");
            
        string replace = code[(code.IndexOf("[REPLACE]", StringComparison.Ordinal) + 9)..].Trim();
        replace = Regex.Replace(replace, @"^\s*$[\r\n]*", "", RegexOptions.Multiline);
        replace = replace.Trim();
        replace = Regex.Replace(replace, @"([^\r])\n", "$1\r\n");
            
        _findInstructions = find.Split(["\r\n"], StringSplitOptions.None);
        replace.Split(["\r\n"], StringSplitOptions.None);
        _replaceList = replace;

        for (int i = 0; i < _findInstructions.Length; i++)
        {
            string parsing = _findInstructions[i];
            MatchCollection matches = Regex.Matches(parsing, "(?<=^|(?<!\\\\)>)(.*?)(?=(?<!\\\\)<|$)");
            foreach (Match match in matches)
            {
                //parsing = parsing.Replace(match.Value, Regex.Escape(match.Value));
                Regex regex = new(Regex.Escape(match.Value));
                parsing = regex.Replace(parsing, Regex.Escape(match.Value), 1);
            }
            //remove < and > unless escaped
            parsing = Regex.Replace(parsing, "(?<!\\\\)<", "");
            parsing = Regex.Replace(parsing, "(?<!\\\\)>", "");
            parsing = parsing.Replace("\\\\<", "<");
            parsing = parsing.Replace("\\\\>", ">");
            _findInstructions[i] = parsing;
        }
        _findList = string.Join(Environment.NewLine, _findInstructions);
    }

    public override void UpdateUI()
    {
        tbFind.Text = _findText;
        tbReplace.Text = _findText;
    }

    public override InsnList DoCorrupt(AbstractInsnNode insn, AsmParser parser, ref int replaces)
    {
        InsnList list = new();
        AbstractInsnNode currentInsn = insn;
        StringBuilder insns = new();

        for (int i = 0; i < _findInstructions.Length; i++)
        {
            string insnString = parser.InsnToString(currentInsn);
                
            if (!Regex.IsMatch(insnString, _findInstructions[i]))
                break;
                
            insns.Append(insnString).Append("\r\n");
                
            if (i == _findInstructions.Length - 1)
            {
                MatchCollection m = Regex.Matches(insns.ToString(), _findList, RegexOptions.Multiline);
                StringBuilder newInsns = new();

                foreach (Match match in m)
                {
                    string replace = _replaceList;
                    for (int j = 0; j < match.Groups.Count; j++)
                    {
                        string group = match.Groups[j + 1].Value;
                        replace = replace.Replace("<$" + (j + 1) + ">", group);
                    }
                    newInsns.Append(replace).Append("\r\n");
                }

                string[] newInsnStrings = newInsns.ToString().Trim().Split(["\r\n"], StringSplitOptions.None);
                foreach (string newInsnString in newInsnStrings)
                {
                    AbstractInsnNode newInsn = parser.ParseInsn(newInsnString);
                    list.Add(newInsn);
                }

                replaces = _findInstructions.Length;
                return list;
            }

            currentInsn = currentInsn.Next;
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
            settings.FindText = _findText;
            settings.ReplaceText = _replaceText;
            settings.FindInstructions = _findInstructions;
            settings.FindList = _findList;
            settings.ReplaceList = _replaceList;
            _engineSettings = settings;
            return settings;
        }
        set
        {
            dynamic settings = value;
            
            if (settings.FindInstructions is Array)
                _findInstructions = settings.FindInstructions;
            else
                _findInstructions = ((IEnumerable<object>)settings.FindInstructions).Select(x => (string)x).ToArray();
            _findList = settings.FindList;
            _replaceList = settings.ReplaceList;
        }
    }
}