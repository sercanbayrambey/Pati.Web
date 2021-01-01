using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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


    }
}
