using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class HttpServer
    {
        public readonly int Port;
        private TcpListener _listener;
        private Route _route;
        public HttpServer(int port)
        {
            Port = port;
            _listener = new TcpListener(System.Net.IPAddress.Any, port);
        }
        public void start()
        {
            _listener.Start();
            Console.WriteLine($"Server started on port { Port}");
            AcceptClientsAsync();
        }

        private async void AcceptClientsAsync() 
        {
            while(true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                HandleClientAsync(client);
            }
        }
        private async Task HandleClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                var buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                var requestText = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                var request = HttpRequest.Parse(requestText);
                var response = _route.RouteRequest(request);

                var responseText = response.ToString();
                var responseBytes = Encoding.UTF8.GetBytes(responseText);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }
    }
}
