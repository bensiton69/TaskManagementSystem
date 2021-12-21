using System;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
        public eUrgentLevel UrgentLevel { get; set; }
        public DateTime Deadline { get; set; }
        public Guid OwnerId { get; set; }
    }
}
