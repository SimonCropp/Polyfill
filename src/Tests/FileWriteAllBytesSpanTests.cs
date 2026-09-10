#if FeatureMemory

using System.IO;

// Runs on every target, so net9.0 and later validate the BCL and everything below the polyfill.
// The byte array overload is the oracle: it exists unchanged on every framework and writes the
// bytes this span overload exists to write without allocating.
public class FileWriteAllBytesSpanTests
{
    [Test]
    public async Task MatchesTheArrayOverload()
    {
        using var directory = new TempDirectory();

        byte[][] payloads = [[], [1], [1, 2, 3], new byte[100000]];
        foreach (var payload in payloads)
        {
            var viaSpan = directory.File("span.bin");
            var viaArray = directory.File("array.bin");

            WriteSpan(viaSpan, payload);
            File.WriteAllBytes(viaArray, payload);

            await Assert.That(File.ReadAllBytes(viaSpan)).IsEquivalentTo(File.ReadAllBytes(viaArray));
        }
    }

    [Test]
    public async Task TruncatesAnExistingFile()
    {
        using var directory = new TempDirectory();
        var path = directory.File("truncate.bin");

        File.WriteAllBytes(path, new byte[50]);
        WriteSpan(path, [1, 2, 3]);
        await Assert.That(new FileInfo(path).Length).IsEqualTo(3);

        WriteSpan(path, []);
        await Assert.That(new FileInfo(path).Length).IsEqualTo(0);
        await Assert.That(File.Exists(path)).IsTrue();
    }

    [Test]
    public async Task CreatesAMissingFile()
    {
        using var directory = new TempDirectory();
        var path = directory.File("fresh.bin");

        WriteSpan(path, [9]);

        await Assert.That(File.Exists(path)).IsTrue();
        await Assert.That(File.ReadAllBytes(path)).IsEquivalentTo(new byte[] { 9 });
    }

    [Test]
    public async Task ArgumentValidationMatchesTheArrayOverload()
    {
        using var directory = new TempDirectory();
        var missingDirectory = Path.Combine(directory.Path, "nope", "x.bin");

        await Assert.That(CaptureSpan(null!)).IsEqualTo(CaptureArray(null!));
        await Assert.That(CaptureSpan("")).IsEqualTo(CaptureArray(""));
        await Assert.That(CaptureSpan("   ")).IsEqualTo(CaptureArray("   "));
        await Assert.That(CaptureSpan(missingDirectory)).IsEqualTo(CaptureArray(missingDirectory));

        // and the concrete shapes, so a change in either overload is visible. Only the ones that
        // are the same everywhere are pinned with their paramName: .NET Framework throws
        // ArgumentException with no paramName for an empty path where .NET names it, and the
        // polyfill inherits whichever the running framework does.
        await Assert.That(CaptureSpan(null!)).IsEqualTo("ArgumentNullException:path");
        await Assert.That(CaptureSpan(missingDirectory)).IsEqualTo("DirectoryNotFoundException:");
        await Assert.That(TypeOfSpanFailure("")).IsEqualTo("ArgumentException");
        await Assert.That(TypeOfSpanFailure("   ")).IsEqualTo("ArgumentException");
    }

    // the span stays inside these helpers, so nothing ref struct shaped lives across an await
    static void WriteSpan(string path, byte[] bytes) =>
        File.WriteAllBytes(path, (ReadOnlySpan<byte>) bytes);

    static string CaptureSpan(string path) =>
        Capture(() => File.WriteAllBytes(path, (ReadOnlySpan<byte>) new byte[] { 1 }));

    static string TypeOfSpanFailure(string path) =>
        CaptureSpan(path).Split(':')[0];

    static string CaptureArray(string path) =>
        Capture(() => File.WriteAllBytes(path, new byte[] { 1 }));

    static string Capture(Action action)
    {
        try
        {
            action();
            return "none";
        }
        catch (Exception exception)
        {
            return $"{exception.GetType().Name}:{(exception as ArgumentException)?.ParamName}";
        }
    }

    class TempDirectory : IDisposable
    {
        public string Path { get; } = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "polyfill-" + Guid.NewGuid().ToString("N"));

        public TempDirectory() =>
            Directory.CreateDirectory(Path);

        public string File(string name) =>
            System.IO.Path.Combine(Path, name);

        public void Dispose()
        {
            try
            {
                Directory.Delete(Path, true);
            }
            catch
            {
            }
        }
    }
}

#endif
