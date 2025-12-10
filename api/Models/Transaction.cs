using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        // FK → Book
        public int BookId { get; set; }
        public Book Book { get; set; }

        // FK → Patron (User)
        public int PatronId { get; set; }
        public api.model.User Patron { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public TransactionStatus Status { get; set; }
        public int LateFees { get; set; }
    }

    public enum TransactionStatus
    {
        CheckedOut,
        Returned,
        Overdue
    }
}
