
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Text;

static partial class Polyfill
{


    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <param name="value">The character to seek.</param>
    /// <returns>true if the value parameter occurs within this string; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")]
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;
}