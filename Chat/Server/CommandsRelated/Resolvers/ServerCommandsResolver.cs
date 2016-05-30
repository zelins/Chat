using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using Commands.Abstracts;
using Server.CommandsRelated.Handlers;

namespace Server.CommandsRelated.Resolvers
{
    class ServerCommandsResolver : IChatCommandsResolver
    {
        private Dictionary<Type, IChatCommandHandler> handlersDictionary;

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
