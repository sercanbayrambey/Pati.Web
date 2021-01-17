using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IShelterApiService
    {
        Task<IDataResult<List<ShelterDto>>> List();
        Task<IDataResult<ShelterDto>> GetById(int id);
        Task<IResult> Add(ShelterDto dto);
        Task<IResult> Update(ShelterDto dto);
        Task<IResult> Delete(int id);
    }
}
