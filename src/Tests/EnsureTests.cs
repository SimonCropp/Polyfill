public class EnsureTests
{
    static string nullString = null!;
    static List<string> nullList = null!;
    static IList<string> nullIList = null!;
    static IReadOnlyList<string> nullIReadOnlyList = null!;
    static ICollection<string> nullICollection = null!;
    static IReadOnlyCollection<string> nullIReadOnlyCollection = null!;
    static IEnumerable<string> nullEnumerable = null!;
    static string[] nullArray = null!;
    static Dictionary<int, string> nullDictionary = null!;
    static IDictionary<int, string> nullIDictionary = null!;
    static IReadOnlyDictionary<int, string> nullIReadOnlyDictionary = null!;
    static object nullObject = null!;

    static string emptyString = string.Empty;
    static List<string> emptyList = [];
    static IList<string> emptyIList = [];
    static IReadOnlyList<string> emptyIReadOnlyList = [];
    static ICollection<string> emptyICollection = [];
    static IReadOnlyCollection<string> emptyIReadOnlyCollection = [];
    static IEnumerable<string> emptyEnumerable = [];
    static Dictionary<int, string> emptyDictionary = [];
    static IDictionary<int, string> emptyIDictionary = new Dictionary<int, string>();
    static IReadOnlyDictionary<int, string> emptyIReadOnlyDictionary = new Dictionary<int, string>();
    static string[] emptyArray = [];
    static string[] nonEmptyArray = ["value"];
    static List<string> nonEmptyList = ["value"];
    static IEnumerable<string> nonEmptyEnumerable = nonEmptyList.Select(x => x);

    [Test]
    public async Task NotNull()
    {
        await Assert.That(() => Ensure.NotNull(nullArray)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullString)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullObject)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullICollection)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullIList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullIReadOnlyList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullIReadOnlyCollection)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullEnumerable)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullDictionary)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullIDictionary)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNull(nullIReadOnlyDictionary)).Throws<ArgumentNullException>();
        Ensure.NotNull(nonEmptyArray);
        Ensure.NotNull(nonEmptyEnumerable);
        Ensure.NotNull("value");
        Ensure.NotNull(nonEmptyEnumerable);
    }

    [Test]
    public async Task NotNullOrEmpty()
    {
#if FeatureMemory
        await Assert.That(() => Ensure.NotNullOrEmpty(Memory<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty((Memory<char>?) null)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty((ReadOnlyMemory<char>?) null)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(ReadOnlyMemory<char>.Empty)).Throws<ArgumentException>();
#endif
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyString)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyIList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyICollection)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyIReadOnlyList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyIReadOnlyCollection)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyArray)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyEnumerable)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyDictionary)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyIDictionary)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(emptyIReadOnlyDictionary)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullString)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullIList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullICollection)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullIReadOnlyList)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullIReadOnlyCollection)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullArray)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullEnumerable)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullDictionary)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullIDictionary)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrEmpty(nullIReadOnlyDictionary)).Throws<ArgumentNullException>();
        Ensure.NotNullOrEmpty(nonEmptyArray);
        Ensure.NotNullOrEmpty(nonEmptyEnumerable);
        Ensure.NotNullOrEmpty("value");
        Ensure.NotNullOrEmpty(nonEmptyEnumerable);
    }

    [Test]
    public async Task NotEmpty()
    {
        await Assert.That(() => Ensure.NotEmpty(emptyString)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyIList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyICollection)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyIReadOnlyList)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyIReadOnlyCollection)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyArray)).Throws<ArgumentException>();
