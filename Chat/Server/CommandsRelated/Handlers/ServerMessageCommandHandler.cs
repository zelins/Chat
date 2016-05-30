using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using Commands.Abstracts;

namespace Server.CommandsRelated.Handlers
{
    internal class ServerMessageCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command)
        {
            var messageCommand = command as MessageCommand;

            Console.WriteLine(
                $"[{messageCommand.Time.ToLongTimeString()}] {messageCommand.Message.Author.Nickname}: {messageCommand.Message.Content}");
        }
    }
}
