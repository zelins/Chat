using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Commands;
using Commands.Abstracts;
using Entities;
using Server.CommandsRelated.Resolvers;
using Utils;

namespace Server
{
    public class Server
    {
        private readonly IChatCommandsResolver resolver;

        private readonly TcpListener listener;

        private ImmutableList<TcpClient> clients;

        private ImmutableQueue<IChatCommand> commandsQueue;

        public Server()
        {
            this.resolver = new ServerCommandsResolver();
            this.listener = TcpListener.Create(1234);
            this.clients = ImmutableList<TcpClient>.Empty;
            this.commandsQueue = ImmutableQueue<IChatCommand>.Empty;
        }

        public async void ListenConnections()
        {
            this.listener.Start();
            while (true)
            {
                var client = await this.listener.AcceptTcpClientAsync();

                this.clients = this.clients.Add(client);
            }
        }

        public async void ProcessConnectedClients()
        {
            while (true)
            {
                IChatCommand command = null;
                if (!this.commandsQueue.IsEmpty)
                {
                    this.commandsQueue = this.commandsQueue.Dequeue(out command);
                }
                command?.Execute(this.resolver);

                for (int i = 0; i < this.clients.Count; i++)
                {
                    var stream = this.clients[i].GetStream();

                    if (stream.DataAvailable)
                    {
                        this.commandsQueue = this.commandsQueue.Enqueue(await stream.ReadCommandAsync());
                    }

                    if (command != null)
                    {
                        await stream.WriteCommandAsync(command);
                    }
                }
            }
        }

        private async Task<IChatCommand> ReceiveDataAsync(TcpClient client)
        {
            var stream = client.GetStream();
            byte[] bytes = await stream.ReadBytesAsync(4);
            int bytesToRead = BitConverter.ToInt32(bytes, 0);
            bytes = await stream.ReadBytesAsync(bytesToRead);
            return (IChatCommand)bytes.DeserializeToObject();
        }

        private async Task<byte[]> ReadFromStreamAsync(Stream stream, int bytes)
        {
            var buffer = new byte[bytes];
            var readpos = 0;
            while (readpos < bytes)
            {
                readpos += await stream.ReadAsync(buffer, readpos, bytes - readpos);
            }
            return buffer;
        }
    }
}