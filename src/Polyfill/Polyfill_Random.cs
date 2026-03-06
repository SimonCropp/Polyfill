namespace Polyfills;

using System;

static partial class Polyfill
{
#if !NET8_0_OR_GREATER

#if FeatureMemory

    /// <summary>
    /// Fills the elements of a specified span with items chosen at random from the provided set of choices.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0)))
    public static void GetItems<T>(
        this Random target,
        ReadOnlySpan<T> choices,
        Span<T> destination)
    {
        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        for (var i = 0; i < destination.Length; i++)
        {
            destination[i] = choices[target.Next(choices.Length)];
        }
    }

    /// <summary>
    /// Creates an array populated with items chosen at random from the provided set of choices.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(system-readonlyspan((-0))-system-int32)
    public static T[] GetItems<T>(
        this Random target,
        ReadOnlySpan<T> choices,
        int length)
    {
        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        var result = new T[length];
        for (var i = 0; i < length; i++)
        {
            result[i] = choices[target.Next(choices.Length)];
        }

        return result;
    }

#endif

    /// <summary>
    /// Creates an array populated with items chosen at random from the provided set of choices.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(-0()-system-int32)
    public static T[] GetItems<T>(
        this Random target,
        T[] choices,
        int length)
    {
        if (choices.Length == 0)
        {
            throw new ArgumentException("Choices cannot be null or empty.", nameof(choices));
        }

        var result = new T[length];
        for (var i = 0; i < length; i++)
        {
            result[i] = choices[target.Next(choices.Length)];
        }

        return result;
    }

#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Returns a non-negative random integer.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextint64?view=net-11.0#system-random-nextint64
    public static long NextInt64(this Random target) =>
        target.NextInt64(long.MaxValue);

    /// <summary>
    /// Returns a non-negative random integer that is less than the specified maximum.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextint64?view=net-11.0#system-random-nextint64(system-int64)
    public static long NextInt64(this Random target, long maxValue) =>
        target.NextInt64(0, maxValue);

    /// <summary>
    /// Returns a random integer that is within a specified range.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextint64?view=net-11.0#system-random-nextint64(system-int64-system-int64)
    public static long NextInt64(this Random target, long minValue, long maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(minValue));
        }

        if (minValue == maxValue)
        {
            return minValue;
        }

        var range = (ulong)(maxValue - minValue);
        var limit = ulong.MaxValue - ulong.MaxValue % range;
        ulong result;
        do
        {
            var buf = new byte[8];
            target.NextBytes(buf);
            result = BitConverter.ToUInt64(buf, 0);
        } while (result >= limit);

        return (long)(result % range) + minValue;
    }

    /// <summary>
    /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextsingle?view=net-11.0
    public static float NextSingle(this Random target) =>
        (float)target.NextDouble();

#endif

#if !NET10_0_OR_GREATER

    /// <summary>
    /// Creates a string filled with random hexadecimal characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.gethexstring?view=net-11.0#system-random-gethexstring(system-int32-system-boolean)
    public static string GetHexString(this Random target, int stringLength, bool lowercase = false)
    {
        if (stringLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stringLength));
        }

        if (stringLength == 0)
        {
            return string.Empty;
        }

        var hexAlphabet = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";
        var chars = new char[stringLength];
        for (var i = 0; i < stringLength; i++)
        {
            chars[i] = hexAlphabet[target.Next(16)];
        }

        return new string(chars);
    }

#if FeatureMemory

    /// <summary>
    /// Fills a buffer with random hexadecimal characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.gethexstring?view=net-11.0#system-random-gethexstring(system-span((system-char))-system-boolean)
    public static void GetHexString(this Random target, Span<char> destination, bool lowercase = false)
    {
        var hexAlphabet = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";
        for (var i = 0; i < destination.Length; i++)
        {
            destination[i] = hexAlphabet[target.Next(16)];
        }
    }

    /// <summary>
    /// Creates a string populated with characters chosen at random from the provided set of choices.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.getstring?view=net-11.0
    public static string GetString(this Random target, ReadOnlySpan<char> choices, int length)
    {
        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if (length == 0)
        {
            return string.Empty;
        }

        var chars = new char[length];
        for (var i = 0; i < length; i++)
        {
            chars[i] = choices[target.Next(choices.Length)];
        }

        return new string(chars);
    }

#endif

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER && FeatureMemory

    /// <summary>
    /// Fills the elements of a specified span of bytes with random numbers.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte)))
    public static void NextBytes(
        this Random target,
        Span<byte> buffer)
    {
        var array = new byte[buffer.Length];
        target.NextBytes(array);
        array.CopyTo(buffer);
    }
#endif

#if !NET8_0_OR_GREATER

    /// <summary>
    /// Performs an in-place shuffle of an array.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte)))
    public static void Shuffle<T>(
        this Random target,
        T[] values)
    {
        var n = values.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var j = target.Next(i, n);

            if (j != i)
            {
                var temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
    }
#if FeatureMemory

    /// <summary>
    /// Performs an in-place shuffle of a span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte)))
    public static void Shuffle<T>(
        this Random target,
        Span<T> values)
    {
        var n = values.Length;

        for (int i = 0; i < n - 1; i++)
        {
            var j = target.Next(i, n);

            if (j != i)
            {
                var temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
    }
#endif
#endif
}
