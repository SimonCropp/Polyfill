public class PathTests
{
#if FeatureMemory
    [Test]
    public async Task GetDirectoryName() =>
        await Assert.That(Path.GetDirectoryName("dir/file.txt".AsSpan()).ToString()).IsEqualTo("dir");

    [Test]
    public async Task GetFileName() =>
        await Assert.That(Path.GetFileName("dir/file.txt".AsSpan()).ToString()).IsEqualTo("file.txt");

    [Test]
    public async Task GetFileNameWithoutExtension() =>
        await Assert.That(Path.GetFileNameWithoutExtension("dir/file.txt".AsSpan()).ToString()).IsEqualTo("file");

    [Test]
    public async Task HasExtension()
    {
        await Assert.That(Path.HasExtension("file.txt".AsSpan())).IsTrue();
        await Assert.That(Path.HasExtension("file".AsSpan())).IsFalse();
    }

    [Test]
    public async Task GetExtension() =>
        await Assert.That(Path.GetExtension("file.txt".AsSpan()).ToString()).IsEqualTo(".txt");

    [Test]
    public async Task IsPathRooted()
    {
        await Assert.That(Path.IsPathRooted("/root/path".AsSpan())).IsTrue();
        await Assert.That(Path.IsPathRooted("relative/path".AsSpan())).IsFalse();
    }

    [Test]
    public async Task Combine()
    {
        ReadOnlySpan<string> paths =
        [
            "folder1",
            "folder2",
            "file.txt"
        ];

        var result = Path.Combine(paths);

        await Assert.That(result.Replace('/', '\\')).IsEqualTo("folder1\\folder2\\file.txt");
    }
#endif

    [Test]
    public async Task GetRelativePath()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        // Sibling directories
        await Assert.That(Path.GetRelativePath(@"C:\Program Files\Common Files", @"C:\Program Files\Microsoft"))
            .IsEqualTo(@"..\Microsoft");

        // Child directory
        await Assert.That(Path.GetRelativePath(@"C:\Program Files\", @"C:\Program Files\Microsoft"))
            .IsEqualTo("Microsoft");

        // Same path
        await Assert.That(Path.GetRelativePath(@"C:\folder", @"C:\folder"))
            .IsEqualTo(".");

        // Same path with trailing separator
        await Assert.That(Path.GetRelativePath(@"C:\folder\", @"C:\folder"))
            .IsEqualTo(".");

        // Multiple levels up
        await Assert.That(Path.GetRelativePath(@"C:\a\b\c", @"C:\a"))
            .IsEqualTo(@"..\..");

        // Deeply nested child
        await Assert.That(Path.GetRelativePath(@"C:\a", @"C:\a\b\c\d"))
            .IsEqualTo(@"b\c\d");

        // Case-insensitive on Windows
        await Assert.That(Path.GetRelativePath(@"C:\Folder", @"C:\folder"))
            .IsEqualTo(".");

        // Different roots returns path as-is
        var result = Path.GetRelativePath(@"C:\folder", @"D:\other");
        await Assert.That(result).IsEqualTo(@"D:\other");

        // Path with spaces
        await Assert.That(Path.GetRelativePath(@"C:\my folder", @"C:\my folder\sub dir"))
            .IsEqualTo(@"sub dir");

        // Path with special characters (# ? %)
        await Assert.That(Path.GetRelativePath(@"C:\base", @"C:\base\file#1"))
            .IsEqualTo(@"file#1");
        await Assert.That(Path.GetRelativePath(@"C:\base", @"C:\base\file%20name"))
            .IsEqualTo(@"file%20name");
    }

    [Test]
    public async Task GetRelativePath_ThrowsOnNull()
    {
        await Assert.That(() => Path.GetRelativePath(null!, "path")).Throws<ArgumentNullException>();
        await Assert.That(() => Path.GetRelativePath("path", null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task GetRelativePath_ThrowsOnEmpty()
    {
        await Assert.That(() => Path.GetRelativePath("", "path")).Throws<ArgumentException>();
        await Assert.That(() => Path.GetRelativePath("path", "")).Throws<ArgumentException>();
    }

    [Test]
    public async Task EndsInDirectorySeparator()
    {
        #if FeatureMemory
        await Assert.That(Path.EndsInDirectorySeparator("file.txt".AsSpan())).IsFalse();
        await Assert.That(Path.EndsInDirectorySeparator("path/".AsSpan())).IsTrue();
        #endif
        await Assert.That(Path.EndsInDirectorySeparator("file.txt")).IsFalse();
        await Assert.That(Path.EndsInDirectorySeparator("path/")).IsTrue();
    }

    [Test]
    public async Task Exists()
    {
        await Assert.That(Path.Exists(null)).IsFalse();
        await Assert.That(Path.Exists("")).IsFalse();
        await Assert.That(Path.Exists("file.txt")).IsFalse();
        await Assert.That(Path.Exists(Environment.CurrentDirectory)).IsTrue();
    }

    [Test]
    [Arguments(@"C:\folder\", @"C:\folder")]
    [Arguments("C:/folder/", "C:/folder")]
    [Arguments("/folder/", "/folder")]
    [Arguments(@"\folder\", @"\folder")]
    [Arguments(@"folder\", "folder")]
    [Arguments("folder/", "folder")]
    [Arguments(@"C:\", @"C:\")]
    [Arguments("C:/", "C:/")]
    [Arguments("", "")]
    [Arguments("/", "/")]
    [Arguments(@"\", @"\")]
    [Arguments(@"\\server\share\", @"\\server\share")]
    [Arguments(@"\\server\share\folder\", @"\\server\share\folder")]
    [Arguments(@"\\?\C:\", @"\\?\C:\")]
    [Arguments(@"\\?\C:\folder\", @"\\?\C:\folder")]
    [Arguments(@"\\?\UNC\", @"\\?\UNC\")]
    [Arguments(@"\\?\UNC\a\", @"\\?\UNC\a\")]
    [Arguments(@"\\?\UNC\a\folder\", @"\\?\UNC\a\folder")]
    public async Task TrimEndingDirectorySeparator_Windows(string input, string expected)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }
        #if FeatureMemory
        await Assert.That(Path.TrimEndingDirectorySeparator(input.AsSpan()).ToString()).IsEqualTo(expected);
        #endif
        await Assert.That(Path.TrimEndingDirectorySeparator(input)).IsEqualTo(expected);
    }

    [Test]
    [Arguments("/folder/", "/folder")]
    [Arguments("folder/", "folder")]
    [Arguments("", "")]
    [Arguments("/", "/")]
    public async Task TrimEndingDirectorySeparator_Unix(string input, string expected)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }
        #if FeatureMemory
        await Assert.That(Path.TrimEndingDirectorySeparator(input.AsSpan()).ToString()).IsEqualTo(expected);
        #endif
        await Assert.That(Path.TrimEndingDirectorySeparator(input)).IsEqualTo(expected);
    }
}
