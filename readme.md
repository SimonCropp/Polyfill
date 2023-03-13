# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only packages that exposes newer .net and C# features to older runtimes.


## Nuget

https://nuget.org/packages/Polyfill/


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
class SkipLocalsInitExample
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
<sup><a href='/src/Tests/SkipLocalsInitExample.cs#L1-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-skiplocalsinit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->



### CallerArgumentExpressionAttribute

Reference: [CallerArgumentExpression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/caller-argument-expression)

<!-- snippet: CallerArgumentExpression -->
<a id='snippet-callerargumentexpression'></a>
```cs
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
<sup><a href='/src/Tests/Guard.cs#L1-L22' title='Snippet source file'>snippet source</a> | <a href='#snippet-callerargumentexpression' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
