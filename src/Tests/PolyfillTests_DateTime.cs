partial class PolyfillTests
{
    [Test]
    public async Task DateTime_Deconstruct_YearMonthDay()
    {
        var dt = new DateTime(2024, 3, 15);
        var (year, month, day) = dt;
        await Assert.That(year).IsEqualTo(2024);
        await Assert.That(month).IsEqualTo(3);
        await Assert.That(day).IsEqualTo(15);
    }

#if NET6_0_OR_GREATER
    [Test]
    public async Task DateTime_Deconstruct_DateAndTime()
    {
        var dt = new DateTime(2024, 3, 15, 10, 30, 45);
        var (date, time) = dt;
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 3, 15));
        await Assert.That(time).IsEqualTo(new TimeOnly(10, 30, 45));
    }
#endif

    [Test]
    public async Task DateTime_TryParse_StringWithProvider()
    {
        var parsed = DateTime.TryParse("2024-03-15", CultureInfo.InvariantCulture, out var result);
        var invalid = DateTime.TryParse("nope", CultureInfo.InvariantCulture, out _);

        await Assert.That(parsed).IsTrue();
        await Assert.That(result).IsEqualTo(new DateTime(2024, 3, 15));
        await Assert.That(invalid).IsFalse();
    }

    [Test]
    public async Task DateTimeOffset_TryParse_StringWithProvider()
    {
        var parsed = DateTimeOffset.TryParse("2024-03-15T10:30:00+05:00", CultureInfo.InvariantCulture, out var result);
        var invalid = DateTimeOffset.TryParse("nope", CultureInfo.InvariantCulture, out _);

        await Assert.That(parsed).IsTrue();
        await Assert.That(result).IsEqualTo(new DateTimeOffset(2024, 3, 15, 10, 30, 0, TimeSpan.FromHours(5)));
        await Assert.That(invalid).IsFalse();
    }

#if FeatureMemory
    [Test]
    public async Task DateTime_TryParse_Span()
    {
        var expected = new DateTime(2024, 3, 15);
        var invariant = CultureInfo.InvariantCulture;

        var span = DateTime.TryParse("2024-03-15".AsSpan(), out var spanResult);
        var provider = DateTime.TryParse("2024-03-15".AsSpan(), invariant, out var providerResult);
        var styles = DateTime.TryParse("2024-03-15".AsSpan(), invariant, DateTimeStyles.None, out var stylesResult);
        var exact = DateTime.TryParseExact("15/03/2024".AsSpan(), "dd/MM/yyyy".AsSpan(), invariant, DateTimeStyles.None, out var exactResult);
        var exactStringFormat = DateTime.TryParseExact("15/03/2024".AsSpan(), "dd/MM/yyyy", invariant, DateTimeStyles.None, out var exactStringFormatResult);
        var invalid = DateTime.TryParse("nope".AsSpan(), out _);
        var invalidExact = DateTime.TryParseExact("2024-03-15".AsSpan(), "dd/MM/yyyy".AsSpan(), invariant, DateTimeStyles.None, out _);

        await Assert.That(span).IsTrue();
        await Assert.That(spanResult).IsEqualTo(expected);
        await Assert.That(provider).IsTrue();
        await Assert.That(providerResult).IsEqualTo(expected);
        await Assert.That(styles).IsTrue();
        await Assert.That(stylesResult).IsEqualTo(expected);
        await Assert.That(exact).IsTrue();
        await Assert.That(exactResult).IsEqualTo(expected);
        await Assert.That(exactStringFormat).IsTrue();
        await Assert.That(exactStringFormatResult).IsEqualTo(expected);
        await Assert.That(invalid).IsFalse();
        await Assert.That(invalidExact).IsFalse();
    }

    [Test]
    public async Task DateTimeOffset_TryParse_Span()
    {
        var expected = new DateTimeOffset(2024, 3, 15, 10, 30, 0, TimeSpan.FromHours(5));
        var invariant = CultureInfo.InvariantCulture;
        const string format = "dd/MM/yyyy HH:mm zzz";

        var span = DateTimeOffset.TryParse("2024-03-15T10:30:00+05:00".AsSpan(), out var spanResult);
        var provider = DateTimeOffset.TryParse("2024-03-15T10:30:00+05:00".AsSpan(), invariant, out var providerResult);
        var styles = DateTimeOffset.TryParse("2024-03-15T10:30:00+05:00".AsSpan(), invariant, DateTimeStyles.None, out var stylesResult);
        var exact = DateTimeOffset.TryParseExact("15/03/2024 10:30 +05:00".AsSpan(), format.AsSpan(), invariant, DateTimeStyles.None, out var exactResult);
        var exactStringFormat = DateTimeOffset.TryParseExact("15/03/2024 10:30 +05:00".AsSpan(), format, invariant, DateTimeStyles.None, out var exactStringFormatResult);
        var invalid = DateTimeOffset.TryParse("nope".AsSpan(), out _);
        var invalidExact = DateTimeOffset.TryParseExact("2024-03-15".AsSpan(), format.AsSpan(), invariant, DateTimeStyles.None, out _);

        await Assert.That(span).IsTrue();
        await Assert.That(spanResult).IsEqualTo(expected);
        await Assert.That(provider).IsTrue();
        await Assert.That(providerResult).IsEqualTo(expected);
        await Assert.That(styles).IsTrue();
        await Assert.That(stylesResult).IsEqualTo(expected);
        await Assert.That(exact).IsTrue();
        await Assert.That(exactResult).IsEqualTo(expected);
        await Assert.That(exactStringFormat).IsTrue();
        await Assert.That(exactStringFormatResult).IsEqualTo(expected);
        await Assert.That(invalid).IsFalse();
        await Assert.That(invalidExact).IsFalse();
    }
#endif

    // Where the BCL declares a TryParse/TryParseExact overload the polyfill must not be compiled:
    // a static member of the type always wins over a static extension member, so the polyfill
    // would be dead code.
    [Test]
    public async Task DateTime_TryParse_NotPolyfilledWhereBclHasIt()
    {
        var duplicates = typeof(Polyfills.Polyfill)
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(_ => _.Name is "TryParse" or "TryParseExact")
            .Where(_ =>
            {
                var parameters = _.GetParameters().Select(_ => _.ParameterType).ToArray();
                var type = parameters[parameters.Length - 1].GetElementType();
                if (type != typeof(DateTime) && type != typeof(DateTimeOffset))
                {
                    return false;
                }

                return type.GetMethod(_.Name, BindingFlags.Public | BindingFlags.Static, null, parameters, null) != null;
            })
            .Select(_ => _.ToString())
            .ToList();

        await Assert.That(duplicates).IsEmpty();
    }
}
