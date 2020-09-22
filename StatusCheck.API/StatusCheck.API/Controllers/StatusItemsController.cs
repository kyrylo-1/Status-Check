using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusCheck.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusItem>>> GetStatusItems()
        {
            return await _context.StatusItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusItem>> GetStatusItem(int id)
        {
            var statusItem = await _context.StatusItems.FindAsync(id);

            if (statusItem == null)
            {
                return NotFound();
            }

            return statusItem;
        }

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

        [HttpPost]
        public async Task<ActionResult<StatusItem>> PostStatusItem(StatusItem statusItem)
        {
            _context.StatusItems.Add(statusItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusItem", new { id = statusItem.Id }, statusItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusItem>> DeleteStatusItem(int id)
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