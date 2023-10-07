using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Dto;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Controllers
{
    [Route("api/[controller]")]
    public class StallRatingController : Controller
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public StallRatingController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StallRating>>> GetStallRatings()
        {
            var stallRatings = await _context.StallRatings.ToListAsync();
            return Ok(stallRatings);
        }

        [HttpGet("getRatingByOrderId")]
        public async Task<ActionResult<StallRating>> GetRatingByOrderId(Guid orderId)
        {
            var userId = await GetUserIdByToken();

            if (userId is null)
            {
                return NotFound("User not found.");
            }

            var stallRating = await _context.StallRatings.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (stallRating == null)
            {
                return NotFound();
            }

            return Ok(stallRating);
        }

        [HttpPost]
        public async Task<ActionResult<StallRating>> PostStallRating(StallRatingDto stallRatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await GetUserIdByToken();

            if (userId is null)
            {
                return NotFound("User not found.");
            }

            var hasExistingRating = await _context.StallRatings.AnyAsync(x => x.UserId == userId
                                && x.OrderId == stallRatingDto.OrderId);

            if (hasExistingRating)
            {
                return Conflict("Rating Existed");
            }

            var stallRating = new StallRating
            {
                Id = Guid.NewGuid(),
                OrderId = stallRatingDto.OrderId,
                StallId = stallRatingDto.StallId,
                UserId = (Guid)userId,
                Rating = stallRatingDto.Rating,
                Review = stallRatingDto.Review
            };

            _context.StallRatings.Add(stallRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStallRating", new { id = stallRating.Id }, stallRating);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStallRating(Guid id, StallRating stallRating)
        {
            if (id != stallRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(stallRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StallRatingExists(id))
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
        public async Task<IActionResult> DeleteStallRating(Guid id)
        {
            var stallRating = await _context.StallRatings.FindAsync(id);
            if (stallRating == null)
            {
                return NotFound();
            }

            _context.StallRatings.Remove(stallRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StallRatingExists(Guid id)
        {
            return _context.StallRatings.Any(e => e.Id == id);
        }

        private async Task<Guid?> GetUserIdByToken()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""); // Remove the "Bearer " prefix

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var phoneNo = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "PhoneNo")?.Value;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNo == phoneNo);
                if (user is not null)
                {
                    return user.Id;
                }
            }
            return null;
        }
    }
}
