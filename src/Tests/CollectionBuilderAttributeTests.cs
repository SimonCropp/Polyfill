public class CollectionBuilderAttributeTests
{
    [Test]
    public Task CollectionBuilderAttributeTests_Compatibility_with_all_TargetFrameworks()
    {
        MyCollection myCollection = [1, 2, 3, 4, 5];
        return Task.CompletedTask;
    }

    [CollectionBuilder(typeof(MyCollection), nameof(Create))]
    public class MyCollection(ReadOnlySpan<int> initValues)
    {
        readonly int[] values = initValues.ToArray();
        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)values).GetEnumerator();

        public static MyCollection Create(ReadOnlySpan<int> values) => new(values);
    }
}
