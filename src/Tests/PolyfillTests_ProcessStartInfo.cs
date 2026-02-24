partial class PolyfillTests
{
    [Test]
    public async Task ProcessStartInfoArgumentList()
    {
        var info = new ProcessStartInfo("cmd.exe");
        var argumentList = info.ArgumentList;
        await Assert.That(argumentList).IsNotNull();
        await Assert.That(argumentList.Count).IsEqualTo(0);

        argumentList.Add("/c");
        argumentList.Add("dir");

        await Assert.That(argumentList.Count).IsEqualTo(2);
        await Assert.That(argumentList[0]).IsEqualTo("/c");
        await Assert.That(argumentList[1]).IsEqualTo("dir");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListReturnsSameInstance()
    {
        var info = new ProcessStartInfo("cmd.exe");
        var list1 = info.ArgumentList;
        var list2 = info.ArgumentList;

        await Assert.That(list1).IsSameReferenceAs(list2);
    }

    [Test]
    public async Task ProcessStartInfoArgumentListIndependentOfArguments()
    {
        var info = new ProcessStartInfo("cmd.exe")
        {
            Arguments = "/c dir"
        };

        var argumentList = info.ArgumentList;
        await Assert.That(argumentList.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ProcessStartInfoArgumentListIndependentPerInstance()
    {
        var info1 = new ProcessStartInfo("cmd.exe");
        var info2 = new ProcessStartInfo("cmd.exe");

        info1.ArgumentList.Add("/c");

        await Assert.That(info1.ArgumentList.Count).IsEqualTo(1);
        await Assert.That(info2.ArgumentList.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ProcessStartInfoSetArgumentsThenAccessArgumentList()
    {
        var info = new ProcessStartInfo("cmd.exe")
        {
            Arguments = "/c dir"
        };

        // ArgumentList is always empty regardless of what Arguments contains
        await Assert.That(info.ArgumentList.Count).IsEqualTo(0);
        // Arguments is unaffected by accessing ArgumentList
        await Assert.That(info.Arguments).IsEqualTo("/c dir");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListItemsPersistAfterSettingArguments()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");

        info.Arguments = "echo hello";

        // The list is not cleared by setting Arguments
        await Assert.That(info.ArgumentList.Count).IsEqualTo(2);
        await Assert.That(info.ArgumentList[0]).IsEqualTo("/c");
        await Assert.That(info.ArgumentList[1]).IsEqualTo("dir");
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsThenAccessArgumentList()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        // ArgumentList is always empty regardless of constructor arguments
        await Assert.That(info.ArgumentList.Count).IsEqualTo(0);
        // The constructor-supplied Arguments string is preserved
        await Assert.That(info.Arguments).IsEqualTo("/c dir");
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsThenAddToArgumentList()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        info.ArgumentList.Add("--verbose");

        await Assert.That(info.ArgumentList.Count).IsEqualTo(1);
        await Assert.That(info.ArgumentList[0]).IsEqualTo("--verbose");
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsThenClearArgumentList()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        // Clearing an already-empty ArgumentList should be harmless
        info.ArgumentList.Clear();

        await Assert.That(info.ArgumentList.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsFileNamePreserved()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        info.ArgumentList.Add("--flag");

        await Assert.That(info.FileName).IsEqualTo("cmd.exe");
    }

    // On old frameworks the polyfill eagerly syncs ArgumentList to Arguments so
    // that Process.Start respects the list.  On new frameworks the runtime does
    // this internally at start time, so Arguments stays empty.  The tests below
    // verify the polyfill-specific syncing behaviour.
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

    [Test]
    public async Task ProcessStartInfoCtorArgumentsOverwrittenByArgumentListSync()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        // Adding to ArgumentList syncs and overwrites the constructor-supplied Arguments
        info.ArgumentList.Add("--verbose");

        await Assert.That(info.Arguments).IsEqualTo("--verbose");
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsClearedByArgumentListClear()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        // Access list, add something, then clear
        info.ArgumentList.Add("--flag");
        info.ArgumentList.Clear();

        await Assert.That(info.Arguments).IsEmpty();
    }

    [Test]
    public async Task ProcessStartInfoCtorArgumentsThenMultipleArgumentListMutations()
    {
        var info = new ProcessStartInfo("cmd.exe", "/c dir");

        info.ArgumentList.Add("one");
        info.ArgumentList.Add("two");
        info.ArgumentList[0] = "replaced";
        info.ArgumentList.Add("three");

        await Assert.That(info.Arguments).IsEqualTo("replaced two three");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListSyncsToArguments()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");
        info.ArgumentList.Add(@"C:\Program Files\dotnet");

        await Assert.That(info.Arguments).IsEqualTo("/c dir \"C:\\Program Files\\dotnet\"");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListEscapesQuotes()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("say");
        info.ArgumentList.Add("hello \"world\"");

        await Assert.That(info.Arguments).IsEqualTo("say \"hello \\\"world\\\"\"");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListClearResetsArguments()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");

        await Assert.That(info.Arguments).IsNotEmpty();

        info.ArgumentList.Clear();

        await Assert.That(info.Arguments).IsEmpty();
    }

    [Test]
    public async Task ProcessStartInfoArgumentListRemoveUpdatesArguments()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");
        info.ArgumentList.Add("extra");

        info.ArgumentList.RemoveAt(2);

        await Assert.That(info.Arguments).IsEqualTo("/c dir");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListSetItemUpdatesArguments()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");

        info.ArgumentList[1] = "echo";

        await Assert.That(info.Arguments).IsEqualTo("/c echo");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListEscapesBackslashesBeforeQuotes()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add(@"path\to\""file");

        await Assert.That(info.Arguments).IsEqualTo(@"""path\to\\\""file""");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListEscapesTrailingBackslashes()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add(@"path with\");

        await Assert.That(info.Arguments).IsEqualTo(@"""path with\\""");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListHandlesEmptyArgument()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("");

        await Assert.That(info.Arguments).IsEqualTo("\"\"");
    }

    [Test]
    public async Task ProcessStartInfoSetArgumentsThenMutateArgumentListOverwrites()
    {
        var info = new ProcessStartInfo("cmd.exe")
        {
            Arguments = "echo hello"
        };

        // Adding to ArgumentList syncs and overwrites Arguments
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");

        await Assert.That(info.Arguments).IsEqualTo("/c dir");
    }

    [Test]
    public async Task ProcessStartInfoArgumentListSyncThenSetArgumentsThenSyncAgain()
    {
        var info = new ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");

        await Assert.That(info.Arguments).IsEqualTo("/c");

        // Manually overwrite Arguments
        info.Arguments = "echo hello";
        await Assert.That(info.Arguments).IsEqualTo("echo hello");

        // List still has its items
        await Assert.That(info.ArgumentList.Count).IsEqualTo(1);

        // Next mutation syncs again, overwriting the manual value
        info.ArgumentList.Add("dir");
        await Assert.That(info.Arguments).IsEqualTo("/c dir");
    }

    [Test]
    public async Task SyncSimpleArguments()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("one");
        list.Add("two");
        list.Add("three");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("one two three");
    }

    [Test]
    public async Task SyncEmptyList()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("");
    }

    [Test]
    public async Task SyncEmptyStringArgument()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("\"\"");
    }

    [Test]
    public async Task SyncArgumentWithSpaces()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("hello world");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("\"hello world\"");
    }

    [Test]
    public async Task SyncArgumentWithTabs()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("hello\tworld");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("\"hello\tworld\"");
    }

    [Test]
    public async Task SyncArgumentWithEmbeddedQuotes()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("say \"hi\"");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("\"say \\\"hi\\\"\"");
    }

    [Test]
    public async Task SyncArgumentWithBackslashNotBeforeQuote()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add(@"a\b\c");
        list.Sync();

        // Backslashes not before quotes are kept as-is
        await Assert.That(info.Arguments).IsEqualTo(@"a\b\c");
    }

    [Test]
    public async Task SyncArgumentWithBackslashesBeforeQuote()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("a\\\"b");
        list.Sync();

        // 1 backslash before quote => 2*1+1=3 backslashes then escaped quote
        await Assert.That(info.Arguments).IsEqualTo("\"a\\\\\\\"b\"");
    }

    [Test]
    public async Task SyncArgumentWithTrailingBackslashes()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("path with\\");
        list.Sync();

        // Trailing backslash before closing quote is doubled
        await Assert.That(info.Arguments).IsEqualTo("\"path with\\\\\"");
    }

    [Test]
    public async Task SyncArgumentWithMultipleTrailingBackslashes()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("path with\\\\");
        list.Sync();

        // 2 trailing backslashes doubled to 4
        await Assert.That(info.Arguments).IsEqualTo("\"path with\\\\\\\\\"");
    }

    [Test]
    public async Task SyncArgumentWithOnlyQuote()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("\"");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("\"\\\"\"");
    }

    [Test]
    public async Task SyncMixedSimpleAndComplexArguments()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("/c");
        list.Add("dir");
        list.Add(@"C:\Program Files\dotnet");
        list.Add("hello \"world\"");
        list.Sync();

        await Assert.That(info.Arguments).IsEqualTo("/c dir \"C:\\Program Files\\dotnet\" \"hello \\\"world\\\"\"");
    }

    [Test]
    public async Task SyncSingleSimpleArgument()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("simple");
        list.Sync();

        // No quoting needed
        await Assert.That(info.Arguments).IsEqualTo("simple");
    }

    [Test]
    public async Task SyncArgumentWithBackslashesThenQuote()
    {
        var info = new ProcessStartInfo();
        var list = new Polyfill.SyncingArgumentList(info);
        list.Add("a\\\\\"b");
        list.Sync();

        // 2 backslashes before quote => 2*2+1=5 backslashes then escaped quote
        await Assert.That(info.Arguments).IsEqualTo("\"a\\\\\\\\\\\"b\"");
    }

#endif
}
