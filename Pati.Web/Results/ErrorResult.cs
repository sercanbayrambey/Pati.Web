namespace Pati.Web.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false)
        {

        }

        public ErrorResult(string message, bool success) : base(message, success)
        {

        }

        public ErrorResult(string message) : base(message, false)
        {

        }
    }
}
