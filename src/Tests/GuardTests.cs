using Polyfills;

[TestFixture]
public class GuardTests
{
    string nullString = null!;
    List<string> nullList = null!;
    IList<string> nullIList = null!;
    IReadOnlyList<string> nullIReadOnlyList = null!;
    ICollection<string> nullICollection = null!;
    IReadOnlyCollection<string> nullIReadOnlyCollection = null!;
    IEnumerable<string> nullEnumerable = null!;
    string[] nullArray = null!;
    Dictionary<int, string> nullDictionary = null!;
    IDictionary<int, string> nullIDictionary = null!;
    IReadOnlyDictionary<int, string> nullIReadOnlyDictionary = null!;
    object nullObject = null!;

    string emptyString = string.Empty;
    List<string> emptyList = [];
    IList<string> emptyIList = [];
    IReadOnlyList<string> emptyIReadOnlyList = [];
    ICollection<string> emptyICollection = [];
    IReadOnlyCollection<string> emptyIReadOnlyCollection = [];
    IEnumerable<string> emptyEnumerable = [];
    Dictionary<int, string> emptyDictionary = new();
    IDictionary<int, string> emptyIDictionary = new Dictionary<int, string>();
    IReadOnlyDictionary<int, string> emptyIReadOnlyDictionary = new Dictionary<int, string>();
    string[] emptyArray = [];

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
    }

    [Test]
    public void NotNullOrEmpty()
    {
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
        Guard.NotEmpty(nullEnumerable);
        Guard.NotEmpty(nullDictionary);
        Guard.NotEmpty(nullIDictionary);
        Guard.NotEmpty(nullIReadOnlyDictionary);
    }

    [Test]
    public void NotWhitespace()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhitespace(" \t"));
        Assert.Throws<ArgumentException>(
            () => Guard.NotWhitespace(string.Empty));
        Guard.NotWhitespace(null);
        Guard.NotWhitespace("value");
    }

    [Test]
    public void NotNullOrWhitespace()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhitespace(" \t"));
        Assert.Throws<ArgumentNullException>(
            () => Guard.NotNullOrWhitespace(null));
        Assert.Throws<ArgumentException>(
            () => Guard.NotNullOrWhitespace(string.Empty));
        Guard.NotNullOrWhitespace("value");
    }
}