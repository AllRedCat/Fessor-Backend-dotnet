using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FessorApi.Data;
using FessorApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace FessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _context.Users.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                CreatedAt = user.CreatedAt,
            };
            
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.Name = user.Name;
            existingUser.Document = user.Document;
            existingUser.Email = user.Email;
            
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = PasswordHasher.HashPassword(user.Password);
            }
            
            existingUser.Role = user.Role;
            existingUser.ProfilePicture = user.ProfilePicture;
            existingUser.SchoolId = user.SchoolId;
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 