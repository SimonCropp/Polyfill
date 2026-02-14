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

}

#endif
