using System.Net;

namespace Pati.Web.Results
{
    public class Result : IResult
    {
        public Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public Result()
        {
            Success = true;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, HttpStatusCode httpStatusCode)
        {
            Success = success;
        }
        public Result(bool success, HttpStatusCode httpStatusCode, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(string message)
        {
            Success = true;
            Message = message;
        }

        public string Message { get; }

        public bool Success { get;  }
        public HttpStatusCode StatusCode { get; }
    }
}
