#if !NET11_0_OR_GREATER && NETCOREAPP3_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Appends the string representation of a specified <see cref="Rune"/> to this instance.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0
    public static StringBuilder Append(this StringBuilder target, Rune value)
    {
        Span<char> chars = stackalloc char[2];
        var count = value.EncodeToUtf16(chars);
        return target.Append(chars.Slice(0, count));
    }

    /// <summary>
    /// Inserts the string representation of a specified <see cref="Rune"/> into this instance at the specified character position.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-10.0
    public static StringBuilder Insert(this StringBuilder target, int index, Rune value) =>
        target.Insert(index, value.ToString());

    /// <summary>
    /// Replaces all occurrences of a specified <see cref="Rune"/> in this instance with another specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0
    public static StringBuilder Replace(this StringBuilder target, Rune oldValue, Rune newValue) =>
        target.Replace(oldValue.ToString(), newValue.ToString());
}

#endif
