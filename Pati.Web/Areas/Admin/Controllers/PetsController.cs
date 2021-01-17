using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class PetsController : BaseController
    {
        private readonly IPetApiService _petApiService;
        private readonly IFileService _fileService;
        public PetsController(IPetApiService petApiService, IFileService fileService)
        {
            _petApiService = petApiService;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index( int p = 1)
        {
            var response = await _petApiService.List(null,p,false);
            if (response.Success)
            {
                var dataCount = await _petApiService.GetPetCount();
                var dtoList = new StaticPagedList<PetDto>(response.Data, p, 21, dataCount.Data);

                return View(dtoList);
            }
            else
            {
                return View(new PagedList<PetDto>(new List<PetDto>(), p, StaticVars.PaginationPageSize));
            }
        }


        public async Task<IActionResult> Add()
        {
            var dto = new PetDto();

            return View("AddOrUpdate", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PetDto dto,List<IFormFile> Files)
        {
            var result = await _petApiService.Add(dto);
            if (result.Success)
            {
                var addedPetId = Int32.Parse(result.Data);
                var fileNames = await _fileService.UploadFile(Files);
                foreach (var item in fileNames)
                {
                    await _petApiService.AddImageToPet(addedPetId, item);

                }
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
            var result = await _petApiService.GetById(id);
            if (result.Success)
            {
                return View("AddOrUpdate", result.Data);
            }
            else
            {
                Alert("Pet bulunamadı." + result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(PetDto dto)
        {
            var result = await _petApiService.Update(dto);
            if (result.Success)
            {
                Alert("Güncelleme işlemi başarılı.");
                return RedirectToAction("Update", dto.PetId);

            }
            else
            {
                Alert("Güncelleme işlemi başarısız.: " + result.Message);
                return View("AddOrUpdate", dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _petApiService.Delete(id);
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
