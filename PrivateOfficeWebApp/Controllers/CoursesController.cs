﻿using System;
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
    public class CoursesController : ControllerBase
    {
        private readonly PrivateOfficeWebAppContext _context;

        public CoursesController(PrivateOfficeWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
	        return await _context.Course.ToListAsync();
        }

        //GET: api/Courses/WithTeacher/id=5
        [HttpGet("WithTeacher/id={id}")]
        [Authorize]
        public async Task<List<Course>> GetCourseWithTeacher(int id)
        {
	        var courses = new List<Course>();
	        foreach (var course in await _context.Course.ToListAsync())
	        {
		        if (course.IdTeacher == id)
			        courses.Add(course);
                foreach (var Class in await _context.Classes.ToListAsync())
                {
	                Class.DateClasses.Add(Class.StartTime);
                    if(Class.IdCourse == course.IdCourse)
						course.Classes.Add(Class);
                }
	        }
	        return courses;
        }

        [HttpGet("GetCourseFromGroup/id={id}")]
        public async Task<ICollection<Course>> GetCousreFromGroup(int id)
        {
            var courses = await _context.Course.ToListAsync();
            List<Course> resultList = new List<Course>();
            foreach (var course in courses)
            {
                if (course.IdGroup == id)
                    resultList.Add(course);
                foreach (var Class in await _context.Classes.ToListAsync())
                {
                    Class.DateClasses.Add(Class.StartTime);
                    if (Class.IdCourse == course.IdCourse)
                        course.Classes.Add(Class);
                }
            }
            return resultList;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.IdCourse)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.IdCourse }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.IdCourse == id);
        }
    }
}
