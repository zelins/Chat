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
        public void Execute(IChatCommand command, object parameter = null)
        {
            var connectCommand = command as ConnectCommand;

            if (connectCommand.IsConnect)
            {
                Console.WriteLine(
                    $@"[{connectCommand.Time.ToLongTimeString()}] User ""{connectCommand.User.Nickname}"" connected to chat");
            }
            else
            {
                Console.WriteLine(
                    $@"[{connectCommand.Time.ToLongTimeString()}] User ""{connectCommand.User.Nickname}"" disconnected from chat");
            }
        }
    }
}