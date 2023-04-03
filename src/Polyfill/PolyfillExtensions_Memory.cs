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
    //TODO: move to generic
    /// <summary>
    /// Indicates whether a specified value is found in a read-only span. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>true if found, false otherwise.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0)")]
    public static bool Contains(this ReadOnlySpan<char> target, char value)
    {
        foreach (var ch in target)
        {
            if (ch == value)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Appends the string representation of a specified read-only character span to this instance.
    /// </summary>
    /// <param name="value">The read-only character span to append.</param>
    /// <returns>A reference to this instance after the append operation is completed.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-readonlyspan((system-char)))")]
    public static StringBuilder Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.Length <= 0)
        {
            return target;
        }

#if AllowUnsafeBlocks
        unsafe
        {
            fixed (char* valueChars = &MemoryMarshal.GetReference(value))
            {
                target.Append(valueChars, value.Length);
            }
        }
#else
        target.Append(value.ToArray());
#endif
        return target;
    }

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-7.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")]
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

    /// <summary>
    /// Returns a value indicating whether the characters in this instance are equal to the characters in a specified
    /// read-only character span.
    /// </summary>
    /// <param name="span">The character span to compare with the current instance.</param>
    /// <remarks>
    /// The Equals method performs an ordinal comparison to determine whether the characters in the current instance
    /// and span are equal.
    /// </remarks>
    /// <returns>true if the characters in this instance and span are the same; otherwise, false.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char)))")]
    public static bool Equals(this StringBuilder target, ReadOnlySpan<char> span)
    {
        if (target.Length != span.Length)
        {
            return false;
        }

        for (var index = 0; index < target.Length; index++)
        {
            var ch1 = target[index];
            var ch2 = span[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }
}
#endif