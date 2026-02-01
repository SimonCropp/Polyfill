partial class PolyfillTests
{
    [Test]
    public async Task EnumerationOptions_DefaultValues()
    {
        var options = new EnumerationOptions();

        await Assert.That(options.RecurseSubdirectories).IsFalse();
        await Assert.That(options.BufferSize).IsEqualTo(0);
        await Assert.That(options.MatchType).IsEqualTo(MatchType.Simple);
        await Assert.That(options.MatchCasing).IsEqualTo(MatchCasing.PlatformDefault);
        await Assert.That(options.ReturnSpecialDirectories).IsFalse();
    }

    [Test]
    public async Task EnumerationOptions_SetProperties()
    {
        var options = new EnumerationOptions
        {
            RecurseSubdirectories = true,
            BufferSize = 4096,
            AttributesToSkip = FileAttributes.ReadOnly,
            MatchType = MatchType.Win32,
            MatchCasing = MatchCasing.CaseInsensitive,
            ReturnSpecialDirectories = true
        };

        await Assert.That(options.RecurseSubdirectories).IsTrue();
        await Assert.That(options.BufferSize).IsEqualTo(4096);
        await Assert.That(options.AttributesToSkip).IsEqualTo(FileAttributes.ReadOnly);
        await Assert.That(options.MatchType).IsEqualTo(MatchType.Win32);
        await Assert.That(options.MatchCasing).IsEqualTo(MatchCasing.CaseInsensitive);
        await Assert.That(options.ReturnSpecialDirectories).IsTrue();
    }

    [Test]
    public async Task MatchType_Values()
    {
        var simple = MatchType.Simple;
        var win32 = MatchType.Win32;
        await Assert.That((int)simple).IsEqualTo(0);
        await Assert.That((int)win32).IsEqualTo(1);
    }

    [Test]
    public async Task MatchCasing_Values()
    {
        var platformDefault = MatchCasing.PlatformDefault;
        var caseSensitive = MatchCasing.CaseSensitive;
        var caseInsensitive = MatchCasing.CaseInsensitive;
        await Assert.That((int)platformDefault).IsEqualTo(0);
        await Assert.That((int)caseSensitive).IsEqualTo(1);
        await Assert.That((int)caseInsensitive).IsEqualTo(2);
    }
}
