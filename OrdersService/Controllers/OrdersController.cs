using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Dto;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public OrdersController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("getOrder")]
        public async Task<List<OrderDto>> GetOrders()
        {
            var result = await _context.Orders.Include(x => x.Payments)
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
                    Quantity = x.Quantity,
                    OrderStatus = x.Payments.Count > 0 ? "Paid" : "Unpaid",
                })
                .ToListAsync();

            return result;
        }

        [HttpGet("getOrderByName")]
        public async Task<ActionResult<OrderDto>> GetOrderByName(string orderName)
        {
            var existingOrder = await _context.Orders.Include(x => x.Payments)
                                .FirstOrDefaultAsync(x => x.OrderName == orderName);

            if (existingOrder is null)
            {
                return NotFound();
            }

            var result = new OrderDto
            {
                Id = existingOrder.Id,
                OrderName = existingOrder.OrderName,
                OrderDescription = existingOrder.OrderDescription,
                OrderDate = existingOrder.OrderDate,
                UserId = existingOrder.UserId,
                FoodName = existingOrder.FoodName,
                FoodCustomization = existingOrder.FoodCustomization,
                FoodPrice = existingOrder.FoodPrice,
                Quantity = existingOrder.Quantity,
                OrderStatus = existingOrder.Payments.Count > 0 ? "Paid" : "Unpaid",
            };

            return result;
        }

        [HttpPost("upsertOrder")]
        public async Task<ActionResult<Order>> PostOrder(OrderDto order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);

            if (existingOrder is null)
            {
                existingOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTimeOffset.Now,
                    UserId = order.UserId,
                    FoodName = order.FoodName,
                    FoodCustomization = order.FoodCustomization,
                    FoodPrice = order.FoodPrice,
                    Quantity = order.Quantity,
                    OrderName = order.OrderName,
                    OrderDescription = order.OrderDescription,

                };
                await _context.Orders.AddAsync(existingOrder);
            }
            else
            {
                existingOrder.Id = Guid.NewGuid();
                existingOrder.OrderDate = DateTimeOffset.Now;
                existingOrder.UserId = order.UserId;
                existingOrder.FoodName = order.FoodName;
                existingOrder.FoodCustomization = order.FoodCustomization;
                existingOrder.FoodPrice = order.FoodPrice;
                existingOrder.Quantity = order.Quantity;
                existingOrder.OrderName = order.OrderName;
                existingOrder.OrderDescription = order.OrderDescription;
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
                FoodName = existingOrder.FoodName,
                FoodCustomization = existingOrder.FoodCustomization,
                FoodPrice = existingOrder.FoodPrice,
                Quantity = existingOrder.Quantity,
                OrderStatus = "",
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

            _context.Orders.Remove(existingOrder);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
