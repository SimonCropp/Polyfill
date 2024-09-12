# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`, `net9.0`


**API count: 338**<!-- singleLineInclude: apiCount. path: /apiCount.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**


## TargetFrameworks

Some polyfills are implemented in a way that will not have the equivalent performance to the actual implementations.

For example the polyfill for `StringBuilder.Append(ReadOnlySpan<char>)` on netcore2 is:

```
public StringBuilder Append(ReadOnlySpan<char> value)
    => target.Append(value.ToString());
```

Which will result in a string allocation.

As Polyfill is implemented as a source only nuget, the implementation for each polyfill is compiled into the IL of the resulting assembly. As a side-effect that implementation will continue to be used even if that assembly is executed in a runtime that has a more efficient implementation available.

As a result, in the context of a project producing nuget package, that project should target all frameworks from the lowest TargetFramework up to and including the current framework. This way the most performant implementation will be used for each runtime. Take the following examples:

 * If a nuget's minimum target is net6, then the resulting TargetFrameworks should also include net7.0 and net8.0
 * If a nuget's minimum target is net471, then the resulting TargetFrameworks should also include net472 and net48"


## Nuget

https://nuget.org/packages/Polyfill/


### SDK / LangVersion

This project uses features from the current stable SDK and C# language. As such consuming projects should target those:


### LangVersion

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <LangVersion>latest</LangVersion>
```


### global.json

```json
{
  "sdk": {
    "version": "8.0.301",
    "rollForward": "latestFeature"
  }
}
```


## Consuming and type visibility

The default type visibility for all polyfills is `internal`. This means it can be consumed in multiple projects and types will not conflict.


### Consuming in an app

If Polyfill is being consumed in a solution that produce an app, then it is recommended to use the Polyfill nuget only in the root "app project" and enable `PolyPublic`.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <PolyPublic>true</PolyPublic>
```

Then all consuming projects, like tests, will not need to use the Polyfill nuget.


### Consuming in a library

If Polyfill is being consumed in a solution that produce a library (and usually a nuget), then the Polyfill nuget can be added to all projects.

If, however, `InternalsVisibleTo` is being used to expose APIs (for example to test projects), then the Polyfill nuget should be added only to the root library project.


## Included polyfills


### ModuleInitializerAttribute

 * [ModuleInitializerAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.moduleinitializerattribute)

Reference: [Module Initializers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/module-initializers)

<!-- snippet: ModuleInitializerAttribute -->
<a id='snippet-ModuleInitializerAttribute'></a>
```cs
static bool InitCalled;

[Test]
public void ModuleInitTest() =>
    Assert.True(InitCalled);

[ModuleInitializer]
public static void ModuleInit() =>
    InitCalled = true;
```
<sup><a href='/src/Tests/ModuleInitSample.cs#L4-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-ModuleInitializerAttribute' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### IsExternalInit

