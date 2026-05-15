#if !NET11_0_OR_GREATER

namespace System.Diagnostics;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the captured text output from a process, including standard output, standard error, and exit status.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processtextoutput?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class ProcessTextOutput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessTextOutput"/> class.
    /// </summary>
    public ProcessTextOutput(ProcessExitStatus exitStatus, string standardOutput, string standardError, int processId)
    {
        ExitStatus = exitStatus;
        StandardOutput = standardOutput;
        StandardError = standardError;
        ProcessId = processId;
    }

    /// <summary>
    /// Gets the exit status of the process.
    /// </summary>
    public ProcessExitStatus ExitStatus { get; }

    /// <summary>
    /// Gets the process identifier.
    /// </summary>
    public int ProcessId { get; }

    /// <summary>
    /// Gets the captured standard error text.
    /// </summary>
    public string StandardError { get; }

    /// <summary>
    /// Gets the captured standard output text.
    /// </summary>
    public string StandardOutput { get; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.ProcessTextOutput))]
#endif
