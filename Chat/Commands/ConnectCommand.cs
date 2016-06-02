using System;
using Commands.Abstracts;
using Entities;

namespace Commands
{
    [Serializable]
    public class ConnectCommand : IChatCommand
    {
        public ConnectCommand(User user, DateTime time, bool isConnect)
        {
            User = user;
            Time = time;
            IsConnect = isConnect;
        }

        public User User { get; }
        public DateTime Time { get; }
        public bool IsConnect { get; }
    }
}