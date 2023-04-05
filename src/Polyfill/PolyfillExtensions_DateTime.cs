
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
    const long TicksPerMicrosecond = 10;

    /// <summary>
    /// Gets the nanosecond component of the time represented by the current DateTime object.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond")]
    public static int Nanosecond(
        this DateTime target)
    {
#if NET7_0_OR_GREATER
        return target.Nanosecond;
#else
        return (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;
#endif
    }

    /// <summary>
    /// Gets the nanosecond component of the time represented by the current DateTimeOffset object.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond")]
    public static int Nanosecond(
        this DateTimeOffset target)
    {
#if NET7_0_OR_GREATER
        return target.Nanosecond;
#else
        return (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;
#endif
    }
    /// <summary>
    /// Gets the microsecond component of the time represented by the current DateTime object.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond")]
    public static int Microsecond(
        this DateTime target)
    {
#if NET7_0_OR_GREATER
        return target.Microsecond;
#else
        return (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;
#endif
    }

    /// <summary>
    /// Gets the microsecond component of the time represented by the current DateTimeOffset object.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond")]
    public static int Microsecond(
        this DateTimeOffset target)
    {
#if NET7_0_OR_GREATER
        return target.Microsecond;
#else
        return (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;
#endif
    }

    static long TicksComponent(this DateTime target)
    {
        var withNoSeconds = new DateTime(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Kind);
        var secondsPart = target - withNoSeconds;
        return secondsPart.Ticks;
    }

    static long TicksComponent(this DateTimeOffset target)
    {
        var withNoSeconds = new DateTimeOffset(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Offset);
        var secondsPart = target - withNoSeconds;
        return secondsPart.Ticks;
    }
}