// ReSharper disable ObjectCreationAsStatement
[TestFixture]
public class ArgumentOutOfRangeExceptionTests
{
    #region ThrowIfZero Tests

    [Test]
    public void ThrowIfZero_WithZeroInt_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero))!;

        Assert.That(ex.ParamName, Is.EqualTo("zero"));
    }

    [Test]
    public void ThrowIfZero_WithZeroLong_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0L;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero))!;

        Assert.That(ex.ParamName, Is.EqualTo("zero"));
    }

    [Test]
    public void ThrowIfZero_WithZeroDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero))!;

        Assert.That(ex.ParamName, Is.EqualTo("zero"));
    }

    [Test]
    public void ThrowIfZero_WithZeroDouble_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0.0;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfZero(zero))!;

        Assert.That(ex.ParamName, Is.EqualTo("zero"));
    }

    [Test]
    public void ThrowIfZero_WithPositiveInt_DoesNotThrow()
    {
        // Arrange
        var positive = 42;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfZero(positive));
    }

    [Test]
    public void ThrowIfZero_WithNegativeInt_DoesNotThrow()
    {
        // Arrange
        var negative = -42;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfZero(negative));
    }

    [TestCase(1)]
    [TestCase(100)]
    [TestCase(-1)]
    [TestCase(-100)]
    [TestCase(int.MaxValue)]
    [TestCase(int.MinValue)]
    public void ThrowIfZero_WithNonZeroInt_DoesNotThrow(int value) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfZero(value));

    #endregion

    #region ThrowIfNegative Tests

    [Test]
    public void ThrowIfNegative_WithNegativeInt_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -1;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negative))!;

        Assert.That(ex.ParamName, Is.EqualTo("negative"));
        Assert.That(ex.ActualValue, Is.EqualTo(-1));
        Assert.That(ex.Message, Does.Contain("non-negative"));
    }

    [Test]
    public void ThrowIfNegative_WithNegativeDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -10.5m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negative))!;

        Assert.That(ex.ParamName, Is.EqualTo("negative"));
        Assert.That(ex.ActualValue, Is.EqualTo(-10.5m));
    }

    [Test]
    public void ThrowIfNegative_WithZero_DoesNotThrow()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(zero));
    }

    [Test]
    public void ThrowIfNegative_WithPositiveInt_DoesNotThrow()
    {
        // Arrange
        var positive = 42;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(positive));
    }

    [TestCase(-1)]
    [TestCase(-100)]
    [TestCase(-1000)]
    [TestCase(int.MinValue)]
    public void ThrowIfNegative_WithNegativeValues_Throws(int value)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(value))!;

        Assert.That(ex.ActualValue, Is.EqualTo(value));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(int.MaxValue)]
    public void ThrowIfNegative_WithNonNegativeValues_DoesNotThrow(int value) =>
        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(value));

    #endregion

    #region ThrowIfNegativeOrZero Tests

    [Test]
    public void ThrowIfNegativeOrZero_WithZero_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero))!;

        Assert.That(ex.ParamName, Is.EqualTo("zero"));
        Assert.That(ex.ActualValue, Is.EqualTo(0));
        Assert.That(ex.Message, Does.Contain("non-zero"));
    }

    [Test]
    public void ThrowIfNegativeOrZero_WithNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negative = -5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(negative))!;

        Assert.That(ex.ParamName, Is.EqualTo("negative"));
        Assert.That(ex.ActualValue, Is.EqualTo(-5));
    }

    [Test]
    public void ThrowIfNegativeOrZero_WithZeroDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var zero = 0.0m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero))!;

        Assert.That(ex.ActualValue, Is.EqualTo(0.0m));
    }

    [Test]
    public void ThrowIfNegativeOrZero_WithPositive_DoesNotThrow()
    {
        // Arrange
        var positive = 1;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(positive));
    }

    [TestCase(-1000)]
    [TestCase(-100)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MinValue)]
    public void ThrowIfNegativeOrZero_WithInvalidValues_Throws(int value)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value))!;

        Assert.That(ex.ActualValue, Is.EqualTo(value));
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(int.MaxValue)]
    public void ThrowIfNegativeOrZero_WithValidValues_DoesNotThrow(int value) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value));

    [Test]
    public void ThrowIfNegativeOrZero_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var negative = -0.001m;
        var zero = 0.0m;
        var positive = 0.001m;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(negative));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(zero));
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(positive));
    }

    #endregion

    #region ThrowIfGreaterThan Tests

    [Test]
    public void ThrowIfGreaterThan_WhenValueIsGreater_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(10));
        Assert.That(ex.Message, Does.Contain("less than or equal to"));
    }

    [Test]
    public void ThrowIfGreaterThan_WhenValueIsEqual_DoesNotThrow()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold));
    }

    [Test]
    public void ThrowIfGreaterThan_WhenValueIsLess_DoesNotThrow()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold));
    }

    [Test]
    public void ThrowIfGreaterThan_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var discount = 105.5m;
        var maxDiscount = 100m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(discount, maxDiscount))!;

        Assert.That(ex.ActualValue, Is.EqualTo(105.5m));
    }

    [Test]
    public void ThrowIfGreaterThan_WithString_ValidatesCorrectly()
    {
        // Arrange
        var value = "zebra";
        var threshold = "apple";

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold))!;

        Assert.That(ex.ActualValue, Is.EqualTo("zebra"));
    }

    [TestCase(101, 100)]
    [TestCase(1000, 999)]
    [TestCase(int.MaxValue, 0)]
    public void ThrowIfGreaterThan_WithValuesGreaterThanThreshold_Throws(int value, int threshold) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold));

    [TestCase(100, 100)]
    [TestCase(99, 100)]
    [TestCase(0, 100)]
    [TestCase(int.MinValue, 0)]
    public void ThrowIfGreaterThan_WithValuesNotGreaterThanThreshold_DoesNotThrow(int value, int threshold) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, threshold));

    #endregion

    #region ThrowIfGreaterThanOrEqual Tests

    [Test]
    public void ThrowIfGreaterThanOrEqual_WhenValueIsGreater_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(10));
        Assert.That(ex.Message, Does.Contain("less than"));
    }

    [Test]
    public void ThrowIfGreaterThanOrEqual_WhenValueIsEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold))!;

        Assert.That(ex.ActualValue, Is.EqualTo(5));
    }

    [Test]
    public void ThrowIfGreaterThanOrEqual_WhenValueIsLess_DoesNotThrow()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold));
    }

    [TestCase(100, 100)]
    [TestCase(101, 100)]
    [TestCase(1000, 999)]
    [TestCase(int.MaxValue, 0)]
    public void ThrowIfGreaterThanOrEqual_WithInvalidValues_Throws(int value, int threshold) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold));

    [TestCase(99, 100)]
    [TestCase(0, 100)]
    [TestCase(int.MinValue, 0)]
    public void ThrowIfGreaterThanOrEqual_WithValidValues_DoesNotThrow(int value, int threshold) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, threshold));

    #endregion

    #region ThrowIfLessThan Tests

    [Test]
    public void ThrowIfLessThan_WhenValueIsLess_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(3));
        Assert.That(ex.Message, Does.Contain("greater than or equal to"));
    }

    [Test]
    public void ThrowIfLessThan_WhenValueIsEqual_DoesNotThrow()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold));
    }

    [Test]
    public void ThrowIfLessThan_WhenValueIsGreater_DoesNotThrow()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold));
    }

    [Test]
    public void ThrowIfLessThan_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var price = 0.50m;
        var minPrice = 1.00m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(price, minPrice))!;

        Assert.That(ex.ActualValue, Is.EqualTo(0.50m));
    }

    [TestCase(0, 1)]
    [TestCase(99, 100)]
    [TestCase(-100, 0)]
    [TestCase(int.MinValue, 0)]
    public void ThrowIfLessThan_WithValuesLessThanThreshold_Throws(int value, int threshold) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold));

    [TestCase(100, 100)]
    [TestCase(101, 100)]
    [TestCase(1000, 999)]
    [TestCase(int.MaxValue, 0)]
    public void ThrowIfLessThan_WithValuesNotLessThanThreshold_DoesNotThrow(int value, int threshold) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfLessThan(value, threshold));

    #endregion
    #region ThrowIfLessThanOrEqual Tests

    [Test]
    public void ThrowIfLessThanOrEqual_WhenValueIsLess_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 3;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(3));
        Assert.That(ex.Message, Does.Contain("greater than"));
    }

    [Test]
    public void ThrowIfLessThanOrEqual_WhenValueIsEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 5;
        var threshold = 5;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold))!;

        Assert.That(ex.ActualValue, Is.EqualTo(5));
    }

    [Test]
    public void ThrowIfLessThanOrEqual_WhenValueIsGreater_DoesNotThrow()
    {
        // Arrange
        var value = 10;
        var threshold = 5;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold));
    }

    [Test]
    public void ThrowIfLessThanOrEqual_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var maxStock = 100m;
        var minStock = 100m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxStock, minStock))!;

        Assert.That(ex.ActualValue, Is.EqualTo(100m));
    }

    [TestCase(0, 1)]
    [TestCase(100, 100)]
    [TestCase(99, 100)]
    [TestCase(-100, 0)]
    [TestCase(int.MinValue, 0)]
    public void ThrowIfLessThanOrEqual_WithInvalidValues_Throws(int value, int threshold) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold));

    [TestCase(101, 100)]
    [TestCase(1000, 999)]
    [TestCase(int.MaxValue, 0)]
    public void ThrowIfLessThanOrEqual_WithValidValues_DoesNotThrow(int value, int threshold) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, threshold));

    [Test]
    public void ThrowIfLessThanOrEqual_EnsuresMinLessThanMax()
    {
        // Arrange - This pattern ensures max > min
        var minValue = 10;
        var maxValue = 5; // Invalid: max should be > min

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxValue, minValue));

        Assert.That(ex, Is.Not.Null);
    }

    #endregion

    #region ThrowIfEqual Tests

    [Test]
    public void ThrowIfEqual_WhenValuesAreEqual_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 42;
        var other = 42;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(42));
        Assert.That(ex.Message, Does.Contain("must not be equal"));
    }

    [Test]
    public void ThrowIfEqual_WhenValuesAreDifferent_DoesNotThrow()
    {
        // Arrange
        var value = 42;
        var other = 43;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other));
    }

    [Test]
    public void ThrowIfEqual_WithZeroValues_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var port = 0;
        var reservedPort = 0;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(port, reservedPort))!;

        Assert.That(ex.ActualValue, Is.EqualTo(0));
    }

    [Test]
    public void ThrowIfEqual_WithStrings_ValidatesCorrectly()
    {
        // Arrange
        var value = "test";
        var other = "test";

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other))!;

        Assert.That(ex.ActualValue, Is.EqualTo("test"));
    }

    [Test]
    public void ThrowIfEqual_WithDifferentStrings_DoesNotThrow()
    {
        // Arrange
        var value = "test1";
        var other = "test2";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other));
    }

    [Test]
    public void ThrowIfEqual_WithDecimal_ValidatesCorrectly()
    {
        // Arrange
        var value = 3.14m;
        var other = 3.14m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other))!;

        Assert.That(ex.ActualValue, Is.EqualTo(3.14m));
    }

    [TestCase(0, 0)]
    [TestCase(100, 100)]
    [TestCase(-42, -42)]
    [TestCase(int.MaxValue, int.MaxValue)]
    [TestCase(int.MinValue, int.MinValue)]
    public void ThrowIfEqual_WithEqualValues_Throws(int value, int other) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other));

    [TestCase(0, 1)]
    [TestCase(100, 101)]
    [TestCase(-42, 42)]
    [TestCase(int.MaxValue, int.MinValue)]
    public void ThrowIfEqual_WithDifferentValues_DoesNotThrow(int value, int other) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(value, other));

    [Test]
    public void ThrowIfEqual_WithCustomType_ValidatesCorrectly()
    {
        // Arrange
        var obj1 = new TestEquatable(42);
        var obj2 = new TestEquatable(42);
        var obj3 = new TestEquatable(43);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(obj1, obj2));

        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(obj1, obj3));
    }

    #endregion

    #region ThrowIfNotEqual Tests

    [Test]
    public void ThrowIfNotEqual_WhenValuesDiffer_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var value = 42;
        var expected = 100;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
        Assert.That(ex.ActualValue, Is.EqualTo(42));
        Assert.That(ex.Message, Does.Contain("must be equal"));
    }

    [Test]
    public void ThrowIfNotEqual_WhenValuesAreEqual_DoesNotThrow()
    {
        // Arrange
        var value = 200;
        var expected = 200;

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected));
    }

    [Test]
    public void ThrowIfNotEqual_WithStatusCode_ValidatesCorrectly()
    {
        // Arrange
        var statusCode = 404;
        var expectedCode = 200;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(statusCode, expectedCode))!;

        Assert.That(ex.ActualValue, Is.EqualTo(404));
    }

    [Test]
    public void ThrowIfNotEqual_WithStrings_ValidatesCorrectly()
    {
        // Arrange
        var value = "actual";
        var expected = "expected";

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected))!;

        Assert.That(ex.ActualValue, Is.EqualTo("actual"));
    }

    [Test]
    public void ThrowIfNotEqual_WithSameStrings_DoesNotThrow()
    {
        // Arrange
        var value = "test";
        var expected = "test";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected));
    }

    [TestCase(0, 1)]
    [TestCase(100, 200)]
    [TestCase(-42, 42)]
    [TestCase(int.MaxValue, int.MinValue)]
    public void ThrowIfNotEqual_WithDifferentValues_Throws(int value, int expected) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected));

    [TestCase(0, 0)]
    [TestCase(100, 100)]
    [TestCase(-42, -42)]
    [TestCase(int.MaxValue, int.MaxValue)]
    [TestCase(int.MinValue, int.MinValue)]
    public void ThrowIfNotEqual_WithEqualValues_DoesNotThrow(int value, int expected) =>
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected));

    [Test]
    public void ThrowIfNotEqual_WithCustomType_ValidatesCorrectly()
    {
        // Arrange
        var obj1 = new TestEquatable(42);
        var obj2 = new TestEquatable(42);
        var obj3 = new TestEquatable(43);

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(obj1, obj2));

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNotEqual(obj1, obj3));
    }

    #endregion

    #region Integration Tests

    [Test]
    public void MultipleValidations_InConstructor_WorkCorrectly()
    {
        // Test valid values
        Assert.DoesNotThrow(() =>
            new TestProduct("PROD-001", 29.99m, 100, 10, 500));

        // Test null SKU
        Assert.Throws<ArgumentNullException>(() =>
            new TestProduct(null!, 29.99m, 100, 10, 500));

        // Test negative price
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", -10m, 100, 10, 500));

        // Test zero price
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", 0m, 100, 10, 500));

        // Test negative stock
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", 29.99m, -5, 10, 500));

        // Test stock below minimum
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", 29.99m, 5, 10, 500));

        // Test stock above maximum
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", 29.99m, 600, 10, 500));

        // Test max less than or equal to min
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TestProduct("PROD-001", 29.99m, 100, 500, 500));
    }

    [Test]
    public void CallerArgumentExpression_CapturesCorrectNames()
    {
        // Test that parameter names are captured correctly
        var negativeValue = -1;
        var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(negativeValue))!;
        Assert.That(ex1.ParamName, Is.EqualTo("negativeValue"));

        var greaterValue = 100;
        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfGreaterThan(greaterValue, 50))!;
        Assert.That(ex2.ParamName, Is.EqualTo("greaterValue"));

        var equalValue = 42;
        var ex3 = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfEqual(equalValue, 42))!;
        Assert.That(ex3.ParamName, Is.EqualTo("equalValue"));
    }

    [Test]
    public void AllMethods_WithDifferentNumericTypes_WorkCorrectly()
    {
        // int
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1));

        // long
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1L));

        // decimal
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0m));

        // double
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0));

        // float
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ArgumentOutOfRangeException.ThrowIfNegative(-1.0f));
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
