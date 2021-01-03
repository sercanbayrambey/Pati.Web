using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }

    }
}
