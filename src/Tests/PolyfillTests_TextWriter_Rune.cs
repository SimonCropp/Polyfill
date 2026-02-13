#if NETCOREAPP3_0_OR_GREATER

using System.IO;
using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task TextWriterWriteRune()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.Write(new Rune('A'));
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("A");
    }

    [Test]
    public async Task TextWriterWriteLineRune()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine(new Rune('A'));
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("A" + Environment.NewLine);
    }

    [Test]
    public async Task TextWriterWriteAsyncRune()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync(new Rune('A'), Cancel.None);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("A");
    }

    [Test]
    public async Task TextWriterWriteLineAsyncRune()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteLineAsync(new Rune('A'), Cancel.None);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("A" + Environment.NewLine);
    }
}

#endif
