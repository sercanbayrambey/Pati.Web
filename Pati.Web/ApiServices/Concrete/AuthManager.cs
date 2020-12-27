using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pati.Data.Dtos;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Models;
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
        public AuthManager(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            httpClient.BaseAddress = new Uri("http://localhost");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.JSON));
        }
        public async Task<bool> SignInAsync(UserLoginDto userLoginDto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userLoginDto), Encoding.UTF8, ContentTypes.JSON);
            var response = await _httpClient.PostAsync("login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _httpContextAccessor.HttpContext.Session.SetString("token", responseContent);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
        }
    }
}
