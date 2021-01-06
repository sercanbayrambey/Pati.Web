using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.CustomFilters;
using Pati.Web.Dtos;
using Pati.Web.StringConsts;

namespace Pati.Web.Areas.Member.Controllers
{
    public class PetsController : BaseController
    {
        private readonly IPetApiService _petApiService;
        public PetsController(IPetApiService petApiService)
        {
            _petApiService = petApiService;
        }
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var request = await _petApiService.List(currentPage);
            if (request.Success)
            {
                return View(request.Data);
            }
            else
            {
                return View(new List<PetDto>());
            }
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
