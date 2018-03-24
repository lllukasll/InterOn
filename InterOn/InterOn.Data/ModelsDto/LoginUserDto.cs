using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InterOn.Data.ModelsDto
{
    public class LoginUserDto
    {
        [Required]
        public string GrantType { get; set; }

        //Do refreshToken
        public int UserId { get; set; }
        public string RefreshToken { get; set; }

        //Do password
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
