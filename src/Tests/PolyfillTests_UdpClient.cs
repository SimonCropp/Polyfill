using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task UdpClientReceiveAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ReceiveAsync(cancel));
    }

    static async Task<Exception?> CancelPendingReceive(UdpClient client)
    {
        using var source = new CancellationTokenSource();
        var task = client.ReceiveAsync(source.Token).AsTask();
        await Task.Delay(100);
        source.Cancel();
        try
        {
            await task;
            return null;
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    [Test]
    public async Task UdpClientReceiveAsync_CancelledWhilePending_ClientStaysUsable()
    {
        using var receiver = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
        var receiverEndPoint = (IPEndPoint) receiver.Client.LocalEndPoint!;
        using var sender = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
        var senderEndPoint = (IPEndPoint) sender.Client.LocalEndPoint!;

        var exception = await CancelPendingReceive(receiver);
        await Assert.That(exception is OperationCanceledException).IsTrue();

        // cancelling again, with no datagram in between, still works
        exception = await CancelPendingReceive(receiver);
        await Assert.That(exception is OperationCanceledException).IsTrue();

        // datagrams sent after the cancellation are received, in order
        await sender.SendAsync([1], 1, receiverEndPoint);
        await sender.SendAsync([2], 1, receiverEndPoint);
        await sender.SendAsync([3], 1, receiverEndPoint);

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var first = await receiver.ReceiveAsync(timeout.Token);
        var second = await receiver.ReceiveAsync(timeout.Token);
        var third = await receiver.ReceiveAsync(timeout.Token);
        await Assert.That(first.Buffer.Single()).IsEqualTo((byte) 1);
        await Assert.That(second.Buffer.Single()).IsEqualTo((byte) 2);
        await Assert.That(third.Buffer.Single()).IsEqualTo((byte) 3);
        await Assert.That(first.RemoteEndPoint.Port).IsEqualTo(senderEndPoint.Port);

        // and the client can still send
        await receiver.SendAsync([4], 1, senderEndPoint);
        var reply = await sender.ReceiveAsync(timeout.Token);
        await Assert.That(reply.Buffer.Single()).IsEqualTo((byte) 4);
    }

    [Test]
    public async Task UdpClientReceiveAsync_ReceivesDatagram()
    {
        using var receiver = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
        var receiverEndPoint = (IPEndPoint) receiver.Client.LocalEndPoint!;
        using var sender = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var task = receiver.ReceiveAsync(timeout.Token).AsTask();
        await sender.SendAsync([1, 2, 3], 3, receiverEndPoint);

        var result = await task;
        await Assert.That(result.Buffer.SequenceEqual(new byte[] {1, 2, 3})).IsTrue();
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemory_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient("localhost", 12345);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, cancel));
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemoryWithEndPoint_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, endpoint, cancel));
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemoryWithHostPort_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, "localhost", 12345, cancel));
    }

    [Test]
    public async Task UdpClientSend_ReadOnlySpan()
    {
        // Arrange
        using var client = new UdpClient("localhost", 12345);
        var data = new byte[] { 1, 2, 3 };

        // Act
        var sent = client.Send(new ReadOnlySpan<byte>(data));

        // Assert
        await Assert.That(sent).IsEqualTo(3);
    }

    [Test]
    public async Task UdpClientSend_ReadOnlySpanWithEndPoint()
    {
        // Arrange
        using var client = new UdpClient(0);
        var data = new byte[] { 1, 2, 3 };
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act
        var sent = client.Send(new ReadOnlySpan<byte>(data), endpoint);

        // Assert
        await Assert.That(sent).IsEqualTo(3);
    }

    [Test]
    public async Task UdpClientSend_ReadOnlySpanWithHostPort()
    {
        // Arrange
        using var client = new UdpClient(0);
        var data = new byte[] { 1, 2, 3 };

        // Act
        var sent = client.Send(new ReadOnlySpan<byte>(data), "localhost", 12345);

        // Assert
        await Assert.That(sent).IsEqualTo(3);
    }
}
