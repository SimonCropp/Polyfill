#if MEMORYREFERENCED && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat")]
    public static bool Contains<T>(this int target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(provider);
        }
        else
        {
            result = target.ToString(format.ToString(),provider);
            
        }

        if (result <= destination.Length)
        {
            result.cop
        }
    }
}
#endif