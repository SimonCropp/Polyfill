#if NETFRAMEWORK || NETSTANDARD2_0

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool Contains(this string target, string value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    public static bool StartsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        return target[0] == value;
    }

    public static bool EndsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        var lastPos = target.Length - 1;
        return lastPos < target.Length &&
               target[lastPos] == value;
    }
}
#endif