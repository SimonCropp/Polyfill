public class TimeSpanPolyfillTests
{
    [Test]
    public async Task From_HappyPath_And_Overflow()
    {
        await Assert.That(TimeSpan.FromMicroseconds(1000000).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);
        await Assert.That(TimeSpan.FromDays(1).Ticks).IsEqualTo(TimeSpan.TicksPerDay);
        await Assert.That(TimeSpan.FromSeconds(1, 0, 0).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);

        // Components that cancel out must stay in range. Accumulation happens in a wide (decimal)
        // space, so even when an individual component product overflows Int64, a cancelling
        // component keeps the result in range (matching the BCL's 128-bit accumulation).
        await Assert.That(TimeSpan.FromSeconds(1000000000000, -1000000000000000, 0).Ticks).IsEqualTo(0L);
        await Assert.That(TimeSpan.FromSeconds(0, -9498038399810654, long.MaxValue).Ticks).IsEqualTo(-2746663629558781930L);

        // The multi-argument overloads reach the integer-based factory on every framework
        // (the single-argument FromMicroseconds(double) BCL overload shadows the polyfill on net7/8).
        await Assert.That(() => TimeSpan.FromDays(int.MaxValue, 0, 0, 0, 0, 0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromSeconds(long.MaxValue, 0, 0)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Microseconds_ReturnsCorrectValue()
    {
        var ts = new TimeSpan(1, 2, 3, 4, 5);
        var microseconds = ts.Microseconds;
        await Assert.That(microseconds).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task Nanoseconds_ReturnsCorrectValue()
    {
        var ts = new TimeSpan(1, 2, 3, 4, 5);
        var nanoseconds = ts.Nanoseconds;
        await Assert.That(nanoseconds).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task Microseconds_Zero_TimeSpan()
    {
        var ts = TimeSpan.Zero;
        await Assert.That(ts.Microseconds).IsEqualTo(0);
        await Assert.That(ts.Nanoseconds).IsEqualTo(0);
    }

    [Test]
    public async Task FromDays_Int()
    {
        var ts = TimeSpan.FromDays(2);
        await Assert.That(ts.TotalDays).IsEqualTo(2.0);
    }

    [Test]
    public async Task FromDays_WithHours()
    {
        var ts = TimeSpan.FromDays(1, 12);
        await Assert.That(ts.TotalHours).IsEqualTo(36.0);
    }

    [Test]
    public async Task FromDays_LongComponents()
    {
        var ts = TimeSpan.FromDays(1, 2, 3L, 4L, 5L, 6L);
        await Assert.That(ts.Ticks).IsEqualTo(new TimeSpan(1, 2, 3, 4, 5).Ticks + 60);

        // values that do not fit in an Int32
        ts = TimeSpan.FromDays(0, 0, 0L, 3_000_000_000L);
        await Assert.That(ts.Ticks).IsEqualTo(3_000_000_000L * TimeSpan.TicksPerSecond);
        ts = TimeSpan.FromDays(0, 0, 0L, 0L, 0L, 5_000_000_000L);
        await Assert.That(ts.Ticks).IsEqualTo(5_000L * TimeSpan.TicksPerSecond);
    }

    [Test]
    public async Task FromHours_LongComponents()
    {
        var ts = TimeSpan.FromHours(1, 2L, 3L, 4L, 5L);
        await Assert.That(ts.Ticks).IsEqualTo(new TimeSpan(0, 1, 2, 3, 4).Ticks + 50);

        // values that do not fit in an Int32
        ts = TimeSpan.FromHours(0, 3_000_000_000L);
        await Assert.That(ts.Ticks).IsEqualTo(3_000_000_000L * TimeSpan.TicksPerMinute);
        ts = TimeSpan.FromHours(0, 0L, 0L, 5_000_000_000L);
        await Assert.That(ts.Ticks).IsEqualTo(5_000_000L * TimeSpan.TicksPerSecond);
    }

    [Test]
    public async Task FromMinutes_LongComponents()
    {
        var ts = TimeSpan.FromMinutes(1L, 2L, 3L, 4L);
        await Assert.That(ts.Ticks).IsEqualTo(new TimeSpan(0, 0, 1, 2, 3).Ticks + 40);

        // values that do not fit in an Int32
        ts = TimeSpan.FromMinutes(3_000_000_000L, 0L);
        await Assert.That(ts.Ticks).IsEqualTo(3_000_000_000L * TimeSpan.TicksPerMinute);
        ts = TimeSpan.FromMinutes(0L, 3_000_000_000L);
        await Assert.That(ts.Ticks).IsEqualTo(3_000_000_000L * TimeSpan.TicksPerSecond);
    }

    [Test]
    public async Task FromHours_Int()
    {
        var ts = TimeSpan.FromHours(5);
        await Assert.That(ts.TotalHours).IsEqualTo(5.0);
    }

    [Test]
    public async Task FromMinutes_Int()
    {
        var ts = TimeSpan.FromMinutes(90);
        await Assert.That(ts.TotalMinutes).IsEqualTo(90.0);
    }

    [Test]
    public async Task FromSeconds_Long()
    {
        var ts = TimeSpan.FromSeconds(120L);
        await Assert.That(ts.TotalSeconds).IsEqualTo(120.0);
    }

    [Test]
    public async Task FromSeconds_WithMilliseconds()
    {
        var ts = TimeSpan.FromSeconds(1L, 500L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(1500.0);
    }

    [Test]
    public async Task FromMilliseconds_Long()
    {
        var ts = TimeSpan.FromMilliseconds(2500L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(2500.0);
    }

    [Test]
    public async Task FromMicroseconds_Long()
    {
        var ts = TimeSpan.FromMicroseconds(1000L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(1.0);
    }

    [Test]
    public async Task UnitConstants()
    {
        await Assert.That(TimeSpan.HoursPerDay).IsEqualTo(24);

        await Assert.That(TimeSpan.MicrosecondsPerMillisecond).IsEqualTo(1_000L);
        await Assert.That(TimeSpan.MicrosecondsPerSecond).IsEqualTo(1_000_000L);
        await Assert.That(TimeSpan.MicrosecondsPerMinute).IsEqualTo(60_000_000L);
        await Assert.That(TimeSpan.MicrosecondsPerHour).IsEqualTo(3_600_000_000L);
        await Assert.That(TimeSpan.MicrosecondsPerDay).IsEqualTo(86_400_000_000L);

        await Assert.That(TimeSpan.MillisecondsPerSecond).IsEqualTo(1_000L);
        await Assert.That(TimeSpan.MillisecondsPerMinute).IsEqualTo(60_000L);
        await Assert.That(TimeSpan.MillisecondsPerHour).IsEqualTo(3_600_000L);
        await Assert.That(TimeSpan.MillisecondsPerDay).IsEqualTo(86_400_000L);

        await Assert.That(TimeSpan.SecondsPerMinute).IsEqualTo(60L);
        await Assert.That(TimeSpan.SecondsPerHour).IsEqualTo(3_600L);
        await Assert.That(TimeSpan.SecondsPerDay).IsEqualTo(86_400L);

        await Assert.That(TimeSpan.MinutesPerHour).IsEqualTo(60L);
        await Assert.That(TimeSpan.MinutesPerDay).IsEqualTo(1_440L);
    }

    // cross-check against the TicksPer* constants, which have been in the BCL since forever
    [Test]
    public async Task UnitConstants_AgreeWithTicksPer()
    {
        // TicksPerMicrosecond is itself net7 only, so compare through milliseconds instead
        await Assert.That(TimeSpan.MicrosecondsPerSecond).IsEqualTo(TimeSpan.MicrosecondsPerMillisecond * TimeSpan.MillisecondsPerSecond);
        await Assert.That(TimeSpan.MillisecondsPerSecond * TimeSpan.TicksPerMillisecond).IsEqualTo(TimeSpan.TicksPerSecond);
        await Assert.That(TimeSpan.SecondsPerMinute * TimeSpan.TicksPerSecond).IsEqualTo(TimeSpan.TicksPerMinute);
        await Assert.That(TimeSpan.MinutesPerHour * TimeSpan.TicksPerMinute).IsEqualTo(TimeSpan.TicksPerHour);
        await Assert.That(TimeSpan.HoursPerDay * TimeSpan.TicksPerHour).IsEqualTo(TimeSpan.TicksPerDay);
        await Assert.That(TimeSpan.SecondsPerDay).IsEqualTo(TimeSpan.SecondsPerHour * TimeSpan.HoursPerDay);
        await Assert.That(TimeSpan.MillisecondsPerDay).IsEqualTo(TimeSpan.MillisecondsPerHour * TimeSpan.HoursPerDay);
        await Assert.That(TimeSpan.MicrosecondsPerDay).IsEqualTo(TimeSpan.MicrosecondsPerHour * TimeSpan.HoursPerDay);
    }

    // What the From* factories are specified to do: sum every component exactly, then throw
    // ArgumentOutOfRangeException when the total does not fit in the tick range.
    static long? ExpectedTicks(long days, long hours, long minutes, long seconds, long milliseconds, long microseconds)
    {
        var ticks =
            (days * 86_400_000_000m +
             hours * 3_600_000_000m +
             minutes * 60_000_000m +
             seconds * 1_000_000m +
             milliseconds * 1_000m +
             microseconds) * 10m;
        if (ticks < long.MinValue || ticks > long.MaxValue)
        {
            return null;
        }

        return (long) ticks;
    }

    // The ticks a factory call produced, or null when it threw ArgumentOutOfRangeException.
    // Any other exception propagates and fails the test.
    static long? ActualTicks(Func<TimeSpan> factory)
    {
        try
        {
            return factory().Ticks;
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    // Runs every multi-component factory that can take these components and returns a description
    // of the first one that disagrees with the specification.
    static string? CompareFactories(int days, int hours, long minutes, long seconds, long milliseconds, long microseconds)
    {
        var expected = ExpectedTicks(days, hours, minutes, seconds, milliseconds, microseconds);
        var actual = ActualTicks(() => TimeSpan.FromDays(days, hours, minutes, seconds, milliseconds, microseconds));
        if (actual != expected)
        {
            return $"FromDays({days}, {hours}, {minutes}, {seconds}, {milliseconds}, {microseconds}): {Describe(actual)} rather than {Describe(expected)}";
        }

        expected = ExpectedTicks(0, hours, minutes, seconds, milliseconds, microseconds);
        actual = ActualTicks(() => TimeSpan.FromHours(hours, minutes, seconds, milliseconds, microseconds));
        if (actual != expected)
        {
            return $"FromHours({hours}, {minutes}, {seconds}, {milliseconds}, {microseconds}): {Describe(actual)} rather than {Describe(expected)}";
        }

        expected = ExpectedTicks(0, 0, minutes, seconds, milliseconds, microseconds);
        actual = ActualTicks(() => TimeSpan.FromMinutes(minutes, seconds, milliseconds, microseconds));
        if (actual != expected)
        {
            return $"FromMinutes({minutes}, {seconds}, {milliseconds}, {microseconds}): {Describe(actual)} rather than {Describe(expected)}";
        }

        expected = ExpectedTicks(0, 0, 0, seconds, milliseconds, microseconds);
        actual = ActualTicks(() => TimeSpan.FromSeconds(seconds, milliseconds, microseconds));
        if (actual != expected)
        {
            return $"FromSeconds({seconds}, {milliseconds}, {microseconds}): {Describe(actual)} rather than {Describe(expected)}";
        }

        expected = ExpectedTicks(0, 0, 0, 0, milliseconds, microseconds);
        actual = ActualTicks(() => TimeSpan.FromMilliseconds(milliseconds, microseconds));
        if (actual != expected)
        {
            return $"FromMilliseconds({milliseconds}, {microseconds}): {Describe(actual)} rather than {Describe(expected)}";
        }

        return null;

        static string Describe(long? ticks)
        {
            if (ticks == null)
            {
                return "ArgumentOutOfRangeException";
            }

            return ticks + " ticks";
        }
    }

    [Test]
    [Arguments(0, 0, 0L, 0L, 0L, 0L)]
    [Arguments(1, 2, 3L, 4L, 5L, 6L)]
    [Arguments(-1, -2, -3L, -4L, -5L, -6L)]
    [Arguments(1, -23, 59L, -59L, 999L, -999L)]
    [Arguments(0, 0, 0L, 0L, 0L, 1L)]
    [Arguments(0, 0, 0L, 0L, 0L, -1L)]
    [Arguments(10_675_199, 2, 48L, 5L, 477L, 580L)]
    [Arguments(10_675_199, 2, 48L, 5L, 477L, 581L)]
    [Arguments(-10_675_199, -2, -48L, -5L, -477L, -580L)]
    [Arguments(-10_675_199, -2, -48L, -5L, -477L, -581L)]
    [Arguments(10_675_200, 0, 0L, 0L, 0L, 0L)]
    [Arguments(-10_675_200, 0, 0L, 0L, 0L, 0L)]
    [Arguments(int.MaxValue, int.MaxValue, long.MaxValue, long.MaxValue, long.MaxValue, long.MaxValue)]
    [Arguments(int.MinValue, int.MinValue, long.MinValue, long.MinValue, long.MinValue, long.MinValue)]
    [Arguments(int.MaxValue, int.MinValue, long.MaxValue, long.MinValue, long.MaxValue, long.MinValue)]
    [Arguments(0, 0, 0L, 0L, 0L, long.MaxValue)]
    [Arguments(0, 0, 0L, 0L, 0L, long.MinValue)]
    [Arguments(0, 0, 0L, 0L, long.MaxValue, long.MinValue)]
    [Arguments(0, 0, long.MaxValue, long.MinValue, 0L, 0L)]
    public async Task From_Components_MatchSpecification(int days, int hours, long minutes, long seconds, long milliseconds, long microseconds) =>
        await Assert.That(CompareFactories(days, hours, minutes, seconds, milliseconds, microseconds)).IsNull();

    // The largest and smallest totals that fit: +/-922337203685477580 microseconds
    [Test]
    public async Task From_Components_TickRangeBoundaries()
    {
        const long maxMicroseconds = long.MaxValue / 10;

        await Assert.That(TimeSpan.FromMilliseconds(0, maxMicroseconds).Ticks).IsEqualTo(9223372036854775800L);
        await Assert.That(TimeSpan.FromMilliseconds(0, -maxMicroseconds).Ticks).IsEqualTo(-9223372036854775800L);
        await Assert.That(() => TimeSpan.FromMilliseconds(0, maxMicroseconds + 1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromMilliseconds(0, -maxMicroseconds - 1)).Throws<ArgumentOutOfRangeException>();

        // reached through every unit at once
        await Assert.That(TimeSpan.FromDays(10_675_199, 2, 48, 5, 477, 580).Ticks).IsEqualTo(9223372036854775800L);
        await Assert.That(() => TimeSpan.FromDays(10_675_199, 2, 48, 5, 477, 581)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(TimeSpan.FromDays(-10_675_199, -2, -48, -5, -477, -580).Ticks).IsEqualTo(-9223372036854775800L);
        await Assert.That(() => TimeSpan.FromDays(-10_675_199, -2, -48, -5, -477, -581)).Throws<ArgumentOutOfRangeException>();

        // components far out of range that cancel back into range
        await Assert.That(TimeSpan.FromMinutes(150_000_000_000_000, -9_000_000_000_000_000 + 5, 0, 0).Ticks).IsEqualTo(5 * TimeSpan.TicksPerSecond);
        await Assert.That(TimeSpan.FromSeconds(long.MaxValue / 1000, -(long.MaxValue / 1000) * 1000, 7).Ticks).IsEqualTo(70L);
        await Assert.That(TimeSpan.FromMilliseconds(long.MaxValue / 1000, -(long.MaxValue / 1000) * 1000 - 3).Ticks).IsEqualTo(-30L);

        // components that are each in range but overflow together
        await Assert.That(() => TimeSpan.FromDays(6_000_000, 0, 0, 6_000_000L * 86_400)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromSeconds(long.MaxValue / 10_000_000, long.MaxValue / 10_000, 0)).Throws<ArgumentOutOfRangeException>();
    }

    // Every component on its own, around each power of two and each power of ten, so that whatever
    // magnitude an implementation switches arithmetic at is covered from both sides.
    [Test]
    public async Task From_Components_SingleComponentMagnitudes()
    {
        var magnitudes = new List<long> {0, 1, long.MaxValue};
        for (var shift = 1; shift < 63; shift++)
        {
            magnitudes.Add((1L << shift) - 1);
            magnitudes.Add(1L << shift);
            magnitudes.Add((1L << shift) + 1);
        }

        var power = 1L;
        for (var exponent = 1; exponent < 19; exponent++)
        {
            power *= 10;
            magnitudes.Add(power - 1);
            magnitudes.Add(power);
            magnitudes.Add(power + 1);
        }

        // the limits of each unit: the last value that fits in the tick range, and its neighbours
        foreach (var ticksPerUnit in new[] {10L, TimeSpan.TicksPerMillisecond, TimeSpan.TicksPerSecond, TimeSpan.TicksPerMinute, TimeSpan.TicksPerHour, TimeSpan.TicksPerDay})
        {
            for (var divisor = 1; divisor <= 16; divisor++)
            {
                var limit = long.MaxValue / divisor / ticksPerUnit;
                magnitudes.Add(limit - 1);
                magnitudes.Add(limit);
                magnitudes.Add(limit + 1);
            }
        }

        var values = new HashSet<long> {long.MinValue};
        foreach (var magnitude in magnitudes)
        {
            values.Add(magnitude);
            values.Add(-magnitude);
        }

        var failures = new List<string>();
        foreach (var value in values)
        {
            Check(CompareFactories(0, 0, 0, 0, 0, value));
            Check(CompareFactories(0, 0, 0, 0, value, 0));
            Check(CompareFactories(0, 0, 0, value, 0, 0));
            Check(CompareFactories(0, 0, value, 0, 0, 0));
            if (value is >= int.MinValue and <= int.MaxValue)
            {
                Check(CompareFactories(0, (int) value, 0, 0, 0, 0));
                Check(CompareFactories((int) value, 0, 0, 0, 0, 0));
            }
        }

        await Assert.That(string.Join(Environment.NewLine, failures)).IsEqualTo(string.Empty);

        void Check(string? failure)
        {
            if (failure != null && failures.Count < 20)
            {
                failures.Add(failure);
            }
        }
    }

    [Test]
    public async Task From_Components_Random_MatchSpecification()
    {
        var random = new Random(20240917);
        var failures = new List<string>();
        for (var index = 0; index < 5_000; index++)
        {
            var failure = CompareFactories(
                (int) Next(31),
                (int) Next(31),
                Next(63),
                Next(63),
                Next(63),
                Next(63));
            if (failure != null && failures.Count < 20)
            {
                failures.Add(failure);
            }
        }

        await Assert.That(string.Join(Environment.NewLine, failures)).IsEqualTo(string.Empty);

        // A signed value with a random number of significant bits, so that small, medium and
        // overflowing magnitudes are all common. One call in four returns zero.
        long Next(int maxBits)
        {
            if (random.Next(4) == 0)
            {
                return 0;
            }

            var bits = random.Next(1, maxBits + 1);
            var buffer = new byte[8];
            random.NextBytes(buffer);
            var value = (long) (BitConverter.ToUInt64(buffer, 0) >> (64 - bits));
            if (random.Next(2) == 0)
            {
                return -value;
            }

            return value;
        }
    }

    [Test]
    public async Task FromMicroseconds_Long_MatchesSpecification()
    {
        // Below net9 a long argument binds to the BCL's FromMicroseconds(double) where that exists
        // (net7 and net8), which rounds. Values a double holds exactly behave the same everywhere.
        foreach (var value in new[] {0L, 1L, -1L, 999L, 1_000_000L, -86_400_000_000L, 1L << 40, -(1L << 52)})
        {
            await Assert.That(TimeSpan.FromMicroseconds(value).Ticks).IsEqualTo(value * 10);
        }

        // 2^53 + 1 is not representable as a double, so this tells the exact overload apart
        var exact = TimeSpan.FromMicroseconds(9_007_199_254_740_993L).Ticks == 90_071_992_547_409_930L;
        if (!exact)
        {
            return;
        }

        const long maxMicroseconds = long.MaxValue / 10;
        foreach (var divisor in new long[] {1, 2, 4, 7, 8, 9, 16})
        {
            foreach (var delta in new long[] {-1, 0, 1})
            {
                var value = maxMicroseconds / divisor + delta;
                await Assert.That(ActualTicks(() => TimeSpan.FromMicroseconds(value))).IsEqualTo(ExpectedTicks(0, 0, 0, 0, 0, value));
                await Assert.That(ActualTicks(() => TimeSpan.FromMicroseconds(-value))).IsEqualTo(ExpectedTicks(0, 0, 0, 0, 0, -value));
            }
        }

        await Assert.That(() => TimeSpan.FromMicroseconds(long.MaxValue)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromMicroseconds(long.MinValue)).Throws<ArgumentOutOfRangeException>();
    }
}
