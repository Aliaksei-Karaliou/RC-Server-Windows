
using RC.Server;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.AddListener("/mama", FileExplorerApi.API.Get);
            server.PageNotFoundCallback = error;
            server.Listen(80);
            Console.WriteLine("Multithread application");
            Console.ReadKey();
        }

        static string error()
        {
            return "404";
        }
    }
}
