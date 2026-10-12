using System.Runtime.Versioning;
// ReSharper disable MethodHasAsyncOverload

[NotInParallel]
public class FilePolyfillTests
{
    const string sourceFilePath = "source.txt";
    const string destinationFilePath = "destination.txt";
    const string restFilePath = "testfile.txt";
    const string hardLinkFilePath = "hardlink.txt";

    [Before(Test)]
    public void SetUp()
    {
        File.Delete(restFilePath);
        File.Delete(sourceFilePath);
        File.Delete(destinationFilePath);
        File.Delete(hardLinkFilePath);
    }

    [After(Test)]
    public void TearDown()
    {
        File.Delete(restFilePath);
        File.Delete(sourceFilePath);
        File.Delete(destinationFilePath);
        File.Delete(hardLinkFilePath);
    }

    [Test]
    public async Task GetSetUnixFileMode_SetUser_RoundTrip()
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

#if FeatureMemory
    [Test]
    public async Task AppendAllBytes()
    {
        var data = "Hello, World!"u8.ToArray();
        // ReSharper disable once MethodHasAsyncOverload
        File.AppendAllBytes(restFilePath, data);

        var result = await File.ReadAllBytesAsync(restFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task AppendAllBytesAsync()
    {
        var data = "Hello, Async World!"u8.ToArray();
        await File.AppendAllBytesAsync(restFilePath, data);

        var result = await File.ReadAllBytesAsync(restFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }
#endif

    [Test]
    public async Task ReadAllLinesAsync_ReadsAllLines()
    {
        var lines = new[] {"Line1", "Line2", "Line3"};
        File.WriteAllLines(restFilePath, lines);

        var result = await File.ReadAllLinesAsync(restFilePath);

        await Assert.That(result.SequenceEqual(lines)).IsTrue();
    }

    [Test]
    public async Task ReadAllLinesAsync_ThrowsIfFileNotFound()
    {
        var nonExistent = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        await Assert.That(async () =>
            await File.ReadAllLinesAsync(nonExistent)).Throws<FileNotFoundException>();
    }

    static string CreateReadLinesTestDirectory()
    {
        var directory = Path.Combine(Path.GetTempPath(), "PolyfillFileReadLines_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        return directory;
    }

    // File contents chosen around the reader's buffer sizes (1024 and 4096 bytes), so that lines end
    // exactly on, just before and just after a buffer boundary, and line terminators get split by one.
    static IEnumerable<string> ReadLinesContents()
    {
        yield return "";
        yield return "\n";
        yield return "\r\n";
        yield return "\r";
        yield return "\n\n\n";
        yield return "single line without terminator";
        yield return "one\ntwo\r\nthree\rfour";
        yield return "one\ntwo\r\nthree\rfour\n";
        yield return "trailing blank lines\n\n\n";
        yield return "\n\nleading blank lines";
        yield return "a\u00e9\u4f60\U0001F30D\nb\u00e9\u4f60\U0001F30D\r\n";

        foreach (var boundary in new[] {1024, 2048, 4096, 8192})
        {
            foreach (var delta in new[] {-2, -1, 0, 1, 2})
            {
                foreach (var terminator in new[] {"\n", "\r\n", "\r", ""})
                {
                    // the first line ends boundary + delta bytes into the file
                    var length = boundary + delta - terminator.Length;
                    yield return new string('x', length) + terminator;
                    yield return new string('x', length) + terminator + "next";
                    yield return new string('x', length) + terminator + "next\n";
                }
            }
        }

        // many lines of the same length, so that buffer boundaries land on every position of a line
        var builder = new StringBuilder();
        for (var index = 0; index < 5000; index++)
        {
            builder.Append("line ").Append(index % 10).Append("\r\n");
        }

        yield return builder.ToString();

        // lines that are each exactly one buffer long
        builder.Clear();
        for (var index = 0; index < 20; index++)
        {
            builder.Append('y', 1023).Append('\n');
        }

        yield return builder.ToString();

        // one line much longer than any buffer
        yield return new string('z', 100_000);
        yield return new string('z', 100_000) + "\n" + new string('w', 50_000);
    }

    [Test]
    public async Task ReadAllLinesAsync_MatchesReadAllLines()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            var index = 0;
            foreach (var content in ReadLinesContents())
            {
                index++;
                File.WriteAllText(path, content, new UTF8Encoding(false));
                var expected = File.ReadAllLines(path);

                var actual = await File.ReadAllLinesAsync(path);

                if (!actual.SequenceEqual(expected))
                {
                    throw new($"ReadAllLinesAsync differs from ReadAllLines for content {index} ({content.Length} chars): {actual.Length} lines rather than {expected.Length}");
                }
            }
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task ReadAllLinesAsync_Encodings_MatchReadAllLines()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            var builder = new StringBuilder();
            for (var index = 0; index < 3000; index++)
            {
                builder.Append("a\u00e9\u4f60\U0001F30D ").Append(index).Append(index % 2 == 0 ? "\n" : "\r\n");
            }

            var content = builder.ToString();
            var encodings = new Encoding[]
            {
                new UTF8Encoding(false),
                new UTF8Encoding(true),
                new UnicodeEncoding(false, true),
                new UnicodeEncoding(true, true),
                new UTF32Encoding(false, true)
            };
            foreach (var encoding in encodings)
            {
                File.WriteAllText(path, content, encoding);
                var expected = File.ReadAllLines(path, encoding);

                await Assert.That((await File.ReadAllLinesAsync(path, encoding)).SequenceEqual(expected)).IsTrue();

                // a byte order mark overrides the encoding that was asked for
                if (encoding.GetPreamble().Length > 0)
                {
                    await Assert.That((await File.ReadAllLinesAsync(path)).SequenceEqual(expected)).IsTrue();
                    await Assert.That((await File.ReadAllLinesAsync(path, Encoding.ASCII)).SequenceEqual(expected)).IsTrue();
                }
            }
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task ReadAllLinesAsync_CanceledToken_ThrowsAndReleasesFile()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            File.WriteAllText(path, "one\ntwo\n");
            using var cancelSource = new CancelSource();
            cancelSource.Cancel();

            await Assert.That(async () => await File.ReadAllLinesAsync(path, cancelSource.Token))
                .Throws<OperationCanceledException>();

            // the file is not left open
            File.Delete(path);
            await Assert.That(File.Exists(path)).IsFalse();
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

#if FeatureAsyncInterfaces
    [Test]
    public async Task ReadLinesAsync_MatchesReadAllLines()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            var index = 0;
            foreach (var content in ReadLinesContents())
            {
                index++;
                File.WriteAllText(path, content, new UTF8Encoding(false));
                var expected = File.ReadAllLines(path);

                var actual = new List<string>();
                await foreach (var line in File.ReadLinesAsync(path))
                {
                    actual.Add(line);
                }

                if (!actual.SequenceEqual(expected))
                {
                    throw new($"ReadLinesAsync differs from ReadAllLines for content {index} ({content.Length} chars): {actual.Count} lines rather than {expected.Length}");
                }
            }
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task ReadLinesAsync_CanceledWhileEnumerating_ThrowsAndReleasesFile()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            File.WriteAllLines(path, Enumerable.Range(0, 5000).Select(_ => "line " + _));
            using var source = new CancelSource();
            var read = new List<string>();

            await Assert.That(async () =>
                {
                    await foreach (var line in File.ReadLinesAsync(path, source.Token))
                    {
                        read.Add(line);
                        if (read.Count == 10)
                        {
                            source.Cancel();
                        }
                    }
                })
                .Throws<OperationCanceledException>();

            // The BCL only notices the cancellation at its next read from the file, so it may hand
            // out a few more lines that it had already buffered. Either way the lines that were
            // returned are the first ones, in order, and the enumeration stops well short of the end.
            await Assert.That(read.Count >= 10).IsTrue();
            await Assert.That(read.Count < 1000).IsTrue();
            await Assert.That(read.SequenceEqual(Enumerable.Range(0, read.Count).Select(_ => "line " + _))).IsTrue();

            // the file is not left open
            File.Delete(path);
            await Assert.That(File.Exists(path)).IsFalse();
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task ReadLinesAsync_StoppingEarly_ReleasesFile()
    {
        var directory = CreateReadLinesTestDirectory();
        try
        {
            var path = Path.Combine(directory, "lines.txt");
            File.WriteAllLines(path, Enumerable.Range(0, 5000).Select(_ => "line " + _));
            var read = new List<string>();

            await foreach (var line in File.ReadLinesAsync(path))
            {
                read.Add(line);
                if (read.Count == 3)
                {
                    break;
                }
            }

            await Assert.That(read.SequenceEqual(new[] {"line 0", "line 1", "line 2"})).IsTrue();

            File.Delete(path);
            await Assert.That(File.Exists(path)).IsFalse();
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
#endif

    [Test]
    public async Task ReadAllLinesAsync_ThrowsIfCancelled()
    {
        File.WriteAllText(restFilePath, "abc");
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        Exception? exception = null;
        try
        {
            await File.ReadAllLinesAsync(restFilePath, cancelSource.Token);
        }
        catch (Exception e)
        {
            exception = e;
        }

        await Assert.That(exception).IsNotNull();
        await Assert.That(exception is OperationCanceledException or TaskCanceledException).IsTrue();
    }

    [Test]
    public async Task WriteAndAppendAsync_CancelledToken_LeavesFileUntouched()
    {
        var directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var cancelSource = new CancelSource();
            cancelSource.Cancel();
            var token = cancelSource.Token;
            var bytes = Encoding.UTF8.GetBytes("new");
            var lines = new[] {"new"};
            var operations = new Dictionary<string, Func<string, Task>>
            {
                ["WriteAllBytesAsync"] = path => File.WriteAllBytesAsync(path, bytes, token),
                ["WriteAllLinesAsync"] = path => File.WriteAllLinesAsync(path, lines, token),
                ["WriteAllLinesAsyncEncoding"] = path => File.WriteAllLinesAsync(path, lines, Encoding.UTF8, token),
                ["WriteAllTextAsync"] = path => File.WriteAllTextAsync(path, "new", token),
                ["WriteAllTextAsyncEncoding"] = path => File.WriteAllTextAsync(path, "new", Encoding.UTF8, token),
                ["AppendAllBytesAsync"] = path => File.AppendAllBytesAsync(path, bytes, token),
                ["AppendAllLinesAsync"] = path => File.AppendAllLinesAsync(path, lines, token),
                ["AppendAllTextAsync"] = path => File.AppendAllTextAsync(path, "new", token),
#if FeatureMemory
                ["WriteAllBytesAsyncMemory"] = path => File.WriteAllBytesAsync(path, new ReadOnlyMemory<byte>(bytes), token),
                ["AppendAllBytesAsyncMemory"] = path => File.AppendAllBytesAsync(path, new ReadOnlyMemory<byte>(bytes), token),
#endif
            };

            foreach (var operation in operations)
            {
                var existing = Path.Combine(directory, operation.Key + ".existing.txt");
                var missing = Path.Combine(directory, operation.Key + ".missing.txt");
                File.WriteAllText(existing, "original");

                foreach (var path in new[] {existing, missing})
                {
                    Exception? exception = null;
                    try
                    {
                        await operation.Value(path);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }

                    await Assert.That(exception is OperationCanceledException).IsTrue();
                }

                // an already cancelled token must not create or truncate the file
                await Assert.That(File.ReadAllText(existing)).IsEqualTo("original");
                await Assert.That(File.Exists(missing)).IsFalse();
            }
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task AppendAllTextAsync()
    {
        var content = "Hello, Async Text!";
        await File.AppendAllTextAsync(restFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(restFilePath);
        await Assert.That(result).IsEqualTo(content);
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public async Task GetUnixFileModeTest()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var expected = UnixFileMode.OtherRead | UnixFileMode.GroupRead | UnixFileMode.UserWrite | UnixFileMode.UserRead;

        var sourceContent = "Test content";
        File.WriteAllText(restFilePath, sourceContent);

        var result = File.GetUnixFileMode(restFilePath);

        await Assert.That(result).IsEqualTo(expected);
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public async Task SetUnixFileModeTest()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var sourceContent = "Test content";
        File.WriteAllText(restFilePath, sourceContent);

        var expected = UnixFileMode.UserWrite | UnixFileMode.UserRead;

        File.SetUnixFileMode(restFilePath, expected);

        var result = File.GetUnixFileMode(restFilePath);

        await Assert.That(result).IsEqualTo(expected);
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public async Task GetSetUnixFileMode_PathWithSpaces()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var directory = Path.Combine(Path.GetTempPath(), "polyfill mode " + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "it's a \"file\".txt");
        try
        {
            File.WriteAllText(path, "content");
            var expected = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead;

            File.SetUnixFileMode(path, expected);

            await Assert.That(File.GetUnixFileMode(path)).IsEqualTo(expected);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public async Task GetSetUnixFileMode_MissingFile_Throws()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "missing.txt");

        await Assert.That(() => File.GetUnixFileMode(missing)).Throws<IOException>();
        await Assert.That(() => File.SetUnixFileMode(missing, UnixFileMode.UserRead)).Throws<IOException>();
    }

#if !NET7_0_OR_GREATER
    // UnixFileModeHelper only exists where File.GetUnixFileMode is polyfilled
    [Test]
    [Arguments("644\n", (int) (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead))]
    [Arguments("644", (int) (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead))]
    [Arguments(" 755\r\n", (int) (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.GroupRead | UnixFileMode.GroupExecute | UnixFileMode.OtherRead | UnixFileMode.OtherExecute))]
    [Arguments("4755\n", (int) (UnixFileMode.SetUser | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.GroupRead | UnixFileMode.GroupExecute | UnixFileMode.OtherRead | UnixFileMode.OtherExecute))]
    [Arguments("6000\n", (int) (UnixFileMode.SetUser | UnixFileMode.SetGroup))]
    [Arguments("1777\n", (int) (UnixFileMode.StickyBit | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute | UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute))]
    [Arguments("0\n", (int) (UnixFileMode.None))]
    [Arguments("44\n", (int) (UnixFileMode.GroupRead | UnixFileMode.OtherRead))]
    // BSD stat -f %p includes the file type
    [Arguments("100644\n", (int) (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead))]
    [Arguments("104755\n", (int) (UnixFileMode.SetUser | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.GroupRead | UnixFileMode.GroupExecute | UnixFileMode.OtherRead | UnixFileMode.OtherExecute))]
    [Arguments("40700\n", (int) (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute))]
    public async Task UnixFileModeHelper_ParseMode(string output, int expected)
    {
        var mode = Polyfills.Polyfill.UnixFileModeHelper.ParseMode(output);

        await Assert.That((int) mode).IsEqualTo(expected);
    }

    [Test]
    [Arguments("")]
    [Arguments("\n")]
    [Arguments("stat: cannot stat")]
    [Arguments("689")]
    public async Task UnixFileModeHelper_ParseMode_Invalid(string output)
    {
        await Assert.That(() => Polyfills.Polyfill.UnixFileModeHelper.ParseMode(output)).Throws<FormatException>();
    }
#endif

#if FeatureMemory
    [Test]
    public async Task WriteAllBytesAsync()
    {
        var data = "Hello, Write Bytes!"u8.ToArray();
        await File.WriteAllBytesAsync(restFilePath, data);

        var result = await File.ReadAllBytesAsync(restFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task WriteAllBytesAsyncMemory()
    {
        ReadOnlyMemory<byte> data = "Hello, Write Bytes!"u8.ToArray();
        await File.WriteAllBytesAsync(restFilePath, data);

        var result = await File.ReadAllTextAsync(restFilePath);
        await Assert.That(result).IsEqualTo("Hello, Write Bytes!");
    }

#endif

    [Test]
    public async Task WriteAllTextAsync()
    {
        var content = "Hello, Write Text!";
        await File.WriteAllTextAsync(restFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(restFilePath);
        await Assert.That(result).IsEqualTo(content);
    }

    // With no encoding given the BCL writes UTF-8 without a byte order mark
    [Test]
    public async Task WriteAllTextAsync_NoEncoding_WritesNoBom()
    {
        await File.WriteAllTextAsync(restFilePath, "abc");

        var result = File.ReadAllBytes(restFilePath);
        await Assert.That(result.SequenceEqual("abc"u8.ToArray())).IsTrue();
    }

    [Test]
    public async Task AppendAllTextAsync_NoEncoding_WritesNoBom()
    {
        await File.AppendAllTextAsync(restFilePath, "abc");

        var result = File.ReadAllBytes(restFilePath);
        await Assert.That(result.SequenceEqual("abc"u8.ToArray())).IsTrue();
    }

    [Test]
    public async Task WriteAllLinesAsync_NoEncoding_WritesNoBom()
    {
        await File.WriteAllLinesAsync(restFilePath, ["abc"]);

        var result = File.ReadAllBytes(restFilePath);
        await Assert.That(result.SequenceEqual(Encoding.UTF8.GetBytes($"abc{Environment.NewLine}"))).IsTrue();
    }

    [Test]
    public async Task AppendAllLinesAsync_NoEncoding_WritesNoBom()
    {
        await File.AppendAllLinesAsync(restFilePath, ["abc"]);

        var result = File.ReadAllBytes(restFilePath);
        await Assert.That(result.SequenceEqual(Encoding.UTF8.GetBytes($"abc{Environment.NewLine}"))).IsTrue();
    }

    [Test]
    public async Task Move_ShouldMoveFileToNewLocation()
    {
        // Arrange
        var content = "Test content";
        File.WriteAllText(sourceFilePath, content);

        // Act
        File.Move(sourceFilePath, destinationFilePath, overwrite: true);

        // Assert
        await Assert.That(File.Exists(sourceFilePath)).IsFalse();
        await Assert.That(File.Exists(destinationFilePath)).IsTrue();
        var result = File.ReadAllText(destinationFilePath);
        await Assert.That(result).IsEqualTo(content);
    }

    [Test]
    public async Task Move_ShouldOverwriteDestinationFile_WhenOverwriteIsTrue()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(sourceFilePath, sourceContent);
        File.WriteAllText(destinationFilePath, destinationContent);

        // Act
        File.Move(sourceFilePath, destinationFilePath, overwrite: true);

        // Assert
        await Assert.That(File.Exists(sourceFilePath)).IsFalse();
        await Assert.That(File.Exists(destinationFilePath)).IsTrue();
        var result = File.ReadAllText(destinationFilePath);
        await Assert.That(result).IsEqualTo(sourceContent);
    }

    [Test]
    public async Task Move_ShouldThrowIOException_WhenDestinationExistsAndOverwriteIsFalse()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(sourceFilePath, sourceContent);
        File.WriteAllText(destinationFilePath, destinationContent);

        // Act & Assert
        await Assert.That(() =>
            File.Move(sourceFilePath, destinationFilePath, overwrite: false)).Throws<IOException>();
    }

    static string CreateMoveDirectory()
    {
        var directory = Path.Combine(Path.GetTempPath(), "polyfill_move_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        return directory;
    }

    [Test]
    public async Task Move_Overwrite_ReadOnlySource()
    {
        var directory = CreateMoveDirectory();
        var source = Path.Combine(directory, "source.txt");
        var destination = Path.Combine(directory, "destination.txt");
        try
        {
            File.WriteAllText(source, "new");
            File.WriteAllText(destination, "old");
            File.SetAttributes(source, FileAttributes.ReadOnly);

            File.Move(source, destination, overwrite: true);

            await Assert.That(File.Exists(source)).IsFalse();
            await Assert.That(File.ReadAllText(destination)).IsEqualTo("new");
            // a move keeps the attributes of the source
            await Assert.That(File.GetAttributes(destination).HasFlag(FileAttributes.ReadOnly)).IsTrue();
        }
        finally
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                File.SetAttributes(file, FileAttributes.Normal);
            }

            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_CaseOnlyRename()
    {
        var directory = CreateMoveDirectory();
        try
        {
            File.WriteAllText(Path.Combine(directory, "name.txt"), "content");

            File.Move(Path.Combine(directory, "name.txt"), Path.Combine(directory, "NAME.txt"), overwrite: true);

            var names = Directory.GetFiles(directory).Select(_ => Path.GetFileName(_)).ToList();
            await Assert.That(names.Count).IsEqualTo(1);
            await Assert.That(names[0]).IsEqualTo("NAME.txt");
            await Assert.That(File.ReadAllText(Path.Combine(directory, "NAME.txt"))).IsEqualTo("content");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_SameMissingPath_Throws()
    {
        var directory = CreateMoveDirectory();
        var missing = Path.Combine(directory, "missing.txt");
        try
        {
            await Assert.That(() => File.Move(missing, missing, overwrite: true)).Throws<FileNotFoundException>();
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_SamePath_KeepsFile()
    {
        var directory = CreateMoveDirectory();
        var file = Path.Combine(directory, "file.txt");
        try
        {
            File.WriteAllText(file, "content");

            File.Move(file, file, overwrite: true);

            await Assert.That(File.ReadAllText(file)).IsEqualTo("content");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_MissingSource_KeepsDestination()
    {
        var directory = CreateMoveDirectory();
        var destination = Path.Combine(directory, "destination.txt");
        try
        {
            File.WriteAllText(destination, "old");

            await Assert.That(() => File.Move(Path.Combine(directory, "missing.txt"), destination, overwrite: true))
                .Throws<FileNotFoundException>();
            await Assert.That(File.ReadAllText(destination)).IsEqualTo("old");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_MissingDestinationDirectory_KeepsSource()
    {
        var directory = CreateMoveDirectory();
        var source = Path.Combine(directory, "source.txt");
        try
        {
            File.WriteAllText(source, "content");

            await Assert.That(() => File.Move(source, Path.Combine(directory, "missing", "destination.txt"), overwrite: true))
                .Throws<DirectoryNotFoundException>();
            await Assert.That(File.ReadAllText(source)).IsEqualTo("content");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Test]
    public async Task Move_Overwrite_ReadOnlyDestination_KeepsBoth()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // only Windows refuses to replace a read-only file
            return;
        }

        var directory = CreateMoveDirectory();
        var source = Path.Combine(directory, "source.txt");
        var destination = Path.Combine(directory, "destination.txt");
        try
        {
            File.WriteAllText(source, "new");
            File.WriteAllText(destination, "old");
            File.SetAttributes(destination, FileAttributes.ReadOnly);

            await Assert.That(() => File.Move(source, destination, overwrite: true))
                .Throws<UnauthorizedAccessException>();
            await Assert.That(File.ReadAllText(source)).IsEqualTo("new");
            await Assert.That(File.ReadAllText(destination)).IsEqualTo("old");
        }
        finally
        {
            File.SetAttributes(destination, FileAttributes.Normal);
            Directory.Delete(directory, true);
        }
    }

#if FeatureAsyncInterfaces

    [Test]
    public async Task ReadLinesAsync_ReadsAllLines()
    {
        var lines = new[] { "Line1", "Line2", "Line3" };
        File.WriteAllLines(restFilePath, lines);

        var result = new List<string>();
        await foreach (var line in File.ReadLinesAsync(restFilePath))
        {
            result.Add(line);
        }

        await Assert.That(result.SequenceEqual(lines)).IsTrue();
    }

    [Test]
    public async Task ReadLinesAsync_WithEncoding_ReadsAllLines()
    {
        var lines = new[] { "Línea1", "Línea2", "Línea3" };
        File.WriteAllLines(restFilePath, lines, Encoding.UTF8);

        var result = new List<string>();
        await foreach (var line in File.ReadLinesAsync(restFilePath, Encoding.UTF8))
        {
            result.Add(line);
        }

        await Assert.That(result.SequenceEqual(lines)).IsTrue();
    }

    [Test]
    public async Task ReadLinesAsync_EmptyFile_ReturnsNoLines()
    {
        File.WriteAllText(restFilePath, "");

        var result = new List<string>();
        await foreach (var line in File.ReadLinesAsync(restFilePath))
        {
            result.Add(line);
        }

        await Assert.That(result.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ReadLinesAsync_ThrowsIfFileNotFound()
    {
        var nonExistent = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        await Assert.That(async () =>
        {
            await foreach (var _ in File.ReadLinesAsync(nonExistent))
            {
            }
        }).Throws<FileNotFoundException>();
    }

    [Test]
    public async Task ReadLinesAsync_ThrowsIfCancelled()
    {
        var lines = new[] { "Line1", "Line2", "Line3" };
        File.WriteAllLines(restFilePath, lines);
        var cancelSource = new CancelSource();
        cancelSource.Cancel();

        Exception? exception = null;
        try
        {
            await foreach (var _ in File.ReadLinesAsync(restFilePath, cancelSource.Token))
            {
            }
        }
        catch (Exception e)
        {
            exception = e;
        }

        await Assert.That(exception).IsNotNull();
        await Assert.That(exception is OperationCanceledException or TaskCanceledException).IsTrue();
    }

#endif

    [Test]
    public async Task OpenNullHandle()
    {
        using var handle = File.OpenNullHandle();

        await Assert.That(handle.IsInvalid).IsFalse();
        await Assert.That(handle.IsClosed).IsFalse();
    }

    // Where the BCL declares File.OpenNullHandle the polyfill must not be compiled: a static
    // member of the type always wins over a static extension member, so it would be dead code.
    [Test]
    public async Task OpenNullHandle_NotPolyfilledWhereBclHasIt()
    {
        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        var inBcl = typeof(File).GetMethod("OpenNullHandle", flags, null, Type.EmptyTypes, null) != null;
        var inPolyfill = typeof(Polyfills.Polyfill).GetMethod("OpenNullHandle", flags, null, Type.EmptyTypes, null) != null;

        await Assert.That(inBcl && inPolyfill).IsFalse();
    }

    [Test]
    public async Task CreateHardLink_CreatesLinkToExistingFile()
    {
        var content = "Hard link test content";
        File.WriteAllText(sourceFilePath, content);

        var result = File.CreateHardLink(hardLinkFilePath, sourceFilePath);

        await Assert.That(result).IsNotNull();
        await Assert.That(File.Exists(hardLinkFilePath)).IsTrue();
        await Assert.That(File.ReadAllText(hardLinkFilePath)).IsEqualTo(content);
    }

    [Test]
    public async Task CreateHardLink_ReturnsFileInfo()
    {
        File.WriteAllText(sourceFilePath, "test");

        var result = File.CreateHardLink(hardLinkFilePath, sourceFilePath);

        await Assert.That(result).IsTypeOf<FileInfo>();
    }

    [Test]
    public async Task CreateHardLink_SharesFileContent()
    {
        File.WriteAllText(sourceFilePath, "original");

        File.CreateHardLink(hardLinkFilePath, sourceFilePath);
        File.WriteAllText(sourceFilePath, "modified");

        await Assert.That(File.ReadAllText(hardLinkFilePath)).IsEqualTo("modified");
    }

    [Test]
    public async Task CreateHardLink_ThrowsIfTargetNotFound() =>
        await Assert.That(() =>
            File.CreateHardLink(hardLinkFilePath, "nonexistent.txt")).Throws<IOException>();

    [Test]
    public async Task FileInfo_CreateAsHardLink()
    {
        var content = "FileInfo hard link test";
        File.WriteAllText(sourceFilePath, content);

        var fileInfo = new FileInfo(hardLinkFilePath);
        fileInfo.CreateAsHardLink(sourceFilePath);

        await Assert.That(File.Exists(hardLinkFilePath)).IsTrue();
        await Assert.That(File.ReadAllText(hardLinkFilePath)).IsEqualTo(content);
    }

    [Test]
    public async Task FileInfo_CreateAsHardLink_ThrowsIfTargetNotFound()
    {
        var fileInfo = new FileInfo(hardLinkFilePath);

        await Assert.That(() =>
            fileInfo.CreateAsHardLink("nonexistent.txt")).Throws<IOException>();
    }
}