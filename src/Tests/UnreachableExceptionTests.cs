namespace Tests;

[TestFixture]
public class UnreachableExceptionTests
{
    [Test]
    public void UnreachableException_Compatiblity_with_all_TargetFrameworks()
    {
        _ = Assert.Throws<UnreachableException>(() => throw new UnreachableException());
    }
}
