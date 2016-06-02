using System;
using System.Collections.Immutable;
using System.Net.Sockets;
using System.Threading.Tasks;
using Commands;
using Commands.Abstracts;
using Server.CommandsRelated.Resolvers;
using Utils;

namespace Server
{
    public class Server
    {
        #region Constructor

        public Server()
        {
            this.resolver = new ServerCommandsResolver();
            this.listener = TcpListener.Create(1234);
            this.syncObject = new object();
            this.clients = ImmutableList<Client>.Empty;
            this.commandsQueue = ImmutableQueue<IChatCommand>.Empty;
        }

        #endregion Constructor

        #region Fields

        private readonly IChatCommandsResolver resolver;

        private readonly TcpListener listener;

        private readonly object syncObject;

        private ImmutableList<Client> clients;

        private ImmutableQueue<IChatCommand> commandsQueue;

        #endregion Fields

        #region Methods

        public async Task StartListen()
        {
            this.listener.Start();
            while (true)
            {
                var client = await this.listener.AcceptTcpClientAsync();

                var newClient = await HandleConnection(client);

                HandleClient(newClient);
            }
        }

        private async Task<Client> HandleConnection(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();

            var connectCommand = await stream.ReadCommandAsync() as ConnectCommand;

            var user = connectCommand.User;

            var client = new Client(user, tcpClient);

            this.clients = this.clients.Add(client);

            EnqueueCommand(connectCommand);

            return client;
        }

        private async Task HandleClient(Client client)
        {
            var tcpClient = client.TcpClient;
            var stream = client.Stream;
            while (true)
            {
                IChatCommand command = null;
                if (!client.CommandsQueue.IsEmpty)
                {
                    client.CommandsQueue = client.CommandsQueue.Dequeue(out command);
                }
                try
                {
                    if (stream.DataAvailable)
                    {
                        EnqueueCommand(await stream.ReadCommandAsync());
                    }
                    if (command != null)
                    {
                        await stream.WriteCommandAsync(command);
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }

            this.clients = this.clients.Remove(client);
            var disconnectCommand = new ConnectCommand
            (
                client.User,
                DateTime.Now,
                false
            );
            EnqueueCommand(disconnectCommand);
        }

        private void EnqueueCommand(IChatCommand command)
        {
            lock (this.syncObject)
            {
                command.Execute(this.resolver);
            }

            foreach (var user in this.clients)
            {
                user.CommandsQueue = user.CommandsQueue.Enqueue(command);
            }
        }

        #endregion Methods
    }
}