# Consuming and type visibility

The default type visibility for all polyfills is `internal`. This means it can be consumed in multiple projects and types will not conflict.

## Recommended consuming pattern

The recommended general way to consume polyfill is to use the source package (`Polyfill`, not `PolyfillLib`), with all types being `internal` (default). As many projects use `InternalsVisibleTo`, this can result in conflicts. To resolve this, specify `<PolyUseEmbeddedAttribute>true</PolyUseEmbeddedAttribute>` under a `PropertyGroup`. That way, every project will have its "embedded" version of the types of Polyfill. Simply put, "embedded" means that even if `InternalsVisibleTo` is used, the "embedded" types are still not visible to the other assemblies.

Alternatively, and depending on your specific scenario, below states other ways to consume Polyfill.

## Consuming in an app

If Polyfill is being consumed in a solution that produce an app, then it is recommended to use the Polyfill nuget only in the root "app project" and enable `PolyPublic`.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <PolyPublic>true</PolyPublic>
```

Then all consuming projects, like tests, will not need to use the Polyfill nuget.


## Consuming in a library

If Polyfill is being consumed in a solution that produce a library (and usually a nuget), then the Polyfill nuget can be added to all projects.

If, however, `InternalsVisibleTo` is being used to expose APIs (for example to test projects), then the Polyfill nuget should be added only to the root library project.


## See also

 * [PolyfillLib](polyfill-lib.md)
 * [Polyfill and TargetFrameworks](target-frameworks.md)
