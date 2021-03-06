﻿using System.Collections.Immutable;
using System.Net.Sockets;
using Commands.Abstracts;
using Entities;

namespace Server
{
    public class Client
    {
        #region Constructor

        public Client(User user, TcpClient client)
        {
            TcpClient = client;
            CommandsQueue = ImmutableQueue<IChatCommand>.Empty;
            User = user;
        }

        #endregion Constructor

        #region Properties

        public ImmutableQueue<IChatCommand> CommandsQueue { get; set; }

        public TcpClient TcpClient { get; }

        public User User { get; }

        public NetworkStream Stream => TcpClient.GetStream();

        #endregion Properties
    }
}