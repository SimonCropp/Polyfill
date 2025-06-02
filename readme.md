# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`
 * `uap10`


**API count: 607**<!-- singleLineInclude: apiCount. path: /apiCount.include.md -->


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

 * https://nuget.org/packages/Polyfill/
 * https://nuget.org/packages/PolyfillLib/ See [#PolyfillLib]


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


## PolyfillLib

Polyfill is source only nuget designed to be consumed by public facing nuget packages. The benefit being no dependency and no chance for dependency conflicts.

Internal facing systems (nugets and build pipelines) have less risk of having dependency conflicts. For example a company with an internal nuget feed used to produce apps. In these scenarios the extra cost of using a source only nuget may be considered less than ideal. ie the added assembly size and the increased build time. PolyfillLib is a standard nuget package containing assemblies for each target runtime. It is an alternative approach to using the Polyfill source only package.

https://nuget.org/packages/PolyfillLib/


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

Enable by adding an MSBuild property `PolyStringInterpolation`

```
<PropertyGroup>
  ...
  <PolyStringInterpolation>true</PolyStringInterpolation>
</PropertyGroup>
```

 * [AppendInterpolatedStringHandler](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendinterpolatedstringhandler)
 * [DefaultInterpolatedStringHandler](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler)
 * [InterpolatedStringHandlerAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerattribute)
 * [InterpolatedStringHandlerArgumentAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerargumentattribute)
 * [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable)

