[TestFixture]
public class ArgumentExceptionTests
{
    #region ThrowIfNullOrEmpty Tests

    [Test]
    public void ThrowIfNullOrEmpty_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(nullString))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullString"));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(emptyString))!;

        Assert.That(ex.ParamName, Is.EqualTo("emptyString"));
        Assert.That(ex.Message, Does.Contain("cannot be an empty string"));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithEmptyStringLiteral_ThrowsArgumentException()
    {
        // Arrange
        var value = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(value))!;

        Assert.That(ex.ParamName, Is.EqualTo("value"));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithWhitespaceString_DoesNotThrow()
    {
        // Arrange
        var whitespaceString = "   ";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(whitespaceString));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithValidString_DoesNotThrow()
    {
        // Arrange
        var validString = "test";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(validString));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithSingleCharacter_DoesNotThrow()
    {
        // Arrange
        var singleChar = "a";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(singleChar));
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithLongString_DoesNotThrow()
    {
        // Arrange
        var longString = new string('a', 1000);

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(longString));
    }

    [Test]
    public void ThrowIfNullOrEmpty_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        var empty = string.Empty;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            MethodThatValidatesEmpty(empty))!;

        Assert.That(ex.ParamName, Is.EqualTo("parameter"));
    }

    #endregion

    #region ThrowIfNullOrWhiteSpace Tests

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(nullString))!;

        Assert.That(ex.ParamName, Is.EqualTo("nullString"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(emptyString))!;

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("whitespace"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithSingleSpace_ThrowsArgumentException()
    {
        // Arrange
        var singleSpace = " ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(singleSpace))!;

        Assert.That(ex.ParamName, Is.EqualTo("singleSpace"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithMultipleSpaces_ThrowsArgumentException()
    {
        // Arrange
        var spaces = "     ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(spaces))!;

        Assert.That(ex.ParamName, Is.EqualTo("spaces"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithTab_ThrowsArgumentException()
    {
        // Arrange
        var tab = "\t";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(tab))!;

        Assert.That(ex.ParamName, Is.EqualTo("tab"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithNewline_ThrowsArgumentException()
    {
        // Arrange
        var newline = "\n";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(newline))!;

        Assert.That(ex.ParamName, Is.EqualTo("newline"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithCarriageReturn_ThrowsArgumentException()
    {
        // Arrange
        var carriageReturn = "\r";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(carriageReturn))!;

        Assert.That(ex.ParamName, Is.EqualTo("carriageReturn"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithMixedWhitespace_ThrowsArgumentException()
    {
        // Arrange
        var mixedWhitespace = " \t\r\n ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(mixedWhitespace))!;

        Assert.That(ex.ParamName, Is.EqualTo("mixedWhitespace"));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithValidString_DoesNotThrow()
    {
        // Arrange
        var validString = "test";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(validString));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithStringContainingSpaces_DoesNotThrow()
    {
        // Arrange
        var stringWithSpaces = "hello world";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(stringWithSpaces));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithLeadingSpace_DoesNotThrow()
    {
        // Arrange
        var leadingSpace = " test";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(leadingSpace));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithTrailingSpace_DoesNotThrow()
    {
        // Arrange
        var trailingSpace = "test ";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(trailingSpace));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithSingleCharacter_DoesNotThrow()
    {
        // Arrange
        var singleChar = "a";

        // Act & Assert
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(singleChar));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        var whitespace = "   ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            MethodThatValidatesWhitespace(whitespace))!;

        Assert.That(ex.ParamName, Is.EqualTo("parameter"));
    }

    #endregion

    #region Multiple Validation Tests

    [Test]
    public void ThrowIfNullOrEmpty_ThenThrowIfNullOrWhiteSpace_BothValidate()
    {
        // Arrange
        string? nullValue = null;
        var emptyValue = string.Empty;
        var whitespaceValue = "   ";
        var validValue = "test";

        // Act & Assert - ThrowIfNullOrEmpty
        Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(nullValue));
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(emptyValue));
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(whitespaceValue));
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(validValue));

        // Act & Assert - ThrowIfNullOrWhiteSpace
        Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(nullValue));
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(emptyValue));
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(whitespaceValue));
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(validValue));
    }

    [Test]
    public void ThrowIfNullOrEmpty_Empty() =>
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(""));

    [Test]
    public void ThrowIfNullOrEmpty_Null() =>
        Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrEmpty(null));

    [TestCase("a")]
    [TestCase("test")]
    [TestCase("hello world")]
    [TestCase("   with spaces   ")]
    public void ThrowIfNullOrEmpty_WithValidValues_DoesNotThrow(string value) =>
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrEmpty(value));

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithNull_Throws()
    {
        string? value = null;
        Assert.Throws<ArgumentNullException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithEmpty_Throws()
    {
        var value = "";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithSingleSpace_Throws()
    {
        var value = " ";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithMultipleSpaces_Throws()
    {
        var value = "   ";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithTab_Throws()
    {
        var value = "\t";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithNewline_Throws()
    {
        var value = "\n";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithCarriageReturn_Throws()
    {
        var value = "\r";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithMixedWhitespace_Throws()
    {
        var value = " \t\r\n ";
        Assert.Throws<ArgumentException>(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));
    }

    [TestCase("a")]
    [TestCase("test")]
    [TestCase("hello world")]
    [TestCase(" test")]
    [TestCase("test ")]
    [TestCase(" test ")]
    public void ThrowIfNullOrWhiteSpace_WithValidValues_DoesNotThrow(string value) =>
        Assert.DoesNotThrow(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value));

    #endregion

    // Helper methods to test parameter name capture
    static void MethodThatValidatesEmpty(string? parameter) =>
        ArgumentException.ThrowIfNullOrEmpty(parameter);

    static void MethodThatValidatesWhitespace(string? parameter) =>
        ArgumentException.ThrowIfNullOrWhiteSpace(parameter);
}