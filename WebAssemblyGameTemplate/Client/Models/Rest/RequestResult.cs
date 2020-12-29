using System.Net;

namespace WebAssemblyGameTemplate.Client.Models
{
    public class RequestResult<T>
    {
        public bool Successful { get; set; }
        public T Value { get; set; }
        public string Reason { get; set; }

        public RequestResult(bool successful, T value, string reason)
        {
            Successful = successful;
            Value = value;
            Reason = reason;
        }

        public static RequestResult<T> Success(T value)
            => new RequestResult<T>(true, value, null);

        public static RequestResult<T> Failed(string reason)
            => new RequestResult<T>(false, default, reason);

        public static RequestResult<T> ErrorCode(HttpStatusCode code)
            => new RequestResult<T>(false, default, $"An unknown error occured:\nServer Response: {code}");
    }
}