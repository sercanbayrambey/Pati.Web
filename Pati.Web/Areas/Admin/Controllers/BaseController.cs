using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.StringConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{

    [Area(AreaConsts.Admin)]
 /*   [Authorize(Roles=RoleConsts.AdminOnly)]*/
    public class BaseController : Controller
    {
    }
}
