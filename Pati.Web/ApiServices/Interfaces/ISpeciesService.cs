using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface ISpeciesService
    {
        Task<IDataResult<List<SpeciesDto>>> List();
        Task<IDataResult<SpeciesDto>> GetById(int id);
        Task<IResult> Add(SpeciesDto dto);
        Task<IResult> Update(SpeciesDto dto);
        Task<IResult> Delete(int id);
    }
}
