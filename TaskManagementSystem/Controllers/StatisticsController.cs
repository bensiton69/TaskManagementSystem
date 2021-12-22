using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    /// <summary>
    /// Statistics controller, Uses UsersComparingService
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUsersComparingService _comparingService;


        public StatisticsController(DataContext context, IUsersComparingService comparingService)
        {
            _context = context;
            _comparingService = comparingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics(DateTime startDateTime, DateTime endDateTime)
        {
            // return number of done tasks from user


            List<AppUser> users = await _context.Users
                .Include(u => u.SystemTasks)
                .ToListAsync();

            List<RateObject<string>> rateObjects = _comparingService.CompareAppUsers(users, startDateTime, endDateTime).ToList();

            return Ok(rateObjects);
        }

    }

}
