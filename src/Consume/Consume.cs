// ReSharper disable RedundantUsingDirective
#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
#pragma warning disable CS4014

class Consume
{
    Consume()
    {
        var type = typeof(AllowNullAttribute);
        type = typeof(DisallowNullAttribute);
        type = typeof(DoesNotReturnAttribute);
        type = typeof(DoesNotReturnIfAttribute);
        type = typeof(MaybeNullAttribute);
        type = typeof(MaybeNullWhenAttribute);
        type = typeof(MemberNotNullAttribute);
        type = typeof(MemberNotNullWhenAttribute);
        type = typeof(NotNullAttribute);
        type = typeof(NotNullIfNotNullAttribute);
        type = typeof(NotNullWhenAttribute);
        type = typeof(CallerArgumentExpressionAttribute);
        type = typeof(IsExternalInit);
        type = typeof(ModuleInitializerAttribute);
        type = typeof(RequiredMemberAttribute);
        type = typeof(SetsRequiredMembersAttribute);
        type = typeof(SkipLocalsInitAttribute);
        //TODO:
        // type = typeof(TupleElementNamesAttribute);
        type = typeof(DebuggerNonUserCodeAttribute);
        type = typeof(UnscopedRefAttribute);
        type = typeof(InterpolatedStringHandlerArgumentAttribute);
        type = typeof(InterpolatedStringHandlerAttribute);
        type = typeof(StringSyntaxAttribute);
        type = typeof(DynamicallyAccessedMembersAttribute);
        type = typeof(DynamicDependencyAttribute);
        type = typeof(RequiresDynamicCodeAttribute);
        type = typeof(RequiresUnreferencedCodeAttribute);
        type = typeof(UnconditionalSuppressMessageAttribute);
        type = typeof(CompilerFeatureRequiredAttribute);
        //TODO:
        //type = typeof(AsyncMethodBuilderAttribute);
        type = typeof(ObsoletedOSPlatformAttribute);
        type = typeof(SupportedOSPlatformAttribute);
        type = typeof(SupportedOSPlatformGuardAttribute);
        type = typeof(TargetPlatformAttribute);
        type = typeof(UnsupportedOSPlatformAttribute);
        type = typeof(UnsupportedOSPlatformGuardAttribute);
        type = typeof(StackTraceHiddenAttribute);
        type = typeof(UnmanagedCallersOnlyAttribute);
        type = typeof(SuppressGCTransitionAttribute);
        type = typeof(DisableRuntimeMarshallingAttribute);
        type = typeof(RequiresUnreferencedCodeAttribute);
        type = typeof(UnreachableException);

#if FeatureValueTuple
        var range = "value"[1..];
        var index = "value"[^2];
        var subArray = new[]
        {
            "value1",
            "value2"
        }[..1];
#endif

        var startsWith = "value".StartsWith('a');
        var endsWith = "value".EndsWith('a');

        IList<string> ilist = new List<string>();
        ilist.AsReadOnly();

        IDictionary<int, int> idictionary = new Dictionary<int, int>();
        idictionary.AsReadOnly();

        typeof(List<string>).IsAssignableTo(typeof(string));
        typeof(List<string>).IsAssignableTo(null);

        var enumerable = (IEnumerable<string>) new List<string>
        {
            "a",
            "b"
        };
        enumerable.TryGetNonEnumeratedCount(out var count);
        var append = enumerable.Append("c");
        var maxBy = enumerable.MaxBy(_ => _);
        var chunk = enumerable.Chunk(3);
        var minBy = enumerable.MinBy(_ => _);
        var distinctBy = enumerable.DistinctBy(_ => _);
        var skipLast = enumerable.SkipLast(1);

        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        dictionary.GetValueOrDefault("key");
        dictionary.GetValueOrDefault("key", "default");

        var concurrentDictionary = new ConcurrentDictionary<string, int>();

        var value = concurrentDictionary.GetOrAdd("Hello", (_, arg) => arg.Length, "World");

        var split = "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries);
        split = "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var contains = "a b".Contains(' ');

        // Test to make sure there are no clashes in the Polyfill code with classes that
        // might be defined in user code. See comments in Debug.cs for more details.
        Debug.Log("Test log to make sure this is working");

        BytePolyfill.TryParse("1", null, out _);
#if FeatureMemory
        BytePolyfill.TryParse("1"u8, null, out _);
        BytePolyfill.TryParse(['1'], out _);
        BytePolyfill.TryParse(['1'], null, out _);
        BytePolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        BytePolyfill.TryParse("1"u8, out _);
        BytePolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        DoublePolyfill.TryParse("1", null, out _);
#if FeatureMemory
        DoublePolyfill.TryParse("1"u8, null, out _);
        DoublePolyfill.TryParse(['1'], out _);
        DoublePolyfill.TryParse(['1'], null, out _);
        DoublePolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        DoublePolyfill.TryParse("1"u8, out _);
        DoublePolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        IntPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        IntPolyfill.TryParse("1"u8, null, out _);
        IntPolyfill.TryParse(['1'], out _);
        IntPolyfill.TryParse(['1'], null, out _);
        IntPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        IntPolyfill.TryParse("1"u8, out _);
        IntPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        LongPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        LongPolyfill.TryParse("1"u8, null, out _);
        LongPolyfill.TryParse(['1'], out _);
        LongPolyfill.TryParse(['1'], null, out _);
        LongPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        LongPolyfill.TryParse("1"u8, out _);
        LongPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        SBytePolyfill.TryParse("1", null, out _);
#if FeatureMemory
        SBytePolyfill.TryParse("1"u8, null, out _);
        SBytePolyfill.TryParse(['1'], out _);
        SBytePolyfill.TryParse(['1'], null, out _);
        SBytePolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        SBytePolyfill.TryParse("1"u8, out _);
        SBytePolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        ShortPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        ShortPolyfill.TryParse("1"u8, null, out _);
        ShortPolyfill.TryParse(['1'], out _);
        ShortPolyfill.TryParse(['1'], null, out _);
        ShortPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        ShortPolyfill.TryParse("1"u8, out _);
        ShortPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        UIntPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        UIntPolyfill.TryParse("1"u8, null, out _);
        UIntPolyfill.TryParse(['1'], out _);
        UIntPolyfill.TryParse(['1'], null, out _);
        UIntPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        UIntPolyfill.TryParse("1"u8, out _);
        UIntPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        ULongPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        ULongPolyfill.TryParse("1"u8, null, out _);
        ULongPolyfill.TryParse(['1'], out _);
        ULongPolyfill.TryParse(['1'], null, out _);
        ULongPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        ULongPolyfill.TryParse("1"u8, out _);
        ULongPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
        UShortPolyfill.TryParse("1", null, out _);
#if FeatureMemory
        UShortPolyfill.TryParse("1"u8, null, out _);
        UShortPolyfill.TryParse(['1'], out _);
        UShortPolyfill.TryParse(['1'], null, out _);
        UShortPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out _);
        UShortPolyfill.TryParse("1"u8, out _);
        UShortPolyfill.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

#if FetureHttp

