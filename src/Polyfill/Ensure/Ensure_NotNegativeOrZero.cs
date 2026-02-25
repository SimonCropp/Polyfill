namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable once RedundantUsingDirective
using System.Numerics;
using System.Runtime.CompilerServices;

#if PolyPublic
public
#endif
static partial class Ensure
{
    [DoesNotReturn]
    static void ThrowNegativeOrZero<T>(T value, string? name) =>
        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be a non-negative and non-zero value.");

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
#if NET7_0_OR_GREATER
        where T : INumberBase<T>
    {
        if (T.IsNegative(value) || T.IsZero(value))
        {
            ThrowNegativeOrZero(value, name);
        }

        return value;
    }
#else
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(default(T)) <= 0)
        {
            ThrowNegativeOrZero(value, name);
        }

        return value;
    }
#endif

#if !NET7_0_OR_GREATER
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static nint NotNegativeOrZero(nint value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        if (value <= 0)
        {
            ThrowNegativeOrZero(value, name);
        }

        return value;
    }
#endif
}