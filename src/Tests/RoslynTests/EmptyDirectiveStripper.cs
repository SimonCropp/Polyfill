#if NET9_0
[TestFixture]
public class EmptyDirectiveStripper
{
    [Test]
    public void EmptyIf()
    {
        var replace = Replace(
            """
            !if NET9_0
            !endif
            """);
        var result = Strip(replace);
        Assert.AreEqual("", result);
    }
    [Test]
    public void EmptyNested()
    {
        var replace = Replace(
            """
            !if NET9_0
            !if NET8_0
            !endif
            !endif
            """);
        var result = Strip(replace);
        Assert.AreEqual("", result);
    }

    [Test]
    public void NonEmptyIf()
    {
        var code = """
                   !if NET9_0
                   aa
                   !endif
                   """;
        var replace = Replace(code);
        var result = Strip(replace);
        Assert.AreEqual(replace, result);
    }

    [Test]
    public void IfElseEndif_EmptyIf()
    {
        var replace = Replace(
            """
            !if NET9_0
            !else NET8_0
            !endif
            """);
        var result = Strip(replace);
        Assert.AreEqual("", result);
    }

    [Test]
    public void IfElseEndif_EmptyElse()
    {
        var replace = Replace(
            """
            !if NET9_0
            a
            !else NET8_0
            !endif
            """);
        var result = Strip(replace);
        var expected = Replace(
            """
            !if NET9_0
            a
            !endif
            """);
        Assert.AreEqual(expected, result);
    }

    static string Replace(string code) =>
        code.Replace('!', '#');

    public static string Strip(string code)
    {
        var split = code.Split("\r\n").ToList();
        while (true)
        {
            var result = InnerSplit(split);
            if(result.Count == split.Count)
            {
                break;
            }
            split = result;
        }

        return string.Join("\r\n", split);
    }

    static List<string> InnerSplit(List<string> split)
    {
        var result = new List<string>();
        for (var index = 0; index < split.Count; index++)
        {
            var line = split[index];
            if(line.StartsWith("#if"))
            {
                var next = split[index + 1];
                if (next.StartsWith("#endif"))
                {
                    index++;
                    continue;
                }
                if (next.StartsWith("#else") &&
                    split[index + 2].StartsWith("#endif"))
                {
                    index += 2;
                    continue;
                }
            }
            if(line.StartsWith("#else"))
            {
                var next = split[index + 1];
                if (next.StartsWith("#endif"))
                {
                    continue;
                }
            }
            result.Add(line);
        }

        return result;
    }
}

#endif