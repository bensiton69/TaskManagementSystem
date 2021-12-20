using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.Models
{
    public class SystemTask
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
        public eUrgentLevel UrgentLevel { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}
