#if MEMORYREFERENCED && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix

using System;
using System.Text;
using System.Runtime.InteropServices;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Indicates whether a specified value is found in a read-only span. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>true if found, false otherwise.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0)")]
    public static bool Contains<T>(this ReadOnlySpan<T> target, T value)
        where T : IEquatable<T>
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index].Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Indicates whether a specified value is found in a only span. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>true if found, false otherwise.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-span((-0))-0)")]
    public static bool Contains<T>(this Span<T> target, T value)
        where T : IEquatable<T>
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index].Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")]
    public static bool SequenceEqual(this ReadOnlySpan<char> target, string other) =>
        target.SequenceEqual(other.AsSpan());

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0)))")]
    public static bool SequenceEqual(this Span<char> target, string other) =>
        target.SequenceEqual(other.AsSpan());

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")]
    public static bool StartsWith(this ReadOnlySpan<char> target, string other) =>
        target.StartsWith(other.AsSpan());

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0)))")]
    public static bool StartsWith(this Span<char> target, string other) =>
        target.StartsWith(other.AsSpan());
}
#endif