using System.Buffers;
using System.IO.Compression;
using System.Security.Cryptography;

// Regression tests for the second-pass bug fixes. Each runs on every target framework,
// exercising the polyfill on older TFMs and the BCL on newer ones.
partial class PolyfillTests
{
    [Test]
    public async Task ConcurrentDictionary_GetOrAdd_NullFactory_Throws()
    {
        var dictionary = new ConcurrentDictionary<string, int>();
        dictionary.TryAdd("a", 1);
        Func<string, int, int> factory = null!;

        // Eager validation: throws for both present and absent keys.
        await Assert.That(() => dictionary.GetOrAdd("a", factory, 5)).Throws<ArgumentNullException>();
        await Assert.That(() => dictionary.GetOrAdd("b", factory, 5)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task List_CopyTo_TooShort_Throws_AndLeavesDestinationUntouched()
    {
        var list = new List<int> {10, 20, 30, 40, 50};

        var destination = new int[3];
        await Assert.That(() => list.CopyTo(new Span<int>(destination))).Throws<ArgumentException>();
        await Assert.That(destination.All(_ => _ == 0)).IsTrue();

        var ok = new int[5];
        list.CopyTo(new Span<int>(ok));
        await Assert.That(string.Join(",", ok)).IsEqualTo("10,20,30,40,50");
    }

    [Test]
    public async Task List_InsertRange_InvalidIndex_Throws()
    {
        var list = new List<int> {1, 2, 3};
        await Assert.That(() => list.InsertRange(10, ReadOnlySpan<int>.Empty)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task EqualityComparer_Create_NullDelegates_Throw()
    {
        await Assert.That(() => EqualityComparer<int>.Create(null!)).Throws<ArgumentNullException>();

        // The keySelector overload's delegate signature differs in nullability between the
        // polyfill (Func<T?, TKey?>) and the .NET 11 BCL (Func<T?, TKey>); adapt accordingly.
#if NET11_0_OR_GREATER
        Func<string?, int> keySelector = null!;
#else
        Func<string?, int?> keySelector = null!;
#endif
        await Assert.That(() => EqualityComparer<string>.Create(keySelector)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task FloatingPoint_TryParse_AllowsThousands()
    {
        var invariant = CultureInfo.InvariantCulture;

        await Assert.That(double.TryParse("1,234.5", invariant, out var d)).IsTrue();
        await Assert.That(d).IsEqualTo(1234.5);

        await Assert.That(double.TryParse("1,234.5".AsSpan(), invariant, out var d2)).IsTrue();
        await Assert.That(d2).IsEqualTo(1234.5);

        await Assert.That(float.TryParse("1,234.5", invariant, out var f)).IsTrue();
        await Assert.That(f).IsEqualTo(1234.5f);

        await Assert.That(float.TryParse("1,234.5".AsSpan(), invariant, out var f2)).IsTrue();
        await Assert.That(f2).IsEqualTo(1234.5f);
    }

    [Test]
    public async Task ArraySegment_CopyTo_DefaultDestination_Throws()
    {
        var source = new ArraySegment<int>(new[] {1, 2, 3});
        ArraySegment<int> destination = default;
        await Assert.That(() => source.CopyTo(destination)).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task TimeSpan_From_HappyPath_And_Overflow()
    {
        await Assert.That(TimeSpan.FromMicroseconds(1000000).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);
        await Assert.That(TimeSpan.FromDays(1).Ticks).IsEqualTo(TimeSpan.TicksPerDay);
        await Assert.That(TimeSpan.FromSeconds(1, 0, 0).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);

        // Components that cancel out must stay in range. Accumulation happens in a wide (decimal)
        // space, so even when an individual component product overflows Int64, a cancelling
        // component keeps the result in range (matching the BCL's 128-bit accumulation).
        await Assert.That(TimeSpan.FromSeconds(1000000000000, -1000000000000000, 0).Ticks).IsEqualTo(0L);
        await Assert.That(TimeSpan.FromSeconds(0, -9498038399810654, long.MaxValue).Ticks).IsEqualTo(-2746663629558781930L);

        // The multi-argument overloads reach the integer-based factory on every framework
        // (the single-argument FromMicroseconds(double) BCL overload shadows the polyfill on net7/8).
        await Assert.That(() => TimeSpan.FromDays(int.MaxValue, 0, 0, 0, 0, 0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromSeconds(long.MaxValue, 0, 0)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Type_GetMethod_Validation()
    {
        var type = typeof(string);
        var types = new[] {typeof(int)};

        // Negative genericParameterCount throws ArgumentException; the exact subtype varies by
        // framework (ArgumentException on net6-8, ArgumentOutOfRangeException on net9+/the polyfill).
        ArgumentException? genericCountError = null;
        try
        {
            type.GetMethod("Substring", -1, BindingFlags.Public | BindingFlags.Instance, null, types, null);
        }
        catch (ArgumentException exception)
        {
            genericCountError = exception;
        }

        await Assert.That(genericCountError).IsNotNull();
        await Assert.That(genericCountError!.ParamName).IsEqualTo("genericParameterCount");

        await Assert.That(() => type.GetMethod(null!, 0, BindingFlags.Public, null, types, null)).Throws<ArgumentNullException>();
        await Assert.That(() => type.GetMethod("Substring", 0, BindingFlags.Public, null, null!, null)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task MemberInfo_HasSameMetadataDefinitionAs_Null_Throws()
    {
        var member = typeof(string).GetMethod("Trim", Type.EmptyTypes)!;
        await Assert.That(() => member.HasSameMetadataDefinitionAs(null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task TaskCompletionSource_TrySetCanceled_PropagatesToken()
    {
        using var source = new CancellationTokenSource();
        source.Cancel();
        var token = source.Token;

        var completionSource = new TaskCompletionSource();
        var result = completionSource.TrySetCanceled(token);

        await Assert.That(result).IsTrue();
        await Assert.That(completionSource.Task.IsCanceled).IsTrue();

        CancellationToken observed = default;
        try
        {
            await completionSource.Task;
        }
        catch (OperationCanceledException exception)
        {
            observed = exception.CancellationToken;
        }

        await Assert.That(observed).IsEqualTo(token);
    }

    [Test]
    public async Task FileUnixMode_SetUser_RoundTrip()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // GetUnixFileMode/SetUnixFileMode are not supported on Windows.
            return;
        }

        var path = Path.GetTempFileName();
        try
        {
            File.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.SetUser);
            var mode = File.GetUnixFileMode(path);

            // setuid is octal 4 (SetUser); it must not be read/written as setgid (octal 2).
            await Assert.That(mode.HasFlag(UnixFileMode.SetUser)).IsTrue();
            await Assert.That(mode.HasFlag(UnixFileMode.SetGroup)).IsFalse();
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public async Task XDocument_And_XElement_LoadAsync_HonorDeclaredEncoding()
    {
        var accentedE = (char) 0x00E9;
        var xml = $"<?xml version=\"1.0\" encoding=\"utf-16\"?><root>caf{accentedE}</root>";
        var expected = $"caf{accentedE}";
        // UTF-16 LE, no BOM: a UTF-8 StreamReader would mis-decode this.
        var bytes = Encoding.Unicode.GetBytes(xml);

        using var documentStream = new MemoryStream(bytes);
        var document = await XDocument.LoadAsync(documentStream, LoadOptions.None, CancellationToken.None);
        await Assert.That(document.Root!.Value).IsEqualTo(expected);

        using var elementStream = new MemoryStream(bytes);
        var element = await XElement.LoadAsync(elementStream, LoadOptions.None, CancellationToken.None);
        await Assert.That(element.Value).IsEqualTo(expected);
    }

    [Test]
    public async Task ZipFile_ExtractToDirectory_Overwrite_PreservesExistingFiles()
    {
        var root = Path.Combine(Path.GetTempPath(), "polyfill_zip_" + Guid.NewGuid().ToString("N"));
        var zipPath = Path.Combine(root, "archive.zip");
        var destination = Path.Combine(root, "dest");
        Directory.CreateDirectory(root);
        Directory.CreateDirectory(destination);
        try
        {
            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            using (var writer = new StreamWriter(archive.CreateEntry("a.txt").Open()))
            {
                writer.Write("from-archive");
            }

            File.WriteAllText(Path.Combine(destination, "preexisting.txt"), "keep-me");
            File.WriteAllText(Path.Combine(destination, "a.txt"), "old");

            await ZipFile.ExtractToDirectoryAsync(zipPath, destination, overwriteFiles: true);

            // Pre-existing file not in the archive is preserved; archive file is overwritten.
            await Assert.That(File.Exists(Path.Combine(destination, "preexisting.txt"))).IsTrue();
            await Assert.That(File.ReadAllText(Path.Combine(destination, "preexisting.txt"))).IsEqualTo("keep-me");
            await Assert.That(File.ReadAllText(Path.Combine(destination, "a.txt"))).IsEqualTo("from-archive");
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [Test]
    public async Task RandomNumberGenerator_GetInt32_ReturnsFullRange()
    {
        var seen = new HashSet<int>();
        for (var i = 0; i < 200; i++)
        {
            seen.Add(RandomNumberGenerator.GetInt32(0, 3));
        }

        await Assert.That(seen.Contains(0)).IsTrue();
        await Assert.That(seen.Contains(1)).IsTrue();
        // The exclusive-upper-bound-minus-one value was previously never produced.
        await Assert.That(seen.Contains(2)).IsTrue();
        await Assert.That(seen.All(_ => _ is >= 0 and < 3)).IsTrue();
    }

    [Test]
    public async Task Convert_FromHexString_ConsumedOnInvalidData()
    {
        var destination = new byte[8];

        // A valid leading nibble is counted as consumed; consumed stops at i unless the low nibble is valid.
        await AssertConsumed("4G", 1); // hi valid, lo invalid
        await AssertConsumed("G4", 0); // hi invalid, lo valid
        await AssertConsumed("GG", 1); // both invalid
        await AssertConsumed("444G", 3); // failure mid-span

        // The UTF-8 byte overload shares the same consumed semantics.
        var bytesStatus = Convert.FromHexString("4G"u8, destination, out var bytesConsumed, out _);
        await Assert.That(bytesStatus).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(bytesConsumed).IsEqualTo(1);

        // DestinationTooSmall takes priority over InvalidData when the buffer is already full.
        var smallDestination = new byte[2];
        var fullStatus = Convert.FromHexString("a1F6CG".AsSpan(), smallDestination, out var fullConsumed, out var fullWritten);
        await Assert.That(fullStatus).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(fullConsumed).IsEqualTo(4);
        await Assert.That(fullWritten).IsEqualTo(2);

        async Task AssertConsumed(string source, int expectedConsumed)
        {
            var status = Convert.FromHexString(source.AsSpan(), destination, out var consumed, out _);
            await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
            await Assert.That(consumed).IsEqualTo(expectedConsumed);
        }
    }

    [Test]
    public async Task ArgumentOutOfRangeException_ThrowIfZero_SetsActualValueAndParamName()
    {
        ArgumentOutOfRangeException? captured = null;
        try
        {
            ArgumentOutOfRangeException.ThrowIfZero(0, "count");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            captured = exception;
        }

        await Assert.That(captured).IsNotNull();
        await Assert.That(captured!.ActualValue).IsEqualTo((object) 0);
        await Assert.That(captured.ParamName).IsEqualTo("count");
    }
}
