using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemTasksController : ControllerBase
    {
        private readonly ISystemTaskRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public SystemTasksController(ISystemTaskRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        // GET: api/SystemTasks
        [HttpGet]
        public async Task<QueryResult<SystemTask>> GetSystemTasks([FromQuery] SystemTaskQuery systemTaskQuery)
        {
            return await _repository.GetSystemTasks(systemTaskQuery);
        }



        // GET: api/SystemTasks/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<SystemTask>> GetSystemTask(Guid id)
        {
            var systemTask = await _repository.GetSystemTask(id, true);

            if (systemTask == null)
            {
                return NotFound();
            }

            return Ok(systemTask);
        }


        // PUT: api/SystemTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSystemTask(Guid id, SystemTask systemTask)
        {
            _repository.UpdateTask(id, systemTask);
            await _unitOfWork.CompleteAsync();

            return Ok(systemTask);

        }

        // POST: api/SystemTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSystemTask(SystemTask systemTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            systemTask.CreationDateTime = DateTime.Now;
            _repository.Add(systemTask);
            await _unitOfWork.CompleteAsync();

            return Ok(systemTask);
        }

        // DELETE: api/SystemTasks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSystemTask(Guid id)
        {
            var systemTask = await _repository.GetSystemTask(id, includeRelated: false);
            if (systemTask == null)
                return NotFound();
            _repository.Remove(systemTask);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }

    }
}
