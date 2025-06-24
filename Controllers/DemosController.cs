using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FessorApi.Data;
using FessorApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace FessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class DemosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DemosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Demo>>> GetDemos() => await _context.Demos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Demo>> GetDemo(long id)
        {
            var demo = await _context.Demos.FindAsync(id);
            if (demo == null) return NotFound();
            return demo;
        }

        [HttpPost]
        public async Task<ActionResult<Demo>> CreateDemo(Demo demo)
        {
            _context.Demos.Add(demo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDemo), new { id = demo.Id }, demo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDemo(long id, Demo demo)
        {
            if (id != demo.Id) return BadRequest();
            _context.Entry(demo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemo(long id)
        {
            var demo = await _context.Demos.FindAsync(id);
            if (demo == null) return NotFound();
            _context.Demos.Remove(demo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 