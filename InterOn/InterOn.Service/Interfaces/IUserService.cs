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

        //Moze zrobic to oddzielnie ? 
        void AssignRoleToUser(UserRole userRole);
        IEnumerable<Role> GetUserRoles(User user);
    }
}
