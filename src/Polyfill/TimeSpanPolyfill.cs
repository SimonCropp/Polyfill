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
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

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

    extension(TimeSpan)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of days, hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromdays?view=net-11.0#system-timespan-fromdays(system-int32-system-int32-system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromDays(int days, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            new((long)days * TimeSpan.TicksPerDay +
                (long)hours * TimeSpan.TicksPerHour +
                (long)minutes * TimeSpan.TicksPerMinute +
                (long)seconds * TimeSpan.TicksPerSecond +
                (long)milliseconds * TimeSpan.TicksPerMillisecond +
                (long)microseconds * MicrosecondsToTicks);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of hours, minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromhours?view=net-11.0#system-timespan-fromhours(system-int32-system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromHours(int hours, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            new((long)hours * TimeSpan.TicksPerHour +
                (long)minutes * TimeSpan.TicksPerMinute +
                (long)seconds * TimeSpan.TicksPerSecond +
                (long)milliseconds * TimeSpan.TicksPerMillisecond +
                (long)microseconds * MicrosecondsToTicks);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of minutes, seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromminutes?view=net-11.0#system-timespan-fromminutes(system-int32-system-int32-system-int32-system-int32)
        public static TimeSpan FromMinutes(int minutes, int seconds = 0, int milliseconds = 0, int microseconds = 0) =>
            new((long)minutes * TimeSpan.TicksPerMinute +
                (long)seconds * TimeSpan.TicksPerSecond +
                (long)milliseconds * TimeSpan.TicksPerMillisecond +
                (long)microseconds * MicrosecondsToTicks);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of seconds, milliseconds, and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=net-11.0#system-timespan-fromseconds(system-int64-system-int64-system-int64)
        public static TimeSpan FromSeconds(long seconds, long milliseconds = 0, long microseconds = 0) =>
            new(seconds * TimeSpan.TicksPerSecond +
                milliseconds * TimeSpan.TicksPerMillisecond +
                microseconds * MicrosecondsToTicks);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of milliseconds and microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-11.0#system-timespan-frommilliseconds(system-int64-system-int64)
        public static TimeSpan FromMilliseconds(long milliseconds, long microseconds = 0) =>
            new(milliseconds * TimeSpan.TicksPerMillisecond +
                microseconds * MicrosecondsToTicks);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpan"/> structure to a specified number of microseconds.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommicroseconds?view=net-11.0#system-timespan-frommicroseconds(system-int64)
        public static TimeSpan FromMicroseconds(long microseconds) =>
            new(microseconds * MicrosecondsToTicks);
    }
#endif
}
