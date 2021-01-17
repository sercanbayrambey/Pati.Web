using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Models
{
    public class PetListParameters
    {
        public int? GenusId { get; set; }
        public int? SpeciesId { get; set; }
        public string SearchTerm { get; set; }

    }
}
