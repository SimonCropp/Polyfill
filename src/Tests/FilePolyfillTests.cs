[TestFixture]
public class FilePolyfillTests
{
    private const string SourceFilePath = "source.txt";
    private const string DestinationFilePath = "destination.txt";
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
        FilePolyfill.AppendAllBytes(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }

    [Test]
    public async Task AppendAllBytesAsync()
    {
        var data = "Hello, Async World!"u8.ToArray();
        await FilePolyfill.AppendAllBytesAsync(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
#endif

    [Test]
    public async Task AppendAllTextAsync()
    {
        var content = "Hello, Async Text!";
        await FilePolyfill.AppendAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await FilePolyfill.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }

#if FeatureMemory
    [Test]
    public async Task WriteAllBytesAsync()
    {
        var data = "Hello, Write Bytes!"u8.ToArray();
        await FilePolyfill.WriteAllBytesAsync(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
#endif

    [Test]
    public async Task WriteAllTextAsync()
    {
        var content = "Hello, Write Text!";
        await FilePolyfill.WriteAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await FilePolyfill.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }

    [Test]
    public void Move_ShouldMoveFileToNewLocation()
    {
        // Arrange
        var content = "Test content";
        File.WriteAllText(SourceFilePath, content);

        // Act
        FilePolyfill.Move(SourceFilePath, DestinationFilePath, overwrite: true);

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
        FilePolyfill.Move(SourceFilePath, DestinationFilePath, overwrite: true);

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
            FilePolyfill.Move(SourceFilePath, DestinationFilePath, overwrite: false));
    }
}