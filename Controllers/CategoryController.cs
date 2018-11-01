using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Persistence;

namespace MoneyTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MoneyTrackerContext _context;

        public CategoryController(MoneyTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<CategoryDA> GetCategories()
        {
            return _context.Categories;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDA([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDA = await _context.Categories.FindAsync(id);

            if (categoryDA == null)
            {
                return NotFound();
            }

            return Ok(categoryDA);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryDA([FromRoute] Guid id, [FromBody] CategoryDA categoryDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryDA.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoryDA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryDAExists(id))
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

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> PostCategoryDA([FromBody] CategoryDA categoryDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(categoryDA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryDA", new { id = categoryDA.ID }, categoryDA);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryDA([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDA = await _context.Categories.FindAsync(id);
            if (categoryDA == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryDA);
            await _context.SaveChangesAsync();

            return Ok(categoryDA);
        }

        private bool CategoryDAExists(Guid id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}