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
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "pet/ ");
        }

        public async Task<IDataResult<PetDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("getPet", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<PetDto>(await response.Content.ReadAsStringAsync());
                return new DataResult<PetDto>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<PetDto>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<PetDto>>> List(int currentPage = 1)
        {

            var query = new Dictionary<string, string>
            {
                ["p"] = currentPage.ToString()
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
