using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Models
{
    public class PetPictureApiModel
    {
        public int PictureId { get; set; }
        public int PetId { get; set; }
        public string PictureUrl { get; set; }

    }
}
