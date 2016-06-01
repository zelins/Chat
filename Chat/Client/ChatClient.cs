using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Commands;
using Commands.Abstracts;
using Entities;
using Utils;

namespace Client
{
    public class ChatClient : IDisposable
    {
        private TcpClient client = new TcpClient();

        private ImmutableQueue<IChatCommand> commandsQueue = ImmutableQueue<IChatCommand>.Empty;

        public User User { get; private set; }

        public async void ConnectToServer(string name)
        {
            await this.client.ConnectAsync("127.0.0.1", 1234);
            User = new User
            {
                Id = Guid.NewGuid(),
                Nickname = name
            };
            IChatCommand command = new ConnectCommand
            (
                User,
                DateTime.Now,
                true
            );

            this.commandsQueue = this.commandsQueue.Enqueue(command);

            HandleCommands();
        }

        public void EnqueueMessage(string message)
        {
            var messageCommand = new MessageCommand
            (
                new Message
                {
                    Author = User,
                    Id = Guid.Empty,
                    Content = message
                },
                DateTime.Now
            );
            this.commandsQueue = this.commandsQueue.Enqueue(messageCommand);
        }

        private async void HandleCommands()
        {
            while (true)
            {
                IChatCommand command = null;
                if (!this.commandsQueue.IsEmpty)
                {
                    this.commandsQueue = this.commandsQueue.Dequeue(out command);
                }

                try
                {
                    var stream = this.client.GetStream();

                    if (command != null)
                    {
                        await stream.WriteCommandAsync(command);
                    }

                    if (stream.DataAvailable)
                    {
                        var c = await stream.ReadCommandAsync();
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Will be implemented by Fody weaving
        /// </summary>
        public void Dispose()
        {
        }
    }
}