﻿#pragma warning disable CS0649
#pragma warning disable CS8618

public class NullabilitySamples
{
    #region NullabilityUsage

    [Test]
    public void Test()
    {
        var type = typeof(NullabilityTarget);
        var arrayField = type.GetField("ArrayField")!;
        var genericField = type.GetField("GenericField")!;

        var context = new NullabilityInfoContext();

        var arrayInfo = context.Create(arrayField);

        Assert.AreEqual(NullabilityState.NotNull, arrayInfo.ReadState);
        Assert.AreEqual(NullabilityState.Nullable, arrayInfo.ElementType!.ReadState);

        var genericInfo = context.Create(genericField);

        Assert.AreEqual(NullabilityState.NotNull, genericInfo.ReadState);
        Assert.AreEqual(NullabilityState.NotNull, genericInfo.GenericTypeArguments[0].ReadState);
        Assert.AreEqual(NullabilityState.Nullable, genericInfo.GenericTypeArguments[1].ReadState);
    }

    #endregion

    #region NullabilityExtension

    [Test]
    public void ExtensionTests()
    {
        var type = typeof(NullabilityTarget);
        var field = type.GetField("StringField")!;
        Assert.True(field.IsNullable());
        Assert.AreEqual(NullabilityState.Nullable, field.GetNullability());
        Assert.AreEqual(NullabilityState.Nullable, field.GetNullabilityInfo().ReadState);
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
    public void Property()
    {
        var type = typeof(PropertyTarget);
        var readWrite = type.GetProperty("ReadWrite")!;
        var write = type.GetProperty("Write")!;
        var read = type.GetProperty("Read")!;
        Assert.True(readWrite.IsNullable());
        Assert.True(write.IsNullable());
        Assert.True(read.IsNullable());
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
