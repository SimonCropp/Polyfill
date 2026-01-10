#if NET6_0_OR_GREATER

using System.IO;

[NotInParallel]
public class RandomAccessPolyfillTests
{
    const string testFilePath = "randomaccess_test.bin";

    [Before(Test)]
    public void SetUp()
    {
        File.Delete(testFilePath);
    }

    [After(Test)]
    public void TearDown()
    {
        File.Delete(testFilePath);
    }

    [Test]
    public async Task SetLength_SetsFileLengthToSpecifiedValue()
    {
        // Arrange
        File.WriteAllBytes(testFilePath, new byte[100]);
        using var handle = File.OpenHandle(testFilePath, FileMode.Open, FileAccess.ReadWrite);

        // Act
        RandomAccess.SetLength(handle, 50);

        // Assert
        var length = RandomAccess.GetLength(handle);
        await Assert.That(length).IsEqualTo(50);
    }

    [Test]
    public async Task SetLength_ExtendsFile()
    {
        // Arrange
        File.WriteAllBytes(testFilePath, new byte[10]);
        using var handle = File.OpenHandle(testFilePath, FileMode.Open, FileAccess.ReadWrite);

        // Act
        RandomAccess.SetLength(handle, 100);

        // Assert
        var length = RandomAccess.GetLength(handle);
        await Assert.That(length).IsEqualTo(100);
    }

    [Test]
    public async Task SetLength_TruncatesFile()
    {
        // Arrange
        File.WriteAllBytes(testFilePath, new byte[100]);
        using var handle = File.OpenHandle(testFilePath, FileMode.Open, FileAccess.ReadWrite);

        // Act
        RandomAccess.SetLength(handle, 10);

        // Assert
        var length = RandomAccess.GetLength(handle);
        await Assert.That(length).IsEqualTo(10);
    }

    [Test]
    public async Task SetLength_SetsToZero()
    {
        // Arrange
        File.WriteAllBytes(testFilePath, new byte[100]);
        using var handle = File.OpenHandle(testFilePath, FileMode.Open, FileAccess.ReadWrite);

        // Act
        RandomAccess.SetLength(handle, 0);

        // Assert
        var length = RandomAccess.GetLength(handle);
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task SetLength_ThrowsOnNegativeLength()
    {
        // Arrange
        File.WriteAllBytes(testFilePath, new byte[10]);
        using var handle = File.OpenHandle(testFilePath, FileMode.Open, FileAccess.ReadWrite);

        // Act & Assert
        await Assert.That(() => RandomAccess.SetLength(handle, -1))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task FlushToDisk_FlushesSuccessfully()
    {
        // Arrange
        using var handle = File.OpenHandle(testFilePath, FileMode.Create, FileAccess.ReadWrite);
        var data = new byte[] { 1, 2, 3, 4, 5 };
        RandomAccess.Write(handle, data, 0);

        // Act - should not throw
        RandomAccess.FlushToDisk(handle);

        // Assert - verify data was written
        var readBuffer = new byte[5];
        RandomAccess.Read(handle, readBuffer, 0);
        await Assert.That(readBuffer.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task FlushToDisk_WorksWithLargeFile()
    {
        // Arrange
        using var handle = File.OpenHandle(testFilePath, FileMode.Create, FileAccess.ReadWrite);
        var data = new byte[1024 * 1024]; // 1 MB
        new Random(42).NextBytes(data);
        RandomAccess.Write(handle, data, 0);

        // Act - should not throw
        RandomAccess.FlushToDisk(handle);

        // Assert
        var length = RandomAccess.GetLength(handle);
        await Assert.That(length).IsEqualTo(1024 * 1024);
    }

    [Test]
    public async Task FlushToDisk_WorksAfterMultipleWrites()
    {
        // Arrange
        using var handle = File.OpenHandle(testFilePath, FileMode.Create, FileAccess.ReadWrite);

        // Multiple writes at different offsets
        RandomAccess.Write(handle, new byte[] { 1, 2, 3 }, 0);
        RandomAccess.Write(handle, new byte[] { 4, 5, 6 }, 100);
        RandomAccess.Write(handle, new byte[] { 7, 8, 9 }, 200);

        // Act - should not throw
        RandomAccess.FlushToDisk(handle);

        // Assert
        var buffer1 = new byte[3];
        var buffer2 = new byte[3];
        var buffer3 = new byte[3];

        RandomAccess.Read(handle, buffer1, 0);
        RandomAccess.Read(handle, buffer2, 100);
        RandomAccess.Read(handle, buffer3, 200);

        await Assert.That(buffer1.SequenceEqual(new byte[] { 1, 2, 3 })).IsTrue();
        await Assert.That(buffer2.SequenceEqual(new byte[] { 4, 5, 6 })).IsTrue();
        await Assert.That(buffer3.SequenceEqual(new byte[] { 7, 8, 9 })).IsTrue();
    }
}

#endif
