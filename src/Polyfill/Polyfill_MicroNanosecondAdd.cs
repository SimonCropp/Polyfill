#if !NET7_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    // 1 microsecond == 10 ticks (1 tick == 100ns). Routing through AddTicks preserves
    // sub-millisecond precision; AddMilliseconds rounds its argument to whole milliseconds
    // on .NET Framework / netcoreapp2.0, which would discard it.

    /// <summary>
    /// Returns a new <see cref="DateTime"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-11.0
    public static DateTime AddMicroseconds(this DateTime target, double microseconds) =>
       target.AddTicks(AddMicrosecondsToTicks(microseconds));

    /// <summary>
    /// Returns a new <see cref="DateTimeOffset"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-11.0
    public static DateTimeOffset AddMicroseconds(this DateTimeOffset target, double microseconds) =>
       target.AddTicks(AddMicrosecondsToTicks(microseconds));

    // The number of microseconds in DateTime.MaxValue
    const long maxMicroseconds = 315537897600000000;

    static long AddMicrosecondsToTicks(double microseconds)
    {
        if (Math.Abs(microseconds) > maxMicroseconds)
        {
            throw new ArgumentOutOfRangeException(nameof(microseconds), "Value to add was out of range.");
        }

        // Mirrors the BCL. Multiplying the double by 10 would lose precision for large values,
        // so the integral part is multiplied as a long, and the ticks of the fractional part added.
        var integralPart = Math.Truncate(microseconds);
        var fractionalPart = microseconds - integralPart;
        var ticks = (long) integralPart * 10;
        ticks += (long) (fractionalPart * 10);
        return ticks;
    }
}
#endif
