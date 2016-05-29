using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands.Abstracts;
using Entities;

namespace Commands
{
    [Serializable]
    public class ConnectCommand : IChatCommand
    {
        public User User { get; set; }
        public DateTime Time { get; set; }
    }
}
