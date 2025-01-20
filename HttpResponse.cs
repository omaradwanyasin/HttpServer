namespace HttpServer
{
    public class HttpResponse
    {
        public int StatusCode { get; set; } = 200;  // Default to OK
        public string Body { get; set; } = string.Empty;

        public override string ToString()
        {
            var responseLine = $"HTTP/1.1 {StatusCode} {GetStatusDescription(StatusCode)}";
            var headers = "Content-Type: text/html\r\n\r\n";  // You can add more headers here.
            return $"{responseLine}\r\n{headers}\r\n{Body}";
        }

        // Method to get a status description based on the code
        private string GetStatusDescription(int code)
        {
            return code switch
            {
                200 => "OK",
                404 => "Not Found",
                _ => "Internal Server Error"
            };
        }
    }

}
