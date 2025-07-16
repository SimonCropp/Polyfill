// ReSharper disable RedundantUsingDirective
// ReSharper disable RedundantAssignment
// ReSharper disable UnusedVariable
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AllUnderscoreLocalParameterName
// ReSharper disable NotAccessedVariable
// ReSharper disable UnnecessaryWhitespace
// ReSharper disable InconsistentNaming
// ReSharper disable CollectionNeverUpdated.Local

#nullable enable

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MemoryStream = System.IO.MemoryStream;
// ReSharper disable MethodHasAsyncOverload

#pragma warning disable CS4014
#pragma warning disable CA1416

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
        type = typeof(ParamCollectionAttribute);
        type = typeof(CallerArgumentExpressionAttribute);
        type = typeof(IsExternalInit);
        type = typeof(KeyValuePair);
        type = typeof(FeatureGuardAttribute);
        type = typeof(FeatureSwitchDefinitionAttribute);
        type = typeof(ModuleInitializerAttribute);
        type = typeof(RequiredMemberAttribute);
        type = typeof(SetsRequiredMembersAttribute);
        type = typeof(SkipLocalsInitAttribute);
        //TODO:
        // type = typeof(TupleElementNamesAttribute);
        type = typeof(DebuggerNonUserCodeAttribute);
        type = typeof(UnscopedRefAttribute);
#if !NoStringInterpolation
        type = typeof(InterpolatedStringHandlerArgumentAttribute);
        type = typeof(InterpolatedStringHandlerAttribute);
#endif
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
#if !NET6_0 && !NET5_0
        type = typeof(ObsoletedOSPlatformAttribute);
        type = typeof(SupportedOSPlatformGuardAttribute);
        type = typeof(UnsupportedOSPlatformGuardAttribute);
#endif
        type = typeof(OSPlatformAttribute);
        type = typeof(SupportedOSPlatformAttribute);
        type = typeof(TargetPlatformAttribute);
        type = typeof(UnsupportedOSPlatformAttribute);
        type = typeof(StackTraceHiddenAttribute);
        type = typeof(UnmanagedCallersOnlyAttribute);
        type = typeof(SuppressGCTransitionAttribute);
        type = typeof(DisableRuntimeMarshallingAttribute);
        type = typeof(RequiresUnreferencedCodeAttribute);
        type = typeof(UnreachableException);
        type = typeof(DebuggerDisableUserUnhandledExceptionsAttribute);

        var (key, value) = KeyValuePair.Create("a", "b");

#if NET6_0_OR_GREATER
        var (date, time, offset) = DateTimeOffset.Now;
        var (dateOnly, timeOnly) = DateTime.Now;
        var (hour, minute) = new TimeOnly();
#endif

        var (year, month, day) = DateTime.Now;
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

    public static void ParamCollection(params List<string> collection)
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

    void GuidUsage()
    {
        var guid = Guid.CreateVersion7();
        guid = Guid.CreateVersion7(DateTimeOffset.UtcNow);
        var result = Guid.TryParse("", null, out guid);
#if FeatureMemory
        ReadOnlySpan<byte> byteSpan = default;
        result = Guid.TryParse(byteSpan, out guid);
        guid = Guid.Parse(byteSpan);

        Span<char> charSpan = default;
        result = Guid.TryParse(charSpan, out guid);
        result = Guid.TryParse(charSpan, null, out guid);
        result = Guid.TryParseExact(charSpan, charSpan, out guid);

#endif
    }

    void SHA256Usage()
    {
        SHA256.HashData((byte[])null!);
        SHA256.HashData((Stream)null!);
#if FeatureValueTask
        SHA256.HashDataAsync(null!, CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA256.HashData((Stream)null!, span);
        SHA256.HashData(readOnlySpan);
        SHA256.HashData(readOnlySpan, span);
        SHA256.TryHashData(readOnlySpan, span, out _);
#if FeatureValueTask
        SHA256.HashDataAsync(null!, memory);
        SHA256.HashDataAsync(null!, memory, CancellationToken.None);
#endif
#endif
    }

    void SHA512Usage()
    {
        SHA512.HashData((byte[])null!);
        SHA512.HashData((Stream)null!);
#if FeatureValueTask
        SHA512.HashDataAsync(null!, CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA512.HashData((Stream)null!, span);
        SHA512.HashData(readOnlySpan);
        SHA512.HashData(readOnlySpan, span);
        SHA512.TryHashData(readOnlySpan, span, out _);
#if FeatureValueTask
        SHA512.HashDataAsync(null!, memory);
        SHA512.HashDataAsync(null!, memory, CancellationToken.None);
#endif
#endif
    }

#if FeatureMemory

    void CollectionBuilderAttribute()
    {
        MyCollection myCollection = [1, 2, 3, 4, 5];
    }

    [CollectionBuilder(typeof(MyCollection), nameof(Create))]
    class MyCollection(ReadOnlySpan<int> initValues)
    {
        int[] values = initValues.ToArray();
        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)values).GetEnumerator();

        public static MyCollection Create(ReadOnlySpan<int> values) => new(values);
    }

