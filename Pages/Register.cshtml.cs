using FessorApi.Data;
using FessorApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FessorApi.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public class InputModel
        {
            [Required]
            [Display(Name = "Nome Completo")]
            public string Name { get; set; } = null!;

            [Required]
            [Display(Name = "Documento (CPF)")]
            public string Document { get; set; } = null!;

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; } = null!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; } = null!;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == Input.Email || u.Document == Input.Document);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Já existe uma conta com este Email ou Documento.");
                return Page();
            }

            var user = new User
            {
                Name = Input.Name,
                Document = Input.Document,
                Email = Input.Email,
                Password = PasswordHasher.HashPassword(Input.Password),
                Role = UserRole.User, // Default role
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Optionally, sign in the user immediately after registration
            // For now, redirecting to the login page to let them log in.
            return RedirectToPage("/Login");
        }
    }
} 