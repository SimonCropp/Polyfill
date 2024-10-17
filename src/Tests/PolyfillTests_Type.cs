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

    public class WithGenericMethod
    {
        public void Method<T>(string value)
        {
        }
        public void Method<T>()
        {
        }
        public void Method<T, K>(string value)
        {
        }
        public void Method<T>(string value1, string value2)
        {
        }
    }

#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETCOREAPP2_0
    [Test]
    public void Type_GetMethod()
    {
        var type = typeof(WithGenericMethod);
        var method = type.GetMethod("Method", 1, BindingFlags.Public | BindingFlags.Instance, [typeof(string)])!;
        Assert.AreEqual("Method", method.Name);
        Assert.AreEqual(1, method.GetParameters().Length);
        Assert.AreEqual(1, method.GetGenericArguments().Length);
    }
#endif
}