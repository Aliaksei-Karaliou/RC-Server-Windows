using RC.Server;
using Server;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Listen(80);
            Console.WriteLine("Multithread application");
            Console.ReadKey();
        }
    }
}
