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
    public class HomeworkGroupsController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public HomeworkGroupsController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/HomeworkGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeworkGroup>>> GetHomeworkGroup()
        {
            return await _context.HomeworkGroup.ToListAsync();
        }

        // GET: api/HomeworkGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkGroup>> GetHomeworkGroup(int id)
        {
            var homeworkGroup = await _context.HomeworkGroup.FindAsync(id);

            if (homeworkGroup == null)
            {
                return NotFound();
            }

            return homeworkGroup;
        }

        // GET: api/HomeworkGroups/5
        [HttpGet("GetHomeworkGroupForClasses/id={id}")]
        public async Task<HomeworkGroup> GetHomeworkGroupForClasses(int id)
        {
            var homeworkGroup = await _context.HomeworkGroup.ToListAsync();
            HomeworkGroup homeworkGroups = new HomeworkGroup();
            foreach (var homeworkGr in homeworkGroup)
            {
                if (homeworkGr.IdClasses == id)
                {
                    homeworkGroups = homeworkGr;
                }
            }
                    return homeworkGroups;
        }

        // PUT: api/HomeworkGroups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeworkGroup(int id, HomeworkGroup homeworkGroup)
        {
            if (id != homeworkGroup.IdHomeworkGroup)
            {
                return BadRequest();
            }

            _context.Entry(homeworkGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeworkGroupExists(id))
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

        // POST: api/HomeworkGroups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HomeworkGroup>> PostHomeworkGroup(HomeworkGroup homeworkGroup)
        {
            _context.HomeworkGroup.Add(homeworkGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomeworkGroup", new { id = homeworkGroup.IdHomeworkGroup }, homeworkGroup);
        }

        // DELETE: api/HomeworkGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeworkGroup>> DeleteHomeworkGroup(int id)
        {
            var homeworkGroup = await _context.HomeworkGroup.FindAsync(id);
            if (homeworkGroup == null)
            {
                return NotFound();
            }

            _context.HomeworkGroup.Remove(homeworkGroup);
            await _context.SaveChangesAsync();

            return homeworkGroup;
        }

        private bool HomeworkGroupExists(int id)
        {
            return _context.HomeworkGroup.Any(e => e.IdHomeworkGroup == id);
        }
    }
}
