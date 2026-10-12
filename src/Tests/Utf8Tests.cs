#if NETCOREAPP3_0_OR_GREATER

using System.Buffers;
using System.Text;
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

    // decodes one scalar at a time
    static int ReferenceIndexOfInvalidSubsequence(byte[] value)
    {
        var index = 0;
        while (index < value.Length)
        {
            var status = Rune.DecodeFromUtf8(new(value, index, value.Length - index), out _, out var consumed);
            if (status != OperationStatus.Done)
            {
                return index;
            }

            index += consumed;
        }

        return -1;
    }

    static void Check(byte[] value, List<string> failures)
    {
        var expected = ReferenceIndexOfInvalidSubsequence(value);
        var index = Utf8.IndexOfInvalidSubsequence(value);
        if (index != expected)
        {
            failures.Add($"length {value.Length}: expected {expected} but got {index} ({BitConverter.ToString(value, 0, Math.Min(value.Length, 48))})");
        }
    }

    static readonly string[] samples =
    [
        "",
        "a",
        "plain ascii text that is long enough to be handled a vector at a time, 0123456789 0123456789 0123456789",
        // 2, 3 and 4 byte sequences
        "caf\u00E9 na\u00EFve \u00FCber",
        "\u65E5\u672C\u8A9E\u306E\u30C6\u30AD\u30B9\u30C8",
        "\U0001F44D\U0001F600\U0010FFFF",
        "ascii then \u00E9 then \u65E5 then \U0001F44D then ascii again, repeated: ascii then \u00E9 then \u65E5 then \U0001F44D",
        // the first and last scalar of each encoded length
        "\0\u007f\u0080\u07FF\u0800\uFFFF\U00010000\U0010FFFF",
        // either side of the surrogate range
        "\uD7FF\uE000",
    ];

    [Test]
    public async Task IndexOfInvalidSubsequence_ValidSamples()
    {
        var failures = new List<string>();
        foreach (var sample in samples)
        {
            var bytes = Encoding.UTF8.GetBytes(sample);
            if (Utf8.IndexOfInvalidSubsequence(bytes) != -1)
            {
                failures.Add($"'{sample}' should be valid");
            }

            Check(bytes, failures);
        }

        for (var length = 0; length <= 300; length++)
        {
            Check(Encoding.UTF8.GetBytes(new string('a', length)), failures);
            Check(Encoding.UTF8.GetBytes(new string('a', length) + "\u00E9"), failures);
            Check(Encoding.UTF8.GetBytes(new string('a', length) + "\U0001F44D" + new string('b', length)), failures);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_CorruptedAtEachPosition()
    {
        var failures = new List<string>();
        byte[] replacements = [0x00, 0x41, 0x7F, 0x80, 0xBF, 0xC0, 0xC1, 0xC2, 0xE0, 0xED, 0xF0, 0xF4, 0xF5, 0xFF];
        foreach (var sample in samples)
        {
            var bytes = Encoding.UTF8.GetBytes(sample);
            for (var position = 0; position < bytes.Length; position++)
            {
                // overwrite one byte
                foreach (var replacement in replacements)
                {
                    var corrupted = (byte[]) bytes.Clone();
                    corrupted[position] = replacement;
                    Check(corrupted, failures);
                }

                // cut the input short, possibly in the middle of a sequence
                var truncated = new byte[position];
                Array.Copy(bytes, truncated, position);
                Check(truncated, failures);

                // drop one byte
                var shortened = new byte[bytes.Length - 1];
                Array.Copy(bytes, 0, shortened, 0, position);
                Array.Copy(bytes, position + 1, shortened, position, bytes.Length - position - 1);
                Check(shortened, failures);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_InvalidAfterLongValidPrefix()
    {
        var failures = new List<string>();
        byte[][] invalidTails =
        [
            [0x80],
            [0xFF],
            [0xC0, 0x80],
            [0xC3],
            [0xE0, 0x80, 0x80],
            [0xED, 0xA0, 0x80],
            [0xF0, 0x80, 0x80, 0x80],
            [0xF4, 0x90, 0x80, 0x80],
            [0xF0, 0x9F, 0x91],
        ];
        foreach (var prefixLength in new[] {0, 1, 15, 16, 17, 31, 32, 33, 63, 64, 65, 127, 128, 129, 1000, 100000})
        {
            foreach (var tail in invalidTails)
            {
                var prefix = Encoding.UTF8.GetBytes(new string('a', prefixLength));
                var bytes = new byte[prefix.Length + tail.Length + 3];
                Array.Copy(prefix, bytes, prefix.Length);
                Array.Copy(tail, 0, bytes, prefix.Length, tail.Length);
                bytes[bytes.Length - 3] = (byte) 'x';
                bytes[bytes.Length - 2] = (byte) 'y';
                bytes[bytes.Length - 1] = (byte) 'z';

                if (Utf8.IndexOfInvalidSubsequence(bytes) != prefixLength)
                {
                    failures.Add($"prefix {prefixLength}, tail {BitConverter.ToString(tail)}: got {Utf8.IndexOfInvalidSubsequence(bytes)}");
                }

                Check(bytes, failures);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_RandomBytes()
    {
        var failures = new List<string>();
        var random = new Random(8765);
        for (var iteration = 0; iteration < 3000; iteration++)
        {
            var bytes = new byte[random.Next(0, 120)];
            // from mostly ascii to arbitrary bytes
            var asciiChance = random.Next(0, 100) / 100.0;
            for (var index = 0; index < bytes.Length; index++)
            {
                if (random.NextDouble() < asciiChance)
                {
                    bytes[index] = (byte) random.Next(0, 0x80);
                }
                else
                {
                    bytes[index] = (byte) random.Next(0, 0x100);
                }
            }

            Check(bytes, failures);
        }

        await Assert.That(failures).IsEmpty();
    }
}

#endif
