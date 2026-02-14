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
