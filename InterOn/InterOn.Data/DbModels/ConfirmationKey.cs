using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.DbModels
{
    public class ConfirmationKey
    {
        public int Id { get; set; }
        public bool Revoked { get; set; }
        public string Key { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
