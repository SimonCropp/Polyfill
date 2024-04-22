partial class PolyfillTests
{
    [Test]
    public void IsAssignableTo()
    {
        Assert.True(typeof(List<string>).IsAssignableTo(typeof(IList)));
        Assert.True(typeof(List<string>).IsAssignableTo<IList>());
        Assert.False(typeof(List<string>).IsAssignableTo(typeof(string)));
        Assert.False(typeof(List<string>).IsAssignableTo<string>());
        Assert.False(typeof(List<string>).IsAssignableTo(null));
    }

    [Test]
    public void IsAssignableFrom()
    {
        Assert.True(typeof(IList).IsAssignableFrom<List<string>>());
        Assert.False(typeof(string).IsAssignableFrom<List<string>>());
    }
}