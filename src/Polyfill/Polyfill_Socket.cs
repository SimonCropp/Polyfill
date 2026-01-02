#if FeatureMemory && FeatureValueTask

namespace Polyfills;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Sends data asynchronously from the specified buffer and monitors cancellation requests.
    /// </summary>
    // Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.sendasync?view=net-10.0
    public static ValueTask<int> SendAsync(this Socket target, ReadOnlyMemory<byte> buffer, SocketFlags socketFlags = SocketFlags.None, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
        }

        return new ValueTask<int>(SendWithCancellationAsync(target, buffer, socketFlags, cancellationToken));
    }

    static async Task<int> SendWithCancellationAsync(Socket target, ReadOnlyMemory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            var bytes = buffer.ToArray();
            // Use synchronous send wrapped in Task to provide a task-based API on older frameworks.
            return await Task.FromResult(target.Send(bytes, 0, bytes.Length, socketFlags));
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

    /// <summary>
    /// Receives data asynchronously into the specified buffer and monitors cancellation requests.
    /// </summary>
    // Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.receiveasync?view=net-10.0
    public static ValueTask<int> ReceiveAsync(this Socket target, Memory<byte> buffer, SocketFlags socketFlags = SocketFlags.None, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
        }

        return new ValueTask<int>(ReceiveWithCancellationAsync(target, buffer, socketFlags, cancellationToken));
    }

    static async Task<int> ReceiveWithCancellationAsync(Socket target, Memory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            var tmp = new byte[buffer.Length];
            int received = target.Receive(tmp, 0, tmp.Length, socketFlags);
            if (received > 0)
            {
                tmp.AsSpan(0, received).CopyTo(buffer.Span);
            }

            return await Task.FromResult(received);
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

    #if FeatureValueTask
    /// <summary>
    /// Connects the socket to a remote endpoint and monitors cancellation requests.
    /// </summary>
    // Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connectasync?view=net-10.0
    public static ValueTask ConnectAsync(this Socket target, EndPoint remoteEP, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationAsync(target, remoteEP, cancellationToken));
    }

    static async Task ConnectWithCancellationAsync(Socket target, EndPoint remoteEP, CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            // Use synchronous Connect wrapped as a task on older frameworks.
            await Task.Run(() => target.Connect(remoteEP)).ConfigureAwait(false);
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
    #endif
}
#endif