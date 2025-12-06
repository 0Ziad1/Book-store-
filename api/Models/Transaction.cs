using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using api.model;
namespace api.Models
{
public class Transaction
{
    public int Id { get; set; }
    [Required]
    public int BookId { get; set; }
    public Book Book { get; set; }
    [Required]
    public int PatronId { get; set; }
    public User Patron { get; set; }

    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsOverdue => ReturnDate == null && DateTime.UtcNow > DueDate;
    public decimal LateFees { get; set; } = 0m;
    public TransactionStatus Status { get; set; } = TransactionStatus.CheckedOut;
}

public enum TransactionStatus
{
    CheckedOut,
    Returned,
    Overdue
}
}