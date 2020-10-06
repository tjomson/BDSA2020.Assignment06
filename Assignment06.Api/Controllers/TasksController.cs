using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment06.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment06.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // GET: tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDTO>>> Get()
        {
            throw new NotImplementedException();
        }

        // GET: tasks/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<TaskDetailsDTO>> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: tasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskCreateDTO task)
        {
            throw new NotImplementedException();
        }

        // PUT: tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDTO task)
        {
            throw new NotImplementedException();
        }

        // DELETE: tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
