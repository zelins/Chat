namespace Client.Messages
{
    public class ChatConnectMessage : ChatMessage
    {
        public ChatConnectMessage(string nickName) : base(nickName)
        {
        }
    }
}