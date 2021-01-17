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
    public class ShelterApiManager : IShelterApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShelterApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "admin");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Add(ShelterDto dto)
        {
            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new Result(false, System.Net.HttpStatusCode.BadRequest);

            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PostAsync("shelter", content);

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

            var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("", query));
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                return new Result(await response.Content.ReadAsStringAsync(), false);
            }
        }

        public async Task<IDataResult<ShelterDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<ShelterDto>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("shelter", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<ShelterDto>(await response.Content.ReadAsStringAsync());
                return new DataResult<ShelterDto>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<ShelterDto>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<ShelterDto>>> List()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<ShelterDto>>(null, false, System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync("admin/shelters");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<ShelterDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<ShelterDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<ShelterDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IResult> Update(ShelterDto dto)
        {
            if (dto.ShelterId <= 0)
                return new ErrorResult();

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PutAsync("shelter", content);

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
