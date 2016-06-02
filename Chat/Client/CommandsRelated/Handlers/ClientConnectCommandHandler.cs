using Commands;
using Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Messages;

namespace Client.CommandsRelated.Handlers
{
    public class ClientConnectCommandHandler : IChatCommandHandler
    {
        public void Execute(IChatCommand command, object parameter = null)
        {
            var connectCommand = command as ConnectCommand;
            var list = parameter as ObservableCollection<ChatMessage>;

            ChatMessage message;

            if (connectCommand.IsConnect)
                message = new ChatConnectMessage(connectCommand.User.Nickname);
            else
                message = new ChatDisconnectMessage(connectCommand.User.Nickname);

            list.Add(message);
        }
    }
}