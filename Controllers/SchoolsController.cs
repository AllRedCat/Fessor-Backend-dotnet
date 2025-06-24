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
    public class SchoolsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SchoolsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools() => await _context.Schools.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return NotFound();
            return school;
        }

        [HttpPost]
        public async Task<ActionResult<School>> CreateSchool(School school)
        {
            school.CreatedAt = DateTime.UtcNow;
            school.UpdatedAt = DateTime.UtcNow;

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, School school)
        {
            if (id != school.Id) return BadRequest();

            var existingSchool = await _context.Schools.FindAsync(id);
            if (existingSchool == null) return NotFound();

            existingSchool.Name = school.Name;
            existingSchool.Address = school.Address;
            existingSchool.City = school.City;
            existingSchool.State = school.State;
            existingSchool.ZipCode = school.ZipCode;
            existingSchool.Principal = school.Principal;
            existingSchool.Phone = school.Phone;
            existingSchool.Email = school.Email;
            existingSchool.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return NotFound();
            
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 