using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class CreateUserDto
    {
        public string Name { get; set; } = String.Empty;
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string email {get;set;}
        
    }
}