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
    public void AgainstNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullArray));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullString));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullObject));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullICollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullEnumerable));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIReadOnlyDictionary));
    }

    [Test]
    public void AgainstNullOrEmpty()
    {
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyString));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyIList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyICollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyArray));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyEnumerable));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyIDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyIReadOnlyDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullString));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullICollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIReadOnlyList));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIReadOnlyCollection));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullArray));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullEnumerable));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIDictionary));
        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIReadOnlyDictionary));
    }
    [Test]
    public void AgainstEmpty()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyString));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyList));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyIList));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyICollection));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyIReadOnlyList));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyIReadOnlyCollection));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyArray));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyEnumerable));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyIDictionary));
        Assert.Throws<ArgumentException>(
            () => Guard.AgainstEmpty(emptyIReadOnlyDictionary));
        Guard.AgainstEmpty(nullString);
        Guard.AgainstEmpty(nullList);
        Guard.AgainstEmpty(nullIList);
        Guard.AgainstEmpty(nullICollection);
        Guard.AgainstEmpty(nullIReadOnlyList);
        Guard.AgainstEmpty(nullIReadOnlyCollection);
        Guard.AgainstEmpty(nullArray);
        Guard.AgainstEmpty(nullEnumerable);
        Guard.AgainstEmpty(nullDictionary);
        Guard.AgainstEmpty(nullIDictionary);
        Guard.AgainstEmpty(nullIReadOnlyDictionary);
    }
}