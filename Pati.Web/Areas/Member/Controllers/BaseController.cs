using Microsoft.AspNetCore.Mvc;
using Pati.Web.CustomFilters;
using Pati.Web.StringConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Member.Controllers
{
    [Area(AreaConsts.Member)]
    [JwtAuthorize]
    public class BaseController : Controller
    {
     
    }
}
