using System;
using System.Collections.Generic;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core
{
    public interface IUsersComparingService
    {
        IEnumerable<RateObject<string>> CompareAppUsers(IEnumerable<AppUser> users, DateTime stratDateTime,
            DateTime endDateTime);
    }
}
