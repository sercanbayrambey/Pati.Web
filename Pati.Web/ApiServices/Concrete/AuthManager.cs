using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pati.Data.Dtos;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using Pati.Web.Models;
using Pati.Web.Results;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
                var activeUser = await GetActiveUser();
                await Login(activeUser.Data);
                return new Result();
            }
            else
            {
                return new Result(false, response.StatusCode, responseContent);
            }
        }

        private async Task Login(UserDto user)
        {
            var claims = new List<Claim>  {
                    new Claim(ClaimTypes.Name, user.UserFirstName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("UserId", user.UserId.ToString())
              };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
        }

        public async Task<IDataResult<UserDto>> GetActiveUser()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = _httpClient.GetAsync("user/getUserData").Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var activeUser = JsonConvert.DeserializeObject<UserDto>(responseMessage.Content.ReadAsStringAsync().Result);
                return new DataResult<UserDto>(activeUser, true, HttpStatusCode.OK);
            }
            return new DataResult<UserDto>(null, false, responseMessage.StatusCode);
        }
    }
}
