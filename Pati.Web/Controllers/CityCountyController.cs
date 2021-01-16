using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Controllers
{
    public class CityCountyController : BaseController
    {
        private readonly ICityApiService _cityApiService;
        private readonly ICountyApiService _countyApiService;
        public CityCountyController(ICityApiService cityApiService, ICountyApiService countyApiService)
        {
            _cityApiService = cityApiService;
            _countyApiService = countyApiService;
        }

        [HttpPost]
        public async Task<IActionResult> GetCities()
        {
            var result = await _cityApiService.List();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode((int)result.StatusCode);
        }

        public async Task<IActionResult> GetCounties(int cityId)
        {
            var result = await _countyApiService.List(cityId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode((int)result.StatusCode);
        }
    }
}
