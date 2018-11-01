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
    public class SpendingItemController : ControllerBase
    {
        private readonly MoneyTrackerContext _context;

        public SpendingItemController(MoneyTrackerContext context)
        {
            _context = context;
        }

        // GET: api/SpendingItem
        [HttpGet]
        public IEnumerable<SpendingItemDA> GetSpendingItems()
        {
            return _context.SpendingItems;
        }

        // GET: api/SpendingItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpendingItemDA([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spendingItemDA = await _context.SpendingItems.FindAsync(id);

            if (spendingItemDA == null)
            {
                return NotFound();
            }

            return Ok(spendingItemDA);
        }

        // PUT: api/SpendingItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpendingItemDA([FromRoute] Guid id, [FromBody] SpendingItemDA spendingItemDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spendingItemDA.ID)
            {
                return BadRequest();
            }

            _context.Entry(spendingItemDA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpendingItemDAExists(id))
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

        // POST: api/SpendingItem
        [HttpPost]
        public async Task<IActionResult> PostSpendingItemDA([FromBody] SpendingItemDA spendingItemDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SpendingItems.Add(spendingItemDA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpendingItemDA", new { id = spendingItemDA.ID }, spendingItemDA);
        }

        // DELETE: api/SpendingItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpendingItemDA([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spendingItemDA = await _context.SpendingItems.FindAsync(id);
            if (spendingItemDA == null)
            {
                return NotFound();
            }

            _context.SpendingItems.Remove(spendingItemDA);
            await _context.SaveChangesAsync();

            return Ok(spendingItemDA);
        }

        private bool SpendingItemDAExists(Guid id)
        {
            return _context.SpendingItems.Any(e => e.ID == id);
        }
    }
}