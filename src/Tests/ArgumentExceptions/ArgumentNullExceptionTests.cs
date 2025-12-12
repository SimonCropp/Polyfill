public class ArgumentNullExceptionTests
{
    [Test]
    public async Task ThrowIfNull_WithNullObject_ThrowsArgumentNullException()
    {
        // Arrange
        object? nullObject = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(nullObject)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullObject");
    }

    [Test]
    public async Task ThrowIfNull_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(nullString)).Throws<ArgumentNullException>();

        await Assert.That(ex).IsNotNull();
        await Assert.That(ex!.ParamName).IsEqualTo("nullString");
    }

    [Test]
    public async Task ThrowIfNull_WithNullArray_ThrowsArgumentNullException()
    {
        // Arrange
        int[]? nullArray = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(nullArray)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullArray");
    }

    [Test]
    public async Task ThrowIfNull_WithNullCustomType_ThrowsArgumentNullException()
    {
        // Arrange
        TestClass? nullCustom = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(nullCustom)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullCustom");
    }

    [Test]
    public void ThrowIfNull_WithNonNullObject_DoesNotThrow()
    {
        // Arrange
        var nonNullObject = new object();

        // Act & Assert
        ArgumentNullException.ThrowIfNull(nonNullObject);
    }

    [Test]
    public void ThrowIfNull_WithNonNullString_DoesNotThrow()
    {
        // Arrange
        var nonNullString = "test";

        // Act & Assert
        ArgumentNullException.ThrowIfNull(nonNullString);
    }

    [Test]
    public void ThrowIfNull_WithEmptyString_DoesNotThrow()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        ArgumentNullException.ThrowIfNull(emptyString);
    }

    [Test]
    public void ThrowIfNull_WithNonNullArray_DoesNotThrow()
    {
        // Arrange
        var nonNullArray = new int[] {1, 2, 3};

        // Act & Assert
        ArgumentNullException.ThrowIfNull(nonNullArray);
    }

    [Test]
    public void ThrowIfNull_WithEmptyArray_DoesNotThrow()
    {
        // Arrange
        var emptyArray = Array.Empty<int>();

        // Act & Assert
        ArgumentNullException.ThrowIfNull(emptyArray);
    }

    [Test]
    public void ThrowIfNull_WithNonNullCustomType_DoesNotThrow()
    {
        // Arrange
        var nonNullCustom = new TestClass();

        // Act & Assert
        ArgumentNullException.ThrowIfNull(nonNullCustom);
    }

    [Test]
    public async Task ThrowIfNull_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        string? nullParameter = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            MethodThatValidatesParameter(nullParameter)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("parameter");
    }

    [Test]
    public async Task ThrowIfNull_WithMultipleParameters_CapturesCorrectNames()
    {
        // Arrange
        string? first = null;
        var second = "valid";
        string? third = null;

        // Act & Assert
        var ex1 = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(first)).Throws<ArgumentNullException>();
        await Assert.That(ex1!.ParamName).IsEqualTo("first");

        ArgumentNullException.ThrowIfNull(second);

        var ex2 = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(third)).Throws<ArgumentNullException>();
        await Assert.That(ex2!.ParamName).IsEqualTo("third");
    }

    [Test]
    public void ThrowIfNull_WithBoxedValueType_DoesNotThrow()
    {
        // Arrange
        object boxedInt = 42;
        object boxedDecimal = 3.14m;
        object boxedBool = true;

        // Act & Assert
        ArgumentNullException.ThrowIfNull(boxedInt);
        ArgumentNullException.ThrowIfNull(boxedDecimal);
        ArgumentNullException.ThrowIfNull(boxedBool);
    }

    [Test]
    public async Task ThrowIfNull_WithNullableValueType_ThrowsWhenNull()
    {
        // Arrange
        int? nullableInt = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentNullException.ThrowIfNull(nullableInt)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullableInt");
    }

    [Test]
    public void ThrowIfNull_WithNullableValueType_DoesNotThrowWhenHasValue()
    {
        // Arrange
        int? nullableInt = 42;

        // Act & Assert
        ArgumentNullException.ThrowIfNull(nullableInt);
    }

    // Helper method to test parameter name capture
    static void MethodThatValidatesParameter(string? parameter) =>
        ArgumentNullException.ThrowIfNull(parameter);

    // Helper class for testing
    class TestClass
    {
        public int Value { get; set; }
    }
}
