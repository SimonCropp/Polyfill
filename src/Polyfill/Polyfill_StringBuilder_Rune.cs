#if NETCOREAPP3_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Returns an enumeration of <see cref="Rune"/>s over the <see cref="StringBuilder"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.enumeraterunes?view=net-11.0
    //Note: Enumerates over a snapshot of the content taken when enumeration starts, so changes made to the StringBuilder while enumerating are not observed.
    public static StringBuilderRuneEnumerator EnumerateRunes(this StringBuilder target) =>
        new(target);

    /// <summary>
    /// Gets the <see cref="Rune"/> that begins at the specified character index.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getruneat?view=net-11.0
    public static Rune GetRuneAt(this StringBuilder target, int index)
    {
        if (!TryReadRuneAt(target, index, out var value, out var wellFormed))
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "Index was out of range. Must be non-negative and less than the length of the StringBuilder.");
        }

        if (!wellFormed)
        {
            throw new ArgumentException("Cannot extract a Unicode scalar value from the specified index in the input.", nameof(index));
        }

        return value;
    }

    /// <summary>
    /// Gets the <see cref="Rune"/> that begins at the specified character index, and returns a value that indicates whether the operation succeeded.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.trygetruneat?view=net-11.0
    public static bool TryGetRuneAt(this StringBuilder target, int index, out Rune value)
    {
        if (!TryReadRuneAt(target, index, out value, out var wellFormed))
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "Index was out of range. Must be non-negative and less than the length of the StringBuilder.");
        }

        return wellFormed;
    }

    /// <summary>
    /// Replaces all occurrences of a specified <see cref="Rune"/> in this instance with another specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-text-rune-system-text-rune)
    public static StringBuilder Replace(this StringBuilder target, Rune oldRune, Rune newRune) =>
        target.Replace(oldRune.ToString(), newRune.ToString());

    /// <summary>
    /// Replaces, within a substring of this instance, all occurrences of a specified <see cref="Rune"/> with another specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-text-rune-system-text-rune-system-int32-system-int32)
    public static StringBuilder Replace(this StringBuilder target, Rune oldRune, Rune newRune, int startIndex, int count) =>
        target.Replace(oldRune.ToString(), newRune.ToString(), startIndex, count);

    /// <summary>
    /// Reads the scalar beginning at <paramref name="index"/>. Returns false when the index is out of range,
    /// otherwise sets <paramref name="wellFormed"/> to indicate whether a scalar could be read.
    /// </summary>
    static bool TryReadRuneAt(StringBuilder target, int index, out Rune value, out bool wellFormed)
    {
        value = default;
        wellFormed = false;

        if (index < 0 ||
            index >= target.Length)
        {
            return false;
        }

        var first = target[index];
        if (!char.IsSurrogate(first))
        {
            value = new(first);
            wellFormed = true;
            return true;
        }

        if (char.IsHighSurrogate(first) &&
            index + 1 < target.Length &&
            char.IsLowSurrogate(target[index + 1]))
        {
            value = new(char.ConvertToUtf32(first, target[index + 1]));
            wellFormed = true;
        }

        return true;
    }
}

#endif
