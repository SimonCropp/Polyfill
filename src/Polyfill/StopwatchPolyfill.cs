#if !NET7_0_OR_GREATER

namespace Polyfills;

using System;
using System.Diagnostics;

static partial class Polyfill
{
    extension(Stopwatch)
    {
        /// <summary>
        /// Gets the elapsed time since the <paramref name="startingTimestamp"/> value retrieved using <see cref="Stopwatch.GetTimestamp"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.getelapsedtime?view=net-11.0#system-diagnostics-stopwatch-getelapsedtime(system-int64)
        public static TimeSpan GetElapsedTime(long startingTimestamp) =>
            GetElapsedTime(startingTimestamp, Stopwatch.GetTimestamp());

        /// <summary>
        /// Gets the elapsed time between two timestamps retrieved using <see cref="Stopwatch.GetTimestamp"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.getelapsedtime?view=net-11.0#system-diagnostics-stopwatch-getelapsedtime(system-int64-system-int64)
        public static TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp) =>
            new((long)((endingTimestamp - startingTimestamp) * ((double)TimeSpan.TicksPerSecond / Stopwatch.Frequency)));
    }
}

#endif
