namespace Client.Messages
{
    public class ChatDisconnectMessage : ChatMessage
    {
        public ChatDisconnectMessage(string nickName) : base(nickName)
        {
        }
    }
}