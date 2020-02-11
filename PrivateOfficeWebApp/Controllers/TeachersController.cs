﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Data;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public TeachersController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher()
        {
            return await _context.Teacher.ToListAsync();
        }


		// GET: api/Teachers/GetTeacherDetails/5
		[HttpGet("GetTeacherDetails/{id}")]
#pragma warning disable 1998
		public async Task<ActionResult<Teacher>> GetTeacherDetails(int id)
#pragma warning restore 1998
		{

            var teachers = _context.Teacher
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(group => group.Group)
                            .ThenInclude(student => student.Student)
                                .ThenInclude(report => report.Report)
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(typeClasses => typeClasses.TypeClasses)
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(controlMeasures => controlMeasures.ControlMeasures)
                            .ThenInclude(task => task.Task)
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(controlMeasures => controlMeasures.ControlMeasures)
                            .ThenInclude(questions => questions.Questions)
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(controlMeasures => controlMeasures.ControlMeasures)
                             .ThenInclude(ticket => ticket.Ticket)
                                 .ThenInclude(questions => questions.Questions)
                .Include(course => course.Course)
                    .ThenInclude(classes => classes.Classes)
                        .ThenInclude(controlMeasures => controlMeasures.ControlMeasures)
                             .ThenInclude(ticket => ticket.Ticket)
                                .ThenInclude(task => task.Task)

                .FirstOrDefault(teacher => teacher.IdTeacher == id);

            if (teachers == null)
            {
                return NotFound();
            }

            return teachers;
        }

		public class RequestLogin
		{
			public string login { get; set; }
            public string password { get; set; }
		} 
        // GET: api/Teachers/Julia 
        [HttpPost("GetTeacherLogin")]
        public async Task<ActionResult<Teacher>> GetTeacherLogin(RequestLogin requestLogin)
        {
            var teachers = await _context.Teacher.ToListAsync();

            if (teachers == null)
            {
	            return NotFound();
            }
            foreach (var teacher in teachers)
            {
	            if(teacher.Login == requestLogin.login)
		            if (teacher.Password == requestLogin.password)
			            return teacher;
            }

            return NotFound();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.IdTeacher)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.IdTeacher }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.IdTeacher == id);
        }
    }
}