    static void Http()
    {
        new HttpClient().GetStreamAsync("", CancellationToken.None);
        new HttpClient().GetStreamAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetByteArrayAsync("", CancellationToken.None);
        new HttpClient().GetByteArrayAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetStringAsync("", CancellationToken.None);
        new HttpClient().GetStringAsync(new Uri(""), CancellationToken.None);

        new ByteArrayContent([]).ReadAsStreamAsync(CancellationToken.None);
        new ByteArrayContent([]).ReadAsByteArrayAsync(CancellationToken.None);
        new ByteArrayContent([]).ReadAsStringAsync(CancellationToken.None);
    }

#endif

    void KeyValuePairDeconstruct(IEnumerable<KeyValuePair<string, string>> variables)
    {
        foreach (var (name, value) in variables)
        {
        }
    }

#if FeatureValueTuple

    static (string value1, bool value2) NamedTupleMethod() =>
        new("value", false);

#endif

    #pragma warning disable ExperimentalMethod
    static void ExperimentalMethodUsage() =>
        ExperimentalMethod();

    [Experimental("ExperimentalMethod")]
    static void ExperimentalMethod()
    {
    }

    async Task CancellationTokenSource()
    {
        var source = new CancellationTokenSource();
        await source.CancelAsync();
    }

    void CancellationTokenUnsafeRegister()
    {
        var source = new CancellationTokenSource();
        var token = source.Token;
        token.UnsafeRegister(state => {}, null);
        token.UnsafeRegister((state, token) => {}, null);
    }

