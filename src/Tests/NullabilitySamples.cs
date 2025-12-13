#pragma warning disable CS0649
#pragma warning disable CS8618

public class NullabilitySamples
{
    #region NullabilityUsage

    [Test]
    public async Task Test()
    {
        var type = typeof(NullabilityTarget);
        var arrayField = type.GetField("ArrayField")!;
        var genericField = type.GetField("GenericField")!;

        var context = new NullabilityInfoContext();

        var arrayInfo = context.Create(arrayField);

        await Assert.That(arrayInfo.ReadState).IsEqualTo(NullabilityState.NotNull);
        await Assert.That(arrayInfo.ElementType!.ReadState).IsEqualTo(NullabilityState.Nullable);

        var genericInfo = context.Create(genericField);

        await Assert.That(genericInfo.ReadState).IsEqualTo(NullabilityState.NotNull);
        await Assert.That(genericInfo.GenericTypeArguments[0].ReadState).IsEqualTo(NullabilityState.NotNull);
        await Assert.That(genericInfo.GenericTypeArguments[1].ReadState).IsEqualTo(NullabilityState.Nullable);
    }

    #endregion

    #region NullabilityExtension

    [Test]
    public async Task ExtensionTests()
    {
        var type = typeof(NullabilityTarget);
        var field = type.GetField("StringField")!;
        await Assert.That(field.IsNullable()).IsTrue();
        await Assert.That(field.GetNullability()).IsEqualTo(NullabilityState.Nullable);
        await Assert.That(field.GetNullabilityInfo().ReadState).IsEqualTo(NullabilityState.Nullable);
    }

    #endregion

    // ReSharper disable UnusedMember.Local
    // ReSharper disable NotAccessedField.Local
    class PropertyTarget
    {
        string? write;
        public string? ReadWrite { get; set; }
        public string? Read { get; }

        public string? Write
        {
            set => write = value;
        }
    }
    // ReSharper restore NotAccessedField.Local
    // ReSharper restore UnusedMember.Local

    [Test]
    public async Task Property()
    {
        var type = typeof(PropertyTarget);
        var readWrite = type.GetProperty("ReadWrite")!;
        var write = type.GetProperty("Write")!;
        var read = type.GetProperty("Read")!;
        await Assert.That(readWrite.IsNullable()).IsTrue();
        await Assert.That(write.IsNullable()).IsTrue();
        await Assert.That(read.IsNullable()).IsTrue();
    }
#pragma warning disable CS0649, CS8618

    #region NullabilityTarget

    class NullabilityTarget
    {
        public string? StringField;
        public string?[] ArrayField;
        public Dictionary<string, object?> GenericField;
    }

    #endregion
}
