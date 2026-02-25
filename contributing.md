# Contributing


## Solution Structure


### Polyfill project

The main project that produces the nuget.


### Tests project

A TUnit test project that verifies all the APIs.


### NoRefsTests project

Some features of Polyfill [require nuget references](/#references) to be enabled. The NoRefsTests project had none of those refecences and tests the subset of features that do not require references.


### PublicTests project

Polyfill supports [making all APIs public](#consuming-and-type-visibility). The PublicTests project tests that scenario.


### UnsafeTests project

Some feature of Polyfill leverage unsafe code for better performance. For example `Append(this StringBuilder, ReadOnlySpan<char>)`. The UnsafeTests project tests this scenario vie enabling `<AllowUnsafeBlocks>True</AllowUnsafeBlocks>`.


### Consume project

Polyfill supports back to `net461` and `netcoreapp2.0`. However NUnit only support back to `net462` and `netcoreapp3.1`. The Consume project targets all frameworks that Polyfill supports, and consumes all APIs to ensure that they all compile on those frameworks


### ConsumeClassicReferences Project

Test the scenario when references are added through `<Reference` instead of `<PackageReference`.


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

Any potential ReSharper or Rider code formatting issues should be disabled. For example:

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

#### XML API comment

The XML API comments should match the actual API.


#### If the API is attribute based

Add a new class containing the Attribute

Example:

<!-- snippet: ModuleInitializerAttribute.cs -->
<a id='snippet-ModuleInitializerAttribute.cs'></a>
```cs
#if !NET

namespace System.Runtime.CompilerServices;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper restore RedundantNameQualifier

/// <summary>
/// Used to indicate to the compiler that a method should be called
/// in its containing module's initializer.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.moduleinitializerattribute?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class ModuleInitializerAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.ModuleInitializerAttribute))]
#endif
```
<sup><a href='/src/Polyfill/ModuleInitializerAttribute.cs#L1-L30' title='Snippet source file'>snippet source</a> | <a href='#snippet-ModuleInitializerAttribute.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


#### If the API is a missing instance method

Add an extension method to `Polyfill_TYPE.cs` where `TYPE` is the type the method extending. So, for example, APIs that target `StreamWriter` go in `Polyfill_StreamWriter.cs`.

Example:

<!-- snippet: Polyfill_TextWriter.cs -->
<a id='snippet-Polyfill_TextWriter.cs'></a>
```cs
namespace Polyfills;

using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if FeatureMemory
using System.Buffers;
#endif

static partial class Polyfill
{
#if !NET11_0_OR_GREATER

    /// <summary>
    /// Asynchronously writes a string to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-string-system-threading-cancellationtoken)
    public static Task WriteAsync(this TextWriter target, string? value, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteAsync(value)
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously writes a line terminator to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-threading-cancellationtoken)
    public static Task WriteLineAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteLineAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously writes a string followed by a line terminator to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-string-system-threading-cancellationtoken)
    public static Task WriteLineAsync(this TextWriter target, string? value, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteLineAsync(value)
            .WaitAsync(cancellationToken);
    }

#endif

#if !NET8_0_OR_GREATER

    //https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/IO/TextWriter.cs#L670

    /// <summary>
    /// Asynchronously clears all buffers for the current writer and causes any buffered data to
    /// be written to the underlying device.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-11.0#system-io-textwriter-flushasync(system-threading-cancellationtoken)
    public static Task FlushAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.FlushAsync()
            .WaitAsync(cancellationToken);
    }

#endif

#if !NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Equivalent to Write(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-text-stringbuilder)
    public static void Write(this TextWriter target, StringBuilder? value)
    {
        if (value == null)
        {
            return;
        }

#if FeatureMemory
        foreach (var chunk in value.GetChunks())
        {
            target.Write(chunk.Span);
        }
#else
        target.Write(value.ToString());
#endif
    }

    /// <summary>
    /// Equivalent to WriteAsync(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static Task WriteAsync(this TextWriter target, StringBuilder? value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (value == null)
        {
            return Task.CompletedTask;
        }

#if FeatureValueTask && FeatureMemory
        return WriteAsyncCore(target, value, cancellationToken);

        static async Task WriteAsyncCore(TextWriter target, StringBuilder builder, CancellationToken cancel)
        {
            foreach (var chunk in builder.GetChunks())
            {
                await target.WriteAsync(chunk, cancel);
            }
        }
#else
        return target.WriteAsync(value.ToString());
#endif
    }
#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER && FeatureMemory
#if FeatureValueTask

    /// <summary>
    /// Asynchronously writes a character memory region to the stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

    /// <summary>
    /// Asynchronously writes the text representation of a character memory region to the stream, followed by a line terminator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteLineAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteLineAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

#endif

    /// <summary>
    /// Writes a character span to the text stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-readonlyspan((system-char)))
    public static void Write(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.Write(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }

    /// <summary>
    /// Writes the text representation of a character span to the text stream, followed by a line terminator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-11.0#system-io-textwriter-writeline(system-readonlyspan((system-char)))
    public static void WriteLine(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.WriteLine(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }
#endif
}
```
<sup><a href='/src/Polyfill/Polyfill_TextWriter.cs#L1-L233' title='Snippet source file'>snippet source</a> | <a href='#snippet-Polyfill_TextWriter.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Add a test

Add a for the new API to the Tests project.

Extension method tests to `PolyfillTests_TYPE.cs` where `TYPE` is the type the method extending. So, for example, APIs that target `StreamWriter` go in `PolyfillTests_StreamWriter.cs`. For example:

<!-- snippet: PolyfillTests_StreamReader.cs -->
<a id='snippet-PolyfillTests_StreamReader.cs'></a>
```cs
partial class PolyfillTests
{
    [Test]
    public async Task StreamReaderReadAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        var result = new char[5];
        var memory = new Memory<char>(result);
        using var reader = new StreamReader(stream);
        var read = await reader.ReadAsync(memory);
        await Assert.That(read).IsEqualTo(5);
        await Assert.That("value".SequenceEqual(result)).IsTrue();
    }

    [Test]
    public async Task StreamReaderReadToEndAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        using var reader = new StreamReader(stream);
        var read = await reader.ReadToEndAsync(Cancel.None);
        await Assert.That(read).IsEqualTo("value");
    }

    [Test]
    public async Task StreamReaderReadLineAsync()
    {
        using var stream = new MemoryStream("line1\nline2"u8.ToArray());
        using var reader = new StreamReader(stream);
        var read = await reader.ReadLineAsync(Cancel.None);
        await Assert.That(read).IsEqualTo("line1");
    }
}
```
<sup><a href='/src/Tests/PolyfillTests_StreamReader.cs#L1-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-PolyfillTests_StreamReader.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Add to the Consume project

Add a simple usage of the API to the Consume project. The usage is there to check it compiles on old runtimes, not correctness.

Put a usage of polyfilled class method into a method in `Consume.cs` with suffix `_Methods` (e.g. `Stream_Methods` for `Stream` type methods). Keep method names in alphabetical order, do not use modifiers.

If new API is a compiler API (e.g. that polyfilled deconstruct method can be used in foreach loop), put usage into *Compiler Features* region.
