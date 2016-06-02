namespace Client.Messages
{
    public class ChatTextMessage : ChatMessage
    {
        public ChatTextMessage(string name, string content) : base(name)
        {
            Content = content;
        }

        public string Content { get; }
    }
}