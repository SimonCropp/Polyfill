partial class PolyfillTests
{
    [Test]
    public void IsAssignableTo()
    {
        Assert.True(typeof(List<string>).IsAssignableTo(typeof(IList)));
        Assert.False(typeof(List<string>).IsAssignableTo(typeof(string)));
        Assert.False(typeof(List<string>).IsAssignableTo(null));
    }
}