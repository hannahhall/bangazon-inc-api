using System;
using System.Collections.Generic;
using System.Linq;
using B_Api.Data;
using B_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B_Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController: Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CustomerController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Customer> customers = new List<Customer>();
            try 
            {
                bool active = bool.Parse(HttpContext.Request.Query["active"]);
                if (active)
                {
                    // customers with orders
                    customers = _context.Customer.Where(customer => 
                        _context.Order.Any(order => order.CustomerId == customer.CustomerId)
                    ).ToList();
                }
                else
                {
                    // customers without orders
                    customers = _context.Customer.Where(customer =>
                        !(_context.Order.Any(order => order.CustomerId == customer.CustomerId))
                    ).ToList();
                }
            } 
            catch(System.ArgumentNullException)
            {
                customers = _context.Customer.ToList();   
            }
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // GET api/customer/5
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = _context.Customer.Single(g => g.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSinglecustomer", new { id = customer.CustomerId }, customer);
        }

        private bool CustomerExists(int id)
        {
            bool exists = _context.Customer.Any(c => c.CustomerId == id);
            return exists;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            _context.Customer.Update(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

    }
}