
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

    /// <summary>
    /// Returns a new <see cref="DateTime"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds")]
    public static DateTime AddMicroseconds(this DateTime target, double microseconds) =>
       target.AddMilliseconds(microseconds / 1000);

    /// <summary>
    /// Returns a new <see cref="DateTimeOffset"/> object that adds a specified number of microseconds to the value of this instance..
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds")]
    public static DateTimeOffset AddMicroseconds(this DateTimeOffset target, double microseconds) =>
       target.AddMilliseconds(microseconds / 1000);

}