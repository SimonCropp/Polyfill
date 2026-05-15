#if !NET6_0_OR_GREATER

namespace System.Runtime.InteropServices;

/// <summary>
/// Specifies a POSIX signal number.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.posixsignal?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
enum PosixSignal
{
    /// <summary>Hangup</summary>
    SIGHUP = -1,
    /// <summary>Interrupt</summary>
    SIGINT = -2,
    /// <summary>Quit</summary>
    SIGQUIT = -3,
    /// <summary>Termination</summary>
    SIGTERM = -4,
    /// <summary>Child stopped</summary>
    SIGCHLD = -5,
    /// <summary>Continue</summary>
    SIGCONT = -6,
    /// <summary>Window resized</summary>
    SIGWINCH = -7,
    /// <summary>Terminal input for background process</summary>
    SIGTTIN = -8,
    /// <summary>Terminal output for background process</summary>
    SIGTTOU = -9,
    /// <summary>Stop typed at terminal</summary>
    SIGTSTP = -10,
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.InteropServices.PosixSignal))]
#endif
