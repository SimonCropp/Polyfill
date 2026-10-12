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

    // The decimal accumulation above costs hundreds of nanoseconds. When every component is small
    // enough that its own tick count is at most an eighth of long.MaxValue, the (at most six)
    // components cannot overflow an Int64 between them, so the ticks can be summed directly and
    // the result is always in range. Anything larger takes the decimal path, which decides
    // whether the total overflows.
    const long MaxFastPathTicks = long.MaxValue / 8;

    static bool IsWithin(long value, long bound) =>
        value >= -bound && value <= bound;

    static TimeSpan FromComponents(long days, long hours, long minutes, long seconds, long milliseconds, long microseconds)
    {
        if (IsWithin(days, MaxFastPathTicks / TimeSpan.TicksPerDay) &&
            IsWithin(hours, MaxFastPathTicks / TimeSpan.TicksPerHour) &&
            IsWithin(minutes, MaxFastPathTicks / TimeSpan.TicksPerMinute) &&
            IsWithin(seconds, MaxFastPathTicks / TimeSpan.TicksPerSecond) &&
            IsWithin(milliseconds, MaxFastPathTicks / TimeSpan.TicksPerMillisecond) &&
            IsWithin(microseconds, MaxFastPathTicks / MicrosecondsToTicks))
        {
            return new(
                days * TimeSpan.TicksPerDay +
                hours * TimeSpan.TicksPerHour +
                minutes * TimeSpan.TicksPerMinute +
                seconds * TimeSpan.TicksPerSecond +
                milliseconds * TimeSpan.TicksPerMillisecond +
                microseconds * MicrosecondsToTicks);
        }

        // Components an overload does not have arrive as zero; leave them out of the decimal sum
        decimal total = microseconds;
        if (days != 0)
        {
            total += (decimal)days * MicrosecondsPerDay;
        }

        if (hours != 0)
        {
            total += (decimal)hours * MicrosecondsPerHour;
        }

        if (minutes != 0)
        {
            total += (decimal)minutes * MicrosecondsPerMinute;
        }

        if (seconds != 0)
        {
            total += (decimal)seconds * MicrosecondsPerSecond;
        }

        if (milliseconds != 0)
        {
            total += (decimal)milliseconds * MicrosecondsPerMillisecond;
        }

        return TicksFromMicroseconds(total);
    }

    extension(TimeSpan)
    {
        /// <summary>
        /// The number of hours in 1 day.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.hoursperday?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static int HoursPerDay => 24;

        /// <summary>
        /// The number of microseconds in 1 day.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecondsperday?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MicrosecondsPerDay => 86_400_000_000;

        /// <summary>
        /// The number of microseconds in 1 hour.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecondsperhour?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MicrosecondsPerHour => 3_600_000_000;

        /// <summary>
        /// The number of microseconds in 1 millisecond.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecondspermillisecond?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MicrosecondsPerMillisecond => 1_000;

        /// <summary>
        /// The number of microseconds in 1 minute.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecondsperminute?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MicrosecondsPerMinute => 60_000_000;

        /// <summary>
        /// The number of microseconds in 1 second.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecondspersecond?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MicrosecondsPerSecond => 1_000_000;

        /// <summary>
        /// The number of milliseconds in 1 day.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.millisecondsperday?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MillisecondsPerDay => 86_400_000;

        /// <summary>
        /// The number of milliseconds in 1 hour.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.millisecondsperhour?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MillisecondsPerHour => 3_600_000;

        /// <summary>
        /// The number of milliseconds in 1 minute.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.millisecondsperminute?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MillisecondsPerMinute => 60_000;

        /// <summary>
        /// The number of milliseconds in 1 second.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.millisecondspersecond?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MillisecondsPerSecond => 1_000;

        /// <summary>
        /// The number of minutes in 1 day.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.minutesperday?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MinutesPerDay => 1_440;

        /// <summary>
        /// The number of minutes in 1 hour.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.minutesperhour?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long MinutesPerHour => 60;

        /// <summary>
        /// The number of seconds in 1 day.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.secondsperday?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long SecondsPerDay => 86_400;

        /// <summary>
        /// The number of seconds in 1 hour.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.secondsperhour?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long SecondsPerHour => 3_600;

        /// <summary>
        /// The number of seconds in 1 minute.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.secondsperminute?view=net-11.0
        //Note: A property rather than a const, since extension members cannot declare constants, so it cannot be used in a constant expression.
        public static long SecondsPerMinute => 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of days, hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromdays?view=net-11.0#system-timespan-fromdays(system-int32-system-int32-system-int64-system-int64-system-int64-system-int64)
        public static TimeSpan FromDays(int days, int hours = 0, long minutes = 0, long seconds = 0, long milliseconds = 0, long microseconds = 0) =>
            FromComponents(days, hours, minutes, seconds, milliseconds, microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromhours?view=net-11.0#system-timespan-fromhours(system-int32-system-int64-system-int64-system-int64-system-int64)
        public static TimeSpan FromHours(int hours, long minutes = 0, long seconds = 0, long milliseconds = 0, long microseconds = 0) =>
            FromComponents(0, hours, minutes, seconds, milliseconds, microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromminutes?view=net-11.0#system-timespan-fromminutes(system-int64-system-int64-system-int64-system-int64)
        public static TimeSpan FromMinutes(long minutes, long seconds = 0, long milliseconds = 0, long microseconds = 0) =>
            FromComponents(0, 0, minutes, seconds, milliseconds, microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=net-11.0#system-timespan-fromseconds(system-int64-system-int64-system-int64)
        public static TimeSpan FromSeconds(long seconds, long milliseconds = 0, long microseconds = 0) =>
            FromComponents(0, 0, 0, seconds, milliseconds, microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of milliseconds and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-11.0#system-timespan-frommilliseconds(system-int64-system-int64)
        public static TimeSpan FromMilliseconds(long milliseconds, long microseconds = 0) =>
            FromComponents(0, 0, 0, 0, milliseconds, microseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommicroseconds?view=net-11.0#system-timespan-frommicroseconds(system-int64)
        public static TimeSpan FromMicroseconds(long microseconds) =>
            FromComponents(0, 0, 0, 0, 0, microseconds);
    }
#endif
}
