using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.ModelsDto
{
    public class UserTokenDto
    {
        public int UserId { get; set; }
        public int IsStop { get; set; }
        public string Token { get; set; }
    }
}
