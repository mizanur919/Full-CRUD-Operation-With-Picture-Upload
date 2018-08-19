using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_Operation_In_MVC_Core.Model.Db;

namespace CRUD_Operation_In_MVC_Core.Controllers
{
    [Produces("application/json")]
    [Route("api/ItemsApi")]
    public class ItemsApiController : Controller
    {
        private readonly mydbContext _context;

        public ItemsApiController(mydbContext context)
        {
            _context = context;
        }

        // GET: api/ItemsApi
        [HttpGet]
        public IEnumerable<Items> GetItems()
        {
            return _context.Items;
        }

        // GET: api/ItemsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItems([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _context.Items.SingleOrDefaultAsync(m => m.Itemcode == id);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // PUT: api/ItemsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems([FromRoute] string id, [FromBody] Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items.Itemcode)
            {
                return BadRequest();
            }

            _context.Entry(items).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        // POST: api/ItemsApi
        [HttpPost]
        public async Task<IActionResult> PostItems([FromBody] Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(items);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemsExists(items.Itemcode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItems", new { id = items.Itemcode }, items);
        }

        // DELETE: api/ItemsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItems([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _context.Items.SingleOrDefaultAsync(m => m.Itemcode == id);
            if (items == null)
            {
                return NotFound();
            }

            _context.Items.Remove(items);
            await _context.SaveChangesAsync();

            return Ok(items);
        }

        private bool ItemsExists(string id)
        {
            return _context.Items.Any(e => e.Itemcode == id);
        }
    }
}