#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

// MatchCasing was added in .NET Core 2.1 and .NET Standard 2.1
// Only polyfill for .NET Framework, .NET Standard 2.0, and .NET Core 2.0
#if NETFRAMEWORK || (NETSTANDARD && !NETSTANDARD2_1_OR_GREATER) || NETCOREAPP2_0

namespace System.IO;

/// <summary>
/// Specifies the type of character casing to match.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.matchcasing?view=net-10.0
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
#endif
