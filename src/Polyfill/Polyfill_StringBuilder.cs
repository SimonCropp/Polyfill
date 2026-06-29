#if FeatureMemory && (!NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER)

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Returns a value indicating whether the characters in this instance are equal to the characters in a specified
    /// read-only character span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals?view=net-11.0#system-text-stringbuilder-equals(system-readonlyspan((system-char)))
    public static bool Equals(this StringBuilder target, ReadOnlySpan<char> span)
    {
        if (target.Length != span.Length)
        {
            return false;
        }

        // Walk the contiguous chunks and compare each with the vectorized SequenceEqual,
        // rather than indexing the StringBuilder per char (which walks the chunk list on every access).
        foreach (var chunk in target.GetChunks())
        {
            var chunkSpan = chunk.Span;
            if (!chunkSpan.SequenceEqual(span.Slice(0, chunkSpan.Length)))
            {
                return false;
            }

            span = span.Slice(chunkSpan.Length);
        }

        return true;
    }
}

#endif
