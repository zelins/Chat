using System;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Server";

            var server = new Server();

            server.StartListen();

            Console.ReadLine();
        }
    }
}