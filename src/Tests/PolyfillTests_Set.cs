#if NET9_0_OR_GREATER
using System.Collections.ObjectModel;

partial class PolyfillTests
{
    [Test]
    public async Task Set_AsReadOnly()
    {
        ISet<int> set = new HashSet<int> { 1, 2, 3 };
        var readOnly = set.AsReadOnly();
        await Assert.That(readOnly).IsNotNull();
        await Assert.That(readOnly.Count).IsEqualTo(3);
        await Assert.That(readOnly.Contains(1)).IsTrue();
        await Assert.That(readOnly.Contains(4)).IsFalse();
    }
}
#endif
