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
}