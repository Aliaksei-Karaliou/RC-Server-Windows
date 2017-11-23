using RC.Server;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.AddListener("/mama", ToHtml);
            server.Listen(80);
            server.PageNotFoundCallback = Error;
            Console.WriteLine("Multithread application");
            Console.ReadKey();
        }

        static string ToHtml(Dictionary<string,string> param)
        {
            return "Mama UUUUUh";
        }

        static string Error()
        {
            return "404";
        }
    }
}
