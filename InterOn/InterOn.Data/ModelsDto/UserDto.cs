using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.ModelsDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
