#if !NET7_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    /// <summary>
    /// Returns a new <see cref="DateTime"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-11.0
    public static DateTime AddMicroseconds(this DateTime target, double microseconds) =>
       target.AddMilliseconds(microseconds / 1000);

    /// <summary>
    /// Returns a new <see cref="DateTimeOffset"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-11.0
    public static DateTimeOffset AddMicroseconds(this DateTimeOffset target, double microseconds) =>
       target.AddMilliseconds(microseconds / 1000);
}
#endif
