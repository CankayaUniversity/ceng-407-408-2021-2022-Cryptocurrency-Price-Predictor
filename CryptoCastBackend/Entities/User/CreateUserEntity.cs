using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class CreateUserEntity
    {
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }    
        public string? Tckn { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public int RoleId { get; set; }
        public string? LanguageCode { get; set; }

    }
}
