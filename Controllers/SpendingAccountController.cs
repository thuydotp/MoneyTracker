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
    public class SpendingAccountController : ControllerBase
    {
        private readonly MoneyTrackerContext _context;

        public SpendingAccountController(MoneyTrackerContext context)
        {
            _context = context;
        }

        // GET: api/SpendingAccount
        [HttpGet]
        public IEnumerable<SpendingAccountDA> GetAccounts()
        {
            return _context.Accounts;
        }

        // GET: api/SpendingAccount/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpendingAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spendingAccountDA = await _context.Accounts.FindAsync(id);

            if (spendingAccountDA == null)
            {
                return NotFound();
            }

            return Ok(spendingAccountDA);
        }

        // PUT: api/SpendingAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpendingAccount([FromRoute] Guid id, [FromBody] SpendingAccountDA spendingAccountDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spendingAccountDA.ID)
            {
                return BadRequest();
            }

            _context.Entry(spendingAccountDA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpendingAccountExists(id))
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

        // POST: api/SpendingAccount
        [HttpPost]
        public async Task<IActionResult> PostSpendingAccount([FromBody] SpendingAccountDA spendingAccountDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Accounts.Add(spendingAccountDA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpendingAccount", new { id = spendingAccountDA.ID }, spendingAccountDA);
        }

        // DELETE: api/SpendingAccount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpendingAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spendingAccountDA = await _context.Accounts.FindAsync(id);
            if (spendingAccountDA == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(spendingAccountDA);
            await _context.SaveChangesAsync();

            return Ok(spendingAccountDA);
        }

        private bool SpendingAccountExists(Guid id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }
    }
}