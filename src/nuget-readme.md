# Polyfill

Source only package that exposes newer .NET and C# features to older runtimes.

The package targets `netstandard2.0` and is designed to support the following runtimes.

 * `net461`, `net462`, `net47`, `net471`, `net472`, `net48`, `net481`
 * `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp3.0`, `netcoreapp3.1`
 * `net5.0`, `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`, `net11.0`
 * `uap10`


## Usage

After installing the NuGet package, polyfilled types and extension methods are available automatically. No extra `using` statements are needed beyond what the newer API requires.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Polyfill" Version="*" PrivateAssets="all" />
  </ItemGroup>
</Project>
```


## Key Features

 * **Source only** — compiles directly into the consuming project, no runtime dependency.
 * **881 polyfilled APIs** across attributes, extension methods, and static helpers.
 * **Conditional compilation** — only the APIs missing from the target framework are included.
 * **Optional feature groups** — Ensure, Guard, Nullability, ArgumentExceptions, StringInterpolation.


## Documentation

See the [full documentation on GitHub](https://github.com/SimonCropp/Polyfill) for the complete API list, configuration options, and troubleshooting.

A compiled library variant is also available: [PolyfillLib](https://nuget.org/packages/PolyfillLib/)


## Powered by

[![JetBrains logo.](jetbrains.png)](https://jb.gg/OpenSourceSupport)
