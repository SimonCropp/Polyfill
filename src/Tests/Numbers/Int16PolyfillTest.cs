public class Int16PolyfillTest
{
    [Test]
    public async Task TryParse()
    {
        await Assert.That(short.TryParse("1"u8, null, out var value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1", null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1"u8, NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1"u8, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);
    }

#if FeatureMemory
    [Test]
    public async Task TryParse_Span_Provider_MatchesString()
    {
        var failures = new List<string>();
        var custom = new NumberFormatInfo
        {
            NumberDecimalSeparator = ",",
            NumberGroupSeparator = ".",
            NegativeSign = "~"
        };
        var providers = new IFormatProvider?[] {null, CultureInfo.InvariantCulture, custom};
        string[] inputs =
        [
            "0", "1", "42", "-42", "+42", "~42", " 42 ", "\t42\r\n", "", " ", "abc", "4 2", "42abc", "007",
            "1,234", "1.234", "1.5", "1,5", "1.234,5", "1,234.5", "1e3", "1E-3", "(7)", "$7", "0x1F", "1_000",
            "127", "128", "-128", "-129", "255", "256", "32767", "32768", "-32769", "65535", "65536",
            "2147483647", "2147483648", "-2147483649", "4294967295", "4294967296",
            "9223372036854775807", "9223372036854775808", "18446744073709551615", "18446744073709551616",
            "79228162514264337593543950335", "79228162514264337593543950336",
            "340282366920938463463374607431768211456", "1E400", "-1E400", "NaN", "Infinity", "-Infinity",
            "１２", "1 234"
        ];

        foreach (var provider in providers)
        {
            foreach (var input in inputs)
            {
                var expected = short.TryParse(input, NumberStyles.Integer, provider, out var expectedValue);

                // the span is a slice of a longer string, so reading outside of it would change the result
                var padded = "9" + input + "9";
                var actual = short.TryParse(padded.AsSpan(1, input.Length), provider, out var actualValue);

                if (expected != actual || !expectedValue.Equals(actualValue))
                {
                    failures.Add($"'{input}' with {provider}: expected {expected} {expectedValue} but was {actual} {actualValue}");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryParse_Span_Provider_Empty()
    {
        var result = short.TryParse(ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture, out var value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(default(short));
    }
#endif
}
