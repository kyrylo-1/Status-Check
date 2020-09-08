using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusCheck.API.Models;

namespace StatusCheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusItemsController : ControllerBase
    {
        private readonly StatusCheckContext _context;

        public StatusItemsController(StatusCheckContext context)
        {
            _context = context;
        }

        // GET: api/StatusItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusItem>>> GetStatusItems()
        {
            return await _context.StatusItems.ToListAsync();
        }

        // GET: api/StatusItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusItem>> GetStatusItem(long id)
        {
            var statusItem = await _context.StatusItems.FindAsync(id);

            if (statusItem == null)
            {
                return NotFound();
            }

            return statusItem;
        }

        // PUT: api/StatusItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusItem(long id, StatusItem statusItem)
        {
            if (id != statusItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(statusItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusItemExists(id))
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

        // POST: api/StatusItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StatusItem>> PostStatusItem(StatusItem statusItem)
        {
            _context.StatusItems.Add(statusItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusItem", new { id = statusItem.Id }, statusItem);
        }

        // DELETE: api/StatusItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusItem>> DeleteStatusItem(long id)
        {
            var statusItem = await _context.StatusItems.FindAsync(id);
            if (statusItem == null)
            {
                return NotFound();
            }

            _context.StatusItems.Remove(statusItem);
            await _context.SaveChangesAsync();

            return statusItem;
        }

        private bool StatusItemExists(long id)
        {
            return _context.StatusItems.Any(e => e.Id == id);
        }
    }
}
