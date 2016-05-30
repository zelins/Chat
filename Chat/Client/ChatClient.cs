using System;
using System.Collections.Immutable;
using System.Net.Sockets;
using Commands;
using Commands.Abstracts;
using Entities;
using Utils;

namespace Client
{
    public class ChatClient : IDisposable
    {
        private readonly TcpClient client = new TcpClient();

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
            IChatCommand command = new ConnectCommand()
            {
                User = User,
                Time = DateTime.Now
            };

            this.commandsQueue = this.commandsQueue.Enqueue(command);

            ProcessCommands();
        }

        public void EnqueueMessage(string message)
        {
            var messageCommand = new MessageCommand
            {
                Message = new Message
                {
                    Author = User,
                    Id = Guid.Empty,
                    Content = message
                },
                Time = DateTime.Now
            };

            commandsQueue = commandsQueue.Enqueue(messageCommand);
        }

        private async void ProcessCommands()
        {
            while (true)
            {
                IChatCommand command = null;
                if (!this.commandsQueue.IsEmpty)
                {
                    this.commandsQueue = this.commandsQueue.Dequeue(out command);
                }
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
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }
    }
}