using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pati.Data.Dtos;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.StringConsts;

namespace Pati.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserApiService _userApiService;
        public HomeController(ILogger<HomeController> logger, IAuthService authService, IUserApiService userApiService)
        {
            _authService = authService;
            _userApiService = userApiService;
        }

        public async Task<IActionResult> Index()
        {
            if (_authService.GetActiveUser().Result.Success)
                return RedirectToAction("Index", "Home", new { area = AreaConsts.Member });
            return View();
        }

        public async Task<IActionResult> Login()
        {
            if (_authService.GetActiveUser().Result.Success)
                return RedirectToAction("Index", "Home", new { area = AreaConsts.Member });
            return View(new UserLoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var loginResult = await _authService.SignInAsync(dto);
            if (loginResult.Success)
            {
                SuccessAlert("Giriş başarılı.");
                return RedirectToAction("Index", "Pets", new { area = AreaConsts.Member });
            }
            else
            {
                ErrorAlert(loginResult.Message);
                return View(dto);
            }
        }

        public IActionResult Register()
        {
            if (_authService.GetActiveUser().Result.Success)
                return RedirectToAction("Index", "Home", new { area = AreaConsts.Member });
            return View(new UserDto());
        }

        [HttpPost]
        public async  Task<IActionResult> Register(UserDto userDto)
        {
            var registerResult = await _userApiService.RegisterAsync(userDto);
            if (registerResult.Success)
            {
                SuccessAlert("Kayıt başarılı.");
                return RedirectToAction("Login");
            }
            else
            {
                ErrorAlert(registerResult.Message);
                return View(userDto);
            }

        }


        public async Task<IActionResult> Logout()
        {
            _authService.SignOut();
            SuccessAlert("Çıkış başarılı.");
            return RedirectToAction("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
