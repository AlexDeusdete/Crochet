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
    public class ProductYarnsController : ControllerBase
    {
        private readonly CrochetAPIContext _context;

        public ProductYarnsController(CrochetAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProductYarns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductYarn>>> GetProductYarns()
        {
            return await _context.ProductYarns.ToListAsync();
        }

        // GET: api/ProductYarns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductYarn>> GetProductYarn(int id)
        {
            var productYarn = await _context.ProductYarns.FindAsync(id);

            if (productYarn == null)
            {
                return NotFound();
            }

            return productYarn;
        }

        // GET: api/ProductYarns/ProductId/0
        [HttpGet("ProductId/{id}")]
        public async Task<ActionResult<IEnumerable<ProductYarn>>> GetProductYarnByProductId(int id)
        {
            var productYarn = await _context.ProductYarns.Where(x => x.ProductId == id).ToListAsync();

            if (productYarn == null)
            {
                return NotFound();
            }

            return productYarn;
        }

        // PUT: api/ProductYarns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductYarn(int id, ProductYarn productYarn)
        {
            productYarn.Yarn = null;
            if (id != productYarn.Id)
            {
                return BadRequest();
            }

            _context.Entry(productYarn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductYarnExists(id))
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

        // POST: api/ProductYarns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductYarn>> PostProductYarn(ProductYarn productYarn)
        {
            productYarn.Yarn = null;
            _context.ProductYarns.Add(productYarn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductYarn", new { id = productYarn.Id }, productYarn);
        }

        // DELETE: api/ProductYarns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductYarn>> DeleteProductYarn(int id)
        {
            var productYarn = await _context.ProductYarns.FindAsync(id);
            if (productYarn == null)
            {
                return NotFound();
            }

            _context.ProductYarns.Remove(productYarn);
            await _context.SaveChangesAsync();

            return productYarn;
        }

        private bool ProductYarnExists(int id)
        {
            return _context.ProductYarns.Any(e => e.Id == id);
        }
    }
}
