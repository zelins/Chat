using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Messages
{
    public abstract class ChatMessage
    {
        protected ChatMessage(string nickName)
        {
            NickName = nickName;
        }

        public string NickName { get; }
    }
}
