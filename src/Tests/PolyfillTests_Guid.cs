using System.Text.RegularExpressions;

partial class PolyfillTests
{
    static Regex guidV7Regex = new("^[0-9a-f]{8}(?:\\-[0-9a-f]{4}){3}-[0-9a-f]{12}$");

    [Test]
    public void GuidCreate7()
    {
        var guid = GuidPolyfill.CreateVersion7();
        Assert.IsTrue(guidV7Regex.IsMatch(guid.ToString()));
    }
}