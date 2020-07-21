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
    public class ProductFinalcialsController : ControllerBase
    {
        private readonly CrochetAPIContext _context;

        public ProductFinalcialsController(CrochetAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProductFinalcials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductFinalcial>>> GetProductFinalcials()
        {
            return await _context.ProductFinalcials.ToListAsync();
        }

        // GET: api/ProductFinalcials/5
        [HttpGet("ProductId/{id}")]
        public async Task<ActionResult<IEnumerable<ProductFinalcial>>> GetProductFinalcialByProductId(int id)
        {
            var productFinalcial = await _context.ProductFinalcials.Where(x => x.ProductId == id).ToListAsync();

            if (productFinalcial == null)
            {
                return NotFound();
            }

            return productFinalcial;
        }

        // GET: api/ProductFinalcials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFinalcial>> GetProductFinalcial(int id)
        {
            var productFinalcial = await _context.ProductFinalcials.FindAsync(id);

            if (productFinalcial == null)
            {
                return NotFound();
            }

            return productFinalcial;
        }

        // PUT: api/ProductFinalcials/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductFinalcial(int id, ProductFinalcial productFinalcial)
        {
            if (id != productFinalcial.Id)
            {
                return BadRequest();
            }

            _context.Entry(productFinalcial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductFinalcialExists(id))
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

        // POST: api/ProductFinalcials
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductFinalcial>> PostProductFinalcial(ProductFinalcial productFinalcial)
        {
            _context.ProductFinalcials.Add(productFinalcial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductFinalcial", new { id = productFinalcial.Id }, productFinalcial);
        }

        // DELETE: api/ProductFinalcials/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductFinalcial>> DeleteProductFinalcial(int id)
        {
            var productFinalcial = await _context.ProductFinalcials.FindAsync(id);
            if (productFinalcial == null)
            {
                return NotFound();
            }

            _context.ProductFinalcials.Remove(productFinalcial);
            await _context.SaveChangesAsync();

            return productFinalcial;
        }

        private bool ProductFinalcialExists(int id)
        {
            return _context.ProductFinalcials.Any(e => e.Id == id);
        }
    }
}
