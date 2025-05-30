// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static class SBytePolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@)
    public static bool TryParse(string? target, IFormatProvider? provider, out sbyte result) =>
#if NET7_0_OR_GREATER
        sbyte.TryParse(target, provider, out result);
#else
        sbyte.TryParse(target, NumberStyles.Integer, provider, out result);
#endif

#if FeatureMemory
    /// <summary>
    /// Tries to parse a span of UTF-8 characters into a value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<byte> target, IFormatProvider? provider, out sbyte result) =>
#if NET8_0_OR_GREATER
        sbyte.TryParse(target, provider, out result);
#else
        sbyte.TryParse(Encoding.UTF8.GetString(target), NumberStyles.Integer, provider, out result);
#endif

    /// <summary>
    /// Converts the span representation of a number in a specified style and culture-specific format to its sbyte equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<char> target, out sbyte result) =>
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        sbyte.TryParse(target, out result);
#else
        sbyte.TryParse(target.ToString(), out result);
#endif

    /// <summary>
    /// Tries to parse a span of characters into a value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<char> target, IFormatProvider? provider, out sbyte result) =>
#if NET7_0_OR_GREATER
        sbyte.TryParse(target, provider, out result);
#else
        sbyte.TryParse(target.ToString(), NumberStyles.Integer, provider, out result);
#endif

    /// <summary>
    /// Tries to parse a span of UTF-8 characters into a value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<byte> target, NumberStyles style, IFormatProvider? provider, out sbyte result) =>
#if NET8_0_OR_GREATER
        sbyte.TryParse(target, style, provider, out result);
#else
        sbyte.TryParse(Encoding.UTF8.GetString(target), style, provider, out result);
#endif

    /// <summary>
    /// Tries to convert a UTF-8 character span containing the string representation of a number to its sbyte equivalent.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<byte> target, out sbyte result) =>
#if NET8_0_OR_GREATER
        sbyte.TryParse(target, out result);
#else
        sbyte.TryParse(Encoding.UTF8.GetString(target), NumberStyles.Integer, null, out result);
#endif

    /// <summary>
    /// Converts the span representation of a number in a specified style and culture-specific format to its sbyte equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)
    public static bool TryParse(ReadOnlySpan<char> target, NumberStyles style, IFormatProvider? provider, out sbyte result) =>
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        sbyte.TryParse(target, style, provider, out result);
#else
        sbyte.TryParse(target.ToString(), style, provider, out result);
#endif
#endif
}