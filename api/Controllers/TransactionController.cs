using System;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TransactionController(ApplicationDBContext context)
        {
            _context = context;
        }

        // -----------------------------------------
        // GET ALL
        // -----------------------------------------
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Book)
                .Include(t => t.Patron)
                .ToListAsync();

            return Ok(transactions);
        }

        // -----------------------------------------
        // BORROW BOOK
        // -----------------------------------------
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRequest req)
        {
            var book = await _context.Books.FindAsync(req.BookId);
            if (book == null)
                return NotFound("Book not found");

            if (book.AvailableCopies <= 0)
                return BadRequest("No copies available");

            var transaction = new Transaction
            {
                BookId = req.BookId,
                PatronId = req.PatronId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = TransactionStatus.CheckedOut,
                LateFees = 0
            };

            book.AvailableCopies--;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        // -----------------------------------------
        // RETURN BOOK
        // -----------------------------------------
        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var t = await _context.Transactions
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (t == null) return NotFound("Transaction not found");

            if (t.Status == TransactionStatus.Returned)
                return BadRequest("Book already returned");

            t.ReturnDate = DateTime.Now;
            t.Status = TransactionStatus.Returned;

            if (t.ReturnDate > t.DueDate)
            {
                int lateDays = (t.ReturnDate.Value - t.DueDate).Days;
                t.LateFees = lateDays * 5; // 5 EGP per day
            }

            t.Book.AvailableCopies++;

            await _context.SaveChangesAsync();
            return Ok(t);
        }

        // -----------------------------------------
        // DELETE
        // -----------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.Transactions.FindAsync(id);
            if (t == null) return NotFound();

            _context.Transactions.Remove(t);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class BorrowRequest
    {
        public int BookId { get; set; }
        public int PatronId { get; set; }
    }
}
