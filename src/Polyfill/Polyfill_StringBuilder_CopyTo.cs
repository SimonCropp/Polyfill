#if FeatureMemory && !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER

namespace Polyfills;

using System;
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
        var destinationIndex = 0;
        while (true)
        {
            if (sourceIndex == target.Length)
            {
                break;
            }

            if (destinationIndex == count)
            {
                break;
            }

            destination[destinationIndex] = target[sourceIndex];
            destinationIndex++;
            sourceIndex++;
        }
    }
}

#endif
