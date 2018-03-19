using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.DbModels
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Roles { get; set; }
    }
}
