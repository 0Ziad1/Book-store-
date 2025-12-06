using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace api.model
{
    public class User
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public string type { get; set; }
        public bool IsSuspended { get; set; } = false;

    }
}