#if FeatureMemory
        Span<char> buffer = [];
        var spanCaught = false;
        try
        {
            Ensure.NotEmpty(buffer);
        }
        catch (ArgumentException)
        {
            spanCaught = true;
        }

        await Assert.That(spanCaught).IsTrue();
        await Assert.That(() => Ensure.NotEmpty(Memory<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(Span<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(ReadOnlyMemory<char>.Empty)).Throws<ArgumentException>();
#endif
        await Assert.That(() => Ensure.NotEmpty(emptyEnumerable)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyDictionary)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyIDictionary)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotEmpty(emptyIReadOnlyDictionary)).Throws<ArgumentException>();
        Ensure.NotEmpty(nullString);
        Ensure.NotEmpty(nullList);
        Ensure.NotEmpty(nullIList);
        Ensure.NotEmpty(nullICollection);
        Ensure.NotEmpty(nullIReadOnlyList);
        Ensure.NotEmpty(nullIReadOnlyCollection);
        Ensure.NotEmpty(nullArray);
        Ensure.NotEmpty(nonEmptyArray);
        Ensure.NotEmpty("value");
        Ensure.NotEmpty(nonEmptyList);
        Ensure.NotEmpty(nullEnumerable);
        Ensure.NotEmpty(nullDictionary);
        Ensure.NotEmpty(nullIDictionary);
        Ensure.NotEmpty(nullIReadOnlyDictionary);
    }

    [Test]
    public async Task NotWhiteSpace()
    {
        await Assert.That(() => Ensure.NotWhiteSpace(" \t")).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotWhiteSpace(string.Empty)).Throws<ArgumentException>();
