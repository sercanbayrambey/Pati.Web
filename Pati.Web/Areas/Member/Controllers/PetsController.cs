using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.CustomFilters;
using Pati.Web.Dtos;
using Pati.Web.StringConsts;
using Pati.Web.Utilities;
using X.PagedList;

namespace Pati.Web.Areas.Member.Controllers
{
    public class PetsController : BaseController
    {
        private readonly IPetApiService _petApiService;
        public PetsController(IPetApiService petApiService)
        {
            _petApiService = petApiService;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            var response = await _petApiService.List(p);
            if (response.Success)
            {
                var dtoList = new StaticPagedList<PetDto>(response.Data,p,20,200);
                
                return View(dtoList);
            }
            else
            {
                return View(new PagedList<PetDto>(new List<PetDto>(),p,StaticVars.PaginationPageSize));
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
