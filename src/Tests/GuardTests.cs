﻿[TestFixture]
public class GuardTests
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
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullArray));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullString));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullObject));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullICollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullIList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullEnumerable));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullIDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNull(nullIReadOnlyDictionary));
        Guard.NotNull(nonEmptyArray);
        Guard.NotNull(nonEmptyEnumerable);
        Guard.NotNull("value");
        Guard.NotNull(nonEmptyEnumerable);
    }

    [Test]
    public void NotNullOrEmpty()
    {
#if FeatureMemory
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(Memory<char>.Empty));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty((Memory<char>?) null));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty((ReadOnlyMemory<char>?) null));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyString));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyIList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyICollection));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyArray));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyEnumerable));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyIDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrEmpty(emptyIReadOnlyDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullString));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullIList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullICollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullArray));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullEnumerable));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullIDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrEmpty(nullIReadOnlyDictionary));
        Guard.NotNullOrEmpty(nonEmptyArray);
        Guard.NotNullOrEmpty(nonEmptyEnumerable);
        Guard.NotNullOrEmpty("value");
        Guard.NotNullOrEmpty(nonEmptyEnumerable);
    }

    [Test]
    public void NotEmpty()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyString));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyIList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyICollection));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyArray));
#if FeatureMemory
        Span<char> buffer = [];
        var spanCaught = false;
        try
        {
            Guard.NotEmpty(buffer);
        }
        catch (ArgumentException)
        {
            spanCaught = true;
        }

        Assert.True(spanCaught);
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(Memory<char>.Empty));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(Span<char>.Empty));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyEnumerable));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyIDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(emptyIReadOnlyDictionary));
        Guard.NotEmpty(nullString);
        Guard.NotEmpty(nullList);
        Guard.NotEmpty(nullIList);
        Guard.NotEmpty(nullICollection);
        Guard.NotEmpty(nullIReadOnlyList);
        Guard.NotEmpty(nullIReadOnlyCollection);
        Guard.NotEmpty(nullArray);
        Guard.NotEmpty(nonEmptyArray);
        Guard.NotEmpty("value");
        Guard.NotEmpty(nonEmptyList);
        Guard.NotEmpty(nullEnumerable);
        Guard.NotEmpty(nullDictionary);
        Guard.NotEmpty(nullIDictionary);
        Guard.NotEmpty(nullIReadOnlyDictionary);
    }

    [Test]
    public void NotWhiteSpace()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhiteSpace(" \t"));
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhiteSpace(string.Empty));
#if FeatureMemory
        Span<char> buffer = [];
        var spanCaught = false;
        try
        {
            Guard.NotWhiteSpace(buffer);
        }
        catch (ArgumentException)
        {
            spanCaught = true;
        }

        Assert.True(spanCaught);
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhiteSpace(Memory<char>.Empty));
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhiteSpace(Span<char>.Empty));
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhiteSpace(ReadOnlyMemory<char>.Empty));
        Guard.NotWhiteSpace((Memory<char>?) null!);
        Guard.NotWhiteSpace((ReadOnlyMemory<char>?) null!);
#endif
        // ReSharper disable once RedundantCast
        Guard.NotWhiteSpace((string) null!);
        Guard.NotWhiteSpace("value");
    }

    [Test]
    public void NotNullOrWhiteSpace()
    {
#if FeatureMemory
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhiteSpace(Memory<char>.Empty));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrWhiteSpace((Memory<char>?) null));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrWhiteSpace((ReadOnlyMemory<char>?) null));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhiteSpace(ReadOnlyMemory<char>.Empty));
#endif
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhiteSpace(" \t"));
        Assert.Throws<ArgumentNullException>(
            // ReSharper disable once RedundantCast
            () => Guard.NotNullOrWhiteSpace((string) null!));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhiteSpace(string.Empty));
        Guard.NotNullOrWhiteSpace("value");
    }
}