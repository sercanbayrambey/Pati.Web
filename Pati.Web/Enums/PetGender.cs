using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Enums
{
    public enum PetGender
    {
        [Display(Name ="Dişi")]
        Female=0,
        [Display(Name = "Erkek")]
        Male
    }
}
