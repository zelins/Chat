using Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;

namespace Client.CommandsRelated.Handlers
{
    public class ClientMessageCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command, object parameter = null)
        {
            var messageCommand = command as MessageCommand;
            var list = parameter as ICollection<string>;

            list.Add(messageCommand.Message.Content);
        }
    }
}