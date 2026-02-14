namespace Polyfills;

using System;
using System.ComponentModel;

static partial class Polyfill
{
#if NET7_0
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this TimeOnly target, out int hour, out int minute, out int second, out int millisecond, out int microsecond)
    {
        hour = target.Hour;
        minute = target.Minute;
        second = target.Second;
        millisecond = target.Millisecond;
        microsecond = target.Microsecond;
    }
#endif

#if NET6_0 || NET7_0
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this TimeOnly target, out int hour, out int minute, out int second, out int millisecond)
    {
        hour = target.Hour;
        minute = target.Minute;
        second = target.Second;
        millisecond = target.Millisecond;
    }

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this TimeOnly target, out int hour, out int minute, out int second)
    {
        hour = target.Hour;
        minute = target.Minute;
        second = target.Second;
    }

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this TimeOnly target, out int hour, out int minute)
    {
        hour = target.Hour;
        minute = target.Minute;
    }
#endif
}
