# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/s6eqqg4ipeovebgd?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill.Source NuGet Status](https://img.shields.io/nuget/v/Polyfill.Source.svg?label=Polyfill.Source)](https://www.nuget.org/packages/Polyfill.Source/)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg?label=Polyfill)](https://www.nuget.org/packages/Polyfill/)



This project ships two packages:


## Polyfill.Source

https://nuget.org/packages/Polyfill.Source/

A source-only nuget designed to be compatible for libraries that are targeting multiple frameworks.


## Polyfill

https://nuget.org/packages/Polyfill/

A traditional nuget that ships a single assembly `Polyfill.dll`.


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
<sup><a href='/src/Polyfill.Source.Tests/Samples.cs#L4-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-moduleinitializerattribute' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### IsExternalInit

`IsExternalInit` is required to use records in older runtimes

<!-- snippet: IsExternalInit -->
<a id='snippet-isexternalinit'></a>
```cs
record MyRecord(string property);
```
<sup><a href='/src/Polyfill.Source.Tests/MyRecord.cs#L1-L5' title='Snippet source file'>snippet source</a> | <a href='#snippet-isexternalinit' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Crack](https://thenounproject.com/term/crack/3968590/) designed by [Adrien Coquet](https://thenounproject.com/coquet_adrien/) from [The Noun Project](https://thenounproject.com).
