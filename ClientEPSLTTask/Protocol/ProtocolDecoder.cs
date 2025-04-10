using System.Net.Sockets;

namespace ClientEPSLTTask.Protocol
{
    public static class ProtocolDecoder
    {
        public static async Task<bool> ReadBooleanResponseAsync(NetworkStream stream)
        {
            var buffer = new byte[1];
            await stream.ReadAsync(buffer);
            return buffer[0] != 0;
        }

        public static async Task<byte> ReadByteResponseAsync(NetworkStream stream)
        {
            var buffer = new byte[1];
            await stream.ReadAsync(buffer);
            return buffer[0];
        }
    }
}
