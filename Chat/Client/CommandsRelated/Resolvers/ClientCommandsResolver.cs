using Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.CommandsRelated.Handlers;
using Commands;

namespace Client.CommandsRelated.Resolvers
{
    public class ClientCommandsResolver : IChatCommandsResolver
    {
        private readonly Dictionary<Type, IChatCommandHandler> handlersDictionary;

        public ClientCommandsResolver()
        {
            this.handlersDictionary = new Dictionary<Type, IChatCommandHandler>
            {
                {typeof(ConnectCommand), new ClientConnectCommandHandler()},
                {typeof(MessageCommand), new ClientMessageCommandHandler()}
            };
        }

        public IChatCommandHandler Resolve(IChatCommand command)
        {
            return this.handlersDictionary[command.GetType()];
        }
    }
}