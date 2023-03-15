// ReSharper disable RedundantUsingDirective
using System;
using System.Text;

namespace Polyfill;

public static class PolyExtensions
{
#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NETSTANDARD2_0

    public static bool Contains(this ReadOnlySpan<char> span, char value)
    {
        foreach (var ch in span)
        {
            if (ch == value)
            {
                return true;
            }
        }

        return false;
    }

    public static void Append(this StringBuilder builder, ReadOnlySpan<char> value)
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
                builder.Append(valueChars, value.Length);
            }
        }
#else
        builder.Append(value.ToArray());
#endif
    }

    public static bool SequenceEqual(this ReadOnlySpan<char> value1, string value2)
    {
        if (value1.Length != value2.Length)
        {
            return false;
        }

        for (var index = 0; index < value1.Length; index++)
        {
            var ch1 = value1[index];
            var ch2 = value2[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }

    public static bool SequenceEqual(this Span<char> value1, string value2)
    {
        if (value1.Length != value2.Length)
        {
            return false;
        }

        for (var index = 0; index < value1.Length; index++)
        {
            var ch1 = value1[index];
            var ch2 = value2[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }

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