#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static partial class GuidPolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-string-system-iformatprovider-system-guid@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out Guid result) =>
#if !NET7_0_OR_GREATER
        Guid.TryParse(target, out result);
#else
        Guid.TryParse(target, provider, out result);
#endif

#if FeatureMemory
    /// <summary>
    /// Tries to parse a span of UTF-8 characters into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@)")]
    public static bool TryParse(ReadOnlySpan<byte> target, IFormatProvider? provider, out byte result) =>
#if !NET8_0_OR_GREATER
        byte.TryParse(Encoding.UTF8.GetString(target.ToArray()), NumberStyles.Integer, provider, out result);
#else
        byte.TryParse(target, provider, out result);
#endif

#endif
}