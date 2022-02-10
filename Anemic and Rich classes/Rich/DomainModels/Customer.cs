using System;

namespace CleanCode.Rich.Models
{
    public class Customer
    {
        public int Id { get; private set; }
        
        private DateTime Birthday { get; set; }

        public bool IsBirthdayToday() => Birthday.DayOfYear == DateTime.Now.DayOfYear;;
    }
}
