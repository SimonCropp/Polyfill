#if FeatureCompression
using System.IO.Compression;

partial class PolyfillTests
{
    [Test]
    public async Task ExtractToDirectory_Extracts_All_Entries()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("test.txt");
            using var writer = new StreamWriter(entry.Open());
            writer.Write("content");
        }

        memStream.Position = 0;
        using var extractArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            extractArchive.ExtractToDirectory(tempDir, overwriteFiles: false);
            var filePath = Path.Combine(tempDir, "test.txt");
            await Assert.That(File.Exists(filePath)).IsTrue();
            await Assert.That(File.ReadAllText(filePath)).IsEqualTo("content");
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ExtractToDirectory_Overwrites_Existing_File_When_True()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("test.txt");
            using var writer = new StreamWriter(entry.Open());
            writer.Write("new content");
        }

        memStream.Position = 0;
        using var extractArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var filePath = Path.Combine(tempDir, "test.txt");
        File.WriteAllText(filePath, "old content");

        try
        {
            extractArchive.ExtractToDirectory(tempDir, overwriteFiles: true);
            await Assert.That(File.ReadAllText(filePath)).IsEqualTo("new content");
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public void ExtractToDirectory_Throws_When_Entry_Would_Escape_Directory()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            archive.CreateEntry("../evil.txt");
        }

        memStream.Position = 0;
        using var extractArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            Assert.Throws<IOException>(() =>
                extractArchive.ExtractToDirectory(tempDir, overwriteFiles: false));
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_ExtractsAllEntries()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        try
        {
            // Create a test archive
            using (var archive = ZipFile.Open(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var writer = new StreamWriter(entry.Open());
                writer.Write("async content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir);

            // Assert
            var filePath = Path.Combine(tempDir, "test.txt");
            Assert.True(File.Exists(filePath));
            Assert.AreEqual("async content", File.ReadAllText(filePath));
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithOverwrite_OverwritesFiles()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var filePath = Path.Combine(tempDir, "test.txt");
        File.WriteAllText(filePath, "old content");

        try
        {
            // Create a test archive
            using (var archive = ZipFile.Open(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var writer = new StreamWriter(entry.Open());
                writer.Write("new content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, overwriteFiles: true);

            // Assert
            Assert.AreEqual("new content", File.ReadAllText(filePath));
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithEncoding_ExtractsCorrectly()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        try
        {
            // Create a test archive
            using (var archive = ZipFile.Open(tempArchive, ZipArchiveMode.Create, Encoding.UTF8))
            {
                var entry = archive.CreateEntry("test.txt");
                using var writer = new StreamWriter(entry.Open());
                writer.Write("encoded content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, Encoding.UTF8);

            // Assert
            var filePath = Path.Combine(tempDir, "test.txt");
            Assert.True(File.Exists(filePath));
            Assert.AreEqual("encoded content", File.ReadAllText(filePath));
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithCancellation_CanBeCancelled()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var cancelSource = new CancelSource();

        try
        {
            // Create a test archive
            using (var archive = ZipFile.Open(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var writer = new StreamWriter(entry.Open());
                writer.Write("content");
            }

            cancelSource.Cancel();

            // Act & Assert - Polyfill throws TaskCanceledException, native .NET 10+ throws OperationCanceledException
            Assert.That(async () => await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, cancelSource.Token),
                Throws.InstanceOf<OperationCanceledException>());
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        Directory.CreateDirectory(tempDir);
        File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");

        try
        {
            // Act
            await ZipFile.CreateFromDirectoryAsync(tempDir, tempArchive);

            // Assert
            Assert.True(File.Exists(tempArchive));
            using var archive = ZipFile.OpenRead(tempArchive);
            Assert.AreEqual(1, archive.Entries.Count);
            Assert.AreEqual("test.txt", archive.Entries[0].Name);
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithCompressionLevel_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        Directory.CreateDirectory(tempDir);
        File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");

        try
        {
            // Act
            await ZipFile.CreateFromDirectoryAsync(
                tempDir,
                tempArchive,
                CompressionLevel.Fastest,
                includeBaseDirectory: false);

            // Assert
            Assert.True(File.Exists(tempArchive));
            using var archive = ZipFile.OpenRead(tempArchive);
            Assert.AreEqual(1, archive.Entries.Count);
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithEncoding_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        Directory.CreateDirectory(tempDir);
        File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");

        try
        {
            // Act
            await ZipFile.CreateFromDirectoryAsync(
                tempDir,
                tempArchive,
                CompressionLevel.Optimal,
                includeBaseDirectory: false,
                Encoding.UTF8);

            // Assert
            Assert.True(File.Exists(tempArchive));
            using var archive = ZipFile.OpenRead(tempArchive);
            Assert.AreEqual(1, archive.Entries.Count);
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithCancellation_CanBeCancelled()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
        Directory.CreateDirectory(tempDir);
        File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
        var cancelSource = new CancelSource();

        try
        {
            cancelSource.Cancel();

            // Act & Assert - Polyfill throws TaskCanceledException, native .NET 10+ throws OperationCanceledException
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
                await ZipFile.CreateFromDirectoryAsync(tempDir, tempArchive, cancelSource.Token));
        }
        finally
        {
            if (File.Exists(tempArchive))
                File.Delete(tempArchive);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }
}
#endif