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
    public class ClassesController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public ClassesController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Classes>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/id=4
        [HttpGet("id={id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Classes>>> GetClassesCourse(int? id)
        {
	        var classes = await _context.Classes.ToListAsync();
            var classResult = new List<Classes>();
	        foreach (var Class in classes)
	        {
		        Class.TypeClasses = await _context.TypeClasses.FindAsync(Class.IdTypeClasses);
		        if (id == Class.IdCourse)
			       classResult.Add(Class);
	        }

	        return classResult;
        }

        [HttpGet("GetClassesFromGroup/id={id}")]
        [Authorize]
        public async Task<ICollection<Classes>> GetClassesFromGroup(int id)
        {
            var classes = await _context.Classes.ToListAsync();
            List<Classes> resultList = new List<Classes>();
            foreach (var clas in classes)
            {
                if (clas.IdGroup == id)
                    resultList.Add(clas);
            }
            return resultList;
        }


        // GET: api/Classes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Classes>> GetClasses(int id)
        {
            var classes = await _context.Classes.FindAsync(id);

            if (classes == null)
            {
                return NotFound();
            }

            return classes;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutClasses(int id, Classes classes)
        {
            if (id != classes.IdClasses)
            {
                return BadRequest();
            }

            _context.Entry(classes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassesExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Classes>> PostClasses(Classes classes)
        {
            _context.Classes.Add(classes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasses", new { id = classes.IdClasses }, classes);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Classes>> DeleteClasses(int id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();

            return classes;
        }

        private bool ClassesExists(int id)
        {
            return _context.Classes.Any(e => e.IdClasses == id);
        }
    }
}
