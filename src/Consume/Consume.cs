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

namespace Consumes;

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
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MemoryStream = System.IO.MemoryStream;
// ReSharper disable MethodHasAsyncOverload
// ReSharper disable RedundantCast
// ReSharper disable NotAccessedField.Local
#pragma warning disable CS0219 // Variable is assigned but its value is never used
#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable CS0169 // Field is never used

#pragma warning disable CS4014
#pragma warning disable CA1416

[SuppressMessage("Performance", "CA1823:Avoid unused private fields")]
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
        type = typeof(EnumerationOptions);
        type = typeof(MatchType);
        type = typeof(MatchCasing);

        var (key, value) = KeyValuePair.Create("a", "b");

#if NET6_0_OR_GREATER
        var (date, time, offset) = DateTimeOffset.Now;
        var (dateOnly, timeOnly) = DateTime.Now;
        var (hour, minute) = new TimeOnly();
#endif

        var (year, month, day) = DateTime.Now;
#if FeatureValueTask
        var completed = ValueTask.CompletedTask;
#endif
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
        guid = Guid.CreateVersion7(timestamp: DateTimeOffset.UtcNow);
        var result = Guid.TryParse(s: "", provider: null, result: out guid);
#if FeatureMemory
        ReadOnlySpan<byte> byteSpan = default;
        result = Guid.TryParse(utf8Text: byteSpan, result: out guid);
        guid = Guid.Parse(utf8Text: byteSpan);

        Span<char> charSpan = default;
        result = Guid.TryParse(input: charSpan, result: out guid);
        result = Guid.TryParse(s: charSpan, provider: null, result: out guid);
        result = Guid.TryParseExact(input: charSpan, format: charSpan, result: out guid);

