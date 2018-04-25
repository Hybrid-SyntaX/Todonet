using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.net.Core.Models;
using Todo.net.Persistence;

//TODO: Write unit tests

namespace Todo.net.Controllers
{

    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly TodoDbContext _context;

        public TasksController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<TodoTask> ReadAll()
        {
            return _context.TodoTasks.OrderBy((t)=>t.CreationDate);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Read([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoTask = await _context.TodoTasks.SingleOrDefaultAsync(m => m.Id == id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TodoTask todoTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoTask.Id)
            {
                return BadRequest();
            }


            _context.Entry(todoTask).State = EntityState.Modified;

            try
            {
                todoTask.LastUpdated=DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoTask todoTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            todoTask.LastUpdated=DateTime.Now;
            todoTask.CreationDate=DateTime.Now;
            _context.TodoTasks.Add(todoTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoTask", new { id = todoTask.Id }, todoTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoTask = await _context.TodoTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }

            _context.TodoTasks.Remove(todoTask);
            await _context.SaveChangesAsync();

            return Ok(todoTask);
        }
        
       
        // PUT: api/Tasks/5
        [HttpPut("{id}/done")]
        public async Task<IActionResult> Do([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoTask = await _context.TodoTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }
            //update date
            
            todoTask.LastUpdated=DateTime.Now;
            todoTask.CompletionDate=DateTime.Now;

            //_context.TodoTasks.Remove(todoTask);
            await _context.SaveChangesAsync();

            return Ok(todoTask);
        }
                // PUT: api/Tasks/5
        [HttpPut("{id}/undone")]
        public async Task<IActionResult> Undo([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoTask = await _context.TodoTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }
            //update date
            
            todoTask.LastUpdated=DateTime.Now;
            todoTask.CompletionDate=null;

            //_context.TodoTasks.Remove(todoTask);
            await _context.SaveChangesAsync();

            return Ok(todoTask);
        }

        private bool TodoTaskExists(Guid id)
        {
            return _context.TodoTasks.Any(e => e.Id == id);
        }
    }
}