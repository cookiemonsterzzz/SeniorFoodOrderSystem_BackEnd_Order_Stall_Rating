using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Dto;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public OrderController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("getOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var userId = await GetUserIdByToken();

            if (userId is null)
            {
                return NotFound("User not found.");
            }

            var result = await _context.Orders
                        .Select(x => new OrderDto
                        {
                            Id = x.Id,
                            OrderName = x.OrderName,
                            OrderDescription = x.OrderDescription,
                            OrderDate = x.OrderDate,
                            UserId = x.UserId,
                            FoodName = x.FoodName,
                            FoodCustomization = x.FoodCustomization,
                            FoodPrice = x.FoodPrice,
                            Amount = x.Amount,
                            Quantity = x.Quantity,
                            OrderStatus = x.OrderStatus
                        })
                        .ToListAsync();

            var statusPriority = new Dictionary<string, int>
                                {
                                    {"Unpaid", 0},
                                    {"Paid", 1},
                                    {"In Progress", 2},
                                    {"Done", 3},
                                    {"Other Status", 4} // Add more statuses as needed
                                };

            // Calculate a sort key based on OrderStatus (Unpaid orders first, then Paid orders)
            result = result.OrderBy(x => statusPriority.ContainsKey(x.OrderStatus)
                                            ? statusPriority[x.OrderStatus]
                                            : 4)
                           .ThenByDescending(x => x.OrderDate)
                           .ToList();

            return result;
        }

        [HttpGet("getOrderById")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null)
            {
                return NotFound("Order not found.");
            }
            else
            {
                var result = new OrderDto
                {
                    Id = order.Id,
                    OrderName = order.OrderName,
                    OrderDescription = order.OrderDescription,
                    OrderDate = order.OrderDate,
                    UserId = order.UserId,
                    FoodName = order.FoodName,
                    FoodCustomization = order.FoodCustomization,
                    FoodPrice = order.FoodPrice,
                    Amount = order.Amount,
                    Quantity = order.Quantity,
                    StallId = order.StallId,
                    OrderStatus = order.OrderStatus
                };

                return Ok(result);
            }
        }

        [HttpPost("upsertOrder")]
        public async Task<ActionResult<OrderDto>> UpsertOrder([FromBody] OrderDto order)
        {
            var userId = await GetUserIdByToken();

            if (userId is null)
            {
                return NotFound("User not found.");
            }

            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);

            if (existingOrder is null)
            {
                // Retrieve the latest order number from the database for the current user
                var latestOrderNumber = await _context.Orders
                                        .OrderByDescending(o => o.OrderDate)
                                        .Select(o => int.Parse(o.OrderName))
                                        .FirstOrDefaultAsync();

                // Increment the order number
                var newOrderName = (latestOrderNumber <= 0
                                    ? 1
                                    : latestOrderNumber + 1)
                                    .ToString();

                existingOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTimeOffset.Now,
                    UserId = (Guid)userId,
                    FoodName = order.FoodName,
                    FoodCustomization = order.FoodCustomization,
                    FoodPrice = order.FoodPrice,
                    Amount = order.Amount,
                    Quantity = order.Quantity,
                    OrderName = newOrderName,
                    OrderDescription = order.OrderDescription,
                    OrderStatus = "Unpaid",
                    StallId = order.StallId,
                    DateTimeCreated = DateTimeOffset.Now,
                };
                await _context.Orders.AddAsync(existingOrder);
            }
            else
            {
                existingOrder.OrderStatus = order.OrderStatus;
                existingOrder.DateTimeUpdated = DateTimeOffset.Now;
                _context.Orders.Update(existingOrder);
            }

            await _context.SaveChangesAsync();

            var result = new OrderDto
            {
                Id = existingOrder.Id,
                OrderName = existingOrder.OrderName,
                OrderDescription = existingOrder.OrderDescription,
                OrderDate = existingOrder.OrderDate,
                UserId = existingOrder.UserId,
                StallId = existingOrder.StallId,
                FoodName = existingOrder.FoodName,
                FoodCustomization = existingOrder.FoodCustomization,
                FoodPrice = existingOrder.FoodPrice,
                Amount = existingOrder.Amount,
                Quantity = existingOrder.Quantity,
                OrderStatus = existingOrder.OrderStatus,
            };

            return Ok(result);
        }

        [HttpPost("deleteOrder")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrder is null)
            {
                return NotFound();
            }
            else
            {
                _context.Orders.Remove(existingOrder);

                await _context.SaveChangesAsync();

                return Ok();
            }
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
