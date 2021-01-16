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
    public class UserApiManager : IUserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Add(UserDto dto)
        {
            return await RegisterAsync(dto);
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

            var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("admin/user", query));
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                return new Result(await response.Content.ReadAsStringAsync(), false);
            }
        }

        public async Task<IDataResult<UserDto>> GetById(int id)
        {
            var query = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<UserDto>(null,false,System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("user/data", query));
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
                return new DataResult<UserDto>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<UserDto>(null, false, response.StatusCode);
            }
        }

        public async Task<IDataResult<List<UserDto>>> List()
        {

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<UserDto>>(null,false,System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.GetAsync("admin/users");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<UserDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<UserDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IResult> RegisterAsync(UserDto userDto)
        {
            if (!userDto.ConfirmPassword.Equals(userDto.Password))
                return new Result("Şifreler uyuşmuyor.", false);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, ContentTypes.JSON);
            var response = await _httpClient.PostAsync("auth/register", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new Result();
            }
            else
            {
                return new Result(false, response.StatusCode, responseContent);
            }
        }

        public async Task<IResult> Update(UserDto dto)
        {
            if (dto.UserId <= 0)
                return new ErrorResult();

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, ContentTypes.JSON);

            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Unauthorized.");


            _httpClient.AddJwtTokenToHeader(token);

            var response = await _httpClient.PutAsync("user", content);

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
