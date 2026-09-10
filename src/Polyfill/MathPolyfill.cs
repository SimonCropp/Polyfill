namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Math)
    {
#if !NET6_0_OR_GREATER

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-single-system-single-system-single)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-uintptr-system-uintptr-system-uintptr)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-uint64-system-uint64-system-uint64)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-uint32-system-uint32-system-uint32)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-uint16-system-uint16-system-uint16)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-sbyte-system-sbyte-system-sbyte)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-int32-system-int32-system-int32)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-int64-system-int64-system-int64)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-int16-system-int16-system-int16)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-double-system-double-system-double)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-decimal-system-decimal-system-decimal)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-byte-system-byte-system-byte)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0#system-math-clamp(system-intptr-system-intptr-system-intptr)
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

#if !NET5_0_OR_GREATER

        /// <summary>
        /// Produces the full product of two 64-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.bigmul?view=net-11.0#system-math-bigmul(system-int64-system-int64-system-int64@)
        public static long BigMul(long a, long b, out long low) =>
            BigMulCore(a, b, out low);

        /// <summary>
        /// Produces the full product of two unsigned 64-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.math.bigmul?view=net-11.0#system-math-bigmul(system-uint64-system-uint64-system-uint64@)
        public static ulong BigMul(ulong a, ulong b, out ulong low) =>
            BigMulCore(a, b, out low);

#endif

    }

#if !NET5_0_OR_GREATER

    // Splits each operand into 32-bit limbs and accumulates the four partial products,
    // which is what the runtime falls back to when there is no widening multiply intrinsic.
    static ulong BigMulCore(ulong left, ulong right, out ulong lower)
    {
        unchecked
        {
            var leftLower = (uint) left;
            var leftUpper = (uint) (left >> 32);
            var rightLower = (uint) right;
            var rightUpper = (uint) (right >> 32);

            var lowerLower = (ulong) leftLower * rightLower;
            var middle = (ulong) leftUpper * rightLower + (lowerLower >> 32);
            var middleLower = (ulong) leftLower * rightUpper + (uint) middle;

            lower = (middleLower << 32) | (uint) lowerLower;
            return (ulong) leftUpper * rightUpper + (middle >> 32) + (middleLower >> 32);
        }
    }

    static long BigMulCore(long left, long right, out long lower)
    {
        unchecked
        {
            var upper = BigMulCore((ulong) left, (ulong) right, out var unsignedLower);
            lower = (long) unsignedLower;
            // reinterpreting the unsigned product as signed overshoots by one operand for
            // each negative operand, so subtract the other operand for each sign bit set
            return (long) upper - ((left >> 63) & right) - ((right >> 63) & left);
        }
    }

#endif
}
