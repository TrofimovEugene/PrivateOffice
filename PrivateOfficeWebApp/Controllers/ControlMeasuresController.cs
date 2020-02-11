using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Data;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlMeasuresController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public ControlMeasuresController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/ControlMeasures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlMeasures>>> GetControlMeasures()
        {
            return await _context.ControlMeasures.ToListAsync();
        }

        // GET: api/ControlMeasures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlMeasures>> GetControlMeasures(int id)
        {
            var controlMeasures = await _context.ControlMeasures.FindAsync(id);

            if (controlMeasures == null)
            {
                return NotFound();
            }

            return controlMeasures;
        }

        // PUT: api/ControlMeasures/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlMeasures(int id, ControlMeasures controlMeasures)
        {
            if (id != controlMeasures.IdControlMeasures)
            {
                return BadRequest();
            }

            _context.Entry(controlMeasures).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlMeasuresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ControlMeasures
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ControlMeasures>> PostControlMeasures(ControlMeasures controlMeasures)
        {
            _context.ControlMeasures.Add(controlMeasures);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlMeasures", new { id = controlMeasures.IdControlMeasures }, controlMeasures);
        }

        // DELETE: api/ControlMeasures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ControlMeasures>> DeleteControlMeasures(int id)
        {
            var controlMeasures = await _context.ControlMeasures.FindAsync(id);
            if (controlMeasures == null)
            {
                return NotFound();
            }

            _context.ControlMeasures.Remove(controlMeasures);
            await _context.SaveChangesAsync();

            return controlMeasures;
        }

        private bool ControlMeasuresExists(int id)
        {
            return _context.ControlMeasures.Any(e => e.IdControlMeasures == id);
        }
    }
}
