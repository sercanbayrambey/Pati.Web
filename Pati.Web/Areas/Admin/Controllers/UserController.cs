using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiService _userApiService;
        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }


        public async Task<IActionResult> Index()
        {
            var response = await _userApiService.List();
            if (response.Success)
            {

                return View(response.Data);
            }
            else
            {
                ErrorAlert(response.Message);
                return RedirectToAction("Index","Home");
            }
        }


        public async Task<IActionResult> Add()
        {
            var dto = new UserDto();

            return View("AddOrUpdate", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto dto)
        {
            var result = await _userApiService.Add(dto);
            if (result.Success)
            {
             
                Alert("Ekleme işlemi başarılı.");
                return RedirectToAction("Index");

            }
            else
            {
                Alert("Ekleme işlemi başarısız." + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _userApiService.GetById(id);
            if (result.Success)
            {
                return View("AddOrUpdate", result.Data);
            }
            else
            {
                Alert("Kullanıcı bulunamadı." + result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserDto dto)
        {
            var result = await _userApiService.Update(dto);
            if (result.Success)
            {
                Alert("Güncelleme işlemi başarılı.");
                return RedirectToAction("Update", dto.UserId);

            }
            else
            {
                Alert("Güncelleme işlemi başarısız.: " + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userApiService.Delete(id);
            if (result.Success)
            {
                Alert("Silme işlemi başarılı.");
            }
            else
            {
                Alert("Silme işlemi başarısız." + result.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
