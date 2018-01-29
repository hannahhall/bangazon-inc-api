using System;
using System.Linq;
using B_Api.Data;
using B_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B_Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public OrderController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _context.Order.ToList();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET api/order/5
        [HttpGet("{id}", Name = "GetSingleOrder")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = _context.Order
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                    .Single(g => g.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                OrderWithProducts orderWproducts = new OrderWithProducts(order);
                orderWproducts.Products = order.OrderProducts.Select(op => op.Product).ToList();

                return Ok(orderWproducts);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Order.Add(order);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (orderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleOrder", new { id = order.OrderId }, order);
        }

        [HttpPost("{id}/orderProducts")]
        public IActionResult Post(int id, [FromBody]OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Order order = _context.Order.SingleOrDefault(o => o.OrderId == orderProduct.OrderId);
            if (id != orderProduct.OrderId || order == null)
            {
                return BadRequest();
            }
            _context.OrderProduct.Add(orderProduct);
            _context.SaveChanges();

            return RedirectToRoute("GetSingleOrder", new { id = order.OrderId });
        }


        private bool orderExists(int id)
        {
            bool exists = _context.Order.Any(c => c.OrderId == id);
            return exists;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }
            _context.Order.Update(order);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{orderId}/orderproducts/{orderProductId}")]
        public IActionResult Delete(int orderId, int orderProductId)
        {
            Order order = _context.Order.Single(g => g.OrderId == orderId);
            OrderProduct orderProduct = _context.OrderProduct.SingleOrDefault(s => s.OrderProductId == orderProductId);

            if (order == null)
            {
                return NotFound();
            }
            order.OrderProducts.Remove(orderProduct);
            _context.SaveChanges();
            return RedirectToRoute("GetSingleOrder", new { id = order.OrderId });
        }

    }
}
