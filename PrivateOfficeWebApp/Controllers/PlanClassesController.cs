using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Data;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanClassesController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public PlanClassesController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/PlanClasses
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PlanClasses>>> GetPlanClasses()
        {
            return await _context.PlanClasses.ToListAsync();
        }

        // GET: api/PlanClasses/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PlanClasses>> GetPlanClasses(int id)
        {
            var planClasses = await _context.PlanClasses.FindAsync(id);

            if (planClasses == null)
            {
                return NotFound();
            }

            return planClasses;
        }

        [HttpGet("GetPlanClassesFromClasses/id={id}")]
        [Authorize]
        public async Task<PlanClasses> GetPlanClassesFromClasses(int id)
        {
            var planClasses = await _context.PlanClasses.ToListAsync();
            PlanClasses plan = new PlanClasses();

            foreach (var planClass in planClasses)
            {
                if (planClass.IdClasses == id)
                {
                    plan = planClass;
                } 
            }
            return plan;
        }
        // PUT: api/PlanClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanClasses(int id, PlanClasses planClasses)
        {
            if (id != planClasses.IdPlanClasses)
            {
                return BadRequest();
            }

            _context.Entry(planClasses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanClassesExists(id))
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

        // POST: api/PlanClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PlanClasses>> PostPlanClasses(PlanClasses planClasses)
        {
            _context.PlanClasses.Add(planClasses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanClasses", new { id = planClasses.IdPlanClasses }, planClasses);
        }

        // DELETE: api/PlanClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlanClasses>> DeletePlanClasses(int id)
        {
            var planClasses = await _context.PlanClasses.FindAsync(id);
            if (planClasses == null)
            {
                return NotFound();
            }

            _context.PlanClasses.Remove(planClasses);
            await _context.SaveChangesAsync();

            return planClasses;
        }

        private bool PlanClassesExists(int id)
        {
            return _context.PlanClasses.Any(e => e.IdPlanClasses == id);
        }
    }
}