Reference: [init (C# Reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init)

<!-- snippet: IsExternalInit -->
<a id='snippet-IsExternalInit'></a>
```cs
class InitSample
{
    public int Member { get; init; }
}
```
<sup><a href='/src/Tests/InitSample.cs#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-IsExternalInit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Nullable attributes

  * [AllowNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.allownullattribute)
  * [DisallowNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.disallownullattribute)
  * [DoesNotReturnAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.doesnotreturnattribute)
  * [DoesNotReturnIfAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.doesnotreturnifattribute)
  * [MaybeNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.maybenullattribute)
  * [MaybeNullWhenAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.maybenullwhenattribute)
  * [MemberNotNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.membernotnullattribute)
  * [MemberNotNullWhenAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.membernotnullwhenattribute)
  * [NotNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullattribute)
  * [NotNullIfNotNullAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullifnotnullattribute)
  * [NotNullWhenAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullwhenattribute)

Reference: [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)


### Required attributes

 * [RequiredMemberAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.requiredmemberattribute)
 * [SetsRequiredMembersAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.setsrequiredmembersattribute)

Reference: [C# required modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required)

<!-- snippet: Required -->
<a id='snippet-Required'></a>
```cs
public class Person
{
    public Person()
    {
    }

    [SetsRequiredMembers]
    public Person(string name) =>
        Name = name;

    public required string Name { get; init; }
}
```
<sup><a href='/src/Tests/SetsRequiredMembersUsage.cs#L3-L18' title='Snippet source file'>snippet source</a> | <a href='#snippet-Required' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### CompilerFeatureRequiredAttribute

 * [CompilerFeatureRequiredAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilerfeaturerequiredattribute)

> Indicates that compiler support for a particular feature is required for the location where this attribute is applied.


### CollectionBuilderAttribute

 * [CollectionBuilderAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.collectionbuilderattribute)

> Can be used to make types compatible with collection expressions


### ConstantExpectedAttribute

 * [ConstantExpectedAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.constantexpectedattribute)

> Indicates that the specified method parameter expects a constant.


### SkipLocalsInit

 * [SkipLocalsInitAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.skiplocalsinitattribute)

Reference: [SkipLocalsInitAttribute](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/general#skiplocalsinit-attribute)

> the SkipLocalsInit attribute prevents the compiler from setting the .locals init flag when emitting to metadata. The SkipLocalsInit attribute is a single-use attribute and can be applied to a method, a property, a class, a struct, an interface, or a module, but not to an assembly. SkipLocalsInit is an alias for SkipLocalsInitAttribute.

<!-- snippet: SkipLocalsInit -->
<a id='snippet-SkipLocalsInit'></a>
```cs
class SkipLocalsInitSample
{
    [SkipLocalsInit]
    static void ReadUninitializedMemory()
    {
        Span<int> numbers = stackalloc int[120];
        for (var i = 0; i < 120; i++)
        {
            Console.WriteLine(numbers[i]);
        }
    }
}
```
<sup><a href='/src/UnsafeTests/SkipLocalsInitExample.cs#L1-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-SkipLocalsInit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Index and Range

Reference: [Indices and ranges](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes)

If consuming in a project that targets net461 or net462, a reference to System.ValueTuple is required. See [References: System.ValueTuple](#systemvaluetuple).

<!-- snippet: IndexRange -->
<a id='snippet-IndexRange'></a>
```cs
[TestFixture]
class IndexRangeSample
{
    [Test]
    public void Range()
    {
        var substring = "value"[2..];
        Assert.AreEqual("lue", substring);
    }

    [Test]
    public void Index()
    {
        var ch = "value"[^2];
        Assert.AreEqual('u', ch);
    }

    [Test]
    public void ArrayIndex()
    {
        var array = new[]
        {
            "value1",
            "value2"
        };

        var value = array[^2];

        Assert.AreEqual("value1", value);
    }
}
```
<sup><a href='/src/Tests/IndexRangeSample.cs#L1-L35' title='Snippet source file'>snippet source</a> | <a href='#snippet-IndexRange' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### OverloadResolutionPriority

 * [Release Notes](https://github.com/dotnet/core/blob/main/release-notes/9.0/preview/preview7/csharp.md#prioritize-better-overloads-with-overloadresolutionpriority-attribute)

> C# introduces a new attribute, System.Runtime.CompilerServices.OverloadResolutionPriority, that can be used by API authors to adjust the relative priority of overloads within a single type as a means of steering API consumers to use specific APIs, even if those APIs would normally be considered ambiguous or otherwise not be chosen by C#'s overload resolution rules. This helps framework and library authors guide API usage as they APIs as they develop new and better patterns.
>
> The OverloadResolutionPriorityAttribute can be used in conjunction with the ObsoleteAttribute. A library author may mark properties, methods, types and other programming elements as obsolete, while leaving them in place for backwards compatibility. Using programming elements marked with the ObsoleteAttribute will result in compiler warnings or errors. However, the type or member is still visible to overload resolution and may be selected over a better overload or cause an ambiguity failure. The OverloadResolutionPriorityAttribute lets library authors fix these problems by lowering the priority of obsolete members when there are better alternatives.

 * [API on learn](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.overloadresolutionpriorityattribute.-ctor)
 * [API Proposal](https://github.com/dotnet/runtime/issues/102173)


#### Usage

<!-- snippet: OverloadResolutionPriority -->
<a id='snippet-OverloadResolutionPriority'></a>
```cs
[TestFixture]
public class OverloadResolutionPriorityAttributeTests
{
    [Test]
    public void Run()
    {
        int[] arr = [1, 2, 3];
        //Prints "Span" because resolution priority is higher
        Method(arr);
    }

    [OverloadResolutionPriority(2)]
    static void Method(ReadOnlySpan<int> list) =>
        Console.WriteLine("Span");

    [OverloadResolutionPriority(1)]
    static void Method(int[] list) =>
        Console.WriteLine("Array");
}
```
<sup><a href='/src/Tests/OverloadResolutionPriorityAttributeTests.cs#L4-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-OverloadResolutionPriority' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### UnscopedRefAttribute

 * [UnscopedRefAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.unscopedrefattribute)

Reference: [Low Level Struct Improvements](https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/low-level-struct-improvements.md)

<!-- snippet: UnscopedRefUsage.cs -->
<a id='snippet-UnscopedRefUsage.cs'></a>
```cs
#if !NET7_0_OR_GREATER

using System.Diagnostics.CodeAnalysis;

struct UnscopedRefUsage
{
    int field;

    [UnscopedRef] ref int Prop1 => ref field;
}

#endif
```
<sup><a href='/src/Consume/UnscopedRefUsage.cs#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-UnscopedRefUsage.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### RequiresPreviewFeaturesAttribute

 * [RequiresPreviewFeatures](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.requirespreviewfeaturesattribute)
 * [Design](https://github.com/dotnet/designs/blob/main/accepted/2021/preview-features/preview-features.md)
 * [API Proposal](https://github.com/dapr/dotnet-sdk/issues/1219)


### CallerArgumentExpressionAttribute

 * [CallerArgumentExpressionAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callerargumentexpressionattribute)

Reference: [CallerArgumentExpression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/caller-argument-expression)

<!-- snippet: CallerArgumentExpression -->
<a id='snippet-CallerArgumentExpression'></a>
```cs
static class FileUtil
{
    public static void FileExists(string path, [CallerArgumentExpression("path")] string argumentName = "")
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"File not found. Path: {path}", argumentName);
        }
    }
}

static class FileUtilUsage
{
    public static string[] Method(string path)
    {
        FileUtil.FileExists(path);
        return File.ReadAllLines(path);
    }
}
```
<sup><a href='/src/Tests/CallerArgumentExpressionUsage.cs#L1-L22' title='Snippet source file'>snippet source</a> | <a href='#snippet-CallerArgumentExpression' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### InterpolatedStringHandler

 * [AppendInterpolatedStringHandler](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendinterpolatedstringhandler)
 * [DefaultInterpolatedStringHandler](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler)
 * [InterpolatedStringHandlerAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerattribute)
 * [InterpolatedStringHandlerArgumentAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerargumentattribute)
 * [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable)

References: [String Interpolation in C# 10 and .NET 6](https://devblogs.microsoft.com/dotnet/string-interpolation-in-c-10-and-net-6/), [Write a custom string interpolation handler](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/interpolated-string-handler)


### StringSyntaxAttribute

 * [StringSyntaxAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.stringsyntaxattribute)

Reference: [.NET 7 - The StringSyntaxAttribute](https://bartwullems.blogspot.com/2022/12/net-7-stringsyntaxattribute.html)


### Trimming annotation attributes

 * [DynamicallyAccessedMembersAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.dynamicallyaccessedmembersattribute)
 * [DynamicDependencyAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.dynamicdependencyattribute)
 * [RequiresUnreferencedCodeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.requiresunreferencedcodeattribute)
 * [RequiresDynamicCodeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.requiresdynamiccodeattribute)
 * [UnconditionalSuppressMessageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.unconditionalsuppressmessageattribute)

Reference: [Prepare .NET libraries for trimming](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/prepare-libraries-for-trimming)


### Platform compatibility

 * [ObsoletedOSPlatformAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.obsoletedosplatformattribute)
 * [SupportedOSPlatformAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.supportedosplatformattribute)
 * [SupportedOSPlatformGuardAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.supportedosplatformguardattribute)
 * [TargetPlatformAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.targetplatformattribute)
 * [UnsupportedOSPlatformAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.unsupportedosplatformattribute)
 * [UnsupportedOSPlatformGuardAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.unsupportedosplatformguardattribute)

Reference: [Platform compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer)


### StackTraceHiddenAttribute

 * [StackTraceHiddenAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stacktracehiddenattribute)

Reference: [C# – Hide a method from the stack trace](https://makolyte.com/csharp-exclude-exception-throw-helper-methods-from-the-stack-trace/)


### UnmanagedCallersOnly

 * [UnmanagedCallersOnlyAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.unmanagedcallersonlyattribute)

Reference: [Improvements in native code interop in .NET 5.0](https://devblogs.microsoft.com/dotnet/improvements-in-native-code-interop-in-net-5-0/)


### SuppressGCTransition

 * [SuppressGCTransitionAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.suppressgctransitionattribute)


### DisableRuntimeMarshalling

 * [DisableRuntimeMarshallingAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.disableruntimemarshallingattribute)


## Extensions

The class `Polyfill` includes the following extension methods:

> [!IMPORTANT]
> The methods using `AppendInterpolatedStringHandler` parameter are not extensions because the compiler prefers to use the overload with `string` parameter instead.


### Extension methods<!-- include: api_list.include.md -->

#### Boolean

 * `Boolean TryFormat(Span<Char>, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat)


#### Byte

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat)


#### CancellationToken

 * `CancellationTokenRegistration Register(Action<Object,CancellationToken>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<Object>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<Object,CancellationToken>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync)


#### Collections.Concurrent.ConcurrentDictionary<TKey,TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(TKey, Func<TKey,TArg,TValue>, TArg)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### DateOnly

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat)


#### DateTime

 * `DateTime AddMicroseconds(Double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds)
 * `Int32 Microsecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond)
 * `Int32 Nanosecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond)
 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat)


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(Double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds)
 * `Int32 Microsecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond)
 * `Int32 Nanosecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond)
 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat)


#### Decimal

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat)


#### Dictionary<TKey,TValue>

 * `Boolean Remove<TKey, TValue>(TKey, TValue&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove)
 * `Boolean TryAdd<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.tryadd)


#### Double

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat)


#### Guid

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))


#### HashSet<T>

 * `Boolean TryGetValue<T>(T, T&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue)


#### HttpClient

 * `Task<Byte[]> GetByteArrayAsync(String, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<Byte[]> GetByteArrayAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(String, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<String> GetStringAsync(String, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<String> GetStringAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<Byte[]> ReadAsByteArrayAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<Stream> ReadAsStreamAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<String> ReadAsStringAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IDictionary<TKey,TValue>

 * `Collections.ObjectModel.ReadOnlyDictionary<TKey,TValue> AsReadOnly<TKey, TValue>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))


#### IEnumerable<TFirst>

 * `IEnumerable<ValueTuple<TFirst,TSecond,TThird>> Zip<TFirst, TSecond, TThird>(IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<ValueTuple<TFirst,TSecond>> Zip<TFirst, TSecond>(IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey,TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource,TKey>, TAccumulate, Func<TAccumulate,TSource,TAccumulate>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey,TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource,TKey>, Func<TKey,TAccumulate>, Func<TAccumulate,TSource,TAccumulate>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> Append<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append)
 * `IEnumerable<TSource[]> Chunk<TSource>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk)
 * `IEnumerable<KeyValuePair<TKey,Int32>> CountBy<TSource, TKey>(Func<TSource,TKey>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby)
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource,TKey>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource ElementAt<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource ElementAtOrDefault<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))
 * `IEnumerable<TSource> Except<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource, IEqualityComparer<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEqualityComparer<TSource>, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource,TKey>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource FirstOrDefault<TSource>(Func<TSource,Boolean>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<ValueTuple<Int32,TSource>> Index<TSource>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))
 * `TSource LastOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource LastOrDefault<TSource>(Func<TSource,Boolean>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource MaxBy<TSource, TKey>(Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource MaxBy<TSource, TKey>(Func<TSource,TKey>, IComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource MinBy<TSource, TKey>(Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource MinBy<TSource, TKey>(Func<TSource,TKey>, IComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource SingleOrDefault<TSource>(Func<TSource,Boolean>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<TSource> SkipLast<TSource>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast)
 * `HashSet<TSource> ToHashSet<TSource>(IEqualityComparer<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `Boolean TryGetNonEnumeratedCount<TSource>(Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount)


#### IList<T>

 * `Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))


#### Int16

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat)


#### Int32

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat)


#### Int64

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat)


#### IReadOnlyDictionary<TKey,TValue>

 * `TValue GetValueOrDefault<TKey, TValue>(TKey)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault)
 * `TValue GetValueOrDefault<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### KeyValuePair<TKey,TValue>

 * `void Deconstruct<TKey, TValue>(TKey&, TValue&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct)


#### List<T>

 * `void AddRange<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange)
 * `void CopyTo<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto)
 * `void InsertRange<T>(Int32, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange)


#### Process

 * `Task WaitForExitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync)


#### Random

 * `void NextBytes(Span<Byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))


