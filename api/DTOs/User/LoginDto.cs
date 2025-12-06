using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class LoginDto
    {
        public string password { get; set; }
        public string email { get; set; }
    }
}