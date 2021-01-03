using Pati.Data.Dtos;
using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IAuthService
    {
        Task<IResult> SignInAsync(UserLoginDto userLoginModel);
        Task<IDataResult<UserDto>> GetActiveUser();
        void SignOut();
    }
}
