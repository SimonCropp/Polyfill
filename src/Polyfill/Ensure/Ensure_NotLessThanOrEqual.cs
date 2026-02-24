
namespace Polyfills;

using System.Numerics;
using System.Runtime.CompilerServices;
using System;

#if PolyPublic
public
#endif
static partial class Ensure
{
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal <paramref name="other"/>.</summary>
    /// <param name="value">The argument to validate as greater than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? name = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) > 0)
        {
            return value;
        }

        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be greater than '{other}'.");
    }

#if !NET8_0_OR_GREATER
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal <paramref name="other"/>.</summary>
    /// <param name="value">The argument to validate as greater than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static nint NotLessThanOrEqual(nint value, nint other, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        if (!(value <= other))
        {
            return value;
        }

        throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be less than or equal to '{other}'.");
    }
#endif
}
