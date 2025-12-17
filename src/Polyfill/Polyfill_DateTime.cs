namespace Polyfills;

using System;
using System.Collections.Generic;
using System.ComponentModel;

static partial class Polyfill
{
#if NET6_0 || NET7_0
    /// <summary>
    /// Deconstructs this <see cref="DateTime"/> instance by <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateTime target, out DateOnly date, out TimeOnly time)
    {
        date = DateOnly.FromDateTime(target);
        time = TimeOnly.FromDateTime(target);
    }
#endif

#if !NET8_0_OR_GREATER
    /// <summary>
    ///  Deconstructs <see cref="DateTime"/> by <see cref="DateTime.Year"/>, <see cref="DateTime.Month"/> and <see cref="DateTime.Day"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateTime target, out int year, out int month, out int day)
    {
        year = target.Year;
        month = target.Month;
        day = target.Day;
    }
#endif

}
