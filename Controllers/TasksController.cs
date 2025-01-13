using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> GetTasks(string? status, Priority? priority, DateTime? dueDate)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status == status);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            if (dueDate.HasValue)
                query = query.Where(t => t.DueDate == dueDate.Value);

            return await query.ToListAsync();
        }

        // GET: api/ToDoTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTask>> GetToDoTask(int id)
        {
            var toDoTask = await _context.Tasks.FindAsync(id);

            if (toDoTask == null)
            {
                return NotFound();
            }

            return toDoTask;
        }

        // POST: api/ToDoTasks
        [HttpPost]
        public async Task<ActionResult<ToDoTask>> PostToDoTask(ToDoTask toDoTask)
        {
            _context.Tasks.Add(toDoTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoTask", new { id = toDoTask.Id }, toDoTask);
        }
       


        // DELETE: api/ToDoTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoTask(int id)
        {
            var toDoTask = await _context.Tasks.FindAsync(id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(toDoTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}