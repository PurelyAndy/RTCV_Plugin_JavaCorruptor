using System;
using System.Text;

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
        int pos = text.IndexOf(findText, StringComparison.Ordinal);
        while (pos >= 0)
        {
            string replacement = generateReplacement();
            newText.Append(text[..pos] + replacement);
            text = text[(pos + findText.Length)..];
            pos = text.IndexOf(findText, StringComparison.Ordinal);
        }
        return newText + text;
    }
}