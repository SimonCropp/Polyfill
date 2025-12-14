#if FeatureMemory

public class StringInterpolationTests
{
    [Test]
    public async Task ShouldInterpolateString()
    {
        Span<char> buffer = ['H', 'e', 'l', 'l', 'o'];
        var number = 15;
        var result = $"{buffer}, you're {number} years old";

        await Assert.That(result).IsEqualTo("Hello, you're 15 years old");
    }

    [Test]
    public async Task ShouldInterpolateStringBuilder()
    {
        var builder = new StringBuilder();
        Span<char> buffer = ['H', 'e', 'l', 'l', 'o'];
        var number = 15;
        Polyfill.Append(builder, $"{buffer}, you're {number} years old {builder.ToString()}");
        var result = builder.ToString();

        await Assert.That(result).IsEqualTo("Hello, you're 15 years old Hello, you're 15 years old ");
    }
}
#endif