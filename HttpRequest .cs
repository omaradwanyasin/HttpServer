using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class HttpRequest
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
        public string Body { get; set; }
        public static HttpRequest Parse(string rawRequest)
        {
            var request = new HttpRequest();
            // Split request into lines
            var lines = rawRequest.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var requestLine = lines[0].Split(' ');
            if (requestLine.Length < 3)
                throw new InvalidOperationException("Invalid HTTP request line.");
            request.Method = requestLine[0];    
            request.Path = requestLine[1];     
            request.HttpVersion = requestLine[2]; 
            // Parse Headers
            int i = 1;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var headerParts = lines[i].Split(':', 2);
                if (headerParts.Length == 2)
                {
                    var key = headerParts[0].Trim();
                    var value = headerParts[1].Trim();
                    request.Headers[key] = value;
                }
                i++;
            }
            if (lines.Length > i + 1)
            {
                request.Body = string.Join("\r\n", lines[(i + 1)..]);
            }

            return request;
        }
    }
}
