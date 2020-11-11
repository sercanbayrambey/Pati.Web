using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pati.Web.ApiServices.Concrete;
using Pati.Web.ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ExtensionMethods
{
    public static class DIExtension
    {

        public static void AddDIScopes(this IServiceCollection services)
        {
            services.AddHttpClient<ICityApiService, CityApiManager>();
            services.AddHttpClient<ICountyApiService, CountyApiManager>();
            services.AddHttpClient<IPetApiService, PetApiManager>();
            services.AddHttpClient<IRoleApiService, RoleApiManager>();
            services.AddHttpClient<IShelterApiService, ShelterApiManager>();
            services.AddHttpClient<IUserApiService, UserApiManager>();
            services.AddHttpClient<IAuthService, AuthManager>();

        }
    }
}
