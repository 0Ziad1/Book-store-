using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.book
{
    public class BorrowRequest
    {
        public int BookId { get; set; }
        public int PatronId { get; set; }
    }
}