#endif
    }

    void SHA256Usage()
    {
        SHA256.HashData(source: (byte[]) null!);
        SHA256.HashData(source: (Stream) null!);
#if FeatureValueTask
        SHA256.HashDataAsync(source: null!, cancellationToken: CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA256.HashData(source: (Stream) null!, destination: span);
        SHA256.HashData(source: readOnlySpan);
        SHA256.HashData(source: readOnlySpan, destination: span);
        SHA256.TryHashData(source: readOnlySpan, destination: span, bytesWritten: out _);
#if FeatureValueTask
        SHA256.HashDataAsync(source: null!, destination: memory);
        SHA256.HashDataAsync(source: null!, destination: memory, cancellationToken: CancellationToken.None);
#endif
#endif
    }

    void SHA512Usage()
    {
        SHA512.HashData(source: (byte[]) null!);
        SHA512.HashData(source: (Stream) null!);
#if FeatureValueTask
        SHA512.HashDataAsync(source: null!, cancellationToken: CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA512.HashData((Stream) null!, destination: span);
        SHA512.HashData(source: readOnlySpan);
        SHA512.HashData(source: readOnlySpan, destination: span);
        SHA512.TryHashData(source: readOnlySpan, destination: span, bytesWritten: out _);
#if FeatureValueTask
        SHA512.HashDataAsync(source: null!, destination: memory);
        SHA512.HashDataAsync(source: null!, destination: memory, cancellationToken: CancellationToken.None);
#endif
#endif
    }

    void SHA1Usage()
    {
        SHA1.HashData(source: (byte[]) null!);
        SHA1.HashData(source: (Stream) null!);
#if FeatureValueTask
        SHA1.HashDataAsync(source: null!, cancellationToken: CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA1.HashData(source: (Stream) null!, destination: span);
        SHA1.HashData(source: readOnlySpan);
        SHA1.HashData(source: readOnlySpan, destination: span);
        SHA1.TryHashData(source: readOnlySpan, destination: span, bytesWritten: out _);
#if FeatureValueTask
        SHA1.HashDataAsync(source: null!, destination: memory);
        SHA1.HashDataAsync(source: null!, destination: memory, cancellationToken: CancellationToken.None);
#endif
#endif
    }

    void SHA384Usage()
    {
        SHA384.HashData(source: (byte[]) null!);
        SHA384.HashData(source: (Stream) null!);
#if FeatureValueTask
        SHA384.HashDataAsync(source: null!, cancellationToken: CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        SHA384.HashData(source: (Stream) null!, destination: span);
        SHA384.HashData(source: readOnlySpan);
        SHA384.HashData(source: readOnlySpan, destination: span);
        SHA384.TryHashData(source: readOnlySpan, destination: span, bytesWritten: out _);
#if FeatureValueTask
        SHA384.HashDataAsync(source: null!, destination: memory);
        SHA384.HashDataAsync(source: null!, destination: memory, cancellationToken: CancellationToken.None);
#endif
#endif
    }

    void MD5Usage()
    {
        MD5.HashData(source: (byte[]) null!);
        MD5.HashData(source: (Stream) null!);
#if FeatureValueTask
        MD5.HashDataAsync(source: null!, cancellationToken: CancellationToken.None);
#endif
#if FeatureMemory
        Span<byte> span = default;
        ReadOnlySpan<byte> readOnlySpan = default;
        Memory<byte> memory = default;

        MD5.HashData(source: (Stream) null!, destination: span);
        MD5.HashData(source: readOnlySpan);
        MD5.HashData(source: readOnlySpan, destination: span);
        MD5.TryHashData(source: readOnlySpan, destination: span, bytesWritten: out _);
#if FeatureValueTask
        MD5.HashDataAsync(source: null!, destination: memory);
        MD5.HashDataAsync(source: null!, destination: memory, cancellationToken: CancellationToken.None);
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
        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>) values).GetEnumerator();

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

#if FeatureMemory
    void BitConverter_Methods()
    {
        var floatFromInt = BitConverter.Int32BitsToSingle(0x3F800000);
        var intFromFloat = BitConverter.SingleToInt32Bits(1.0f);
        var floatFromUInt = BitConverter.UInt32BitsToSingle(0x3F800000u);
        var uintFromFloat = BitConverter.SingleToUInt32Bits(1.0f);
        var doubleFromULong = BitConverter.UInt64BitsToDouble(0x3FF0000000000000ul);
        var ulongFromDouble = BitConverter.DoubleToUInt64Bits(1.0);
    }
#endif

    void Byte_Methods()
    {
        byte.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        byte.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        byte.TryParse(s: ['1'], result: out _);
        byte.TryParse(s: ['1'], provider: null, result: out _);
        byte.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        byte.TryParse(utf8Text: "1"u8, result: out _);
        byte.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
#endif
    }

    void CancellationToken_Methods()
    {
        var source = new CancellationTokenSource();
        var token = source.Token;
        token.UnsafeRegister(_ =>
        {
        }, null);
        token.UnsafeRegister((_, _) =>
        {
        }, null);
    }

    async Task CancellationTokenSource_Methods()
    {
        var source = new CancellationTokenSource();
        await source.CancelAsync();
    }


    class WithMethods
    {
        public void NonGenericMethod(string value) { }
        public void GenericMethod<T>(string value) { }
        public void GenericMethod<T1, T2>(string value, int count) { }
    }

    void Type_GetMethod()
    {
        var type = typeof(WithMethods);

        // Non-generic method
        var nonGeneric = type.GetMethod("NonGenericMethod", 0, BindingFlags.Public | BindingFlags.Instance, [typeof(string)]);

        // Generic method with 1 type parameter
        var generic1 = type.GetMethod("GenericMethod", 1, BindingFlags.Public | BindingFlags.Instance, [typeof(string)]);

        // Generic method with 2 type parameters
        var generic2 = type.GetMethod("GenericMethod", 2, BindingFlags.Public | BindingFlags.Instance, [typeof(string), typeof(int)]);
    }

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
        var dictionary = new Dictionary<string, string?> {{"key", "value"}};
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

    #if PolyArgumentExceptions

    #region ArgumentExceptionUsage

    void ArgumentExceptionExample(Order order, Customer customer, string customerId, string email, decimal discountPercentage, int quantity)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(discountPercentage, 100m);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        this.order = order;
        this.customer = customer;
        this.customerId = customerId;
        this.email = email;
        this.discountPercentage = discountPercentage;
        this.quantity = quantity;
    }

    void ObjectDisposedExceptionExample(bool isDisposed)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        ObjectDisposedException.ThrowIf(isDisposed, typeof(Consume));
    }

    #endregion

    #endif

    #if PolyEnsure

    #region EnsureUsage

    void EnsureExample(Order order, Customer customer, string customerId, string email, decimal discountPercentage, int quantity)
    {
        this.order = Ensure.NotNull(order);
        this.customer = Ensure.NotNull(customer);
        this.customerId = Ensure.NotNullOrWhiteSpace(customerId);
        this.email = Ensure.NotNullOrWhiteSpace(email);
        this.discountPercentage = Ensure.NotGreaterThan(discountPercentage, 100m);
        this.quantity = Ensure.NotNegativeOrZero(quantity);
    }

    #endregion

    #endif

    decimal discountPercentage;
    int quantity;
    string email = null!;
    string customerId = null!;
    Customer customer = null!;
    Order order = null!;

    class Customer;

    class Order;

    void Double_Methods()
    {
        double.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        double.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        double.TryParse(s: ['1'], result: out _);
        double.TryParse(s: ['1'], provider: null, result: out _);
        double.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        double.TryParse(utf8Text: "1"u8, result: out _);
        double.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
#endif
    }

    void DictionaryEntry_Methods()
    {
        var entry = new DictionaryEntry("key", "value");
        var (key, value) = entry;
    }

    void Enum_Methods()
    {
        var values = Enum.GetValuesAsUnderlyingType(typeof(DayOfWeek));
        values = Enum.GetValuesAsUnderlyingType<DayOfWeek>();
    }

    void EnumerationOptions_Methods()
    {
        var options = new EnumerationOptions
        {
            RecurseSubdirectories = true,
            BufferSize = 4096,
            AttributesToSkip = FileAttributes.ReadOnly,
            MatchType = MatchType.Win32,
            MatchCasing = MatchCasing.CaseInsensitive,
            ReturnSpecialDirectories = true
        };
        var recurse = options.RecurseSubdirectories;
        var buffer = options.BufferSize;
        var attrs = options.AttributesToSkip;
        var matchType = options.MatchType;
        var matchCasing = options.MatchCasing;
        var special = options.ReturnSpecialDirectories;
    }

#if NETFRAMEWORK || NETSTANDARD && !NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0
    void Directory_Methods_WithEnumerationOptions()
    {
        var options = new EnumerationOptions();
        var files = Polyfill.EnumerateFiles(".", "*", options);
        var dirs = Polyfill.EnumerateDirectories(".", "*", options);
        var entries = Polyfill.EnumerateFileSystemEntries(".", "*", options);
        var filesArray = Polyfill.GetFiles(".", "*", options);
        var dirsArray = Polyfill.GetDirectories(".", "*", options);
        var entriesArray = Polyfill.GetFileSystemEntries(".", "*", options);
    }

    void DirectoryInfo_Methods_WithEnumerationOptions()
    {
        var dirInfo = new DirectoryInfo(".");
        var options = new EnumerationOptions();
        var files = dirInfo.EnumerateFiles("*", options);
        var dirs = dirInfo.EnumerateDirectories("*", options);
        var entries = dirInfo.EnumerateFileSystemInfos("*", options);
        var filesArray = dirInfo.GetFiles("*", options);
        var dirsArray = dirInfo.GetDirectories("*", options);
        var entriesArray = dirInfo.GetFileSystemInfos("*", options);
    }
#else
    void Directory_Methods_WithEnumerationOptions()
    {
        var options = new EnumerationOptions();
        var files = Directory.EnumerateFiles(".", "*", options);
        var dirs = Directory.EnumerateDirectories(".", "*", options);
        var entries = Directory.EnumerateFileSystemEntries(".", "*", options);
        var filesArray = Directory.GetFiles(".", "*", options);
        var dirsArray = Directory.GetDirectories(".", "*", options);
        var entriesArray = Directory.GetFileSystemEntries(".", "*", options);
    }

    void DirectoryInfo_Methods_WithEnumerationOptions()
    {
        var dirInfo = new DirectoryInfo(".");
        var options = new EnumerationOptions();
        var files = dirInfo.EnumerateFiles("*", options);
        var dirs = dirInfo.EnumerateDirectories("*", options);
        var entries = dirInfo.EnumerateFileSystemInfos("*", options);
        var filesArray = dirInfo.GetFiles("*", options);
        var dirsArray = dirInfo.GetDirectories("*", options);
        var entriesArray = dirInfo.GetFileSystemInfos("*", options);
    }
#endif

#if !NET11_0_OR_GREATER
    void Console_Methods()
    {
        using var stdin = Console.OpenStandardInputHandle();
        using var stdout = Console.OpenStandardOutputHandle();
        using var stderr = Console.OpenStandardErrorHandle();
    }
#endif

    void File_Methods()
    {
        const string TestFilePath = "testfile.txt";

        var sourceContent = "Test content";
        File.WriteAllText(TestFilePath, sourceContent);

        var fileMode = File.GetUnixFileMode(TestFilePath);

        // Use the | bitwise OR operator to combine multiple file modes
        File.SetUnixFileMode(TestFilePath, UnixFileMode.OtherRead | UnixFileMode.OtherWrite);

#if !NET11_0_OR_GREATER
        using var nullHandle = File.OpenNullHandle();
#endif

        FileSystemInfo hardLink = File.CreateHardLink("hardlink.txt", TestFilePath);
        var fileInfo = new FileInfo("hardlink2.txt");
        fileInfo.CreateAsHardLink(TestFilePath);
    }

    void HashSet_Methods()
    {
        var set = new HashSet<string> {"value"};
        var found = set.TryGetValue("value", out var result);
        set.EnsureCapacity(1);
        set.TrimExcess(1);
        set.TrimExcess();
    }

#if FeatureHttp
    void HttpClient_Methods(HttpClient target)
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

#if FeatureValueTask
    async Task TcpClient_Methods()
    {
        using var client = new TcpClient();
        await client.ConnectAsync(IPAddress.Loopback, 12345, CancellationToken.None);
        await client.ConnectAsync([IPAddress.Loopback], 12345, CancellationToken.None);
        await client.ConnectAsync("localhost", 12345, CancellationToken.None);
        await client.ConnectAsync(new(IPAddress.Loopback, 12345), CancellationToken.None);
    }

    async Task UdpClient_Methods()
    {
        using var client = new UdpClient(0);
        await client.ReceiveAsync(CancellationToken.None);
#if FeatureMemory
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);
        using var connectedClient = new UdpClient("localhost", 12345);
        await connectedClient.SendAsync(data, CancellationToken.None);
        await client.SendAsync(data, new(IPAddress.Loopback, 12345), CancellationToken.None);
        await client.SendAsync(data, "localhost", 12345, CancellationToken.None);
#endif
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
        var unionBy = enumerable.UnionBy(["c"], _ => _, comparer: default);
    }

    void IList_Methods()
    {
        IList<string> ilist = new List<string>();
        ilist.AsReadOnly();
    }

