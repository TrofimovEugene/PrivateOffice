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
    public class ReportsController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public ReportsController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport()
        {
            return await _context.Report.ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // GET: api/Reports/5
        [HttpGet("GetReportsFromStudent/id={id}")]
        [Authorize]
        public async Task<ICollection<Report>> GetReportsFromStudent(int id)
        {
            var report = await _context.Report.ToListAsync();
            List<Report> result = new List<Report>();
            foreach (var reports in report)
            {
                 if (reports.IdStudent == id)
                {
                    result.Add(reports);
                }
               
            }
          
            return result;
        }


        // PUT: api/Reports/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.IdReport)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        // POST: api/Reports
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _context.Report.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.IdReport }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Report>> DeleteReport(int id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return report;
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.IdReport == id);
        }
    }
}
