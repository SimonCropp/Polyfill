#pragma warning disable

#if FeatureMemory && !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    extension(StringBuilder target)
    {
        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendformat?view=net-11.0#system-text-stringbuilder-appendformat(system-string-system-readonlyspan((system-object)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL formats straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the BCL array overload, exactly as it does without Polyfill.
        public StringBuilder AppendFormat(string format, params ReadOnlySpan<object?> args) =>
            target.AppendFormat(format, args.ToArray());

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendformat?view=net-11.0#system-text-stringbuilder-appendformat(system-iformatprovider-system-string-system-readonlyspan((system-object)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL formats straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the BCL array overload, exactly as it does without Polyfill.
        public StringBuilder AppendFormat(IFormatProvider? provider, string format, params ReadOnlySpan<object?> args) =>
            target.AppendFormat(provider, format, args.ToArray());

        /// <summary>
        /// Concatenates the string representations of the elements in the provided span, using the specified char separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-readonlyspan((system-object)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL joins straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the array overload, exactly as it does without Polyfill.
        public StringBuilder AppendJoin(char separator, params ReadOnlySpan<object?> values) =>
            target.AppendJoin(separator, values.ToArray());

        /// <summary>
        /// Concatenates the strings of the provided span, using the specified char separator between each string.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-readonlyspan((system-string)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL joins straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the array overload, exactly as it does without Polyfill.
        public StringBuilder AppendJoin(char separator, params ReadOnlySpan<string?> values) =>
            target.AppendJoin(separator, values.ToArray());

        /// <summary>
        /// Concatenates the string representations of the elements in the provided span, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-readonlyspan((system-object)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL joins straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the array overload, exactly as it does without Polyfill.
        public StringBuilder AppendJoin(string? separator, params ReadOnlySpan<object?> values) =>
            target.AppendJoin(separator, values.ToArray());

        /// <summary>
        /// Concatenates the strings of the provided span, using the specified separator between each string.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-readonlyspan((system-string)))
        //Note: Copies the span to an array and uses the array overload, so this allocates where the BCL joins straight from the span.
        //Note: Only reached when the argument is already a ReadOnlySpan. A loose argument list binds to the array overload, exactly as it does without Polyfill.
        public StringBuilder AppendJoin(string? separator, params ReadOnlySpan<string?> values) =>
            target.AppendJoin(separator, values.ToArray());
    }
}

#endif
