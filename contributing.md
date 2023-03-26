# Contributing


## Solution Structure


### Polyfill

The main project that produces the nuget.


### Tests

A nUnit test project that verifies all the APIs.


### NoRefsTests

Some features of Polyfill [require nuget references](/#references) to be enabled. The NoRefsTests project had none of those refecences and tests the subset of features that do not require references.


### PublicTests

Polyfill supports [making all APIs public](#consuming-and-type-visibility). The PublicTests project tests that scenario.


### UnsafeTests

Some feature of Polyfill leverage unsafe code for better performance. For example `Append(this StringBuilder, ReadOnlySpan<char>)`. The UnsafeTests project tests this scenario vie enabling `<AllowUnsafeBlocks>True</AllowUnsafeBlocks>`.


### Consume

Polyfill supports back to `net461` and `netcoreapp2.0`. However nUnit only support back to `net462` and `netcoreapp3.1`. The Consume project targets all frameworks that Polyfill supports, and consumes all APIs to ensure that they all compile on those frameworks


## Submitting a new polyfill API


### Valid APIs

An API is a valid candidate to be polyfilled if it exists in the current stable version of .net or is planned for a future version .net

APIs that require a reference to a bridging nuget (similar to [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/) or [System.Memory](https://www.nuget.org/packages/System.Memory/)) will only be accepted if, in a future version of .net that nuget is not required.


### Raise Pull Request not an Issue

If a new API is valid, dont bother raising a GitHub issue to ask about it. Instead submit a Pull Request that adds that API. Any discussion can happen in the PR comments.


### Add the new API to the Polyfill project


#### Conditional Compilation

The code for the API should be wrapped in conditional compilation statements

```
#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X
```

The following additional compilation constants are provided:

 * `NETCOREAPPX`: indicates if netcore is being targeted.
 * `NETCOREAPP2X`: indicates if any major or minor version of netcore 2 is being targeted.
 * `NETCOREAPP3X`: indicates if any major or minor version of netcore 3 is being targeted.
 * `NET4X`: indicates if any major or minor version of NET46 is being targeted.
 * `NET46X`: indicates if any major or minor version of NET46 is being targeted.
 * `NET47X`: indicates if any major or minor version of NET47 is being targeted.
 * `NET48X`: indicates if any major or minor version of NET48 is being targeted.
 * `MEMORYREFERENCED`: indicates if [System.Memory](https://www.nuget.org/packages/System.Memory/)) is referenced.
 * `TASKSEXTENSIONSREFERENCED`: indicates if [System.Threading.Tasks.Extensions](https://www.nuget.org/packages/System.Threading.Tasks.Extensions/)) is referenced. 
 * `VALUETUPLEREFERENCED`: indicates if [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/)) is referenced.


#### Warnings disabled

Warnings must be disabled with a pragma.

```
#pragma warning disable
```

This is required to prevent custom code formatting rule in consuming projects from giving false warnings


#### ReSharper / Rider

Any potential ReSharper or Rider cde formatting issues should be disabled. For example:

```
// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global
```


#### Assume Implicit usings is disabled

Having Implicit enabled is optional for the consuming project. So ensure all using statements are included.
