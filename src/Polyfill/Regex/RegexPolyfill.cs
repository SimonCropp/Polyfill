namespace Polyfills;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

#if FeatureMemory
#if NET7_0_OR_GREATER
using ValueMatchEnumerator = System.Text.RegularExpressions.Regex.ValueMatchEnumerator;
#else
using ValueMatchEnumerator = System.Text.RegularExpressions.ValueMatchEnumerator;
#endif
#endif

static partial class Polyfill
{
    extension(Regex)
    {
#if FeatureMemory
#if !NET7_0_OR_GREATER
        /// <summary>
        /// Indicates whether the specified regular expression finds a match in the specified input span, using the specified matching options and time-out interval.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan)
        public static bool IsMatch(ReadOnlySpan<char> input, string pattern, RegexOptions options, TimeSpan timeout) =>
            Regex.IsMatch(input.ToString(), pattern, options, timeout);

        /// <summary>
        /// Indicates whether the specified regular expression finds a match in the specified input span, using the specified matching options.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions)
        public static bool IsMatch(ReadOnlySpan<char> input, string pattern, RegexOptions options) =>
            Regex.IsMatch(input.ToString(), pattern, options);

        /// <summary>
        /// Indicates whether the specified regular expression finds a match in the specified input span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string)
        public static bool IsMatch(ReadOnlySpan<char> input, string pattern) =>
            Regex.IsMatch(input.ToString(), pattern);

        /// <summary>
        /// Searches an input span for all occurrences of a regular expression and returns a Regex.ValueMatchEnumerator to iterate over the matches.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string)
        public static ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char> input, string pattern) =>
            RegexCache.GetOrAdd(pattern).EnumerateMatches(input);

        /// <summary>
        /// Searches an input span for all occurrences of a regular expression and returns a Regex.ValueMatchEnumerator to iterate over the matches.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan)
        public static ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char> input, string pattern, RegexOptions options, TimeSpan timeout) =>
            RegexCache.GetOrAdd(pattern, options, timeout).EnumerateMatches(input);

        /// <summary>
        /// Searches an input span for all occurrences of a regular expression and returns a Regex.ValueMatchEnumerator to iterate over the matches.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions)
        public static ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char> input, string pattern, RegexOptions options) =>
            RegexCache.GetOrAdd(pattern, options, TimeSpan.MaxValue).EnumerateMatches(input);
#endif
#endif

#if !NET9_0_OR_GREATER && FeatureMemory && FeatureValueTuple

        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string)
        public static ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char> input, string pattern)
        {
            var regex = new Regex(pattern);
            return new ValueSplitEnumerator(input, regex, 0, 0);
        }

        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions)
        public static ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char> input, string pattern, RegexOptions options)
        {
            var regex = new Regex(pattern, options);
            return new ValueSplitEnumerator(input, regex, 0, 0);
        }

        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan)
        public static ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char> input, string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            var regex = new Regex(pattern, options, matchTimeout);
            return new ValueSplitEnumerator(input, regex, 0, 0);
        }

#endif
    }
}
