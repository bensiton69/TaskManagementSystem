using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Controllers
{
    /// <summary>
    /// Controller that returns the enums as IEnumerable of strings
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        /// <summary>
        /// Endpoint to return the types of status enum
        /// </summary>
        /// <returns></returns>
        [HttpGet("Statuses")]
        public IEnumerable<string> GetStatuses()
        {
            return Enum.GetValues(typeof(eStatus))
                .Cast<eStatus>()
                .Select(v => v.ToString());
        }
        /// <summary>
        /// Endpoint to return the types of UrgentLevel enum
        /// </summary>
        /// <returns></returns>
        [HttpGet("UrgentLevel")]
        public IEnumerable<string> GetUrgentLevel()
        {
            return Enum.GetValues(typeof(eUrgentLevel))
                .Cast<eUrgentLevel>()
                .Select(v => v.ToString());
        }
    }
}
