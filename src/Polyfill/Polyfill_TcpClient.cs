#if !NET5_0_OR_GREATER && FeatureValueTask

namespace Polyfills;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Connects the client to a remote TCP host using the specified IP address and port number as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-10.0#system-net-sockets-tcpclient-connectasync(system-net-ipaddress-system-int32-system-threading-cancellationtoken)
    public static ValueTask ConnectAsync(
        this TcpClient target,
        IPAddress address,
        int port,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationAsync(target, address, port, cancellationToken));
    }

    static async Task ConnectWithCancellationAsync(
        TcpClient target,
        IPAddress address,
        int port,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            await target.ConnectAsync(address, port);
        }
        catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
        catch (SocketException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
    }
}
#endif
