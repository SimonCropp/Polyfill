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
    static void ThrowZero(string? name) =>
        throw new ArgumentOutOfRangeException(name, $"{name} must be a non-zero value.");

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
    /// <param name="value">The argument to validate as non-zero.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
#if NET7_0_OR_GREATER
        where T : INumberBase<T>
    {
        if (T.IsZero(value))
        {
            ThrowZero(name);
        }

        return value;
    }
#else
        where T : struct, IEquatable<T>
    {
        if (value.Equals(default(T)))
        {
            ThrowZero(name);
        }

        return value;
    }
#endif

#if !NET7_0_OR_GREATER
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
    /// <param name="value">The argument to validate as non-zero.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static nint NotZero(nint value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        if(value == 0)
        {
            ThrowZero(name);
        }

        return value;
    }
#endif
}