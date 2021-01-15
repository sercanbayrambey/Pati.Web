using Microsoft.AspNetCore.Mvc;
using Pati.Web.ApiServices.Concrete;
using Pati.Web.ApiServices.Interfaces;
using Pati.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactManager;
        public ContactController(IContactService contactManager)
        {
            _contactManager = contactManager;    
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactDto contactDto)
        {
            if (contactDto == null)
                return NotFound();

            var result = await _contactManager.Send(contactDto);
            if (result.Success)
            {
                SuccessAlert("Mesajınız gönderildi.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
