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
    public class HomeworkController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public HomeworkController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Homework
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomework()
        {
            return await _context.Homework.ToListAsync();
        }

        // GET: api/Homework/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Homework>> GetHomework(int id)
        {
            var homework = await _context.Homework.FindAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return homework;
        }

        [HttpGet("GetHomeworkFromStudent/id={id}")]
        [Authorize]
        public async Task<ICollection<Homework>> GetHomeworkFromStudent(int id)
        {
            var homeworks = await _context.Homework.ToListAsync();
            List<Homework> resultList = new List<Homework>();
            foreach (var homework in homeworks)
            {
                if (homework.IdStudent == id)
                    resultList.Add(homework);
            }
            return resultList;
        }

        // PUT: api/Homework/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutHomework(int id, Homework homework)
        {
            if (id != homework.IdHomework)
            {
                return BadRequest();
            }

            _context.Entry(homework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeworkExists(id))
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

        

        // POST: api/Homework
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Homework>> PostHomework(Homework homework)
        {
            _context.Homework.Add(homework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomework", new { id = homework.IdHomework }, homework);
        }

        // DELETE: api/Homework/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Homework>> DeleteHomework(int id)
        {
            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }

            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();

            return homework;
        }

        private bool HomeworkExists(int id)
        {
            return _context.Homework.Any(e => e.IdHomework == id);
        }
    }
}
