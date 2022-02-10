using System;

namespace CleanCode.SLA.Before.Models
{
    public class User
    {
        public DateTime DateOfBirth { get; set; }

        public bool HasBirthDay { get; internal set; }

        public string Modifier { get; internal set; }

        public object Icon { get; internal set; }

        public string Greeting { get; internal set; }
    }
}
