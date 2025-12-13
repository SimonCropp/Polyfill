// ReSharper disable RedundantExplicitParamsArrayCreation
// ReSharper disable ReplaceSliceWithRangeIndexer
[SuppressMessage("Style", "IDE0057:Use range operator")]
public class StringPolyfillTest
{
    [Test]
    public async Task Join()
    {
        await Assert.That(string.Join('a', ["b", "c"])).IsEqualTo("bac");
        await Assert.That(string.Join("a1", ["b", "c"])).IsEqualTo("ba1c");
        await Assert.That(string.Join("a1", ["b", null, "c"])).IsEqualTo("ba1a1c");
        await Assert.That(string.Join('a', new object[] {"b", "c"})).IsEqualTo("bac");
        await Assert.That(string.Join('a', new object?[] {"b", null, "c"})).IsEqualTo("baac");
        // ReSharper disable once RedundantCast
        await Assert.That(string.Join('a', (IEnumerable<string>) new List<string> {"b", "c"})).IsEqualTo("bac");
    }

    [Test]
    public async Task Create() =>
        await Assert.That(
            string.Create(
                5,
                'a',
                (span, state) =>
                {
                    for (var i = 0; i < span.Length; i++)
                    {
                        span[i] = state++;
                    }
                })).IsEqualTo("abcde");

    [Test]
    public async Task Create_WithZeroLength_ReturnsEmptyString()
    {
        var result = string.Create(0,
            (object?) null,
            static (span, state) => throw new InvalidOperationException("Should not be called"));

        await Assert.That(result).IsEqualTo(string.Empty);
        await Assert.That(result.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Create_WithNegativeLength_ThrowsArgumentOutOfRangeException() =>
        await Assert.That(
            () => string.Create(
                -1,
                (object?) null,
                static (span, state) =>
        {
        })).Throws<ArgumentOutOfRangeException>();

    [Test]
    public async Task Create_FillsStringWithCharacter()
    {
        var result = string.Create(
            5,
            'X',
            static (span, state) => span.Fill(state));

        await Assert.That(result).IsEqualTo("XXXXX");
    }

    [Test]
    public async Task Create_WithStateObject_UsesStateCorrectly()
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

        await Assert.That(result).IsEqualTo("Hello Worl");
    }

    [Test]
    public async Task Create_ReverseString_WorksCorrectly()
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

        await Assert.That(reversed).IsEqualTo("fedcba");
    }

    [Test]
    public async Task Create_WithNumbers_FormatsCorrectly()
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

        await Assert.That(result).IsEqualTo("1,2,3");
    }

    [Test]
    public async Task Create_LargeString_WorksCorrectly()
    {
        var length = 10000;
        var result = string.Create(
            length,
            'A',
            static (span, state) => span.Fill(state));

        await Assert.That(result.Length).IsEqualTo(length);
        await Assert.That(result.All(c => c == 'A')).IsTrue();
    }

    [Test]
    public async Task Create_WithComplexState_PreservesAllData()
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

        await Assert.That(result).IsEqualTo("123-Test-True\0\0\0\0\0\0\0");
    }

    [Test]
    public async Task Create_ModifyingEachCharacter_WorksCorrectly()
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

        await Assert.That(result).IsEqualTo("abcdefghijklmnopqrstuvwxyz");
    }

    [Test]
    public async Task Create_WithUnicodeCharacters_WorksCorrectly()
    {
        var emoji = "😀🎉🔥";

        var result = string.Create(
            emoji.Length,
            emoji,
            static (span, state) => state.AsSpan().CopyTo(span));

        await Assert.That(result).IsEqualTo(emoji);
    }
}