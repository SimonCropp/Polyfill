namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER

    /// <summary>
    /// Appends a copy of a substring within a specified string builder to this instance.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32)
    public static StringBuilder Append(this StringBuilder target, StringBuilder? value, int startIndex, int count)
    {
        if (value == null)
        {
            if (startIndex == 0 && count == 0)
            {
                return target;
            }

            throw new ArgumentNullException(nameof(value));
        }

        if (count == 0)
        {
            return target;
        }

        return target.Append(value.ToString(), startIndex, count);
    }

#endif

#if FeatureMemory && !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER

    /// <summary>
    /// Appends the string representation of a specified read-only character span to this instance.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-readonlyspan((system-char)))
    public static StringBuilder Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.Length <= 0)
        {
            return target;
        }

#if AllowUnsafeBlocks
        unsafe
        {
            fixed (char* valueChars = value)
            {
                target.Append(valueChars, value.Length);
            }
        }
#else
        target.Append(value.ToString());
#endif
        return target;
    }

#endif
}