#if FeatureMemory && FeatureUnsafe
    void Interlocked_Methods()
    {
        var intValue = 0xFF;
        Interlocked.And(ref intValue, 0x0F);
        Interlocked.Or(ref intValue, 0xF0);

        var longValue = 0xFFL;
        Interlocked.And(ref longValue, 0x0FL);
        Interlocked.Or(ref longValue, 0xF0L);
    }
#endif

    void Int_Methods()
    {
        int.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        int.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        int.TryParse(s: ['1'], result: out _);
        int.TryParse(s: ['1'], provider: null, result: out _);
        int.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        int.TryParse(utf8Text: "1"u8, result: out _);
        int.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
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
        long.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        long.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        long.TryParse(s: ['1'], result: out _);
        long.TryParse(s: ['1'], provider: null, result: out _);
        long.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        long.TryParse(utf8Text: "1"u8, result: out _);
        long.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
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
        var isWindows11 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000);

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

    void ProcessStartInfo_Methods()
    {
        var info = new ProcessStartInfo("cmd.exe");
        var argumentList = info.ArgumentList;
        argumentList.Add("/c");
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
        // ReSharper disable once RedundantExplicitParamsArrayCreation
        split = readOnlySpan.SplitAny(['a']);
        split = readOnlySpan.SplitAny("a".AsSpan());
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
        sbyte.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        sbyte.TryParse("1"u8, provider: null, result: out _);
        sbyte.TryParse(s: ['1'], result: out _);
        sbyte.TryParse(s: ['1'], provider: null, result: out _);
        sbyte.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        sbyte.TryParse(utf8Text: "1"u8, result: out _);
        sbyte.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
#endif
    }

    void Short_Methods()
    {
        short.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        short.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        short.TryParse(s: ['1'], result: out _);
        short.TryParse(s: ['1'], provider: null, result: out _);
        short.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        short.TryParse(utf8Text: "1"u8, result: out _);
        short.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
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

        var array = Enumerable.Range(0, 5).ToArray();

#if !NET8_0_OR_GREATER
        Polyfill.Shuffle(array);
#else
        Random.Shared.Shuffle(array);
#endif

        var numbers = new Span<int>(array);
        numbers.Sort();
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
        var input = new byte[] {1, 2};
        using var stream = new MemoryStream(input);
        var result = new byte[2];
#if FeatureMemory && FeatureValueTask
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
#if FeatureMemory && FeatureValueTask
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

#if FeatureAsyncInterfaces
    async Task Task_WhenEach_Methods()
    {
        var tasks = new[] { Task.CompletedTask, Task.CompletedTask };
        await foreach (var task in Task.WhenEach(tasks))
        {
        }

        var tasksGeneric = new[] { Task.FromResult(1), Task.FromResult(2) };
        await foreach (var task in Task.WhenEach(tasksGeneric))
        {
        }

#if !NET9_0_OR_GREATER
        // CancellationToken overloads only exist in polyfill, not in native .NET 9+ implementation
        await foreach (var task in Task.WhenEach(tasks, CancellationToken.None))
        {
        }
        await foreach (var task in Task.WhenEach(tasksGeneric, CancellationToken.None))
        {
        }
#endif
    }
#endif

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

    void TimeSpan_Methods()
    {
        var timeSpan = TimeSpan.FromMilliseconds(1000L);
    }

    async Task TextWriter_Methods()
    {
        TextWriter target = new StringWriter();
        target.Write(new StringBuilder());
        await target.FlushAsync(CancellationToken.None);
        await target.WriteAsync(new StringBuilder());
        await target.WriteAsync("a", CancellationToken.None);
        await target.WriteLineAsync(CancellationToken.None);
        await target.WriteLineAsync("a", CancellationToken.None);
#if FeatureMemory && FeatureValueTask
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
        result = typeof(string).IsGenericMethodParameter;
        var member = typeof(string).GetMemberWithSameMetadataDefinitionAs(info);
    }

    void UInt_Methods()
    {
        uint.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        uint.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        uint.TryParse(s: ['1'], result: out _);
        uint.TryParse(s: ['1'], provider: null, result: out _);
        uint.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        uint.TryParse(utf8Text: "1"u8, result: out _);
        uint.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
#endif
    }

    void ULong_Methods()
    {
        ulong.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        ulong.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        ulong.TryParse(s: ['1'], result: out _);
        ulong.TryParse(s: ['1'], provider: null, result: out _);
        ulong.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, provider: null, result: out _);
        ulong.TryParse(utf8Text: "1"u8, result: out _);
        ulong.TryParse(s: ['1'], style: NumberStyles.Integer, provider: null, result: out _);
#endif
    }

    void UShort_Methods()
    {
        ushort.TryParse(s: "1", provider: null, result: out _);
#if FeatureMemory
        ushort.TryParse(utf8Text: "1"u8, provider: null, result: out _);
        ushort.TryParse(s: ['1'], result: out _);
        ushort.TryParse(s: ['1'], provider: null, result: out _);
        ushort.TryParse(utf8Text: "1"u8, style: NumberStyles.Integer, null, result: out _);
        ushort.TryParse(utf8Text: "1"u8, result: out _);
        ushort.TryParse(s: ['1'], style: NumberStyles.Integer, null, result: out _);
#endif
    }

    async Task XDocument_Methods(XDocument document)
    {
        document.SaveAsync(new XmlTextWriter(null!), CancellationToken.None);
        document.SaveAsync(new StringWriter(), SaveOptions.None, CancellationToken.None);
        document.SaveAsync(new MemoryStream(), SaveOptions.None, CancellationToken.None);
        document.WriteToAsync(XmlWriter.Create(TextWriter.Null), CancellationToken.None);
        await XDocument.LoadAsync((Stream) null!, LoadOptions.None, CancellationToken.None);
        await XDocument.LoadAsync((TextReader) null!, LoadOptions.None, CancellationToken.None);
        await XDocument.LoadAsync((XmlReader) null!, LoadOptions.None, CancellationToken.None);
    }

    async Task XElement_Methods(XElement element)
    {
        element.SaveAsync(new XmlTextWriter(null!), CancellationToken.None);
        element.SaveAsync(new StringWriter(), SaveOptions.None, CancellationToken.None);
        element.SaveAsync(new MemoryStream(), SaveOptions.None, CancellationToken.None);
        element.WriteToAsync(XmlWriter.Create(TextWriter.Null), CancellationToken.None);
        await XElement.LoadAsync((Stream) null!, LoadOptions.None, CancellationToken.None);
        await XElement.LoadAsync((TextReader) null!, LoadOptions.None, CancellationToken.None);
        await XElement.LoadAsync((XmlReader) null!, LoadOptions.None, CancellationToken.None);
    }

    async Task XNode_Methods(XNode node, XmlReader xmlReader)
    {
        node.WriteToAsync(XmlWriter.Create(TextWriter.Null), CancellationToken.None);
        await XNode.ReadFromAsync(xmlReader, CancellationToken.None);
    }

    void XComment_Methods(XComment comment)
    {
        comment.WriteToAsync(XmlWriter.Create(TextWriter.Null), CancellationToken.None);
    }

    void XCData_Methods(XCData cdata)
    {
        cdata.WriteToAsync(XmlWriter.Create(TextWriter.Null), CancellationToken.None);
    }

