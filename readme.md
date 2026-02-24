# <img src="/src/icon.png" height="30px" width="30px"> Polyfill

[![Build status](https://img.shields.io/appveyor/build/SimonCropp/Polyfill)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`
 * `uap10`


**API count: 736**<!-- singleLineInclude: apiCount. path: /apiCount.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**


### Powered by

[![JetBrains logo.](https://resources.jetbrains.com/storage/products/company/brand/logos/jetbrains.svg)](https://jb.gg/OpenSourceSupport)


## Nuget

 * https://nuget.org/packages/Polyfill/
 * https://nuget.org/packages/PolyfillLib/ (See [PolyfillLib](polyfill-lib.md))


## TargetFrameworks

It is recommended that projects that consume Polyfill multi-target all TFMs that the project is expected to be consumed in. See [Polyfill and TargetFrameworks](target-frameworks.md)


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
    "version": "10.0.100",
    "rollForward": "latestFeature"
  }
}
```


## Assembly size impact


### Assembly Sizes<!-- include: assemblySize. path: /assemblySize.include.md -->

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       225.0KB |  +217.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       177.0KB |  +168.5KB |   +12.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       231.5KB |  +223.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       230.0KB |  +223.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       230.0KB |  +223.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       230.0KB |  +221.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       228.5KB |  +220.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       228.5KB |  +220.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       228.5KB |  +220.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       208.5KB |  +199.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       187.0KB |  +178.0KB |   +12.0KB |            +10.0KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       187.0KB |  +178.0KB |   +12.0KB |            +10.0KB |             +12.5KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       183.5KB |  +174.0KB |   +12.5KB |            +10.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       182.0KB |  +172.5KB |    +9.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       152.0KB |  +142.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       112.5KB |  +102.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        85.0KB |   +75.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        68.5KB |   +59.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        34.5KB |   +24.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       340.4KB |  +332.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netstandard2.1 |          8.5KB |       267.8KB |  +259.3KB |   +19.9KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net461         |          8.5KB |       347.4KB |  +338.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net462         |          7.0KB |       345.9KB |  +338.9KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| net47          |          7.0KB |       345.6KB |  +338.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       345.7KB |  +337.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       343.1KB |  +334.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net48          |          8.5KB |       343.1KB |  +334.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net481         |          8.5KB |       343.1KB |  +334.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       314.8KB |  +305.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.1  |          9.0KB |       282.9KB |  +273.9KB |   +19.9KB |            +11.7KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       282.9KB |  +273.9KB |   +19.9KB |            +11.7KB |             +17.5KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       270.5KB |  +261.0KB |   +20.4KB |            +11.7KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       268.9KB |  +259.4KB |   +16.9KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net5.0         |          9.5KB |       222.7KB |  +213.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       168.3KB |  +158.3KB |   +17.4KB |             +8.7KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       124.3KB |  +114.3KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        98.7KB |   +89.2KB |   +16.1KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        50.9KB |   +40.9KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        34.1KB |   +24.1KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.7KB |   +18.7KB |   +16.6KB |                    |              +1.6KB |      +4.7KB |
<!-- endInclude -->


## PolyfillLib

To consume Polyfill as a library (instead of a source-only package) see [PolyfillLib](polyfill-lib.md)


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
public async Task ModuleInitTest() =>
    await Assert.That(InitCalled).IsTrue();

[ModuleInitializer]
public static void ModuleInit() =>
    InitCalled = true;
```
<sup><a href='/src/Tests/ModuleInitSample.cs#L3-L15' title='Snippet source file'>snippet source</a> | <a href='#snippet-ModuleInitializerAttribute' title='Start of snippet'>anchor</a></sup>
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
class IndexRangeSample
{
    [Test]
    public async Task Range()
    {
        var substring = "value"[2..];
        await Assert.That(substring).IsEqualTo("lue");
    }

    [Test]
    public async Task Index()
    {
        var ch = "value"[^2];
        await Assert.That(ch).IsEqualTo('u');
    }

    [Test]
    public async Task ArrayIndex()
    {
        var array = new[]
        {
            "value1",
            "value2"
        };

        var value = array[^2];

        await Assert.That(value).IsEqualTo("value1");
    }
}
```
<sup><a href='/src/Tests/IndexRangeSample.cs#L1-L34' title='Snippet source file'>snippet source</a> | <a href='#snippet-IndexRange' title='Start of snippet'>anchor</a></sup>
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
public class OverloadResolutionPriorityAttributeTests
{
    [Test]
    public Task Run()
    {
        int[] arr = [1, 2, 3];
        //Prints "Span" because resolution priority is higher
        Method(arr);
        return Task.CompletedTask;
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
<sup><a href='/src/Consume/UnscopedRefUsage.cs#L7-L14' title='Snippet source file'>snippet source</a> | <a href='#snippet-UnscopedRefUsage' title='Start of snippet'>anchor</a></sup>
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
    public static void FileExists(string path, [CallerArgumentExpression("path")] string name = "")
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"File not found. Path: {path}", name);
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

#### ArgumentException

 * `void ThrowIfNullOrEmpty(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorempty?view=net-11.0#system-argumentexception-throwifnullorempty(system-string-system-string))
 * `void ThrowIfNullOrWhiteSpace(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorwhitespace?view=net-11.0#system-argumentexception-throwifnullorwhitespace(system-string-system-string))


#### ArgumentNullException

 * `void ThrowIfNull(object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-11.0#system-argumentnullexception-throwifnull(system-object-system-string))
 * `void ThrowIfNull(void*)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-11.0#system-argumentnullexception-throwifnull(system-void*-system-string))


#### ArgumentOutOfRangeException

 * `void ThrowIfEqual<T>(T, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifequal?view=net-11.0#system-argumentoutofrangeexception-throwifequal-1(-0-0-system-string))
 * `void ThrowIfGreaterThan(nint, nint)`
 * `void ThrowIfGreaterThan<T>(T, T) where T : IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifgreaterthan?view=net-11.0#system-argumentoutofrangeexception-throwifgreaterthan-1(-0-0-system-string))
 * `void ThrowIfGreaterThanOrEqual(nint, nint)`
 * `void ThrowIfGreaterThanOrEqual<T>(T, T) where T : IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifgreaterthanorequal?view=net-11.0#system-argumentoutofrangeexception-throwifgreaterthanorequal-1(-0-0-system-string))
 * `void ThrowIfLessThan(nint, nint)`
 * `void ThrowIfLessThan<T>(T, T) where T : IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwiflessthan?view=net-11.0#system-argumentoutofrangeexception-throwiflessthan-1(-0-0-system-string))
 * `void ThrowIfLessThanOrEqual(nint, nint)`
 * `void ThrowIfLessThanOrEqual<T>(T, T) where T : IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwiflessthanorequal?view=net-11.0#system-argumentoutofrangeexception-throwiflessthanorequal-1(-0-0-system-string))
 * `void ThrowIfNegative(nint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegative?view=net-11.0#system-argumentoutofrangeexception-throwifnegative-1(-0-system-string))
 * `void ThrowIfNegative<T>(T) where T : struct, IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegative?view=net-11.0#system-argumentoutofrangeexception-throwifnegative-1(-0-system-string))
 * `void ThrowIfNegativeOrZero(nint)`
 * `void ThrowIfNegativeOrZero<T>(T) where T : struct, IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnegativeorzero?view=net-11.0#system-argumentoutofrangeexception-throwifnegativeorzero-1(-0-system-string))
 * `void ThrowIfNotEqual<T>(T, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifnotequal?view=net-11.0#system-argumentoutofrangeexception-throwifnotequal-1(-0-0-system-string))
 * `void ThrowIfZero(nint)`
 * `void ThrowIfZero<T>(T) where T : struct, IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.throwifzero?view=net-11.0#system-argumentoutofrangeexception-throwifzero-1(-0-system-string))


#### ArraySegment<T>

 * `void CopyTo<T>(ArraySegment<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0()))
 * `void CopyTo<T>(T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0()-system-int32))
 * `void CopyTo<T>(T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0()))
 * `ArraySegmentEnumerator<T> GetEnumerator<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.getenumerator?view=net-11.0)


#### Boolean

 * `bool TryFormat(Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat?view=net-11.0)


#### Byte

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-11.0#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-11.0#system-byte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(string?, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-string-system-iformatprovider-system-byte@))


#### CancellationToken

 * `CancellationTokenRegistration Register(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register?view=net-11.0#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-11.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<object?>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-11.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-11.0)


#### ConcurrentBag<T>

 * `void Clear<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear?view=net-11.0)


#### ConcurrentDictionary<TKey, TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-11.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### ConcurrentQueue<T>

 * `void Clear<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear?view=net-11.0)


#### Console

 * `SafeFileHandle OpenStandardErrorHandle()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.console.openstandarderrorhandle?view=net-11.0)
 * `SafeFileHandle OpenStandardInputHandle()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.console.openstandardinputhandle?view=net-11.0)
 * `SafeFileHandle OpenStandardOutputHandle()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.console.openstandardoutputhandle?view=net-11.0)


#### Convert

 * `byte[] FromHexString(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-11.0#system-convert-fromhexstring(system-readonlyspan((system-char))))
 * `byte[] FromHexString(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-11.0#system-convert-fromhexstring(system-string))
 * `string ToHexString(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-byte()-system-int32-system-int32))
 * `string ToHexString(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-byte()))
 * `string ToHexString(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-readonlyspan((system-byte))))
 * `string ToHexStringLower(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-byte()-system-int32-system-int32))
 * `string ToHexStringLower(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-byte()))
 * `string ToHexStringLower(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-readonlyspan((system-byte))))
 * `bool TryToHexString(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstring?view=net-11.0)
 * `bool TryToHexStringLower(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstringlower?view=net-11.0)


