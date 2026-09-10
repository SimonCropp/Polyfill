#pragma warning disable

#if !NET9_0_OR_GREATER

namespace Polyfills;

using System.Threading;

static partial class Polyfill
{
    // There is no 8 or 16 bit interlocked primitive below net9, and the two ways of faking one
    // are both worse than a lock:
    //
    //  * Widening to a CompareExchange on the containing 32 bit word writes the neighbouring
    //    bytes back, so a concurrent plain write to an adjacent byte can be lost. The BCL does
    //    not have that behaviour, so it would be a silent downgrade rather than a slowdown.
    //  * It also needs the address to find the aligned word, so it can read and write outside
    //    the allocation for a location at the end of one.
    //
    // The lock keeps each operation indivisible with respect to every other one, and since these
    // members do not exist at all below net9 there is nothing outside this file to interoperate
    // with. Writing only the target byte also leaves neighbours alone, matching the BCL.
    static object narrowInterlockedLock = new();

    extension(Interlocked)
    {
        /// <summary>
        /// Sets an 8-bit unsigned integer to a specified value and returns the original value, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.exchange?view=net-11.0#system-threading-interlocked-exchange(system-byte@-system-byte)
        //Note: Uses a lock rather than a hardware atomic, since no 8 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static byte Exchange(ref byte location1, byte value)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                location1 = value;
                return original;
            }
        }

        /// <summary>
        /// Compares two 8-bit unsigned integers for equality and, if they are equal, replaces the first value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.compareexchange?view=net-11.0#system-threading-interlocked-compareexchange(system-byte@-system-byte-system-byte)
        //Note: Uses a lock rather than a hardware atomic, since no 8 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static byte CompareExchange(ref byte location1, byte value, byte comparand)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                if (original == comparand)
                {
                    location1 = value;
                }

                return original;
            }
        }

        /// <summary>
        /// Sets an 8-bit signed integer to a specified value and returns the original value, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.exchange?view=net-11.0#system-threading-interlocked-exchange(system-sbyte@-system-sbyte)
        //Note: Uses a lock rather than a hardware atomic, since no 8 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static sbyte Exchange(ref sbyte location1, sbyte value)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                location1 = value;
                return original;
            }
        }

        /// <summary>
        /// Compares two 8-bit signed integers for equality and, if they are equal, replaces the first value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.compareexchange?view=net-11.0#system-threading-interlocked-compareexchange(system-sbyte@-system-sbyte-system-sbyte)
        //Note: Uses a lock rather than a hardware atomic, since no 8 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static sbyte CompareExchange(ref sbyte location1, sbyte value, sbyte comparand)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                if (original == comparand)
                {
                    location1 = value;
                }

                return original;
            }
        }

        /// <summary>
        /// Sets a 16-bit signed integer to a specified value and returns the original value, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.exchange?view=net-11.0#system-threading-interlocked-exchange(system-int16@-system-int16)
        //Note: Uses a lock rather than a hardware atomic, since no 16 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static short Exchange(ref short location1, short value)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                location1 = value;
                return original;
            }
        }

        /// <summary>
        /// Compares two 16-bit signed integers for equality and, if they are equal, replaces the first value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.compareexchange?view=net-11.0#system-threading-interlocked-compareexchange(system-int16@-system-int16-system-int16)
        //Note: Uses a lock rather than a hardware atomic, since no 16 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static short CompareExchange(ref short location1, short value, short comparand)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                if (original == comparand)
                {
                    location1 = value;
                }

                return original;
            }
        }

        /// <summary>
        /// Sets a 16-bit unsigned integer to a specified value and returns the original value, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.exchange?view=net-11.0#system-threading-interlocked-exchange(system-uint16@-system-uint16)
        //Note: Uses a lock rather than a hardware atomic, since no 16 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static ushort Exchange(ref ushort location1, ushort value)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                location1 = value;
                return original;
            }
        }

        /// <summary>
        /// Compares two 16-bit unsigned integers for equality and, if they are equal, replaces the first value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.compareexchange?view=net-11.0#system-threading-interlocked-compareexchange(system-uint16@-system-uint16-system-uint16)
        //Note: Uses a lock rather than a hardware atomic, since no 16 bit interlocked primitive exists below net9. Correct against other Interlocked calls, but a concurrent plain write to the same location can be lost, where the BCL operation is indivisible.
        public static ushort CompareExchange(ref ushort location1, ushort value, ushort comparand)
        {
            lock (narrowInterlockedLock)
            {
                var original = location1;
                if (original == comparand)
                {
                    location1 = value;
                }

                return original;
            }
        }
    }
}

#endif
