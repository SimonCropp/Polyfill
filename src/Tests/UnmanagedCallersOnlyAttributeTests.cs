// AssociatedSourceType is on Polyfill's recreated attribute (below net5) and on the BCL's from net11. The BCL attribute
// on net5 to net10 has no such field, so this can't compile there.
#if !NET || NET11_0_OR_GREATER

using System.Reflection;

public class UnmanagedCallersOnlyAttributeTests
{
    [UnmanagedCallersOnly(AssociatedSourceType = typeof(UnmanagedCallersOnlyAttributeTests))]
    static void Callback()
    {
    }

    [Test]
    public async Task AssociatedSourceType_RoundTrips()
    {
        var method = typeof(UnmanagedCallersOnlyAttributeTests).GetMethod(nameof(Callback), BindingFlags.NonPublic | BindingFlags.Static)!;
        var attribute = method.GetCustomAttribute<UnmanagedCallersOnlyAttribute>()!;
        await Assert.That(attribute.AssociatedSourceType).IsEqualTo(typeof(UnmanagedCallersOnlyAttributeTests));
    }
}

#endif
