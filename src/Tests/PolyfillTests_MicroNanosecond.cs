partial class PolyfillTests
{
    static DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
    static DateTime dateTime = DateTime.Now;

    [Test]
    public async Task AddMicroseconds()
    {
        var fromTicksDateTimeOffset = dateTimeOffset.AddSeconds(1);
        var fromMicrosecondsDateTimeOffset = dateTimeOffset.AddMicroseconds(1000000);
        await Assert.That(fromMicrosecondsDateTimeOffset).IsEqualTo(fromTicksDateTimeOffset);

        var fromTicksDateTime = dateTime.AddSeconds(1);
        var fromMicrosecondsDateTime = dateTime.AddMicroseconds(1000000);
        await Assert.That(fromMicrosecondsDateTime).IsEqualTo(fromTicksDateTime);
    }

    [Test]
    public async Task AddMicroseconds_SubMillisecond()
    {
        // 1 microsecond == 10 ticks; sub-millisecond precision must be preserved
        // (AddMilliseconds rounds to whole milliseconds on .NET Framework).
        var baseDateTime = new DateTime(2020, 1, 1);
        await Assert.That((baseDateTime.AddMicroseconds(500) - baseDateTime).Ticks).IsEqualTo(5000L);
        await Assert.That((baseDateTime.AddMicroseconds(1) - baseDateTime).Ticks).IsEqualTo(10L);

        var baseOffset = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
        await Assert.That((baseOffset.AddMicroseconds(500) - baseOffset).Ticks).IsEqualTo(5000L);
        await Assert.That((baseOffset.AddMicroseconds(1) - baseOffset).Ticks).IsEqualTo(10L);
    }

    [Test]
    public async Task AddMicroseconds_LargeValue_KeepsPrecision()
    {
        // 2000000000000001 * 10 is not representable as a double, so the multiplication must not be done as a double
        var baseDateTime = new DateTime(0);
        await Assert.That(baseDateTime.AddMicroseconds(2000000000000001d).Ticks).IsEqualTo(20000000000000010L);
        await Assert.That(baseDateTime.AddMicroseconds(2000000000000001.5d).Ticks).IsEqualTo(20000000000000015L);

        var laterDateTime = new DateTime(20000000000000010L);
        await Assert.That(laterDateTime.AddMicroseconds(-2000000000000001d).Ticks).IsEqualTo(0L);

        var baseOffset = new DateTimeOffset(0, TimeSpan.Zero);
        await Assert.That(baseOffset.AddMicroseconds(2000000000000001d).Ticks).IsEqualTo(20000000000000010L);
        await Assert.That(baseOffset.AddMicroseconds(2000000000000001.5d).Ticks).IsEqualTo(20000000000000015L);

        var laterOffset = new DateTimeOffset(20000000000000010L, TimeSpan.Zero);
        await Assert.That(laterOffset.AddMicroseconds(-2000000000000001d).Ticks).IsEqualTo(0L);
    }

    [Test]
    public async Task AddMicroseconds_Fractional()
    {
        var baseDateTime = new DateTime(2020, 1, 1);
        await Assert.That((baseDateTime.AddMicroseconds(1.5) - baseDateTime).Ticks).IsEqualTo(15L);
        await Assert.That((baseDateTime.AddMicroseconds(-1.5) - baseDateTime).Ticks).IsEqualTo(-15L);
        await Assert.That((baseDateTime.AddMicroseconds(0.05) - baseDateTime).Ticks).IsEqualTo(0L);

        var baseOffset = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
        await Assert.That((baseOffset.AddMicroseconds(1.5) - baseOffset).Ticks).IsEqualTo(15L);
        await Assert.That((baseOffset.AddMicroseconds(-1.5) - baseOffset).Ticks).IsEqualTo(-15L);
    }

    [Test]
    public async Task AddMicroseconds_OutOfRange_Throws()
    {
        var baseDateTime = new DateTime(2020, 1, 1);
        await Assert.That(() => baseDateTime.AddMicroseconds(1e30)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => baseDateTime.AddMicroseconds(-1e30)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => baseDateTime.AddMicroseconds(double.PositiveInfinity)).Throws<ArgumentOutOfRangeException>();

        var baseOffset = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
        await Assert.That(() => baseOffset.AddMicroseconds(1e30)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => baseOffset.AddMicroseconds(-1e30)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Microsecond_and_Nanosecond()
    {
        // 1234567 sub-second ticks decompose to 123 ms, 456 µs, 700 ns
        var dateTimeValue = new DateTime(2020, 1, 1, 12, 30, 45).AddTicks(1234567);
        await Assert.That(dateTimeValue.Microsecond).IsEqualTo(456);
        await Assert.That(dateTimeValue.Nanosecond).IsEqualTo(700);

        var dateTimeOffsetValue = new DateTimeOffset(dateTimeValue, TimeSpan.Zero);
        await Assert.That(dateTimeOffsetValue.Microsecond).IsEqualTo(456);
        await Assert.That(dateTimeOffsetValue.Nanosecond).IsEqualTo(700);

        var timeSpanValue = new TimeSpan(1234567);
        await Assert.That(timeSpanValue.Microseconds).IsEqualTo(456);
        await Assert.That(timeSpanValue.Nanoseconds).IsEqualTo(700);
    }

    static IEnumerable<long> SampleTicks(long min, long max)
    {
        yield return min;
        yield return max;
        foreach (var offset in new long[] {1, 9, 10, 11, 99, 100, 9_999, 10_000, 9_999_999, 10_000_000, 599_999_999, 600_000_000, 600_000_001})
        {
            yield return min + offset;
            yield return max - offset;
        }

        // a fixed sequence spread over the whole range
        var range = unchecked((ulong) (max - min));
        var state = 0x9E3779B97F4A7C15UL;
        for (var i = 0; i < 2000; i++)
        {
            state = unchecked(state * 6364136223846793005UL + 1442695040888963407UL);
            yield return unchecked(min + (long) (state % range));
        }
    }

    [Test]
    public async Task DateTime_Microsecond_and_Nanosecond_AllRanges()
    {
        var failures = new List<string>();

        foreach (var kind in new[] {DateTimeKind.Unspecified, DateTimeKind.Utc, DateTimeKind.Local})
        {
            foreach (var ticks in SampleTicks(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks))
            {
                var value = new DateTime(ticks, kind);
                var microsecond = value.Microsecond;
                var nanosecond = value.Nanosecond;

                // what is left over once the whole milliseconds are taken out
                var whole = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, kind);
                var rest = ticks - whole.Ticks;

                if (microsecond != (int) (ticks / 10 % 1000) ||
                    nanosecond != (int) (ticks % 10) * 100 ||
                    microsecond * 10 + nanosecond / 100 != rest)
                {
                    failures.Add($"{kind} {ticks}: {microsecond} {nanosecond}");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task DateTimeOffset_Microsecond_and_Nanosecond_AllRanges()
    {
        var failures = new List<string>();
        var offsets = new[]
        {
            TimeSpan.Zero,
            TimeSpan.FromHours(14),
            TimeSpan.FromHours(-14),
            new TimeSpan(5, 30, 0),
            new TimeSpan(5, 45, 0),
            new TimeSpan(-3, -30, 0),
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(-1)
        };

        foreach (var offset in offsets)
        {
            // the range of clock times for which the UTC time is still valid
            var min = Math.Max(DateTime.MinValue.Ticks, DateTime.MinValue.Ticks + offset.Ticks);
            var max = Math.Min(DateTime.MaxValue.Ticks, DateTime.MaxValue.Ticks + offset.Ticks);

            foreach (var ticks in SampleTicks(min, max))
            {
                var value = new DateTimeOffset(ticks, offset);
                var microsecond = value.Microsecond;
                var nanosecond = value.Nanosecond;

                var whole = new DateTimeOffset(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, offset);
                var rest = ticks - whole.Ticks;

                if (microsecond != (int) (ticks / 10 % 1000) ||
                    nanosecond != (int) (ticks % 10) * 100 ||
                    microsecond * 10 + nanosecond / 100 != rest ||
                    microsecond != value.UtcDateTime.Microsecond ||
                    nanosecond != value.UtcDateTime.Nanosecond)
                {
                    failures.Add($"{offset} {ticks}: {microsecond} {nanosecond}");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TimeSpan_Microseconds_and_Nanoseconds_AllRanges()
    {
        var failures = new List<string>();
        var samples = SampleTicks(TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks)
            .Concat(SampleTicks(-700_000_000, 700_000_000))
            .Concat([0, 1, -1, 9, -9, 10, -10, 1234567, -1234567]);

        foreach (var ticks in samples)
        {
            var value = new TimeSpan(ticks);
            var microseconds = value.Microseconds;
            var nanoseconds = value.Nanoseconds;

            // the components add back up to the value, each carrying the sign of the value
            var sum =
                value.Days * TimeSpan.TicksPerDay +
                value.Hours * TimeSpan.TicksPerHour +
                value.Minutes * TimeSpan.TicksPerMinute +
                value.Seconds * TimeSpan.TicksPerSecond +
                value.Milliseconds * TimeSpan.TicksPerMillisecond +
                microseconds * 10L +
                nanoseconds / 100;

            if (microseconds != (int) (ticks / 10 % 1000) ||
                nanoseconds != (int) (ticks % 10) * 100 ||
                sum != ticks ||
                (ticks >= 0 && (microseconds < 0 || nanoseconds < 0)) ||
                (ticks <= 0 && (microseconds > 0 || nanoseconds > 0)))
            {
                failures.Add($"{ticks}: {microseconds} {nanoseconds}");
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TimeSpan_Microseconds_and_Nanoseconds_Limits()
    {
        // -9223372036854775808 and 9223372036854775807 ticks
        await Assert.That(TimeSpan.MinValue.Microseconds).IsEqualTo(-580);
        await Assert.That(TimeSpan.MinValue.Nanoseconds).IsEqualTo(-800);
        await Assert.That(TimeSpan.MaxValue.Microseconds).IsEqualTo(580);
        await Assert.That(TimeSpan.MaxValue.Nanoseconds).IsEqualTo(700);

        var negative = new TimeSpan(-1234567);
        await Assert.That(negative.Microseconds).IsEqualTo(-456);
        await Assert.That(negative.Nanoseconds).IsEqualTo(-700);

        await Assert.That(DateTime.MinValue.Microsecond).IsEqualTo(0);
        await Assert.That(DateTime.MinValue.Nanosecond).IsEqualTo(0);
        await Assert.That(DateTime.MaxValue.Microsecond).IsEqualTo(999);
        await Assert.That(DateTime.MaxValue.Nanosecond).IsEqualTo(900);
        await Assert.That(DateTimeOffset.MinValue.Microsecond).IsEqualTo(0);
        await Assert.That(DateTimeOffset.MinValue.Nanosecond).IsEqualTo(0);
        await Assert.That(DateTimeOffset.MaxValue.Microsecond).IsEqualTo(999);
        await Assert.That(DateTimeOffset.MaxValue.Nanosecond).IsEqualTo(900);
    }
}