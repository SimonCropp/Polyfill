partial class PolyfillTests
{
#if NETFRAMEWORK || NETSTANDARD && !NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0
    [Test]
    public async Task Directory_EnumerateFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = Polyfill.EnumerateFiles(tempDir, "*.txt", options).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = Polyfill.GetFiles(tempDir, "*.txt", options);
            await Assert.That(files.Length).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var subDir = Path.Combine(tempDir, "subdir");
        Directory.CreateDirectory(subDir);
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = Polyfill.EnumerateDirectories(tempDir, "*", options).ToList();
            await Assert.That(dirs.Count).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var subDir = Path.Combine(tempDir, "subdir");
        Directory.CreateDirectory(subDir);
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = Polyfill.GetDirectories(tempDir, "*", options);
            await Assert.That(dirs.Length).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFileSystemEntries_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
            Directory.CreateDirectory(Path.Combine(tempDir, "subdir"));
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = Polyfill.EnumerateFileSystemEntries(tempDir, "*", options).ToList();
            await Assert.That(entries.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetFileSystemEntries_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
            Directory.CreateDirectory(Path.Combine(tempDir, "subdir"));
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = Polyfill.GetFileSystemEntries(tempDir, "*", options);
            await Assert.That(entries.Length).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFiles_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "root.txt"), "content");
            var subDir = Path.Combine(tempDir, "subdir");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(subDir, "nested.txt"), "content");

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var filesNoRecurse = Polyfill.EnumerateFiles(tempDir, "*.txt", optionsNoRecurse).ToList();
            await Assert.That(filesNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var filesRecurse = Polyfill.EnumerateFiles(tempDir, "*.txt", optionsRecurse).ToList();
            await Assert.That(filesRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFiles_AttributesToSkip()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            var normalFile = Path.Combine(tempDir, "normal.txt");
            var hiddenFile = Path.Combine(tempDir, "hidden.txt");
            File.WriteAllText(normalFile, "content");
            File.WriteAllText(hiddenFile, "content");
            File.SetAttributes(hiddenFile, FileAttributes.Hidden);

            var optionsSkipHidden = new EnumerationOptions { AttributesToSkip = FileAttributes.Hidden };
            var files = Polyfill.EnumerateFiles(tempDir, "*.txt", optionsSkipHidden).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
            await Assert.That(files[0]).IsEqualTo(normalFile);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateDirectories_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            var subDir1 = Path.Combine(tempDir, "sub1");
            Directory.CreateDirectory(subDir1);
            var subDir2 = Path.Combine(subDir1, "sub2");
            Directory.CreateDirectory(subDir2);

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var dirsNoRecurse = Polyfill.EnumerateDirectories(tempDir, "*", optionsNoRecurse).ToList();
            await Assert.That(dirsNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var dirsRecurse = Polyfill.EnumerateDirectories(tempDir, "*", optionsRecurse).ToList();
            await Assert.That(dirsRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }
#else
    [Test]
    public async Task Directory_EnumerateFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = Directory.EnumerateFiles(tempDir, "*.txt", options).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var files = Directory.GetFiles(tempDir, "*.txt", options);
            await Assert.That(files.Length).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var subDir = Path.Combine(tempDir, "subdir");
        Directory.CreateDirectory(subDir);
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = Directory.EnumerateDirectories(tempDir, "*", options).ToList();
            await Assert.That(dirs.Count).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetDirectories_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var subDir = Path.Combine(tempDir, "subdir");
        Directory.CreateDirectory(subDir);
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var dirs = Directory.GetDirectories(tempDir, "*", options);
            await Assert.That(dirs.Length).IsEqualTo(1);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFileSystemEntries_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            Directory.CreateDirectory(Path.Combine(tempDir, "subdir"));
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = Directory.EnumerateFileSystemEntries(tempDir, "*", options).ToList();
            await Assert.That(entries.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_GetFileSystemEntries_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "test.txt"), "content");
            Directory.CreateDirectory(Path.Combine(tempDir, "subdir"));
            var options = new EnumerationOptions { RecurseSubdirectories = false };
            var entries = Directory.GetFileSystemEntries(tempDir, "*", options);
            await Assert.That(entries.Length).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFiles_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            await File.WriteAllTextAsync(Path.Combine(tempDir, "root.txt"), "content");
            var subDir = Path.Combine(tempDir, "subdir");
            Directory.CreateDirectory(subDir);
            await File.WriteAllTextAsync(Path.Combine(subDir, "nested.txt"), "content");

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var filesNoRecurse = Directory.EnumerateFiles(tempDir, "*.txt", optionsNoRecurse).ToList();
            await Assert.That(filesNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var filesRecurse = Directory.EnumerateFiles(tempDir, "*.txt", optionsRecurse).ToList();
            await Assert.That(filesRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateFiles_AttributesToSkip()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            var normalFile = Path.Combine(tempDir, "normal.txt");
            var hiddenFile = Path.Combine(tempDir, "hidden.txt");
            await File.WriteAllTextAsync(normalFile, "content");
            await File.WriteAllTextAsync(hiddenFile, "content");
            File.SetAttributes(hiddenFile, FileAttributes.Hidden);

            var optionsSkipHidden = new EnumerationOptions { AttributesToSkip = FileAttributes.Hidden };
            var files = Directory.EnumerateFiles(tempDir, "*.txt", optionsSkipHidden).ToList();
            await Assert.That(files.Count).IsEqualTo(1);
            await Assert.That(files[0]).IsEqualTo(normalFile);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Test]
    public async Task Directory_EnumerateDirectories_RecurseSubdirectories()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            var subDir1 = Path.Combine(tempDir, "sub1");
            Directory.CreateDirectory(subDir1);
            var subDir2 = Path.Combine(subDir1, "sub2");
            Directory.CreateDirectory(subDir2);

            var optionsNoRecurse = new EnumerationOptions { RecurseSubdirectories = false };
            var dirsNoRecurse = Directory.EnumerateDirectories(tempDir, "*", optionsNoRecurse).ToList();
            await Assert.That(dirsNoRecurse.Count).IsEqualTo(1);

            var optionsRecurse = new EnumerationOptions { RecurseSubdirectories = true };
            var dirsRecurse = Directory.EnumerateDirectories(tempDir, "*", optionsRecurse).ToList();
            await Assert.That(dirsRecurse.Count).IsEqualTo(2);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }
#endif
}
