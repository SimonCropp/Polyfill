// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
using System;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Polyfill;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
public static partial class PolyExtensions
{
#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NETSTANDARD2_0

    public static bool StartsWith(this string value, char ch)
    {
        if (value.Length == 0)
        {
            return false;
        }

        return value[0] == ch;
    }

    public static bool EndsWith(this string value, char ch)
    {
        if (value.Length == 0)
        {
            return false;
        }

        var lastPos = value.Length - 1;
        return lastPos < value.Length &&
               value[lastPos] == ch;
    }
#endif
}