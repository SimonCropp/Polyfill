
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Text;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto")]
    public static void CopyTo(this string target, Span<char> destination) =>
        target.AsSpan().CopyTo(destination);

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    /// <returns>true if the data was copied; false if the destination was too short to fit the contents of the string.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto")]
    public static bool TryCopyTo(this string target, Span<char> destination) =>
        target.AsSpan().TryCopyTo(destination);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <param name="value">The character to seek.</param>
    /// <returns>true if the value parameter occurs within this string; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")]
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;
#endif
}