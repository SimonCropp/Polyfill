# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`


**See [Milestones](../../milestones?state=closed) for release notes.**

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
    "version": "7.0.306",
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
<a id='snippet-moduleinitializerattribute'></a>
```cs
static bool InitCalled;

[Test]
public void ModuleInitTest() =>
    Assert.True(InitCalled);

[ModuleInitializer]
public static void ModuleInit() =>
    InitCalled = true;
```
<sup><a href='/src/Tests/ModuleInitSample.cs#L4-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-moduleinitializerattribute' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### IsExternalInit

Reference: [init (C# Reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init)

<!-- snippet: IsExternalInit -->
<a id='snippet-isexternalinit'></a>
```cs
class InitExample
{
    public int Member { get; init; }
}
```
<sup><a href='/src/Tests/MyRecord.cs#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-isexternalinit' title='Start of snippet'>anchor</a></sup>
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
<a id='snippet-required'></a>
```cs
public class Person
{
    public Person() { }

    [SetsRequiredMembers]
    public Person(string name) =>
        Name = name;

    public required string Name { get; init; }
}
```
<sup><a href='/src/Tests/Required.cs#L2-L13' title='Snippet source file'>snippet source</a> | <a href='#snippet-required' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### CompilerFeatureRequiredAttribute

 * [CompilerFeatureRequiredAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilerfeaturerequiredattribute)

> Indicates that compiler support for a particular feature is required for the location where this attribute is applied.


### SkipLocalsInit

 * [SkipLocalsInitAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.skiplocalsinitattribute)

Reference: [SkipLocalsInitAttribute](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/general#skiplocalsinit-attribute)

> the SkipLocalsInit attribute prevents the compiler from setting the .locals init flag when emitting to metadata. The SkipLocalsInit attribute is a single-use attribute and can be applied to a method, a property, a class, a struct, an interface, or a module, but not to an assembly. SkipLocalsInit is an alias for SkipLocalsInitAttribute.

<!-- snippet: SkipLocalsInit -->
<a id='snippet-skiplocalsinit'></a>
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
<sup><a href='/src/UnsafeTests/SkipLocalsInitExample.cs#L1-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-skiplocalsinit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Index and Range

Reference: [Indices and ranges](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes)

If consuming in a project that targets net461 or net462, a reference to System.ValueTuple is required. See [References: System.ValueTuple](#systemvaluetuple).

<!-- snippet: IndexRange -->
<a id='snippet-indexrange'></a>
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
}
```
<sup><a href='/src/Tests/IndexRangeSample.cs#L1-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-indexrange' title='Start of snippet'>anchor</a></sup>
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


### CallerArgumentExpressionAttribute

 * [CallerArgumentExpressionAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callerargumentexpressionattribute)

Reference: [CallerArgumentExpression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/caller-argument-expression)

<!-- snippet: CallerArgumentExpression -->
<a id='snippet-callerargumentexpression'></a>
```cs
using System.IO;

static class Guard
{
    public static void FileExists(string path, [CallerArgumentExpression("path")] string argumentName = "")
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"File not found. Path: {path}", argumentName);
        }
    }
}

static class GuardUsage
{
    public static string[] Method(string path)
    {
        Guard.FileExists(path);
        return File.ReadAllLines(path);
    }
}
```
<sup><a href='/src/Tests/Guard.cs#L3-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-callerargumentexpression' title='Start of snippet'>anchor</a></sup>
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


#### Dictionary<TKey,TValue>

 * `Boolean Remove<TKey, TValue>(TKey, TValue&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove)


#### IEnumerable<TSource>

 * `IEnumerable<TSource[]> Chunk<TSource>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk)
 * `IEnumerable<TSource> Except<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-8.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-8.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource, IEqualityComparer<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEqualityComparer<TSource>, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `TSource MaxBy<TSource, TKey>(Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource MaxBy<TSource, TKey>(Func<TSource,TKey>, IComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-8.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource MinBy<TSource, TKey>(Func<TSource,TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource MinBy<TSource, TKey>(Func<TSource,TKey>, IComparer<TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-8.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `IEnumerable<TSource> SkipLast<TSource>(Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast)
 * `HashSet<TSource> ToHashSet<TSource>(IEqualityComparer<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))


#### IReadOnlyDictionary<TKey,TValue>

 * `TValue GetValueOrDefault<TKey, TValue>(TKey)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault)
 * `TValue GetValueOrDefault<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### KeyValuePair<TKey,TValue>

 * `Void Deconstruct<TKey, TValue>(TKey&, TValue&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct)


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


