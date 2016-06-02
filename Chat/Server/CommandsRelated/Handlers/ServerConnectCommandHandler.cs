using System;
using Commands;
using Commands.Abstracts;

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