// ReSharper disable RedundantUsingDirective
#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
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

#if (NET46X && VALUETUPLEREFERENCED) || NET47X || NET48X || NETSTANDARD2_0 || NETCOREAPP2X
        var range = "value"[1..];
        var index = "value"[^2];
#endif

        var startsWith = "value".StartsWith('a');
        var endsWith = "value".EndsWith('a');

        var enumerable = (IEnumerable<string>) new List<string>
        {
            "a",
            "b"
        };
        var append = enumerable.Append("c");
        var maxBy = enumerable.MaxBy(_ => _);
        var chunk = enumerable.Chunk(3);
        var minBy = enumerable.MinBy(_ => _);
        var skipLast = enumerable.SkipLast(1);

        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        dictionary.GetValueOrDefault("key");
        dictionary.GetValueOrDefault("key", "default");

        var split = "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries);
        split = "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var contains = "a b".Contains(' ');

        // Test to make sure there are no clashes in the Polyfill code with classes that
        // might be defined in user code. See comments in Debug.cs for more details.
        Debug.Log("Test log to make sure this is working");
    }

#if HTTPREFERENCED

    static void Http()
    {
        new HttpClient().GetStreamAsync("", CancellationToken.None);
        new HttpClient().GetStreamAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetByteArrayAsync("", CancellationToken.None);
        new HttpClient().GetByteArrayAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetStringAsync("", CancellationToken.None);
        new HttpClient().GetStringAsync(new Uri(""), CancellationToken.None);

        new ByteArrayContent(Array.Empty<byte>()).ReadAsStreamAsync(CancellationToken.None);
        new ByteArrayContent(Array.Empty<byte>()).ReadAsByteArrayAsync(CancellationToken.None);
        new ByteArrayContent(Array.Empty<byte>()).ReadAsStringAsync(CancellationToken.None);
    }

#endif

    void KeyValuePairDeconstruct(IEnumerable<KeyValuePair<string, string>> variables)
    {
        foreach (var (name, value) in variables)
        {
        }
    }

#if (NET46X && VALUETUPLEREFERENCED) || NET47X || NET48X || NETSTANDARD2_0 || NETCOREAPP2X

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

#if MEMORYREFERENCED

    async Task StreamReaderReadAsync()
    {
        var result = new char[5];
        var memory = new Memory<char>(result);
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadAsync(memory);
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

    void HasSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = info.HasSameMetadataDefinitionAs(info);
    }

    void GetMemberWithSameMetadataDefinitionAs(MemberInfo info)
    {
        var result = typeof(string).GetMemberWithSameMetadataDefinitionAs(info);
    }
}