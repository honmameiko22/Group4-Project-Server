using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/Reserves")]
    public class ReservesController : Controller
    {
        private readonly DataContext _context;

        public ReservesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reserves
        [HttpGet]
        public IEnumerable<Reserve> GetReserves()
        {
            return _context.Reserves;
        }

        // GET: api/Reserves/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserve([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserve = await _context.Reserves.SingleOrDefaultAsync(m => m.ReserveId == id);

            if (reserve == null)
            {
                return NotFound();
            }

            return Ok(reserve);
        }

        // PUT: api/Reserves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserve([FromRoute] int id, [FromBody] Reserve reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserve.ReserveId)
            {
                return BadRequest();
            }

            _context.Entry(reserve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReserveExists(id))
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

        // POST: api/Reserves
        [HttpPost]
        public async Task<IActionResult> PostReserve([FromBody] Reserve reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reserves.Add(reserve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserve", new { id = reserve.ReserveId }, reserve);
        }

        // DELETE: api/Reserves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserve([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserve = await _context.Reserves.SingleOrDefaultAsync(m => m.ReserveId == id);
            if (reserve == null)
            {
                return NotFound();
            }

            _context.Reserves.Remove(reserve);
            await _context.SaveChangesAsync();

            return Ok(reserve);
        }

        private bool ReserveExists(int id)
        {
            return _context.Reserves.Any(e => e.ReserveId == id);
        }
    }
}