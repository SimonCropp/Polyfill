#if FeatureMemory
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0 || WINDOWS_UWP
#nullable enable

namespace System.Buffers;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
// Encapsulates a method that receives a span of objects of type T and a state object of type TArg.
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.spanaction-2?view=net-11.0
#if PolyPublic
public
#endif
delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Buffers.SpanAction<,>))]
#endif
#endif
