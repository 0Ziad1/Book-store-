using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class getUserDto
    {
        public int id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool IsSuspended { get; set; } = false;
    }
}