using System.Collections.Generic;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;

namespace InterOn.Service.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        bool CheckIfLoginUnique(string login);

        //Role
        void AssignRoleToUser(UserRole userRole);
        IEnumerable<Role> GetUserRoles(User user);

        //Token
        bool AddToken(UserToken userToken);
        bool ExpireUserToken(UserToken userToken);
        UserToken GetUserToken(string refreshToken, int userId);

        //Confirmation Email
        bool SendConfirmationEmail(User user);
        bool ConfirmEmail(ConfirmationKey key);

        //Confirmation Key
        ConfirmationKey GenerateConfirmationKey(User user);
        ConfirmationKey GetConfirmationKey(int userId, string key);
        bool RevokeConfirmationKey(ConfirmationKey key);
        bool DeleteConfirmationKey(ConfirmationKey key);

        //Password
        bool CheckPassword(string password, int userId);
        bool ChangePassword(int userId, ChangePasswordDto changePasswordDto);
    }
}
