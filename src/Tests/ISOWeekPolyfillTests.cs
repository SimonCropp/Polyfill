#if NET6_0_OR_GREATER

using System.Globalization;

// Runs on net6.0 and later, so net10.0 and above validate the BCL and net6.0 through net9.0
// the polyfill. The DateTime overloads have existed since netcoreapp3.0 on every one of those,
// so they are the oracle rather than hand written expectations.
public class ISOWeekPolyfillTests
{
    [Test]
    public async Task MatchesTheDateTimeOverloads()
    {
        var failures = 0;
        var visited = 0;
        var date = new DateTime(1, 1, 1);
        var last = new DateTime(9999, 12, 31);
        while (true)
        {
            visited++;
            var only = DateOnly.FromDateTime(date);
            if (ISOWeek.GetWeekOfYear(only) != ISOWeek.GetWeekOfYear(date) ||
                ISOWeek.GetYear(only) != ISOWeek.GetYear(date))
            {
                failures++;
                break;
            }

            if (date == last)
            {
                break;
            }

            date = date.AddDays(1);
        }

        await Assert.That(failures).IsEqualTo(0);
        // every representable date, so a future edit cannot quietly shrink the sweep
        await Assert.That(visited).IsEqualTo(3652059);
    }

    [Test]
    public async Task ToDateOnly_MatchesToDateTime()
    {
        var failures = 0;
        var visited = 0;
        for (var year = 1; year <= 9999; year++)
        {
            var weeks = ISOWeek.GetWeeksInYear(year);
            for (var week = 1; week <= weeks; week++)
            {
                foreach (var day in new[]
                         {
                             DayOfWeek.Monday,
                             DayOfWeek.Tuesday,
                             DayOfWeek.Wednesday,
                             DayOfWeek.Thursday,
                             DayOfWeek.Friday,
                             DayOfWeek.Saturday,
                             DayOfWeek.Sunday
                         })
                {
                    visited++;
                    if (!SameOutcome(year, week, day))
                    {
                        failures++;
                    }
                }
            }
        }

        await Assert.That(failures).IsEqualTo(0);
        await Assert.That(visited).IsEqualTo(3652061);
    }

    [Test]
    public async Task KnownValues()
    {
        // 2020-01-01 falls in ISO week 1 of 2020, and 2021-01-01 in week 53 of 2020
        await Assert.That(ISOWeek.GetWeekOfYear(new DateOnly(2020, 1, 1))).IsEqualTo(1);
        await Assert.That(ISOWeek.GetYear(new DateOnly(2020, 1, 1))).IsEqualTo(2020);
        await Assert.That(ISOWeek.GetWeekOfYear(new DateOnly(2021, 1, 1))).IsEqualTo(53);
        await Assert.That(ISOWeek.GetYear(new DateOnly(2021, 1, 1))).IsEqualTo(2020);

        await Assert.That(ISOWeek.ToDateOnly(2020, 1, DayOfWeek.Monday)).IsEqualTo(new DateOnly(2019, 12, 30));
        await Assert.That(ISOWeek.ToDateOnly(2020, 53, DayOfWeek.Sunday)).IsEqualTo(new DateOnly(2021, 1, 3));
        await Assert.That(ISOWeek.ToDateOnly(1, 1, DayOfWeek.Monday)).IsEqualTo(new DateOnly(1, 1, 1));
    }

    [Test]
    public async Task ArgumentValidation()
    {
        await Assert.That(Throws(0, 1, DayOfWeek.Monday)).IsEqualTo("ArgumentOutOfRangeException:year");
        await Assert.That(Throws(10000, 1, DayOfWeek.Monday)).IsEqualTo("ArgumentOutOfRangeException:year");
        await Assert.That(Throws(2024, 0, DayOfWeek.Monday)).IsEqualTo("ArgumentOutOfRangeException:week");
        await Assert.That(Throws(2024, 54, DayOfWeek.Monday)).IsEqualTo("ArgumentOutOfRangeException:week");
        await Assert.That(Throws(2024, 1, (DayOfWeek) 9)).IsEqualTo("ArgumentOutOfRangeException:dayOfWeek");

        // week 53 is accepted for a year that has one
        await Assert.That(Throws(2020, 53, DayOfWeek.Monday)).IsEqualTo("none");

        // running off the end of the representable range reports the DateTime arithmetic failure,
        // which is what the BCL does too, since ToDateOnly is built on ToDateTime
        await Assert.That(Throws(9999, 52, DayOfWeek.Sunday)).IsEqualTo("ArgumentOutOfRangeException:value");
    }

    // compares the values rather than formatted strings, since this runs over three and a half
    // million combinations and formatting would dominate
    static bool SameOutcome(int year, int week, DayOfWeek day)
    {
        DateOnly viaDateOnly = default;
        string? dateOnlyError = null;
        try
        {
            viaDateOnly = ISOWeek.ToDateOnly(year, week, day);
        }
        catch (ArgumentException exception)
        {
            dateOnlyError = exception.ParamName;
        }

        DateOnly viaDateTime = default;
        string? dateTimeError = null;
        try
        {
            viaDateTime = DateOnly.FromDateTime(ISOWeek.ToDateTime(year, week, day));
        }
        catch (ArgumentException exception)
        {
            dateTimeError = exception.ParamName;
        }

        if (dateOnlyError != null ||
            dateTimeError != null)
        {
            return dateOnlyError == dateTimeError;
        }

        return viaDateOnly == viaDateTime;
    }

    static string Throws(int year, int week, DayOfWeek day)
    {
        try
        {
            ISOWeek.ToDateOnly(year, week, day);
            return "none";
        }
        catch (ArgumentException exception)
        {
            return $"{exception.GetType().Name}:{exception.ParamName}";
        }
    }
}

#endif
