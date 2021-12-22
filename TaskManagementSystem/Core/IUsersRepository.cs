using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core
{
    public interface IUsersRepository
    {
        Task<ActionResult<IEnumerable<AppUser>>> GetUsers();
        Task<ActionResult<AppUser>> GetUser(Guid id);
    }
}