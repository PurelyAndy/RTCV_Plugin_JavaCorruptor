using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ObjectWeb.Asm.Tree;
using RTCV.UI;
using RTCV.UI.Modular;

namespace Java_Corruptor;

public static class Extensions
{
    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in a StringBuilder are replaced with a string returned by a function.
    /// </summary>
    /// <param name="text">The string to search in.</param>
    /// <param name="findText">The string to be replaced.</param>
    /// <param name="generateReplacement">A function that returns a replacement string.</param>
    public static string Replace(this string text, string findText, Func<string> generateReplacement)
    {
        StringBuilder newText = new();
        int position = text.IndexOf(findText, StringComparison.Ordinal);
        while (position >= 0)
        {
            string replacement = generateReplacement();
            newText.Append(text[..position] + replacement);
            text = text[(position + findText.Length)..];
            position = text.IndexOf(findText, StringComparison.Ordinal);
        }
        return newText + text;
    }

    public static void Deconstruct(this KeyValuePair<string, (ClassNode, byte[], string)> kvp, out string a, out (ClassNode, byte[], string) b)
    {
        (a, b) = (kvp.Key, kvp.Value);
    }
    
    public static void HandleMouseDownP(this ComponentForm form, object s, MouseEventArgs e) => typeof(ComponentForm).GetMethod("HandleMouseDown", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(form, [s, e]);
    public static void HandleFormClosingP(this ComponentForm form, object s, FormClosingEventArgs e) => typeof(ComponentForm).GetMethod("HandleFormClosing", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(form, [s, e]);
    public static void LoadToMainP(this CanvasGrid cg) => typeof(CanvasGrid).GetMethod("LoadToMain", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(cg, null);
}