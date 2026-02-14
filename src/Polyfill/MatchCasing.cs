#if NETFRAMEWORK || NETSTANDARD && !NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0

namespace System.IO;

/// <summary>
/// Specifies the type of character casing to match.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.matchcasing?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
enum MatchCasing
{
    /// <summary>
    /// Matches using the default casing for the given platform.
    /// </summary>
    PlatformDefault = 0,

    /// <summary>
    /// Match respecting character casing.
    /// </summary>
    CaseSensitive = 1,

    /// <summary>
    /// Match ignoring character casing.
    /// </summary>
    CaseInsensitive = 2
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.MatchCasing))]
#endif
