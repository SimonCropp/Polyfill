partial class PolyfillTests
{
    static Regex guidV7Regex = new("^[0-9a-f]{8}(?:\\-[0-9a-f]{4}){3}-[0-9a-f]{12}$");

    [Test]
    public async Task GuidCreate7()
    {
        var guid = Guid.CreateVersion7();
        await Assert.That(guidV7Regex.IsMatch(guid.ToString())).IsTrue();
    }
}