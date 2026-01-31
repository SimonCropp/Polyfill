#if !NET7_0_OR_GREATER
namespace Polyfills;

using System;

static partial class Polyfill
{
    const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond * 1000;

    extension(TimeSpan target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="TimeSpan"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-10.0
        public int Nanoseconds =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="TimeSpan"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-10.0
        public int Microseconds =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;
    }

    extension(DateTime target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="DateTime"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-10.0
        public int Nanosecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="DateTime"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-10.0
        public int Microsecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;
    }

    extension(DateTimeOffset target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-10.0
        public int Nanosecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-10.0
        public int Microsecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;
    }

    static long TicksComponent(this TimeSpan target)
    {
        var noSeconds = new TimeSpan(target.Days, target.Hours, target.Minutes, 0);
        var secondsPart = target - noSeconds;
        return secondsPart.Ticks;
    }

    static long TicksComponent(this DateTime target)
    {
        var noSeconds = new DateTime(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Kind);
        var secondsPart = target - noSeconds;
        return secondsPart.Ticks;
    }

    static long TicksComponent(this DateTimeOffset target)
    {
        var noSeconds = new DateTimeOffset(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Offset);
        var secondsPart = target - noSeconds;
        return secondsPart.Ticks;
    }
}
#endif
