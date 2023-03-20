# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only package that exposes newer .net and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`


## Nuget

https://nuget.org/packages/Polyfill/


### SDK / LangVersion

This project leverages features the current stable SDK and c# language. As such consuming projects should target those:


### LangVersion

```
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <LangVersion>latest</LangVersion>
```


### global.json

```
{
  "sdk": {
    "version": "7.0.202",
    "rollForward": "latestFeature"
  }
}
```


## Included polyfills


### ModuleInitializerAttribute

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
    private int member;

    public int Member
    {
        get => member;
        init => member = value;
    }
}
```
<sup><a href='/src/Tests/MyRecord.cs#L1-L14' title='Snippet source file'>snippet source</a> | <a href='#snippet-isexternalinit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Nullable attributes

Reference: [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)

  * `[AllowNull]`
  * `[DisallowNull]`
  * `[DoesNotReturn]`
  * `[DoesNotReturnIf]`
  * `[MaybeNull]`
  * `[MaybeNullWhen]`
  * `[MemberNotNull]`
  * `[MemberNotNullWhen]`
  * `[NotNull]`
  * `[NotNullIfNotNull]`
  * `[NotNullWhen]`


### Required attributes

Reference: [C# required modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required)

 * [`[RequiredMember]`](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.requiredmemberattribute)
 * [`[SetsRequiredMembers]`](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.setsrequiredmembersattribute)

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

Reference: [CompilerFeatureRequiredAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilerfeaturerequiredattribute)

> Indicates that compiler support for a particular feature is required for the location where this attribute is applied.


### SkipLocalsInit attribute

Reference: (SkipLocalsInit attribute)(https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/general#skiplocalsinit-attribute)

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
        for (int i = 0; i < 120; i++)
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

Reference: [Low Level Struct Improvements](https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/low-level-struct-improvements.md)

<!-- snippet: UnscopedRefUsage.cs -->
<a id='snippet-UnscopedRefUsage.cs'></a>
```cs
using System.Diagnostics.CodeAnalysis;

struct UnscopedRefUsage
{
    int field;

    [UnscopedRef] ref int Prop1 => ref field;
}
```
<sup><a href='/src/Consume/UnscopedRefUsage.cs#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-UnscopedRefUsage.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### CallerArgumentExpressionAttribute

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
<sup><a href='/src/Tests/Guard.cs#L2-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-callerargumentexpression' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Alternatives


### PolySharp

https://github.com/Sergio0694/PolySharp


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


## Extensions

The class `Polyfill.PolyExtensions` includes the following extension methods:


### String

  * `bool StartsWith(this string, char)`
  * `bool EndsWith(this string, char)`


### Memory

[System.Memory](#systemmemory) reference required.

  * `bool Contains(this ReadOnlySpan<char>, char)`
  * `void Append(this StringBuilder, ReadOnlySpan<char>)`
  * `bool SequenceEqual(this ReadOnlySpan<char>, string)`
  * `bool Equals(this StringBuilder, ReadOnlySpan<char>)`
  * `bool SequenceEqual(this Span<char>, string)`


### Stream

[System.Threading.Tasks.Extensions](#systemthreadingtasksextensions) reference required.

  * `ValueTask<int> ReadAsync(this Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
  * `ValueTask WriteAsync(this Stream, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))
  * `ValueTask<int> ReadAsync(this StreamReader, Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.readasync#system-io-streamreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
  * `ValueTask<int> WriteAsync(this StreamWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter.writeasync#system-io-streamwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


## References

If any of the below reference are included, the related polyfills will be disabled.


### System.ValueTuple

If consuming in a project that targets `net461` or `net462`, a reference to [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/) nuget is required.

```
<PackageReference Include="System.ValueTuple"
                  Version="4.5.0"
                  Condition="$(TargetFramework.StartsWith('net46'))" />
```


### System.Memory

If consuming in a project that targets `netstandard`, `netframework`, or `netcoreapp2*`, a reference to [System.Memory](https://www.nuget.org/packages/System.Memory/) nuget is required.

```
<PackageReference Include="System.Memory"
                  Version="4.5.5"
                  Condition="$(TargetFrameworkIdentifier) == '.NETStandard' or
                             $(TargetFrameworkIdentifier) == '.NETFramework' or
                             $(TargetFramework.StartsWith('netcoreapp2'))" />
```


### System.Threading.Tasks.Extensions

If consuming in a project that target `netframework`, `netstandard2`, or 'netcoreapp2', a reference to [System.Threading.Tasks.Extensions](https://www.nuget.org/packages/System.Threading.Tasks.Extensions/) nuget is required.

```
<PackageReference Include="System.Threading.Tasks.Extensions"
                  Version="4.5.4"
                  Condition="$(TargetFramework) == 'netstandard2.0' or
                             $(TargetFramework) == 'netcoreapp2.0' or
                             $(TargetFrameworkIdentifier) == '.NETFramework'" />
```


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
