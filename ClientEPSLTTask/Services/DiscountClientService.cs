using ClientEPSLTTask.Protocol;
using System.Net.Sockets;

namespace ClientEPSLTTask.Services
{
    public class DiscountClientService : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public DiscountClientService(string host, int port)
        {
            _client = new TcpClient();
            _client.Connect(host, port);
            _stream = _client.GetStream();
        }

        public Task SendGenerateAsync(ushort count, byte length) =>
            ProtocolEncoder.SendGenerateRequestAsync(_stream, count, length);

        public Task SendUseCodeAsync(string code) =>
            ProtocolEncoder.SendUseCodeRequestAsync(_stream, code);

        public Task<bool> ReadBoolAsync() =>
            ProtocolDecoder.ReadBooleanResponseAsync(_stream);

        public Task<byte> ReadByteAsync() =>
            ProtocolDecoder.ReadByteResponseAsync(_stream);

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
        }
    }
}
