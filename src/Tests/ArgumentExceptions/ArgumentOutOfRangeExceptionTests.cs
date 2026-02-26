// ReSharper disable ObjectCreationAsStatement

public class ArgumentOutOfRangeExceptionTests
{
    #region ThrowIfZero Tests

    [Test]
    public async Task ThrowIfZero_WithZeroInt_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("zero");
    }

    [Test]
    public async Task ThrowIfZero_WithZeroLong_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0L;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("zero");
    }

    [Test]
    public async Task ThrowIfZero_WithZeroDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("zero");
    }

    [Test]
    public async Task ThrowIfZero_WithZeroDouble_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0.0;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("zero");
    }

    [Test]
    public async Task ThrowIfZero_WithPositiveInt_DoesNotThrow()
    {
        // Arrange
        var positive = 42;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(positive)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfZero_WithNegativeInt_DoesNotThrow()
    {
        // Arrange
        var negative = -42;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(negative)).ThrowsNothing();
    }

    [Test]
    [Arguments(1)]
    [Arguments(100)]
    [Arguments(-1)]
    [Arguments(-100)]
    [Arguments(int.MaxValue)]
    [Arguments(int.MinValue)]
    public async Task ThrowIfZero_WithNonZeroInt_DoesNotThrow(int value) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfZero(value)).ThrowsNothing();

    #endregion

    #region ThrowIfNegative Tests

    [Test]
    public async Task ThrowIfNegative_WithNegativeInt_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -1;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negative)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("negative");
        await Assert.That(ex.ActualValue).IsEqualTo(-1);
        await Assert.That(ex.Message).Contains("non-negative");
    }

    [Test]
    public async Task ThrowIfNegative_WithNegativeDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -10.5m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negative)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("negative");
        await Assert.That(ex.ActualValue).IsEqualTo(-10.5m);
    }

    [Test]
    public async Task ThrowIfNegative_WithZero_DoesNotThrow()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(zero)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNegative_WithPositiveInt_DoesNotThrow()
    {
        // Arrange
        var positive = 42;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(positive)).ThrowsNothing();
    }

    [Test]
    [Arguments(-1)]
    [Arguments(-100)]
    [Arguments(-1000)]
    [Arguments(int.MinValue)]
    public async Task ThrowIfNegative_WithNegativeValues_Throws(int value)
    {
        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(value)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(value);
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(100)]
    [Arguments(1000)]
    [Arguments(int.MaxValue)]
    public async Task ThrowIfNegative_WithNonNegativeValues_DoesNotThrow(int value) =>
        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(value)).ThrowsNothing();

    #endregion

    #region ThrowIfNegativeOrZero Tests

    [Test]
    public async Task ThrowIfNegativeOrZero_WithZero_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("zero");
        await Assert.That(ex.ActualValue).IsEqualTo(0);
        await Assert.That(ex.Message).Contains("non-zero");
    }

    [Test]
    public async Task ThrowIfNegativeOrZero_WithNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(negative)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("negative");
        await Assert.That(ex.ActualValue).IsEqualTo(-5);
    }

    [Test]
    public async Task ThrowIfNegativeOrZero_WithZeroDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0.0m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(0.0m);
    }

    [Test]
    public async Task ThrowIfNegativeOrZero_WithPositive_DoesNotThrow()
    {
        // Arrange
        var positive = 1;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(positive)).ThrowsNothing();
    }

    [Test]
    [Arguments(-1000)]
    [Arguments(-100)]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(int.MinValue)]
    public async Task ThrowIfNegativeOrZero_WithInvalidValues_Throws(int value)
    {
        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(value);
    }

    [Test]
    [Arguments(1)]
    [Arguments(10)]
    [Arguments(100)]
    [Arguments(1000)]
    [Arguments(int.MaxValue)]
    public async Task ThrowIfNegativeOrZero_WithValidValues_DoesNotThrow(int value) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value)).ThrowsNothing();

    [Test]
    public async Task ThrowIfNegativeOrZero_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var negative = -0.001m;
        var zero = 0.0m;
        var positive = 0.001m;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(negative)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(positive)).ThrowsNothing();
    }

    #endregion

    #region ThrowIfGreaterThan Tests

    [Test]
    public async Task ThrowIfGreaterThan_WhenValueIsGreater_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(10);
        await Assert.That(ex.Message).Contains("less than or equal to");
    }

    [Test]
    public async Task ThrowIfGreaterThan_WhenValueIsEqual_DoesNotThrow()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfGreaterThan_WhenValueIsLess_DoesNotThrow()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfGreaterThan_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var discount = 105.5m;
        var maxDiscount = 100m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(discount, maxDiscount)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(105.5m);
    }

    [Test]
    public async Task ThrowIfGreaterThan_WithString_ValidatesCorrectly()
    {
        // Arrange
        var value = "zebra";
        var threshold = "apple";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo("zebra");
    }

    [Test]
    [Arguments(101, 100)]
    [Arguments(1000, 999)]
    [Arguments(int.MaxValue, 0)]
    public async Task ThrowIfGreaterThan_WithValuesGreaterThanThreshold_Throws(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(100, 100)]
    [Arguments(99, 100)]
    [Arguments(0, 100)]
    [Arguments(int.MinValue, 0)]
    public async Task ThrowIfGreaterThan_WithValuesNotGreaterThanThreshold_DoesNotThrow(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold)).ThrowsNothing();

    #endregion

    #region ThrowIfGreaterThanOrEqual Tests

    [Test]
    public async Task ThrowIfGreaterThanOrEqual_WhenValueIsGreater_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(10);
        await Assert.That(ex.Message).Contains("less than");
    }

    [Test]
    public async Task ThrowIfGreaterThanOrEqual_WhenValueIsEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(5);
    }

    [Test]
    public async Task ThrowIfGreaterThanOrEqual_WhenValueIsLess_DoesNotThrow()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold)).ThrowsNothing();
    }

    [Test]
    [Arguments(100, 100)]
    [Arguments(101, 100)]
    [Arguments(1000, 999)]
    [Arguments(int.MaxValue, 0)]
    public async Task ThrowIfGreaterThanOrEqual_WithInvalidValues_Throws(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(99, 100)]
    [Arguments(0, 100)]
    [Arguments(int.MinValue, 0)]
    public async Task ThrowIfGreaterThanOrEqual_WithValidValues_DoesNotThrow(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold)).ThrowsNothing();

    #endregion

    #region ThrowIfLessThan Tests

    [Test]
    public async Task ThrowIfLessThan_WhenValueIsLess_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(3);
        await Assert.That(ex.Message).Contains("greater than or equal to");
    }

    [Test]
    public async Task ThrowIfLessThan_WhenValueIsEqual_DoesNotThrow()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfLessThan_WhenValueIsGreater_DoesNotThrow()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfLessThan_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var price = 0.50m;
        var minPrice = 1.00m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(price, minPrice)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(0.50m);
    }

    [Test]
    [Arguments(0, 1)]
    [Arguments(99, 100)]
    [Arguments(-100, 0)]
    [Arguments(int.MinValue, 0)]
    public async Task ThrowIfLessThan_WithValuesLessThanThreshold_Throws(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(100, 100)]
    [Arguments(101, 100)]
    [Arguments(1000, 999)]
    [Arguments(int.MaxValue, 0)]
    public async Task ThrowIfLessThan_WithValuesNotLessThanThreshold_DoesNotThrow(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold)).ThrowsNothing();

    #endregion
    #region ThrowIfLessThanOrEqual Tests

    [Test]
    public async Task ThrowIfLessThanOrEqual_WhenValueIsLess_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(3);
        await Assert.That(ex.Message).Contains("greater than");
    }

    [Test]
    public async Task ThrowIfLessThanOrEqual_WhenValueIsEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(5);
    }

    [Test]
    public async Task ThrowIfLessThanOrEqual_WhenValueIsGreater_DoesNotThrow()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfLessThanOrEqual_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var maxStock = 100m;
        var minStock = 100m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxStock, minStock)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(100m);
    }

    [Test]
    [Arguments(0, 1)]
    [Arguments(100, 100)]
    [Arguments(99, 100)]
    [Arguments(-100, 0)]
    [Arguments(int.MinValue, 0)]
    public async Task ThrowIfLessThanOrEqual_WithInvalidValues_Throws(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(101, 100)]
    [Arguments(1000, 999)]
    [Arguments(int.MaxValue, 0)]
    public async Task ThrowIfLessThanOrEqual_WithValidValues_DoesNotThrow(int value, int threshold) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold)).ThrowsNothing();

    [Test]
    public async Task ThrowIfLessThanOrEqual_EnsuresMinLessThanMax()
    {
        // Arrange - This pattern ensures max > min
        var minValue = 10;
        var maxValue = 5; // Invalid: max should be > min

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxValue, minValue)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex).IsNotNull();
    }

    #endregion

    #region ThrowIfEqual Tests

    [Test]
    public async Task ThrowIfEqual_WhenValuesAreEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 42;
        var other = 42;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(42);
        await Assert.That(ex.Message).Contains("must not be equal");
    }

    [Test]
    public async Task ThrowIfEqual_WhenValuesAreDifferent_DoesNotThrow()
    {
        // Arrange
        var value = 42;
        var other = 43;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfEqual_WithZeroValues_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var port = 0;
        var reservedPort = 0;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(port, reservedPort)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(0);
    }

    [Test]
    public async Task ThrowIfEqual_WithStrings_ValidatesCorrectly()
    {
        // Arrange
        var value = "test";
        var other = "test";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo("test");
    }

    [Test]
    public async Task ThrowIfEqual_WithDifferentStrings_DoesNotThrow()
    {
        // Arrange
        var value = "test1";
        var other = "test2";

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfEqual_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var value = 3.14m;
        var other = 3.14m;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(3.14m);
    }

    [Test]
    [Arguments(0, 0)]
    [Arguments(100, 100)]
    [Arguments(-42, -42)]
    [Arguments(int.MaxValue, int.MaxValue)]
    [Arguments(int.MinValue, int.MinValue)]
    public async Task ThrowIfEqual_WithEqualValues_Throws(int value, int other) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(0, 1)]
    [Arguments(100, 101)]
    [Arguments(-42, 42)]
    [Arguments(int.MaxValue, int.MinValue)]
    public async Task ThrowIfEqual_WithDifferentValues_DoesNotThrow(int value, int other) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other)).ThrowsNothing();

    [Test]
    public async Task ThrowIfEqual_WithCustomType_ValidatesCorrectly()
    {
        // Arrange
        var obj1 = new TestEquatable(42);
        var obj2 = new TestEquatable(42);
        var obj3 = new TestEquatable(43);

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(obj1, obj2)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(obj1, obj3)).ThrowsNothing();
    }

    #endregion

    #region ThrowIfNotEqual Tests

    [Test]
    public async Task ThrowIfNotEqual_WhenValuesDiffer_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 42;
        var expected = 100;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
        await Assert.That(ex.ActualValue).IsEqualTo(42);
        await Assert.That(ex.Message).Contains("must be equal");
    }

    [Test]
    public async Task ThrowIfNotEqual_WhenValuesAreEqual_DoesNotThrow()
    {
        // Arrange
        var value = 200;
        var expected = 200;

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNotEqual_WithStatusCode_ValidatesCorrectly()
    {
        // Arrange
        var statusCode = 404;
        var expectedCode = 200;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(statusCode, expectedCode)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo(404);
    }

    [Test]
    public async Task ThrowIfNotEqual_WithStrings_ValidatesCorrectly()
    {
        // Arrange
        var value = "actual";
        var expected = "expected";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(ex!.ActualValue).IsEqualTo("actual");
    }

    [Test]
    public async Task ThrowIfNotEqual_WithSameStrings_DoesNotThrow()
    {
        // Arrange
        var value = "test";
        var expected = "test";

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).ThrowsNothing();
    }

    [Test]
    [Arguments(0, 1)]
    [Arguments(100, 200)]
    [Arguments(-42, 42)]
    [Arguments(int.MaxValue, int.MinValue)]
    public async Task ThrowIfNotEqual_WithDifferentValues_Throws(int value, int expected) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).Throws<ArgumentOutOfRangeException>();

    [Test]
    [Arguments(0, 0)]
    [Arguments(100, 100)]
    [Arguments(-42, -42)]
    [Arguments(int.MaxValue, int.MaxValue)]
    [Arguments(int.MinValue, int.MinValue)]
    public async Task ThrowIfNotEqual_WithEqualValues_DoesNotThrow(int value, int expected) =>
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected)).ThrowsNothing();

    [Test]
    public async Task ThrowIfNotEqual_WithCustomType_ValidatesCorrectly()
    {
        // Arrange
        var obj1 = new TestEquatable(42);
        var obj2 = new TestEquatable(42);
        var obj3 = new TestEquatable(43);

        // Act & Assert
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(obj1, obj2)).ThrowsNothing();

        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(obj1, obj3)).Throws<ArgumentOutOfRangeException>();
    }

    #endregion

    #region Integration Tests

    [Test]
    public async Task MultipleValidations_InConstructor_WorkCorrectly()
    {
        // Test valid values
        await Assert.That(() =>
            new TestProduct("PROD-001", 29.99m, 100, 10, 500)).ThrowsNothing();

        // Test null SKU
        await Assert.That(() =>
            new TestProduct(null!, 29.99m, 100, 10, 500)).Throws<ArgumentNullException>();

        // Test negative price
        await Assert.That(() =>
            new TestProduct("PROD-001", -10m, 100, 10, 500)).Throws<ArgumentOutOfRangeException>();

        // Test zero price
        await Assert.That(() =>
            new TestProduct("PROD-001", 0m, 100, 10, 500)).Throws<ArgumentOutOfRangeException>();

        // Test negative stock
        await Assert.That(() =>
            new TestProduct("PROD-001", 29.99m, -5, 10, 500)).Throws<ArgumentOutOfRangeException>();

        // Test stock below minimum
        await Assert.That(() =>
            new TestProduct("PROD-001", 29.99m, 5, 10, 500)).Throws<ArgumentOutOfRangeException>();

        // Test stock above maximum
        await Assert.That(() =>
            new TestProduct("PROD-001", 29.99m, 600, 10, 500)).Throws<ArgumentOutOfRangeException>();

        // Test max less than or equal to min
        await Assert.That(() =>
            new TestProduct("PROD-001", 29.99m, 100, 500, 500)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task CallerArgumentExpression_CapturesCorrectNames()
    {
        // Test that parameter names are captured correctly
        var negativeValue = -1;
        var ex1 = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negativeValue)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(ex1!.ParamName).IsEqualTo("negativeValue");

        var greaterValue = 100;
        var ex2 = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(greaterValue, 50)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(ex2!.ParamName).IsEqualTo("greaterValue");

        var equalValue = 42;
        var ex3 = await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(equalValue, 42)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(ex3!.ParamName).IsEqualTo("equalValue");
    }

    [Test]
    public async Task AllMethods_WithDifferentNumericTypes_WorkCorrectly()
    {
        // int
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1)).Throws<ArgumentOutOfRangeException>();

        // long
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1L)).Throws<ArgumentOutOfRangeException>();

        // decimal
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0m)).Throws<ArgumentOutOfRangeException>();

        // double
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0)).Throws<ArgumentOutOfRangeException>();

        // float
        await Assert.That(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0f)).Throws<ArgumentOutOfRangeException>();
    }

    #endregion

    #region Helper Classes

    class TestEquatable(int value) : IEquatable<TestEquatable>
    {
        readonly int value = value;

        public bool Equals(TestEquatable? other)
        {
            if (other is null) return false;
            return value == other.value;
        }

        public override bool Equals(object? obj) =>
            Equals(obj as TestEquatable);

        public override int GetHashCode() =>
            value.GetHashCode();
    }

    class TestProduct
    {
        public TestProduct(
            string sku,
            decimal price,
            int stockLevel,
            int minStockLevel,
            int maxStockLevel)
        {
            ArgumentNullException.ThrowIfNull(sku);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            ArgumentOutOfRangeException.ThrowIfNegative(stockLevel);
            ArgumentOutOfRangeException.ThrowIfNegative(minStockLevel);
            ArgumentOutOfRangeException.ThrowIfNegative(maxStockLevel);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxStockLevel, minStockLevel);
            ArgumentOutOfRangeException.ThrowIfLessThan(stockLevel, minStockLevel);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(stockLevel, maxStockLevel);
        }
    }

    #endregion
}
