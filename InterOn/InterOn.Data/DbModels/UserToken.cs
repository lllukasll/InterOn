using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.DbModels
{
    public class UserToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsStop { get; set; }
        public string Token { get; set; }
    }
}
