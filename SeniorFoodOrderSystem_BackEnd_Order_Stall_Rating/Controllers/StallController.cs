using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Controllers
{
    [Route("api/[controller]")]
    public class StallController : Controller
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public StallController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stall>>> GetStalls()
        {
            return await _context.Stalls.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stall>> GetStall(Guid id)
        {
            var stall = await _context.Stalls.FindAsync(id);

            if (stall == null)
            {
                return NotFound();
            }

            return stall;
        }

        [HttpPost]
        public async Task<ActionResult<Stall>> CreateStall(Stall stall)
        {
            stall.Id = Guid.NewGuid();
            _context.Stalls.Add(stall);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStall), new { id = stall.Id }, stall);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStall(Guid id, Stall stall)
        {
            if (id != stall.Id)
            {
                return BadRequest();
            }

            _context.Entry(stall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StallExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStall(Guid id)
        {
            var stall = await _context.Stalls.FindAsync(id);
            if (stall == null)
            {
                return NotFound();
            }

            _context.Stalls.Remove(stall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StallExists(Guid id)
        {
            return _context.Stalls.Any(e => e.Id == id);
        }
    }
}
