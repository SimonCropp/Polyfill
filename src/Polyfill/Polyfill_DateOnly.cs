#if NET6_0 || NET7_0
namespace Polyfills;

using System;
using System.Collections.Generic;
using System.ComponentModel;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.deconstruct?view=net-11.0
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DateOnly target, out int year, out int month, out int day)
    {
        year = target.Year;
        month = target.Month;
        day = target.Day;
    }
}

#endif
