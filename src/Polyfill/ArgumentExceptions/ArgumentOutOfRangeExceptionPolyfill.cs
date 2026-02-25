namespace Polyfills;

using System.Diagnostics.CodeAnalysis;
// ReSharper disable once RedundantUsingDirective
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(ArgumentOutOfRangeException)
    {
#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifzero?view=net-11.0#system-argumentoutofrangeexception-throwifzero-1(-0-system-string)
        public static void ThrowIfZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
#if NET7_0_OR_GREATER
            where T : INumberBase<T>
        {
            if (T.IsZero(value))
            {
                ThrowZero(paramName);
            }
        }
#else
            where T : struct, IEquatable<T>
        {
            if (value.Equals(default))
            {
                ThrowZero(paramName);
            }
        }
#endif

#if !NET7_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
        public static void ThrowIfZero(nint value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value == 0)
            {
                ThrowZero(paramName);
            }
        }
#endif

        [DoesNotReturn]
        static void ThrowZero(string? paramName) =>
            throw new ArgumentOutOfRangeException(paramName, "Value must not be zero.");
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegative?view=net-11.0#system-argumentoutofrangeexception-throwifnegative-1(-0-system-string)
        public static void ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
#if NET7_0_OR_GREATER
            where T : INumberBase<T>
        {
            if (T.IsNegative(value))
            {
                ThrowNegative(value, paramName);
            }
        }
#else
            where T : struct, IComparable<T>
        {
            if (value.CompareTo(default(T)) < 0)
            {
                ThrowNegative(value, paramName);
            }
        }
#endif

#if !NET7_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegative?view=net-11.0#system-argumentoutofrangeexception-throwifnegative-1(-0-system-string)
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.</summary>
        public static void ThrowIfNegative(nint value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value < 0)
            {
                ThrowNegative(value, paramName);
            }
        }
#endif

        [DoesNotReturn]
        static void ThrowNegative<T>(T value, string? paramName) =>
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be non-negative.");

#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegativeorzero?view=net-11.0#system-argumentoutofrangeexception-throwifnegativeorzero-1(-0-system-string)
        public static void ThrowIfNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
#if NET7_0_OR_GREATER
            where T : INumberBase<T>
        {
            if (T.IsNegative(value) || T.IsZero(value))
            {
                ThrowNegativeOrZero(value, paramName);
            }
        }
#else
            where T : struct, IComparable<T>
        {
            if (value.CompareTo(default(T)) <= 0)
            {
                ThrowNegativeOrZero(value, paramName);
            }
        }
#endif
#endif

#if !NET7_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
        public static void ThrowIfNegativeOrZero(nint value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value <= 0)
            {
                ThrowNegativeOrZero(value, paramName);
            }
        }
#endif

#if !NET8_0_OR_GREATER
        [DoesNotReturn]
        static void ThrowNegativeOrZero<T>(T value, string? name) =>
            throw new ArgumentOutOfRangeException(name, value, $"{name} ('{value}') must be a non-negative and non-zero value.");
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifequal?view=net-11.0#system-argumentoutofrangeexception-throwifequal-1(-0-0-system-string)
        public static void ThrowIfEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IEquatable<T>?
        {
            if (EqualityComparer<T>.Default.Equals(value, other))
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must not be equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnotequal?view=net-11.0#system-argumentoutofrangeexception-throwifnotequal-1(-0-0-system-string)
        public static void ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IEquatable<T>?
        {
            if (!EqualityComparer<T>.Default.Equals(value, other))
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifgreaterthan?view=net-11.0#system-argumentoutofrangeexception-throwifgreaterthan-1(-0-0-system-string)
        public static void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than or equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
        public static void ThrowIfGreaterThan(nint value, nint other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value > other)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than or equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifgreaterthanorequal?view=net-11.0#system-argumentoutofrangeexception-throwifgreaterthanorequal-1(-0-0-system-string)
        public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) >= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>.</summary>
        public static void ThrowIfGreaterThanOrEqual(nint value, nint other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value >= other)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than or equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwiflessthan?view=net-11.0#system-argumentoutofrangeexception-throwiflessthan-1(-0-0-system-string)
        public static void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than or equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.</summary>
        public static void ThrowIfLessThan(nint value, nint other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value < other)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than or equal to {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwiflessthanorequal?view=net-11.0#system-argumentoutofrangeexception-throwiflessthanorequal-1(-0-0-system-string)
        public static void ThrowIfLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than {other}.");
            }
        }
#endif

#if !NET8_0_OR_GREATER
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal to <paramref name="other"/>.</summary>
        public static void ThrowIfLessThanOrEqual(nint value, nint other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value <= other)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than or equal to {other}.");
            }
        }
#endif
    }
}