#if FeatureMemory
        Span<char> buffer = [];
        var spanCaught = false;
        try
        {
            Ensure.NotWhiteSpace(buffer);
        }
        catch (ArgumentException)
        {
            spanCaught = true;
        }

        await Assert.That(spanCaught).IsTrue();
        await Assert.That(() => Ensure.NotWhiteSpace(Memory<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotWhiteSpace(Span<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotWhiteSpace(ReadOnlyMemory<char>.Empty)).Throws<ArgumentException>();
        Ensure.NotWhiteSpace((Memory<char>?) null!);
        Ensure.NotWhiteSpace((ReadOnlyMemory<char>?) null!);
#endif
        // ReSharper disable once RedundantCast
        Ensure.NotWhiteSpace((string) null!);
        Ensure.NotWhiteSpace("value");
    }

    [Test]
    public async Task NotNullOrWhiteSpace()
    {
#if FeatureMemory
        await Assert.That(() => Ensure.NotNullOrWhiteSpace(Memory<char>.Empty)).Throws<ArgumentException>();
        await Assert.That(() => Ensure.NotNullOrWhiteSpace((Memory<char>?) null)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrWhiteSpace((ReadOnlyMemory<char>?) null)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrWhiteSpace(ReadOnlyMemory<char>.Empty)).Throws<ArgumentException>();
#endif
        await Assert.That(() => Ensure.NotNullOrWhiteSpace(" \t")).Throws<ArgumentException>();
        await Assert.That(
            // ReSharper disable once RedundantCast
            () => Ensure.NotNullOrWhiteSpace((string) null!)).Throws<ArgumentNullException>();
        await Assert.That(() => Ensure.NotNullOrWhiteSpace(string.Empty)).Throws<ArgumentException>();
        Ensure.NotNullOrWhiteSpace("value");
    }

    [Test]
    public void NotEqual_WhenValuesAreDifferent_DoesNotThrow()
    {
        Ensure.NotEqual(5, 10);
        Ensure.NotEqual("hello", "world");
        Ensure.NotEqual(DateTime.Now, DateTime.MinValue);
    }

    [Test]
    public async Task NotEqual_WhenValuesAreEqual_ThrowsArgumentOutOfRangeException()
    {
        const int value = 42;
        const int other = 42;

        var exception = await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("value");
        await Assert.That(exception.Message).Contains("must not be equal to");
        await Assert.That(exception.Message).Contains("'42'");
    }

    [Test]
    public async Task NotEqual_WhenStringsAreEqual_ThrowsArgumentOutOfRangeException()
    {
        const string value = "test";
        const string other = "test";

        var exception = await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("value");
        await Assert.That(exception.Message).Contains("'test'");
    }

    [Test]
    public async Task NotEqual_WhenBothNull_ThrowsArgumentOutOfRangeException()
    {
        string? value = null;
        string? other = null;

        var exception = await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("value");
        await Assert.That(exception.Message).Contains("'null'");
    }

    [Test]
    public void NotEqual_WhenOneIsNull_DoesNotThrow()
    {
        // Arrange & Act & Assert
        Ensure.NotEqual("value", null);
        Ensure.NotEqual(null, "value");
    }

    [Test]
    public async Task NotEqual_WithCustomType_WhenEqual_ThrowsArgumentOutOfRangeException()
    {
        var value = new Person("John", 30);
        var other = new Person("John", 30);

        await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public void NotEqual_WithCustomType_WhenDifferent_DoesNotThrow()
    {
        var value = new Person("John", 30);
        var other = new Person("Jane", 25);

        Ensure.NotEqual(value, other);
    }

    [Test]
    public async Task NotEqual_WithNullableValueType_WhenBothHaveSameValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 42;
        int? other = 42;

        await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task NotEqual_WithNullableValueType_WhenBothNull_ThrowsArgumentOutOfRangeException()
    {
        int? value = null;
        int? other = null;

        await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public void NotEqual_WithNullableValueType_WhenDifferent_DoesNotThrow()
    {
        Ensure.NotEqual<int?>(42, 10);
        Ensure.NotEqual((int?) 42, null);
        Ensure.NotEqual(null, (int?) 42);
    }

    [Test]
    public async Task NotEqual_PreservesParameterName_WhenUsingCallerArgumentExpression()
    {
        var myVariable = 100;
        var otherValue = 100;

        var exception = await Assert.That(() => Ensure.NotEqual(myVariable, otherValue)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("myVariable");
    }

    [Test]
    [Arguments(0, 1)]
    [Arguments(-5, 5)]
    [Arguments(int.MaxValue, int.MinValue)]
    public void NotEqual_WithDifferentIntegers_DoesNotThrow(int value, int other) =>
        Ensure.NotEqual(value, other);

    [Test]
    [Arguments(5, 5)]
    [Arguments(0, 0)]
    [Arguments(-10, -10)]
    public async Task NotEqual_WithEqualIntegers_ThrowsArgumentOutOfRangeException(int value, int other) =>
        await Assert.That(() => Ensure.NotEqual(value, other)).Throws<ArgumentOutOfRangeException>();

    record Person(string Name, int Age);

    #if NET10_0_OR_GREATER
    public class User
    {
        int age;
        string email;
        decimal accountBalance;
        string username;

        public User(string username, string email, int age, decimal accountBalance)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);
            ArgumentNullException.ThrowIfNull(email);
            ArgumentOutOfRangeException.ThrowIfNegative(age);
            ArgumentOutOfRangeException.ThrowIfNegative(accountBalance);
            this.username = username;
            this.age = age;
            this.email = email;
            this.accountBalance = accountBalance;
        }
    }
#endif

    [Test]
    public async Task NotNegativeOrZero_WithPositive_DoesNotThrow()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint positiveNint = 3;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var positiveInt = 1;
        var positiveDouble = 1.0;

        await Assert.That(() => Ensure.NotNegativeOrZero(positiveNint)).ThrowsNothing();
        await Assert.That(() => Ensure.NotNegativeOrZero(positiveInt)).ThrowsNothing();
        await Assert.That(() => Ensure.NotNegativeOrZero(positiveDouble)).ThrowsNothing();
    }

    [Test]
    public async Task NotNegativeOrZero_WithNegativeOrZero_ThrowsNegativeOrZero()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint minusOneNint = -1;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint zeroNint = 0;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var minusOneInt = -1;
        var zeroInt = 0;
        var zeroDouble = 0.0;

        await Assert.That(() => Ensure.NotNegativeOrZero(minusOneNint)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegativeOrZero(zeroNint)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegativeOrZero(zeroInt)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegativeOrZero(minusOneNint)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegativeOrZero(minusOneInt)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegativeOrZero(zeroDouble)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task NotNegative_WithNegative_ThrowsNegative()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint minusOneNint = -1;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var minusOneInt = -1;
        var minusTwoDouble = -2.0;

        await Assert.That(() => Ensure.NotNegative(minusOneNint)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegative(minusOneInt)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => Ensure.NotNegative(minusTwoDouble)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task NotNegative_WithPositive_DoesNotThrow()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint positiveNint = 3;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var positiveInt = 1;
        var positiveDouble = 0.0;

        await Assert.That(() => Ensure.NotNegative(positiveNint)).ThrowsNothing();
        await Assert.That(() => Ensure.NotNegative(positiveInt)).ThrowsNothing();
        await Assert.That(() => Ensure.NotNegative(positiveDouble)).ThrowsNothing();
    }

    [Test]
    public async Task NoDuplicates_WithUniqueValues_DoesNotThrow()
    {
        var values = new[] { 1, 2, 3, 4, 5 };

        await Assert.That(() => Ensure.NoDuplicates(values)).ThrowsNothing();
    }

    [Test]
    public async Task NoDuplicates_WithEmptyCollection_DoesNotThrow()
    {
        var values = Array.Empty<int>();

        await Assert.That(() => Ensure.NoDuplicates(values)).ThrowsNothing();
    }

    [Test]
    public async Task NoDuplicates_WithSingleItem_DoesNotThrow()
    {
        var values = new[] { 42 };

        await Assert.That(() => Ensure.NoDuplicates(values)).ThrowsNothing();
    }

    [Test]
    public async Task NoDuplicates_WithDuplicates_ThrowsArgumentException()
    {
        var values = new[] { 1, 2, 3, 2, 4 };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.ParamName).IsEqualTo("values");
    }

    [Test]
    public async Task NoDuplicates_WithDuplicates_MessageIncludesDuplicateValue()
    {
        var values = new[] { 1, 2, 3, 2, 4 };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.Message).Contains("Duplicate value: 2");
    }

    [Test]
    public async Task NoDuplicates_WithStringDuplicates_ThrowsWithCorrectValue()
    {
        var values = new[] { "apple", "banana", "cherry", "banana" };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.Message).Contains("Duplicate value: banana");
    }

    [Test]
    public async Task NoDuplicates_WithConsecutiveDuplicates_ThrowsOnFirstDuplicate()
    {
        var values = new[] { 1, 2, 2, 3, 3 };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.Message).Contains("Duplicate value: 2");
    }

    [Test]
    public async Task NoDuplicates_CapturesParameterName_UsingCallerArgumentExpression()
    {
        var myCollection = new[] { 1, 2, 3, 2 };

        var exception = await Assert.That(() => Ensure.NoDuplicates(myCollection)).Throws<ArgumentException>();

        await Assert.That(exception!.ParamName).IsEqualTo("myCollection");
    }

    [Test]
    public async Task NoDuplicates_WithReferenceTypeDuplicates_ThrowsCorrectly()
    {
        var person1 = new Person("John",20);
        var person2 = new Person("Jane",20);
        var values = new[] { person1, person2, person1 };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.Message).Contains($"Duplicate value: {person1}");
    }

    [Test]
    [Arguments(new[] { 1, 1 })]
    [Arguments(new[] { 5, 5, 5 })]
    [Arguments(new[] { 1, 2, 1 })]
    public async Task NoDuplicates_WithVariousDuplicatePatterns_Throws(int[] values) =>
        await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

    [Test]
    public async Task NoDuplicates_WithNullValues_HandlesCorrectly()
    {
        var values = new string?[] { "a", null, "b", null };

        var exception = await Assert.That(() => Ensure.NoDuplicates(values)).Throws<ArgumentException>();

        await Assert.That(exception!.Message).Contains("Duplicate value:");
    }
}
