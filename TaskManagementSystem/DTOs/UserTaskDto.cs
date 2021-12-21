using System.Collections.Generic;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.DTOs
{
    public class UserTaskDto
    {
        public string Username { get; set; }
        public ICollection<TaskDto> SystemTasks { get; set; }

        public UserTaskDto()
        {
            SystemTasks = new List<TaskDto>();
        }
    }
}
