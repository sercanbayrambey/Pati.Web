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
            var request = await _petApiService.List(p);
            if (request.Success)
            {
                var dtoList = new StaticPagedList<PetDto>(request.Data,p,20,200);
                
                return View(dtoList);
            }
            else
            {
                return View(new PagedList<PetDto>(new List<PetDto>(),p,StaticVars.PaginationPageSize));
            }
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
