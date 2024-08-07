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
    Dictionary<int,string> nullDictionary = null!;
    IDictionary<int,string> nullIDictionary = null!;
    IReadOnlyDictionary<int,string> nullIReadOnlyDictionary = null!;
    object nullObject = null!;

    string emptyString = string.Empty;
    List<string> emptyList = [];
    IEnumerable<string> emptyEnumerable = [];
    Dictionary<int,string> emptyDictionary = new();
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
            () => Guard.AgainstNull(nullIList));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNull(nullIReadOnlyList));

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
            () => Guard.AgainstNullOrEmpty(emptyArray));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyEnumerable));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(emptyDictionary));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullString));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullList));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIList));

        Assert.Throws<ArgumentNullException>(
            () => Guard.AgainstNullOrEmpty(nullIReadOnlyList));

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

}