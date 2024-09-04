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
using System.Xml;
using System.Xml.Linq;
using MemoryStream = System.IO.MemoryStream;

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
#if FeatureMemory
        type = typeof(CollectionBuilderAttribute);
#endif
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
        //Array not supported due to no RuntimeHelpers.GetSubArray
        // var subArray = new[]
        // {
        //     "value1",
        //     "value2"
        // }[..1];
#endif

        var startsWith = "value".StartsWith('a');
        var endsWith = "value".EndsWith('a');

        IList<string> ilist = new List<string>();
        ilist.AsReadOnly();

        IDictionary<int, int> idictionary = new Dictionary<int, int>();
        idictionary.AsReadOnly();

        typeof(List<string>).IsAssignableTo(typeof(string));
        typeof(List<string>).IsAssignableTo(null);

        var enumerable = (IEnumerable<string>)new List<string>
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

        dictionary.TryAdd("key", "value");

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

    #region Compiler Features
    void DeconstructTupleInForeach(IEnumerable<KeyValuePair<string, string>> variables)
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
#pragma warning restore ExperimentalMethod

    [RequiresPreviewFeatures("This method uses a preview feature.")]
    void UsePreviewFeature()
    {
    }

    [OverloadResolutionPriority(1)]
    void Method(int x)
    {
    }

    [OverloadResolutionPriority(2)]
    void Method(string x)
    {
    }

    [OverloadResolutionPriority(3)]
    void Method(object x)
    {
    }

#if FeatureMemory
    void CollectionBuilderAttribute()
    {
        MyCollection myCollection = [1, 2, 3, 4, 5];
    }

    [CollectionBuilder(typeof(MyCollection), nameof(Create))]
    class MyCollection(ReadOnlySpan<int> initValues)
    {
        readonly int[] values = initValues.ToArray();
        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)values).GetEnumerator();

        public static MyCollection Create(ReadOnlySpan<int> values) => new(values);
    }
#endif

    #endregion

    void CancellationToken_UnsafeRegister()
    {
        var source = new CancellationTokenSource();
        var token = source.Token;
        token.UnsafeRegister(state =>
        {
        }, null);
        token.UnsafeRegister((state, token) =>
        {
        }, null);
    }

    async Task CancellationTokenSource_CancelAsync()
    {
        var source = new CancellationTokenSource();
        await source.CancelAsync();
    }

    void HashSet_TryGetValue()
    {
        var set = new HashSet<string>
        {
            "value"
        };
        var found = set.TryGetValue("value", out var result);
    }

#if FetureHttp
    static void HttpClient()
    {
        new HttpClient().GetStreamAsync("", CancellationToken.None);
        new HttpClient().GetStreamAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetByteArrayAsync("", CancellationToken.None);
        new HttpClient().GetByteArrayAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetStringAsync("", CancellationToken.None);
        new HttpClient().GetStringAsync(new Uri(""), CancellationToken.None);
    }

    static void HttpContent()
    {
        new ByteArrayContent([]).ReadAsStreamAsync(CancellationToken.None);
        new ByteArrayContent([]).ReadAsByteArrayAsync(CancellationToken.None);
        new ByteArrayContent([]).ReadAsStringAsync(CancellationToken.None);
    }
#endif

#if FeatureMemory
    void List_AddRange_ReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
    }
#endif

#if FeatureMemory
    void List_CopyToSpan()
    {
        var list = new List<char>
        {
            'a'
        };
        var array = new char[1];
        list.CopyTo(array.AsSpan());
    }
#endif

#if FeatureMemory
    void List_InsertRange_ReadOnlySpan()
    {
        var list = new List<char>
        {
            'a'
        };
        list.InsertRange(1, "bc".AsSpan());
    }
#endif

    void MemberInfo_HasSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = info.HasSameMetadataDefinitionAs(info);
    }

    async Task Process_WaitForExitAsync()
    {
        var process = new Process();
        await process.WaitForExitAsync();
    }

#if FeatureMemory
    void Random_NextBytes_Span()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
    }
#endif

    void Random_Shuffle_Array()
    {
        var random = new Random();
        var buffer = new byte[10];
        random.Shuffle(buffer);
    }

#if FeatureMemory
    void Random_Shuffle_Span()
    {
        var random = new Random();
        Span<byte> span = new byte[10];
        random.Shuffle(span);
    }
#endif

