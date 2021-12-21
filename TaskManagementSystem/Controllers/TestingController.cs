using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TestingController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost("/TaskCreate")]
        public async Task<IActionResult> TaskCreate()
        {
            Guid id = Guid.Parse("90f19746-cda2-41d2-695a-08d9c482c6d1");
            var user = await _context.Users.FindAsync(id);
            SystemTask systemTask = new SystemTask()
            {
                Title = "with times creation time",
                Description = "stam",
                Status = eStatus.Done,
                UrgentLevel = eUrgentLevel.Medium,
            };

            await _context.SystemTasks.AddAsync(systemTask);

            return Ok(systemTask);
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

            // From AppUser to userMeetingDTO
            // From SystemTask to TaskDto 
            var users = await _context.Users.Include(x => x.SystemTasks).ToListAsync();
            var newUsersDTOs = new List<UserTaskDto>();
            foreach (AppUser user in users)
            {
                var taskDtos = new List<TaskDto>();
                foreach (SystemTask userMeeting in user.SystemTasks)
                {
                    taskDtos.Add(new TaskDto() { Title = userMeeting.Title, OwnerId = userMeeting.OwnerId });
                }

                newUsersDTOs.Add(new UserTaskDto() { Username = user.UserName,SystemTasks = taskDtos });
            }

            return Ok(newUsersDTOs);
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetTasks()
        {
            var result = _context.SystemTasks
                .AsQueryable();

            return Ok(result);
        }

        [HttpGet("TestMap")]
        public async Task<ActionResult> TestMap()
        {
            Guid id = Guid.Parse("90f19746-cda2-41d2-695a-08d9c482c6d1");
            var user = await _context.Users.Include(a => a.SystemTasks).FirstAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var mappedCollection = _mapper.Map<ICollection<SystemTask>, List<TaskDto>>(user.SystemTasks);

            var mappedUsersCollection = _mapper.Map<ICollection<AppUser>, List<UserTaskDto>>(await _context.Users.ToListAsync());

            var mappedUser = _mapper.Map<AppUser, UserTaskDto>(user);


            //SystemTask systemTask = await _context.SystemTasks.FirstAsync();
            //var mappedTask = _mapper.Map<SystemTask, TaskDto>(systemTask);

            return Ok(mappedUsersCollection);

        }
    }
}
