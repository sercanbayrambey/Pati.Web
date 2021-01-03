using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ViewComponents
{
    public class TopNavbar : ViewComponent
    {
        private readonly IAuthService _authService;
        public TopNavbar(IAuthService authService)
        {
            _authService = authService;

        }
        public IViewComponentResult Invoke()
        {
            var activeUserReq = _authService.GetActiveUser().Result;
            if (activeUserReq.Success)
            {
                var model = new UserDto { UserFirstName = activeUserReq.Data.UserFirstName};
                return View("LoggedUserNavbar", model);
            }
            return View("GuestNavbar"); 
        }
    }
}
