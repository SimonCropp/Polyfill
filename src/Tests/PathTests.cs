[TestFixture]
public class PathTests
{
#if FeatureMemory
    [Test]
    public void GetDirectoryName() =>
        Assert.AreEqual("dir", PathPolyfill.GetDirectoryName("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void GetFileName() =>
        Assert.AreEqual("file.txt", PathPolyfill.GetFileName("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void GetFileNameWithoutExtension() =>
        Assert.AreEqual("file", PathPolyfill.GetFileNameWithoutExtension("dir/file.txt".AsSpan()).ToString());

    [Test]
    public void HasExtension()
    {
        Assert.True(PathPolyfill.HasExtension("file.txt".AsSpan()));
        Assert.False(PathPolyfill.HasExtension("file".AsSpan()));
    }

    [Test]
    public void GetExtension() =>
        Assert.AreEqual(".txt", PathPolyfill.GetExtension("file.txt".AsSpan()).ToString());

    [Test]
    public void Combine()
    {
        ReadOnlySpan<string> paths =
        [
            "folder1",
            "folder2",
            "file.txt"
        ];

        var result = PathPolyfill.Combine(paths);

        Assert.AreEqual("folder1\\folder2\\file.txt", result);
    }
#endif

    [Test]
    public void EndsInDirectorySeparator()
    {
        #if FeatureMemory
        Assert.False(PathPolyfill.EndsInDirectorySeparator("file.txt".AsSpan()));
        Assert.True(PathPolyfill.EndsInDirectorySeparator("path/".AsSpan()));
        #endif
        Assert.False(PathPolyfill.EndsInDirectorySeparator("file.txt"));
        Assert.True(PathPolyfill.EndsInDirectorySeparator("path/"));
    }

    [Test]
    public void Exists()
    {
        Assert.False(PathPolyfill.Exists(null));
        Assert.False(PathPolyfill.Exists(""));
        Assert.False(PathPolyfill.Exists("file.txt"));
        Assert.True(PathPolyfill.Exists(Environment.CurrentDirectory));
    }
}