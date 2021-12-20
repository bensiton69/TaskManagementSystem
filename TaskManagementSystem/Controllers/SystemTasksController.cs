using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemTasksController : ControllerBase
    {
        private readonly DataContext _context;

        public SystemTasksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SystemTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemTask>>> GetSystemTask()
        {
            return await _context.SystemTask.ToListAsync();
        }

        // GET: api/SystemTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemTask>> GetSystemTask(Guid id)
        {
            var systemTask = await _context.SystemTask.FindAsync(id);

            if (systemTask == null)
            {
                return NotFound();
            }

            return systemTask;
        }

        // PUT: api/SystemTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutSystemTask(Guid id, SystemTask systemTask)
        {
            if (id != systemTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(systemTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemTaskExists(id))
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

        // POST: api/SystemTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SystemTask>> PostSystemTask(SystemTask systemTask)
        {
            _context.SystemTask.Add(systemTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSystemTask", new { id = systemTask.Id }, systemTask);
        }

        // DELETE: api/SystemTasks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSystemTask(Guid id)
        {
            var systemTask = await _context.SystemTask.FindAsync(id);
            if (systemTask == null)
            {
                return NotFound();
            }

            _context.SystemTask.Remove(systemTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SystemTaskExists(Guid id)
        {
            return _context.SystemTask.Any(e => e.Id == id);
        }
    }
}
