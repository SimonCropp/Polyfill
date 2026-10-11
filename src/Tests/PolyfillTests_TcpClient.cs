using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task TcpClientConnectAsync_IPAddress_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(IPAddress.Loopback, 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_IPAddressArray_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();
        var addresses = new[] { IPAddress.Loopback };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(addresses, 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_Host_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync("localhost", 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_IPEndPoint_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(endpoint, cancel));
    }

    [Test]
    public Task TcpClientConnectAsync_IPAddress_CancelledWhilePending_ThrowsOperationCanceledException() =>
        CancelPendingConnect((client, port, cancel) => client.ConnectAsync(IPAddress.Loopback, port, cancel));

    [Test]
    public Task TcpClientConnectAsync_IPAddressArray_CancelledWhilePending_ThrowsOperationCanceledException() =>
        CancelPendingConnect((client, port, cancel) => client.ConnectAsync(new[] {IPAddress.Loopback}, port, cancel));

    [Test]
    public Task TcpClientConnectAsync_Host_CancelledWhilePending_ThrowsOperationCanceledException() =>
        CancelPendingConnect((client, port, cancel) => client.ConnectAsync("localhost", port, cancel));

    [Test]
    public Task TcpClientConnectAsync_IPEndPoint_CancelledWhilePending_ThrowsOperationCanceledException() =>
        CancelPendingConnect((client, port, cancel) => client.ConnectAsync(new IPEndPoint(IPAddress.Loopback, port), cancel));

    static async Task CancelPendingConnect(Func<TcpClient, int, Cancel, ValueTask> connect)
    {
        // A loopback port with nothing listening. On Windows the connect stays pending while it is retried
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint) listener.LocalEndpoint).Port;
        listener.Stop();

        using var source = new CancellationTokenSource();
        using var client = new TcpClient();
        var task = connect(client, port, source.Token).AsTask();

        await Task.WhenAny(task, Task.Delay(100));
        if (task.IsCompleted)
        {
            // Refused immediately (non Windows), so there is no pending connect to cancel
            await Assert.That(task.IsFaulted).IsTrue();
            return;
        }

        source.Cancel();

        Exception? exception = null;
        try
        {
            await task;
        }
        catch (Exception caught)
        {
            exception = caught;
        }

        await Assert.That(exception is OperationCanceledException).IsTrue();
    }
}