References: [String Interpolation in C# 10 and .NET 6](https://devblogs.microsoft.com/dotnet/string-interpolation-in-c-10-and-net-6/), [Write a custom string interpolation handler](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/interpolated-string-handler)

> [!IMPORTANT]
> In the below extension method list, the methods using `AppendInterpolatedStringHandler` parameter are not extensions because the compiler prefers to use the overload with `string` parameter instead.


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


### Extension methods<!-- include: api_list.include.md -->

#### bool

 * `bool TryFormat(bool, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat?view=net-10.0)


#### byte

 * `bool TryFormat(byte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(byte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### CancellationToken

 * `CancellationTokenRegistration Register(CancellationToken, Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register?view=net-10.0#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync(CancellationTokenSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-10.0)


#### ConcurrentBag<T>

 * `void Clear<T>(ConcurrentBag<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear?view=net-10.0)


#### ConcurrentDictionary<TKey, TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(ConcurrentDictionary<TKey, TValue>, TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### ConcurrentQueue<T>

 * `void Clear<T>(ConcurrentQueue<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear?view=net-10.0)


#### DateOnly

 * `void Deconstruct(DateOnly, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.deconstruct?view=net-10.0)
 * `bool TryFormat(DateOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTime

 * `DateTime AddMicroseconds(DateTime, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-10.0)
 * `void Deconstruct(DateTime, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(DateTime, DateOnly, TimeOnly)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@))
 * `int Microsecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-10.0)
 * `int Nanosecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-10.0)
 * `bool TryFormat(DateTime, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTime, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(DateTimeOffset, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-10.0)
 * `void Deconstruct(DateTimeOffset, DateOnly, TimeOnly, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-10.0)
 * `int Microsecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-10.0)
 * `int Nanosecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-10.0)
 * `bool TryFormat(DateTimeOffset, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTimeOffset, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### decimal

 * `bool TryFormat(decimal, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(decimal, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DefaultInterpolatedStringHandler

 * `void Clear(DefaultInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear?view=net-10.0)


#### Delegate

 * `bool HasSingleTarget(Delegate)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget?view=net-10.0)


#### Dictionary<TKey, TValue>

 * `void EnsureCapacity<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-10.0#system-collections-generic-dictionary-2-trimexcess(system-int32))
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)


#### DictionaryEntry

 * `void Deconstruct(DictionaryEntry, object, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct?view=net-10.0#system-collections-dictionaryentry-deconstruct(system-object@-system-object@))


#### double

 * `bool TryFormat(double, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(double, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Encoding

 * `int GetByteCount(Encoding, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount?view=net-10.0#system-text-encoding-getbytecount(system-readonlyspan((system-char))))
 * `int GetBytes(Encoding, ReadOnlySpan<char>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-10.0#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte))))
 * `int GetCharCount(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount?view=net-10.0#system-text-encoding-getcharcount(system-readonlyspan((system-byte))))
 * `int GetChars(Encoding, ReadOnlySpan<byte>, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars?view=net-10.0#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char))))
 * `string GetString(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring?view=net-10.0#system-text-encoding-getstring(system-readonlyspan((system-byte))))
 * `bool TryGetBytes(Encoding, ReadOnlySpan<char>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetbytes?view=net-10.0)
 * `bool TryGetChars(Encoding, ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetchars?view=net-10.0)


#### EventInfo

 * `NullabilityState GetNullability(EventInfo)`
 * `NullabilityInfo GetNullabilityInfo(EventInfo)`
 * `bool IsNullable(EventInfo)`


#### FieldInfo

 * `NullabilityState GetNullability(FieldInfo)`
 * `NullabilityInfo GetNullabilityInfo(FieldInfo)`
 * `bool IsNullable(FieldInfo)`


#### float

 * `bool TryFormat(float, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(float, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Guid

 * `bool TryFormat(Guid, Span<byte>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))))
 * `bool TryFormat(Guid, Span<char>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))


#### HashSet<T>

 * `void EnsureCapacity<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity?view=net-10.0#system-collections-generic-hashset-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?view=net-10.0#system-collections-generic-hashset-1-trimexcess(system-int32))
 * `bool TryGetValue<T>(HashSet<T>, T, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue?view=net-10.0)


#### HttpClient

 * `Task<byte[]> GetByteArrayAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<byte[]> ReadAsByteArrayAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync?view=net-10.0#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<Stream> ReadAsStreamAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync?view=net-10.0#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<string> ReadAsStringAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-10.0#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))
 * `bool Remove<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove?view=net-10.0)
 * `bool TryAdd<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd?view=net-10.0)


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TFirst>, IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> Append<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append?view=net-10.0)
 * `IEnumerable<TSource[]> Chunk<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk?view=net-10.0)
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby?view=net-10.0)
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource ElementAt<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-10.0#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource? ElementAtOrDefault<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault?view=net-10.0#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<(int Index, TSource Item)> Index<TSource>(IEnumerable<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index?view=net-10.0#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource? Max<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-10.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? Min<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-10.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast?view=net-10.0)
 * `IEnumerable<TSource> Take<TSource>(IEnumerable<TSource>, Range)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-10.0#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range))
 * `IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast?view=net-10.0)
 * `HashSet<TSource> ToHashSet<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-10.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool TryGetNonEnumeratedCount<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount?view=net-10.0)


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>(IList<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))


#### int

 * `bool TryFormat(int, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(int, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### IReadOnlyDictionary<TKey, TValue>

 * `TValue GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-10.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### ISet<T>

 * `ReadOnlySet<T> AsReadOnly<T>(ISet<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0))))


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(KeyValuePair<TKey, TValue>, TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct?view=net-10.0)


#### List<T>

 * `void AddRange<T>(List<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange?view=net-10.0)
 * `void CopyTo<T>(List<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-10.0)
 * `void EnsureCapacity<T>(List<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-10.0#system-collections-generic-list-1-ensurecapacity(system-int32))
 * `void InsertRange<T>(List<T>, int, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-10.0)
 * `void TrimExcess<T>(List<T>)`


#### long

 * `bool TryFormat(long, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(long, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### MemberInfo

 * `NullabilityState GetNullability(MemberInfo)`
 * `NullabilityInfo GetNullabilityInfo(MemberInfo)`
 * `bool HasSameMetadataDefinitionAs(MemberInfo, MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas?view=net-10.0)
 * `bool IsNullable(MemberInfo)`


#### MethodInfo

 * `T CreateDelegate<T>(MethodInfo) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1)
 * `T CreateDelegate<T>(MethodInfo, object?) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1(system-object))


#### OrderedDictionary<TKey, TValue>

 * `bool TryAdd<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)
 * `bool TryGetValue<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)


#### ParameterInfo

 * `NullabilityState GetNullability(ParameterInfo)`
 * `NullabilityInfo GetNullabilityInfo(ParameterInfo)`
 * `bool IsNullable(ParameterInfo)`


#### Process

 * `Task WaitForExitAsync(Process, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-10.0)


#### PropertyInfo

 * `NullabilityState GetNullability(PropertyInfo)`
 * `NullabilityInfo GetNullabilityInfo(PropertyInfo)`
 * `bool IsNullable(PropertyInfo)`


#### Queue<T>

 * `void EnsureCapacity<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity?view=net-10.0#system-collections-generic-queue-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-10.0#system-collections-generic-queue-1-trimexcess(system-int32))


#### Random

 * `void GetItems<T>(Random, ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `T[] GetItems<T>(Random, ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-int32))
 * `T[] GetItems<T>(Random, T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(-0()-system-int32))
 * `void NextBytes(Random, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))


