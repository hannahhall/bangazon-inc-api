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
    public class TrainingProgramController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TrainingProgramController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var trainingPrograms = _context.TrainingProgram.ToList();
            if (trainingPrograms == null)
            {
                return NotFound();
            }
            return Ok(trainingPrograms);
        }

        // GET api/trainingProgram/5
        [HttpGet("{id}", Name = "GetSingleTrainingProgram")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TrainingProgram trainingProgram = _context.TrainingProgram.Single(c => c.Id == id);

                if (trainingProgram == null)
                {
                    return NotFound();
                }

                return Ok(trainingProgram);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR!!!!!!!!!: {ex}");
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]TrainingProgram trainingProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingProgram.Add(trainingProgram);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainingProgramExists(trainingProgram.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleTrainingProgram", new { id = trainingProgram.Id }, trainingProgram);
        }

        [HttpPost("{id}/employeesAttending")]
        public IActionResult Post(int id, [FromBody]TrainingEmployee employeeTrainingProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TrainingProgram trainingProgram = _context.TrainingProgram.SingleOrDefault(o => o.Id == employeeTrainingProgram.Id);
            if (id != employeeTrainingProgram.Id || trainingProgram == null)
            {
                return BadRequest();
            }
            _context.TrainingEmployee.Add(employeeTrainingProgram);
            _context.SaveChanges();

            return RedirectToRoute("GetSingleTrainingProgram", new { id = trainingProgram.Id });
        }


        private bool TrainingProgramExists(int id)
        {
            bool exists = _context.TrainingProgram.Any(c => c.Id == id);
            return exists;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TrainingProgram trainingProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingProgram.Id)
            {
                return BadRequest();
            }
            _context.TrainingProgram.Update(trainingProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProgramExists(id))
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

        // DELETE api/trainingprogram/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TrainingProgram trainingProgram = _context.TrainingProgram.Single(m => m.Id == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            if (trainingProgram.StartDate < DateTime.Now)
            {
                return BadRequest("Can't delete a program that's already happened");
            }
            _context.TrainingProgram.Remove(trainingProgram);
            _context.SaveChanges();

            return Ok(trainingProgram);
        }

        [HttpDelete("{trainingProgramId}/employeesattending/{employeeId}")]
        public IActionResult Delete(int trainingProgramId, int employeeId)
        {
            TrainingProgram trainingProgram = _context.TrainingProgram.Single(g => g.Id == trainingProgramId);
            if (trainingProgram.StartDate < DateTime.Now)
            {
                return BadRequest("Can't delete from a program that's already happened");
            }
            TrainingEmployee employeeTrainingProgram = _context.TrainingEmployee.SingleOrDefault(s => s.EmployeeId == employeeId);

            if (trainingProgram == null)
            {
                return NotFound();
            }
            trainingProgram.TrainingEmployees.Remove(employeeTrainingProgram);
            _context.SaveChanges();
            return RedirectToRoute("GetSingleTrainingProgram", new { id = trainingProgram.Id });
        }

    }
}
