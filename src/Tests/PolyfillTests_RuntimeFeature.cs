#if NET471_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER

using System.Runtime.CompilerServices;

partial class PolyfillTests
{
    [Test]
    public async Task RuntimeFeatureIsMultithreadingSupported()
    {
        await Assert.That(RuntimeFeature.IsMultithreadingSupported).IsTrue();
    }
}

#endif
