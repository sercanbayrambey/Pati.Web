using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IGenusService
    {
        Task<IDataResult<List<GenusDto>>> List();
        Task<IDataResult<GenusDto>> GetById(int id);
        Task<IResult> Add(GenusDto dto);
        Task<IResult> Update(GenusDto dto);
        Task<IResult> Delete(int id);
    }
}
