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
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports
                .Include(r => r.User)
                .Include(r => r.Student)
                .Include(r => r.School)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.User)
                .Include(r => r.Student)
                .Include(r => r.School)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (report == null) return NotFound();
            return report;
        }

        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport(Report report)
        {
            // Validar se o User existe
            var user = await _context.Users.FindAsync(report.UserId);
            if (user == null)
            {
                return BadRequest("User não encontrado");
            }

            // Validar se o Student existe
            var student = await _context.Students.FindAsync(report.StudentId);
            if (student == null)
            {
                return BadRequest("Student não encontrado");
            }

            // Validar se o School existe
            var school = await _context.Schools.FindAsync(report.SchoolId);
            if (school == null)
            {
                return BadRequest("School não encontrada");
            }

            // Definir timestamps
            report.CreatedAt = DateTime.UtcNow;
            report.UpdatedAt = DateTime.UtcNow;

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            // Retornar o report com dados relacionados
            var createdReport = await _context.Reports
                .Include(r => r.User)
                .Include(r => r.Student)
                .Include(r => r.School)
                .FirstOrDefaultAsync(r => r.Id == report.Id);

            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, createdReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.Id) return BadRequest();

            var existingReport = await _context.Reports.FindAsync(id);
            if (existingReport == null) return NotFound();

            // Validar se o User existe
            var user = await _context.Users.FindAsync(report.UserId);
            if (user == null)
            {
                return BadRequest("User não encontrado");
            }

            // Validar se o Student existe
            var student = await _context.Students.FindAsync(report.StudentId);
            if (student == null)
            {
                return BadRequest("Student não encontrado");
            }

            // Validar se o School existe
            var school = await _context.Schools.FindAsync(report.SchoolId);
            if (school == null)
            {
                return BadRequest("School não encontrada");
            }

            // Atualizar campos
            existingReport.StudentId = report.StudentId;
            existingReport.UserId = report.UserId;
            existingReport.SchoolId = report.SchoolId;
            existingReport.Content = report.Content;
            existingReport.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();
            
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 