using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Persistence
{
    public class UsersRepository:IUsersRepository
    {
        public Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<AppUser>> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
