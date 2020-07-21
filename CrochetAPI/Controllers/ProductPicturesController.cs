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
    public class ProductPicturesController : ControllerBase
    {
        private readonly CrochetAPIContext _context;

        public ProductPicturesController(CrochetAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProductPictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPicture>>> GetProductPictures()
        {
            return await _context.ProductPictures.ToListAsync();
        }

        // GET: api/ProductPictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPicture>> GetProductPicture(int id)
        {
            var productPicture = await _context.ProductPictures.FindAsync(id);

            if (productPicture == null)
            {
                return NotFound();
            }

            return productPicture;
        }

        // GET: api/ProductPictures/ProductId/5
        [HttpGet("ProductId/{id}")]
        public async Task<ActionResult<IEnumerable<ProductPicture>>> GetProductPictureByProductId(int id)
        {
            var productPicture = await _context.ProductPictures.Where(x => x.ProductId == id).ToListAsync();

            if (productPicture == null)
            {
                return NotFound();
            }

            return productPicture;
        }

        // PUT: api/ProductPictures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductPicture>> PutProductPicture(int id, ProductPicture productPicture)
        {
            if (id != productPicture.Id)
            {
                return BadRequest();
            }

            _context.Entry(productPicture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var newPicture = await _context.ProductPictures.FindAsync(id);

            return newPicture;
        }

        // POST: api/ProductPictures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductPicture>> PostProductPicture(ProductPicture productPicture)
        {
            _context.ProductPictures.Add(productPicture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPicture", new { id = productPicture.Id }, productPicture);
        }

        // DELETE: api/ProductPictures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductPicture>> DeleteProductPicture(int id)
        {
            var productPicture = await _context.ProductPictures.FindAsync(id);
            if (productPicture == null)
            {
                return NotFound();
            }

            _context.ProductPictures.Remove(productPicture);
            await _context.SaveChangesAsync();

            return productPicture;
        }

        private bool ProductPictureExists(int id)
        {
            return _context.ProductPictures.Any(e => e.Id == id);
        }
    }
}
