using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        bool CheckLogin(string username);
        void CreateUser(User user);
        void Save();
    }
}
