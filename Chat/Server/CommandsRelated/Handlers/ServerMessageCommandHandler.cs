using System;
using Commands;
using Commands.Abstracts;

namespace Server.CommandsRelated.Handlers
{
    internal class ServerMessageCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command, object parameter = null)
        {
            var messageCommand = command as MessageCommand;

            Console.WriteLine(
                $"[{messageCommand.Time.ToLongTimeString()}] {messageCommand.Message.Author.Nickname}: {messageCommand.Message.Content}");
        }
    }
}