using Commands;
using Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.CommandsRelated.Handlers
{
    internal class ServerConnectCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command)
        {
            var connectCommand = command as ConnectCommand;

            Console.WriteLine(
                $@"[{connectCommand.Time.ToLongTimeString()}] User ""{connectCommand.User.Nickname}"" connected to chat");
        }
    }
}