#endif

#if FeatureValueTuple

    void Ranges()
    {
        var range = "value"[1..];
        var index = "value"[^2];
        //Array not supported due to no RuntimeHelpers.GetSubArray
        // var subArray = new[]
        // {
        //     "value1",
        //     "value2"
        // }[..1];
    }

#endif

    void Byte_Methods()
    {
        byte.TryParse("1", null, out _);
#if FeatureMemory
        byte.TryParse("1"u8, null, out _);
        byte.TryParse(['1'], out _);
        byte.TryParse(['1'], null, out _);
        byte.TryParse("1"u8, NumberStyles.Integer, null, out _);
        byte.TryParse("1"u8, out _);
        byte.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void CancellationToken_Methods()
    {
        var source = new CancellationTokenSource();
        var token = source.Token;
        token.UnsafeRegister(_ => { }, null);
        token.UnsafeRegister((_, _) => { }, null);
    }

    async Task CancellationTokenSource_Methods()
    {
        var source = new CancellationTokenSource();
        await source.CancelAsync();
    }


#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETCOREAPP2_0
    class WithGenericMethod
    {
        public void GenericMethod<T>(string value)
        {
        }
    }

    void Type_GetMethod()
    {
        var type = typeof(WithGenericMethod);
        type.GetMethod("GenericMethod", 1, BindingFlags.Public, [typeof(string)]);
    }
#endif

    void ConcurrentDictionary_Methods()
    {
        var dict = new ConcurrentDictionary<string, int>();
        var value = dict.GetOrAdd("Hello", static (_, arg) => arg.Length, "World");
    }

#if FeatureMemory
    void String_Normalize()
    {
        var span = "Café".AsSpan();
        var normalizedLength = span.GetNormalizedLength(NormalizationForm.FormC);
        var isNormalized = span.IsNormalized(NormalizationForm.FormC);
        Span<char> destination = new char[10];
        var tryNormalize = span.TryNormalize(destination, out var chars, NormalizationForm.FormC);
    }
#endif

#if NET9_0_OR_GREATER
    void OrderedDictionary_Methods()
    {
        var dict = new OrderedDictionary<string, int>();
        var result = dict.TryAdd("Hello", 1, out var index1);
        result = dict.TryGetValue("Hello", out var value, out var index2);
    }
#endif

    void ConcurrentBag_Methods()
    {
        var bag = new ConcurrentBag<string>();
        bag.Clear();
    }

    void ConcurrentQueue_Methods()
    {
        var queue = new ConcurrentQueue<string>();
        queue.Clear();
    }

    void Dictionary_Methods()
    {
        var dictionary = new Dictionary<string, string?> { { "key", "value" } };
        dictionary.GetValueOrDefault("key");
        dictionary.GetValueOrDefault("key", "default");
        dictionary.TryAdd("key", "value");
        dictionary.Remove("key");
        dictionary.EnsureCapacity(1);
        dictionary.TrimExcess(1);
        dictionary.TrimExcess();

        IDictionary<string, string?> iDictionary = dictionary;
        iDictionary.TryAdd("key", "value");
        iDictionary.TryAdd("key", "value");
        iDictionary.Remove("key");

    }

    void Lock_Methods()
    {
        var locker = new Lock();
        var held = locker.IsHeldByCurrentThread;
        locker.Enter();
    }

    void Double_Methods()
    {
        double.TryParse("1", null, out _);
#if FeatureMemory
        double.TryParse("1"u8, null, out _);
        double.TryParse(['1'], out _);
        double.TryParse(['1'], null, out _);
        double.TryParse("1"u8, NumberStyles.Integer, null, out _);
        double.TryParse("1"u8, out _);
        double.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void DictionaryEntry_Methods()
    {
        var entry = new DictionaryEntry("key", "value");
        var (key, value) = entry;
    }

    void File_Methods()
    {
        const string TestFilePath = "testfile.txt";

        var sourceContent = "Test content";
        File.WriteAllText(TestFilePath, sourceContent);

        var fileMode = File.GetUnixFileMode(TestFilePath);

        // Use the | bitwise OR operator to combine multiple file modes
        File.SetUnixFileMode(TestFilePath, UnixFileMode.OtherRead | UnixFileMode.OtherWrite);
    }

    void HashSet_Methods()
    {
        var set = new HashSet<string> { "value" };
        var found = set.TryGetValue("value", out var result);
        set.EnsureCapacity(1);
        set.TrimExcess(1);
        set.TrimExcess();
    }

#if FeatureHttp
    void HttpClient_Methods( HttpClient target)
    {
        target.GetStreamAsync("", CancellationToken.None);
        target.GetStreamAsync(new Uri(""), CancellationToken.None);
        target.GetByteArrayAsync("", CancellationToken.None);
        target.GetByteArrayAsync(new Uri(""), CancellationToken.None);
        target.GetStringAsync("", CancellationToken.None);
        target.GetStringAsync(new Uri(""), CancellationToken.None);
    }

    void HttpContent_Methods(ByteArrayContent target)
    {
        target.ReadAsStreamAsync(CancellationToken.None);
        target.ReadAsByteArrayAsync(CancellationToken.None);
        target.ReadAsStringAsync(CancellationToken.None);
    }
#endif

    void IDictionary_Methods()
    {
        IDictionary<int, int> idictionary = new Dictionary<int, int>();
        idictionary.AsReadOnly();
    }

    void IEnumerable_Methods()
    {
        IEnumerable<string> enumerable = new List<string>
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
        int[] numbers = [1, 2, 3, 4];
        string[] words = ["one", "two", "three"];
        var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);
#if FeatureValueTuple
        var elementAt = enumerable.ElementAt(new Index(1));

        var take = enumerable.Take(1..3);
#endif
        var takeLast = enumerable.TakeLast(3);
    }

    void IList_Methods()
    {
        IList<string> ilist = new List<string>();
        ilist.AsReadOnly();
    }

    void Int_Methods()
    {
        int.TryParse("1", null, out _);
#if FeatureMemory
        int.TryParse("1"u8, null, out _);
        int.TryParse(['1'], out _);
        int.TryParse(['1'], null, out _);
        int.TryParse("1"u8, NumberStyles.Integer, null, out _);
        int.TryParse("1"u8, out _);
        int.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void List_Methods()
    {
        var list = new List<char>();
        list.EnsureCapacity(1);
        list.TrimExcess();
#if FeatureMemory
        var array = new char[1];
        list.AddRange("ab".AsSpan());
        list.CopyTo(array.AsSpan());
        list.InsertRange(1, "bc".AsSpan());
#endif
    }

    void Queue_Methods()
    {
        var queue = new Queue<char>();
        queue.EnsureCapacity(1);
        queue.TrimExcess(1);
        queue.TrimExcess();
    }

    void Long_Methods()
    {
        long.TryParse("1", null, out _);
#if FeatureMemory
        long.TryParse("1"u8, null, out _);
        long.TryParse(['1'], out _);
        long.TryParse(['1'], null, out _);
        long.TryParse("1"u8, NumberStyles.Integer, null, out _);
        long.TryParse("1"u8, out _);
        long.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void MemberInfo_Methods(MemberInfo info)
    {
        var result = info.HasSameMetadataDefinitionAs(info);
    }

    #if FeatureRuntimeInformation
    void OperatingSystem_Methods()
    {
        var isOSPlatform = OperatingSystem.IsOSPlatform("windows");
        var isOSPlatformWindows10 = OperatingSystem.IsOSPlatformVersionAtLeast("windows", 10, 0, 10240);

        var isWindows = OperatingSystem.IsWindows();
        var isWindows11 = OperatingSystem.IsWindowsVersionAtLeast(10,0,22000);

        var isMacOS = OperatingSystem.IsMacOS();
        var isMacOsSonoma = OperatingSystem.IsMacOSVersionAtLeast(14);
        var isMacCatalyst = OperatingSystem.IsMacCatalyst();
        var isMacCatalyst17 = OperatingSystem.IsMacCatalystVersionAtLeast(17);

        var isLinux = OperatingSystem.IsLinux();

        var isFreeBSD = OperatingSystem.IsFreeBSD();
        var isFreeBSD14 = OperatingSystem.IsFreeBSDVersionAtLeast(14, 0);

        var isIOS = OperatingSystem.IsIOS();
        var isIOS18 = OperatingSystem.IsIOSVersionAtLeast(18);

        var isAndroid = OperatingSystem.IsAndroid();
        var isAndroid13 = OperatingSystem.IsAndroidVersionAtLeast(13);

        var isTvOS = OperatingSystem.IsTvOS();
        var isTvOS17 = OperatingSystem.IsTvOSVersionAtLeast(17);

        var isWatchOS = OperatingSystem.IsWatchOS();
        var isWatchOS11 = OperatingSystem.IsWatchOSVersionAtLeast(11);

        var isWasi = OperatingSystem.IsWasi();
        var isBrowser = OperatingSystem.IsBrowser();
    }
#endif

    async Task Process_Methods()
    {
        var process = new Process();
        await process.WaitForExitAsync();
        process.Kill(true);
    }

    void Random_Methods()
    {
        var random = new Random();
#if FeatureMemory
        Span<byte> bufferSpan = new byte[10];
        random.NextBytes(bufferSpan);
        random.Shuffle(bufferSpan);

        ReadOnlySpan<int> choicesSpan = [1, 2, 3, 4, 5];
        Span<int> destination = new int[10];
        random.GetItems(choicesSpan, destination);

        var resultFromSpan = random.GetItems(choicesSpan, 10);
#endif

        int[] choicesArray = [1, 2, 3, 4, 5];
        var resultFromArray = random.GetItems(choicesArray, 10);

        var bufferArray = new byte[10];
        random.Shuffle(bufferArray);
    }

#if FeatureMemory

    void ReadOnlySpan_Methods()
    {
        var readOnlySpan = "value".AsSpan();
        foreach (var line in readOnlySpan.EnumerateLines())
        {
        }

        var result = readOnlySpan.EndsWith("value");
        result = readOnlySpan.EndsWith("value", StringComparison.Ordinal);
        result = readOnlySpan.SequenceEqual("value");
        result = readOnlySpan.StartsWith("value");
        result = readOnlySpan.StartsWith('a');
        result = readOnlySpan.EndsWith('a');
        result = readOnlySpan.StartsWith("value", StringComparison.Ordinal);
#if FeatureValueTuple
        var split = readOnlySpan.Split('a');
        split = readOnlySpan.Split("a".AsSpan());
#if LangVersion13
        // ReSharper disable once RedundantExplicitParamsArrayCreation
        split = readOnlySpan.SplitAny(['a']);
        split = readOnlySpan.SplitAny("a".AsSpan());
#endif
#endif
    }

#endif

#if FeatureMemory

    void Regex_Methods()
    {
        var regex = new Regex("result");
        regex.IsMatch("value".AsSpan());
    }

#endif

#if NET9_0_OR_GREATER
    void Set_Methods()
    {
        var set = new HashSet<string>
        {
            "value"
        };
        set.AsReadOnly();
    }
#endif

    void SByte_Methods()
    {
        sbyte.TryParse("1", null, out _);
#if FeatureMemory
        sbyte.TryParse("1"u8, null, out _);
        sbyte.TryParse(['1'], out _);
        sbyte.TryParse(['1'], null, out _);
        sbyte.TryParse("1"u8, NumberStyles.Integer, null, out _);
        sbyte.TryParse("1"u8, out _);
        sbyte.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void Short_Methods()
    {
        short.TryParse("1", null, out _);
#if FeatureMemory
        short.TryParse("1"u8, null, out _);
        short.TryParse(['1'], out _);
        short.TryParse(['1'], null, out _);
        short.TryParse("1"u8, NumberStyles.Integer, null, out _);
        short.TryParse("1"u8, out _);
        short.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void SortedList_Methods()
    {
        var list = new SortedList<int, char>();
        var key = list.GetKeyAtIndex(0);
        var value = list.GetValueAtIndex(0);
    }

#if FeatureMemory

    void Span_Methods()
    {
        var span = new Span<char>(new char[1]);
        _ = span.TrimEnd();
        _ = span.TrimStart();
    }

#endif

    void Stack_Methods()
    {
        var stack = new Stack<char>();
        stack.TryPeek(out var ch1);
        stack.TryPop(out var ch2);
        stack.EnsureCapacity(1);
        stack.TrimExcess(1);
        stack.TrimExcess();
    }

    async Task Stream_Methods()
    {
        var input = new byte[] { 1, 2 };
        using var stream = new MemoryStream(input);
        var result = new byte[2];
#if FeatureMemory
        var memory = new Memory<byte>(result);
        var read = await stream.ReadAsync(memory);
#endif
        await stream.CopyToAsync(stream);
        #if FeatureValueTask
        await stream.DisposeAsync();
        #endif
    }

    async Task StreamReader_Methods()
    {
        var result = new char[5];
        var reader = new StreamReader(new MemoryStream());
#if FeatureMemory
        var memory = new Memory<char>(result);
        var count = await reader.ReadAsync(memory);
#endif
        var line = await reader.ReadLineAsync(CancellationToken.None);
        var text = await reader.ReadToEndAsync(CancellationToken.None);
    }

    void String_Methods()
    {
        var contains = "a b".Contains(' ');
        var endsWith = "value".EndsWith('a');

        var splitChar = "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries);
        splitChar = "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var startsWith = "value".StartsWith('a');

        var splitString = "a b".Split(" ", StringSplitOptions.RemoveEmptyEntries);
        splitString = "a b".Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
    }

    #if !NoStringInterpolation && FeatureMemory
    void DefaultInterpolatedStringHandler_Methods()
    {
        var handler = new DefaultInterpolatedStringHandler();
        handler.AppendLiteral("value");
        handler.Clear();
    }
    #endif

    void StringBuilder_Methods()
    {
        var builder = new StringBuilder("value");
#if FeatureMemory
        builder.Append("suffix".AsSpan());
        var targetSpan = new Span<char>(new char[1]);
        builder.CopyTo(0, targetSpan, 1);
        builder.Insert(1, targetSpan);
        var equals = builder.Equals("value".AsSpan());

#if NET6_0_OR_GREATER
        var result = builder.Replace("a".AsSpan(), "a".AsSpan());
        result = builder.Replace("a".AsSpan(), "a".AsSpan(), 1, 1);
#endif
#endif
    }

    void Task_Methods()
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

    void TaskCompletionSource_NonGeneric_Methods()
    {
        var tcs = new TaskCompletionSource();
    }

    void TaskCompletionSource_Generic_Methods()
    {
        var completionSource = new TaskCompletionSource<int>();
        var tokenSource = new CancellationTokenSource();
        completionSource.SetCanceled(tokenSource.Token);
    }

    async Task TextWriter_Methods()
    {
        TextWriter target = new StringWriter();
        target.Write(new StringBuilder());
        await target.FlushAsync(CancellationToken.None);
        await target.WriteAsync(new StringBuilder());
#if FeatureMemory
        target.WriteLine("a".AsSpan());
        target.Write("a".AsSpan());
        var memory = "a".AsMemory();
        await target.WriteLineAsync(memory);
        await target.WriteAsync(memory);
#endif
    }

    void Type_Methods(MemberInfo info)
    {
        var result = typeof(List<string>).IsAssignableTo(typeof(string));
        result = typeof(List<string>).IsAssignableTo(null);
        result = typeof(string).IsGenericMethodParameter();
        var member = typeof(string).GetMemberWithSameMetadataDefinitionAs(info);
    }

    void UInt_Methods()
    {
        uint.TryParse("1", null, out _);
#if FeatureMemory
        uint.TryParse("1"u8, null, out _);
        uint.TryParse(['1'], out _);
        uint.TryParse(['1'], null, out _);
        uint.TryParse("1"u8, NumberStyles.Integer, null, out _);
        uint.TryParse("1"u8, out _);
        uint.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void ULong_Methods()
    {
        ulong.TryParse("1", null, out _);
#if FeatureMemory
        ulong.TryParse("1"u8, null, out _);
        ulong.TryParse(['1'], out _);
        ulong.TryParse(['1'], null, out _);
        ulong.TryParse("1"u8, NumberStyles.Integer, null, out _);
        ulong.TryParse("1"u8, out _);
        ulong.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    void UShort_Methods()
    {
        ushort.TryParse("1", null, out _);
#if FeatureMemory
        ushort.TryParse("1"u8, null, out _);
        ushort.TryParse(['1'], out _);
        ushort.TryParse(['1'], null, out _);
        ushort.TryParse("1"u8, NumberStyles.Integer, null, out _);
        ushort.TryParse("1"u8, out _);
        ushort.TryParse(['1'], NumberStyles.Integer, null, out _);
#endif
    }

    async Task XDocument_Methods(XDocument document)
    {
        document.SaveAsync(new XmlTextWriter(null!), CancellationToken.None);
        document.SaveAsync(new StringWriter(), SaveOptions.None, CancellationToken.None);
        document.SaveAsync(new MemoryStream(), SaveOptions.None, CancellationToken.None);
        await XDocument.LoadAsync((Stream)null!, LoadOptions.None, CancellationToken.None);
        await XDocument.LoadAsync((TextReader)null!, LoadOptions.None, CancellationToken.None);
        await XDocument.LoadAsync((XmlReader)null!, LoadOptions.None, CancellationToken.None);
    }

    async Task XElement_Methods(XElement element)
    {
        element.SaveAsync(new XmlTextWriter(null!), CancellationToken.None);
        element.SaveAsync(new StringWriter(), SaveOptions.None, CancellationToken.None);
        element.SaveAsync(new MemoryStream(), SaveOptions.None, CancellationToken.None);
        await XElement.LoadAsync((Stream)null!, LoadOptions.None, CancellationToken.None);
        await XElement.LoadAsync((TextReader)null!, LoadOptions.None, CancellationToken.None);
        await XElement.LoadAsync((XmlReader)null!, LoadOptions.None, CancellationToken.None);
    }

#if FeatureCompression
    void ZipArchiveEntry_Methods(ZipArchive zip, ZipArchiveEntry entry)
    {
        entry.OpenAsync();
        zip.CreateEntryFromFile("file.txt", "entry.txt");
        zip.CreateEntryFromFile("file.txt", "entry.txt", CompressionLevel.Optimal);
        zip.CreateEntryFromFileAsync("file.txt", "entry.txt");
        zip.CreateEntryFromFileAsync("file.txt", "entry.txt", CompressionLevel.Optimal);
        zip.ExtractToDirectory("destinationPath", true);
        zip.ExtractToDirectoryAsync("destinationPath", true);
        entry.ExtractToFile("destinationPath", true);
        entry.ExtractToFileAsync("destinationPath", true);
    }
#endif
}