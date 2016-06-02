using System;
using System.Collections.Generic;
using Commands;
using Commands.Abstracts;
using Server.CommandsRelated.Handlers;

namespace Server.CommandsRelated.Resolvers
{
    internal class ServerCommandsResolver : IChatCommandsResolver
    {
        private readonly Dictionary<Type, IChatCommandHandler> handlersDictionary;

        public ServerCommandsResolver()
        {
            this.handlersDictionary = new Dictionary<Type, IChatCommandHandler>
            {
                {typeof(ConnectCommand), new ServerConnectCommandHandler()},
                {typeof(MessageCommand), new ServerMessageCommandHandler()}
            };
        }

        public IChatCommandHandler Resolve(IChatCommand command)
        {
            return this.handlersDictionary[command.GetType()];
        }
    }
}