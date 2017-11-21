using System.Net;
using System.Net.Sockets;

namespace RC_Server_Windows
{
    public class Server
    {
        private TcpListener Listener;

        public void Listen(int port)
        {
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();

            while (true)
            {
                Client client = new Client();
                client.Handle(Listener.AcceptTcpClient(), "RC Server works");
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
