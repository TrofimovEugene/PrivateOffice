using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
		[Authorize(Roles = "admin")]
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
		// GET: api/Teachers/GetTeacherLogin
        [HttpPost("GetTeacherLogin")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Teacher>> GetTeacherLogin(string requestLogin)
        {
	        var teachers = await _context.Teacher.ToListAsync();

	        if (teachers == null)
	        {
		        return NotFound();
	        }

	        foreach (var teacher in teachers)
	        {
		        if (teacher.Login == requestLogin)
			        return teacher;
	        }

	        return NotFound();
        }


        public class Response
        {
	        public string access_token { get; set; }
            public string username { get; set; }
            public int idTeacher { get; set; }
        }

        [HttpPost("token")]
        public async Task<ActionResult<Response>> Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
 
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var teachers = await _context.Teacher.ToListAsync();

            foreach (var teacher in teachers)
            {
	            if (teacher.Login == identity.Name)
		            return new Response
		            {
			            access_token = encodedJwt,
			            username = identity.Name,
			            idTeacher = teacher.IdTeacher
		            };
            }

            return null;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
	        var teachers = _context.Teacher.ToList();
	        Teacher teacher = null;
	        foreach (var teacher_elem in teachers)
	        {
		        if (teacher_elem.Login == username)
			        if (teacher_elem.Password == password)
				        teacher = teacher_elem;
	        }
            if (teacher != null)
	        {
		        var claims = new List<Claim>
		        {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, teacher.Login),
			        new Claim(ClaimsIdentity.DefaultRoleClaimType, teacher.Role)
		        };
		        ClaimsIdentity claimsIdentity =
			        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
				        ClaimsIdentity.DefaultRoleClaimType);
		        return claimsIdentity;
	        }

	        // если пользователя не найдено
	        return null;
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
        [Authorize]
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
        [Authorize(Roles = "admin")]
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
