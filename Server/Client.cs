using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Client
    {
        public void Handle(TcpClient tcpClient, string html)
        {
            string header = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;
            byte[] buffer = Encoding.ASCII.GetBytes(header);
            tcpClient.GetStream().Write(buffer, 0, buffer.Length);
            tcpClient.Close();
        }
    }
}
