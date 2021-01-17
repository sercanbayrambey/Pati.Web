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
    public class GenusManager : IGenusService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISpeciesService _speciesService;
        public GenusManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ISpeciesService speciesService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
            _httpContextAccessor = httpContextAccessor;
            _speciesService = speciesService;
        }

        public async Task<IResult> Add(GenusDto dto)
        {
            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new Result(false,System.Net.HttpStatusCode.BadRequest);

            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PostAsync("admin/genus", content);

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

            var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("admin/genus", query));
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                return new Result(await response.Content.ReadAsStringAsync(), false);
            }
        }

        public async Task<IDataResult<GenusDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<GenusDto>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("genus", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<GenusDto>(await response.Content.ReadAsStringAsync());
                return new DataResult<GenusDto>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<GenusDto>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<GenusDto>>> List()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<GenusDto>>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync("allGenuses");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<GenusDto>>(await response.Content.ReadAsStringAsync());
                dto.ForEach(x => x.SpeciesDtos = _speciesService.List().Result.Data);
                return new DataResult<List<GenusDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<GenusDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IResult> Update(GenusDto dto)
        {
            if (dto.GenusId <= 0)
                return new ErrorResult();

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PutAsync("admin/genus", content);

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
