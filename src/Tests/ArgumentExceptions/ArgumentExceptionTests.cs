public class ArgumentExceptionTests
{
    #region ThrowIfNullOrEmpty Tests

    [Test]
    public async Task ThrowIfNullOrEmpty_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(nullString)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullString");
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(emptyString)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("emptyString");
        await Assert.That(ex.Message.Contains("cannot be an empty string")).IsTrue();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_WithEmptyStringLiteral_ThrowsArgumentException()
    {
        // Arrange
        var value = "";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(value)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("value");
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithWhitespaceString_DoesNotThrow()
    {
        // Arrange
        var whitespaceString = "   ";

        // Act & Assert
        ArgumentException.ThrowIfNullOrEmpty(whitespaceString);
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithValidString_DoesNotThrow()
    {
        // Arrange
        var validString = "test";

        // Act & Assert
        ArgumentException.ThrowIfNullOrEmpty(validString);
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithSingleCharacter_DoesNotThrow()
    {
        // Arrange
        var singleChar = "a";

        // Act & Assert
        ArgumentException.ThrowIfNullOrEmpty(singleChar);
    }

    [Test]
    public void ThrowIfNullOrEmpty_WithLongString_DoesNotThrow()
    {
        // Arrange
        var longString = new string('a', 1000);

        // Act & Assert
        ArgumentException.ThrowIfNullOrEmpty(longString);
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        var empty = string.Empty;

        // Act & Assert
        var ex = await Assert.That(() =>
            MethodThatValidatesEmpty(empty)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("parameter");
    }

    #endregion

    #region ThrowIfNullOrWhiteSpace Tests

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithNullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(nullString)).Throws<ArgumentNullException>();

        await Assert.That(ex!.ParamName).IsEqualTo("nullString");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var emptyString = string.Empty;

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(emptyString)).Throws<ArgumentException>();

        await Assert.That(ex).IsNotNull();
        await Assert.That(ex!.Message).Contains("whitespace");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithSingleSpace_ThrowsArgumentException()
    {
        // Arrange
        var singleSpace = " ";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(singleSpace)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("singleSpace");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithMultipleSpaces_ThrowsArgumentException()
    {
        // Arrange
        var spaces = "     ";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(spaces)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("spaces");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithTab_ThrowsArgumentException()
    {
        // Arrange
        var tab = "\t";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(tab)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("tab");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithNewline_ThrowsArgumentException()
    {
        // Arrange
        var newline = "\n";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(newline)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("newline");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithCarriageReturn_ThrowsArgumentException()
    {
        // Arrange
        var carriageReturn = "\r";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(carriageReturn)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("carriageReturn");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithMixedWhitespace_ThrowsArgumentException()
    {
        // Arrange
        var mixedWhitespace = " \t\r\n ";

        // Act & Assert
        var ex = await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(mixedWhitespace)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("mixedWhitespace");
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithValidString_DoesNotThrow()
    {
        // Arrange
        var validString = "test";

        // Act & Assert
        ArgumentException.ThrowIfNullOrWhiteSpace(validString);
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithStringContainingSpaces_DoesNotThrow()
    {
        // Arrange
        var stringWithSpaces = "hello world";

        // Act & Assert
        ArgumentException.ThrowIfNullOrWhiteSpace(stringWithSpaces);
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithLeadingSpace_DoesNotThrow()
    {
        // Arrange
        var leadingSpace = " test";

        // Act & Assert
        ArgumentException.ThrowIfNullOrWhiteSpace(leadingSpace);
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithTrailingSpace_DoesNotThrow()
    {
        // Arrange
        var trailingSpace = "test ";

        // Act & Assert
        ArgumentException.ThrowIfNullOrWhiteSpace(trailingSpace);
    }

    [Test]
    public void ThrowIfNullOrWhiteSpace_WithSingleCharacter_DoesNotThrow()
    {
        // Arrange
        var singleChar = "a";

        // Act & Assert
        ArgumentException.ThrowIfNullOrWhiteSpace(singleChar);
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_CalledInMethod_CapturesCorrectParameterName()
    {
        // Arrange
        var whitespace = "   ";

        // Act & Assert
        var ex = await Assert.That(() =>
            MethodThatValidatesWhitespace(whitespace)).Throws<ArgumentException>();

        await Assert.That(ex!.ParamName).IsEqualTo("parameter");
    }

    #endregion

    #region Multiple Validation Tests

    [Test]
    public async Task ThrowIfNullOrEmpty_ThenThrowIfNullOrWhiteSpace_BothValidate()
    {
        // Arrange
        string? nullValue = null;
        var emptyValue = string.Empty;
        var whitespaceValue = "   ";
        var validValue = "test";

        // Act & Assert - ThrowIfNullOrEmpty
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(nullValue)).Throws<ArgumentNullException>();
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(emptyValue)).Throws<ArgumentException>();
        ArgumentException.ThrowIfNullOrEmpty(whitespaceValue);
        ArgumentException.ThrowIfNullOrEmpty(validValue);

        // Act & Assert - ThrowIfNullOrWhiteSpace
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(nullValue)).Throws<ArgumentNullException>();
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(emptyValue)).Throws<ArgumentException>();
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(whitespaceValue)).Throws<ArgumentException>();
        ArgumentException.ThrowIfNullOrWhiteSpace(validValue);
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_Empty() =>
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty("")).Throws<ArgumentException>();

    [Test]
    public async Task ThrowIfNullOrEmpty_Null() =>
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrEmpty(null)).Throws<ArgumentNullException>();

    [Arguments("a")]
    [Arguments("test")]
    [Arguments("hello world")]
    [Arguments("   with spaces   ")]
    [Test]
    public void ThrowIfNullOrEmpty_WithValidValues_DoesNotThrow(string value) =>
        ArgumentException.ThrowIfNullOrEmpty(value);

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithNull_Throws()
    {
        string? value = null;
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithEmpty_Throws()
    {
        var value = "";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithSingleSpace_Throws()
    {
        var value = " ";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithMultipleSpaces_Throws()
    {
        var value = "   ";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithTab_Throws()
    {
        var value = "\t";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithNewline_Throws()
    {
        var value = "\n";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithCarriageReturn_Throws()
    {
        var value = "\r";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WithMixedWhitespace_Throws()
    {
        var value = " \t\r\n ";
        await Assert.That(() =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value)).Throws<ArgumentException>();
    }

    [Arguments("a")]
    [Arguments("test")]
    [Arguments("hello world")]
    [Arguments(" test")]
    [Arguments("test ")]
    [Arguments(" test ")]
    [Test]
    public void ThrowIfNullOrWhiteSpace_WithValidValues_DoesNotThrow(string value) =>
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

    #endregion

    // Helper methods to test parameter name capture
    static void MethodThatValidatesEmpty(string? parameter) =>
        ArgumentException.ThrowIfNullOrEmpty(parameter);

    static void MethodThatValidatesWhitespace(string? parameter) =>
        ArgumentException.ThrowIfNullOrWhiteSpace(parameter);
}
