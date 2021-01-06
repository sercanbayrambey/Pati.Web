using Newtonsoft.Json;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
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
        public UserApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
        }

        public async Task<IResult> RegisterAsync(UserDto userDto)
        {
            if (!userDto.ConfirmPassword.Equals(userDto.Password))
                return new Result("Şifreler uyuşmuyor.", false);
            userDto.UserBirthDate = "21-06-2000";

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
    }
}