#if FeatureMemory
    void ReadOnlySpan_EnumerateLines()
    {
        foreach (var line in "ab".AsSpan().EnumerateLines())
        {
        }
    }
#endif

#if FeatureMemory
    void Regex_IsMatch()
    {
        var regex = new Regex("result");
        regex.IsMatch("value".AsSpan());
    }
#endif

    void SortedList()
    {
        var list = new SortedList<int, char>();
        var key = list.GetKeyAtIndex(0);
        var value = list.GetValueAtIndex(0);
    }

#if FeatureMemory
    void Span_EndsWith()
    {
        var result = "value".AsSpan().EndsWith("value");
        result = "value".AsSpan().EndsWith("value", StringComparison.Ordinal);
    }

    void Span_SequenceEqual()
    {
        var result = "value".AsSpan().SequenceEqual("value");
    }
    void Span_StartsWith()
    {
        var startsWith = "value".AsSpan().StartsWith("value");
        startsWith = "value".AsSpan().StartsWith("value", StringComparison.Ordinal);
    }

    void Span_TrimEnd()
    {
        var span = new Span<char>(new char[1]);
        span.TrimEnd();
    }

    void Span_TrimStart()
    {
        var span = new Span<char>(new char[1]);
        span.TrimStart();
    }

    async Task Stream_ReadAsync()
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

    async Task StreamReader_ReadAsync()
    {
        var result = new char[5];
        var memory = new Memory<char>(result);
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadAsync(memory);
    }

    async Task StreamReader_ReadLineAsync()
    {
        TextReader reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadLineAsync(CancellationToken.None);
    }

    async Task StreamReader_ReadToEndAsync()
    {
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadToEndAsync(CancellationToken.None);
    }

    void StringBuilder_Append_Span()
    {
        var builder = new StringBuilder();
        builder.Append("value".AsSpan());
    }

    void StringBuilder_CopyTo()
    {
        var builder = new StringBuilder("value");
        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
    }

    void StringBuilder_Equals_Span()
    {
        var builder = new StringBuilder("value");
        var equals = builder.Equals("value".AsSpan());
    }
#endif

#if NET6_0_OR_GREATER
    void StringBuilder_Replace_ReadOnlySpan()
    {
        var builder = new StringBuilder();

        var result = builder.Replace("a".AsSpan(), "a".AsSpan());
        result = builder.Replace("a".AsSpan(), "a".AsSpan(), 1, 1);
    }
#endif

    void Task_WaitAsync()
    {
        var action = () =>
        {
        };
        var func = () => 0;
        new Task(action).WaitAsync(CancellationToken.None);
        new Task(action).WaitAsync(TimeSpan.Zero);
        new Task(action).WaitAsync(TimeSpan.Zero, CancellationToken.None);
        new Task<int>(func).WaitAsync(CancellationToken.None);
        new Task<int>(func).WaitAsync(TimeSpan.Zero);
        new Task<int>(func).WaitAsync(TimeSpan.Zero, CancellationToken.None);
    }

    void TaskCompletionSource_NonGeneric()
    {
        var tcs = new TaskCompletionSource();
    }

    void TaskCompletionSource_SetCanceled_WithCancellationToken()
    {
        var completionSource = new TaskCompletionSource<int>();
        var tokenSource = new CancellationTokenSource();
        completionSource.SetCanceled(tokenSource.Token);
    }

#if FeatureMemory
    async Task TextWriter()
    {
        TextWriter target = new StringWriter();
        target.Write(new StringBuilder());
        target.WriteAsync(new StringBuilder());
        target.WriteLine("a".AsSpan());
        await target.WriteLineAsync("a".AsMemory());
        target.Write("a".AsSpan());
        await target.WriteAsync("a".AsMemory());
    }
#endif
    void Type_GetMemberWithSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = typeof(string).GetMemberWithSameMetadataDefinitionAs(info);
    }

    void Type_IsGenericMethodParameter()
    {
        var result = typeof(string).IsGenericMethodParameter();
    }

    void XDocument_SaveAsync()
    {
        var document = new XDocument();
        document.SaveAsync(new XmlTextWriter(null!), CancellationToken.None);
        document.SaveAsync(new StringWriter(), SaveOptions.None, CancellationToken.None);
        document.SaveAsync(new MemoryStream(), SaveOptions.None, CancellationToken.None);
    }
}