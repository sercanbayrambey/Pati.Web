using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class ShelterController : BaseController
    {
        private readonly IShelterApiService _shelterService;
        public ShelterController(IShelterApiService shelterService)
        {
            _shelterService = shelterService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _shelterService.List();
            if (response.Success)
            {

                return View(response.Data);
            }
            else
            {
                ErrorAlert(response.Message);
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<IActionResult> Add()
        {
            var dto = new ShelterDto();

            return View("AddOrUpdate", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShelterDto dto)
        {
            var result = await _shelterService.Add(dto);
            if (result.Success)
            {
                Alert("Ekleme işlemi başarılı.");
                return RedirectToAction("Index");

            }
            else
            {
                ErrorAlert("Ekleme işlemi başarısız." + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _shelterService.GetById(id);
            if (result.Success)
            {
                return View("AddOrUpdate", result.Data);
            }
            else
            {
                ErrorAlert("Bir hata oluştu." + result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ShelterDto dto)
        {
            var result = await _shelterService.Update(dto);
            if (result.Success)
            {
                Alert("Güncelleme işlemi başarılı.");
                return RedirectToAction("Update", dto.ShelterId);

            }
            else
            {
                ErrorAlert("Güncelleme işlemi başarısız.: " + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _shelterService.Delete(id);
            if (result.Success)
            {
                Alert("Silme işlemi başarılı.");
            }
            else
            {
                ErrorAlert("Silme işlemi başarısız." + result.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetGenusses()
        {
            var response = await _shelterService.List();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

    }
}
