namespace Polyfills;

using System.Runtime.CompilerServices;


using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[StackTraceHidden]
#if NET5_0_OR_GREATER
[Obsolete("Use Polyfills.Ensure", UrlFormat = "https://github.com/SimonCropp/Polyfill?tab=readme-ov-file#ensure-1")]
#else
[Obsolete("Use Polyfills.Ensure. https://github.com/SimonCropp/Polyfill?tab=readme-ov-file#ensure-1")]
#endif
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static partial class Guard
{
    public static void FileExists(string path, [CallerArgumentExpression("path")] string name = "") =>
        Ensure.FileExists(path, name);

    public static void DirectoryExists(string path, [CallerArgumentExpression("path")] string name = "") =>
        Ensure.DirectoryExists(path, name);
}