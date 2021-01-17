using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using Pati.Web.ExtensionMethods;
using Pati.Web.Results;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Concrete
{
    public class SpeciesManager : ISpeciesService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenusService _genusService;
        public SpeciesManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IGenusService genusService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "");
            _httpContextAccessor = httpContextAccessor;
            _genusService = genusService;
        }

        public async Task<IResult> Add(SpeciesDto dto)
        {
            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new Result(false, System.Net.HttpStatusCode.BadRequest);

            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PostAsync("admin/species", content);

            if (response.IsSuccessStatusCode)
            {
                return new Result();
            }
            else
            {
                return new Result(false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> Delete(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };


            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("admin/species", query));
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                return new Result(await response.Content.ReadAsStringAsync(), false);
            }
        }

        public async Task<IDataResult<SpeciesDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<SpeciesDto>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("getSpecies", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<SpeciesDto>(await response.Content.ReadAsStringAsync());
                return new DataResult<SpeciesDto>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<SpeciesDto>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<SpeciesDto>>> List()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<SpeciesDto>>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync("allSpecies");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<SpeciesDto>>(await response.Content.ReadAsStringAsync());
                dto.ForEach(x => x.GenusName = _genusService.GetById(x.GenusId).Result.Data?.GenusName);
                return new DataResult<List<SpeciesDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<SpeciesDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<SpeciesDto>>> List(int genusId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<SpeciesDto>>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var query = new Dictionary<string, string>
            {
                ["id"] = genusId.ToString()
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("species",query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<SpeciesDto>>(await response.Content.ReadAsStringAsync());
                dto.ForEach(x => x.GenusName = _genusService.GetById(x.GenusId).Result.Data?.GenusName);
                return new DataResult<List<SpeciesDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<SpeciesDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IResult> Update(SpeciesDto dto)
        {
            if (dto.GenusId <= 0)
                return new ErrorResult();

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PutAsync("admin/species", content);

            if (response.IsSuccessStatusCode)
            {
                return new Result();
            }
            else
            {
                return new ErrorResult(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
