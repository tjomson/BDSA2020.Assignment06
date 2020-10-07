using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment06.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Assignment06.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        // GET: tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDTO>>> Get()
        {
            return await _repository.Read().ToListAsync();
        }

        // GET: tasks/5
        // [HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetailsDTO>> Get(int id)
        {
            return await _repository.Read(id);
        }

        // POST: tasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskCreateDTO task)
        {
            var status = await _repository.Create(task);

            if(status.response == Status.Conflict)
            {
                return Conflict();
            } 
            
            return CreatedAtAction(nameof(Get), new { status.createdId }, default);
        }

        // PUT: tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDTO task)
        {
            var status = await _repository.Update(task);

            if(status == Status.NotFound)
            {
                return NotFound();
            }

            if(status == Status.Conflict)
            {
                return Conflict();
            }

            return new StatusCodeResult((int) status);
        }

        // DELETE: tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _repository.Delete(id);

            if(status == Status.NotFound)
            {
                return NotFound();
            }
            
            if(status == Status.Conflict)
            {
                return Conflict();
            }

            return new StatusCodeResult((int) status);
        }
    }
}
