# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`


**API count: 559**<!-- singleLineInclude: apiCount. path: /apiCount.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**


### Powered by

[![JetBrains logo.](https://resources.jetbrains.com/storage/products/company/brand/logos/jetbrains.svg)](https://jb.gg/OpenSourceSupport)


## TargetFrameworks

Some polyfills are implemented in a way that will not always have the equivalent performance to the actual implementations.

For example the polyfill for `StringBuilder.Append(ReadOnlySpan<char>)` on netcore2 is:

```
public StringBuilder Append(ReadOnlySpan<char> value)
    => target.Append(value.ToString());
```

Which will result in a string allocation.

As Polyfill is implemented as a source only nuget, the implementation for each polyfill is compiled into the IL of the consuming assembly. As a side-effect that implementation will continue to be used even if that assembly is executed in a runtime that has a more efficient implementation available.

As a result, in the context of a project producing nuget package, that project should target all frameworks from the lowest TargetFramework up to and including the current framework. This way the most performant implementation will be used for each runtime. Take the following examples:

 * If a nuget's minimum target is net6, then the resulting TargetFrameworks should also include net7.0 and net8.0
 * If a nuget's minimum target is net471, then the resulting TargetFrameworks should also include net472 and net48"


## Nuget

https://nuget.org/packages/Polyfill/


## SDK / LangVersion

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
    "version": "9.0.202",
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


## Troubleshooting

Make sure `DefineConstants` is not set from dotnet CLI, which would override important constants set by Polyfill.

Instead of using `dotnet publish -p:DefineConstants=MY_CONSTANT`, set the constant indirectly in the project:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <DefineConstants Condition="'$(MyConstant)' == 'true'">$(DefineConstants);MY_CONSTANT</DefineConstants>
```
and use `dotnet publish -p:MyConstant=true`.


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

<!-- snippet: UnscopedRefUsage -->
<a id='snippet-UnscopedRefUsage'></a>
```cs
struct UnscopedRefUsage
{
    int field1;

    [UnscopedRef] ref int Prop1 => ref field1;
}
```
<sup><a href='/src/Consume/UnscopedRefUsage.cs#L5-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-UnscopedRefUsage' title='Start of snippet'>anchor</a></sup>
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

#### bool

 * `bool TryFormat(bool, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat)


#### byte

 * `bool TryFormat(byte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(byte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat#system-byte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### CancellationToken

 * `CancellationTokenRegistration Register(CancellationToken, Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync(CancellationTokenSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync)


#### ConcurrentBag<T>

 * `void Clear<T>(ConcurrentBag<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear)


#### ConcurrentDictionary<TKey, TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(ConcurrentDictionary<TKey, TValue>, TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### ConcurrentQueue<T>

 * `void Clear<T>(ConcurrentQueue<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear)


#### DateOnly

 * `bool TryFormat(DateOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat#system-dateonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTime

 * `DateTime AddMicroseconds(DateTime, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds)
 * `void Deconstruct(DateTime, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(DateTime, DateOnly, TimeOnly)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct#system-datetime-deconstruct(system-dateonly@-system-timeonly@))
 * `int Microsecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond)
 * `int Nanosecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond)
 * `bool TryFormat(DateTime, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTime, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat#system-datetime-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(DateTimeOffset, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds)
 * `void Deconstruct(DateTimeOffset, DateOnly, TimeOnly, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct)
 * `int Microsecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond)
 * `int Nanosecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond)
 * `bool TryFormat(DateTimeOffset, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTimeOffset, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat#system-datetimeoffset-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### decimal

 * `bool TryFormat(decimal, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(decimal, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat#system-decimal-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DefaultInterpolatedStringHandler

 * `void Clear(DefaultInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear)


#### Delegate

 * `bool HasSingleTarget(Delegate)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget)


#### Dictionary<TKey, TValue>

 * `void EnsureCapacity<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity)
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess#system-collections-generic-dictionary-2-trimexcess(system-int32))
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity)


#### DictionaryEntry

 * `void Deconstruct(DictionaryEntry, object, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct#system-collections-dictionaryentry-deconstruct(system-object@-system-object@))


#### double

 * `bool TryFormat(double, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(double, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat#system-double-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Encoding

 * `int GetByteCount(Encoding, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount#system-text-encoding-getbytecount(system-readonlyspan((system-char))))
 * `int GetBytes(Encoding, ReadOnlySpan<char>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte))))
 * `int GetCharCount(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount#system-text-encoding-getcharcount(system-readonlyspan((system-byte))))
 * `int GetChars(Encoding, ReadOnlySpan<byte>, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char))))
 * `string GetString(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring#system-text-encoding-getstring(system-readonlyspan((system-byte))))
 * `bool TryGetBytes(Encoding, ReadOnlySpan<char>, Span<byte>, int)`
 * `bool TryGetChars(Encoding, ReadOnlySpan<byte>, Span<char>, int)`


#### EventInfo

 * `NullabilityState GetNullability(EventInfo)`
 * `NullabilityInfo GetNullabilityInfo(EventInfo)`
 * `bool IsNullable(EventInfo)`


#### FieldInfo

 * `NullabilityState GetNullability(FieldInfo)`
 * `NullabilityInfo GetNullabilityInfo(FieldInfo)`
 * `bool IsNullable(FieldInfo)`


#### float

 * `bool TryFormat(float, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(float, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat#system-single-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Guid

 * `bool TryFormat(Guid, Span<byte>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))))
 * `bool TryFormat(Guid, Span<char>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))


#### HashSet<T>

 * `void EnsureCapacity<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity#system-collections-generic-hashset-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?#system-collections-generic-hashset-1-trimexcess(system-int32))
 * `bool TryGetValue<T>(HashSet<T>, T, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue)


#### HttpClient

 * `Task<byte[]> GetByteArrayAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<byte[]> ReadAsByteArrayAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<Stream> ReadAsStreamAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<string> ReadAsStringAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))
 * `bool Remove<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove)
 * `bool TryAdd<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd)


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TFirst>, IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> Append<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append)
 * `IEnumerable<TSource[]> Chunk<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk)
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby)
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource ElementAt<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource? ElementAtOrDefault<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<(int Index, TSource Item)> Index<TSource>(IEnumerable<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource? Max<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? Min<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast)
 * `IEnumerable<TSource> Take<TSource>(IEnumerable<TSource>, Range)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range))
 * `IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast)
 * `HashSet<TSource> ToHashSet<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool TryGetNonEnumeratedCount<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount)


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>(IList<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))


#### int

 * `bool TryFormat(int, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(int, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat#system-int32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### IReadOnlyDictionary<TKey, TValue>

 * `TValue? GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault)
 * `TValue GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### ISet<T>

 * `ReadOnlySet<T> AsReadOnly<T>(ISet<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0))))


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(KeyValuePair<TKey, TValue>, TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct)


#### List<T>

 * `void AddRange<T>(List<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange)
 * `void CopyTo<T>(List<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto)
 * `void EnsureCapacity<T>(List<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity#system-collections-generic-list-1-ensurecapacity(system-int32))
 * `void InsertRange<T>(List<T>, int, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange)
 * `void TrimExcess<T>(List<T>)`


#### long

 * `bool TryFormat(long, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(long, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat#system-int64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### MemberInfo

 * `NullabilityState GetNullability(MemberInfo)`
 * `NullabilityInfo GetNullabilityInfo(MemberInfo)`
 * `bool HasSameMetadataDefinitionAs(MemberInfo, MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas)
 * `bool IsNullable(MemberInfo)`


#### MethodInfo

 * `T CreateDelegate<T>(MethodInfo) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate#system-reflection-methodinfo-createdelegate-1)
 * `T CreateDelegate<T>(MethodInfo, object?) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?#system-reflection-methodinfo-createdelegate-1(system-object))


#### OrderedDictionary<TKey, TValue>

 * `bool TryAdd<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue)
 * `bool TryGetValue<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue)


#### ParameterInfo

 * `NullabilityState GetNullability(ParameterInfo)`
 * `NullabilityInfo GetNullabilityInfo(ParameterInfo)`
 * `bool IsNullable(ParameterInfo)`


#### Process

 * `Task WaitForExitAsync(Process, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync)


#### PropertyInfo

 * `NullabilityState GetNullability(PropertyInfo)`
 * `NullabilityInfo GetNullabilityInfo(PropertyInfo)`
 * `bool IsNullable(PropertyInfo)`


#### Queue<T>

 * `void EnsureCapacity<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity#system-collections-generic-queue-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess#system-collections-generic-queue-1-trimexcess(system-int32))


#### Random

 * `void GetItems<T>(Random, ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `T[] GetItems<T>(Random, ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems#system-random-getitems-1(system-readonlyspan((-0))-system-int32))
 * `T[] GetItems<T>(Random, T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems#system-random-getitems-1(-0()-system-int32))
 * `void NextBytes(Random, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte))))


#### ReadOnlySpan<char>

 * `bool EndsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `int GetNormalizedLength(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool IsNormalized(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool SequenceEqual(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool TryNormalize(ReadOnlySpan<char>, Span<char>, int, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform))


#### ReadOnlySpan<T>

 * `bool Contains<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `bool EndsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool StartsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))))
 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(Regex, ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(Regex, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))


#### sbyte

 * `bool TryFormat(sbyte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(sbyte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat#system-sbyte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### short

 * `bool TryFormat(short, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(short, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat#system-int16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex)
 * `TValue GetValueAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex)


#### Span<char>

 * `bool EndsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `bool SequenceEqual(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Span<char> TrimEnd(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend#system-memoryextensions-trimend(system-span((system-char))))
 * `Span<char> TrimStart(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart#system-memoryextensions-trimstart(system-span((system-char))))


#### Span<T>

 * `bool Contains<T>(Span<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-span((-0))-0))


#### Stack<T>

 * `void EnsureCapacity<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity)
 * `void TrimExcess<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess#system-collections-generic-stack-1-trimexcess(system-int32))
 * `bool TryPeek<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek)
 * `bool TryPop<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop)


#### Stream

 * `Task CopyToAsync(Stream, Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask DisposeAsync(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync)
 * `ValueTask<int> ReadAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(Stream, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### string

 * `bool Contains(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char-system-stringcomparison))
 * `bool Contains(string, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison))
 * `bool Contains(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `void CopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto)
 * `bool EndsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith#system-string-endswith(system-char))
 * `int GetHashCode(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison))
 * `int IndexOf(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof#system-string-indexof(system-char-system-stringcomparison))
 * `string ReplaceLineEndings(string, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings#system-string-replacelineendings(system-string))
 * `string ReplaceLineEndings(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings#system-string-replacelineendings)
 * `string[] Split(string, char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions))
 * `string[] Split(string, char, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `bool StartsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith#system-string-startswith(system-char))
 * `bool TryCopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto)


#### StringBuilder

 * `StringBuilder Append(StringBuilder, StringBuilder?, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32))
 * `StringBuilder Append(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin<T>(StringBuilder, char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string?, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin(StringBuilder, string?, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, string?, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(StringBuilder, char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin<T>(StringBuilder, char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `void CopyTo(StringBuilder, int, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `bool Equals(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char))))
 * `ChunkEnumerator GetChunks(StringBuilder)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks)
 * `StringBuilder Insert(StringBuilder, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char))))
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))


#### Task

 * `Task WaitAsync(Task, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(Task, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan))
 * `Task WaitAsync(Task, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(TaskCompletionSource<T>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<int> ReadAsync(TextReader, Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<string> ReadLineAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<string> ReadToEndAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `Task FlushAsync(TextWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync#system-io-textwriter-flushasync(system-threading-cancellationtoken))
 * `void Write(TextWriter, StringBuilder?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder))
 * `void Write(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `Task WriteAsync(TextWriter, StringBuilder?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void WriteLine(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `ValueTask WriteLineAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


#### TimeOnly

 * `void Deconstruct(TimeOnly, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct#system-timeonly-deconstruct(system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@))
 * `bool TryFormat(TimeOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat#system-timeonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TimeSpan

 * `int Microseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds)
 * `int Nanoseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds)
 * `bool TryFormat(TimeSpan, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeSpan, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat#system-timespan-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `MethodInfo? GetMethod(Type, string, int, BindingFlags, Type[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type()))
 * `bool IsAssignableFrom<T>(Type)`
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableTo(Type, Type?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto)
 * `bool IsGenericMethodParameter(Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter)


#### uint

 * `bool TryFormat(uint, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(uint, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat#system-uint32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### ulong

 * `bool TryFormat(ulong, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ulong, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat#system-uint64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### ushort

 * `bool TryFormat(ushort, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ushort, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat#system-uint16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### XDocument

 * `Task SaveAsync(XDocument, XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))


### Static helpers

#### BytePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(string?, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-string-system-iformatprovider-system-byte@))


#### ConvertPolyfill

 * `string ToHexString(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring#system-convert-tohexstring(system-byte()-system-int32-system-int32))


#### DateTimeOffsetPolyfill

 * `bool TryParse(ReadOnlySpan<char>, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParse(string?, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))


#### DateTimePolyfill

 * `bool TryParse(ReadOnlySpan<char>, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParse(string?, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))


#### DelegatePolyfill

 * `InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate?) where TDelegate : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist)


#### DoublePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(string?, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-string-system-iformatprovider-system-double@))


#### EnumPolyfill

 * `string[] GetNames<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames)
 * `TEnum[] GetValues<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues)
 * `bool IsDefined<TEnum>(TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined#system-enum-isdefined-1(-0))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char))))
 * `TEnum Parse<TEnum>(string, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(string) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean))
 * `bool TryFormat<TEnum>(TEnum, Span<char>, int, ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryformat)
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, bool, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@))
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-0@))


#### FilePolyfill

 * `void AppendAllBytes(string, byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes#system-io-file-appendallbytes(system-string-system-byte()))
 * `void AppendAllBytes(string, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes#system-io-file-appendallbytes(system-string-system-readonlyspan((system-byte))))
 * `Task AppendAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync#system-io-file-appendallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task AppendAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void AppendAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void AppendAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-text-encoding-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync#system-io-file-appendalltextasync(system-string-system-string-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync#system-io-file-appendalltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `void Move(string, string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.move?view=net-9.0#system-io-file-move(system-string-system-string-system-boolean))
 * `Task<byte[]> ReadAllBytesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readallbytesasync)
 * `Task<string[]> ReadAllLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `Task<string[]> ReadAllLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync#system-io-file-readalltextasync(system-string-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync#system-io-file-readalltextasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writeallbytesasync#system-io-file-writeallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void WriteAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext#system-io-file-writealltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void WriteAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext#system-io-file-writealltext(system-string-system-readonlyspan((system-char))))
 * `Task WriteAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))


#### GuidPolyfill

 * `Guid CreateVersion7()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7#system-guid-createversion7)
 * `Guid CreateVersion7(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7#system-guid-createversion7(system-datetimeoffset))
 * `bool TryParse(ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@))
 * `bool TryParse(string?, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-string-system-iformatprovider-system-guid@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@))


#### IntPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(string?, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-string-system-iformatprovider-system-int32@))


#### LongPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(string?, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-string-system-iformatprovider-system-int64@))


#### MathPolyfill

 * `byte Clamp(byte, byte, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `decimal Clamp(decimal, decimal, decimal)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `double Clamp(double, double, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `float Clamp(float, float, float)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `int Clamp(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `long Clamp(long, long, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `nint Clamp(nint, nint, nint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `nuint Clamp(nuint, nuint, nuint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `sbyte Clamp(sbyte, sbyte, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `short Clamp(short, short, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `uint Clamp(uint, uint, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `ulong Clamp(ulong, ulong, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)
 * `ushort Clamp(ushort, ushort, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp)


#### OperatingSystemPolyfill

 * `bool IsAndroid()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroid)
 * `bool IsAndroidVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroidversionatleast)
 * `bool IsBrowser()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isbrowser)
 * `bool IsFreeBSD()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsd)
 * `bool IsFreeBSDVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsdversionatleast)
 * `bool IsIOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isios)
 * `bool IsIOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isiosversionatleast)
 * `bool IsLinux()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.islinux)
 * `bool IsMacCatalyst()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalyst)
 * `bool IsMacCatalystVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalystversionatleast)
 * `bool IsMacOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacos)
 * `bool IsMacOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacosversionatleast)
 * `bool IsOSPlatform(string)`
 * `bool IsOSPlatformVersionAtLeast(string, int, int, int, int)`
 * `bool IsTvOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvos)
 * `bool IsTvOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvosversionatleast)
 * `bool IsWasi()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswasi)
 * `bool IsWatchOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchos)
 * `bool IsWatchOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchosversionatleast)
 * `bool IsWindows()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindows)
 * `bool IsWindowsVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindowsversionatleast)


