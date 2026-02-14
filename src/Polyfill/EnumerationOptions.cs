#if NETFRAMEWORK || NETSTANDARD && !NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0

namespace System.IO;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Provides file and directory enumeration options.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
class EnumerationOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerationOptions"/> class with the recommended default options.
    /// </summary>
    public EnumerationOptions()
    {
        IgnoreInaccessible = true;
        AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
        MaxRecursionDepth = int.MaxValue;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether to recurse into subdirectories while enumerating. The default is <see langword="false"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.recursesubdirectories?view=net-11.0
    public bool RecurseSubdirectories { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether to skip files or directories when access is denied (for example, <see cref="UnauthorizedAccessException"/> or <see cref="System.Security.SecurityException"/>). The default is <see langword="true"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.ignoreinaccessible?view=net-11.0
    public bool IgnoreInaccessible { get; set; }

    /// <summary>
    /// Gets or sets the suggested buffer size, in bytes. The default is 0 (no suggestion).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.buffersize?view=net-11.0
    public int BufferSize { get; set; }

    /// <summary>
    /// Gets or sets the attributes to skip. The default is <c>FileAttributes.Hidden | FileAttributes.System</c>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.attributestoskip?view=net-11.0
    public FileAttributes AttributesToSkip { get; set; }

    /// <summary>
    /// Gets or sets the match type. The default is <see cref="MatchType.Simple"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.matchtype?view=net-11.0
    public MatchType MatchType { get; set; }

    /// <summary>
    /// Gets or sets the case matching behavior. The default is <see cref="MatchCasing.PlatformDefault"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.matchcasing?view=net-11.0
    public MatchCasing MatchCasing { get; set; }

    int maxRecursionDepth;

    /// <summary>
    /// Gets or sets a value that indicates the maximum directory depth to recurse while enumerating, when <see cref="RecurseSubdirectories"/> is set to <see langword="true"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.maxrecursiondepth?view=net-11.0
    public int MaxRecursionDepth
    {
        get => maxRecursionDepth;
        set => maxRecursionDepth = value < 0 ? int.MaxValue : value;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether to return the special directory entries "." and "..".
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.returnspecialdirectories?view=net-11.0
    public bool ReturnSpecialDirectories { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.EnumerationOptions))]
#endif