#### ReadOnlySpan<char>

 * `bool EndsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `int GetNormalizedLength(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength?view=net-10.0#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool IsNormalized(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized?view=net-10.0#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool SequenceEqual(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool TryNormalize(ReadOnlySpan<char>, Span<char>, int, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize?view=net-10.0#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform))


#### ReadOnlySpan<T>

 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool Contains<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `bool Contains<T>(ReadOnlySpan<T>, T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>, SearchValues<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool EndsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `int IndexOf<T>(ReadOnlySpan<T>, T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.indexof?view=net-10.0#system-memoryextensions-indexof-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `int IndexOfAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool StartsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))))
 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(Regex, ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(Regex, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))


#### sbyte

 * `bool TryFormat(sbyte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(sbyte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### short

 * `bool TryFormat(short, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(short, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex?view=net-10.0)
 * `TValue GetValueAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex?view=net-10.0)


#### Span<char>

 * `bool EndsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `bool SequenceEqual(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Span<char> TrimEnd(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend?view=net-10.0#system-memoryextensions-trimend(system-span((system-char))))
 * `Span<char> TrimStart(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart?view=net-10.0#system-memoryextensions-trimstart(system-span((system-char))))


#### Span<T>

 * `int CommonPrefixLength<T>(Span<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))))
 * `int CommonPrefixLength<T>(Span<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool Contains<T>(Span<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-span((-0))-0))


#### Stack<T>

 * `void EnsureCapacity<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity?view=net-10.0)
 * `void TrimExcess<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess?view=net-10.0#system-collections-generic-stack-1-trimexcess(system-int32))
 * `bool TryPeek<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek?view=net-10.0)
 * `bool TryPop<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop?view=net-10.0)


#### Stream

 * `Task CopyToAsync(Stream, Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync?view=net-10.0#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask DisposeAsync(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync?view=net-10.0)
 * `ValueTask<int> ReadAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-10.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(Stream, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync?view=net-10.0#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### string

 * `bool Contains(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char))
 * `bool Contains(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char-system-stringcomparison))
 * `bool Contains(string, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-string-system-stringcomparison))
 * `void CopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-10.0)
 * `bool EndsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-10.0#system-string-endswith(system-char))
 * `int GetHashCode(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-stringcomparison))
 * `int IndexOf(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-10.0#system-string-indexof(system-char-system-stringcomparison))
 * `string ReplaceLineEndings(string, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings(system-string))
 * `string ReplaceLineEndings(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings)
 * `string[] Split(string, char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-stringsplitoptions))
 * `string[] Split(string, char, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `bool StartsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0#system-string-startswith(system-char))
 * `bool TryCopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-10.0)


#### StringBuilder

 * `StringBuilder Append(StringBuilder, StringBuilder?, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32))
 * `StringBuilder Append(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin<T>(StringBuilder, char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string?, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin(StringBuilder, string?, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, string?, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(StringBuilder, char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin<T>(StringBuilder, char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `void CopyTo(StringBuilder, int, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto?view=net-10.0#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `bool Equals(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals?view=net-10.0#system-text-stringbuilder-equals(system-readonlyspan((system-char))))
 * `ChunkEnumerator GetChunks(StringBuilder)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks?view=net-10.0)
 * `StringBuilder Insert(StringBuilder, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-10.0#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char))))
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))


