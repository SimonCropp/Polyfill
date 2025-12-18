// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

public class Memory_CommonPrefixLength
{
    [Test]
    [Arguments(0, 0)]
    [Arguments(1, 0)]
    [Arguments(0, 1)]
    [Arguments(2, 0)]
    [Arguments(0, 2)]
    public async Task OneOrBothZeroLength_Returns0(int length1, int length2) =>
        await ValidateWithDefaultValues(length1, length2, NonDefaultEqualityComparer<char>.Instance, 0);

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(15)]
    public async Task SameLengthAllEqual_ReturnsLength(int length) =>
        await ValidateWithDefaultValues(length, length, NonDefaultEqualityComparer<char>.Instance, length);

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(15)]
    public async Task FirstShorterAllEqual_ReturnsFirstLength(int length) =>
        await ValidateWithDefaultValues(length, length + 1, NonDefaultEqualityComparer<char>.Instance, length);

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(15)]
    public async Task SecondShorterAllEqual_ReturnsSecondLength(int length) =>
        await ValidateWithDefaultValues(length + 1, length, NonDefaultEqualityComparer<char>.Instance, length);

    static async Task ValidateWithDefaultValues<T>(int length1, int length2, IEqualityComparer<T> customComparer, int expected)
    {
        await Assert.That(((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2])).IsEqualTo(expected);
        await Assert.That(((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], null)).IsEqualTo(expected);
        await Assert.That(((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], EqualityComparer<T>.Default)).IsEqualTo(expected);
        await Assert.That(((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], customComparer)).IsEqualTo(expected);

        await Assert.That(((Span<T>)new T[length1]).CommonPrefixLength(new T[length2])).IsEqualTo(expected);
        await Assert.That(((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], null)).IsEqualTo(expected);
        await Assert.That(((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], EqualityComparer<T>.Default)).IsEqualTo(expected);
        await Assert.That(((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], customComparer)).IsEqualTo(expected);
    }

    [Test]
    public async Task PartialEquals_ReturnsPrefixLength_Byte()
    {
        var arr1 = new byte[]
        {
            1,
            2,
            3,
            4,
            5
        };
        var arr2 = new byte[]
        {
            1,
            2,
            3,
            6,
            7
        };

        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance)).IsEqualTo(3);

        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2)).IsEqualTo(3);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(3);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default)).IsEqualTo(3);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance)).IsEqualTo(3);

        // Vectorized code path
        arr1 =
        [
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17
        ];
        arr2 =
        [
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            42,
            15,
            16,
            17
        ];

        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2)).IsEqualTo(13);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(13);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default)).IsEqualTo(13);
        await Assert.That(((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance)).IsEqualTo(13);

        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2)).IsEqualTo(13);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(13);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default)).IsEqualTo(13);
        await Assert.That(((Span<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance)).IsEqualTo(13);
    }

    [Test]
    public async Task PartialEquals_ReturnsPrefixLength_ValueType()
    {
        var arr1 = new int[]
        {
            1,
            2,
            3
        };
        var arr2 = new int[]
        {
            1,
            2,
            6
        };

        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2)).IsEqualTo(2);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(2);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(2);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(2);

        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2)).IsEqualTo(2);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(2);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(2);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(2);

        // Vectorized code path
        arr1 =
        [
            1,
            2,
            3,
            4,
            5
        ];
        arr2 =
        [
            1,
            2,
            3,
            6,
            7
        ];

        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(3);
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(3);

        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2)).IsEqualTo(3);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(3);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(3);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(3);
    }

    [Test]
    public async Task PartialEquals_ReturnsPrefixLength_ReferenceType()
    {
        var arr1 = new string[]
        {
            null!,
            "a",
            null!,
            "b",
            "c",
            "d",
            "e"
        };
        var arr2 = new string[]
        {
            null!,
            "a",
            null!,
            "b",
            "f",
            "g",
            "h"
        };

        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2)).IsEqualTo(4);
        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(4);
        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default)).IsEqualTo(4);
        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance)).IsEqualTo(4);

        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2)).IsEqualTo(4);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, null)).IsEqualTo(4);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default)).IsEqualTo(4);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance)).IsEqualTo(4);
    }

    [Test]
    public async Task Comparer_UsedInComparisons_ReferenceType()
    {
        var arr1 = new string[]
        {
            null!,
            "a",
            null!,
            "b",
            "c",
            "d",
            "e"
        };
        var arr2 = new string[]
        {
            null!,
            "A",
            null!,
            "B",
            "F",
            "G",
            "H"
        };

        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, StringComparer.OrdinalIgnoreCase)).IsEqualTo(4);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, StringComparer.OrdinalIgnoreCase)).IsEqualTo(4);

        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance)).IsEqualTo(1);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance)).IsEqualTo(1);

        await Assert.That(((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default)).IsEqualTo(1);
        await Assert.That(((Span<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default)).IsEqualTo(1);
    }

    [Test]
    public async Task Comparer_UsedInComparisons_ValueType()
    {
        var arr1 = new[]
        {
            1,
            2,
            3,
            4,
            5,
            6
        };
        var arr2 = new[]
        {
            -1,
            2,
            -3,
            4,
            -7,
            -8
        };

        var absoluteValueComparer = Create<int>((x, y) => Math.Abs(x) == Math.Abs(y));
        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, absoluteValueComparer)).IsEqualTo(4);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, absoluteValueComparer)).IsEqualTo(4);

        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(0);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance)).IsEqualTo(0);

        await Assert.That(((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(0);
        await Assert.That(((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default)).IsEqualTo(0);
    }

    sealed class NonDefaultEqualityComparer<T>
    {
        public static EqualityComparer<T> Instance { get; } = Create<T>((x, y) => EqualityComparer<T?>.Default.Equals(x, y));
    }

    static EqualityComparer<T> Create<T>(Func<T?, T?, bool> equals, Func<T, int>? getHashCode = null)
    {
        getHashCode ??= _ => throw new NotSupportedException();

        return new DelegateEqualityComparer<T>(equals, getHashCode);
    }

    sealed class DelegateEqualityComparer<T>(Func<T?, T?, bool> equals, Func<T, int> getHashCode) :
        EqualityComparer<T>
    {
        Func<T?, T?, bool> equals = equals;
        Func<T, int> getHashCode = getHashCode;

        public override bool Equals(T? x, T? y) =>
            equals(x, y);

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override int GetHashCode([DisallowNull] T obj) =>
            getHashCode(obj);

        public override bool Equals(object? obj) =>
            obj is DelegateEqualityComparer<T> other &&
            equals == other.equals &&
            getHashCode == other.getHashCode;

        public override int GetHashCode() =>
            equals.GetHashCode() + getHashCode.GetHashCode();
    }
}