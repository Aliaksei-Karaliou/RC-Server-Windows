using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RC.Server
{
    public class Server
    {
        private TcpListener Listener;

        private const int MIN_THREADS = 1;
        private const int MAX_THREADS = 5;

        public Server()
        {
            ThreadPool.SetMinThreads(MIN_THREADS, MIN_THREADS);
            ThreadPool.SetMaxThreads(MAX_THREADS, MAX_THREADS);
        }

        public void Listen(int port)
        {
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleInThread));
        }

        private void HandleInThread(object obj)
        {
            while (true)
            {
                new Client().Handle(Listener.AcceptTcpClient(), "RC");
            }
 
        }

        ~Server()
        {
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
    }
}
