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
