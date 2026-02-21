partial class PolyfillTests
{
    [Test]
    public async Task EnumGetValues()
    {
        var dayOfWeeks = Enum.GetValues<DayOfWeek>();
        await Assert.That(dayOfWeeks[0]).IsEqualTo(DayOfWeek.Sunday);
    }

    [Test]
    public async Task EnumGetValuesAsUnderlyingType()
    {
        var values = Enum.GetValuesAsUnderlyingType(typeof(DayOfWeek));
        await Assert.That(values).IsTypeOf<int[]>();
        var intValues = (int[]) values;
        await Assert.That(intValues[0]).IsEqualTo(0);

        var genericValues = Enum.GetValuesAsUnderlyingType<DayOfWeek>();
        await Assert.That(genericValues).IsTypeOf<int[]>();
        var genericIntValues = (int[]) genericValues;
        await Assert.That(genericIntValues[0]).IsEqualTo(0);
    }

    [Test]
    public async Task EnumGetNames()
    {
        var dayOfWeeks = Enum.GetNames<DayOfWeek>();
        await Assert.That(dayOfWeeks[0]).IsEqualTo("Sunday");
    }

    [Test]
    public async Task Parse()
    {
        var dayOfWeek = Enum.Parse<DayOfWeek>("Sunday");
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);

        await Assert.That(() => Enum.Parse<DayOfWeek>("a")).Throws<ArgumentException>();

        dayOfWeek = Enum.Parse<DayOfWeek>("sunday", true);
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);

        await Assert.That(() => Enum.Parse<DayOfWeek>("a", true)).Throws<ArgumentException>();

#if FeatureMemory

        dayOfWeek = Enum.Parse<DayOfWeek>("Sunday".AsSpan());
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);

        await Assert.That(() => Enum.Parse<DayOfWeek>("a".AsSpan())).Throws<ArgumentException>();

        dayOfWeek = Enum.Parse<DayOfWeek>("sunday".AsSpan(), true);
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);

        await Assert.That(() => Enum.Parse<DayOfWeek>("a".AsSpan(), true)).Throws<ArgumentException>();

#endif
    }

#if FeatureMemory

    [Test]
    public async Task TryParse()
    {
        var result = Enum.TryParse<DayOfWeek>("Sunday".AsSpan(), out var dayOfWeek);
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);
        await Assert.That(result).IsTrue();

        result = Enum.TryParse<DayOfWeek>("sunday".AsSpan(), true, out dayOfWeek);
        await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Sunday);
        await Assert.That(result).IsTrue();
    }

    enum Colors
    {
        Red,
        Green,
        Blue
    }

    [Test]
    public Task EnumTryFormat_ValidValue()
    {
        Span<char> buffer = stackalloc char[10];
        var result = Enum.TryFormat(Colors.Green, buffer, out var charsWritten);
        if (!result)
        {
            throw new("Expected result to be true");
        }

        if (buffer[..charsWritten].ToString() != "Green")
        {
            throw new($"Expected 'Green' but got '{buffer[..charsWritten].ToString()}'");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task EnumTryFormat_BufferTooSmall()
    {
        Span<char> buffer = stackalloc char[3];
        var result = Enum.TryFormat(Colors.Blue, buffer, out var charsWritten);
        if (result)
        {
            throw new("Expected result to be false");
        }

        if (charsWritten != 0)
        {
            throw new($"Expected 0 but got {charsWritten}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task EnumTryFormat_WithFormatSpecifier()
    {
        Span<char> buffer = stackalloc char[10];
        var result = Enum.TryFormat(Colors.Red, buffer, out var charsWritten, "G");
        if (!result)
        {
            throw new("Expected result to be true");
        }
        if (buffer[..charsWritten].ToString() != "Red")
        {
            throw new($"Expected 'Red' but got '{buffer[..charsWritten].ToString()}'");
        }
        return Task.CompletedTask;
    }

#endif
}