#### ReadOnlySpan<Char>

 * `Boolean EndsWith(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `Boolean SequenceEqual(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `Boolean StartsWith(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))


#### ReadOnlySpan<T>

 * `Boolean Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `Polyfills.Polyfill/SpanSplitEnumerator<T> Split<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `Polyfills.Polyfill/SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `Polyfills.Polyfill/SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `Polyfills.Polyfill/SpanSplitEnumerator<T> SplitAny<T>(Buffers.SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))


#### Reflection.EventInfo

 * `Reflection.NullabilityState GetNullability()`
 * `Reflection.NullabilityInfo GetNullabilityInfo()`
 * `Boolean IsNullable()`


#### Reflection.FieldInfo

 * `Reflection.NullabilityState GetNullability()`
 * `Reflection.NullabilityInfo GetNullabilityInfo()`
 * `Boolean IsNullable()`


#### Reflection.MemberInfo

 * `Reflection.NullabilityState GetNullability()`
 * `Reflection.NullabilityInfo GetNullabilityInfo()`
 * `Boolean HasSameMetadataDefinitionAs(Reflection.MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas)
 * `Boolean IsNullable()`


#### Reflection.ParameterInfo

 * `Reflection.NullabilityState GetNullability()`
 * `Reflection.NullabilityInfo GetNullabilityInfo()`
 * `Boolean IsNullable()`


#### Reflection.PropertyInfo

 * `Reflection.NullabilityState GetNullability()`
 * `Reflection.NullabilityInfo GetNullabilityInfo()`
 * `Boolean IsNullable()`


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32))
 * `Boolean IsMatch(ReadOnlySpan<Char>, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `Boolean IsMatch(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))


#### SByte

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat)


#### Single

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat)


#### SortedList<TKey,TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex)
 * `TValue GetValueAtIndex<TKey, TValue>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex)


