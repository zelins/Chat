using System;
using System.Collections.Immutable;
using System.Net.Sockets;
using Commands.Abstracts;
using Entities;

namespace Server
{
    public class Client
    {
        public ImmutableQueue<IChatCommand> CommandsQueue { get; set; }

        public TcpClient TcpClient { get; }

        public User User { get; }

        public NetworkStream Stream => this.TcpClient.GetStream();

        public Client(User user, TcpClient client)
        {
            this.TcpClient = client;
            this.CommandsQueue = ImmutableQueue<IChatCommand>.Empty;
            User = user;
        }
    }
}