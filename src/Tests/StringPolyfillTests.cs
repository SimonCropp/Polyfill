// ReSharper disable RedundantExplicitParamsArrayCreation
// ReSharper disable ReplaceSliceWithRangeIndexer
[TestFixture]
[SuppressMessage("Style", "IDE0057:Use range operator")]
public class StringPolyfillTest
{
    [Test]
    public void Join()
    {
        Assert.AreEqual("bac", string.Join('a', ["b", "c"]));
        Assert.AreEqual("ba1c", string.Join("a1", ["b", "c"]));
        Assert.AreEqual("ba1a1c", string.Join("a1", ["b", null, "c"]));
        Assert.AreEqual("bac", string.Join('a', new object[] {"b", "c"}));
        Assert.AreEqual("baac", string.Join('a', new object?[] {"b", null, "c"}));
        // ReSharper disable once RedundantCast
        Assert.AreEqual("bac", string.Join('a', (IEnumerable<string>) new List<string> {"b", "c"}));
    }

    [Test]
    public void Create() =>
        Assert.AreEqual("abcde",
            string.Create(
                5,
                'a',
                (span, state) =>
                {
                    for (var i = 0; i < span.Length; i++)
                    {
                        span[i] = state++;
                    }
                }));

    [Test]
    public void Create_WithZeroLength_ReturnsEmptyString()
    {
        var result = string.Create(0,
            (object?) null,
            static (span, state) => throw new InvalidOperationException("Should not be called"));

        Assert.That(result, Is.EqualTo(string.Empty));
        Assert.That(result.Length, Is.EqualTo(0));
    }

    [Test]
    public void Create_WithNegativeLength_ThrowsArgumentOutOfRangeException() =>
        Assert.Throws<ArgumentOutOfRangeException>(
            () => string.Create(
                -1,
                (object?) null,
                static (span, state) =>
        {
        }));

    [Test]
    public void Create_FillsStringWithCharacter()
    {
        var result = string.Create(
            5,
            'X',
            static (span, state) => span.Fill(state));

        Assert.That(result, Is.EqualTo("XXXXX"));
    }

    [Test]
    public void Create_WithStateObject_UsesStateCorrectly()
    {
        var state = (first: "Hello", second: "World");

        var result = string.Create(
            10,
            state,
            static (span, s) =>
            {
                s.first.AsSpan().CopyTo(span);
                span[5] = ' ';
                s.second.AsSpan().Slice(0, 4).CopyTo(span.Slice(6));
            });

        Assert.That(result, Is.EqualTo("Hello Worl"));
    }

    [Test]
    public void Create_ReverseString_WorksCorrectly()
    {
        var original = "abcdef";

        var reversed = string.Create(
            original.Length,
            original,
            static (span, state) =>
            {
                for (var i = 0; i < state.Length; i++)
                {
                    span[i] = state[state.Length - 1 - i];
                }
            });

        Assert.That(reversed, Is.EqualTo("fedcba"));
    }

    [Test]
    public void Create_WithNumbers_FormatsCorrectly()
    {
        var numbers = new[] {1, 2, 3};

        var result = string.Create(
            5,
            numbers,
            static (span, state) =>
            {
                span[0] = (char) ('0' + state[0]);
                span[1] = ',';
                span[2] = (char) ('0' + state[1]);
                span[3] = ',';
                span[4] = (char) ('0' + state[2]);
            });

        Assert.That(result, Is.EqualTo("1,2,3"));
    }

    [Test]
    public void Create_LargeString_WorksCorrectly()
    {
        var length = 10000;
        var result = string.Create(
            length,
            'A',
            static (span, state) => span.Fill(state));

        Assert.That(result.Length, Is.EqualTo(length));
        Assert.That(result.All(c => c == 'A'), Is.True);
    }

    [Test]
    public void Create_WithComplexState_PreservesAllData()
    {
        var state = new
        {
            Id = 123,
            Name = "Test",
            IsActive = true
        };

        var result = string.Create(
            20,
            state,
            static (span, s) =>
            {
                var pos = 0;

                // Write ID
                var idStr = s.Id.ToString();
                idStr.AsSpan().CopyTo(span.Slice(pos));
                pos += idStr.Length;

                span[pos++] = '-';

                // Write Name
                s.Name.AsSpan().CopyTo(span.Slice(pos));
                pos += s.Name.Length;

                span[pos++] = '-';

                // Write IsActive
                var activeStr = s.IsActive ? "True" : "False";
                activeStr.AsSpan().CopyTo(span.Slice(pos));
            });

        Assert.That(result, Is.EqualTo("123-Test-True\0\0\0\0\0\0\0"));
    }

    [Test]
    public void Create_ModifyingEachCharacter_WorksCorrectly()
    {
        var result = string.Create(
            26,
            'a',
            static (span, startChar) =>
            {
                for (var i = 0; i < span.Length; i++)
                {
                    span[i] = (char) (startChar + i);
                }
            });

        Assert.That(result, Is.EqualTo("abcdefghijklmnopqrstuvwxyz"));
    }

    [Test]
    public void Create_WithUnicodeCharacters_WorksCorrectly()
    {
        var emoji = "😀🎉🔥";

        var result = string.Create(
            emoji.Length,
            emoji,
            static (span, state) => state.AsSpan().CopyTo(span));

        Assert.That(result, Is.EqualTo(emoji));
    }
}