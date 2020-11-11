using Pati.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(UserLoginModel userLoginModel);
        void SignOut();
    }
}
