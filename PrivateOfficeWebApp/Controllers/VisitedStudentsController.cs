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
    public class VisitedStudentsController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public VisitedStudentsController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/VisitedStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitedStudent>>> GetVisitedStudents()
        {
            return await _context.VisitedStudents.ToListAsync();
        }

        // GET: api/VisitedStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitedStudent>> GetVisitedStudent(int id)
        {
            var visitedStudent = await _context.VisitedStudents.FindAsync(id);

            if (visitedStudent == null)
            {
                return NotFound();
            }

            return visitedStudent;
        }

        [HttpGet("GetVisitedFromClasses&id={id}")]
        public async Task<ICollection<VisitedStudent>> GetVisitedFromClasses(int id)
        {
            var visited = await _context.VisitedStudents.ToListAsync();
            List<VisitedStudent> resultList = new List<VisitedStudent>();
            foreach (var visit in visited)
            {
                if (visit.IdClasses == id)
                    resultList.Add(visit);
            }
            return resultList;
        }

        [HttpGet("GetVisitedFromStudent&id={id}")]
        public async Task<ICollection<VisitedStudent>> GetVisitedFromStudent(int id)
        {
            var visited = await _context.VisitedStudents.ToListAsync();
            List<VisitedStudent> resultList = new List<VisitedStudent>();
            foreach (var visit in visited)
            {
                if (visit.IdStudent == id)
                    resultList.Add(visit);
            }
            return resultList;
        }


        // PUT: api/VisitedStudents/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitedStudent(int id, VisitedStudent visitedStudent)
        {
            if (id != visitedStudent.IdVisitedStudent)
            {
                return BadRequest();
            }

            
            _context.Entry(visitedStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitedStudentExists(id))
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

        // POST: api/VisitedStudents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VisitedStudent>> PostVisitedStudent(VisitedStudent visitedStudent)
        {
            _context.VisitedStudents.Add(visitedStudent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVisitedStudent", new {id = visitedStudent.IdVisitedStudent}, visitedStudent);
            }
        

        // DELETE: api/VisitedStudents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitedStudent>> DeleteVisitedStudent(int id)
        {
            var visitedStudent = await _context.VisitedStudents.FindAsync(id);
            if (visitedStudent == null)
            {
                return NotFound();
            }

            _context.VisitedStudents.Remove(visitedStudent);
            await _context.SaveChangesAsync();

            return visitedStudent;
        }

        private bool VisitedStudentExists(int id)
        {
            return _context.VisitedStudents.Any(e => e.IdVisitedStudent == id);
        }
    }
}
