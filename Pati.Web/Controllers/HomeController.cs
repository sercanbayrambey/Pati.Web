using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pati.Data.Dtos;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Models;
using Pati.Web.StringConsts;

namespace Pati.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAuthService _authService;
        public HomeController(ILogger<HomeController> logger, IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
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
                ErrorAlert("dasdsdadas");
                ErrorAlert(loginResult.Message);
                return View(dto);
            }
        }

        public IActionResult Register()
        {
            return View();
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
