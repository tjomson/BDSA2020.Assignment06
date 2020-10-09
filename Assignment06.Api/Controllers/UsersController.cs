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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        
        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListDTO>>> Get()
        {
            var result = await _repository.ReadAsync();
            return result.ToList();
        }

        // GET: users/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> Get(int id)
        {
            var result = await _repository.ReadAsync(id);
            
            if(result == null)
            {
                return NotFound();
            }

            return await _repository.ReadAsync(id);
        }

        // POST: users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDTO user)
        {
            var status = await _repository.CreateAsync(user);

            if(status.response == Status.Conflict)
            {
                return Conflict();
            } 
            
            return CreatedAtAction(nameof(Get), new { status.taskId }, default);
        }

        // PUT: users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserUpdateDTO user)
        {
            var response = await _repository.UpdateAsync(user);

            if(response == Status.NotFound)
            {
                return NotFound();
            }
            
            if(response == Status.Conflict)
            {
                return Conflict();
            }

            return new StatusCodeResult((int) response);
        }

        // DELETE: users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repository.DeleteAsync(id);

            if(response == Status.Conflict)
            {
                return Conflict();
            }

            return new StatusCodeResult((int) response);
        }
    }
}
