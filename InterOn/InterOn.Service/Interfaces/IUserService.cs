using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;

namespace InterOn.Service.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
    }
}
