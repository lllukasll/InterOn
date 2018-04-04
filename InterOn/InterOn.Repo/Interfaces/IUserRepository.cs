using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;

namespace InterOn.Repo.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        bool CheckLogin(string username);

        void UpdateUser(User user);
        void CreateUser(User user);
        void Save();

        //Moze to przeniose ... jeszcze nie wiem :)
        void AssignRoleToUser(UserRole userRole);
        IEnumerable<Role> GetUserRoles(int userId);

        bool AddToken(UserToken userToken);
        UserToken GetUserToken(string refreshToken, int userId);
        bool ExpireUserToken(UserToken userToken);

        //Confirmation Key
        void ConfirmEmail(ConfirmationKey key);
        void AddConfirmationKey(ConfirmationKey key);
        ConfirmationKey GetConfirmationKey(int userId, string key);
        void RevokeConfirmationKey(ConfirmationKey key);
        void DeleteConfirmationKey(ConfirmationKey key);
    }
}
