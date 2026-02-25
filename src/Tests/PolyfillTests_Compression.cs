#if FeatureCompression
using System.IO.Compression;

partial class PolyfillTests
{
    [Test]
    public async Task ZipArchiveEntry_Open_WithFileAccess()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("test.txt");
            using var stream = await entry.OpenAsync();
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync("content");
        }

        memStream.Position = 0;
        using var readArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var readEntry = readArchive.Entries[0];

        // ReSharper disable once MethodHasAsyncOverload
        using var readStream = readEntry.Open(FileAccess.Read);
        using var reader = new StreamReader(readStream);
        var result = await reader.ReadToEndAsync();
        await Assert.That(result).IsEqualTo("content");
    }

#if FeatureValueTask
    [Test]
    public async Task ZipArchiveEntry_OpenAsync_WithFileAccess()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("test.txt");
            // ReSharper disable once MethodHasAsyncOverload
            // ReSharper disable once MethodHasAsyncOverload
            using var stream = entry.Open();
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync("content");
        }

        memStream.Position = 0;
        using var readArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var readEntry = readArchive.Entries[0];

        using var readStream = await readEntry.OpenAsync(FileAccess.Read);
        using var reader = new StreamReader(readStream);
        var result = await reader.ReadToEndAsync();
        await Assert.That(result).IsEqualTo("content");
    }
#endif

    [Test]
    public async Task ExtractToDirectory_Extracts_All_Entries()
    {
        using var memStream = new MemoryStream();
        using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("test.txt");
            using var stream = await entry.OpenAsync();
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync("content");
        }

        memStream.Position = 0;
        using var extractArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            // ReSharper disable once MethodHasAsyncOverload
            extractArchive.ExtractToDirectory(tempDir, overwriteFiles: false);
            var filePath = Path.Combine(tempDir, "test.txt");
            await Assert.That(File.Exists(filePath)).IsTrue();
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("content");
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
            // ReSharper disable once MethodHasAsyncOverload
            using var stream = entry.Open();
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync("new content");
        }

        memStream.Position = 0;
        using var extractArchive = new ZipArchive(memStream, ZipArchiveMode.Read);
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var filePath = Path.Combine(tempDir, "test.txt");
        await File.WriteAllTextAsync(filePath, "old content");

        try
        {
            // ReSharper disable once MethodHasAsyncOverload
            extractArchive.ExtractToDirectory(tempDir, overwriteFiles: true);
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("new content");
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task ExtractToDirectory_Throws_When_Entry_Would_Escape_Directory()
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
            await Assert.That(() =>
                extractArchive.ExtractToDirectory(tempDir, overwriteFiles: false)).Throws<IOException>();
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
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        try
        {
            // Create a test archive
            using (var archive = await ZipFile.OpenAsync(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var stream = await entry.OpenAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync("async content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir);

            // Assert
            var filePath = Path.Combine(tempDir, "test.txt");
            await Assert.That(File.Exists(filePath)).IsTrue();
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("async content");
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithOverwrite_OverwritesFiles()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var filePath = Path.Combine(tempDir, "test.txt");
        await File.WriteAllTextAsync(filePath, "old content");

        try
        {
            // Create a test archive
            using (var archive = await ZipFile.OpenAsync(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var stream = await entry.OpenAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync("new content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, overwriteFiles: true);

            // Assert
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("new content");
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithEncoding_ExtractsCorrectly()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        try
        {
            // Create a test archive
            using (var archive = await ZipFile.OpenAsync(tempArchive, ZipArchiveMode.Create, Encoding.UTF8))
            {
                var entry = archive.CreateEntry("test.txt");
                using var stream = await entry.OpenAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync("encoded content");
            }

            // Act
            await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, Encoding.UTF8);

            // Assert
            var filePath = Path.Combine(tempDir, "test.txt");
            await Assert.That(File.Exists(filePath)).IsTrue();
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("encoded content");
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_ExtractToDirectoryAsync_WithCancellation_CanBeCancelled()
    {
        // Arrange
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var cancelSource = new CancelSource();

        try
        {
            // Create a test archive
            using (var archive = ZipFile.Open(tempArchive, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("test.txt");
                using var stream = entry.Open();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync("content");
            }

            await cancelSource.CancelAsync();

            // Act & Assert - Polyfill throws TaskCanceledException, native .NET 10+ throws OperationCanceledException
            await Assert.That(async () => await ZipFile.ExtractToDirectoryAsync(tempArchive, tempDir, cancelSource.Token))
                .Throws<OperationCanceledException>();
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        Directory.CreateDirectory(tempDir);
        await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");

        try
        {
            // Act
            await ZipFile.CreateFromDirectoryAsync(tempDir, tempArchive);

            // Assert
            await Assert.That(File.Exists(tempArchive)).IsTrue();
            using var archive = await ZipFile.OpenReadAsync(tempArchive);
            await Assert.That(archive.Entries.Count).IsEqualTo(1);
            await Assert.That(archive.Entries[0].Name).IsEqualTo("test.txt");
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithCompressionLevel_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        Directory.CreateDirectory(tempDir);
        await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");

        try
        {
            // Act
            await ZipFile.CreateFromDirectoryAsync(
                tempDir,
                tempArchive,
                CompressionLevel.Fastest,
                includeBaseDirectory: false);

            // Assert
            await Assert.That(File.Exists(tempArchive)).IsTrue();
            using var archive = await ZipFile.OpenReadAsync(tempArchive);
            await Assert.That(archive.Entries.Count).IsEqualTo(1);
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithEncoding_CreatesArchive()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        Directory.CreateDirectory(tempDir);
        await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");

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
            await Assert.That(File.Exists(tempArchive)).IsTrue();
            using var archive = await ZipFile.OpenReadAsync(tempArchive);
            await Assert.That(archive.Entries.Count).IsEqualTo(1);
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Test]
    public async Task ZipFile_CreateFromDirectoryAsync_WithCancellation_CanBeCancelled()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempArchive = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        Directory.CreateDirectory(tempDir);
        await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
        var cancelSource = new CancelSource();

        try
        {
            await cancelSource.CancelAsync();

            // Act & Assert - Polyfill throws TaskCanceledException, native .NET 10+ throws OperationCanceledException
            await Assert.That(async () =>
                await ZipFile.CreateFromDirectoryAsync(tempDir, tempArchive, cancelSource.Token)).Throws<OperationCanceledException>();
        }
        finally
        {
            if (File.Exists(tempArchive))
            {
                File.Delete(tempArchive);
            }

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
#endif