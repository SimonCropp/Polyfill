namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System.Numerics;
using System.Runtime.CompilerServices;
using System;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif
static partial class Ensure
{
    [DoesNotReturn]
    static void ThrowNegative<T>(T value, string? name) =>
        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be a non-negative value.");

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.</summary>
    /// <param name="value">The argument to validate as non-negative.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
#if NET7_0_OR_GREATER
        where T : INumberBase<T>
    {
        if (T.IsNegative(value))
        {
            ThrowNegative(value, name);
        }

        return value;
    }
#else
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(default(T)) < 0)
        {
            ThrowNegative(value, name);
        }

        return value;
    }
#endif

#if !NET7_0_OR_GREATER
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.</summary>
    /// <param name="value">The argument to validate as non-negative.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static nint NotNegative(nint value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        if (value < 0)
        {
            ThrowNegative(value, name);
        }

        return value;
    }
#endif
}