#### Process

 * `Task WaitForExitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync)


#### Double

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat)


#### Guid

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))


#### Int16

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat)


#### Int32

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat)


#### Int64

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat)


#### Stream

 * `Task CopyToAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<Int32> ReadAsync(Memory<Byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(ReadOnlyMemory<Byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<Int32> ReadAsync(Memory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<String> ReadLineAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<String> ReadToEndAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `Void Write(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `ValueTask WriteAsync(ReadOnlyMemory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Void WriteLine(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `ValueTask WriteLineAsync(ReadOnlyMemory<Char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


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


#### ReadOnlySpan<Char>

 * `Boolean EndsWith(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `Boolean SequenceEqual(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `Boolean StartsWith(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))


#### ReadOnlySpan<T>

 * `Boolean Contains<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))


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


#### SByte

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat)


#### Single

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat)


#### Span<Char>

 * `Boolean EndsWith(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Boolean SequenceEqual(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `Boolean StartsWith(String)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))


#### Span<T>

 * `Boolean Contains<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-span((-0))-0))


#### String

 * `Boolean Contains(String, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison))
 * `Boolean Contains(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `Void CopyTo(Span<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto)
 * `Boolean EndsWith(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `Int32 GetHashCode(StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison))
 * `String[] Split(Char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions))
 * `String[] Split(Char, Int32, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `Boolean StartsWith(Char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char))
 * `Boolean TryCopyTo(Span<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto)


#### RegularExpressions.Regex

 * `Boolean IsMatch(ReadOnlySpan<Char>, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `Boolean IsMatch(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))


#### StringBuilder

 * `StringBuilder Append(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(IFormatProvider, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin(String, String[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin(String, Object[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(Char, String[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(Char, Object[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin<T>(Char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(String, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=netcore-2.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(IFormatProvider, AppendInterpolatedStringHandler&)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `Void CopyTo(Int32, Span<Char>, Int32)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `Boolean Equals(ReadOnlySpan<Char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char))))


#### CancellationToken

 * `CancellationTokenRegistration Register(Action<Object,CancellationToken>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<Object>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<Object,CancellationToken>, Object)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync)


#### Task

 * `Task WaitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan))
 * `Task WaitAsync(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))


#### TimeSpan

 * `Int32 Microseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds)
 * `Int32 Nanoseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds)
 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `Boolean IsGenericMethodParameter()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter)


#### UInt16

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat)


#### UInt32

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat)


#### UInt64

 * `Boolean TryFormat(Span<Char>, Int32&, ReadOnlySpan<Char>, IFormatProvider)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat)

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

If using ValueTask APIs and consuming in a project that target `netframework`, `netstandard2`, or 'netcoreapp2', a reference to [System.Threading.Tasks.Extensions](https://www.nuget.org/packages/System.Threading.Tasks.Extensions/) nuget is required.

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
<a id='snippet-nullabilitytarget'></a>
```cs
class NullabilityTarget
{
    public string? StringField;
    public string?[] ArrayField;
    public Dictionary<string, object?> GenericField;
}
```
<sup><a href='/src/Tests/NullabilityTarget.cs#L3-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-nullabilitytarget' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### NullabilityInfoContext

<!-- snippet: NullabilityUsage -->
<a id='snippet-nullabilityusage'></a>
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
<sup><a href='/src/Tests/NullabilitySamples.cs#L6-L29' title='Snippet source file'>snippet source</a> | <a href='#snippet-nullabilityusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### NullabilityInfoExtensions

`NullabilityInfoExtensions` provides static and thread safe wrapper around `NullabilityInfoContext`. It adds three extension methods to each of ParameterInfo, PropertyInfo, EventInfo, and FieldInfo.

 * `GetNullabilityInfo`: returns the `NullabilityInfo` for the target info.
 * `GetNullability`: returns the `NullabilityState` for the state (`NullabilityInfo.ReadState` or `NullabilityInfo.WriteState` depending on which has more info) of target info.
 * `IsNullable`: given the state (`NullabilityInfo.ReadState` or `NullabilityInfo.WriteState` depending on which has more info) of the info:
   * Returns true if state is `NullabilityState.Nullable`.
   * Returns false if state is `NullabilityState.NotNull`.
   * Throws an exception if state is `NullabilityState.Unknown`.


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


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
