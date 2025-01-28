[TestFixture]
public class FilePolyfillTests
{
    const string TestFilePath = "testfile.txt";

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

#if FeatureMemory
    [Test]
    public async Task AppendAllBytes()
    {
        var data = "Hello, World!"u8.ToArray();
        // ReSharper disable once MethodHasAsyncOverload
        FilePolyfill.AppendAllBytes(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }

    [Test]
    public async Task AppendAllBytesAsync()
    {
        var data = "Hello, Async World!"u8.ToArray();
        await FilePolyfill.AppendAllBytesAsync(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
#endif

    [Test]
    public async Task AppendAllTextAsync()
    {
        var content = "Hello, Async Text!";
        await FilePolyfill.AppendAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await FilePolyfill.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }

    #if FeatureMemory
    [Test]
    public async Task WriteAllBytesAsync()
    {
        var data = "Hello, Write Bytes!"u8.ToArray();
        await FilePolyfill.WriteAllBytesAsync(TestFilePath, data);

        var result = await FilePolyfill.ReadAllBytesAsync(TestFilePath);
        Assert.AreEqual(data, result);
    }
    #endif

    [Test]
    public async Task WriteAllTextAsync()
    {
        var content = "Hello, Write Text!";
        await FilePolyfill.WriteAllTextAsync(TestFilePath, content, Encoding.UTF8);

        var result = await FilePolyfill.ReadAllTextAsync(TestFilePath);
        Assert.AreEqual(content, result);
    }
}