#### DateOnly

 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.deconstruct?view=net-11.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-11.0#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-11.0#system-dateonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTime

 * `DateTime AddMicroseconds(double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-11.0)
 * `void Deconstruct(DateOnly, TimeOnly)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-11.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@))
 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-11.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@))
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-11.0#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-11.0#system-datetime-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<char>, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParse(string?, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-11.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-11.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `Microsecond` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-11.0)
 * `Nanosecond` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-11.0)


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-11.0)
 * `void Deconstruct(DateOnly, TimeOnly, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-11.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-11.0#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-11.0#system-datetimeoffset-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<char>, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParse(string?, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-11.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-11.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `Microsecond` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-11.0)
 * `Nanosecond` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-11.0)


#### Decimal

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-11.0#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-11.0#system-decimal-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DefaultInterpolatedStringHandler

 * `void Clear()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear?view=net-11.0)


#### Delegate

 * `InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate?) where TDelegate : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-11.0)
 * `HasSingleTarget` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget?view=net-11.0)


#### Dictionary<TKey, TValue>

 * `void EnsureCapacity<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-11.0)
 * `void TrimExcess<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-11.0#system-collections-generic-dictionary-2-trimexcess(system-int32))
 * `void TrimExcess<TKey, TValue>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-11.0)


