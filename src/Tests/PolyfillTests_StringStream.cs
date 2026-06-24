#if FeatureMemory
using System.IO;
using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task StringStream_ReadsExpectedUtf8Bytes()
    {
        var expected = Encoding.UTF8.GetBytes("Hello, World!");
        using var stream = new StringStream("Hello, World!", Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_FromReadOnlyMemory_ReadsExpectedBytes()
    {
        var expected = Encoding.UTF8.GetBytes("chars");
        using var stream = new StringStream("chars".AsMemory(), Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_Unicode_RoundTripsThroughEncoding()
    {
        const string input = "Unicode: 你好世界 🌍";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_ReadInSmallChunks_ReconstructsOutput()
    {
        var expected = Encoding.UTF8.GetBytes("The quick brown fox");
        using var stream = new StringStream("The quick brown fox", Encoding.UTF8);

        using var accumulator = new MemoryStream();
        var buffer = new byte[3];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            accumulator.Write(buffer, 0, read);
        }

        await Assert.That(accumulator.ToArray()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_ReadByte_ReturnsBytesThenMinusOne()
    {
        using var stream = new StringStream("AB", Encoding.ASCII);

        var first = stream.ReadByte();
        var second = stream.ReadByte();
        var third = stream.ReadByte();

        await Assert.That(first).IsEqualTo((int)'A');
        await Assert.That(second).IsEqualTo((int)'B');
        await Assert.That(third).IsEqualTo(-1);
    }

    [Test]
    public async Task StringStream_Empty_ReadsZero()
    {
        using var stream = new StringStream("", Encoding.UTF8);

        var buffer = new byte[8];
        var read = stream.Read(buffer, 0, buffer.Length);

        await Assert.That(read).IsEqualTo(0);
    }

    [Test]
    public async Task StringStream_NeverEmitsBom()
    {
        // UTF8 with BOM preamble: StringStream must not write the preamble.
        var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        using var stream = new StringStream("x", encoding);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(new byte[] { (byte)'x' });
    }

    [Test]
    public async Task StringStream_ReadAsync_ReadsExpectedBytes()
    {
        var expected = Encoding.UTF8.GetBytes("async");
        using var stream = new StringStream("async", Encoding.UTF8);

        var buffer = new byte[expected.Length];
        var total = 0;
        int read;
        while (total < buffer.Length &&
               (read = await stream.ReadAsync(buffer, total, buffer.Length - total)) > 0)
        {
            total += read;
        }

        await Assert.That(total).IsEqualTo(expected.Length);
        await Assert.That(buffer).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_Capabilities()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsFalse();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(stream.Encoding).IsEqualTo((Encoding)Encoding.UTF8);
    }

    [Test]
    public async Task StringStream_SeekingMembersThrow()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(() => stream.Length).Throws<NotSupportedException>();
        await Assert.That(() => stream.Position).Throws<NotSupportedException>();
        await Assert.That(() => stream.Seek(0, SeekOrigin.Begin)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task StringStream_WritingMembersThrow()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(() => stream.Write(new byte[1], 0, 1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.SetLength(1)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task StringStream_NullArgumentsThrow()
    {
        await Assert.That(() => new StringStream((string)null!, Encoding.UTF8)).Throws<ArgumentNullException>();
        await Assert.That(() => new StringStream("x", null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task StringStream_AfterDispose_CanReadIsFalse()
    {
        var stream = new StringStream("data", Encoding.UTF8);
        stream.Dispose();

        await Assert.That(stream.CanRead).IsFalse();
    }

    static byte[] ReadToEnd(Stream stream)
    {
        using var memory = new MemoryStream();
        var buffer = new byte[16];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            memory.Write(buffer, 0, read);
        }

        return memory.ToArray();
    }
}
#endif
