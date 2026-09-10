#if !NET10_0_OR_GREATER

namespace Polyfills;

using System.Threading;

static partial class Polyfill
{
    extension(Volatile)
    {
        /// <summary>
        /// Synchronizes memory access as follows: the processor that executes the current thread cannot reorder
        /// instructions in such a way that memory reads before the call to <c>ReadBarrier</c> execute after memory
        /// accesses that follow the call to <c>ReadBarrier</c>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.volatile.readbarrier?view=net-11.0
        //Note: Implemented as a full fence, since no one way barrier exists below net10. Correct but stronger than required, and unlike the BCL it costs a processor instruction on x86 and x64.
        public static void ReadBarrier() =>
            Interlocked.MemoryBarrier();

        /// <summary>
        /// Synchronizes memory access as follows: the processor that executes the current thread cannot reorder
        /// instructions in such a way that memory accesses before the call to <c>WriteBarrier</c> execute after
        /// memory writes that follow the call to <c>WriteBarrier</c>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.volatile.writebarrier?view=net-11.0
        //Note: Implemented as a full fence, since no one way barrier exists below net10. Correct but stronger than required, and unlike the BCL it costs a processor instruction on x86 and x64.
        public static void WriteBarrier() =>
            Interlocked.MemoryBarrier();
    }
}

#endif
