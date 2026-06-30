namespace Polyfills;

using System;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER
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
            (int) (target.TicksComponent() / TicksPerMicrosecond % 1000);

        long TicksComponent()
        {
            var noSeconds = new TimeSpan(target.Days, target.Hours, target.Minutes, 0);
            var secondsPart = target - noSeconds;
            return secondsPart.Ticks;
        }
    }
#endif

#if !NET9_0_OR_GREATER
    const long MicrosecondsToTicks = TimeSpan.TicksPerMillisecond / 1000;
    const long MicrosecondsPerMillisecond = 1_000;
    const long MicrosecondsPerSecond = 1_000_000;
    const long MicrosecondsPerMinute = 60_000_000;
    const long MicrosecondsPerHour = 3_600_000_000;
    const long MicrosecondsPerDay = 86_400_000_000;

    // The BCL From* factories accumulate every component in microseconds in a 128-bit-wide space
    // and range-check only the FINAL tick count, throwing ArgumentOutOfRangeException on overflow.
    // Int64 microsecond accumulation overflows too early for the long overloads when components
    // cancel into a valid range, so accumulate in decimal (exact for these magnitudes) to mirror
    // the BCL. The message matches the BCL's Overflow_TimeSpanTooLong.
    static TimeSpan TicksFromMicroseconds(decimal totalMicroseconds)
    {
        var ticks = totalMicroseconds * MicrosecondsToTicks;
        if (ticks < long.MinValue || ticks > long.MaxValue)
        {
            throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
        }

        return new((long)ticks);
    }

    extension(TimeSpan)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of days, hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromdays?view=net-11.0#system-timespan-fromdays(system-int32-system-int32-system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromDays(int days, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            TicksFromMicroseconds(
                (decimal)days * MicrosecondsPerDay +
                (decimal)hours * MicrosecondsPerHour +
                (decimal)minutes * MicrosecondsPerMinute +
                (decimal)seconds * MicrosecondsPerSecond +
                (decimal)milliseconds * MicrosecondsPerMillisecond +
                microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromhours?view=net-11.0#system-timespan-fromhours(system-int32-system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromHours(int hours, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            TicksFromMicroseconds(
                (decimal)hours * MicrosecondsPerHour +
                (decimal)minutes * MicrosecondsPerMinute +
                (decimal)seconds * MicrosecondsPerSecond +
                (decimal)milliseconds * MicrosecondsPerMillisecond +
                microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromminutes?view=net-11.0#system-timespan-fromminutes(system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromMinutes(int minutes, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            TicksFromMicroseconds(
                (decimal)minutes * MicrosecondsPerMinute +
                (decimal)seconds * MicrosecondsPerSecond +
                (decimal)milliseconds * MicrosecondsPerMillisecond +
                microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=net-11.0#system-timespan-fromseconds(system-int64-system-int64-system-int64)
        public static TimeSpan FromSeconds(long seconds, long milliseconds = 0, long microseconds = 0) =>
            TicksFromMicroseconds(
                (decimal)seconds * MicrosecondsPerSecond +
                (decimal)milliseconds * MicrosecondsPerMillisecond +
                microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of milliseconds and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-11.0#system-timespan-frommilliseconds(system-int64-system-int64)
        public static TimeSpan FromMilliseconds(long milliseconds, long microseconds = 0) =>
            TicksFromMicroseconds(
                (decimal)milliseconds * MicrosecondsPerMillisecond +
                microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommicroseconds?view=net-11.0#system-timespan-frommicroseconds(system-int64)
        public static TimeSpan FromMicroseconds(long microseconds) =>
            TicksFromMicroseconds(microseconds);
    }
#endif
}
