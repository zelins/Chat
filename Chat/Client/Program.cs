using System;

namespace Client
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Chat Client";

            var client = new ChatClient();

            var name = Console.ReadLine();

            client.Login(name);

            while (true)
            {
                var message = Console.ReadLine();
                client.EnqueueMessage(message);
            }
        }
    }
}