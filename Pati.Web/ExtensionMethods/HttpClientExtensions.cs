using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pati.Web.ExtensionMethods
{
    public static class HttpClientExtensions
    {

        public static void AddJwtTokenToHeader(this HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
