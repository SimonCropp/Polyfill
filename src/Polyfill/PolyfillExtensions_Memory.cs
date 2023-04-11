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
            var item = target[index];
            if (item.Equals(value))
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
            var item = target[index];
            if (item.Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")]
    public static bool SequenceEqual(this ReadOnlySpan<char> target, string other)
    {
        if (target.Length != other.Length)
        {
            return false;
        }

        for (var index = 0; index < target.Length; index++)
        {
            var ch1 = target[index];
            var ch2 = other[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0)))")]
    public static bool SequenceEqual(this Span<char> target, string other)
    {
        if (target.Length != other.Length)
        {
            return false;
        }

        for (var index = 0; index < target.Length; index++)
        {
            var ch1 = target[index];
            var ch2 = other[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }
}
#endif