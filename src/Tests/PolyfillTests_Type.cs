partial class PolyfillTests
{
    [Test]
    public async Task Type_GetMethod_Validation()
    {
        var type = typeof(string);
        var types = new[] {typeof(int)};

        // Negative genericParameterCount throws ArgumentException; the exact subtype varies by
        // framework (ArgumentException on net6-8, ArgumentOutOfRangeException on net9+/the polyfill).
        ArgumentException? genericCountError = null;
        try
        {
            type.GetMethod("Substring", -1, BindingFlags.Public | BindingFlags.Instance, null, types, null);
        }
        catch (ArgumentException exception)
        {
            genericCountError = exception;
        }

        await Assert.That(genericCountError).IsNotNull();
        await Assert.That(genericCountError!.ParamName).IsEqualTo("genericParameterCount");

        await Assert.That(() => type.GetMethod(null!, 0, BindingFlags.Public, null, types, null)).Throws<ArgumentNullException>();
        await Assert.That(() => type.GetMethod("Substring", 0, BindingFlags.Public, null, null!, null)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Type_GetMethod_UsesBinderForAssignableTypes()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

        // no WriteLine(Uri) exists, so the binder selects WriteLine(object)
        var method = typeof(Console).GetMethod("WriteLine", 0, flags, null, [typeof(Uri)], null);

        await Assert.That(method).IsNotNull();
        await Assert.That(method!.GetParameters()[0].ParameterType).IsEqualTo(typeof(object));
    }

    [Test]
    public async Task Type_GetMethod_ExactMatchPreferred()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

        var method = typeof(Console).GetMethod("WriteLine", 0, flags, null, [typeof(string)], null);

        await Assert.That(method).IsNotNull();
        await Assert.That(method!.GetParameters()[0].ParameterType).IsEqualTo(typeof(string));
    }

    [Test]
    public async Task Type_GetMethod_NoApplicableMethod()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        var method = typeof(string).GetMethod("Substring", 0, flags, null, [typeof(string)], null);

        await Assert.That(method).IsNull();
    }

    [Test]
    public async Task Type_GetMethod_IgnoreCase()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        var caseSensitive = typeof(string).GetMethod("substring", 0, flags, null, [typeof(int)], null);
        var ignoreCase = typeof(string).GetMethod("substring", 0, flags | BindingFlags.IgnoreCase, null, [typeof(int)], null);

        await Assert.That(caseSensitive).IsNull();
        await Assert.That(ignoreCase).IsNotNull();
        await Assert.That(ignoreCase!.Name).IsEqualTo("Substring");
    }

    class LastCandidateBinder :
        Binder
    {
        public int SelectMethodCalls;

        public override MethodBase? SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[]? modifiers)
        {
            SelectMethodCalls++;
            foreach (var candidate in match)
            {
                if (candidate.GetParameters()[0].ParameterType == typeof(object))
                {
                    return candidate;
                }
            }

            return null;
        }

        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo? culture) =>
            throw new NotSupportedException();

        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object?[] args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? names, out object? state) =>
            throw new NotSupportedException();

        public override object ChangeType(object value, Type type, CultureInfo? culture) =>
            throw new NotSupportedException();

        public override void ReorderArgumentArray(ref object?[] args, object state) =>
            throw new NotSupportedException();

        public override PropertyInfo? SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type? returnType, Type[]? indexes, ParameterModifier[]? modifiers) =>
            throw new NotSupportedException();
    }

    [Test]
    public async Task Type_GetMethod_UsesSuppliedBinder()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
        var binder = new LastCandidateBinder();

        // the default binder would select WriteLine(string)
        var method = typeof(Console).GetMethod("WriteLine", 0, flags, binder, [typeof(string)], null);

        await Assert.That(binder.SelectMethodCalls).IsEqualTo(1);
        await Assert.That(method).IsNotNull();
        await Assert.That(method!.GetParameters()[0].ParameterType).IsEqualTo(typeof(object));
    }

    [Test]
    public async Task MemberInfo_HasSameMetadataDefinitionAs_Null_Throws()
    {
        var member = typeof(string).GetMethod("Trim", Type.EmptyTypes)!;
        await Assert.That(() => member.HasSameMetadataDefinitionAs(null!)).Throws<ArgumentNullException>();
    }

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
    public async Task GetMemberWithSameMetadataDefinitionAs_NotFound()
    {
        var method = typeof(Uri).GetMethod("ToString", Type.EmptyTypes)!;

        var exception = await Assert.That(() => typeof(string).GetMemberWithSameMetadataDefinitionAs(method))
            .ThrowsExactly<ArgumentException>();

        await Assert.That(exception!.ParamName).IsEqualTo("member");
    }

    [Test]
    public async Task GetMemberWithSameMetadataDefinitionAs_Null()
    {
        var exception = await Assert.That(() => typeof(string).GetMemberWithSameMetadataDefinitionAs(null!))
            .ThrowsExactly<ArgumentNullException>();

        await Assert.That(exception!.ParamName).IsEqualTo("member");
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
    public async Task GetNullableUnderlyingType()
    {
        await Assert.That(typeof(int?).GetNullableUnderlyingType()).IsEqualTo(typeof(int));
        await Assert.That(typeof(int).GetNullableUnderlyingType()).IsNull();
        await Assert.That(typeof(string).GetNullableUnderlyingType()).IsNull();
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