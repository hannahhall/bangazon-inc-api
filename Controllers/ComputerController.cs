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
    public class ComputerController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ComputerController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var computers = _context.Computer.ToList();
            if (computers == null)
            {
                return NotFound();
            }
            return Ok(computers);
        }

        // GET api/computer/5
        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Computer computer = _context.Computer.Single(c => c.ComputerId == id);

                if (computer == null)
                {
                    return NotFound();
                }

                return Ok(computer);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Computer.Add(computer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (computerExists(computer.ComputerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleComputer", new { id = computer.ComputerId }, computer);
        }

        [HttpPost("{id}/employeeComputers")]
        public IActionResult Post(int id, [FromBody]EmployeeComputer employeeComputer)
        {
            employeeComputer.StartDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Computer computer = _context.Computer.SingleOrDefault(o => o.ComputerId == employeeComputer.ComputerId);
            if (id != employeeComputer.ComputerId || computer == null)
            {
                return BadRequest();
            }
            _context.EmployeeComputer.Add(employeeComputer);
            _context.SaveChanges();

            return RedirectToRoute("GetSingleComputer", new { id = computer.ComputerId });
        }


        private bool computerExists(int id)
        {
            bool exists = _context.Computer.Any(c => c.ComputerId == id);
            return exists;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computer.ComputerId)
            {
                return BadRequest();
            }
            _context.Computer.Update(computer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!computerExists(id))
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

        // DELETE api/paymenttype/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Computer computer = _context.Computer.Single(m => m.ComputerId == id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computer.Remove(computer);
            _context.SaveChanges();

            return Ok(computer);
        }

        [HttpDelete("{computerId}/computerproducts/{employeeComputerId}")]
        public IActionResult Delete(int computerId, int employeeComputerId)
        {
            Computer computer = _context.Computer.Single(g => g.ComputerId == computerId);
            EmployeeComputer employeeComputer = _context.EmployeeComputer.SingleOrDefault(s => s.EmployeeComputerId == employeeComputerId);

            if (computer == null)
            {
                return NotFound();
            }
            computer.EmployeeComputers.Remove(employeeComputer);
            _context.SaveChanges();
            return RedirectToRoute("GetSingleComputer", new { id = computer.ComputerId });
        }

    }
}
