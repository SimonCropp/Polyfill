using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static TUnit.Core.HookType;
// ReSharper disable MethodHasAsyncOverload

[NotInParallel]
public class FilePolyfillTests
{
    const string SourceFilePath = "source.txt";
    const string DestinationFilePath = "destination.txt";
    const string TestFilePath = "testfile.txt";

    [Before(Test)]
    public void SetUp()
    {
        File.Delete(TestFilePath);
        File.Delete(SourceFilePath);
        File.Delete(DestinationFilePath);
    }

    [After(Test)]
    public void TearDown()
    {
        File.Delete(TestFilePath);
        File.Delete(SourceFilePath);
        File.Delete(DestinationFilePath);
    }

#if FeatureMemory
    [Test]
    public async Task AppendAllBytes()
    {
        var data = "Hello, World!"u8.ToArray();
        // ReSharper disable once MethodHasAsyncOverload
        File.AppendAllBytes(TestFilePath, data);

        var result = await File.ReadAllBytesAsync(TestFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task AppendAllBytesAsync()
    {
        var data = "Hello, Async World!"u8.ToArray();
        await File.AppendAllBytesAsync(TestFilePath, data);

        var result = await File.ReadAllBytesAsync(TestFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }
#endif

    [Test]
    public async Task ReadAllLinesAsync_ReadsAllLines()
    {
        var lines = new[] {"Line1", "Line2", "Line3"};
        File.WriteAllLines(TestFilePath, lines);

        var result = await File.ReadAllLinesAsync(TestFilePath);

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
        File.WriteAllText(TestFilePath, "abc");
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        Exception? exception = null;
        try
        {
            await File.ReadAllLinesAsync(TestFilePath, cancelSource.Token);
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
        await File.AppendAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(TestFilePath);
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
        File.WriteAllText(TestFilePath, sourceContent);

        var result = File.GetUnixFileMode(TestFilePath);

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
        File.WriteAllText(TestFilePath, sourceContent);

        var expected = UnixFileMode.UserWrite | UnixFileMode.UserRead;

        File.SetUnixFileMode(TestFilePath, expected);

        var result = File.GetUnixFileMode(TestFilePath);

        await Assert.That(result).IsEqualTo(expected);
    }

#if FeatureMemory
    [Test]
    public async Task WriteAllBytesAsync()
    {
        var data = "Hello, Write Bytes!"u8.ToArray();
        await File.WriteAllBytesAsync(TestFilePath, data);

        var result = await File.ReadAllBytesAsync(TestFilePath);
        await Assert.That(result.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task WriteAllBytesAsyncMemory()
    {
        ReadOnlyMemory<byte> data = "Hello, Write Bytes!"u8.ToArray();
        await File.WriteAllBytesAsync(TestFilePath, data);

        var result = await File.ReadAllTextAsync(TestFilePath);
        await Assert.That(result).IsEqualTo("Hello, Write Bytes!");
    }

#endif

    [Test]
    public async Task WriteAllTextAsync()
    {
        var content = "Hello, Write Text!";
        await File.WriteAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(TestFilePath);
        await Assert.That(result).IsEqualTo(content);
    }

    [Test]
    public async Task Move_ShouldMoveFileToNewLocation()
    {
        // Arrange
        var content = "Test content";
        File.WriteAllText(SourceFilePath, content);

        // Act
        File.Move(SourceFilePath, DestinationFilePath, overwrite: true);

        // Assert
        await Assert.That(File.Exists(SourceFilePath)).IsFalse();
        await Assert.That(File.Exists(DestinationFilePath)).IsTrue();
        var result = File.ReadAllText(DestinationFilePath);
        await Assert.That(result).IsEqualTo(content);
    }

    [Test]
    public async Task Move_ShouldOverwriteDestinationFile_WhenOverwriteIsTrue()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(SourceFilePath, sourceContent);
        File.WriteAllText(DestinationFilePath, destinationContent);

        // Act
        File.Move(SourceFilePath, DestinationFilePath, overwrite: true);

        // Assert
        await Assert.That(File.Exists(SourceFilePath)).IsFalse();
        await Assert.That(File.Exists(DestinationFilePath)).IsTrue();
        var result = File.ReadAllText(DestinationFilePath);
        await Assert.That(result).IsEqualTo(sourceContent);
    }

    [Test]
    public async Task Move_ShouldThrowIOException_WhenDestinationExistsAndOverwriteIsFalse()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(SourceFilePath, sourceContent);
        File.WriteAllText(DestinationFilePath, destinationContent);

        // Act & Assert
        await Assert.That(() =>
            File.Move(SourceFilePath, DestinationFilePath, overwrite: false)).Throws<IOException>();
    }
}