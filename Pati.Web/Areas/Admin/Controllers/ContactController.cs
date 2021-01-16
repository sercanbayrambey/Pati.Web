using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _contactService.List();
            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                ErrorAlert("Beklenmedik bir hata oluştu.");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
