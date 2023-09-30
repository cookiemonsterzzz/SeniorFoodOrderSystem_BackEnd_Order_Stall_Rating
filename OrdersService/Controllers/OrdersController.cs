using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Models;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;

        public OrdersController(OrdersDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
          if (_context.Orders == null)
          {
                return null!;
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrders(Guid id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var orders = await _context.Orders.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'OrdersDbContext.Orders'  is null.");
            }

            Order dbOrder = new Order();
            dbOrder.Id = new Guid();
            dbOrder.Orderdate = DateTimeOffset.Now;
            dbOrder.Userid = order.Userid;
            dbOrder.Foodname = order.Foodname;
            dbOrder.Foodcustomization = order.Foodcustomization;
            dbOrder.Foodprice = order.Foodprice;
            dbOrder.Quantity = order.Quantity;
            dbOrder.Ordername = order.Ordername;
            dbOrder.Orderdescription = order.Orderdescription;

            _context.Orders.Add(dbOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = dbOrder.Id }, dbOrder);
        }
    }
}
