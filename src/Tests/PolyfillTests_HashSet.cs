partial class PolyfillTests
{
    [Test]
    public async Task TryGetValue()
    {
        var value = "value";
        var set = new HashSet<string>
        {
            value
        };
        var found = set.TryGetValue("value", out var result);
        await Assert.That(found).IsTrue();
        await Assert.That(result!).IsEqualTo(value);
        found = set.TryGetValue("value2", out result);
        await Assert.That(result).IsNull();
        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task HashSet_EnsureCapacity()
    {
        var set = new HashSet<int>();
        set.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task HashSet_TrimExcess()
    {
        var set = new HashSet<int> { 1, 2, 3 };
        set.TrimExcess(100);
        // Should not throw
        await Assert.That(set.Count).IsEqualTo(3);
    }
}