#### PathPolyfill

 * `string Combine(ReadOnlySpan<string>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine#system-io-path-combine(system-readonlyspan((system-string))))
 * `bool EndsInDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator#system-io-path-endsindirectoryseparator(system-readonlyspan((system-char))))
 * `bool EndsInDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator#system-io-path-endsindirectoryseparator(system-string))
 * `bool Exists(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.exists)
 * `ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname#system-io-path-getdirectoryname(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getextension#system-io-path-getextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilename#system-io-path-getfilename(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `bool HasExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator#system-io-path-trimendingdirectoryseparator(system-readonlyspan((system-char))))
 * `string TrimEndingDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator#system-io-path-trimendingdirectoryseparator(system-string))


#### RandomPolyfill



#### RegexPolyfill

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `bool IsMatch(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string))


#### SBytePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@))
 * `bool TryParse(string?, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@))


#### ShortPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@))
 * `bool TryParse(string?, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-string-system-iformatprovider-system-int16@))


#### StringPolyfill

 * `string Join(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-object()))
 * `string Join(char, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-readonlyspan((system-object))))
 * `string Join(char, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-readonlyspan((system-string))))
 * `string Join(char, string?[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()-system-int32-system-int32))
 * `string Join(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()))
 * `string Join(string?, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-string-system-readonlyspan((system-object))))
 * `string Join(string?, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-string-system-readonlyspan((system-string))))
 * `string Join<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join-1(system-char-system-collections-generic-ienumerable((-0))))


#### UIntPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@))
 * `bool TryParse(string?, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@))


#### ULongPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@))
 * `bool TryParse(string?, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@))


#### UShortPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@))
 * `bool TryParse(string?, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@))


#### Guard

 * `void DirectoryExists(string)`
 * `void FileExists(string)`
 * `void NotEmpty(string?)`
 * `void NotEmpty<T>(Memory<T>?)`
 * `void NotEmpty<T>(Memory<T>)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>?)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>)`
 * `void NotEmpty<T>(ReadOnlySpan<T>)`
 * `void NotEmpty<T>(Span<T>)`
 * `void NotEmpty<T>(T?) where T : IEnumerable`
 * `string NotNull(string?)`
 * `T NotNull<T>(T?) where T : class`
 * `Memory<char> NotNullOrEmpty(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrEmpty(ReadOnlyMemory<char>?)`
 * `string NotNullOrEmpty(string?)`
 * `T NotNullOrEmpty<T>(T?) where T : IEnumerable`
 * `Memory<char> NotNullOrWhiteSpace(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrWhiteSpace(ReadOnlyMemory<char>?)`
 * `string NotNullOrWhiteSpace(string?)`
 * `void NotWhiteSpace(Memory<char>?)`
 * `void NotWhiteSpace(ReadOnlyMemory<char>?)`
 * `void NotWhiteSpace(ReadOnlySpan<char>)`
 * `void NotWhiteSpace(Span<char>)`
 * `void NotWhiteSpace(string?)`


#### Lock

 * `void Enter()`
 * `Scope EnterScope()`
 * `void Exit()`
 * `bool TryEnter()`
 * `bool TryEnter(int)`
 * `bool TryEnter(TimeSpan)`


#### KeyValuePair

 * `KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create)


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

### System.Runtime.InteropServices

If using the RuntimeInformation class or OSPlatform struct and consuming in a project that targets `netframework`, a reference to [System.Runtime.InteropServices.RuntimeInformation](https://www.nuget.org/packages/System.Runtime.InteropServices.RuntimeInformation) nuget is required.

```xml
<PackageReference Include="System.Runtime.InteropServices.RuntimeInformation"
                  Version="4.3.0"
                  Condition="$(TargetFrameworkIdentifier) == '.NETFramework'" />
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

Enable by adding an MSBuild property `PolyNullability`

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

Enable by adding an MSBuild property `PolyGuard`

```
<PropertyGroup>
  ...
  <PolyGuard>true</PolyGuard>
</PropertyGroup>
```

`Guard` is designed to be a an alternative to the `ArgumentException.ThrowIf*` APIs added in net7.

 * `ArgumentException.ThrowIfNullOrEmpty` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorempty)
 * `ArgumentException.ThrowIfNullOrWhiteSpace` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorwhitespace)
 * `ArgumentNullException.ThrowIfNull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull)

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
