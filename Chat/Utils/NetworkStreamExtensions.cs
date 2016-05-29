using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Commands.Abstracts;

namespace Utils
{
    public static class NetworkStreamExtensions
    {
        public static async Task<IChatCommand> ReadCommandAsync(this NetworkStream stream)
        {
            byte[] bytes = await stream.ReadBytesAsync(4);
            int bytesToRead = BitConverter.ToInt32(bytes, 0);
            bytes = await stream.ReadBytesAsync(bytesToRead);
            return (IChatCommand)bytes.DeserializeToObject();
        }

        public static async Task WriteCommandAsync(this NetworkStream stream, IChatCommand command)
        {
            var commandBytes = command.SerializeToByteArray();
            var commandSizeBytes = BitConverter.GetBytes(commandBytes.Length);

            await stream.WriteAsync(commandSizeBytes, 0, commandSizeBytes.Length);

            await stream.WriteAsync(commandBytes, 0, commandBytes.Length);
        }

        public static async Task<byte[]> ReadBytesAsync(this NetworkStream stream, int bytesToRead)
        {
            var buffer = new byte[bytesToRead];
            var offset = 0;
            while (offset < bytesToRead)
            {
                offset += await stream.ReadAsync(buffer, offset, bytesToRead - offset);
            }
            return buffer;
        }
    }
}
