global using global::Polyfills;

namespace Polyfills;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[EditorBrowsable(EditorBrowsableState.Never)]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static partial class Polyfill;