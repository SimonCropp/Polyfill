using System.Reflection;

public class UnionTests
{
    [Test]
    public async Task IUnion_IsInterfaceExposingObjectValue()
    {
        var type = typeof(IUnion);
        await Assert.That(type.IsInterface).IsTrue();

        var property = type.GetProperty("Value");
        await Assert.That(property).IsNotNull();
        await Assert.That(property!.PropertyType).IsEqualTo(typeof(object));
    }

    [Test]
    public async Task UnionAttribute_HasExpectedShape()
    {
        var type = typeof(UnionAttribute);
        await Assert.That(type.IsSealed).IsTrue();
        await Assert.That(typeof(Attribute).IsAssignableFrom(type)).IsTrue();

        var usage = type.GetCustomAttribute<AttributeUsageAttribute>();
        await Assert.That(usage).IsNotNull();
        await Assert.That(usage!.ValidOn).IsEqualTo(AttributeTargets.Class | AttributeTargets.Struct);
        await Assert.That(usage.AllowMultiple).IsFalse();
        await Assert.That(usage.Inherited).IsFalse();
    }

    [Test]
    public async Task UserDefinedUnion_ValueRoundTrips()
    {
        IUnion fromInt = new IntOrString(5);
        IUnion fromText = new IntOrString("text");

        var intValue = fromInt.Value;
        var textValue = fromText.Value;

        await Assert.That(intValue).IsEqualTo(5);
        await Assert.That(textValue).IsEqualTo("text");
    }

    [Test]
    public async Task UserDefinedUnion_IsMarkedAndImplementsIUnion()
    {
        var type = typeof(IntOrString);

        await Assert.That(type.GetCustomAttribute<UnionAttribute>()).IsNotNull();
        await Assert.That(typeof(IUnion).IsAssignableFrom(type)).IsTrue();
    }

    [Union]
    readonly struct IntOrString : IUnion
    {
        readonly object? value;

        public IntOrString(int value) => this.value = value;

        public IntOrString(string value) => this.value = value;

        public object? Value => value;
    }
}
