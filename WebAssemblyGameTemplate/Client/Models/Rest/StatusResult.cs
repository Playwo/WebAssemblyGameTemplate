using System.Net;

namespace WebAssemblyGameTemplate.Client.Models
{
    public sealed class StatusResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode => ((int) StatusCode >= 200) && ((int) StatusCode <= 299);
        public T Result { get; set; }

        public StatusResult(HttpStatusCode statusCode, T result)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public static StatusResult<T> Failed(HttpStatusCode statusCode)
            => new StatusResult<T>(statusCode, default);

        public static StatusResult<T> Success(HttpStatusCode statusCode, T result)
            => new StatusResult<T>(statusCode, result);
    }
}