#if FeatureCompression
    void ZipArchiveEntry_Methods(ZipArchive zip, ZipArchiveEntry entry)
    {
        entry.Open(FileAccess.Read);
#if FeatureValueTask
        entry.OpenAsync(FileAccess.Read);
#endif
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

    async Task ZipFile_Methods()
    {
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination");
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", CancellationToken.None);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", overwriteFiles: true);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", overwriteFiles: true, CancellationToken.None);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", Encoding.UTF8);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", Encoding.UTF8, CancellationToken.None);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", Encoding.UTF8, overwriteFiles: true);
        await ZipFile.ExtractToDirectoryAsync("archive.zip", "destination", Encoding.UTF8, overwriteFiles: true, CancellationToken.None);

        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip");
        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip", CancellationToken.None);
        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip", CompressionLevel.Optimal, includeBaseDirectory: false);
        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip", CompressionLevel.Optimal, includeBaseDirectory: false, CancellationToken.None);
        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip", CompressionLevel.Optimal, includeBaseDirectory: false, Encoding.UTF8);
        await ZipFile.CreateFromDirectoryAsync("source", "archive.zip", CompressionLevel.Optimal, includeBaseDirectory: false, Encoding.UTF8, CancellationToken.None);

        await ZipFile.OpenAsync("archive.zip", ZipArchiveMode.Read);
        await ZipFile.OpenAsync("archive.zip", ZipArchiveMode.Read, CancellationToken.None);
        await ZipFile.OpenAsync("archive.zip", ZipArchiveMode.Read, Encoding.UTF8);
        await ZipFile.OpenAsync("archive.zip", ZipArchiveMode.Read, Encoding.UTF8, CancellationToken.None);

        await ZipFile.OpenReadAsync("archive.zip");
        await ZipFile.OpenReadAsync("archive.zip", CancellationToken.None);
    }
#endif
}
