using System.Runtime.InteropServices;
using System.Runtime.Versioning;

[TestFixture]
[Parallelizable(ParallelScope.None)]
public class FilePolyfillTests
{
    const string SourceFilePath = "source.txt";
    const string DestinationFilePath = "destination.txt";
    const string TestFilePath = "testfile.txt";

    [SetUp]
    public void SetUp()
    {
        File.Delete(TestFilePath);
        File.Delete(SourceFilePath);
        File.Delete(DestinationFilePath);
    }

    [TearDown]
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
        Assert.AreEqual(data, result);
    }

    [Test]
    public async Task AppendAllBytesAsync()
    {
        var data = "Hello, Async World!"u8.ToArray();
        await File.AppendAllBytesAsync(TestFilePath, data);

        var result = await File.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
#endif

    [Test]
    public async Task ReadAllLinesAsync_ReadsAllLines()
    {
        var lines = new[] {"Line1", "Line2", "Line3"};
        File.WriteAllLines(TestFilePath, lines);

        var result = await File.ReadAllLinesAsync(TestFilePath);

        Assert.AreEqual(lines, result);
    }

    [Test]
    public void ReadAllLinesAsync_ThrowsIfFileNotFound()
    {
        var nonExistent = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await File.ReadAllLinesAsync(nonExistent));
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

        Assert.IsNotNull(exception);
        Assert.IsTrue(exception is OperationCanceledException or TaskCanceledException);
    }

    [Test]
    public async Task AppendAllTextAsync()
    {
        var content = "Hello, Async Text!";
        await File.AppendAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public void GetUnixFileModeTest()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var expected = UnixFileMode.OtherRead | UnixFileMode.GroupRead | UnixFileMode.UserWrite | UnixFileMode.UserRead;

        var sourceContent = "Test content";
        File.WriteAllText(TestFilePath, sourceContent);

        var result = File.GetUnixFileMode(TestFilePath);

        Assert.AreEqual(expected, result);
    }

    [UnsupportedOSPlatform("windows")]
    [Test]
    public void SetUnixFileModeTest()
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

        Assert.AreEqual(expected, result);
    }

#if FeatureMemory
    [Test]
    public async Task WriteAllBytesAsync()
    {
        var data = "Hello, Write Bytes!"u8.ToArray();
        await File.WriteAllBytesAsync(TestFilePath, data);

        var result = await File.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
#endif

    [Test]
    public async Task WriteAllTextAsync()
    {
        var content = "Hello, Write Text!";
        await File.WriteAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await File.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }

    [Test]
    public void Move_ShouldMoveFileToNewLocation()
    {
        // Arrange
        var content = "Test content";
        File.WriteAllText(SourceFilePath, content);

        // Act
        File.Move(SourceFilePath, DestinationFilePath, overwrite: true);

        // Assert
        Assert.IsFalse(File.Exists(SourceFilePath), "Source file should no longer exist.");
        Assert.IsTrue(File.Exists(DestinationFilePath), "Destination file should exist.");
        var result = File.ReadAllText(DestinationFilePath);
        Assert.AreEqual(content, result, "File content should remain unchanged.");
    }

    [Test]
    public void Move_ShouldOverwriteDestinationFile_WhenOverwriteIsTrue()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(SourceFilePath, sourceContent);
        File.WriteAllText(DestinationFilePath, destinationContent);

        // Act
        File.Move(SourceFilePath, DestinationFilePath, overwrite: true);

        // Assert
        Assert.IsFalse(File.Exists(SourceFilePath), "Source file should no longer exist.");
        Assert.IsTrue(File.Exists(DestinationFilePath), "Destination file should exist.");
        var result = File.ReadAllText(DestinationFilePath);
        Assert.AreEqual(sourceContent, result, "Destination file content should be replaced by source content.");
    }

    [Test]
    public void Move_ShouldThrowIOException_WhenDestinationExistsAndOverwriteIsFalse()
    {
        // Arrange
        var sourceContent = "Source content";
        var destinationContent = "Destination content";
        File.WriteAllText(SourceFilePath, sourceContent);
        File.WriteAllText(DestinationFilePath, destinationContent);

        // Act & Assert
        Assert.Throws<IOException>(() =>
            File.Move(SourceFilePath, DestinationFilePath, overwrite: false));
    }
}