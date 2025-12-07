using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.User;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _context;
        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.users.ToListAsync();
            var usersDto = users.Select(u => u.ToUserDto()).ToList();
            return Ok(usersDto);
        }


        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
            var user = _context.users.Find(id).ToUserDto();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateUserDto updatedUser)
        {
            var existingUser = await _context.users.FirstOrDefaultAsync(u => u.id == id);

            if (existingUser == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            // Update fields
            existingUser.Name = updatedUser.Name;
            existingUser.phoneNumber = updatedUser.phoneNumber;
            existingUser.IsSuspended = updatedUser.IsSuspended;
            existingUser.email = updatedUser.email;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully!" });
        }
        [HttpPost("register")]
        public IActionResult CreateNewUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.email))
            {
                return BadRequest("Email is required.");
            }

            // Check if email already exists
            var existingUser = _context.users.FirstOrDefault(u => u.email == userDto.email);
            if (existingUser != null)
            {
                return Conflict(new { message = "This email is already taken." });
                // Conflict = HTTP 409
            }
            var userModel = userDto.ToUserFromCreateDto();
            _context.users.Add(userModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { id = userModel.id }, userDto.ToUserFromCreateDto());
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = _context.users.FirstOrDefault(x => x.email == login.email);

            if (user == null || user.password != login.password)
                return Unauthorized(new { message = "Invalid email or password" });

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.email),
        new Claim(ClaimTypes.Role, user.type ?? "User")
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok(new { message = "Login successful" });
        }


    }
}