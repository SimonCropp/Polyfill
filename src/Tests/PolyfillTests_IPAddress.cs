#if FeatureMemory

using System.Net;
using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task IPAddressParseCharSpan()
    {
        var address = IPAddress.Parse("127.0.0.1".AsSpan());
        await Assert.That(address).IsEqualTo(IPAddress.Loopback);
    }

    [Test]
    public async Task IPAddressTryParseCharSpan()
    {
        var result = IPAddress.TryParse("127.0.0.1".AsSpan(), out var address);
        await Assert.That(result).IsTrue();
        await Assert.That(address).IsEqualTo(IPAddress.Loopback);
    }

    [Test]
    public async Task IPAddressTryParseCharSpan_Invalid()
    {
        var result = IPAddress.TryParse("not-an-ip".AsSpan(), out var address);
        await Assert.That(result).IsFalse();
        await Assert.That(address).IsNull();
    }

    [Test]
    public async Task IPAddressTryParseUtf8Span()
    {
        var bytes = Encoding.UTF8.GetBytes("127.0.0.1");
        var result = IPAddress.TryParse((ReadOnlySpan<byte>) bytes, out var address);
        await Assert.That(result).IsTrue();
        await Assert.That(address).IsEqualTo(IPAddress.Loopback);
    }

    [Test]
    public async Task IPAddressTryParseUtf8Span_Invalid()
    {
        var bytes = Encoding.UTF8.GetBytes("not-an-ip");
        var result = IPAddress.TryParse((ReadOnlySpan<byte>) bytes, out var address);
        await Assert.That(result).IsFalse();
        await Assert.That(address).IsNull();
    }
}

#endif