    async Task ProcessWaitForExitAsync()
    {
        var process = new Process();
        await process.WaitForExitAsync();
    }

    async Task StreamReaderReadToEndAsync()
    {
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadToEndAsync(CancellationToken.None);
    }

    async Task StreamReaderReadLineAsync()
    {
        TextReader reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadLineAsync(CancellationToken.None);
    }

    void WaitAsync()
    {
        var action = () => {};
        var func = () => 0;
        new Task(action).WaitAsync(CancellationToken.None);
        new Task(action).WaitAsync(TimeSpan.Zero);
        new Task(action).WaitAsync(TimeSpan.Zero, CancellationToken.None);
        new Task<int>(func).WaitAsync(CancellationToken.None);
        new Task<int>(func).WaitAsync(TimeSpan.Zero);
        new Task<int>(func).WaitAsync(TimeSpan.Zero, CancellationToken.None);
    }

#if FeatureMemory

    public void ListAddRangeReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
    }

    public void EnumerateLinesReadOnlySpan()
    {
        foreach (var line in "ab".AsSpan().EnumerateLines())
        {
        }
    }

    public void ListInsertRangeReadOnlySpan()
    {
        var list = new List<char>
        {
            'a'
        };
        list.InsertRange(1, "bc".AsSpan());
    }

    public void ListCopyToSpan()
    {
        var list = new List<char>
        {
            'a'
        };
        var array = new char[1];
        list.CopyTo(array.AsSpan());
    }

    async Task StreamReaderReadAsync()
    {
        var result = new char[5];
        var memory = new Memory<char>(result);
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadAsync(memory);
    }

    void RegexIsMatch()
    {
        var regex = new Regex("result");
        regex.IsMatch("value".AsSpan());
    }

    async Task StreamReadAsync()
    {
        var input = new byte[]
        {
            1,
            2
        };
        using var stream = new MemoryStream(input);
        var result = new byte[2];
        var memory = new Memory<byte>(result);
        var read = await stream.ReadAsync(memory);
    }

    void StringBuilderCopyTo()
    {
        var builder = new StringBuilder("value");
        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
    }

    void SpanTrimEnd()
    {
        var span = new Span<char>(new char[1]);
        span.TrimEnd();
        span.TrimStart();
    }

    void SpanSequenceEqual()
    {
        var result = "value".AsSpan().SequenceEqual("value");
    }

    void SpanStartsWith()
    {
        var startsWith = "value".AsSpan().StartsWith("value");
        startsWith = "value".AsSpan().StartsWith("value", StringComparison.Ordinal);
    }

    void SpanEndsWith()
    {
        var result = "value".AsSpan().EndsWith("value");
        result = "value".AsSpan().EndsWith("value", StringComparison.Ordinal);
    }

    void SpanStringBuilderAppend()
    {
        var builder = new StringBuilder();
        builder.Append("value".AsSpan());
    }

    void StringEqualsSpan()
    {
        var builder = new StringBuilder("value");
        var equals = builder.Equals("value".AsSpan());
    }

#endif

    void IsGenericMethodParameter()
    {
        var result = typeof(string).IsGenericMethodParameter();
    }

    void HashSetTryGetValue()
    {
        var set = new HashSet<string>
        {
            "value"
        };
        var found = set.TryGetValue("value", out var result);
    }
#if NET6_0_OR_GREATER
    void StringBuilderReplaceReadOnlySpan()
    {
        var builder = new StringBuilder();

        var result = builder.Replace("a".AsSpan(), "a".AsSpan());
        result = builder.Replace("a".AsSpan(), "a".AsSpan(), 1, 1);
    }
#endif

    void HasSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = info.HasSameMetadataDefinitionAs(info);
    }

    void GetMemberWithSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = typeof(string).GetMemberWithSameMetadataDefinitionAs(info);
    }

    public void SortedList()
    {
        var list = new SortedList<int, char>();
        var key = list.GetKeyAtIndex(0);
        var value = list.GetValueAtIndex(0);
    }
}