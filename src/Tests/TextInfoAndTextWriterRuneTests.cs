// Rune ships from netcoreapp3.0, so these can't compile lower.
#if NETCOREAPP3_0_OR_GREATER

using System.Text;

// net11.0 runs the BCL, netcoreapp3.0 to net10.0 the polyfill.
public class TextInfoRunePolyfillTests
{
    static Rune[] runes =
    [
        new('a'),
        new('A'),
        new('i'),
        new('I'),
        new('İ'),
        new('ı'),
        new('ß'),
        new('5'),
        new('Σ'),
        new('σ'),
        // Deseret capital and small long i, outside the Basic Multilingual Plane
        new(0x10400),
        new(0x10428),
        // Adlam capital and small alif
        new(0x1E900),
        new(0x1E922),
        // an emoji, which has no case
        new(0x1F600)
    ];

    static string[] cultures = ["", "en-US", "tr-TR"];

    // Rune.ToLower(Rune, CultureInfo) and Rune.ToUpper(Rune, CultureInfo) are the oracle: they are in the BCL
    // from netcoreapp3.0 and convert through the culture's TextInfo.
    [Test]
    public async Task ToLowerAndToUpper_MatchRune()
    {
        var failures = new List<string>();
        foreach (var name in cultures)
        {
            var culture = CultureInfo.GetCultureInfo(name);
            var textInfo = culture.TextInfo;
            foreach (var rune in runes)
            {
                var lower = textInfo.ToLower(rune);
                var expectedLower = Rune.ToLower(rune, culture);
                if (lower != expectedLower)
                {
                    failures.Add($"'{name}' ToLower U+{rune.Value:X4}: expected U+{expectedLower.Value:X4}, got U+{lower.Value:X4}");
                }

                var upper = textInfo.ToUpper(rune);
                var expectedUpper = Rune.ToUpper(rune, culture);
                if (upper != expectedUpper)
                {
                    failures.Add($"'{name}' ToUpper U+{rune.Value:X4}: expected U+{expectedUpper.Value:X4}, got U+{upper.Value:X4}");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task SupplementaryRunesChangeCase()
    {
        var textInfo = CultureInfo.InvariantCulture.TextInfo;
        await Assert.That(textInfo.ToLower(new Rune(0x10400))).IsEqualTo(new Rune(0x10428));
        await Assert.That(textInfo.ToUpper(new Rune(0x10428))).IsEqualTo(new Rune(0x10400));
    }
}

public class TextWriterRunePolyfillTests
{
    static readonly Rune deseret = new(0x10400);

    [Test]
    public async Task WriteAsync_WritesTheRune()
    {
        using var writer = new StringWriter();
        await writer.WriteAsync(new Rune('a'));
        await writer.WriteAsync(deseret);
        await writer.WriteLineAsync(deseret);
        await writer.WriteLineAsync(new Rune('b'));

        await Assert.That(writer.ToString()).IsEqualTo($"a{deseret}{deseret}{writer.NewLine}b{writer.NewLine}");
    }

    // The BCL's base implementation sends a BMP rune to WriteAsync(char)/WriteLineAsync(char), and a surrogate pair to
    // Write(char) then Write(char)/WriteLine(char). The polyfill makes the same calls, so derived writers see the same.
    [Test]
    public async Task WriteAsync_CallsTheSameMembersAsTheBcl()
    {
        var writer = new RecordingWriter();
        await writer.WriteAsync(new Rune('a'));
        await writer.WriteAsync(deseret);
        await writer.WriteLineAsync(new Rune('b'));
        await writer.WriteLineAsync(deseret);

        await Assert.That(string.Join(" ", writer.Calls)).IsEqualTo(
            "WriteAsync(a) Write(D801) Write(DC00) WriteLineAsync(b) Write(D801) WriteLine(DC00)");
    }

    class RecordingWriter : TextWriter
    {
        public List<string> Calls { get; } = [];

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value) =>
            Calls.Add($"Write({Describe(value)})");

        public override void WriteLine(char value) =>
            Calls.Add($"WriteLine({Describe(value)})");

        public override Task WriteAsync(char value)
        {
            Calls.Add($"WriteAsync({Describe(value)})");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(char value)
        {
            Calls.Add($"WriteLineAsync({Describe(value)})");
            return Task.CompletedTask;
        }

        static string Describe(char value) =>
            char.IsSurrogate(value) ? ((int) value).ToString("X4") : value.ToString();
    }
}

#endif
