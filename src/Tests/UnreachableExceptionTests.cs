public class UnreachableExceptionTests
{
    [Test]
    public async Task UnreachableException_Compatibility_with_all_TargetFrameworks() =>
        await Assert.That(() => throw new UnreachableException()).Throws<UnreachableException>();
}
