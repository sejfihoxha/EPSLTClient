using EPSLTTaskServer.Application;
using System.Net.Sockets;
using System.Text;

namespace ClientEPSLTTask.Protocol
{
    public static class ProtocolEncoder
    {
        public static async Task SendGenerateRequestAsync(NetworkStream stream, ushort count, byte length)
        {
            // 1 byte for type + 3 for data
            var buffer = new byte[4];
            buffer[0] = (int)RequestTypeEnum.Generate;
            BitConverter.GetBytes(count).CopyTo(buffer, 1);
            buffer[3] = length;

            await stream.WriteAsync(buffer);
        }

        public static async Task SendUseCodeRequestAsync(NetworkStream stream, string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length < 7 || code.Length > 8)
                throw new ArgumentException("Code must be 7 or 8 characters.");

            var codeBytes = Encoding.ASCII.GetBytes(code);
            // 1 byte for type + 7/8 for code
            var buffer = new byte[1 + codeBytes.Length];

            buffer[0] = (int)RequestTypeEnum.Use;
            codeBytes.CopyTo(buffer, 1);

            await stream.WriteAsync(buffer);
        }
    }
}