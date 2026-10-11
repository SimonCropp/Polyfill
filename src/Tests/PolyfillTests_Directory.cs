partial class PolyfillTests
{
    [Test]
    public async Task Directory_EnumerateFiles_WithEnumerationOptions()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
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
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
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
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
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
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "content");
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
            File.WriteAllText(Path.Combine(tempDir, "root.txt"), "content");
            var subDir = Path.Combine(tempDir, "subdir");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(subDir, "nested.txt"), "content");

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
            File.WriteAllText(normalFile, "content");
            File.WriteAllText(hiddenFile, "content");
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

    static string CreateEnumerationTree()
    {
        var root = Path.Combine(Path.GetTempPath(), "polyfill_enum_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(root, "sub", "deep", "deeper"));
        File.WriteAllText(Path.Combine(root, "a.txt"), "");
        File.WriteAllText(Path.Combine(root, "B.TXT"), "");
        File.WriteAllText(Path.Combine(root, "LICENSE"), "");
        File.WriteAllText(Path.Combine(root, "sub", "c.txt"), "");
        File.WriteAllText(Path.Combine(root, "sub", "deep", "d.txt"), "");
        File.WriteAllText(Path.Combine(root, "sub", "deep", "deeper", "e.txt"), "");
        return root;
    }

    // paths relative to root, with forward slashes, in ordinal order
    static string DescribeEntries(string root, IEnumerable<string> paths) =>
        string.Join("|", paths
            .Select(_ => _.Substring(root.Length + 1).Replace('\\', '/'))
            .OrderBy(_ => _, StringComparer.Ordinal));

    [Test]
    public async Task Directory_EnumerationOptions_MatchTypeAndCasing()
    {
        var root = CreateEnumerationTree();
        try
        {
            string Files(string pattern, EnumerationOptions options) =>
                DescribeEntries(root, Directory.EnumerateFiles(root, pattern, options));

            // MatchType.Simple is the default: "*.*" needs a period in the name
            await Assert.That(Files("*.*", new())).IsEqualTo("B.TXT|a.txt");
            await Assert.That(Files("*", new())).IsEqualTo("B.TXT|LICENSE|a.txt");
            await Assert.That(Files("*.", new())).IsEqualTo("");
            await Assert.That(Files("", new())).IsEqualTo("");
            await Assert.That(Files("a.txt", new())).IsEqualTo("a.txt");
            await Assert.That(Files("?", new())).IsEqualTo("");
            await Assert.That(Files("?????", new())).IsEqualTo("B.TXT|a.txt");

            EnumerationOptions Win32() => new() {MatchType = MatchType.Win32};
            await Assert.That(Files("*.*", Win32())).IsEqualTo("B.TXT|LICENSE|a.txt");
            await Assert.That(Files("", Win32())).IsEqualTo("B.TXT|LICENSE|a.txt");
            await Assert.That(Files("*.", Win32())).IsEqualTo("LICENSE");
            await Assert.That(Files("LICENSE.*", Win32())).IsEqualTo("LICENSE");
            await Assert.That(Files("????????.???", Win32())).IsEqualTo("B.TXT|LICENSE|a.txt");

            await Assert.That(Files("*.txt", new() {MatchCasing = MatchCasing.CaseInsensitive})).IsEqualTo("B.TXT|a.txt");
            await Assert.That(Files("*.txt", new() {MatchCasing = MatchCasing.CaseSensitive})).IsEqualTo("a.txt");
            await Assert.That(Files("*.TXT", new() {MatchCasing = MatchCasing.CaseSensitive})).IsEqualTo("B.TXT");
            await Assert.That(Files("b.*", new() {MatchCasing = MatchCasing.CaseSensitive, MatchType = MatchType.Win32})).IsEqualTo("");
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    // EnumerationOptions.MaxRecursionDepth was added in net6
#if NETFRAMEWORK || NET6_0_OR_GREATER
    [Test]
    public async Task Directory_EnumerationOptions_MaxRecursionDepth()
    {
        var root = CreateEnumerationTree();
        try
        {
            EnumerationOptions Depth(int depth) =>
                new()
                {
                    RecurseSubdirectories = true,
                    MaxRecursionDepth = depth,
                    MatchCasing = MatchCasing.CaseInsensitive
                };

            string Files(EnumerationOptions options) =>
                DescribeEntries(root, Directory.GetFiles(root, "*.txt", options));

            await Assert.That(Files(new() {RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive}))
                .IsEqualTo("B.TXT|a.txt|sub/c.txt|sub/deep/d.txt|sub/deep/deeper/e.txt");
            await Assert.That(Files(Depth(2))).IsEqualTo("B.TXT|a.txt|sub/c.txt|sub/deep/d.txt");
            await Assert.That(Files(Depth(1))).IsEqualTo("B.TXT|a.txt|sub/c.txt");
            await Assert.That(Files(Depth(0))).IsEqualTo("B.TXT|a.txt");

            await Assert.That(DescribeEntries(root, Directory.GetDirectories(root, "*", Depth(1)))).IsEqualTo("sub|sub/deep");
            await Assert.That(DescribeEntries(root, Directory.GetFileSystemEntries(root, "d*", Depth(1)))).IsEqualTo("sub/deep");
            await Assert.That(DescribeEntries(root, Directory.GetFileSystemEntries(root, "d*", Depth(2)))).IsEqualTo("sub/deep|sub/deep/d.txt|sub/deep/deeper");

            // a directory in the pattern is applied to the path
            await Assert.That(DescribeEntries(root, Directory.GetFiles(root, Path.Combine("sub", "*.txt"), Depth(1)))).IsEqualTo("sub/c.txt|sub/deep/d.txt");
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }
#endif

    [Test]
    public async Task Directory_EnumerationOptions_SkippedDirectoryIsNotRecursed()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // the hidden attribute can only be set on Windows
            return;
        }

        var root = CreateEnumerationTree();
        try
        {
            var skipped = Directory.CreateDirectory(Path.Combine(root, "skipped"));
            File.WriteAllText(Path.Combine(skipped.FullName, "f.txt"), "");
            skipped.Attributes |= FileAttributes.Hidden;
            File.WriteAllText(Path.Combine(root, "hidden.txt"), "");
            File.SetAttributes(Path.Combine(root, "hidden.txt"), FileAttributes.Hidden);

            // by default hidden entries are skipped, including everything below a hidden directory
            var defaults = new EnumerationOptions {RecurseSubdirectories = true};
            await Assert.That(DescribeEntries(root, Directory.GetFiles(root, "*.txt", defaults)))
                .IsEqualTo("B.TXT|a.txt|sub/c.txt|sub/deep/d.txt|sub/deep/deeper/e.txt");
            await Assert.That(DescribeEntries(root, Directory.GetDirectories(root, "*", defaults)))
                .IsEqualTo("sub|sub/deep|sub/deep/deeper");

            var skipNothing = new EnumerationOptions {RecurseSubdirectories = true, AttributesToSkip = 0};
            await Assert.That(DescribeEntries(root, Directory.GetFiles(root, "*.txt", skipNothing)))
                .IsEqualTo("B.TXT|a.txt|hidden.txt|skipped/f.txt|sub/c.txt|sub/deep/d.txt|sub/deep/deeper/e.txt");
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [Test]
    public async Task Directory_EnumerationOptions_IgnoreInaccessible()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // uses icacls to make a directory inaccessible
            return;
        }

        static void Icacls(string path, string argument)
        {
            using var process = System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo("icacls", '"' + path + "\" " + argument)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                })!;
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new("icacls " + argument + " failed with exit code " + process.ExitCode);
            }
        }

        var root = CreateEnumerationTree();
        var denied = Path.Combine(root, "sub", "deep");
        // deny Everyone the right to list the directory
        Icacls(denied, "/deny *S-1-1-0:(RD)");
        try
        {
            // IgnoreInaccessible is true by default
            var defaults = new EnumerationOptions {RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive};
            await Assert.That(DescribeEntries(root, Directory.GetFiles(root, "*.txt", defaults)))
                .IsEqualTo("B.TXT|a.txt|sub/c.txt");
            await Assert.That(DescribeEntries(root, Directory.GetDirectories(root, "*", defaults)))
                .IsEqualTo("sub|sub/deep");

            var strict = new EnumerationOptions {RecurseSubdirectories = true, IgnoreInaccessible = false};
            await Assert.That(() => Directory.GetFiles(root, "*.txt", strict)).Throws<UnauthorizedAccessException>();
        }
        finally
        {
            Icacls(denied, "/remove:d *S-1-1-0");
            Directory.Delete(root, true);
        }
    }
}
