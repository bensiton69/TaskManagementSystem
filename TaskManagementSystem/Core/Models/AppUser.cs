using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskManagementSystem.Core.Models
{
    public class AppUser : IAppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<SystemTask> SystemTasks { get; set; }

        public AppUser()
        {
            SystemTasks = new List<SystemTask>();
        }
    }
}
