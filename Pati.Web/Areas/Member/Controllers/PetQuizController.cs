using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pati.Web.Areas.Member.Controllers
{
    public class PetQuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
