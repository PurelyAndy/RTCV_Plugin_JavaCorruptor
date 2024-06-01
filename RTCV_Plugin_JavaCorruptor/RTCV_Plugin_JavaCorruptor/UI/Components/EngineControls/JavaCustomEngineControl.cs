using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.Common;
using RTCV.Common.CustomExtensions;
using RTCV.CorruptCore;
using RTCV.UI;
using WinFormsSyntaxHighlighter;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Java_Corruptor.UI.Components.EngineControls;

// TODO: regex shouldn't have to be put into <>, just interpret the instructions as regex and let the user escape characters as needed.
// This will allow for <> to be used for dynamic values in the replace instructions, like calling a method with a matched value as an argument, or doing logic.
public partial class JavaCustomEngineControl
{
    private readonly SyntaxHighlighter _findHighlighter;
    private readonly SyntaxHighlighter _replaceHighlighter;
    private readonly Regex _blockCommentRegex = new(@"/\*(.|\n)*?\*/", RegexOptions.Compiled);
    private readonly Regex _lineCommentRegex = new(@"//.*", RegexOptions.Compiled);
    private readonly Regex _lineEndingFix = new(@"([^\r])\n", RegexOptions.Compiled);
    private readonly Regex _ifElseRegex = new(@"<if \$(\d+) (==|!=) (.+?(?<!\\))>(.+?(?<!\\))<else>(.+?(?<!\\))</if>", RegexOptions.Compiled);
    private readonly Regex _randomRegex = new(@"<random(F|D|I|L) ([1-9]\d*\.\d*|0?\.\d*[1-9]\d*|[1-9]\d*),([1-9]\d*\.\d*|0?\.\d*[1-9]\d*|[1-9]\d*)>", RegexOptions.Compiled);
    private readonly Regex _labelNameRegex = new(@"<label (\w+)>", RegexOptions.Compiled);
    private string _path = "";
    private string[] _findInstructions, _replaceInstructions;
    private Regex[] _findRegexes;
    private string _findList, _replaceList, _findText, _replaceText;
    private Regex _findRegex;

