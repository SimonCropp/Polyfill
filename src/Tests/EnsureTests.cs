[TestFixture]
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
    public void NotNull()
    {
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullArray));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullString));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullObject));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullICollection));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullIList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullEnumerable));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullDictionary));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullIDictionary));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(nullIReadOnlyDictionary));
        Ensure.NotNull(nonEmptyArray);
        Ensure.NotNull(nonEmptyEnumerable);
        Ensure.NotNull("value");
        Ensure.NotNull(nonEmptyEnumerable);
    }

    [Test]
    public void NotNullOrEmpty()
    {
#if FeatureMemory
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(Memory<char>.Empty));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty((Memory<char>?) null));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty((ReadOnlyMemory<char>?) null));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyString));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyList));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyIList));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyICollection));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyArray));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyEnumerable));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyDictionary));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyIDictionary));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrEmpty(emptyIReadOnlyDictionary));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullString));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullIList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullICollection));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullArray));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullEnumerable));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullDictionary));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullIDictionary));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrEmpty(nullIReadOnlyDictionary));
        Ensure.NotNullOrEmpty(nonEmptyArray);
        Ensure.NotNullOrEmpty(nonEmptyEnumerable);
        Ensure.NotNullOrEmpty("value");
        Ensure.NotNullOrEmpty(nonEmptyEnumerable);
    }

    [Test]
    public void NotEmpty()
    {
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyString));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyList));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyIList));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyICollection));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyArray));
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

        Assert.True(spanCaught);
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(Memory<char>.Empty));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(Span<char>.Empty));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyEnumerable));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyDictionary));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyIDictionary));
        Assert.Throws<ArgumentException>(() => Ensure.NotEmpty(emptyIReadOnlyDictionary));
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
    public void NotWhiteSpace()
    {
        Assert.Throws<ArgumentException>(() => Ensure.NotWhiteSpace(" \t"));
        Assert.Throws<ArgumentException>(() => Ensure.NotWhiteSpace(string.Empty));
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

        Assert.True(spanCaught);
        Assert.Throws<ArgumentException>(() => Ensure.NotWhiteSpace(Memory<char>.Empty));
        Assert.Throws<ArgumentException>(() => Ensure.NotWhiteSpace(Span<char>.Empty));
        Assert.Throws<ArgumentException>(() => Ensure.NotWhiteSpace(ReadOnlyMemory<char>.Empty));
        Ensure.NotWhiteSpace((Memory<char>?) null!);
        Ensure.NotWhiteSpace((ReadOnlyMemory<char>?) null!);
#endif
        // ReSharper disable once RedundantCast
        Ensure.NotWhiteSpace((string) null!);
        Ensure.NotWhiteSpace("value");
    }

    [Test]
    public void NotNullOrWhiteSpace()
    {
#if FeatureMemory
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrWhiteSpace(Memory<char>.Empty));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrWhiteSpace((Memory<char>?) null));
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNullOrWhiteSpace((ReadOnlyMemory<char>?) null));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrWhiteSpace(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrWhiteSpace(" \t"));
        Assert.Throws<ArgumentNullException>(
            // ReSharper disable once RedundantCast
            () => Ensure.NotNullOrWhiteSpace((string) null!));
        Assert.Throws<ArgumentException>(() => Ensure.NotNullOrWhiteSpace(string.Empty));
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
    public void NotEqual_WhenValuesAreEqual_ThrowsArgumentOutOfRangeException()
    {
        const int value = 42;
        const int other = 42;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other))!;

        Assert.That(exception.ParamName, Is.EqualTo("value"));
        Assert.That(exception.Message, Does.Contain("must not be equal to"));
        Assert.That(exception.Message, Does.Contain("'42'"));
    }

    [Test]
    public void NotEqual_WhenStringsAreEqual_ThrowsArgumentOutOfRangeException()
    {
        const string value = "test";
        const string other = "test";

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other))!;

        Assert.That(exception.ParamName, Is.EqualTo("value"));
        Assert.That(exception.Message, Does.Contain("'test'"));
    }

    [Test]
    public void NotEqual_WhenBothNull_ThrowsArgumentOutOfRangeException()
    {
        string? value = null;
        string? other = null;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other))!;

        Assert.That(exception.ParamName, Is.EqualTo("value"));
        Assert.That(exception.Message, Does.Contain("'null'"));
    }

    [Test]
    public void NotEqual_WhenOneIsNull_DoesNotThrow()
    {
        // Arrange & Act & Assert
        Ensure.NotEqual("value", null);
        Ensure.NotEqual(null, "value");
    }

    [Test]
    public void NotEqual_WithCustomType_WhenEqual_ThrowsArgumentOutOfRangeException()
    {
        var value = new Person("John", 30);
        var other = new Person("John", 30);

        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other));
    }

    [Test]
    public void NotEqual_WithCustomType_WhenDifferent_DoesNotThrow()
    {
        var value = new Person("John", 30);
        var other = new Person("Jane", 25);

        Ensure.NotEqual(value, other);
    }

    [Test]
    public void NotEqual_WithNullableValueType_WhenBothHaveSameValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 42;
        int? other = 42;

        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other));
    }

    [Test]
    public void NotEqual_WithNullableValueType_WhenBothNull_ThrowsArgumentOutOfRangeException()
    {
        int? value = null;
        int? other = null;

        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other));
    }

    [Test]
    public void NotEqual_WithNullableValueType_WhenDifferent_DoesNotThrow()
    {
        Ensure.NotEqual<int?>(42, 10);
        Ensure.NotEqual((int?) 42, null);
        Ensure.NotEqual(null, (int?) 42);
    }

    [Test]
    public void NotEqual_PreservesParameterName_WhenUsingCallerArgumentExpression()
    {
        var myVariable = 100;
        var otherValue = 100;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(myVariable, otherValue))!;

        Assert.That(exception.ParamName, Is.EqualTo("myVariable"));
    }

    [TestCase(0, 1)]
    [TestCase(-5, 5)]
    [TestCase(int.MaxValue, int.MinValue)]
    public void NotEqual_WithDifferentIntegers_DoesNotThrow(int value, int other) =>
        Ensure.NotEqual(value, other);

    [TestCase(5, 5)]
    [TestCase(0, 0)]
    [TestCase(-10, -10)]
    public void NotEqual_WithEqualIntegers_ThrowsArgumentOutOfRangeException(int value, int other) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotEqual(value, other));

    private record Person(string Name, int Age);

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
    public void NotNegative_WithNegative_ThrowsNegative()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint minusOneNint = -1;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var minusOneInt = -1;
        var minusTwoDouble = -2.0;

        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegative(minusOneNint));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegative(minusOneInt));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegative(minusTwoDouble));
    }

    [Test]
    public void NotNegativeOrZero_WithPositive_DoesNotThrow()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint positiveNint = 3;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var positiveInt = 1;
        var positiveDouble = 0.0;

        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveNint));
        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveInt));
        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveDouble));
    }

    [Test]
    public void NotNegativeOrZero_WithNegativeOrZero_ThrowsNegativeOrZero()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint minusOneNint = -1;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint zeroNint = 0;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var minusOneInt = -1;
        var zeroInt = 0;
        var zeroDouble = 0.0;

        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(minusOneNint));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(zeroNint));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(zeroInt));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(minusOneNint));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(minusOneInt));
        Assert.Throws<ArgumentOutOfRangeException>(() => Ensure.NotNegativeOrZero(zeroDouble));
    }

    [Test]
    public void NotNegative_WithPositive_DoesNotThrow()
    {
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        nint positiveNint = 3;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        var positiveInt = 1;
        var positiveDouble = 0.0;

        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveNint));
        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveInt));
        Assert.DoesNotThrow(() => Ensure.NotNegative(positiveDouble));
    }

    [Test]
    public void NoDuplicates_WithUniqueValues_DoesNotThrow()
    {
        var values = new[] { 1, 2, 3, 4, 5 };

        Assert.DoesNotThrow(() => Ensure.NoDuplicates(values));
    }

    [Test]
    public void NoDuplicates_WithEmptyCollection_DoesNotThrow()
    {
        var values = Array.Empty<int>();

        Assert.DoesNotThrow(() => Ensure.NoDuplicates(values));
    }

    [Test]
    public void NoDuplicates_WithSingleItem_DoesNotThrow()
    {
        var values = new[] { 42 };

        Assert.DoesNotThrow(() => Ensure.NoDuplicates(values));
    }

    [Test]
    public void NoDuplicates_WithDuplicates_ThrowsArgumentException()
    {
        var values = new[] { 1, 2, 3, 2, 4 };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.ParamName, Is.EqualTo("values"));
    }

    [Test]
    public void NoDuplicates_WithDuplicates_MessageIncludesDuplicateValue()
    {
        var values = new[] { 1, 2, 3, 2, 4 };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.Message, Does.Contain("Duplicate value: 2"));
    }

    [Test]
    public void NoDuplicates_WithStringDuplicates_ThrowsWithCorrectValue()
    {
        var values = new[] { "apple", "banana", "cherry", "banana" };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.Message, Does.Contain("Duplicate value: banana"));
    }

    [Test]
    public void NoDuplicates_WithConsecutiveDuplicates_ThrowsOnFirstDuplicate()
    {
        var values = new[] { 1, 2, 2, 3, 3 };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.Message, Does.Contain("Duplicate value: 2"));
    }

    [Test]
    public void NoDuplicates_CapturesParameterName_UsingCallerArgumentExpression()
    {
        var myCollection = new[] { 1, 2, 3, 2 };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(myCollection))!;

        Assert.That(exception.ParamName, Is.EqualTo("myCollection"));
    }

    [Test]
    public void NoDuplicates_WithReferenceTypeDuplicates_ThrowsCorrectly()
    {
        var person1 = new Person("John",20);
        var person2 = new Person("Jane",20);
        var values = new[] { person1, person2, person1 };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.Message, Does.Contain($"Duplicate value: {person1}"));
    }

    [TestCase(new[] { 1, 1 })]
    [TestCase(new[] { 5, 5, 5 })]
    [TestCase(new[] { 1, 2, 1 })]
    public void NoDuplicates_WithVariousDuplicatePatterns_Throws(int[] values) =>
        Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values));

    [Test]
    public void NoDuplicates_WithNullValues_HandlesCorrectly()
    {
        var values = new string?[] { "a", null, "b", null };

        var exception = Assert.Throws<ArgumentException>(() => Ensure.NoDuplicates(values))!;

        Assert.That(exception.Message, Does.Contain("Duplicate value:"));
    }
}