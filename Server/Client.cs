using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace RC.Server
{
    class Client
    {
        private Dictionary<string, Func<Dictionary<string, string>, string>> callbackList = new Dictionary<string, Func<Dictionary<string, string>, string>>();
        public Func<string> PageNotFoundCallback { get; set; }

        public void AddListener(string request, Func<Dictionary<string, string>, string> listener)
        {
            callbackList.Add(request, listener);
        }

        public void Handle(TcpClient tcpClient)
        {

            string request = string.Empty;
            byte[] buffer = new byte[4096];
            int count;

            while ((count = tcpClient.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);

                if (request.IndexOf("\r\n\r\n") >= 0)
                {
                    break;
                }
            }

            string path = GetRequestPath(request);
            Uri uri = new Uri("http://localhost" + path);
            var localPath = uri.LocalPath;

            var query=parseQuery(uri.Query);
            string responce= callbackList.ContainsKey(localPath)? callbackList[localPath]?.Invoke(parseQuery(uri.Query)): PageNotFoundCallback?.Invoke();

            if (responce != null)
            {
                string header = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + responce.Length + "\n\n" + responce;
                byte[] buf = Encoding.ASCII.GetBytes(header);
                tcpClient.GetStream().Write(buf, 0, buf.Length);
                tcpClient.Close();
            }
        }

        private string GetRequestPath(string request)
        {
            Match reqMatch = Regex.Match(request, @"(GET|POST) (\/.*) HTTP");
            string result = Uri.UnescapeDataString(reqMatch.Groups[2].Value);
            return result;
        }

        private Dictionary<string, string> parseQuery(string query)
        {
            var result = new Dictionary<string, string>();

            if (query.Length > 0)
            {
                var parameters = query.Substring(1).Split('&');

                foreach (string param in parameters)
                {
                    var parameter = param.Split('=');
                    if (parameter.Length == 2)
                    {
                        result.Add(parameter[0], parameter[1]);
                    }
                }
            }

            return result;
        }
    }
}
