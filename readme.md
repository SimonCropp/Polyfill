# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg)](https://www.nuget.org/packages/Polyfill/)

Source only packages that exposes newer .net and C# features to older runtimes.


## Nuget

https://nuget.org/packages/Polyfill/


## Usage


### ModuleInitializerAttribute

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
<sup><a href='/src/Tests/Samples.cs#L4-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-moduleinitializerattribute' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### IsExternalInit

`IsExternalInit` is required to use records in older runtimes

<!-- snippet: IsExternalInit -->
<a id='snippet-isexternalinit'></a>
```cs
record MyRecord(string property);
```
<sup><a href='/src/Tests/MyRecord.cs#L1-L5' title='Snippet source file'>snippet source</a> | <a href='#snippet-isexternalinit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Nullable attributes

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


### Required members

 * [`[RequiredMember]`](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.requiredmemberattribute)
 * [`[SetsRequiredMembers]`](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.setsrequiredmembersattribute)


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
