using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pati.Web.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(T data, bool success, HttpStatusCode statusCode) : base(success,statusCode)
        {
            Data = data;
        }
    }
}