#### Span<Char>

 * `Boolean EndsWith(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `Boolean SequenceEqual(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `Boolean StartsWith(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Span<Char> TrimEnd()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend#system-memoryextensions-trimend(system-span((system-char))))
 * `Span<Char> TrimStart()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart#system-memoryextensions-trimstart(system-span((system-char))))


#### Span<T>

 * `Boolean Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-span((-0))-0))


#### Stream

 * `Task CopyToAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<Int32> ReadAsync(Memory<Byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(ReadOnlyMemory<Byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### String

 * `Boolean Contains(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison))
 * `Boolean Contains(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `void CopyTo(Span<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto)
 * `Boolean EndsWith(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `Int32 GetHashCode(StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison))
 * `String[] Split(Char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions))
 * `String[] Split(Char, Int32, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `Boolean StartsWith(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `Boolean TryCopyTo(Span<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto)


#### StringBuilder

 * `StringBuilder Append(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, StringBuilder/AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider, StringBuilder/AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin(String, String[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin(String, Object[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(Char, String[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(Char, Object[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin<T>(Char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(String, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, StringBuilder/AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider, StringBuilder/AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `void CopyTo(Int32, Span<Char>, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `Boolean Equals(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char))))
 * `Polyfills.Polyfill/ChunkEnumerator GetChunks()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks)
 * `StringBuilder Replace(ReadOnlySpan<Char>, ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))
 * `StringBuilder Replace(ReadOnlySpan<Char>, ReadOnlySpan<Char>, Int32, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))


#### Task

 * `Task WaitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan))
 * `Task WaitAsync(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<Int32> ReadAsync(Memory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<String> ReadLineAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<String> ReadToEndAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `void Write(StringBuilder)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder))
 * `void Write(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `Task WriteAsync(StringBuilder, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(ReadOnlyMemory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void WriteLine(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `ValueTask WriteLineAsync(ReadOnlyMemory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


#### TimeOnly

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat)


#### TimeSpan

 * `Int32 Microseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds)
 * `Int32 Nanoseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds)
 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `Boolean IsAssignableFrom<T>()`
 * `Boolean IsAssignableTo<T>()`
 * `Boolean IsAssignableTo(Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto)
 * `Boolean IsGenericMethodParameter()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter)


#### UInt16

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat)


#### UInt32

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat)


#### UInt64

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat)


#### Xml.Linq.XDocument

 * `Task SaveAsync(Xml.XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task SaveAsync(Stream, Xml.Linq.SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(TextWriter, Xml.Linq.SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))


### Static helpers

#### EnumPolyfill

 * `String[] GetNames<TEnum>() where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames)
 * `TEnum[] GetValues<TEnum>() where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues)
 * `TEnum Parse<TEnum>(String) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(String, Boolean) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(ReadOnlySpan<Char>) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char))))
 * `TEnum Parse<TEnum>(ReadOnlySpan<Char>, Boolean) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean))
 * `Boolean TryParse<TEnum>(ReadOnlySpan<Char>, TEnum&) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-0@))
 * `Boolean TryParse<TEnum>(ReadOnlySpan<Char>, Boolean, TEnum&) where TEnum : Enum, ValueType` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@))


#### RegexPolyfill

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `Regex/ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `Regex/ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `Regex/ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<Char>, String, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `Boolean IsMatch(ReadOnlySpan<Char>, String, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `Boolean IsMatch(ReadOnlySpan<Char>, String, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `Boolean IsMatch(ReadOnlySpan<Char>, String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string))


#### StringPolyfill

 * `String Join(Char, String[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()))
 * `String Join(Char, Object[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-object()))
 * `String Join(Char, String[], Int32, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()-system-int32-system-int32))
 * `String Join<T>(Char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join-1(system-char-system-collections-generic-ienumerable((-0))))


#### BytePolyfill

 * `Boolean TryParse(String, IFormatProvider, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-string-system-iformatprovider-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, Byte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))


#### DoublePolyfill

 * `Boolean TryParse(String, IFormatProvider, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-string-system-iformatprovider-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-double@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, Double&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@))


#### IntPolyfill

 * `Boolean TryParse(String, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-string-system-iformatprovider-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))


#### LongPolyfill

 * `Boolean TryParse(String, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-string-system-iformatprovider-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, Int32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))


#### SBytePolyfill

 * `Boolean TryParse(String, IFormatProvider, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, SByte&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))


#### ShortPolyfill

 * `Boolean TryParse(String, IFormatProvider, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-string-system-iformatprovider-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, Int16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))


#### UIntPolyfill

 * `Boolean TryParse(String, IFormatProvider, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, UInt32&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))


#### ULongPolyfill

 * `Boolean TryParse(String, IFormatProvider, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, UInt64&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))


#### UShortPolyfill

 * `Boolean TryParse(String, IFormatProvider, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, IFormatProvider, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, IFormatProvider, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, Globalization.NumberStyles, IFormatProvider, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Byte>, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `Boolean TryParse(ReadOnlySpan<Char>, Globalization.NumberStyles, IFormatProvider, UInt16&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))


#### Guard

 * `void DirectoryExists(String)`
 * `void FileExists(String)`
 * `void NotEmpty(String)`
 * `void NotEmpty<T>(ReadOnlySpan<T>)`
 * `void NotEmpty<T>(Span<T>)`
 * `void NotEmpty<T>(Nullable<Memory<T>>)`
 * `void NotEmpty<T>(Memory<T>)`
 * `void NotEmpty<T>(Nullable<ReadOnlyMemory<T>>)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>)`
 * `void NotEmpty<T>(T) where T : Collections.IEnumerable`
 * `T NotNull<T>(T)`
 * `String NotNull(String)`
 * `String NotNullOrEmpty(String)`
 * `T NotNullOrEmpty<T>(T) where T : Collections.IEnumerable`
 * `Memory<Char> NotNullOrEmpty(Nullable<Memory<Char>>)`
 * `ReadOnlyMemory<Char> NotNullOrEmpty(Nullable<ReadOnlyMemory<Char>>)`
 * `String NotNullOrWhiteSpace(String)`
 * `Memory<Char> NotNullOrWhiteSpace(Nullable<Memory<Char>>)`
 * `ReadOnlyMemory<Char> NotNullOrWhiteSpace(Nullable<ReadOnlyMemory<Char>>)`
 * `void NotWhiteSpace(String)`
 * `void NotWhiteSpace(ReadOnlySpan<Char>)`
 * `void NotWhiteSpace(Nullable<Memory<Char>>)`
 * `void NotWhiteSpace(Nullable<ReadOnlyMemory<Char>>)`
 * `void NotWhiteSpace(Span<Char>)`


#### TaskCompletionSource<!-- endInclude -->


## References

If any of the below reference are not included, the related polyfills will be disabled.


### System.ValueTuple

If consuming in a project that targets `net461` or `net462`, a reference to [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/) nuget is required.

```xml
<PackageReference Include="System.ValueTuple"
                  Version="4.5.0"
                  Condition="$(TargetFramework.StartsWith('net46'))" />
```


### System.Memory

If using Span APIs and consuming in a project that targets `netstandard`, `netframework`, or `netcoreapp2*`, a reference to [System.Memory](https://www.nuget.org/packages/System.Memory/) nuget is required.

```xml
<PackageReference Include="System.Memory"
                  Version="4.5.5"
                  Condition="$(TargetFrameworkIdentifier) == '.NETStandard' or
                             $(TargetFrameworkIdentifier) == '.NETFramework' or
                             $(TargetFramework.StartsWith('netcoreapp2'))" />
```


### System.Threading.Tasks.Extensions

If using ValueTask APIs and consuming in a project that target `netframework`, `netstandard2`, or `netcoreapp2`, a reference to [System.Threading.Tasks.Extensions](https://www.nuget.org/packages/System.Threading.Tasks.Extensions/) nuget is required.

```xml
<PackageReference Include="System.Threading.Tasks.Extensions"
                  Version="4.5.4"
                  Condition="$(TargetFramework) == 'netstandard2.0' or
                             $(TargetFramework) == 'netcoreapp2.0' or
                             $(TargetFrameworkIdentifier) == '.NETFramework'" />
```


## Nullability


### Example target class

Given the following class

<!-- snippet: NullabilityTarget -->
<a id='snippet-NullabilityTarget'></a>
```cs
class NullabilityTarget
{
    public string? StringField;
    public string?[] ArrayField;
    public Dictionary<string, object?> GenericField;
}
```
<sup><a href='/src/Tests/NullabilitySamples.cs#L74-L83' title='Snippet source file'>snippet source</a> | <a href='#snippet-NullabilityTarget' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### NullabilityInfoContext

<!-- snippet: NullabilityUsage -->
<a id='snippet-NullabilityUsage'></a>
```cs
[Test]
public void Test()
{
    var type = typeof(NullabilityTarget);
    var arrayField = type.GetField("ArrayField")!;
    var genericField = type.GetField("GenericField")!;

    var context = new NullabilityInfoContext();

    var arrayInfo = context.Create(arrayField);

    Assert.AreEqual(NullabilityState.NotNull, arrayInfo.ReadState);
    Assert.AreEqual(NullabilityState.Nullable, arrayInfo.ElementType!.ReadState);

    var genericInfo = context.Create(genericField);

    Assert.AreEqual(NullabilityState.NotNull, genericInfo.ReadState);
    Assert.AreEqual(NullabilityState.NotNull, genericInfo.GenericTypeArguments[0].ReadState);
    Assert.AreEqual(NullabilityState.Nullable, genericInfo.GenericTypeArguments[1].ReadState);
}
```
<sup><a href='/src/Tests/NullabilitySamples.cs#L6-L29' title='Snippet source file'>snippet source</a> | <a href='#snippet-NullabilityUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### NullabilityInfoExtensions

Enable by adding and MSBuild property `PolyNullability`

```
<PropertyGroup>
  ...
  <PolyNullability>true</PolyNullability>
</PropertyGroup>
```


`NullabilityInfoExtensions` provides static and thread safe wrapper around `NullabilityInfoContext`. It adds three extension methods to each of ParameterInfo, PropertyInfo, EventInfo, and FieldInfo.

 * `GetNullabilityInfo`: returns the `NullabilityInfo` for the target info.
 * `GetNullability`: returns the `NullabilityState` for the state (`NullabilityInfo.ReadState` or `NullabilityInfo.WriteState` depending on which has more info) of target info.
 * `IsNullable`: given the state (`NullabilityInfo.ReadState` or `NullabilityInfo.WriteState` depending on which has more info) of the info:
   * Returns true if state is `NullabilityState.Nullable`.
   * Returns false if state is `NullabilityState.NotNull`.
   * Throws an exception if state is `NullabilityState.Unknown`.


## Guard

Enable by adding and MSBuild property `PolyGuard`

```
<PropertyGroup>
  ...
  <PolyGuard>true</PolyGuard>
</PropertyGroup>
```

`Guard` is designed to be a an alternative to the `ArgumentException.ThrowIf*` APIs added in net7.

 * `ArgumentException.ThrowIfNullOrEmpty` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorempty)
 * `ArgumentException.ThrowIfNullOrWhiteSpace` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorwhitespace)
 * `ArgumentNullException.ThrowIfNull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-8.0)

With the equivalent Guard APIs:

 * `Guard.NotNullOrEmpty`
 * `Guard.NotNullOrWhiteSpace`
 * `Guard.NotNull`

`Polyfills.Guard` provides the following APIs:


#### Guard<!-- include: api_guard. path: /api_guard.include.md -->

 * `void DirectoryExists(String)`
 * `void FileExists(String)`
 * `void NotEmpty(String)`
 * `void NotEmpty<T>(ReadOnlySpan<T>)`
 * `void NotEmpty<T>(Span<T>)`
 * `void NotEmpty<T>(Nullable<Memory<T>>)`
 * `void NotEmpty<T>(Memory<T>)`
 * `void NotEmpty<T>(Nullable<ReadOnlyMemory<T>>)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>)`
 * `void NotEmpty<T>(T) where T : Collections.IEnumerable`
 * `T NotNull<T>(T)`
 * `String NotNull(String)`
 * `String NotNullOrEmpty(String)`
 * `T NotNullOrEmpty<T>(T) where T : Collections.IEnumerable`
 * `Memory<Char> NotNullOrEmpty(Nullable<Memory<Char>>)`
 * `ReadOnlyMemory<Char> NotNullOrEmpty(Nullable<ReadOnlyMemory<Char>>)`
 * `String NotNullOrWhiteSpace(String)`
 * `Memory<Char> NotNullOrWhiteSpace(Nullable<Memory<Char>>)`
 * `ReadOnlyMemory<Char> NotNullOrWhiteSpace(Nullable<ReadOnlyMemory<Char>>)`
 * `void NotWhiteSpace(String)`
 * `void NotWhiteSpace(ReadOnlySpan<Char>)`
 * `void NotWhiteSpace(Nullable<Memory<Char>>)`
 * `void NotWhiteSpace(Nullable<ReadOnlyMemory<Char>>)`
 * `void NotWhiteSpace(Span<Char>)`

<!-- endInclude -->


## Alternatives


### PolyShim

https://github.com/Tyrrrz/PolyShim


### PolySharp

https://github.com/Sergio0694/PolySharp


### Theraot.Core

https://github.com/theraot/Theraot


### Combination of

 * https://github.com/manuelroemer/Nullable
 * https://github.com/bgrainger/IndexRange
 * https://github.com/manuelroemer/IsExternalInit


### Reason this project was created instead of using the above

PolySharp uses c# source generators. In my opinion a "source-only package" implementation is better because:

 * Simpler implementation
 * Easier to debug if something goes wrong.
 * Uses less memory at compile time. Since there is no source generator assembly to load.
 * Faster at compile time. Since no source generator is required to execute.

The combination of the other 3 packages is not ideal because:

 * Required multiple packages to be referenced.
 * Does not cover all the scenarios included in this package.


## Notes

 * [.NET 9.0 Preview 1 API Changes](https://github.com/dotnet/core/tree/main/release-notes/9.0/preview/preview1/api-diff)


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