#### DictionaryEntry

 * `void Deconstruct(object, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct?view=net-11.0#system-collections-dictionaryentry-deconstruct(system-object@-system-object@))


#### DirectoryInfo

 * `IEnumerable<DirectoryInfo> EnumerateDirectories(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratedirectories?view=net-11.0#system-io-directoryinfo-enumeratedirectories(system-string-system-io-enumerationoptions))
 * `IEnumerable<FileInfo> EnumerateFiles(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratefiles?view=net-11.0#system-io-directoryinfo-enumeratefiles(system-string-system-io-enumerationoptions))
 * `IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratefilesysteminfos?view=net-11.0#system-io-directoryinfo-enumeratefilesysteminfos(system-string-system-io-enumerationoptions))
 * `DirectoryInfo[] GetDirectories(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getdirectories?view=net-11.0#system-io-directoryinfo-getdirectories(system-string-system-io-enumerationoptions))
 * `FileInfo[] GetFiles(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfiles?view=net-11.0#system-io-directoryinfo-getfiles(system-string-system-io-enumerationoptions))
 * `FileSystemInfo[] GetFileSystemInfos(string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfilesysteminfos?view=net-11.0#system-io-directoryinfo-getfilesysteminfos(system-string-system-io-enumerationoptions))


#### Double

 * `ulong DoubleToUInt64Bits(double)`
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-11.0#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-11.0#system-double-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-byte))-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-char))-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(string?, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-11.0#system-double-tryparse(system-string-system-iformatprovider-system-double@))


#### Encoding

 * `int GetByteCount(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount?view=net-11.0#system-text-encoding-getbytecount(system-readonlyspan((system-char))))
 * `int GetBytes(ReadOnlySpan<char>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-11.0#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte))))
 * `int GetCharCount(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount?view=net-11.0#system-text-encoding-getcharcount(system-readonlyspan((system-byte))))
 * `int GetChars(ReadOnlySpan<byte>, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars?view=net-11.0#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char))))
 * `string GetString(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring?view=net-11.0#system-text-encoding-getstring(system-readonlyspan((system-byte))))
 * `bool TryGetBytes(ReadOnlySpan<char>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetbytes?view=net-11.0)
 * `bool TryGetChars(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetchars?view=net-11.0)


#### Enum

 * `string[] GetNames<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames?view=net-11.0)
 * `TEnum[] GetValues<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues?view=net-11.0)
 * `Array GetValuesAsUnderlyingType(Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvaluesasunderlyingtype?view=net-11.0#system-enum-getvaluesasunderlyingtype(system-type))
 * `Array GetValuesAsUnderlyingType<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvaluesasunderlyingtype?view=net-11.0#system-enum-getvaluesasunderlyingtype-1)
 * `bool IsDefined<TEnum>(TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined?view=net-11.0#system-enum-isdefined-1(-0))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-readonlyspan((system-char))))
 * `TEnum Parse<TEnum>(string, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(string) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-string-system-boolean))
 * `bool TryFormat<TEnum>(TEnum, Span<char>, int, ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryformat?view=net-11.0)
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, bool, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-11.0#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@))
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-11.0#system-enum-tryparse-1(system-readonlyspan((system-char))-0@))


#### Enumerable

 * `IEnumerable<T> InfiniteSequence<T>(T, T) where T : IAdditionOperators<T, T, T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.infinitesequence?view=net-11.0)
 * `IEnumerable<T> Sequence<T>(T, T, T) where T : INumber<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sequence?view=net-11.0)


#### Environment

 * `ProcessId` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.environment.processid?view=net-11.0#system-environment-processid)


#### EventInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### FieldInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### File

 * `void AppendAllBytes(string, byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-11.0#system-io-file-appendallbytes(system-string-system-byte()))
 * `void AppendAllBytes(string, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-11.0#system-io-file-appendallbytes(system-string-system-readonlyspan((system-byte))))
 * `Task AppendAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-11.0#system-io-file-appendallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task AppendAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-11.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-11.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-11.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void AppendAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-11.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void AppendAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-11.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-11.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-11.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-text-encoding-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-11.0#system-io-file-appendalltextasync(system-string-system-string-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-11.0#system-io-file-appendalltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `FileSystemInfo CreateHardLink(string, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.createhardlink?view=net-11.0)
 * `void Move(string, string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.move?view=net-11.0#system-io-file-move(system-string-system-string-system-boolean))
 * `SafeFileHandle OpenNullHandle()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.opennullhandle?view=net-11.0)
 * `Task<byte[]> ReadAllBytesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readallbytesasync?view=net-11.0)
 * `Task<string[]> ReadAllLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-11.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `Task<string[]> ReadAllLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-11.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-11.0#system-io-file-readalltextasync(system-string-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-11.0#system-io-file-readalltextasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-11.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-11.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writeallbytesasync?view=net-11.0#system-io-file-writeallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-11.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-11.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-11.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void WriteAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-11.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void WriteAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-11.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))))
 * `Task WriteAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-11.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-11.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))


