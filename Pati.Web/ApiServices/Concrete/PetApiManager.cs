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
    public class PetApiManager : IPetApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PetApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress + "pet/");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<string>> Add(PetDto dto)
        {

            var postData = JsonConvert.SerializeObject(dto);
            
            var content = new StringContent(postData, Encoding.UTF8,ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<string>("Unauthorized.",false,System.Net.HttpStatusCode.NotFound);

            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PostAsync("",content);

            if (response.IsSuccessStatusCode)
            {
                return new DataResult<string>(await response.Content.ReadAsStringAsync(), true, System.Net.HttpStatusCode.NotFound);
            }
            else
            {
                return new DataResult<string>(await response.Content.ReadAsStringAsync(), false, System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<IResult> AddImageToPet(int petId, string fileName)
        {
            var query = new Dictionary<string, string>
            {
                ["PetId"] = petId.ToString(),
                ["PictureUrl"] = fileName
            };
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, ContentTypes.JSON);



   /*         var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<string>("Unauthorized.", false, System.Net.HttpStatusCode.NotFound);

            _httpClient.AddJwtTokenToHeader(token);*/

            var response = await _httpClient.PostAsync("photo",content);
            if (response.IsSuccessStatusCode)
            {
                return new Result();
            }
            else
            {
                return new Result(false);
            }
        }

        public Task<IResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PetDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("", query));
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

        public async Task<IDataResult<int>> GetPetCount()
        {
            var response = await _httpClient.GetAsync("petCount");
            if (response.IsSuccessStatusCode)
            {
                return new DataResult<int>(Convert.ToInt32(await response.Content.ReadAsStringAsync()), true, response.StatusCode);
            }
            else
            {
                return new DataResult<int>(0, false, response.StatusCode);
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

        public async Task<IResult> Update(PetDto dto)
        {
            if (dto.PetId <= 0)
                return new ErrorResult();

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");

            var petToUpdate = await GetById(dto.PetId);

            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PutAsync("", content);

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
