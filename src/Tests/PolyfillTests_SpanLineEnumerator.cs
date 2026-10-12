partial class PolyfillTests
{
    static List<string> EnumerateLinesToList(string input)
    {
        var list = new List<string>();
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            list.Add(line.ToString());
        }

        return list;
    }

    [Test]
    [Arguments("", new[] {""})]
    [Arguments("a", new[] {"a"})]
    [Arguments("a\nb", new[] {"a", "b"})]
    [Arguments("a\rb", new[] {"a", "b"})]
    [Arguments("a\r\nb", new[] {"a", "b"})]
    [Arguments("a\n\rb", new[] {"a", "", "b"})]
    [Arguments("a\fb", new[] {"a", "b"})]
    [Arguments("a\u0085b", new[] {"a", "b"})]
    [Arguments("a\u2028b", new[] {"a", "b"})]
    [Arguments("a\u2029b", new[] {"a", "b"})]
    [Arguments("a\n", new[] {"a", ""})]
    [Arguments("a\r", new[] {"a", ""})]
    [Arguments("a\r\n", new[] {"a", ""})]
    [Arguments("\n", new[] {"", ""})]
    [Arguments("\r\n", new[] {"", ""})]
    [Arguments("\r\r\n\n", new[] {"", "", "", ""})]
    [Arguments("\na", new[] {"", "a"})]
    [Arguments("a\n\nb", new[] {"a", "", "b"})]
    [Arguments("a\r\n\r\nb", new[] {"a", "", "b"})]
    [Arguments("one\ntwo\rthree\r\nfour\ffive\u0085six\u2028seven\u2029eight", new[] {"one", "two", "three", "four", "five", "six", "seven", "eight"})]
    [Arguments("a\vb\tc\u0084d\u0086e\u2027f\u202Ag\u000Eh", new[] {"a\vb\tc\u0084d\u0086e\u2027f\u202Ag\u000Eh"})]
    public async Task ReadOnlySpan_EnumerateLines_Cases(string input, string[] expected)
    {
        var list = EnumerateLinesToList(input);

        await Assert.That(list.Count).IsEqualTo(expected.Length);
        for (var index = 0; index < expected.Length; index++)
        {
            await Assert.That(list[index]).IsEqualTo(expected[index]);
        }
    }

    [Test]
    public async Task ReadOnlySpan_EnumerateLines_OnlyNewlineCharsSplit()
    {
        var splitting = new List<int>();
        for (var value = 0; value <= char.MaxValue; value++)
        {
            var input = "a" + (char) value + "b";
            var count = 0;
            foreach (var line in input.AsSpan().EnumerateLines())
            {
                count++;
            }

            if (count != 1)
            {
                splitting.Add(value);
            }
        }

        await Assert.That(string.Join(",", splitting)).IsEqualTo("10,12,13,133,8232,8233");
    }

    [Test]
    public async Task ReadOnlySpan_EnumerateLines_ManyLfLines()
    {
        var builder = new StringBuilder();
        for (var index = 0; index < 5000; index++)
        {
            builder.Append("line ").Append(index).Append('\n');
        }

        var list = EnumerateLinesToList(builder.ToString());

        await Assert.That(list.Count).IsEqualTo(5001);
        await Assert.That(list[0]).IsEqualTo("line 0");
        await Assert.That(list[4999]).IsEqualTo("line 4999");
        await Assert.That(list[5000]).IsEqualTo("");
    }

    [Test]
    public async Task ReadOnlySpan_EnumerateLines_MoveNextAfterEnd()
    {
        var enumerator = "a\nb".AsSpan().EnumerateLines();

        var first = enumerator.MoveNext();
        var second = enumerator.MoveNext();
        var third = enumerator.MoveNext();
        var fourth = enumerator.MoveNext();

        await Assert.That(first).IsTrue();
        await Assert.That(second).IsTrue();
        await Assert.That(third).IsFalse();
        await Assert.That(fourth).IsFalse();
    }
}
