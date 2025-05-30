// <auto-generated />
#pragma warning disable

#if FeatureValueTuple && !RefsBclMemory
#if !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

namespace System;

using Diagnostics;
using Diagnostics.CodeAnalysis;
using Runtime.CompilerServices;

/// <summary>Represent a range has start and end indexes.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
record Range(Index Start, Index End)
{
    /// <summary>Converts the value of the current Range object to its equivalent string representation.</summary>
    public override string ToString() =>
        $"{Start}..{End}";

    /// <summary>Create a Range object starting from start index to the end of the collection.</summary>
    public static Range StartAt(Index start) => new(start, Index.End);

    /// <summary>Create a Range object starting from first element in the collection to the end Index.</summary>
    public static Range EndAt(Index end) => new(Index.Start, end);

    /// <summary>Create a Range object starting from first element to the end.</summary>
    public static Range All => new(Index.Start, Index.End);

    /// <summary>Calculate the start offset and length of range object using a collection length.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int Offset, int Length) GetOffsetAndLength(int length)
    {
        int start;
        var startIndex = Start;
        if (startIndex.IsFromEnd)
        {
            start = length - startIndex.Value;
        }
        else
        {
            start = startIndex.Value;
        }

        int end;
        var endIndex = End;
        if (endIndex.IsFromEnd)
        {
            end = length - endIndex.Value;
        }
        else
        {
            end = endIndex.Value;
        }

        if ((uint)end > (uint)length || (uint)start > (uint)end)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        return (start, end - start);
    }
}
#else
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(System.Range))]
#endif
#endif