#### FileInfo

 * `void CreateAsHardLink(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo.createashardlink?view=net-11.0)


#### FileUnixMode

 * `UnixFileMode GetUnixFileMode(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.getunixfilemode?view=net-11.0#system-io-file-getunixfilemode(system-string))
 * `void SetUnixFileMode(string, UnixFileMode)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.setunixfilemode?view=net-11.0#system-io-file-setunixfilemode(system-string-system-io-unixfilemode))


#### Guid

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-11.0#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-11.0#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))
 * `Guid CreateVersion7()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-11.0#system-guid-createversion7)
 * `Guid CreateVersion7(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-11.0#system-guid-createversion7(system-datetimeoffset))
 * `Guid Parse(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.parse?view=net-11.0#system-guid-parse(system-readonlyspan((system-byte))))
 * `bool TryParse(ReadOnlySpan<byte>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-byte))-system-guid@))
 * `bool TryParse(ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@))
 * `bool TryParse(string?, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-string-system-iformatprovider-system-guid@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact?view=net-11.0#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@))


#### HashSet<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity?view=net-11.0#system-collections-generic-hashset-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?view=net-11.0#system-collections-generic-hashset-1-trimexcess(system-int32))
 * `bool TryGetValue<T>(T, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue?view=net-11.0)


#### HttpClient

 * `Task<byte[]> GetByteArrayAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-11.0#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-11.0#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-11.0#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-11.0#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-11.0#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-11.0#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<byte[]> ReadAsByteArrayAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync?view=net-11.0#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<Stream> ReadAsStreamAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync?view=net-11.0#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<string> ReadAsStringAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-11.0#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>() where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))
 * `bool Remove<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove?view=net-11.0)
 * `bool TryAdd<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd?view=net-11.0)


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-11.0#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-11.0#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-11.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-11.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> Append<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append?view=net-11.0)
 * `IEnumerable<TSource[]> Chunk<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk?view=net-11.0)
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby?view=net-11.0)
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-11.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-11.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource ElementAt<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-11.0#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource? ElementAtOrDefault<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault?view=net-11.0#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))
 * `IEnumerable<TSource> Except<TSource>(IEqualityComparer<TSource>?, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-11.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-11.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `TSource FirstOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-11.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-11.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<(int Index, TSource Item)> Index<TSource>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index?view=net-11.0#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))
 * `TSource LastOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-11.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource LastOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-11.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource? Max<TSource>(IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-11.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MaxBy<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-11.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? MaxBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-11.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? Min<TSource>(IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-11.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MinBy<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-11.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? MinBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-11.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource SingleOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-11.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-11.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<TSource> SkipLast<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast?view=net-11.0)
 * `IEnumerable<TSource> Take<TSource>(Range)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-11.0#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range))
 * `IEnumerable<TSource> TakeLast<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast?view=net-11.0)
 * `HashSet<TSource> ToHashSet<TSource>(IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-11.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool TryGetNonEnumeratedCount<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount?view=net-11.0)
 * `IEnumerable<TSource> UnionBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/de-de/dotnet/api/system.linq.enumerable.unionby?view=net-11.0#system-linq-enumerable-unionby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> UnionBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/de-de/dotnet/api/system.linq.enumerable.unionby?view=net-11.0#system-linq-enumerable-unionby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-func((-0-1))))


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))


#### Int16

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-11.0#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-11.0#system-int16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@))
 * `bool TryParse(string?, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-11.0#system-int16-tryparse(system-string-system-iformatprovider-system-int16@))


#### Int32

 * `float Int32BitsToSingle(int)`
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-11.0#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-11.0#system-int32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(string?, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-11.0#system-int32-tryparse(system-string-system-iformatprovider-system-int32@))


#### Int64

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-11.0#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-11.0#system-int64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(string?, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-11.0#system-int64-tryparse(system-string-system-iformatprovider-system-int64@))


#### Interlocked



#### IReadOnlyDictionary<TKey, TValue>

 * `TValue GetValueOrDefault<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-11.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))
 * `TValue? GetValueOrDefault<TKey, TValue>(TKey)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-11.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0))


#### ISet<T>

 * `ReadOnlySet<T> AsReadOnly<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0))))


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct?view=net-11.0)


#### List<T>

 * `void AddRange<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange?view=net-11.0)
 * `void CopyTo<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-11.0)
 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-11.0#system-collections-generic-list-1-ensurecapacity(system-int32))
 * `void InsertRange<T>(int, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-11.0)
 * `void TrimExcess<T>()`


#### Math

 * `byte Clamp(byte, byte, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `decimal Clamp(decimal, decimal, decimal)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `double Clamp(double, double, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `float Clamp(float, float, float)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `int Clamp(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `long Clamp(long, long, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `nint Clamp(nint, nint, nint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `nuint Clamp(nuint, nuint, nuint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `sbyte Clamp(sbyte, sbyte, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `short Clamp(short, short, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `uint Clamp(uint, uint, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `ulong Clamp(ulong, ulong, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)
 * `ushort Clamp(ushort, ushort, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-11.0)


#### MD5

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdata?view=net-11.0#system-security-cryptography-md5-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdata?view=net-11.0#system-security-cryptography-md5-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdata?view=net-11.0#system-security-cryptography-md5-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdata?view=net-11.0#system-security-cryptography-md5-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdata?view=net-11.0#system-security-cryptography-md5-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdataasync?view=net-11.0#system-security-cryptography-md5-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.hashdataasync?view=net-11.0#system-security-cryptography-md5-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5.tryhashdata?view=net-11.0)


#### MemberInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool HasSameMetadataDefinitionAs(MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas?view=net-11.0)
 * `bool IsNullable()`


#### MethodInfo

 * `T CreateDelegate<T>(object?) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-11.0#system-reflection-methodinfo-createdelegate-1(system-object))
 * `T CreateDelegate<T>() where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-11.0#system-reflection-methodinfo-createdelegate-1)


#### ObjectDisposedException

 * `void ThrowIf(bool, object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.objectdisposedexception.throwif?view=net-11.0##system-objectdisposedexception-throwif(system-boolean-system-object))
 * `void ThrowIf(bool, Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.objectdisposedexception.throwif?view=net-11.0##system-objectdisposedexception-throwif(system-boolean-system-type))


#### OperatingSystem

 * `bool IsAndroid()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroid?view=net-11.0)
 * `bool IsAndroidVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroidversionatleast?view=net-11.0)
 * `bool IsBrowser()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isbrowser?view=net-11.0)
 * `bool IsFreeBSD()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsd?view=net-11.0)
 * `bool IsFreeBSDVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsdversionatleast?view=net-11.0)
 * `bool IsIOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isios?view=net-11.0)
 * `bool IsIOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isiosversionatleast?view=net-11.0)
 * `bool IsLinux()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.islinux?view=net-11.0)
 * `bool IsMacCatalyst()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalyst?view=net-11.0)
 * `bool IsMacCatalystVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalystversionatleast?view=net-11.0)
 * `bool IsMacOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacos?view=net-11.0)
 * `bool IsMacOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacosversionatleast?view=net-11.0)
 * `bool IsOSPlatform(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatform?view=net-11.0)
 * `bool IsOSPlatformVersionAtLeast(string, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatformversionatleast?view=net-11.0)
 * `bool IsTvOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvos?view=net-11.0)
 * `bool IsTvOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvosversionatleast?view=net-11.0)
 * `bool IsWasi()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswasi?view=net-11.0)
 * `bool IsWatchOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchos?view=net-11.0)
 * `bool IsWatchOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchosversionatleast?view=net-11.0)
 * `bool IsWindows()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindows?view=net-11.0)
 * `bool IsWindowsVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindowsversionatleast?view=net-11.0)


