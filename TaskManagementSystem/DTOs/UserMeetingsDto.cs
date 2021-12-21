using System.Collections.Generic;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.DTOs
{
    public class UserMeetingsDto
    {
        public string Username { get; set; }
        public ICollection<TaskDto> SystemTasks { get; set; }

        public UserMeetingsDto()
        {
            SystemTasks = new List<TaskDto>();
        }
    }
}
