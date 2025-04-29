using System.Net;

namespace application.Server.Services.Models
{
    public class ApplicationHttpResponse<T>
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
