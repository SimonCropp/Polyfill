#if FeatureMemory

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Returns an enumeration of lines over the provided span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-11.0#system-memoryextensions-enumeratelines(system-readonlyspan((system-char)))
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> target) => new(target);

    /// <summary>
    /// Returns an enumeration of lines over the provided span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-11.0#system-memoryextensions-enumeratelines(system-span((system-char)))
    public static SpanLineEnumerator EnumerateLines(this Span<char> target) => new(target);

#endif

#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Removes all leading white-space characters from the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart?view=net-11.0#system-memoryextensions-trimstart(system-span((system-char)))
    public static Span<char> TrimStart(this Span<char> target) => target.Slice(ClampStart(target));

    /// <summary>
    /// Removes all trailing white-space characters from the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend?view=net-11.0#system-memoryextensions-trimend(system-span((system-char)))
    public static Span<char> TrimEnd(this Span<char> target) => target.Slice(0, ClampEnd(target, 0));

    static int ClampStart(ReadOnlySpan<char> target)
    {
        var start = 0;

        for (; start < target.Length; start++)
        {
            if (!char.IsWhiteSpace(target[start]))
            {
                break;
            }
        }

        return start;
    }

    static int ClampEnd(ReadOnlySpan<char> target, int start)
    {
        var end = target.Length - 1;

        for (; end >= start; end--)
        {
            if (!char.IsWhiteSpace(target[end]))
            {
                break;
            }
        }

        return end - start + 1;
    }

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using IEquatable{T}.Equals(T).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-11.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static bool SequenceEqual(
        this ReadOnlySpan<char> target,
        string other) =>
        target.SequenceEqual(other.AsSpan());

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using IEquatable{T}.Equals(T).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-11.0#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0)))
    public static bool SequenceEqual(
        this Span<char> target,
        string other) =>
        target.SequenceEqual(other.AsSpan());

#endif

#if !NET11_0_OR_GREATER

    /// <summary>
    /// Copies the source span to the destination span, converting it to lowercase using ordinal casing rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.tolowerordinal?view=net-11.0
    //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
    public static int ToLowerOrdinal(this ReadOnlySpan<char> source, Span<char> destination) =>
        ToOrdinalCase(source, destination, toUpper: false);

    /// <summary>
    /// Copies the source span to the destination span, converting it to uppercase using ordinal casing rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.toupperordinal?view=net-11.0
    //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
    public static int ToUpperOrdinal(this ReadOnlySpan<char> source, Span<char> destination) =>
        ToOrdinalCase(source, destination, toUpper: true);

    static int ToOrdinalCase(ReadOnlySpan<char> source, Span<char> destination, bool toUpper)
    {
        if (source.Overlaps(destination))
        {
            throw new InvalidOperationException("The source and destination buffers overlap.");
        }

        if (destination.Length < source.Length)
        {
            return -1;
        }

        for (var index = 0; index < source.Length; index++)
        {
            var current = source[index];
            if (char.IsHighSurrogate(current) &&
                index + 1 < source.Length &&
                char.IsLowSurrogate(source[index + 1]))
            {
                var cased = ToOrdinalCaseSurrogatePair(source.Slice(index, 2).ToString(), toUpper);
                destination[index] = cased[0];
                destination[index + 1] = cased[1];
                index++;
                continue;
            }

            destination[index] = toUpper ? ToUpperOrdinalChar(current) : ToLowerOrdinalChar(current);
        }

        return source.Length;
    }

#endif

}

#endif
