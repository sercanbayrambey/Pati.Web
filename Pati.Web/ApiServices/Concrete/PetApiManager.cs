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
    public class PetApiManager : IPetApiService
    {
        private readonly HttpClient _httpClient;

        public PetApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
        }

        public async Task<IDataResult<List<PetDto>>> List(int currentPage = 1)
        {
            var query = new Dictionary<string, string>
            {
                ["pageId"] = currentPage.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("getPets", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<PetDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<PetDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<PetDto>>(null, false, response.StatusCode);
            }
        }
    }
}
