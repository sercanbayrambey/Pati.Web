using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class GenusController : BaseController
    {
        private readonly IGenusService _genusService;
        public GenusController(IGenusService genusService)
        {
            _genusService = genusService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _genusService.List();
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
            var dto = new GenusDto();

            return View("AddOrUpdate", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenusDto dto)
        {
            var result = await _genusService.Add(dto);
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
            var result = await _genusService.GetById(id);
            if (result.Success)
            {
                return View("AddOrUpdate", result.Data);
            }
            else
            {
                ErrorAlert("Pet bulunamadı." + result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(GenusDto dto)
        {
            var result = await _genusService.Update(dto);
            if (result.Success)
            {
                Alert("Güncelleme işlemi başarılı.");
                return RedirectToAction("Update", dto.GenusId);

            }
            else
            {
                ErrorAlert("Güncelleme işlemi başarısız.: " + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _genusService.Delete(id);
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

    }
}
