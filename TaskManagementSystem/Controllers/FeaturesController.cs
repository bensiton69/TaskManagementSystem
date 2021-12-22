using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {

        [HttpGet("Statuses")]
        public IEnumerable<string> GetStatuses()
        {
            return Enum.GetValues(typeof(eStatus))
                .Cast<eStatus>()
                .Select(v => v.ToString());
        }

        [HttpGet("UrgentLevel")]
        public IEnumerable<string> GetUrgentLevel()
        {
            return Enum.GetValues(typeof(eUrgentLevel))
                .Cast<eUrgentLevel>()
                .Select(v => v.ToString());
        }
    }
}
