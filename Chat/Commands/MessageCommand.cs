﻿using System;
using Commands.Abstracts;
using Entities;

namespace Commands
{
    [Serializable]
    public class MessageCommand : IChatCommand
    {
        public MessageCommand(Message message, DateTime time)
        {
            Message = message;
            Time = time;
        }

        public Message Message { get; }
        public DateTime Time { get; }
    }
}