using Newtonsoft.Json;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using Pati.Web.Results;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Concrete
{
    public class CityApiManager : ICityApiService
    {

        private readonly HttpClient _httpClient;
        public CityApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "cities/");
        }
        public async Task<IDataResult<List<CityDto>>> List()
        {

            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<CityDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<CityDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<CityDto>>(null, false, response.StatusCode);
            }
        }
    }
}
