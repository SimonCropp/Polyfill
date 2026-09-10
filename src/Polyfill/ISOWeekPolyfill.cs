#if NET6_0_OR_GREATER && !NET10_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;

static partial class Polyfill
{
    extension(ISOWeek)
    {
        /// <summary>
        /// Calculates the ISO week number of a given date.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.isoweek.getweekofyear?view=net-11.0#system-globalization-isoweek-getweekofyear(system-dateonly)
        //Note: Only available on net6.0 and later, since DateOnly does not exist below that.
        public static int GetWeekOfYear(DateOnly date) =>
            ISOWeek.GetWeekOfYear(date.ToDateTime(TimeOnly.MinValue));

        /// <summary>
        /// Calculates the ISO week-numbering year (also called ISO year informally) mapped to the input date.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.isoweek.getyear?view=net-11.0#system-globalization-isoweek-getyear(system-dateonly)
        //Note: Only available on net6.0 and later, since DateOnly does not exist below that.
        public static int GetYear(DateOnly date) =>
            ISOWeek.GetYear(date.ToDateTime(TimeOnly.MinValue));

        /// <summary>
        /// Maps the ISO week date represented by a specified ISO year, week number, and day of week to the equivalent <see cref="DateOnly"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.isoweek.todateonly?view=net-11.0
        //Note: Only available on net6.0 and later, since DateOnly does not exist below that.
        public static DateOnly ToDateOnly(int year, int week, DayOfWeek dayOfWeek) =>
            DateOnly.FromDateTime(ISOWeek.ToDateTime(year, week, dayOfWeek));
    }
}

#endif
