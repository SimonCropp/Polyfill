partial class PolyfillTests
{
    [Test]
    public async Task HasSameMetadataDefinitionAs()
    {
        var method1 = typeof(string).GetMethod("Contains", [typeof(string)])!;
        var method2 = typeof(string).GetMethod("Contains", [typeof(string)])!;
        await Assert.That(method1.HasSameMetadataDefinitionAs(method2)).IsTrue();

        var method3 = typeof(string).GetMethod("StartsWith", [typeof(string)])!;
        await Assert.That(method1.HasSameMetadataDefinitionAs(method3)).IsFalse();
    }

    [Test]
    public async Task GetMemberWithSameMetadataDefinitionAs()
    {
        var method = typeof(string).GetMethod("Contains", [typeof(string)])!;
        var found = typeof(string).GetMemberWithSameMetadataDefinitionAs(method);
        await Assert.That(found.Name).IsEqualTo("Contains");
    }

    [Test]
    public async Task IsGenericMethodParameter()
    {
        var method = typeof(List<>).GetMethod("Add")!;
        var paramType = method.GetParameters()[0].ParameterType;
        await Assert.That(paramType.IsGenericMethodParameter).IsFalse();

        var genericMethod = typeof(Array).GetMethod("Empty")!;
        var genericParam = genericMethod.GetGenericArguments()[0];
        await Assert.That(genericParam.IsGenericMethodParameter).IsTrue();
    }

    [Test]
    public async Task IsAssignableTo()
    {
        await Assert.That(typeof(List<string>).IsAssignableTo(typeof(IList))).IsTrue();
        await Assert.That(typeof(List<string>).IsAssignableTo<IList>()).IsTrue();
        await Assert.That(typeof(List<string>).IsAssignableTo(typeof(string))).IsFalse();
        await Assert.That(typeof(List<string>).IsAssignableTo<string>()).IsFalse();
        await Assert.That(typeof(List<string>).IsAssignableTo(null)).IsFalse();
    }

    [Test]
    public async Task IsAssignableFrom()
    {
        await Assert.That(typeof(IList).IsAssignableFrom<List<string>>()).IsTrue();
        await Assert.That(typeof(string).IsAssignableFrom<List<string>>()).IsFalse();
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
    public async Task Type_GetMethod()
    {
        var type = typeof(WithGenericMethod);
        var method = type.GetMethod("Method", 1, BindingFlags.Public | BindingFlags.Instance, [typeof(string)])!;
        await Assert.That(method.Name).IsEqualTo("Method");
        await Assert.That(method.GetParameters().Length).IsEqualTo(1);
        await Assert.That(method.GetGenericArguments().Length).IsEqualTo(1);
    }
#endif
}