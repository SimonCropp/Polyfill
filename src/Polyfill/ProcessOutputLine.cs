#if !NET11_0_OR_GREATER

namespace System.Diagnostics;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a line of output from a process, including whether it came from standard error.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processoutputline?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
readonly struct ProcessOutputLine
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessOutputLine"/> struct.
    /// </summary>
    public ProcessOutputLine(string content, bool standardError)
    {
        Content = content;
        StandardError = standardError;
    }

    /// <summary>
    /// Gets the content of the output line.
    /// </summary>
    public string Content { get; }

    /// <summary>
    /// Gets a value indicating whether the line came from the standard error stream.
    /// </summary>
    public bool StandardError { get; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.ProcessOutputLine))]
#endif
