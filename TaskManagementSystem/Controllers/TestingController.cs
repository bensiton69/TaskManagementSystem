using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Extensions;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly DataContext _context;

        public TestingController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("/Test")]
        public async Task<IActionResult> PostTest()
        {
            Guid id = Guid.Parse("90f19746-cda2-41d2-695a-08d9c482c6d1");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            //return Ok(user);

            user.SystemTasks.Add(new SystemTask(){ Title = "Othello" });
            _context.Attach(user);
            _context.Entry(user).Collection(x => x.SystemTasks).IsModified = true;
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.SystemTasks).ToListAsync();
            var newUsersDTOs = new List<UserMeetingsDto>();
            foreach (AppUser user in users)
            {
                var taskDtos = new List<TaskDto>();
                foreach (SystemTask userMeeting in user.SystemTasks)
                {
                    taskDtos.Add(new TaskDto() { Title = userMeeting.Title, OwnerId = userMeeting.OwnerId });
                }

                newUsersDTOs.Add(new UserMeetingsDto() { Username = user.UserName,SystemTasks = taskDtos });
            }

            return Ok(newUsersDTOs);
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetTasks()
        {
            var result = _context.SystemTasks
                //.Include(x => x.OwnerId)
                .AsQueryable();

            return Ok(result);
        }
    }
}
