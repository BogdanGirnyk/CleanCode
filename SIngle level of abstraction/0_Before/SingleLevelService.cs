using CleanCode.SLA.Before.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.SLA.Before
{
    public class SingleLevelService
    {
        private readonly List<User> users;

        public void SetBirthdayModifiersToUsers()
        {
            foreach (User user in users.Where(c => c.DateOfBirth.DayOfYear == DateTime.Now.DayOfYear))
            {
                user.HasBirthDay = true;
                user.Modifier = UserModifiers.PersonalHoliday;
                user.Icon = Icons.Birthday;
                user.Greeting = "Happy birthday!";
            }
        }
    }
}
