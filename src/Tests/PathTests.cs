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

    // Expected values are what Path.GetRelativePath returns on net10 on Windows
    static readonly (string relativeTo, string path, string expected)[] relativePathCases =
    [
        (@"C:\", @"C:\", @"."),
        (@"C:\a", @"C:\a", @"."),
        (@"C:\a", @"C:\a\", @"."),
        (@"C:\a\", @"C:\a", @"."),
        (@"C:\a\", @"C:\a\", @"."),
        (@"C:\a", @"C:\a\b", @"b"),
        (@"C:\a", @"C:\a\b\", @"b\"),
        (@"C:\a\", @"C:\a\b", @"b"),
        (@"C:\a\", @"C:\a\b\", @"b\"),
        (@"C:\a\b", @"C:\a", @".."),
        (@"C:\a\b", @"C:\a\", @".."),
        (@"C:\a\b\", @"C:\a", @".."),
        (@"C:\a\b\", @"C:\a\", @".."),
        (@"C:\a\b\c", @"C:\a\x\y", @"..\..\x\y"),
        (@"C:\a\b\c", @"C:\a\x\y\", @"..\..\x\y\"),
        (@"C:\a\b\c\", @"C:\a\x\y\", @"..\..\x\y\"),
        (@"C:\a\b\c\d\e", @"C:\a\b", @"..\..\.."),
        (@"C:\a", @"C:\a\b\c\d", @"b\c\d"),
        (@"C:\Foodie", @"C:\Foobar", @"..\Foobar"),
        (@"C:\Foo", @"C:\Foobar", @"..\Foobar"),
        (@"C:\Foobar", @"C:\Foo", @"..\Foo"),
        (@"C:\Foo\Bar", @"C:\Bar\Bar", @"..\..\Bar\Bar"),
        (@"C:\Foo\Foo", @"C:\Foo\Bar", @"..\Bar"),
        (@"C:\", @"C:\a", @"a"),
        (@"C:\", @"C:\a\", @"a\"),
        (@"C:\a", @"C:\", @".."),
        (@"C:\a\b", @"C:\", @"..\.."),
        (@"C:\a\b\", @"C:\", @"..\.."),
        (@"C:\a", @"D:\a", @"D:\a"),
        (@"C:\a", @"D:\a\", @"D:\a\"),
        (@"C:\", @"D:\", @"D:\"),
        (@"C:\a\b", @"D:\a\b\c", @"D:\a\b\c"),
        (@"C:\A\b", @"c:\a\B\c", @"c"),
        (@"C:\FOO", @"c:\foo", @"."),
        (@"C:\FOO\", @"c:\foo", @"."),
        (@"C:\FOO\x", @"C:\foo\Y", @"..\Y"),
        (@"C:\Foo\Bar", @"C:\foo\baz\", @"..\baz\"),
        (@"c:\a", @"C:\b", @"..\b"),
        (@"C:\É", @"C:\é\x", @"x"),
        (@"C:\base", @"C:\base\file%20name", @"file%20name"),
        (@"C:\base", @"C:\base\a%2Db", @"a%2Db"),
        (@"C:\base", @"C:\base\a%2Db\", @"a%2Db\"),
        (@"C:\ba%2Dse\x", @"C:\ba%2Dse\y", @"..\y"),
        (@"C:\ba%2Dse\x", @"C:\ba-se\y", @"..\..\ba-se\y"),
        (@"C:\base", @"C:\base\100%", @"100%"),
        (@"C:\base", @"C:\base\%41", @"%41"),
        (@"C:\a%20b", @"C:\a b", @"..\a b"),
        (@"C:\base", @"C:\base\file#1", @"file#1"),
        (@"C:\base\a#b", @"C:\base\c#d\", @"..\c#d\"),
        (@"C:\base", @"C:\base\a&b+c=d", @"a&b+c=d"),
        (@"C:\my folder", @"C:\my folder\sub dir", @"sub dir"),
        (@"C:\my folder\one two", @"C:\my folder\three four\", @"..\three four\"),
        (@"C:\bäse\x", @"C:\bäse\ÿ", @"..\ÿ"),
        (@"C:\日本\x", @"C:\日本\語\", @"..\語\"),
        (@"C:/a/b", @"C:/a/c/", @"..\c\"),
        (@"C:/a/b/", @"C:\a\b\c/d", @"c\d"),
        (@"C:\a\b\..\c", @"C:\a\.\d", @"..\d"),
        (@"C:\a\b\..\..\c", @"C:\c\d\..\", @"."),
        (@"C:\a\.hidden", @"C:\a\..b", @"..\..b"),
        (@"C:\a\b.txt", @"C:\a\b.txt.bak", @"..\b.txt.bak"),
        (@"\\server\share\a", @"\\server\share\b", @"..\b"),
        (@"\\server\share\a", @"\\server\share\a\b\", @"b\"),
        (@"\\server\share\a", @"\\server\other\b", @"\\server\other\b"),
        (@"\\server\share\a", @"\\other\share\a", @"\\other\share\a"),
        (@"\\server\share", @"\\server\share\a\", @"a\"),
        (@"\\server\share\", @"\\server\share\a", @"a"),
        (@"\\Server\Share\a", @"\\server\share\A", @"."),
        (@"\\server\share\a", @"C:\a", @"C:\a"),
        (@"C:\a", @"\\server\share\a", @"\\server\share\a"),
        (@"a\b", @"a\c", @"..\c"),
        (@"a\b", @"a\c\", @"..\c\"),
        (@"a", @"a\b\", @"b\"),
        (@"a\b\", @"a", @".."),
        (@".", @"x", @"x"),
        (@"x\y", @".", @"..\.."),
        (@"..\a", @"..\b\c", @"..\b\c")
    ];

    [Test]
    public async Task GetRelativePath_MatchesBcl()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var failures = new List<string>();
        foreach (var (relativeTo, path, expected) in relativePathCases)
        {
            var actual = Path.GetRelativePath(relativeTo, path);
            if (actual != expected)
            {
                failures.Add($"GetRelativePath({relativeTo}, {path}) returned {actual} rather than {expected}");
            }
        }

        await Assert.That(string.Join(Environment.NewLine, failures)).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task GetRelativePath_ResultCombinesBackToPath()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        // Combining the result with the "from" path has to lead back to the "to" path
        foreach (var (relativeTo, path, _) in relativePathCases)
        {
            var relative = Path.GetRelativePath(relativeTo, path);
            var combined = Path.GetFullPath(Path.Combine(Path.GetFullPath(relativeTo), relative));

            await Assert.That(string.Equals(combined.TrimEnd('\\'), Path.GetFullPath(path).TrimEnd('\\'), StringComparison.OrdinalIgnoreCase)).IsTrue();
        }
    }

    [Test]
    public async Task GetRelativePath_RelativeInputsUseCurrentDirectory()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var current = Directory.GetCurrentDirectory();

        await Assert.That(Path.GetRelativePath(current, "child")).IsEqualTo("child");
        await Assert.That(Path.GetRelativePath("child", current)).IsEqualTo("..");
        await Assert.That(Path.GetRelativePath(".", Path.Combine(current, "a", "b") + "\\")).IsEqualTo(@"a\b\");
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
    public async Task Join()
    {
        await Assert.That(Path.Join("folder1", "folder2", "file.txt"))
            .IsEqualTo($"folder1{Path.DirectorySeparatorChar}folder2{Path.DirectorySeparatorChar}file.txt");

        await Assert.That(Path.Join("folder/", "file.txt")).IsEqualTo("folder/file.txt");

        await Assert.That(Path.Join("folder", "", "file.txt"))
            .IsEqualTo($"folder{Path.DirectorySeparatorChar}file.txt");

        await Assert.That(Path.Join("folder", null, "file.txt"))
            .IsEqualTo($"folder{Path.DirectorySeparatorChar}file.txt");

        await Assert.That(Path.Join(Array.Empty<string>())).IsEqualTo(string.Empty);
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
