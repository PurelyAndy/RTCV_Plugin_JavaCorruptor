using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using SlimDX.DXGI;
using WinFormsSyntaxHighlighter;

namespace Java_Corruptor.UI;

public partial class DisassemblerForm : Form, IColorize
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly SyntaxHighlighter _highlighter;
    private readonly Regex _stringRegex = new("\"[^\"]+\"", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _className = new(@"(?<=\/){0,1}(?<!;|\()(?!L|\d)\w+(?=\.|;|\$|(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _numbers = new(@"(?<=\$| )-?(?:\d+\.)?\d+[ILFD]?", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _package = new(@"(?<!;|\()(?!L)(?:\w+\/)+", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _field = new(@"(?<=\.)<{0,1}\w+>{0,1}(?!\()", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _methodR = new(@"(?<=\.)<{0,1}\w+>{0,1}(?=\()", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _primitives = new(@"(?<=[ ;()ZBCSIJFD[])(?<!L)[ZBCSIJFDV](?=[ZBCSIJFDL[)]|(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _classBegin = new(@"(?<=[ ;()ZBCSIJFD[])L(?=[\w\/]+)", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _classEnd = new(@"(?<=[\w\/]+);(?=(?:\r?$)|[ L)ZBCSIJFD[])", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _firstWord = new(@"^(?:\w)+(?= |(?:\r?$))", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _handleOpcodes = new(@"(?<=\[)H_\w+(?= )", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _frame = new(@"^FRAME\r?$", RegexOptions.Multiline | RegexOptions.Compiled);
    
    public DisassemblerForm()
    {
        InitializeComponent();
        tbDisassembly.WordWrap = false;
        _highlighter = new(tbDisassembly);
        tbDisassembly.AutoWordSelection = true;
        tbDisassembly.AutoWordSelection = false;
        RTCV.Common.S.RegisterColorizable(this);
        Load += Colorize;
        FormClosed += DeregisterColorizable;
    }

    private void DeregisterColorizable(object sender, FormClosedEventArgs e)
    {
        RTCV.Common.S.DeregisterColorizable(this);
    }

    public void DisassemblerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    class Commands
    {
    }

    public void OpenMethod(string method, int lineNumber, int selectedLines)
    {
        tbMethod.Text = method;
        JavaBlastTools.ReloadClasses();
        int indexOf = method.LastIndexOf('.');
        if (indexOf < 0)
            indexOf = method.IndexOf('<') - 1;
        ClassNode classNode = AsmUtilities.FindClass(method[..indexOf]);
        MethodNode m = classNode.FindMethod(method[(indexOf + 1)..]);
        if (m == null)
        {
            MessageBox.Show($"Method {method} not found.", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Logger.Error($"Method {method} was meant to be disassembled, but it could not be found.");
            return;
        }

        if (m.Instructions.Size > 1500)
        {
            if (DialogResult.No == MessageBox.Show($"This method is very large ({m.Instructions.Size} instructions) and may take a while to disassemble. Are you sure you want to continue?", "Large Method", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;
        }
        Stopwatch sw = Stopwatch.StartNew();
        AsmParser parser = new();
        parser.RegisterLabelsFrom(m.Instructions);
        sw.Stop();
        Logger.Info($"Registered labels for {method} in {sw.ElapsedMilliseconds}ms.");
        sw.Restart();
        AbstractInsnNode[] insns = m.Instructions.ToArray();
        if (insns.Length == 0)
        {
            tbDisassembly.Text = $"Method {method} has no instructions";
            if ((classNode.Access & Opcodes.Acc_Interface) != 0)
                tbDisassembly.Text += " because its class is an interface.";
            else if ((m.Access & Opcodes.Acc_Abstract) != 0)
                tbDisassembly.Text += " because it is abstract.";
            else
                tbDisassembly.Text += " because it is empty.";
            return;
        }
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
        text = text.Remove(text.Length - 2); // remove the last line break
        sw.Stop();
        Logger.Info($"Disassembled {method} in {sw.ElapsedMilliseconds}ms.");
        sw.Restart();
        
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
        _highlighter.AddPattern(new(_methodR), new(Color.FromArgb(0xDC, 0xDC, 0xAA)));
        _highlighter.AddPattern(new(_field), new(Color.FromArgb(0x9C, 0xDC, 0xFE)));
        _highlighter.AddPattern(new(_stringRegex), new(Color.FromArgb(0xCE, 0x91, 0x78)));
        _highlighter.AddPattern(new(_package), new(Color.FromArgb(0xFF, 0xFF, 0xFF)));
            
        tbDisassembly.Text = text;
        sw.Stop();
        Logger.Info($"Highlighted {method} in {sw.ElapsedMilliseconds}ms.");
        // stupid hack because ScrollToCaret wasn't doing anything
        Task.Run(() =>
        {
            Task.Delay(5).Wait();
            tbDisassembly.Invoke(new Action(() =>
            {
                if (lineNumber + selectedLines > tbDisassembly.Lines.Length)
                    return;
                int firstLineIndex = tbDisassembly.GetFirstCharIndexFromLine(lineNumber);
                int lastLineIndex = tbDisassembly.GetFirstCharIndexFromLine(lineNumber + selectedLines);
                tbDisassembly.Focus();
                tbDisassembly.Select(firstLineIndex, lastLineIndex - firstLineIndex);
                tbDisassembly.ScrollToCaret();
                tbDisassembly.SetVScrollPos(tbDisassembly.GetVScrollPos() - 150);
            }));
        });
    }

    private Regex label = new(@"^[A-Z]+:(?:\r?$)", RegexOptions.Multiline);
        
    private int _lastLineCount;
    private void tbDisassembly_TextChanged(object sender, EventArgs e)
    {
        if (tbDisassembly.Lines.Length == _lastLineCount)
            return;
        _lastLineCount = tbDisassembly.Lines.Length;
        tbLineNumbers.Text = "";
        int baseWidth = TextRenderer.MeasureText(_lastLineCount.ToString(), tbLineNumbers.Font).Width;
        int unPadding = TextRenderer.MeasureText("1", tbLineNumbers.Font).Width / 2;
        tbLineNumbers.Width = baseWidth - unPadding;
        tbDisassembly.Location = tbDisassembly.Location with { X = tbLineNumbers.Location.X + tbLineNumbers.Width };
        tbDisassembly.Width = Width - tbLineNumbers.Width - 43;
        for (int i = 0; i < _lastLineCount; i++)
            tbLineNumbers.Text += i + "\r\n";
    }

    private void btnDisassemble_Click(object sender, EventArgs e)
    {
        OpenMethod(tbMethod.Text, 0, 0);
    }


    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetScrollInfo(int hWnd, int nBar, ref ScrollInfo lpsi);

    [StructLayout(LayoutKind.Sequential)]
    private struct ScrollInfo
    {
        public uint cbSize;
        public uint fMask;
        public int nMin;
        public int nMax;
        public uint nPage;
        public int nPos;
        public int nTrackPos;
    };
    private void tbDisassembly_VScroll(object sender, EventArgs e)
    {
        ScrollInfo si = new();
        si.cbSize = (uint)Marshal.SizeOf(si);
        si.fMask = 0x10;
        GetScrollInfo((int)tbDisassembly.Handle, 1, ref si);
        tbLineNumbers.SetVScrollPos(si.nTrackPos);
    }

    private void Colorize(object sender, EventArgs e) => Recolor();
    public void Recolor()
    {
        Colors.SetRTCColor(Colors.GeneralColor.ChangeColorBrightness(-0.3f), this);
    }
}