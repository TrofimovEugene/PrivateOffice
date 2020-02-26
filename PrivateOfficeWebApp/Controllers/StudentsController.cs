using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrivateOfficeWebApp.Data;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public StudentsController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        [HttpGet("GetStudentFromGroup/id={id}")]
        public async Task<ICollection<Student>> GetStudentFromGroup(int id)
        {
	        var students = await _context.Student.ToListAsync();
            List<Student> resultList = new List<Student>();
            foreach (var student in students)
	        {
		        if (student.IdGroup == id)
					resultList.Add(student);
	        }
	        return resultList;
        }

        public class RequestLogin
        {
            public string login { get; set; }
            public string password { get; set; }
        }
    

        [HttpPost("GetStudentLogin")]
        public async Task<ActionResult<Student>> GetStudentLogin(RequestLogin requestLogin)
        {
            var students = await _context.Student.ToListAsync();

            if (students == null)
            {
                return NotFound();
            }
            foreach (var student in students)
            {
                if (student.Login == requestLogin.login)
                    if (student.Password == requestLogin.password)
                        return student;
            }

            return NotFound();
        }

        public class Response
        {
            public string access_token { get; set; }
            public string username { get; set; }
            public int idStudent { get; set; }
        }

        [HttpPost("StudentToken")]
        public async Task<ActionResult<Response>> StudentToken(string username, string password)
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

            var students = await _context.Student.ToListAsync();

            foreach (var student in students)
            {
                if (student.Login == identity.Name)
                    return new Response
                    {
                        access_token = encodedJwt,
                        username = identity.Name,
                        idStudent = student.IdStudent
                    };
            }

            return null;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var students = _context.Student.ToList();
            Student student = null;
            foreach (var student_elem in students)
            {
                if (student_elem.Login == username)
                    if (student_elem.Password == password)
                        student = student_elem;
            }
            if (student != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, student.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, student.Role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.IdStudent)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.IdStudent }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.IdStudent == id);
        }
    }
}