#### OrderedDictionary<TKey, TValue>

 * `bool TryAdd<TKey, TValue>(TKey, TValue, int) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ordereddictionary-2.tryadd?view=net-11.0#system-collections-generic-ordereddictionary-2-tryadd(-0-1-system-int32@))
 * `bool TryGetValue<TKey, TValue>(TKey, TValue, int) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ordereddictionary-2.trygetvalue?view=net-11.0#system-collections-generic-ordereddictionary-2-trygetvalue(-0-1@-system-int32@))


#### ParameterInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### Path

 * `string Combine(ReadOnlySpan<string>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-11.0#system-io-path-combine(system-readonlyspan((system-string))))
 * `bool EndsInDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-11.0#system-io-path-endsindirectoryseparator(system-readonlyspan((system-char))))
 * `bool EndsInDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-11.0#system-io-path-endsindirectoryseparator(system-string))
 * `bool Exists(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.exists?view=net-11.0)
 * `ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname?view=net-11.0#system-io-path-getdirectoryname(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-11.0#system-io-path-getextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilename?view=net-11.0#system-io-path-getfilename(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-11.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `bool HasExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-11.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-11.0#system-io-path-trimendingdirectoryseparator(system-readonlyspan((system-char))))
 * `string TrimEndingDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-11.0#system-io-path-trimendingdirectoryseparator(system-string))


#### Process

 * `void Kill(bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?view=net-11.0#system-diagnostics-process-kill(system-boolean))
 * `Task WaitForExitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-11.0)


#### PropertyInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### Queue<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity?view=net-11.0#system-collections-generic-queue-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-11.0#system-collections-generic-queue-1-trimexcess(system-int32))


#### Random

 * `T[] GetItems<T>(ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(system-readonlyspan((-0))-system-int32))
 * `void GetItems<T>(ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `T[] GetItems<T>(T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-11.0#system-random-getitems-1(-0()-system-int32))
 * `void NextBytes(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-11.0#system-random-nextbytes(system-span((system-byte))))
 * `Shared` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.shared?view=net-11.0)


#### RandomNumberGenerator

 * `void Fill(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.fill?view=net-11.0)
 * `byte[] GetBytes(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getbytes?view=net-11.0#system-security-cryptography-randomnumbergenerator-getbytes(system-int32))
 * `string GetHexString(int, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.gethexstring?view=net-11.0#system-security-cryptography-randomnumbergenerator-gethexstring(system-int32-system-boolean))
 * `void GetHexString(Span<char>, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-11.0)
 * `int GetInt32(int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-11.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32))
 * `int GetInt32(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-11.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32))
 * `T[] GetItems<T>(ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-11.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-int32))
 * `void GetItems<T>(ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-11.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `string GetString(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-11.0)
 * `void Shuffle<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.shuffle?view=net-11.0)


#### ReadOnlySpan<char>

 * `bool EndsWith(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-11.0#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `int GetNormalizedLength(NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength?view=net-11.0#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool IsNormalized(NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized?view=net-11.0#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool SequenceEqual(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-11.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-11.0#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool TryNormalize(Span<char>, int, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize?view=net-11.0#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform))


#### ReadOnlySpan<T>

 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool Contains<T>(T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `bool Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `int CountAny<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CountAny<T>(SearchValues<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool EndsWith<T>(T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `int IndexOf<T>(T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.indexof?view=net-11.0#system-memoryextensions-indexof-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `int IndexOfAny<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> Split<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool StartsWith<T>(T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-int32-system-int32))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-int32))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))))
 * `bool IsMatch(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-11.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratesplits?view=net-11.0#system-text-regularexpressions-regex-enumeratesplits(system-readonlyspan((system-char))-system-string))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `bool IsMatch(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-11.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string))


#### SByte

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-11.0#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-11.0#system-sbyte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@))
 * `bool TryParse(string?, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-11.0#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@))


#### SHA1

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdata?view=net-11.0#system-security-cryptography-sha1-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdata?view=net-11.0#system-security-cryptography-sha1-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdata?view=net-11.0#system-security-cryptography-sha1-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdata?view=net-11.0#system-security-cryptography-sha1-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdata?view=net-11.0#system-security-cryptography-sha1-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdataasync?view=net-11.0#system-security-cryptography-sha1-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.hashdataasync?view=net-11.0#system-security-cryptography-sha1-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha1.tryhashdata?view=net-11.0)


#### SHA256

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-11.0#system-security-cryptography-sha256-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-11.0#system-security-cryptography-sha256-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.tryhashdata?view=net-11.0)


#### SHA384

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdata?view=net-11.0#system-security-cryptography-sha384-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdata?view=net-11.0#system-security-cryptography-sha384-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdata?view=net-11.0#system-security-cryptography-sha384-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdata?view=net-11.0#system-security-cryptography-sha384-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdata?view=net-11.0#system-security-cryptography-sha384-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdataasync?view=net-11.0#system-security-cryptography-sha384-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.hashdataasync?view=net-11.0#system-security-cryptography-sha384-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha384.tryhashdata?view=net-11.0)


#### SHA512

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-11.0#system-security-cryptography-sha512-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-11.0#system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-11.0#system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-11.0#system-security-cryptography-sha512-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-11.0#system-security-cryptography-sha512-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-11.0#system-security-cryptography-sha512-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-11.0#system-security-cryptography-sha512-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.tryhashdata?view=net-11.0)


#### Single

 * `int SingleToInt32Bits(float)`
 * `uint SingleToUInt32Bits(float)`
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-11.0#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-11.0#system-single-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex?view=net-11.0)
 * `TValue GetValueAtIndex<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex?view=net-11.0)


