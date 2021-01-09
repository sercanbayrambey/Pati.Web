using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IPetApiService
    {
        Task<IDataResult<List<PetDto>>> List(int currentPage = 1);
        Task<IDataResult<PetDto>> GetById(int id);
        Task<IResult> Add(PetDto dto);

        Task<IResult> Update(PetDto dto);
        Task<IResult> Delete(int id);
    }
}
