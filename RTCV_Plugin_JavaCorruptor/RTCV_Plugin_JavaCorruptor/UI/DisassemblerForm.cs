using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Java_Corruptor;
using Java_Corruptor.BlastClasses;
using ObjectWeb.Asm.Tree;
using WinFormsSyntaxHighlighter;

namespace Java_Corruptor.UI
{
    public partial class DisassemblerForm : RTCV.UI.Modular.ColorizedForm
    {
        private readonly SyntaxHighlighter _highlighter;
        private readonly Regex _stringRegex = new("\"[^\"]+\"", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _className = new(@"(?<=\/){0,1}(?<!;|\()(?!L|\d)\w+(?=\.|;|\$|(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _numbers = new(@"(?<=\$| )(?:\d+\.)?\d+[ILFD]?", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _package = new(@"(?<!;|\()(?!L)(?:\w+\/)+", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _field = new(@"(?<=\.)<{0,1}\w+>{0,1}(?!\()", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _methodr = new(@"(?<=\.)<{0,1}\w+>{0,1}(?=\()", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _primitives = new(@"(?<=[ ;()ZBCSIJFD[])(?<!L)[ZBCSIJFDV](?=[ZBCSIJFDL[)]|(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _classBegin = new(@"(?<=[ ;()ZBCSIJFD[])L(?=[\w\/]+)", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _classEnd = new(@"(?<=[\w\/]+);(?=(?:\r?$)|[ L)ZBCSIJFD[])", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _firstWord = new(@"^(?:\w)+(?= |(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _handleOpcodes = new(@"(?<=\[)H_\w+(?= )", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _frame = new(@"^FRAME\r?$", RegexOptions.Multiline | RegexOptions.Compiled);
        public DisassemblerForm()
        {
            InitializeComponent();
            _highlighter = new(tbDisassembly);
        }
        
        public void DisassemblerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        
        public void OpenMethod(string method)
        {
            tbMethod.Text = method;
            AsmUtilities.Classes.Clear();
            JavaBlastTools.LoadClassesFromCurrentJar();
            MethodNode m = AsmUtilities.FindMethod(method);
            if (m == null)
                return;
            AsmParser parser = new();
            parser.RegisterLabelsFrom(m.Instructions);
            AbstractInsnNode[] insns = m.Instructions.ToArray();
            string text = "";
            for (int i = 0; i < insns.Length; i++)
            {
                if (insns[i] is null)
                    continue;
                string stringed = parser.InsnToString(insns[i]);
                if (stringed == "")
                    stringed = "FRAME";
                text += stringed + "\r\n";
            }
            string labels = "(?<=^| )(?:";
            foreach (KeyValuePair<LabelNode, string> pair in parser.LabelNames)
                labels += "(?:" + pair.Value + ")|";
            labels = labels.Remove(labels.Length - 1) + ")(?=(?::(?:\\r?$))| |(?:\\r?$))";
            label = new(labels, RegexOptions.Multiline);
            tbDisassembly.Text = "";
            _highlighter.Reset();
            _highlighter.AddPattern(new(label), new(Color.FromArgb(0x9C, 0xDC, 0xFE)));
            _highlighter.AddPattern(new(_frame), new(Color.FromArgb(0xA9, 0xA9, 0xA9), false, true));
            _highlighter.AddPattern(new(_handleOpcodes), new(Color.FromArgb(0x56, 0x9C, 0xD6)));
            _highlighter.AddPattern(new(_firstWord), new(Color.FromArgb(0x56, 0x9C, 0xD6)));
            _highlighter.AddPattern(new(_primitives), new(Color.FromArgb(0x56, 0x9C, 0xD6)));
            _highlighter.AddPattern(new(_classEnd), new(Color.FromArgb(0xA9, 0xA9, 0xA9)));
            _highlighter.AddPattern(new(_classBegin), new(Color.FromArgb(0xA9, 0xA9, 0xA9)));
            _highlighter.AddPattern(new(_numbers), new(Color.FromArgb(0xB5, 0xCE, 0xA8)));
            _highlighter.AddPattern(new(_className), new(Color.FromArgb(0x4E, 0xC9, 0xB0)));
            _highlighter.AddPattern(new(_methodr), new(Color.FromArgb(0xDC, 0xDC, 0xAA)));
            _highlighter.AddPattern(new(_field), new(Color.FromArgb(0x9C, 0xDC, 0xFE)));
            _highlighter.AddPattern(new(_stringRegex), new(Color.FromArgb(0xCE, 0x91, 0x78)));
            _highlighter.AddPattern(new(_package), new(Color.FromArgb(0xFF, 0xFF, 0xFF)));
            
            tbDisassembly.Text = text;
        }

        private Regex label = new(@"^[A-Z]+:(?:\r?$)", RegexOptions.Multiline);
        
        private int _lastLineCount;
        private void tbDisassembly_TextChanged(object sender, EventArgs e)
        {
            if (tbDisassembly.Lines.Length == _lastLineCount)
                return;
            _lastLineCount = tbDisassembly.Lines.Length;
            tbLineNumbers.Text = "";
            tbLineNumbers.Font = tbDisassembly.Font;
            tbLineNumbers.Width =
                TextRenderer.MeasureText(_lastLineCount.ToString(), tbLineNumbers.Font).Width -
                (TextRenderer.MeasureText("1", tbLineNumbers.Font).Width / 2);
            //disable scrollbar on line numbers
            tbLineNumbers.ScrollBars = RichTextBoxScrollBars.None;
            tbDisassembly.Location = tbDisassembly.Location with { X = tbLineNumbers.Location.X + tbLineNumbers.Width };
            tbDisassembly.Width = Width - tbLineNumbers.Width - 43;
            for (int i = 0; i <= _lastLineCount; i++)
                tbLineNumbers.Text += i + "\r\n";
        }

        private void btnDisassemble_Click(object sender, EventArgs e)
        {
            OpenMethod(tbMethod.Text);
        }

        private void tbDisassembly_VScroll(object sender, EventArgs e)
        {
            tbLineNumbers.SetVScrollPos(tbDisassembly.GetVScrollPos());
        }
    }
}