namespace Polyfills;

using System;
using System.Diagnostics;

static partial class Polyfill
{
    extension(Math)
    {
#if !NET6_0_OR_GREATER

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nuint Clamp(nuint value, nuint min, nuint max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Clamp(ulong value, ulong min, ulong max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(uint value, uint min, uint max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Clamp(ushort value, ushort min, ushort max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(long value, long min, long max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Clamp(short value, short min, short max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Clamp(decimal value, decimal min, decimal max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(byte value, byte min, byte max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nint Clamp(nint value, nint min, nint max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than {max}.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

#endif
    }
}
