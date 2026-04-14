#if NET461 || NET462 || NET47 || WINDOWS_UWP
#nullable disable

namespace System.Net.Sockets;

using System.Net;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// The result of a receive-from asynchronous socket operation.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socketreceivefromresult?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
struct SocketReceiveFromResult
{
    /// <summary>
    /// The number of bytes received.
    /// </summary>
    public int ReceivedBytes;

    /// <summary>
    /// The source endpoint.
    /// </summary>
    public EndPoint RemoteEndPoint;
}
#endif
