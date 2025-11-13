[TestFixture]
public class PathTests
{
#if FeatureMemory
    [Test]
    public void GetDirectoryName() =>
        Assert.AreEqual("dir", Path.GetDirectoryName("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void GetFileName() =>
        Assert.AreEqual("file.txt", Path.GetFileName("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void GetFileNameWithoutExtension() =>
        Assert.AreEqual("file", Path.GetFileNameWithoutExtension("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void HasExtension()
    {
        Assert.True(Path.HasExtension("file.txt".AsSpan()));
        Assert.False(Path.HasExtension("file".AsSpan()));
    }

    [Test]
    public void GetExtension() =>
        Assert.AreEqual(".txt", Path.GetExtension("file.txt".AsSpan()).ToString());

    [Test]
    public void Combine()
    {
        ReadOnlySpan<string> paths =
        [
            "folder1",
            "folder2",
            "file.txt"
        ];

        var result = Path.Combine(paths);

        Assert.AreEqual("folder1\\folder2\\file.txt", result.Replace('/','\\'));
    }
#endif

    [Test]
    public void EndsInDirectorySeparator()
    {
        #if FeatureMemory
        Assert.False(Path.EndsInDirectorySeparator("file.txt".AsSpan()));
        Assert.True(Path.EndsInDirectorySeparator("path/".AsSpan()));
        #endif
        Assert.False(Path.EndsInDirectorySeparator("file.txt"));
        Assert.True(Path.EndsInDirectorySeparator("path/"));
    }

    [Test]
    public void Exists()
    {
        Assert.False(Path.Exists(null));
        Assert.False(Path.Exists(""));
        Assert.False(Path.Exists("file.txt"));
        Assert.True(Path.Exists(Environment.CurrentDirectory));
    }

    [Test]
    [Platform("win")]
    [TestCase(@"C:\folder\", @"C:\folder")]
    [TestCase(@"C:/folder/", @"C:/folder")]
    [TestCase(@"/folder/", @"/folder")]
    [TestCase(@"\folder\", @"\folder")]
    [TestCase(@"folder\", @"folder")]
    [TestCase(@"folder/", @"folder")]
    [TestCase(@"C:\", @"C:\")]
    [TestCase(@"C:/", @"C:/")]
    [TestCase(@"", @"")]
    [TestCase(@"/", @"/")]
    [TestCase(@"\", @"\")]
    [TestCase(@"\\server\share\", @"\\server\share")]
    [TestCase(@"\\server\share\folder\", @"\\server\share\folder")]
    [TestCase(@"\\?\C:\", @"\\?\C:\")]
    [TestCase(@"\\?\C:\folder\", @"\\?\C:\folder")]
    [TestCase(@"\\?\UNC\", @"\\?\UNC\")]
    [TestCase(@"\\?\UNC\a\", @"\\?\UNC\a\")]
    [TestCase(@"\\?\UNC\a\folder\", @"\\?\UNC\a\folder")]
    public void TrimEndingDirectorySeparator_Windows(string input, string expected)
    {
        #if FeatureMemory
        Assert.AreEqual(expected, Path.TrimEndingDirectorySeparator(input.AsSpan()).ToString());
        #endif
        Assert.AreEqual(expected, Path.TrimEndingDirectorySeparator(input));
    }

    [Test]
    [Platform(Exclude = "win")]
    [TestCase(@"/folder/", @"/folder")]
    [TestCase(@"folder/", @"folder")]
    [TestCase(@"", @"")]
    [TestCase(@"/", @"/")]
    public void TrimEndingDirectorySeparator_Unix(string input, string expected)
    {
        #if FeatureMemory
        Assert.AreEqual(expected, Path.TrimEndingDirectorySeparator(input.AsSpan()).ToString());
        #endif
        Assert.AreEqual(expected, Path.TrimEndingDirectorySeparator(input));
    }
}