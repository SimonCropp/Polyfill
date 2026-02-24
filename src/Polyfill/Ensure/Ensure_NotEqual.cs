namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if PolyPublic
public
#endif
static partial class Ensure
{
    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.</summary>
    /// <param name="value">The argument to validate as not equal to <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="name">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    public static T NotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        if (!EqualityComparer<T>.Default.Equals(value, other))
        {
            return value;
        }

        throw new ArgumentOutOfRangeException(
            name,
            value,
            $"{name} ('{(object?) value ?? "null"}') must not be equal to '{(object?) other ?? "null"}'.");
    }
}