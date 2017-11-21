using RC_Server_Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
