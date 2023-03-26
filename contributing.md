# Contributing


## Solution Structure


### Polyfill

The main project that produces the nuget.


### Tests

A NUnit test project that verifies all the APIs.


### NoRefsTests

Some features of Polyfill [require nuget references](/#references) to be enabled. The NoRefsTests project had none of those refecences and tests the subset of features that do not require references.


### PublicTests

Polyfill supports [making all APIs public](#consuming-and-type-visibility). The PublicTests project tests that scenario.


### UnsafeTests

Some feature of Polyfill leverage unsafe code for better performance. For example `Append(this StringBuilder, ReadOnlySpan<char>)`. The UnsafeTests project tests this scenario vie enabling `<AllowUnsafeBlocks>True</AllowUnsafeBlocks>`.


### Consume

Polyfill supports back to `net461` and `netcoreapp2.0`. However NUnit only support back to `net462` and `netcoreapp3.1`. The Consume project targets all frameworks that Polyfill supports, and consumes all APIs to ensure that they all compile on those frameworks


## Submitting a new polyfill API


### Valid APIs

An API is a valid candidate to be polyfilled if it exists in the current stable version of .net or is planned for a future version .net

APIs that require a reference to a bridging nuget (similar to [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/) or [System.Memory](https://www.nuget.org/packages/System.Memory/)) will only be accepted if, in a future version of .net that nuget is not required.


### Raise Pull Request not an Issue

If a new API is valid, dont bother raising a GitHub issue to ask about it. Instead submit a Pull Request that adds that API. Any discussion can happen in the PR comments.


### Add the new API to the Polyfill project


#### Conditional Compilation

The code for the API should be wrapped in conditional compilation statements. For example:

```
#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X
```

The following additional compilation constants are provided:

 * `NETCOREAPPX`: indicates if netcore is being targeted.
 * `NETCOREAPP2X`: indicates if any major or minor version of netcore 2 is being targeted.
 * `NETCOREAPP3X`: indicates if any major or minor version of netcore 3 is being targeted.
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

Having Implicit usings enabled is optional for the consuming project. So ensure all using statements are included.


#### Make public if 

Polyfill supports [making all APIs public](#consuming-and-type-visibility). This is done by making types public if `PolyPublic`. For example:

```
#if PolyPublic
public
#endif
sealed class ...
```


#### If the API is attribute based

Add a new class containing the Attribute

Example:

<!-- snippet: ModuleInitializerAttribute.cs -->
<a id='snippet-ModuleInitializerAttribute.cs'></a>
```cs
#if !NET5_0_OR_GREATER

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices;

/// <summary>
/// Used to indicate to the compiler that a method should be called
/// in its containing module's initializer.
/// </summary>
/// <remarks>
/// When one or more valid methods
/// with this attribute are found in a compilation, the compiler will
/// emit a module initializer which calls each of the attributed methods.
///
/// Certain requirements are imposed on any method targeted with this attribute:
/// - The method must be `static`.
/// - The method must be an ordinary member method, as opposed to a property accessor, constructor, local function, etc.
/// - The method must be parameterless.
/// - The method must return `void`.
/// - The method must not be generic or be contained in a generic type.
/// - The method's effective accessibility must be `internal` or `public`.
///
/// The specification for module initializers in the .NET runtime can be found here:
/// https://github.com/dotnet/runtime/blob/master/docs/design/specs/Ecma-335-Augments.md#module-initializer
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
#if PolyPublic
public
#endif
sealed class ModuleInitializerAttribute :
    Attribute
{
}

#endif
```
<sup><a href='/src/Polyfill/ModuleInitializerAttribute.cs#L1-L46' title='Snippet source file'>snippet source</a> | <a href='#snippet-ModuleInitializerAttribute.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


#### If the API is a missing instance method

Add an extension method to `PolyfillExtensions_TYPE.cs` where `TYPE` is the type the method extending. So, for example, APIs that target `StreamWriter` go in `PolyfillExtensions_StreamWriter.cs`.

Example:

<!-- snippet: PolyfillExtensions_StreamWriter.cs -->
<a id='snippet-PolyfillExtensions_StreamWriter.cs'></a>
```cs
#if TASKSEXTENSIONSREFERENCED && (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0)

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask WriteAsync(
        this StreamWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
#endif
```
<sup><a href='/src/Polyfill/PolyfillExtensions_StreamWriter.cs#L1-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-PolyfillExtensions_StreamWriter.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
