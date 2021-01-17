using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.CustomFilters;
using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.StringConsts;
using Pati.Web.Utilities;
using X.PagedList;

namespace Pati.Web.Areas.Member.Controllers
{
    public class PetsController : BaseController
    {
        private readonly IPetApiService _petApiService;
        private readonly IGenusService _genusService;
        public PetsController(IPetApiService petApiService, IGenusService genusService)
        {
            _petApiService = petApiService;
            _genusService = genusService;
        }
        public async Task<IActionResult> Index(PetListParameters petListParameters,int p = 1)
        {
            var result = await _genusService.List();
            if (result.Success)
            {
                ViewBag.Categories = result.Data;
            }


            var response = await _petApiService.List(null,p);
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

        public async Task<IActionResult> Details(int id)
        {
            var response = await _petApiService.GetById(id);

            if (response.Success)
            {
                if (response.Data == null)
                {
                    return NotFound();
                }

                return View(response.Data);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }

        }
    }
}
