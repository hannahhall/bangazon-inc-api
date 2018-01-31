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
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public EmployeeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employee.ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        // GET api/employee/5
        [HttpGet("{id}", Name = "GetSingleEmployee")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee employee = _context.Employee.Single(g => g.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/employee
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(employee);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleemployee", new { id = employee.EmployeeId }, employee);
        }

        private bool EmployeeExists(int id)
        {
            bool exists = _context.Employee.Any(c => c.EmployeeId == id);
            return exists;
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            _context.Employee.Update(employee);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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