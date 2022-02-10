using CleanCode.SLA.After.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.SLA.After
{
    public class SingleLevelService
    {
        private readonly List<User> users;

        public void SetBirthdayModifiersToUsers()
        {
            foreach (User user in SelectUsersWithBirthday())
            {
                user.SetBirthdayModifiers();
            }
        }

        private IEnumerable<User> SelectUsersWithBirthday()
        {
            return users.Where(c => c.DateOfBirth.DayOfYear == DateTime.Now.DayOfYear);
        }
    }
}