#### Span<char>

 * `bool EndsWith(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-11.0#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `bool SequenceEqual(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-11.0#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-11.0#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Span<char> TrimEnd()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend?view=net-11.0#system-memoryextensions-trimend(system-span((system-char))))
 * `Span<char> TrimStart()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart?view=net-11.0#system-memoryextensions-trimstart(system-span((system-char))))


#### Span<T>

 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-span((-0))-0))
 * `void Sort<T>(Comparison<T>)`
 * `void Sort<T>() where T : IComparable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-1(system-span((-0))))


#### Span<TKey>

 * `void Sort<TKey, TValue, TComparer>(Span<TValue>, TComparer) where TComparer : IComparer<TKey>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-3(system-span((-0))-system-span((-1))-2))
 * `void Sort<TKey, TValue>(Span<TValue>, Comparison<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-2(system-span((-0))-system-span((-1))-system-comparison((-0))))
 * `void Sort<TKey, TValue>(Span<TValue>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-2(system-span((-0))-system-span((-1))))


#### Stack<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity?view=net-11.0)
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess?view=net-11.0#system-collections-generic-stack-1-trimexcess(system-int32))
 * `bool TryPeek<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek?view=net-11.0)
 * `bool TryPop<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop?view=net-11.0)


#### Stream

 * `Task CopyToAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync?view=net-11.0#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask DisposeAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync?view=net-11.0)
 * `int Read(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.read?view=net-11.0#system-io-stream-read(system-span((system-byte))))
 * `ValueTask<int> ReadAsync(Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-11.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `int ReadAtLeast(Span<byte>, int, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleast?view=net-11.0)
 * `ValueTask<int> ReadAtLeastAsync(Memory<byte>, int, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-11.0)
 * `void ReadExactly(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-11.0#system-io-stream-readexactly(system-byte()-system-int32-system-int32))
 * `void ReadExactly(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-11.0#system-io-stream-readexactly(system-span((system-byte))))
 * `ValueTask ReadExactlyAsync(byte[], int, int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactlyasync?view=net-11.0#system-io-stream-readexactlyasync(system-byte()-system-int32-system-int32-system-threading-cancellationtoken))
 * `ValueTask ReadExactlyAsync(Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-11.0)
 * `ValueTask WriteAsync(ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync?view=net-11.0#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### String

 * `bool Contains(char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-char-system-stringcomparison))
 * `bool Contains(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-char))
 * `bool Contains(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-string-system-stringcomparison))
 * `void CopyTo(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-11.0)
 * `bool EndsWith(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-11.0#system-string-endswith(system-char))
 * `IEnumerable<string> EnumerateDirectories(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories?view=net-11.0#system-io-directory-enumeratedirectories(system-string-system-string-system-io-enumerationoptions))
 * `IEnumerable<string> EnumerateFiles(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-11.0#system-io-directory-enumeratefiles(system-string-system-string-system-io-enumerationoptions))
 * `IEnumerable<string> EnumerateFileSystemEntries(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefilesystementries?view=net-11.0#system-io-directory-enumeratefilesystementries(system-string-system-string-system-io-enumerationoptions))
 * `string[] GetDirectories(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=net-11.0#system-io-directory-getdirectories(system-string-system-string-system-io-enumerationoptions))
 * `string[] GetFiles(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-11.0#system-io-directory-getfiles(system-string-system-string-system-io-enumerationoptions))
 * `string[] GetFileSystemEntries(string, string, EnumerationOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getfilesystementries?view=net-11.0#system-io-directory-getfilesystementries(system-string-system-string-system-io-enumerationoptions))
 * `int GetHashCode(StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-stringcomparison))
 * `int IndexOf(char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-char-system-stringcomparison))
 * `string ReplaceLineEndings(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-11.0#system-string-replacelineendings(system-string))
 * `string ReplaceLineEndings()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-11.0#system-string-replacelineendings)
 * `string[] Split(char, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `string[] Split(char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-char-system-stringsplitoptions))
 * `string[] Split(string, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-string-system-int32-system-stringsplitoptions))
 * `string[] Split(string, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-string-system-stringsplitoptions))
 * `bool StartsWith(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-11.0#system-string-startswith(system-char))
 * `bool TryCopyTo(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-11.0)
 * `string Create<TState>(int, TState, System.Buffers.SpanAction<char, TState>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.create?view=net-11.0#system-string-create-1(system-int32-0-system-buffers-spanaction((system-char-0))))
 * `int GetHashCode(ReadOnlySpan<char>, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-readonlyspan((system-char))-system-stringcomparison))
 * `int GetHashCode(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-readonlyspan((system-char))))
 * `string Join(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-object()))
 * `string Join(char, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-readonlyspan((system-object))))
 * `string Join(char, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-readonlyspan((system-string))))
 * `string Join(char, string?[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-string()-system-int32-system-int32))
 * `string Join(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-string()))
 * `string Join(string?, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-string-system-readonlyspan((system-object))))
 * `string Join(string?, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-string-system-readonlyspan((system-string))))
 * `string Join<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join-1(system-char-system-collections-generic-ienumerable((-0))))


#### StringBuilder

 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(StringBuilder?, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32))
 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(string?, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(string?, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(string, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(string?, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `void CopyTo(int, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto?view=net-11.0#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `bool Equals(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals?view=net-11.0#system-text-stringbuilder-equals(system-readonlyspan((system-char))))
 * `ChunkEnumerator GetChunks()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks?view=net-11.0)
 * `StringBuilder Insert(int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-11.0#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char))))
 * `StringBuilder Replace(ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))
 * `StringBuilder Replace(ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-11.0#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))


#### Task

 * `Task WaitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-timespan))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-11.0#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-11.0#system-threading-tasks-task-1-waitasync(system-timespan))


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-11.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TaskWhenEach

 * `IAsyncEnumerable<Task> WhenEach(IEnumerable<Task>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wheneach?view=net-11.0)
 * `IAsyncEnumerable<Task<TResult>> WhenEach<TResult>(IEnumerable<Task<TResult>>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wheneach?view=net-11.0)


#### TcpClient

 * `ValueTask ConnectAsync(IPAddress, int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipaddress-system-int32-system-threading-cancellationtoken))
 * `ValueTask ConnectAsync(IPAddress[], int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipaddress()-system-int32-system-threading-cancellationtoken))
 * `ValueTask ConnectAsync(IPEndPoint, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipendpoint-system-threading-cancellationtoken))
 * `ValueTask ConnectAsync(string, int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-string-system-int32-system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<int> ReadAsync(Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync?view=net-11.0#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<string> ReadLineAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-11.0#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<string> ReadToEndAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-11.0#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `Task FlushAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-11.0#system-io-textwriter-flushasync(system-threading-cancellationtoken))
 * `void Write(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `void Write(StringBuilder?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-text-stringbuilder))
 * `ValueTask WriteAsync(ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task WriteAsync(string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-string-system-threading-cancellationtoken))
 * `Task WriteAsync(StringBuilder?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void WriteLine(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-11.0#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `Task WriteLineAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-threading-cancellationtoken))
 * `ValueTask WriteLineAsync(ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task WriteLineAsync(string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-string-system-threading-cancellationtoken))


#### TimeOnly

 * `void Deconstruct(int, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-11.0#system-timeonly-deconstruct(system-int32@-system-int32@))
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-11.0#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-11.0#system-timeonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TimeSpan

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-11.0#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-11.0#system-timespan-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `Microseconds` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-11.0)
 * `Nanoseconds` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-11.0)


#### Type

 * `MemberInfo GetMemberWithSameMetadataDefinitionAs(MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmemberwithsamemetadatadefinitionas?view=net-11.0)
 * `MethodInfo? GetMethod(string, int, BindingFlags, Binder?, Type[], ParameterModifier[]?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-11.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-reflection-binder-system-type()-system-reflection-parametermodifier()))
 * `MethodInfo? GetMethod(string, int, BindingFlags, Type[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-11.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type()))
 * `bool IsAssignableFrom<T>()`
 * `bool IsAssignableTo(Type?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto?view=net-11.0)
 * `bool IsAssignableTo<T>()`
 * `IsGenericMethodParameter` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter?view=net-11.0)


