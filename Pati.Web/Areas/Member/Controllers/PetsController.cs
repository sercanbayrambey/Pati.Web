using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pati.Web.CustomFilters;
using Pati.Web.Dtos;
using Pati.Web.StringConsts;

namespace Pati.Web.Areas.Member.Controllers
{
    public class PetsController : BaseController
    {
        public IActionResult Index()
        {
            List<PetDto> pets = new List<PetDto>();

            pets.Add(new PetDto { PetName = "Reçel", PetBirthDate = DateTime.Now, PetAdditionInfo = "Çok güzel bir hayvan", Gender = "Erkek", Genus = "Sokak Kedisi" });
            pets.Add(new PetDto { PetName = "Çakıl", PetBirthDate = DateTime.Now, PetAdditionInfo = "Çok güzel bir hayvan", Gender = "Karı", Genus = "Muhabbet kuşu" });
            return View(pets);
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
