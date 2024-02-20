#if FeatureMemory

[TestFixture]
public class StringInterpolationTests
{
    [Test]
    public void ShouldInterpolateString()
    {
        Span<char> buffer = stackalloc char[] { 'H', 'e', 'l', 'l', 'o' };
        var number = 15;
        var result = $"{buffer}, you're {number} years old";

        Assert.AreEqual("Hello, you're 15 years old", result);
    }

    [Test]
    public void ShouldInterpolateStringBuilder()
    {
        var sb = new StringBuilder();
        Span<char> buffer = stackalloc char[] { 'H', 'e', 'l', 'l', 'o' };
        var number = 15;
        Polyfill.Append(sb, $"{buffer}, you're {number} years old {sb.ToString()}");
        var result = sb.ToString();

        Assert.AreEqual("Hello, you're 15 years old Hello, you're 15 years old ", result);
    }
}
#endif