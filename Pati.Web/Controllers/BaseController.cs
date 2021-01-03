using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Controllers
{
    public class BaseController : Controller
    {
        public void SuccessAlert(string message)
        {
            string msg = @"<script type='text/javascript'>$.notify({message: '" + message +
                         "'}, {timer: 1000,type:'success',placement: {from: 'top',align:'center'}});</script>";
            TempData["notification"] = msg;
        }

        public void ErrorAlert(string message)
        {
            string msg = @"<script type='text/javascript'>$.notify({message: '" + message +
                         "'}, {timer: 1000,type:'danger',placement: {from: 'top',align:'center'}});</script>";
            TempData["notification"] = msg;
        }

    }
}
