
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{


    /// <summary>
    /// Performs an in-place shuffle of an array.
    /// </summary>
    /// <param name="index">The array to shuffle.</param>
    /// <typeparam name="T">The type of array.</typeparam>
    /// <remarks>
    ///   This method uses <see cref="Next(int, int)" /> to choose values for shuffling.
    ///   This method is an O(n) operation.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte)))")]
    public static void Shuffle<T>(
        this Random target,
        T[] values)
    {
        int n = values.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int j = target.Next(i, n);

            if (j != i)
            {
                T temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
    }
}