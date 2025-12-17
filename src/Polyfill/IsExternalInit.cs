#if !NET

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Reserved to be used by the compiler for tracking metadata. This class should not be used by developers in source code.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.isexternalinit?view=net-10.0
#if PolyPublic
public
#endif
static class IsExternalInit;

#else
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(IsExternalInit))]
#endif