using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Dtos
{
    public class CountyDto
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int CityId { get; set; }
    }
}
