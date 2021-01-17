using Microsoft.AspNetCore.Http;
using Pati.Web.Enums;
using System;
using System.Collections.Generic;

namespace Pati.Web.Dtos
{
    public class PetDto
    {
        public int PetId { get; set; }
        public int? ShelterId { get; set; }
        public int? SpeciesId { get; set; }
        public string PetName { get; set; }
        public string PetWeight { get; set; }
        public string PetHeight { get; set; }
        public string PetAdditionInfo { get; set; }
        public string PetVaccineInfo { get; set; }
        public DateTime PetBirthDate { get; set; }
        public bool HasPassport { get; set; }
        public string Genus { get; set; }
        public PetGender PetGender { get; set; }
        public List<string> Images { get; set; }
        public int CountyId { get; set; }
        public int GenusId { get; set; }



    }
}
