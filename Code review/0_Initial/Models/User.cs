using System;

namespace CleanCode.Initial.Models
{
    public class User
    {
        public string Id { get; set; }

        public UserType Type { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastActivityDate { get; set; }

    }

    public enum UserType
    {
        SuperUser,
        Corrupted
    }
}
