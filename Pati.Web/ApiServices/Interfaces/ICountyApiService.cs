﻿using Pati.Web.Dtos;
using Pati.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface ICountyApiService
    {
        Task<IDataResult<List<CountyDto>>> List(int cityId);
    }
}
