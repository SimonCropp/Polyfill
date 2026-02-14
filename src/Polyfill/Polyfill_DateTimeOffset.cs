#if NET6_0 || NET7_0
namespace Polyfills;

using System;
using System.Collections.Generic;
using System.ComponentModel;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-11.0
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateTimeOffset target, out DateOnly date, out TimeOnly time, out TimeSpan offset)
    {
        date = DateOnly.FromDateTime(target.DateTime);
        time = TimeOnly.FromDateTime(target.DateTime);

        offset = target.Offset;
    }
}

#endif