    public JavaCustomEngineControl()
    {
        InitializeComponent();
        string opcodePattern = string.Join("|", AsmUtilities.Opcodes.Values);
        _findHighlighter = new(tbFind);
        _findHighlighter.AddPattern(new(_blockCommentRegex), new(Color.FromArgb(106, 169, 101)));
        _findHighlighter.AddPattern(new(_lineCommentRegex), new(Color.FromArgb(106, 169, 101)));
        _findHighlighter.AddPattern(new(opcodePattern), new(Color.FromArgb(86, 156, 214)));

        _replaceHighlighter = new(tbReplace);
        _replaceHighlighter.AddPattern(new(_blockCommentRegex), new(Color.FromArgb(106, 169, 101)));
        _replaceHighlighter.AddPattern(new(_lineCommentRegex), new(Color.FromArgb(106, 169, 101)));
        _replaceHighlighter.AddPattern(new(opcodePattern), new(Color.FromArgb(86, 156, 214)));
        _replaceHighlighter.AddPattern(new("if|else|random|label"), new(Color.FromArgb(197, 134, 192)));
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
    
    private void EngineTextChanged(object sender, EventArgs e)
    {
        btnErrorCheck.BackColor = Color.FromArgb(224, 128, 128);
        btnErrorCheck.ForeColor = Color.Black;
    }
    
    private void CheckForErrors(object sender, EventArgs e)
    {
        Prepare();
        AsmUtilities.Classes.Clear();
        JavaBlastTools.LoadClassesFromCurrentJar();
        List<string> errors = [];
        foreach (ClassNode clazz in AsmUtilities.Classes.Values)
        {
            foreach (MethodNode method in clazz.Methods)
            {
                AsmParser parser = new();
                parser.RegisterLabelsFrom(method.Instructions);
                AbstractInsnNode insn = method.Instructions.First;
                int index = 0;
                while (insn != null)
                {
                    AbstractInsnNode currentInsn = insn;
                    StringBuilder insns = new();

                    for (int i = 0; i < _findInstructions.Length; i++)
                    {
                        string insnString = parser.InsnToString(currentInsn);

                        if (!_findRegexes[i].IsMatch(insnString))
                            break;

                        insns.Append(insnString).Append("\r\n");

                        if (i == _findInstructions.Length - 1)
                        {
                            string[] newInsnStrings = ProcessEngineCode(insns, parser);
                            
                            for (int j = 0; j < newInsnStrings.Length; j++)
                            {
                                string newInsnString = newInsnStrings[j];
                                if (!parser.ValidateInsn(newInsnString, out string message))
                                {
                                    errors.Add($"Error in: {clazz.Name}.{method.Name}{method.Desc}\nat instruction: {index}\ncaused by: {_replaceInstructions[j]}\n{message}");
                                }
                            }

                            break;
                        }

                        currentInsn = currentInsn.Next;
                    }

                    insn = insn.Next;
                    index++;
                }
            }
        }
        
        if (errors.Any(x => x != null))
        {
            bool userIsACoward = false;
            string errorText = "";
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<long> times = [];
            int i = 0;
            for (; i < errors.Count; i++)
            {
                errorText += $"  {i % 5 + 1}. {errors[i]}\n";
                if (i % 5 == 4)
                {
                    string text = errorText;
                    text += $"\n\n{errors.Count - 1 - i} more errors found, do you want to see more?";
                    if (errors.Count > 125 && times.Count > 1)
                    {
                        long average = (long)times.GetRange(Math.Max(times.Count - 5, 0), Math.Min(5, times.Count)).Average();
                        text += $" Keep going! At this rate, it'll take you just {average * (errors.Count - i - 1) / 1000} seconds to finish reading all of these errors!";
                    }
                    errorText = "";
                    if (DialogResult.Yes != MessageBox.Show(text, $"Errors found - {(float)(i + 1)/errors.Count:P2} through", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                    {
                        userIsACoward = true;
                        break;
                    }
                    times.Add(stopwatch.ElapsedMilliseconds);
                    stopwatch.Restart();
                }
            }
            if (i % 5 != 0 && !userIsACoward)
                MessageBox.Show(errorText, "Errors found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);
            if (errors.Count > 125 && !userIsACoward)
            {
                if (File.Exists(@"C:\Windows\Media\tada.wav"))
                {
                    using SoundPlayer soundPlayer = new(@"C:\Windows\Media\tada.wav");
                    soundPlayer.Play();
                }
                MessageBox.Show(
                    $"You did it! You read all {errors.Count} of the errors, and it only took you {times.Aggregate((a, b) => a + b) / 1000} seconds!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        else
        {
            MessageBox.Show("No errors found", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnErrorCheck.BackColor = btnSave.BackColor;
            btnErrorCheck.ForeColor = Color.White;
        }
    }

    private (string Find, string Replace) SeparateEngineText(string fileText)
    {
        string text = _lineEndingFix.Replace(fileText, "$1\r\n");
        
        Dictionary<int, (int, string)> comments = new();

        foreach (Match match in _blockCommentRegex.Matches(text))
            comments.Add(match.Index, (match.Length, match.Value));

        foreach (Match match in _lineCommentRegex.Matches(text))
            comments.Add(match.Index, (match.Length, match.Value));

        comments = comments.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        text = _blockCommentRegex.Replace(text, "");
        text = _lineCommentRegex.Replace(text, "");

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
        find = _lineEndingFix.Replace(find, "$1\r\n");
            
        string replace = code[(code.IndexOf("[REPLACE]", StringComparison.Ordinal) + 9)..].Trim();
        replace = Regex.Replace(replace, @"^\s*$[\r\n]*", "", RegexOptions.Multiline);
        replace = replace.Trim();
        replace = _lineEndingFix.Replace(find, "$1\r\n");
            
        _findInstructions = find.Split(["\r\n"], StringSplitOptions.None);
        _replaceInstructions = replace.Split(["\r\n"], StringSplitOptions.None);

        EscapeNonRegexSectionsOf(_findInstructions);
        _findList = string.Join(Environment.NewLine, _findInstructions);
        if (!_findList.StartsWith("^"))
            _findList = "^" + _findList;
        _findList += @"\r?$";
        _replaceList = replace;
        
        _findRegex = new(_findList, RegexOptions.Multiline | RegexOptions.Compiled);
        _findRegexes = new Regex[_findInstructions.Length];
        for (int i = 0; i < _findInstructions.Length; i++)
        {
            _findRegexes[i] = new(_findInstructions[i], RegexOptions.Compiled);
        }
    }

    private static void EscapeNonRegexSectionsOf(IList<string> textList)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            string parsing = textList[i];
            MatchCollection matches = Regex.Matches(parsing, "(?<=^|(?<!\\\\)>)(.*?)(?=(?<!\\\\)<|$)");
            foreach (Match match in matches)
            {
                Regex regex = new(Regex.Escape(match.Value));
                parsing = regex.Replace(parsing, Regex.Escape(match.Value), 1); // Escape all the text that isn't in <>
            }
            // Remove the unescaped < and > characters
            parsing = Regex.Replace(parsing, "(?<!\\\\)<", "");
            parsing = Regex.Replace(parsing, "(?<!\\\\)>", "");
            parsing = parsing.Replace("\\\\<", "<");
            parsing = parsing.Replace("\\\\>", ">");
            if (parsing.EndsWith("$"))
                parsing = parsing[..^1];
            textList[i] = parsing;
        }
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
                
            if (!_findRegexes[i].IsMatch(insnString))
                break;
            
            insns.Append(insnString).Append("\r\n");
                
            if (i == _findInstructions.Length - 1)
            {
                string[] newInsnStrings = ProcessEngineCode(insns, parser);
                
                foreach (string newInsnString in newInsnStrings)
                {
                    list.Add(parser.ParseInsn(newInsnString));
                }

                replaces = _findInstructions.Length;
                return list;
            }

            currentInsn = currentInsn.Next;
        }

        return list;
    }

    private string[] ProcessEngineCode(StringBuilder insns, AsmParser parser)
    {
        MatchCollection m = _findRegex.Matches(insns.ToString());
        StringBuilder newInsns = new();
        
        foreach (Match match in m)
        {
            string newInsn = _replaceList;
            for (int j = 0; j < match.Groups.Count; j++)
            {
                string matchText = match.Groups[j + 1].Value;
                newInsn = newInsn.Replace("<$" + (j + 1) + ">", matchText);
            }
            MatchCollection ifElseMatches = _ifElseRegex.Matches(newInsn);
            foreach (Match ifElseMatch in ifElseMatches)
            {
                string groupText = match.Groups[int.Parse(ifElseMatch.Groups[1].Value)].Value;
                string operation = ifElseMatch.Groups[2].Value;
                string value = ifElseMatch.Groups[3].Value.Replace("\\<", "<");
                string ifTrue = ifElseMatch.Groups[4].Value.Replace("\\<", "<");
                string ifFalse = ifElseMatch.Groups[5].Value.Replace("\\<", "<");
                string replacement = "";
                if (operation == "==")
                    replacement = groupText == value ? ifTrue : ifFalse;
                else if (operation == "!=")
                    replacement = groupText != value ? ifTrue : ifFalse;
                newInsn = newInsn.Replace(ifElseMatch.Value, replacement);
            }
            MatchCollection randomMatches = _randomRegex.Matches(newInsn);
            foreach (Match randomMatch in randomMatches)
            {
                string type = randomMatch.Groups[1].Value;
                string min = randomMatch.Groups[2].Value;
                string max = randomMatch.Groups[3].Value;
                Random rand = JavaGeneralParametersForm.Random;
                string result = type switch
                {
                    "F" => ((float)(rand.NextDouble() * (double.Parse(max) - double.Parse(min)) + double.Parse(min))).ToString(CultureInfo.InvariantCulture),
                    "D" => (rand.NextDouble() * (double.Parse(max) - double.Parse(min)) + double.Parse(min)).ToString(CultureInfo.InvariantCulture),
                    "I" => rand.Next(int.Parse(min), int.Parse(max)).ToString(),
                    "L" => rand.NextLong(long.Parse(min), long.Parse(max)).ToString(),
                    _ => "",
                };
                newInsn = newInsn.Replace(randomMatch.Value, result);
            }
            Dictionary<string, string> labels = new();
            MatchCollection labelNameMatches = _labelNameRegex.Matches(newInsn);
            foreach (Match labelNameMatch in labelNameMatches)
            {
                string labelName = labelNameMatch.Groups[1].Value;
                if (labels.TryGetValue(labelName, out string label3))
                    newInsn = newInsn.Replace(labelNameMatch.Value, label3);
                else
                {
                    string label = JavaGeneralParametersForm.Random.Next().ToString();
                    labels.Add(labelName, label);
                    newInsn = newInsn.Replace(labelNameMatch.Value, label);
                }
            }
            newInsns.Append(newInsn).Append("\r\n");
        }
        string str = newInsns.ToString().Trim().Replace("<random>", () => JavaGeneralParametersForm.Random.NextDouble().ToString(CultureInfo.InvariantCulture));

        string[] newInsnStrings = str.Split(["\r\n"], StringSplitOptions.None);
        return newInsnStrings;
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