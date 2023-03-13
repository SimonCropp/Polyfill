# <img src="/src/icon.png" height="30px"> Polyfill

[![Build status](https://ci.appveyor.com/api/projects/status/636i70gvxfuwdq38?svg=true)](https://ci.appveyor.com/project/SimonCropp/Polyfill)
[![Polyfill.Source NuGet Status](https://img.shields.io/nuget/v/Polyfill.Source.svg?label=Polyfill.Source)](https://www.nuget.org/packages/Polyfill.Source/)
[![Polyfill NuGet Status](https://img.shields.io/nuget/v/Polyfill.svg?label=Nullability)](https://www.nuget.org/packages/Polyfill/)



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

[Fact]
public void ModuleInitTest() =>
    Assert.True(InitCalled);

[ModuleInitializer]
public static void ModuleInit() =>
    InitCalled = true;
```
<sup><a href='/src/Polyfill.Source.Tests/Samples.cs#L5-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-moduleinitializerattribute' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Reflection](https://thenounproject.com/term/reflection/4087162/) designed by [Yogi Aprelliyanto](https://thenounproject.com/yogiaprelliyanto/) from [The Noun Project](https://thenounproject.com).
