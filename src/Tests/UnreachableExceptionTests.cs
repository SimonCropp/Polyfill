[TestFixture]
public class UnreachableExceptionTests
{
    [Test]
    public void UnreachableException_Compatibility_with_all_TargetFrameworks() =>
        _ = Assert.Throws<UnreachableException>(() => throw new UnreachableException());
}
