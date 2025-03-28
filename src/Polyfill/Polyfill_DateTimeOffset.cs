// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
#if NET6_0 || NET7_0
    /// <summary>
    /// Deconstructs this <see cref="DateTimeOffset"/> instance by <see cref="DateOnly"/>, <see cref="TimeOnly"/>, and <see cref="TimeSpan"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-9.0#definition
    public static void Deconstruct(this DateTimeOffset target, out DateOnly date, out TimeOnly time, out TimeSpan offset)
    {
        date = DateOnly.FromDateTime(target.DateTime);
        time = TimeOnly.FromDateTime(target.DateTime);

        offset = target.Offset;
    }
#endif
}
