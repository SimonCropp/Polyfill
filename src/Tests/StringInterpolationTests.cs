#if FeatureMemory

[TestFixture]
public class StringInterpolationTests
{
    [Test]
    public void ShouldInterpolateString()
    {
        Span<char> buffer = ['H', 'e', 'l', 'l', 'o'];
        var number = 15;
        var result = $"{buffer}, you're {number} years old";

        Assert.AreEqual("Hello, you're 15 years old", result);
    }

    [Test]
    public void ShouldInterpolateStringBuilder()
    {
        var builder = new StringBuilder();
        Span<char> buffer = ['H', 'e', 'l', 'l', 'o'];
        var number = 15;
        Polyfill.Append(builder, $"{buffer}, you're {number} years old {builder.ToString()}");
        var result = builder.ToString();

        Assert.AreEqual("Hello, you're 15 years old Hello, you're 15 years old ", result);
    }
}
#endif