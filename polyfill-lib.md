## PolyfillLib

Polyfill is source only nuget designed to be consumed by public facing nuget packages. The benefit being no dependency and no chance for dependency conflicts.

Internal facing systems (nugets and build pipelines) have less risk of having dependency conflicts. For example a company with an internal nuget feed used to produce apps. In these scenarios the extra cost of using a source only nuget may be considered less than ideal. ie the added assembly size and the increased build time. PolyfillLib is a standard nuget package containing assemblies for each target runtime. It is an alternative approach to using the Polyfill source only package.

https://nuget.org/packages/PolyfillLib/

## See also

 * [Consuming and type visibility](consuming.md))
 * [Polyfill and TargetFrameworks](target-frameworks.md))