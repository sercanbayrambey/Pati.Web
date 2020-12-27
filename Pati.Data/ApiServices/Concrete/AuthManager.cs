using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            httpClient.BaseAddress = new Uri("URL");
        }
        public async Task<bool> SignInAsync(UserLoginModel userLoginModel)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userLoginModel), Encoding.UTF8);
            var response = await _httpClient.PostAsync("", stringContent);

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
