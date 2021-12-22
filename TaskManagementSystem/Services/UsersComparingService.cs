using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Services
{
    public class UsersComparingService : IUsersComparingService
    {
        public IEnumerable<RateObject<string>> CompareAppUsers(IEnumerable<AppUser> users, DateTime stratDateTime, DateTime endDateTime)
        {
            List<RateObject<string>> rateObjects = new List<RateObject<string>>();

            foreach (AppUser appUser in users)
            {
                int numOfDone = appUser.SystemTasks
                    .Where(s => stratDateTime <= s.Deadline && s.Deadline <= endDateTime)
                    .Where(s => s.Status == eStatus.Done).ToList().Count;

                rateObjects.Add(new RateObject<string> { Count = numOfDone, Object = appUser.UserName });
            }
            rateObjects.Sort((x, y) => y.Count.CompareTo(x.Count));
            return rateObjects;
        }
    }
}
