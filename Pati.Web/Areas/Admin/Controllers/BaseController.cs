using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.CustomFilters;
using Pati.Web.StringConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
   /* [JwtAuthorize]*/
    [Area(AreaConsts.Admin)]
    public class BaseController : Controller
    {
        public void Alert(string message)
        {
            string msg = @"<script type='text/javascript'>$.notify({message: '" + message +
                         "'}, {timer: 1000,placement: {from: 'top',align:'center'}});</script>";
            TempData["notification"] = msg;
        }

    }
}
