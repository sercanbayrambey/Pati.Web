using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserFirstName { get; set; }
        public string Email { get; set; }

        public string UserBirthDate { get; set; }
        public string UserLocation { get; set; }

        public string Password { get; set; }

        public string UserRole { get; set; }
    }
}
