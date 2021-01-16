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
        public string UserLastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserMail { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string UserSex { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; } = "0";
        public int CountyId { get; set; }
         
    }
}
