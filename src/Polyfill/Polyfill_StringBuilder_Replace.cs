#if !NET9_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Replaces all instances of one string with another in part of this builder.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char)))
    public static StringBuilder Replace(this StringBuilder target, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue) =>
        target.Replace(oldValue.ToString(), newValue.ToString());

    /// <summary>
    /// Replaces all instances of one read-only character span with another in part of this builder.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32)
    public static StringBuilder Replace(this StringBuilder target, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue, int startIndex, int count) =>
        target.Replace(oldValue.ToString(), newValue.ToString(), startIndex, count);
}
#endif
