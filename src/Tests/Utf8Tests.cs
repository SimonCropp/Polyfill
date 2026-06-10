#if NETCOREAPP3_0_OR_GREATER

using System.Text.Unicode;

public class Utf8Tests
{
    [Test]
    public async Task IndexOfInvalidSubsequence_Valid()
    {
        byte[] bytes = "Hello"u8.ToArray();
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_ValidMultibyte()
    {
        // U+1F44D 👍 = F0 9F 91 8D
        byte[] bytes = [0xF0, 0x9F, 0x91, 0x8D];
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_BadContinuation()
    {
        // 0xC3 starts a 2-byte sequence but 0x28 is not a continuation byte
        byte[] bytes = [0xC3, 0x28];
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_LoneContinuation()
    {
        byte[] bytes = [0x41, 0x80, 0x42];
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(1);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_Truncated()
    {
        // 'A' followed by an incomplete 4-byte sequence (F0 9F 91 then EOF)
        byte[] bytes = [0x41, 0xF0, 0x9F, 0x91];
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(1);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_Surrogate()
    {
        // ED A0 80 would encode U+D800 (a surrogate) and is ill-formed
        byte[] bytes = [0xED, 0xA0, 0x80];
        var index = Utf8.IndexOfInvalidSubsequence(bytes);
        await Assert.That(index).IsEqualTo(0);
    }
}

#endif
