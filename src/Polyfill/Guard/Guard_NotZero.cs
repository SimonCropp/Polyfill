namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System.Numerics;
using System.Runtime.CompilerServices;
using System;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif
static partial class Guard
{
    [DoesNotReturn]
    static void ThrowZero<T>(T value, string? name) =>
        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be a non-zero value.");

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
    /// <param name="value">The argument to validate as non-zero.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
#if NET7_0_OR_GREATER
        where T : INumberBase<T>
    {
        return Ensure.NotZero(value, name);
    }
#else
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(default(T)) == 0)
        {
            ThrowZero(value, name);
        }

        return value;
    }
#endif
}