public class DirectoryPolyfillTests
{
    [Test]
    public async Task CreateTempSubdirectory_NoPrefix()
    {
        var dir = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(dir.Exists).IsTrue();
        }
        finally
        {
            dir.Delete(true);
        }
    }

    [Test]
    public async Task CreateTempSubdirectory_WithPrefix()
    {
        var dir = Directory.CreateTempSubdirectory("testprefix");
        try
        {
            await Assert.That(dir.Exists).IsTrue();
            await Assert.That(dir.Name).StartsWith("testprefix");
        }
        finally
        {
            dir.Delete(true);
        }
    }
}