#### Task

 * `Task WaitAsync(Task, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(Task, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan))
 * `Task WaitAsync(Task, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(TaskCompletionSource<T>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-10.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<int> ReadAsync(TextReader, Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync?view=net-10.0#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<string> ReadLineAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<string> ReadToEndAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `Task FlushAsync(TextWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-10.0#system-io-textwriter-flushasync(system-threading-cancellationtoken))
 * `void Write(TextWriter, StringBuilder?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-text-stringbuilder))
 * `void Write(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `Task WriteAsync(TextWriter, StringBuilder?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void WriteLine(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-10.0#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `ValueTask WriteLineAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-10.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


#### TimeOnly

 * `void Deconstruct(TimeOnly, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@))
 * `bool TryFormat(TimeOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TimeSpan

 * `int Microseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-10.0)
 * `int Nanoseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-10.0)
 * `bool TryFormat(TimeSpan, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeSpan, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `MethodInfo? GetMethod(Type, string, int, BindingFlags, Type[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-10.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type()))
 * `bool IsAssignableFrom<T>(Type)`
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableTo(Type, Type?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto?view=net-10.0)
 * `bool IsGenericMethodParameter(Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter?view=net-10.0)


#### uint

 * `bool TryFormat(uint, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(uint, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### ulong

 * `bool TryFormat(ulong, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ulong, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### ushort

 * `bool TryFormat(ushort, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ushort, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### XDocument

 * `Task SaveAsync(XDocument, XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))


#### ZipArchive

 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(ZipArchive, string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(ZipArchive, string, string, CompressionLevel, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `void ExtractToDirectory(ZipArchive, string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string-system-boolean))
 * `Task ExtractToDirectoryAsync(ZipArchive, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string))
 * `Task ExtractToDirectoryAsync(ZipArchive, string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectoryasync?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectoryasync(system-io-compression-ziparchive-system-string-system-boolean-system-threading-cancellationtoken))


#### ZipArchiveEntry

 * `Task ExtractToFileAsync(ZipArchiveEntry, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-threading-cancellationtoken))
 * `Task ExtractToFileAsync(ZipArchiveEntry, string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-boolean-system-threading-cancellationtoken))
 * `Task<Stream> OpenAsync(ZipArchiveEntry, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-10.0)


### Static helpers

#### BytePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(string?, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-string-system-iformatprovider-system-byte@))


#### ConvertPolyfill

 * `byte[] FromHexString(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-10.0#system-convert-fromhexstring(system-readonlyspan((system-char))))
 * `byte[] FromHexString(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-10.0#system-convert-fromhexstring(system-string))
 * `string ToHexString(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte()-system-int32-system-int32))
 * `string ToHexString(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte()))
 * `string ToHexString(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-readonlyspan((system-byte))))
 * `string ToHexStringLower(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-byte()-system-int32-system-int32))
 * `string ToHexStringLower(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-byte()))
 * `string ToHexStringLower(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-readonlyspan((system-byte))))
 * `bool TryToHexString(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstring?view=net-10.0)
 * `bool TryToHexStringLower(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstringlower?view=net-10.0)


#### DateTimeOffsetPolyfill

 * `bool TryParse(ReadOnlySpan<char>, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParse(string?, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-10.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-10.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))


#### DateTimePolyfill

 * `bool TryParse(ReadOnlySpan<char>, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParse(string?, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-10.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-10.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))


#### DelegatePolyfill

 * `InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate?) where TDelegate : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-10.0)


#### DoublePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(string?, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-string-system-iformatprovider-system-double@))


#### EnumPolyfill

 * `string[] GetNames<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames?view=net-10.0)
 * `TEnum[] GetValues<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues?view=net-10.0)
 * `bool IsDefined<TEnum>(TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined?view=net-10.0#system-enum-isdefined-1(-0))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-readonlyspan((system-char))))
 * `TEnum Parse<TEnum>(string, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(string) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-string-system-boolean))
 * `bool TryFormat<TEnum>(TEnum, Span<char>, int, ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryformat?view=net-10.0)
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, bool, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-10.0#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@))
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-10.0#system-enum-tryparse-1(system-readonlyspan((system-char))-0@))


#### FilePolyfill

 * `void AppendAllBytes(string, byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-10.0#system-io-file-appendallbytes(system-string-system-byte()))
 * `void AppendAllBytes(string, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-10.0#system-io-file-appendallbytes(system-string-system-readonlyspan((system-byte))))
 * `Task AppendAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task AppendAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-10.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-10.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void AppendAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-10.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void AppendAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-10.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-text-encoding-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-string-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `void Move(string, string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.move?system-io-file-move(system-string-system-string-system-boolean)?view=net-10.0)
 * `Task<byte[]> ReadAllBytesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readallbytesasync?view=net-10.0)
 * `Task<string[]> ReadAllLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `Task<string[]> ReadAllLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-10.0#system-io-file-readalltextasync(system-string-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-10.0#system-io-file-readalltextasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writeallbytesasync?view=net-10.0#system-io-file-writeallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-10.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-10.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void WriteAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-10.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void WriteAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-10.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))))
 * `Task WriteAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-10.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-10.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))


#### GuidPolyfill

 * `Guid CreateVersion7()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-10.0#system-guid-createversion7)
 * `Guid CreateVersion7(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-10.0#system-guid-createversion7(system-datetimeoffset))
 * `bool TryParse(ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@))
 * `bool TryParse(string?, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-string-system-iformatprovider-system-guid@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact?view=net-10.0#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@))


#### IntPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(string?, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-string-system-iformatprovider-system-int32@))


#### LongPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(string?, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-string-system-iformatprovider-system-int64@))


#### MathPolyfill

 * `byte Clamp(byte, byte, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `decimal Clamp(decimal, decimal, decimal)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `double Clamp(double, double, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `float Clamp(float, float, float)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `int Clamp(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `long Clamp(long, long, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `nint Clamp(nint, nint, nint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `nuint Clamp(nuint, nuint, nuint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `sbyte Clamp(sbyte, sbyte, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `short Clamp(short, short, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `uint Clamp(uint, uint, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `ulong Clamp(ulong, ulong, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `ushort Clamp(ushort, ushort, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)


#### OperatingSystemPolyfill

 * `bool IsAndroid()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroid?view=net-10.0)
 * `bool IsAndroidVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroidversionatleast?view=net-10.0)
 * `bool IsBrowser()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isbrowser?view=net-10.0)
 * `bool IsFreeBSD()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsd?view=net-10.0)
 * `bool IsFreeBSDVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsdversionatleast?view=net-10.0)
 * `bool IsIOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isios?view=net-10.0)
 * `bool IsIOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isiosversionatleast?view=net-10.0)
 * `bool IsLinux()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.islinux?view=net-10.0)
 * `bool IsMacCatalyst()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalyst?view=net-10.0)
 * `bool IsMacCatalystVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalystversionatleast?view=net-10.0)
 * `bool IsMacOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacos?view=net-10.0)
 * `bool IsMacOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacosversionatleast?view=net-10.0)
 * `bool IsOSPlatform(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatform?view=net-10.0)
 * `bool IsOSPlatformVersionAtLeast(string, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatformversionatleast?view=net-10.0)
 * `bool IsTvOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvos?view=net-10.0)
 * `bool IsTvOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvosversionatleast?view=net-10.0)
 * `bool IsWasi()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswasi?view=net-10.0)
 * `bool IsWatchOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchos?view=net-10.0)
 * `bool IsWatchOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchosversionatleast?view=net-10.0)
 * `bool IsWindows()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindows?view=net-10.0)
 * `bool IsWindowsVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindowsversionatleast?view=net-10.0)


#### PathPolyfill

 * `string Combine(ReadOnlySpan<string>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-10.0#system-io-path-combine(system-readonlyspan((system-string))))
 * `bool EndsInDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-10.0#system-io-path-endsindirectoryseparator(system-readonlyspan((system-char))))
 * `bool EndsInDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-10.0#system-io-path-endsindirectoryseparator(system-string))
 * `bool Exists(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.exists?view=net-10.0)
 * `ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname?view=net-10.0#system-io-path-getdirectoryname(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-10.0#system-io-path-getextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilename?view=net-10.0#system-io-path-getfilename(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-10.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `bool HasExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-10.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-10.0#system-io-path-trimendingdirectoryseparator(system-readonlyspan((system-char))))
 * `string TrimEndingDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-10.0#system-io-path-trimendingdirectoryseparator(system-string))


#### RandomPolyfill



#### RegexPolyfill

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `bool IsMatch(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string))


#### SBytePolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@))
 * `bool TryParse(string?, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@))


#### SHA256Polyfill

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0#system-security-cryptography-sha256-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-10.0?system-security-cryptography-sha256-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-10.0?system-security-cryptography-sha256-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.tryhashdata?view=net-10.0)


#### SHA512Polyfill

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0#system-security-cryptography-sha512-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.tryhashdata?view=net-10.0)


#### ShortPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@))
 * `bool TryParse(string?, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-string-system-iformatprovider-system-int16@))


#### StringPolyfill

 * `string Join(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-object()))
 * `string Join(char, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-readonlyspan((system-object))))
 * `string Join(char, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-readonlyspan((system-string))))
 * `string Join(char, string?[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-string()-system-int32-system-int32))
 * `string Join(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-string()))
 * `string Join(string?, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-string-system-readonlyspan((system-object))))
 * `string Join(string?, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-string-system-readonlyspan((system-string))))
 * `string Join<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join-1(system-char-system-collections-generic-ienumerable((-0))))


#### UIntPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@))
 * `bool TryParse(string?, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@))


#### ULongPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@))
 * `bool TryParse(string?, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@))


#### UShortPolyfill

 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@))
 * `bool TryParse(string?, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@))


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

 * `KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create?view=net-10.0)


#### TaskCompletionSource

#### UnreachableException
<!-- endInclude -->


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

## NullabilityInfo

Enable by adding an MSBuild property `PolyNullability`

```
<PropertyGroup>
  ...
  <PolyNullability>true</PolyNullability>
</PropertyGroup>
```


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
