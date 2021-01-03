using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pati.Data.Dtos;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Models;
using Pati.Web.Results;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.JSON));
        }
        public async Task<IResult> SignInAsync(UserLoginDto userLoginDto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userLoginDto), Encoding.UTF8, ContentTypes.JSON);
            var response = await _httpClient.PostAsync("auth/login", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _httpContextAccessor.HttpContext.Session.SetString("token", responseContent);
                return new Result();
            }
            else
            {
                return new Result(false, response.StatusCode, responseContent);
            }
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
        }
    }
}
