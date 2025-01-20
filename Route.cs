using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Route
    {
        private readonly Dictionary<string, Func<HttpRequest, HttpResponse>> _getRoutes = new();
        private readonly Dictionary<string, Func<HttpRequest, HttpResponse>> _postRoutes = new();
        public void AddGetRoute(string path, Func<HttpRequest, HttpResponse> handler)
        {
            _getRoutes[path] = handler;
        }

        // Method to add POST routes
        public void AddPostRoute(string path, Func<HttpRequest, HttpResponse> handler)
        {
            _postRoutes[path] = handler;
        }
        public HttpResponse RouteRequest(HttpRequest request)
        {
            if (request.Method == "GET" && _getRoutes.ContainsKey(request.Path))
            {
                return _getRoutes[request.Path](request);
            }
            if (request.Method == "POST" && _postRoutes.ContainsKey(request.Path))
            {
                return _postRoutes[request.Path](request);
            }
            return new HttpResponse
            {
                StatusCode = 404,
                Body = "Not Found"
            };
        }

    }

}
