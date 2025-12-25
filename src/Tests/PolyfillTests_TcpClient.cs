using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task TcpClientConnectAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(IPAddress.Loopback, 12345, cancel));
    }
}
