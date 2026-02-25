partial class PolyfillTests
{
    [Test]
    public async Task DirectoryInfo_EnumerateFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = dirInfo.EnumerateFiles("*.txt", options).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_GetFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = dirInfo.GetFiles("*.txt", options);
            await Assert.That(files.Length).IsEqualTo(1);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        var subDir = dirInfo.CreateSubdirectory("subdir");
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = dirInfo.EnumerateDirectories("*", options).ToList();
            await Assert.That(dirs.Count).IsEqualTo(1);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_GetDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        var subDir = dirInfo.CreateSubdirectory("subdir");
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = dirInfo.GetDirectories("*", options);
            await Assert.That(dirs.Length).IsEqualTo(1);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateFileSystemInfos_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            dirInfo.CreateSubdirectory("subdir");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = dirInfo.EnumerateFileSystemInfos("*", options).ToList();
            await Assert.That(entries.Count).IsEqualTo(2);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_GetFileSystemInfos_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            dirInfo.CreateSubdirectory("subdir");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = dirInfo.GetFileSystemInfos("*", options);
            await Assert.That(entries.Length).IsEqualTo(2);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateFiles_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "root.txt"), "content");
            var subDir = dirInfo.CreateSubdirectory("subdir");
            await File.WriteAllTextAsync(Path.Combine(subDir.FullName, "nested.txt"), "content");

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var filesNoRecurse = dirInfo.EnumerateFiles("*.txt", optionsNoRecurse).ToList();
            await Assert.That(filesNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var filesRecurse = dirInfo.EnumerateFiles("*.txt", optionsRecurse).ToList();
            await Assert.That(filesRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateFiles_AttributesToSkip()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            var normalFile = Path.Combine(tempDir, "normal.txt");
            var hiddenFile = Path.Combine(tempDir, "hidden.txt");
            await File.WriteAllTextAsync(normalFile, "content");
            await File.WriteAllTextAsync(hiddenFile, "content");
            File.SetAttributes(hiddenFile, FileAttributes.Hidden);

            var optionsSkipHidden = new EnumerationOptions { AttributesToSkip = FileAttributes.Hidden };
            var files = dirInfo.EnumerateFiles("*.txt", optionsSkipHidden).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
            await Assert.That(files[0].Name).IsEqualTo("normal.txt");
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateDirectories_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            var subDir1 = dirInfo.CreateSubdirectory("sub1");
            subDir1.CreateSubdirectory("sub2");

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var dirsNoRecurse = dirInfo.EnumerateDirectories("*", optionsNoRecurse).ToList();
            await Assert.That(dirsNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var dirsRecurse = dirInfo.EnumerateDirectories("*", optionsRecurse).ToList();
            await Assert.That(dirsRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_EnumerateDirectories_AttributesToSkip()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            var normalDir = dirInfo.CreateSubdirectory("normal");
            var hiddenDir = dirInfo.CreateSubdirectory("hidden");
            hiddenDir.Attributes |= FileAttributes.Hidden;

            var optionsSkipHidden = new EnumerationOptions { AttributesToSkip = FileAttributes.Hidden };
            var dirs = dirInfo.EnumerateDirectories("*", optionsSkipHidden).ToList();
            await Assert.That(dirs.Count).IsEqualTo(1);
            await Assert.That(dirs[0].Name).IsEqualTo("normal");
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }

    [Test]
    public async Task DirectoryInfo_GetFiles_MultipleFiles()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirInfo = new DirectoryInfo(tempDir);
        dirInfo.Create();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "file1.txt"), "content");
            await File.WriteAllTextAsync(Path.Combine(tempDir, "file2.txt"), "content");
            await File.WriteAllTextAsync(Path.Combine(tempDir, "file3.log"), "content");

            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var txtFiles = dirInfo.GetFiles("*.txt", options);
            await Assert.That(txtFiles.Length).IsEqualTo(2);

            var allFiles = dirInfo.GetFiles("*", options);
            await Assert.That(allFiles.Length).IsEqualTo(3);
        }
        finally
        {
            dirInfo.Delete(true);
        }
    }
}
