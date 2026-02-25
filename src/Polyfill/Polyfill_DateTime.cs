namespace Polyfills;

using System;
using System.ComponentModel;

static partial class Polyfill
{
#if NET6_0 || NET7_0
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-11.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateTime target, out DateOnly date, out TimeOnly time)
    {
        date = DateOnly.FromDateTime(target);
        time = TimeOnly.FromDateTime(target);
    }
#endif

#if !NET8_0_OR_GREATER
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-11.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateTime target, out int year, out int month, out int day)
    {
        year = target.Year;
        month = target.Month;
        day = target.Day;
    }
#endif

}
