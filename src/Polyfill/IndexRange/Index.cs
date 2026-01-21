#if FeatureValueTuple && !RefsBclMemory
#if !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

namespace System;

using Diagnostics;
using Diagnostics.CodeAnalysis;
using Runtime.CompilerServices;

/// <summary>Represent a type can be used to index a collection either from the start or the end.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
readonly struct Index : IEquatable<Index>
{
    readonly int _value;

    /// <summary>Construct an Index using a value and indicating if the index is from the start or from the end.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Index(int value, bool fromEnd = false)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        if (fromEnd)
        {
            _value = ~value;
        }
        else
        {
            _value = value;
        }
    }

    Index(int value) =>
        _value = value;

    /// <summary>Create an Index pointing at first element.</summary>
    public static Index Start => new(0);

    /// <summary>Create an Index pointing at beyond last element.</summary>
    public static Index End => new(~0);

    /// <summary>Create an Index from the start at the position indicated by the value.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Index FromStart(int value)
    {
        if (value < 0)
        {
            throw new IndexOutOfRangeException(nameof(value));
        }

        return new(value);
    }

    /// <summary>Create an Index from the end at the position indicated by the value.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Index FromEnd(int value)
    {
        if (value < 0)
        {
            throw new IndexOutOfRangeException(nameof(value));
        }

        return new(~value);
    }

    /// <summary>Returns the index value.</summary>
    public int Value
    {
        get
        {
            if (_value < 0)
            {
                return ~_value;
            }

            return _value;
        }
    }

    /// <summary>Indicates whether the index is from the start or the end.</summary>
    public bool IsFromEnd => _value < 0;

    /// <summary>Calculate the offset from the start using the giving collection length.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffset(int length)
    {
        var offset = _value;
        if (IsFromEnd)
        {
            offset += length + 1;
        }

        return offset;
    }

    /// <summary>Indicates whether the current Index object is equal to another object of the same type.</summary>
    public override bool Equals(object value) => value is Index index && _value == index._value;

    /// <summary>Indicates whether the current Index object is equal to another Index object.</summary>
    public bool Equals(Index other) => _value == other._value;

    /// <summary>Returns the hash code for this instance.</summary>
    public override int GetHashCode() => _value;

    /// <summary>Converts integer number to an Index.</summary>
    public static implicit operator Index(int value) => FromStart(value);

    /// <summary>Converts the value of the current Index object to its equivalent string representation.</summary>
    public override string ToString()
    {
        if (IsFromEnd)
        {
            return ToStringFromEnd();
        }

        return ((uint)Value).ToString();
    }

    string ToStringFromEnd() =>
        '^' + Value.ToString();
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Index))]
#endif
#endif