#### UdpClient

 * `ValueTask<UdpReceiveResult> ReceiveAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.receiveasync?view=net-11.0#system-net-sockets-udpclient-receiveasync(system-threading-cancellationtoken))
 * `ValueTask<int> SendAsync(ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask<int> SendAsync(ReadOnlyMemory<byte>, IPEndPoint?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-net-ipendpoint-system-threading-cancellationtoken))
 * `ValueTask<int> SendAsync(ReadOnlyMemory<byte>, string?, int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-string-system-int32-system-threading-cancellationtoken))


#### UInt16

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-11.0#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-11.0#system-uint16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@))
 * `bool TryParse(string?, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-11.0#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@))


#### UInt32

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-11.0#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-11.0#system-uint32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `float UInt32BitsToSingle(uint)`
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@))
 * `bool TryParse(string?, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@))


#### UInt64

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-11.0#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-11.0#system-uint64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `double UInt64BitsToDouble(ulong)`
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@))
 * `bool TryParse(string?, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@))


#### ValueTask

 * `CompletedTask` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask.completedtask?view=net-11.0)


#### XDocument

 * `Task SaveAsync(Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-11.0#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-11.0#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-11.0#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(Stream, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(TextReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(XmlReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))


#### XElement

 * `Task SaveAsync(Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task<XElement> LoadAsync(Stream, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XElement> LoadAsync(TextReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XElement> LoadAsync(XmlReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))


#### ZipArchive

 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-11.0)
 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CompressionLevel, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-11.0)
 * `void ExtractToDirectory(string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string-system-boolean))
 * `Task ExtractToDirectoryAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectoryasync(system-io-compression-ziparchive-system-string-system-boolean-system-threading-cancellationtoken))
 * `Task ExtractToDirectoryAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string))


#### ZipArchiveEntry

 * `Task ExtractToFileAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-11.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-boolean-system-threading-cancellationtoken))
 * `Task ExtractToFileAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-11.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-threading-cancellationtoken))
 * `Stream Open(FileAccess)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.open?view=net-11.0)
 * `Task<Stream> OpenAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-11.0)
 * `ValueTask<Stream> OpenAsync(FileAccess, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-11.0)
 * `ExternalAttributes` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.externalattributes?view=net-11.0)


