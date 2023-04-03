// ReSharper disable RedundantUsingDirective
#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        type = typeof(TupleElementNamesAttribute);
        type = typeof(DebuggerNonUserCodeAttribute);
        type = typeof(ValueTuple<>);
        type = typeof(ValueTuple);
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
        type = typeof(AsyncMethodBuilderAttribute);
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

        var range = "value"[1..];
        var index = "value"[^2];

        var startsWith = "value".StartsWith('a');
        var endsWith = "value".EndsWith('a');

        var enumerable = (IEnumerable<string>)new List<string> {"a", "b"};
        var append = enumerable.Append("c");
        var skipLast = enumerable.SkipLast(1);

        var dictionary = new Dictionary<string,string?>
        {
            {"key", "value"}
        };

        dictionary.GetValueOrDefault("key");
        dictionary.GetValueOrDefault("key", "default");

        var split = "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries);
        split = "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var contains = "a b".Contains(' ');
    }

    static void Http()
    {
        new HttpClient().GetStreamAsync("", CancellationToken.None);
        new HttpClient().GetStreamAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetByteArrayAsync("", CancellationToken.None);
        new HttpClient().GetByteArrayAsync(new Uri(""), CancellationToken.None);
        new HttpClient().GetStringAsync("", CancellationToken.None);
        new HttpClient().GetStringAsync(new Uri(""), CancellationToken.None);

        new ByteArrayContent(new byte[] { }).ReadAsStreamAsync(CancellationToken.None);
        new ByteArrayContent(new byte[] { }).ReadAsByteArrayAsync(CancellationToken.None);
        new ByteArrayContent(new byte[] { }).ReadAsStringAsync(CancellationToken.None);
    }

    void KeyValuePairDeconstruct(IEnumerable<KeyValuePair<string, string>> variables)
    {
        foreach (var (name, value) in variables)
        {
        }
    }

    static (string value1, bool value2) NamedTupleMethod() =>
        new("value", false);

    async Task StreamReaderReadAsync()
    {
        var result = new char[5];
        var memory = new Memory<char>(result);
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadAsync(memory);
    }

    async Task StreamReaderReadToEndAsync()
    {
        var reader = new StreamReader(new MemoryStream());
        var read = await reader.ReadToEndAsync(CancellationToken.None);
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

    void SpanContains()
    {
        var contains = "value".AsSpan().Contains('e');
        contains = new Span<char>().Contains('e');
    }

    void SpanSequenceEqual()
    {
        var sequenceEqual = "value".AsSpan().SequenceEqual("value");
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
}