using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Data;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public GroupsController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            return await _context.Group.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var @group = await _context.Group.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }
        // GET: api/Groups/5
        [HttpGet("GetCountStudentInGroup/id={id}")]
        [Authorize]
        public async Task<int> GetCountStudentInGroup(int id)
        {
            var students = await _context.Student.ToListAsync();
            var countStudent = 0;
            foreach (var student in students)
            {
                if (student.IdGroup == id)
                    countStudent++;
            }
            return countStudent;
        }

        [HttpGet("GetCountHomeworkInGroup/id={id}")]
        [Authorize]
        public async Task<int> GetCountHomeworkInGroup(int id)
        {
            var homeworkGroups = await _context.HomeworkGroup.ToListAsync();
            var countHomeworkGroups = 0;
            foreach (var homewrokgroup in homeworkGroups)
            {
                if (homewrokgroup.IdGroup == id)
                    countHomeworkGroups++;
            }
            return countHomeworkGroups;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.IdGroup)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
            _context.Group.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.IdGroup }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Group>> DeleteGroup(int id)
        {
            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();

            return @group;
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.IdGroup == id);
        }
    }
}
