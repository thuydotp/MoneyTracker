using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Models;
using MoneyTracker.Persistence;

namespace MoneyTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly MoneyTrackerContext _context;

        public TransactionController(MoneyTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Transaction
        [HttpGet]
        public IEnumerable<Transaction> GetTransactions()
        {
            var model =  _context.Transactions
                .Include(x => x.Category).Select(x => new Transaction {
                    ID = x.ID,
                    ChangeValue = x.ChangeValue,
                    Description = x.Description,
                    CategoryID = x.CategoryID,
                    CategoryName = x.Category.CategoryName,
                    SpendingAccountID = x.SpendingAccountID,
                    SpendingAccountName = x.SpendingAccount.AccountName,
                    Type = (SpendingType)x.Type,
                    RecordDate = x.RecordDate
                }).ToList();
            return model;
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionDA = await _context.Transactions.FindAsync(id);

            if (transactionDA == null)
            {
                return NotFound();
            }

            return Ok(transactionDA);
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction([FromRoute] Guid id, [FromBody] TransactionDA transactionDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactionDA.ID)
            {
                return BadRequest();
            }

            _context.Entry(transactionDA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transaction
        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionDA transactionDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Transactions.Add(transactionDA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transactionDA.ID }, transactionDA);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionDA = await _context.Transactions.FindAsync(id);
            if (transactionDA == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactionDA);
            await _context.SaveChangesAsync();

            return Ok(transactionDA);
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.ID == id);
        }
    }
}