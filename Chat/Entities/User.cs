using System;

namespace Entities
{
    [Serializable]
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
    }
}