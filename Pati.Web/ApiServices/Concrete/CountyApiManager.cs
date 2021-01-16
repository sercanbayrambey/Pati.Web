using Microsoft.AspNetCore.WebUtilities;
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
    public class CountyApiManager : ICountyApiService
    {
        private readonly HttpClient _httpClient;
        public CountyApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "counties/");
        }
        public async Task<IDataResult<List<CountyDto>>> List(int cityId)
        {
            if (cityId <= 0 || cityId>81)
            {
                return new DataResult<List<CountyDto>>(null, false, System.Net.HttpStatusCode.BadRequest);
            }


            var query = new Dictionary<string, string>
            {
                ["id"] = cityId.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("",query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<CountyDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<CountyDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<CountyDto>>(null, false, response.StatusCode);
            }
        }
      
    }
}
