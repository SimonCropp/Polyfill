[TestFixture]
public class ArgumentNullExceptionTests
{
    [Test]
    public void ThrowIfNull_WithNullObject_ThrowsArgumentNullException()
    {
        // Arrange
        object? nullObject = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(nullObject))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullObject"));
    }

    [Test]
    public void ThrowIfNull_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(nullString))!;

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("nullString"));
    }

    [Test]
    public void ThrowIfNull_WithNullArray_ThrowsArgumentNullException()
    {
        // Arrange
        int[]? nullArray = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(nullArray))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullArray"));
    }

    [Test]
    public void ThrowIfNull_WithNullCustomType_ThrowsArgumentNullException()
    {
        // Arrange
        TestClass? nullCustom = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(nullCustom))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullCustom"));
    }

    [Test]
    public void ThrowIfNull_WithNonNullObject_DoesNotThrow()
    {
        // Arrange
        var nonNullObject = new object();

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(nonNullObject));
    }

    [Test]
    public void ThrowIfNull_WithNonNullString_DoesNotThrow()
    {
        // Arrange
        var nonNullString = "test";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(nonNullString));
    }

    [Test]
    public void ThrowIfNull_WithEmptyString_DoesNotThrow()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(emptyString));
    }

    [Test]
    public void ThrowIfNull_WithNonNullArray_DoesNotThrow()
    {
        // Arrange
        var nonNullArray = new int[] {1, 2, 3};

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(nonNullArray));
    }

    [Test]
    public void ThrowIfNull_WithEmptyArray_DoesNotThrow()
    {
        // Arrange
        var emptyArray = Array.Empty<int>();

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(emptyArray));
    }

    [Test]
    public void ThrowIfNull_WithNonNullCustomType_DoesNotThrow()
    {
        // Arrange
        var nonNullCustom = new TestClass();

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(nonNullCustom));
    }

    [Test]
    public void ThrowIfNull_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        string? nullParameter = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            MethodThatValidatesParameter(nullParameter))!;

        Assert.That(ex.ParamName, Is.EqualTo("parameter"));
    }

    [Test]
    public void ThrowIfNull_WithMultipleParameters_CapturesCorrectNames()
    {
        // Arrange
        string? first = null;
        var second = "valid";
        string? third = null;

        // Act & Assert
        var ex1 = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(first))!;
        Assert.That(ex1.ParamName, Is.EqualTo("first"));

        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(second));

        var ex2 = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(third))!;
        Assert.That(ex2.ParamName, Is.EqualTo("third"));
    }

    [Test]
    public void ThrowIfNull_WithBoxedValueType_DoesNotThrow()
    {
        // Arrange
        object boxedInt = 42;
        object boxedDecimal = 3.14m;
        object boxedBool = true;

        // Act & Assert
        Assert.DoesNotThrow(() => ArgumentNullException.ThrowIfNull(boxedInt));
        Assert.DoesNotThrow(() => ArgumentNullException.ThrowIfNull(boxedDecimal));
        Assert.DoesNotThrow(() => ArgumentNullException.ThrowIfNull(boxedBool));
    }

    [Test]
    public void ThrowIfNull_WithNullableValueType_ThrowsWhenNull()
    {
        // Arrange
        int? nullableInt = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentNullException.ThrowIfNull(nullableInt))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullableInt"));
    }

    [Test]
    public void ThrowIfNull_WithNullableValueType_DoesNotThrowWhenHasValue()
    {
        // Arrange
        int? nullableInt = 42;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentNullException.ThrowIfNull(nullableInt));
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