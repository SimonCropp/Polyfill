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