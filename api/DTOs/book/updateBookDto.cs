using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class updateBookDto
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}