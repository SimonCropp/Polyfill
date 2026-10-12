#if FeatureMemory && !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER

namespace Polyfills;

using System;
using System.Buffers;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Copies the characters from a specified segment of this instance to a destination Char span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto?view=net-11.0#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32)
    public static void CopyTo(
        this StringBuilder target,
        int sourceIndex,
        Span<char> destination,
        int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if ((uint)sourceIndex > (uint)target.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(sourceIndex));
        }

        if (sourceIndex > target.Length - count)
        {
            throw new ArgumentException("The source StringBuilder does not contain enough characters from sourceIndex onward to satisfy count.");
        }

        if (count > destination.Length)
        {
            throw new ArgumentException("The destination span is too short to hold the requested characters.", nameof(destination));
        }

        if (count == 0)
        {
            return;
        }

        // The indexer walks the chunk list on every call, so copy in blocks through the array overload instead.
        var buffer = ArrayPool<char>.Shared.Rent(Math.Min(count, 16384));
        try
        {
            var copied = 0;
            while (copied < count)
            {
                var length = Math.Min(count - copied, buffer.Length);
                target.CopyTo(sourceIndex + copied, buffer, 0, length);
                buffer.AsSpan(0, length).CopyTo(destination.Slice(copied));
                copied += length;
            }
        }
        finally
        {
            ArrayPool<char>.Shared.Return(buffer);
        }
    }
}

#endif
