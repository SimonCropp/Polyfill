
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{


    const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond * 1000;

    /// <summary>
    /// Gets the nanosecond component of the time represented by the current <see cref="TimeSpan"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds")]
    public static int Nanoseconds(this TimeSpan target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

    /// <summary>
    /// Gets the nanosecond component of the time represented by the current <see cref="DateTime"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond")]
    public static int Nanosecond(this DateTime target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

    /// <summary>
    /// Gets the nanosecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond")]
    public static int Nanosecond(this DateTimeOffset target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

    /// <summary>
    /// Gets the microsecond component of the time represented by the current <see cref="TimeSpan"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds")]
    public static int Microseconds(this TimeSpan target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

    /// <summary>
    /// Gets the microsecond component of the time represented by the current <see cref="DateTime"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond")]
    public static int Microsecond(this DateTime target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

    /// <summary>
    /// Gets the microsecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond")]
    public static int Microsecond(this DateTimeOffset target) =>
        (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

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