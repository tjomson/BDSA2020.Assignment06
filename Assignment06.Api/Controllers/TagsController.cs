using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment06.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Assignment06.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _repository;
        
        public TagsController(ITagRepository repository)
        {
            _repository = repository;
        }

        // GET: tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> Get()
        {
            return await _repository.Read().ToListAsync();
        }

        // GET: tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> Get(int id)
        {
            return await _repository.Read(id);
        }

        // POST: tags
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagCreateDTO tag)
        {
            var status = await _repository.Create(tag);

            if(status.response == Status.Conflict)
            {
                return Conflict();
            } 
            
            return CreatedAtAction(nameof(Get), new { status.taskId }, default);
        }

        // PUT: tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TagUpdateDTO tag)
        {
            var response = await _repository.Update(tag);

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

        // DELETE: tags/5
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
