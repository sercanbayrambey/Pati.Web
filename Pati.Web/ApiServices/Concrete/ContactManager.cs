using Microsoft.AspNetCore.Http;
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
    public class ContactManager : IContactService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(StaticVars.BaseAPIAdress);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<List<ContactDto>>> List()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return new DataResult<List<ContactDto>>(null,false,System.Net.HttpStatusCode.Unauthorized);


            _httpClient.AddJwtTokenToHeader(token);
            var response = await _httpClient.GetAsync("admin/contact");
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<List<ContactDto>>(await response.Content.ReadAsStringAsync());
                return new DataResult<List<ContactDto>>(dto, true, response.StatusCode);
            }
            else
            {
                return new DataResult<List<ContactDto>>(null, false, response.StatusCode);
            }
        }

        public async Task<IResult> Send(ContactDto dto)
        {

            var postData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(postData, Encoding.UTF8, "application/json");

            
            var response = await _httpClient.PostAsync("contact", content);

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
