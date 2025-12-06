using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.User;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/book")]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _context;
        public BookController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("manageBooks")]
        public IActionResult CreateBook([FromBody] CreateBookDto dto)
        {
            // 1. Validate empty or null fields
            if (string.IsNullOrWhiteSpace(dto.ISBN) || string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest(new { message = "ISBN and Title are required." });

            // 2. Duplicate ISBN check
            var exists = _context.Books.FirstOrDefault(b => b.ISBN == dto.ISBN);
            if (exists != null)
            {
                return Conflict(new { message = "A book with this ISBN already exists." });
            }

            // 3. Map DTO to model
            var book = new Book
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Authors = dto.Authors,
                Publisher = dto.Publisher,
                PublicationYear = dto.PublicationYear,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies
            };

            // 4. Save to database
            _context.Books.Add(book);
            _context.SaveChanges();

            // 5. Return standard API response
            return CreatedAtAction(nameof(GetBookById),
                new { id = book.Id },
                new { message = "Book added successfully", book });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] updateBookDto updatedBook)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            // Update fields
            existingBook.Title = updatedBook.Title;
            existingBook.Authors = updatedBook.Authors;
            existingBook.Publisher = updatedBook.Publisher;
            existingBook.PublicationYear = updatedBook.PublicationYear;
            existingBook.TotalCopies = updatedBook.TotalCopies;
            existingBook.AvailableCopies = updatedBook.AvailableCopies;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Book updated successfully!" });
        }

        // GET /api/books/{id}
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
                return NotFound(new { message = "Book not found" });

            return Ok(book);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Find the book by ID
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            // Remove the book
            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok(new { message = "Book removed successfully" });
        }
    }
}
