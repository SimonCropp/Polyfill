#if MEMORYREFERENCED

// ReSharper disable RedundantUsingDirective
using System;
using System.Text;

namespace Polyfill;

static partial class PolyExtensions
{
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

    public static void Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.Length <= 0)
        {
            return;
        }

#if AllowUnsafeBlocks
        unsafe
        {
            fixed (char* valueChars = &System.Runtime.InteropServices.MemoryMarshal.GetReference(value))
            {
                target.Append(valueChars, value.Length);
            }
        }
#else
        target.Append(value.ToArray());
#endif
    }

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