using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task SocketSendAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new CancellationToken(true);
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(IPAddress.Loopback, port);

        var data = new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3 });

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.SendAsync(data, SocketFlags.None, cancel));

        listener.Stop();
    }

    [Test]
    public async Task SocketReceiveAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new CancellationToken(true);
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(IPAddress.Loopback, port);

        var buffer = new Memory<byte>(new byte[10]);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ReceiveAsync(buffer, SocketFlags.None, cancel));

        listener.Stop();
    }

    [Test]
    public async Task SocketConnectAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new CancellationToken(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ConnectAsync(endpoint, cancel));
    }

    [Test]
    public async Task SocketSendAsync_SendsData()
    {
        // Arrange
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        using var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(IPAddress.Loopback, port);

        using var serverSocket = listener.AcceptSocket();

        var data = new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3 });

        // Act
        var bytesSent = await clientSocket.SendAsync(data, SocketFlags.None, CancellationToken.None);

        // Assert
        await Assert.That(bytesSent).IsEqualTo(3);

        listener.Stop();
    }

    [Test]
    public async Task SocketReceiveAsync_ReceivesData()
    {
        // Arrange
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        using var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(IPAddress.Loopback, port);

        using var serverSocket = listener.AcceptSocket();

        var sendData = new byte[] { 1, 2, 3 };
        serverSocket.Send(sendData);

        var buffer = new Memory<byte>(new byte[10]);

        // Act
        var bytesReceived = await clientSocket.ReceiveAsync(buffer, SocketFlags.None, CancellationToken.None);

        // Assert
        await Assert.That(bytesReceived).IsEqualTo(3);
        await Assert.That(buffer.Span[0]).IsEqualTo((byte)1);
        await Assert.That(buffer.Span[1]).IsEqualTo((byte)2);
        await Assert.That(buffer.Span[2]).IsEqualTo((byte)3);

        listener.Stop();
    }

    [Test]
    public async Task SocketConnectAsync_Connects()
    {
        // Arrange
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endpoint = new IPEndPoint(IPAddress.Loopback, port);

        // Act
        await socket.ConnectAsync(endpoint, CancellationToken.None);

        // Assert
        await Assert.That(socket.Connected).IsTrue();

        listener.Stop();
    }
}
