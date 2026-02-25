
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
    static void ThrowNegativeOrZero<T>(T value, string? name) =>
        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be a non-negative and non-zero value.");

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
#if NET7_0_OR_GREATER
        where T : INumberBase<T>
    {
        return Ensure.NotNegativeOrZero(value, name);
    }
#else
        where T : struct, IComparable<T>
    {
        return Ensure.NotNegativeOrZero(value, name);
    }
#endif
}