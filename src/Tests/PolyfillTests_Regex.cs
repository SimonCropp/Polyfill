using System.Text.RegularExpressions;

partial class PolyfillTests
{
    [Test]
    public void RegexIsMatch()
    {
        var regex = new Regex(@"\d+");
        var match = regex.Match("a55a");
        Assert.IsTrue(match.Success);
    }

    [Test]
    public void EnumerateMatches()
    {
        var regex = new Regex(@"\d+");
        var span = "a55a".AsSpan();
        var found = false;
        foreach (var match in regex.EnumerateMatches(span))
        {
            found = true;
            Assert.AreEqual(2,match.Index);
            Assert.AreEqual(2,match.Length);
        }
        Assert.IsTrue(found);
    }
}
