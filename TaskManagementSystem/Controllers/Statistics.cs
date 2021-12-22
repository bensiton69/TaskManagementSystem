using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Statistics : ControllerBase
    {
        private readonly DataContext _context;

        public Statistics(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics(DateTime stratDateTime, DateTime endDateTime)
        {
            //stratDateTime <= s.Deadline <= endDateTime
            // 2021-10-19
            // return number of done from user
            AppUser user = await _context.Users
                .Include(u => u.SystemTasks)
                .FirstOrDefaultAsync();
            var numberOfDone = user.SystemTasks
                .Where(s => stratDateTime <= s.Deadline && s.Deadline<= endDateTime)
                .Where(s => s.Status == eStatus.Done).ToList().Count;
            return Ok(numberOfDone);
        }
    }

}
