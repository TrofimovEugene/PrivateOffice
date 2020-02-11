﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeDataBaseAPI.Data;
using PrivateOfficeDataBaseAPI.DataBaseModels;
using PrivateOfficeDataBaseAPI.Models;

namespace PrivateOfficeDataBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeClassesController : ControllerBase
    {
        private readonly PrivateOfficeDataBaseAPIContext _context;

        public TypeClassesController(PrivateOfficeDataBaseAPIContext context)
        {
            _context = context;
        }

        // GET: api/TypeClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeClasses>>> GetTypeClasses()
        {
            return await _context.TypeClasses.ToListAsync();
        }

        // GET: api/TypeClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeClasses>> GetTypeClasses(int id)
        {
            var typeClasses = await _context.TypeClasses.FindAsync(id);

            if (typeClasses == null)
            {
                return NotFound();
            }

            return typeClasses;
        }

        // PUT: api/TypeClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeClasses(int id, TypeClasses typeClasses)
        {
            if (id != typeClasses.IdTypeClasses)
            {
                return BadRequest();
            }

            _context.Entry(typeClasses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeClassesExists(id))
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

        // POST: api/TypeClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TypeClasses>> PostTypeClasses(TypeClasses typeClasses)
        {
            _context.TypeClasses.Add(typeClasses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeClasses", new { id = typeClasses.IdTypeClasses }, typeClasses);
        }

        // DELETE: api/TypeClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeClasses>> DeleteTypeClasses(int id)
        {
            var typeClasses = await _context.TypeClasses.FindAsync(id);
            if (typeClasses == null)
            {
                return NotFound();
            }

            _context.TypeClasses.Remove(typeClasses);
            await _context.SaveChangesAsync();

            return typeClasses;
        }

        private bool TypeClassesExists(int id)
        {
            return _context.TypeClasses.Any(e => e.IdTypeClasses == id);
        }
    }
}
