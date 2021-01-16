using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IUserApiService
    {
        Task<IDataResult<List<UserDto>>> List();
        Task<IDataResult<UserDto>> GetById(int id);
        Task<IResult> Add(UserDto dto);
        Task<IResult> Update(UserDto dto);
        Task<IResult> Delete(int id);
        Task<IResult> RegisterAsync(UserDto userDto);
    }
}