#### ZipFile

 * `Task CreateFromDirectoryAsync(string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0)
 * `Task CreateFromDirectoryAsync(string, string, CompressionLevel, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0)
 * `Task CreateFromDirectoryAsync(string, string, CompressionLevel, bool, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0)
 * `Task ExtractToDirectoryAsync(string, string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0)
 * `Task ExtractToDirectoryAsync(string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0)
 * `Task ExtractToDirectoryAsync(string, string, Encoding, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0)
 * `Task ExtractToDirectoryAsync(string, string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0)


#### Ensure

 * `void DirectoryExists(string)`
 * `T Equal<T>(T, T)`
 * `void FileExists(string)`
 * `void NoDuplicates<T>(IEnumerable<T>)`
 * `string? NotEmpty(string?)`
 * `Memory<T>? NotEmpty<T>(Memory<T>?)`
 * `Memory<T> NotEmpty<T>(Memory<T>)`
 * `ReadOnlyMemory<T>? NotEmpty<T>(ReadOnlyMemory<T>?)`
 * `ReadOnlyMemory<T> NotEmpty<T>(ReadOnlyMemory<T>)`
 * `ReadOnlySpan<T> NotEmpty<T>(ReadOnlySpan<T>)`
 * `Span<T> NotEmpty<T>(Span<T>)`
 * `T? NotEmpty<T>(T?) where T : IEnumerable`
 * `T NotEqual<T>(T, T)`
 * `nint NotGreaterThan(nint, nint)`
 * `T NotGreaterThan<T>(T, T) where T : IComparable<T>`
 * `nint NotGreaterThanOrEqual(nint, nint)`
 * `T NotGreaterThanOrEqual<T>(T, T) where T : IComparable<T>`
 * `nint NotLessThan(nint, nint)`
 * `T NotLessThan<T>(T, T) where T : IComparable<T>`
 * `nint NotLessThanOrEqual(nint, nint)`
 * `T NotLessThanOrEqual<T>(T, T) where T : IComparable<T>`
 * `nint NotNegative(nint)`
 * `T NotNegative<T>(T) where T : struct, IComparable<T>`
 * `nint NotNegativeOrZero(nint)`
 * `T NotNegativeOrZero<T>(T) where T : struct, IComparable<T>`
 * `string NotNull(string?)`
 * `T NotNull<T>(T?) where T : class`
 * `Memory<char> NotNullOrEmpty(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrEmpty(ReadOnlyMemory<char>?)`
 * `string NotNullOrEmpty(string?)`
 * `T NotNullOrEmpty<T>(T?) where T : IEnumerable`
 * `Memory<char> NotNullOrWhiteSpace(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrWhiteSpace(ReadOnlyMemory<char>?)`
 * `string NotNullOrWhiteSpace(string?)`
 * `Memory<char>? NotWhiteSpace(Memory<char>?)`
 * `ReadOnlyMemory<char>? NotWhiteSpace(ReadOnlyMemory<char>?)`
 * `ReadOnlySpan<char> NotWhiteSpace(ReadOnlySpan<char>)`
 * `Span<char> NotWhiteSpace(Span<char>)`
 * `string? NotWhiteSpace(string?)`
 * `nint NotZero(nint)`
 * `T NotZero<T>(T) where T : struct, IEquatable<T>`


#### Lock

 * `void Enter()`
 * `Scope EnterScope()`
 * `void Exit()`
 * `bool TryEnter()`
 * `bool TryEnter(int)`
 * `bool TryEnter(TimeSpan)`


#### KeyValuePair

 * `KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create?view=net-11.0)


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

#### Version Warning

If System.Memory is installed but the version is older than 4.5.5, a build warning `PolyfillMemoryVersion` will be raised. This is because older versions lack the required types for FeatureMemory to be enabled, which can cause polyfills to silently fail.

To suppress this warning:

```xml
<PropertyGroup>
  <PolyfillNoWarnIncorrectVersion>true</PolyfillNoWarnIncorrectVersion>
</PropertyGroup>
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
public async Task Test()
{
    var type = typeof(NullabilityTarget);
    var arrayField = type.GetField("ArrayField")!;
    var genericField = type.GetField("GenericField")!;

    var context = new NullabilityInfoContext();

    var arrayInfo = context.Create(arrayField);

    await Assert.That(arrayInfo.ReadState).IsEqualTo(NullabilityState.NotNull);
    await Assert.That(arrayInfo.ElementType!.ReadState).IsEqualTo(NullabilityState.Nullable);

    var genericInfo = context.Create(genericField);

    await Assert.That(genericInfo.ReadState).IsEqualTo(NullabilityState.NotNull);
    await Assert.That(genericInfo.GenericTypeArguments[0].ReadState).IsEqualTo(NullabilityState.NotNull);
    await Assert.That(genericInfo.GenericTypeArguments[1].ReadState).IsEqualTo(NullabilityState.Nullable);
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


## Ensure

Enable by adding an MSBuild property `PolyEnsure`

```
<PropertyGroup>
  ...
  <PolyEnsure>true</PolyEnsure>
</PropertyGroup>
```

`Ensure` is designed to be an alternative to the `Argument*Exception.ThrowIf*` APIs added in net7.

 * `ArgumentException.ThrowIf*` [reference](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0&term=ArgumentException.ThrowIf)
 * `ArgumentNullException.ThrowIf*` [reference](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0&term=ArgumentNullException.ThrowIf)
 * `ArgumentOutOfRangeException.ThrowIf*` [reference](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0&term=ArgumentOutOfRangeException.ThrowIf)

With the equivalent Ensure APIs:

 * `Ensure.NotNullOrEmpty`
 * `Ensure.NotNullOrWhiteSpace`
 * `Ensure.NotNull`


### Justification

 * `Argument*Exception` are overly verbose
 * `Argument*Exception` do not provide "check return value value semantics"


### Comparison


#### `Argument*Exception` example

<!-- snippet: ArgumentExceptionUsage -->
<a id='snippet-ArgumentExceptionUsage'></a>
```cs
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
```
<sup><a href='/src/Consume/Consume.cs#L472-L496' title='Snippet source file'>snippet source</a> | <a href='#snippet-ArgumentExceptionUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


#### `Ensure` example

<!-- snippet: EnsureUsage -->
<a id='snippet-EnsureUsage'></a>
```cs
void EnsureExample(Order order, Customer customer, string customerId, string email, decimal discountPercentage, int quantity)
{
    this.order = Ensure.NotNull(order);
    this.customer = Ensure.NotNull(customer);
    this.customerId = Ensure.NotNullOrWhiteSpace(customerId);
    this.email = Ensure.NotNullOrWhiteSpace(email);
    this.discountPercentage = Ensure.NotGreaterThan(discountPercentage, 100m);
    this.quantity = Ensure.NotNegativeOrZero(quantity);
}
```
<sup><a href='/src/Consume/Consume.cs#L502-L514' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnsureUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### APIs

`Polyfills.Ensure` provides the following APIs:


#### Ensure<!-- include: api_ensure. path: /api_ensure.include.md -->

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


## ArgumentException

Enable `Argument*Exception` polyfills by adding an MSBuild property `PolyArgumentExceptions`

```
<PropertyGroup>
  ...
  <PolyArgumentExceptions>true</PolyArgumentExceptions>
</PropertyGroup>
```


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
