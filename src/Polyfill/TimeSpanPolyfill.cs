#if !NET7_0_OR_GREATER
namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(TimeSpan target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="TimeSpan"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-11.0
        public int Nanoseconds =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="TimeSpan"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-11.0
        public int Microseconds =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

        long TicksComponent()
        {
            var noSeconds = new TimeSpan(target.Days, target.Hours, target.Minutes, 0);
            var secondsPart = target - noSeconds;
            return secondsPart.Ticks;
        }
    }

}
#endif
