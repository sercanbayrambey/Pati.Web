using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Dtos
{
    public class PetDto
    {
        public int PetId { get; set; }
        public int ShelterId { get; set; }
        public int SpeciesId { get; set; }
        public string PetName { get; set; }
        public string PetWeight { get; set; }
        public string PetAdditionInfo { get; set; }
        public string PetVaccineInfo { get; set; }
        public DateTime PetBirthDate { get; set; }
        public bool HasPassport { get; set; }
        public string Genus { get; set; }
        public string Gender { get; set; }

    }
}
