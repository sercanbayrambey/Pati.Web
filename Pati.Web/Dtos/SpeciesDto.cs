using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Dtos
{
    public class SpeciesDto
    {
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public int GenusId { get; set; }
        public string GenusName { get; set; }

    }
}
