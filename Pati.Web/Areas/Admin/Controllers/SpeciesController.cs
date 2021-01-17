using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class SpeciesController: BaseController
    {
        private readonly ISpeciesService _speciesService;
        private readonly IGenusService _genusService;
        public SpeciesController(ISpeciesService speciesService, IGenusService genusService)
        {
            _speciesService = speciesService;
            _genusService = genusService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _speciesService.List();
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
            var dto = new SpeciesDto();
            GenerateViewbags(dto);

            return View("AddOrUpdate", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SpeciesDto dto)
        {
            var result = await _speciesService.Add(dto);
            if (result.Success)
            {
                Alert("Ekleme işlemi başarılı.");
                return RedirectToAction("Index");

            }
            else
            {
                ErrorAlert("Ekleme işlemi başarısız." + result.Message);
                GenerateViewbags(dto);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _speciesService.GetById(id);
            if (result.Success)
            {
                GenerateViewbags(result.Data);
                return View("AddOrUpdate", result.Data);
            }
            else
            {
                ErrorAlert("Pet bulunamadı." + result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpeciesDto dto)
        {
            var result = await _speciesService.Update(dto);
            if (result.Success)
            {
                Alert("Güncelleme işlemi başarılı.");
                return RedirectToAction("Update", dto.GenusId);

            }
            else
            {
                ErrorAlert("Güncelleme işlemi başarısız.: " + result.Message);
                GenerateViewbags(dto);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _speciesService.Delete(id);
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

        private void GenerateViewbags(SpeciesDto speciesDto)
        {
            ViewBag.Genusses = new SelectList(_genusService.List().Result.Data, "GenusId", "GenusName",speciesDto.GenusId);
        }

        [HttpPost]
        public async Task<IActionResult> GetSpecies(int genusId)
        {
            var response = await _speciesService.List(genusId);
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
