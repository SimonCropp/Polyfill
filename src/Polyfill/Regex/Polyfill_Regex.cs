namespace Polyfills;

using System;
using System.Text.RegularExpressions;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER && FeatureMemory
    /// <summary>
    /// Indicates whether the regular expression specified in the Regex constructor finds a match in a specified input span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32)
    public static bool IsMatch(this Regex target, ReadOnlySpan<char> input, int startat) =>
        target.IsMatch(input.ToString(), startat);

    /// <summary>
    /// Indicates whether the regular expression specified in the Regex constructor finds a match in a specified input span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char)))
    public static bool IsMatch(this Regex target, ReadOnlySpan<char> input) =>
        target.IsMatch(input.ToString());

    /// <summary>
    /// Searches an input span for all occurrences of a regular expression and returns a Regex.ValueMatchEnumerator to iterate over the matches.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char)))
    public static ValueMatchEnumerator EnumerateMatches(this Regex target, ReadOnlySpan<char> input) =>
        new(target, input, target.RightToLeft ? input.Length : 0);

    /// <summary>
    /// Searches an input span for all occurrences of a regular expression and returns a Regex.ValueMatchEnumerator to iterate over the matches.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32)
    public static ValueMatchEnumerator EnumerateMatches(this Regex target, ReadOnlySpan<char> input, int startat) =>
        new(target, input, startat);
#endif

#if !NET9_0_OR_GREATER && FeatureMemory && FeatureValueTuple

     //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char)))
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input) =>
        new(input, regex, 0, 0);

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-int32)
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");
        }

        return new ValueSplitEnumerator(input, regex, count, 0);
    }

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-int32-system-int32)
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input, int count, int startat)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");
        }

        if (startat < 0 || startat > input.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startat), "Start position must be within the input span.");
        }

        return new ValueSplitEnumerator(input, regex, count, startat);
    }

#endif
}
