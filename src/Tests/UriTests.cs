namespace Tests;

public class UriTests
{
    [Test]
    public async Task UriSchemeData()
    {
        var scheme = Uri.UriSchemeData;
        await Assert.That(scheme).IsEqualTo("data");
    }
}
