using System;

namespace Entities
{
    [Serializable]
    public class Message
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
    }
}