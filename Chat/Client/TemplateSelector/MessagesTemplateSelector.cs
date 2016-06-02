using System.Windows;
using System.Windows.Controls;
using Client.Messages;

namespace Client.TemplateSelector
{
    public class MessagesTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item is ChatMessage)
            {
                var type = item.GetType();

                if (type == typeof(ChatConnectMessage))
                {
                    return element.FindResource("ConnectDataTemplate") as DataTemplate;
                }
                if (type == typeof(ChatDisconnectMessage))
                {
                    return element.FindResource("DisconnectDataTemplate") as DataTemplate;
                }
                if (type == typeof(ChatTextMessage))
                {
                    return element.FindResource("MessageDataTemplate") as DataTemplate;
                }
            }

            return null;
        }
    }
}
