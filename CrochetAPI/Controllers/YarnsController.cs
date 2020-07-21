using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crochet.Models;
using CrochetAPI.Data;

namespace CrochetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YarnsController : ControllerBase
    {
        private readonly CrochetAPIContext _context;

        public YarnsController(CrochetAPIContext context)
        {
            _context = context;
        }

        // GET: api/Yarns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Yarn>>> GetYarns()
        {
            return await _context.Yarns.ToListAsync();
        }

        // GET: api/Yarns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Yarn>> GetYarn(int id)
        {
            var yarn = await _context.Yarns.FindAsync(id);
            yarn.Brand = await _context.Brands.FindAsync(yarn.BrandId);
            if (yarn == null)
            {
                return NotFound();
            }

            return yarn;
        }

        // PUT: api/Yarns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYarn(int id, Yarn yarn)
        {
            yarn.Brand = null;
            if (id != yarn.Id)
            {
                return BadRequest();
            }

            _context.Entry(yarn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YarnExists(id))
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

        // POST: api/Yarns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Yarn>> PostYarn(Yarn yarn)
        {
            yarn.Brand = null;
            _context.Yarns.Add(yarn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYarn", new { id = yarn.Id }, yarn);
        }

        // DELETE: api/Yarns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Yarn>> DeleteYarn(int id)
        {
            var yarn = await _context.Yarns.FindAsync(id);
            if (yarn == null)
            {
                return NotFound();
            }

            _context.Yarns.Remove(yarn);
            await _context.SaveChangesAsync();

            return yarn;
        }

        private bool YarnExists(int id)
        {
            return _context.Yarns.Any(e => e.Id == id);
        }
    }
}
