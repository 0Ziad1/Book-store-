using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class UserDto
    {
        public int id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}