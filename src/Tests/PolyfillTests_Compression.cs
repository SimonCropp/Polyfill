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
}
#endif