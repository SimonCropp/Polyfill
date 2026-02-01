#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

// MatchType was added in .NET Core 2.1 and .NET Standard 2.1
// Only polyfill for .NET Framework, .NET Standard 2.0, and .NET Core 2.0
#if NETFRAMEWORK || (NETSTANDARD && !NETSTANDARD2_1_OR_GREATER) || NETCOREAPP2_0

namespace System.IO;

/// <summary>
/// Specifies the type of wildcard matching to use.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.matchtype?view=net-10.0
#if PolyPublic
public
#endif
enum MatchType
{
    /// <summary>
    /// Match using '*' and '?' wildcards.
    /// '*' matches zero to any amount of characters. '?' matches exactly one character.
    /// </summary>
    Simple = 0,

    /// <summary>
    /// Match using Win32 DOS style matching semantics.
    /// '*', '?', '&lt;', '&gt;', and '"' are all considered wildcards.
    /// </summary>
    Win32 = 1
}
#endif
