using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Chat Client";

            var client = new ChatClient();

            var name = Console.ReadLine();

            client.ConnectToServer(name);

            while (true)
            {
                var message = Console.ReadLine();
                client.EnqueueMessage(message);
            }
        }
    }
}
