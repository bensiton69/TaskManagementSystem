using System;
using System.Collections.Generic;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core
{
    public interface IAppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        ICollection<SystemTask> SystemTasks { get; set; }
    }
}
