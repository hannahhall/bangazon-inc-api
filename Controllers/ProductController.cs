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
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Product.ToList();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET api/product/5
        [HttpGet("{id}", Name = "GetSingleProduct")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product product = _context.Product.Single(g => g.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleProduct", new { id = product.ProductId }, product);
        }

        
        private bool ProductExists(int id)
        {
            bool exists = _context.Product.Any(c => c.ProductId == id);
            return exists;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Product.Update(product);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
