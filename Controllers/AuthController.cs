using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FessorApi.Data;
using FessorApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace FessorApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context) => _context = context;

        public class LoginDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class ChangePasswordDto
        {
            public string CurrentPassword { get; set; } = null!;
            public string NewPassword { get; set; } = null!;
        }

        public class UserProfileDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Document { get; set; } = null!;
            public string Email { get; set; } = null!;
            public UserRole Role { get; set; }
            public string? ProfilePicture { get; set; }
            public int? SchoolId { get; set; }
            public School? School { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized();

            // Verificar senha usando hash
            if (!PasswordHasher.VerifyPassword(dto.Password, user.Password))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            // Verificar senha atual
            if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, user.Password))
            {
                return BadRequest("Senha atual incorreta");
            }

            // Hash da nova senha
            user.Password = PasswordHasher.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok("Senha alterada com sucesso");
        }

        [HttpGet("/api/me")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Retorna o usuário sem a senha por segurança
            return new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Document = user.Document,
                Email = user.Email,
                Role = user.Role,
                ProfilePicture = user.ProfilePicture,
                SchoolId = user.SchoolId,
                School = user.School,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        [HttpGet("/api/me/admin-only")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> AdminOnlyEndpoint()
        {
            return Ok("Este endpoint só pode ser acessado por administradores!");
        }

        [HttpGet("/api/me/user-demo")]
        [Authorize(Roles = "User,Demo")]
        public ActionResult<string> UserAndDemoEndpoint()
        {
            return Ok("Este endpoint pode ser acessado por usuários e demos!");
        }
    }
} 