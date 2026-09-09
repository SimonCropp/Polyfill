partial class PolyfillTests
{
    [Test]
    public async Task ConditionalWeakTableRemove()
    {
        var key = "key";
        var table = new ConditionalWeakTable<string, string>();
        table.Add(key, "value");

        await Assert.That(table.Remove(key, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo("value");

        await Assert.That(table.Remove(key, out var missing)).IsFalse();
        await Assert.That(missing).IsNull();
        await Assert.That(table.TryGetValue(key, out _)).IsFalse();
    }
}
