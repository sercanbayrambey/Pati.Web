using System.Net;

namespace Pati.Web.Results
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
        HttpStatusCode StatusCode { get; }
    }
}
