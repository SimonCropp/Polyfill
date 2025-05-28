// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

[TestFixture]
public static class Memory_CommonPrefixLength
{
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(0, 1)]
    [TestCase(2, 0)]
    [TestCase(0, 2)]
    public static void OneOrBothZeroLength_Returns0(int length1, int length2) =>
        ValidateWithDefaultValues(length1, length2, NonDefaultEqualityComparer<char>.Instance, 0);

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(15)]
    public static void SameLengthAllEqual_ReturnsLength(int length) =>
        ValidateWithDefaultValues(length, length, NonDefaultEqualityComparer<char>.Instance, length);

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(15)]
    public static void FirstShorterAllEqual_ReturnsFirstLength(int length) =>
        ValidateWithDefaultValues(length, length + 1, NonDefaultEqualityComparer<char>.Instance, length);

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(15)]
    public static void SecondShorterAllEqual_ReturnsSecondLength(int length) =>
        ValidateWithDefaultValues(length + 1, length, NonDefaultEqualityComparer<char>.Instance, length);

    static void ValidateWithDefaultValues<T>(int length1, int length2, IEqualityComparer<T> customComparer, int expected)
    {
        Assert.AreEqual(expected, ((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2]));
        Assert.AreEqual(expected, ((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], null));
        Assert.AreEqual(expected, ((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], EqualityComparer<T>.Default));
        Assert.AreEqual(expected, ((ReadOnlySpan<T>)new T[length1]).CommonPrefixLength(new T[length2], customComparer));

        Assert.AreEqual(expected, ((Span<T>)new T[length1]).CommonPrefixLength(new T[length2]));
        Assert.AreEqual(expected, ((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], null));
        Assert.AreEqual(expected, ((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], EqualityComparer<T>.Default));
        Assert.AreEqual(expected, ((Span<T>)new T[length1]).CommonPrefixLength(new T[length2], customComparer));
    }

    [TestCase]
    public static void PartialEquals_ReturnsPrefixLength_Byte()
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

        Assert.AreEqual(3, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(3, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(3, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default));
        Assert.AreEqual(3, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance));

        Assert.AreEqual(3, ((Span<byte>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(3, ((Span<byte>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(3, ((Span<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default));
        Assert.AreEqual(3, ((Span<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance));

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

        Assert.AreEqual(13, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(13, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(13, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default));
        Assert.AreEqual(13, ((ReadOnlySpan<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance));

        Assert.AreEqual(13, ((Span<byte>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(13, ((Span<byte>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(13, ((Span<byte>)arr1).CommonPrefixLength(arr2, EqualityComparer<byte>.Default));
        Assert.AreEqual(13, ((Span<byte>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<byte>.Instance));
    }

    [Test]
    public static void PartialEquals_ReturnsPrefixLength_ValueType()
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

        Assert.AreEqual(2, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(2, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(2, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
        Assert.AreEqual(2, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));

        Assert.AreEqual(2, ((Span<int>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(2, ((Span<int>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(2, ((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
        Assert.AreEqual(2, ((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));

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

        Assert.AreEqual(3, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(3, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(3, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
        Assert.AreEqual(3, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));

        Assert.AreEqual(3, ((Span<int>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(3, ((Span<int>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(3, ((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
        Assert.AreEqual(3, ((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));
    }

    [Test]
    public static void PartialEquals_ReturnsPrefixLength_ReferenceType()
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

        Assert.AreEqual(4, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(4, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(4, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default));
        Assert.AreEqual(4, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance));

        Assert.AreEqual(4, ((Span<string>)arr1).CommonPrefixLength(arr2));
        Assert.AreEqual(4, ((Span<string>)arr1).CommonPrefixLength(arr2, null));
        Assert.AreEqual(4, ((Span<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default));
        Assert.AreEqual(4, ((Span<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance));
    }

    [Test]
    public static void Comparer_UsedInComparisons_ReferenceType()
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

        Assert.AreEqual(4, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, StringComparer.OrdinalIgnoreCase));
        Assert.AreEqual(4, ((Span<string>)arr1).CommonPrefixLength(arr2, StringComparer.OrdinalIgnoreCase));

        Assert.AreEqual(1, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance));
        Assert.AreEqual(1, ((Span<string>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<string>.Instance));

        Assert.AreEqual(1, ((ReadOnlySpan<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default));
        Assert.AreEqual(1, ((Span<string>)arr1).CommonPrefixLength(arr2, EqualityComparer<string>.Default));
    }

    [Test]
    public static void Comparer_UsedInComparisons_ValueType()
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
        Assert.AreEqual(4, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, absoluteValueComparer));
        Assert.AreEqual(4, ((Span<int>)arr1).CommonPrefixLength(arr2, absoluteValueComparer));

        Assert.AreEqual(0, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));
        Assert.AreEqual(0, ((Span<int>)arr1).CommonPrefixLength(arr2, NonDefaultEqualityComparer<int>.Instance));

        Assert.AreEqual(0, ((ReadOnlySpan<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
        Assert.AreEqual(0, ((Span<int>)arr1).CommonPrefixLength(arr2, EqualityComparer<int>.Default));
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