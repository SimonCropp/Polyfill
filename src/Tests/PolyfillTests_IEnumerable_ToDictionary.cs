using System.Collections.Generic;
using System.Linq;

partial class PolyfillTests
{
    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs()
    {
        var pairs = new[]
        {
            new KeyValuePair<string, int>("a", 1),
            new KeyValuePair<string, int>("b", 2),
        };
        var dict = pairs.ToDictionary();
        await Assert.That(dict["a"]).IsEqualTo(1);
        await Assert.That(dict["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs_WithComparer()
    {
        var pairs = new[]
        {
            new KeyValuePair<string, int>("A", 1),
        };
        var dict = pairs.ToDictionary(StringComparer.OrdinalIgnoreCase);
        await Assert.That(dict["a"]).IsEqualTo(1);
    }

    [Test]
    public async Task IEnumerable_ToDictionary_FromDictionary()
    {
        var source = new Dictionary<string, int> { ["x"] = 10, ["y"] = 20 };
        var dict = source.ToDictionary();
        await Assert.That(dict["x"]).IsEqualTo(10);
        await Assert.That(dict["y"]).IsEqualTo(20);
    }
}
