﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.CustomFilters;
using Pati.Web.StringConsts;

namespace Pati.Web.Areas.Member.Controllers
{
   
    public class PetQuizController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
