using System;

namespace Auth.JWT.Models
{
    public class User
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string GivenName { get; set; } = null!;

    }
}
