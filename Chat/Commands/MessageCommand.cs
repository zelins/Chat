using Commands.Abstracts;
using Entities;
using System;

namespace Commands
{
    [Serializable]
    internal class MessageCommand : IChatCommand
    {
        public Message Message { get; set; }
        public DateTime Time { get; set; }
    }
}