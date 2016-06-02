using Commands;
using Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommandsRelated.Handlers
{
    public class ClientConnectCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command, object parameter = null)
        {
            var connectCommand = command as ConnectCommand;
            var list = parameter as ObservableCollection<string>;

            list.Add(connectCommand.User.Nickname + " connected");
        }
    }
}