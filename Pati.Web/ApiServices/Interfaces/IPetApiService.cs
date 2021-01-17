using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IPetApiService
    {
        Task<IDataResult<List<PetDto>>> List(PetListParameters petListParameters,int currentPage = 1, bool getImages = true );
        Task<IDataResult<PetDto>> GetById(int id);
        Task<IDataResult<string>> Add(PetDto dto);
        Task<IResult> AddImageToPet(int petId, string fileName);

        Task<IResult> Update(PetDto dto);
        Task<IResult> Delete(int id);
        Task<List<string>> GetPetImages(int petId);

        Task<IDataResult<int>> GetPetCount();
    }
}
