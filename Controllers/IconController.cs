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
    public class IconController : ControllerBase
    {
        private readonly MoneyTrackerContext _context;

        public IconController(MoneyTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Icon
        [HttpGet]
        public IEnumerable<IconDA> GetIcons()
        {
            return _context.Icons;
        }

        // GET: api/Icon/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIcon([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var iconDA = await _context.Icons.FindAsync(id);

            if (iconDA == null)
            {
                return NotFound();
            }

            return Ok(iconDA);
        }

        // PUT: api/Icon/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIcon([FromRoute] Guid id, [FromBody] IconDA iconDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iconDA.ID)
            {
                return BadRequest();
            }

            _context.Entry(iconDA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IconExists(id))
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

        // POST: api/Icon
        [HttpPost]
        public async Task<IActionResult> PostIcon([FromBody] IconDA iconDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Icons.Add(iconDA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIcon", new { id = iconDA.ID }, iconDA);
        }

        // DELETE: api/Icon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIcon([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var iconDA = await _context.Icons.FindAsync(id);
            if (iconDA == null)
            {
                return NotFound();
            }

            _context.Icons.Remove(iconDA);
            await _context.SaveChangesAsync();

            return Ok(iconDA);
        }

        private bool IconExists(Guid id)
        {
            return _context.Icons.Any(e => e.ID == id);
        }
    }
}