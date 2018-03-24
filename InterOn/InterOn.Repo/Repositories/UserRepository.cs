using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Repo.Interfaces;
using SQLitePCL;

namespace InterOn.Repo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            return user;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.AsEnumerable();

            return users;
        }

        public bool CheckLogin(string username)
        {
            var user = _context.Users.Any(x => x.Username == username);

            return user;
        }

        //Moze przeniesc do UserRole ? 
        public void AssignRoleToUser(UserRole userRole)
        {
            var user = _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            var roles = _context.UserRoles.Where(c => c.Roles.Id == userId).AsEnumerable();

            List<Role> tmp = new List<Role>();
            foreach (var v in roles)
            {
                var role = _context.Roles.SingleOrDefault(c => c.Id == v.RoleId);
                tmp.Add(role);
            }

            return tmp;
        }

        public bool AddToken(UserToken userToken)
        {
            try
            {
                _context.UserTokens.Add(userToken);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }    
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
            
        }

        public UserToken GetUserToken(string refreshToken, int userId)
        {
            var token = _context.UserTokens.SingleOrDefault(x => x.Token == refreshToken && x.UserId == userId);

            return token;
        }

        public bool ExpireUserToken(UserToken userToken)
        {
            try
            {
                _context.UserTokens.Update(userToken);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Confirmation Key
        public ConfirmationKey GetConfirmationKey(int userId, string key)
        {
            return _context.ConfirmationKeys.SingleOrDefault(x => x.Key == key && x.UserId == userId);
        }

        public void ConfirmEmail(ConfirmationKey key)
        {
            var user = GetUserById(key.UserId);

            var tmpUser = user;
            tmpUser.EmailConfirmed = true;

            UpdateUser(tmpUser);

            key.Revoked = true;

            RevokeConfirmationKey(key);
        }

        public void AddConfirmationKey(ConfirmationKey key)
        {
            _context.ConfirmationKeys.Add(key);
            _context.SaveChanges();
        }

        public void RevokeConfirmationKey(ConfirmationKey key)
        {
            _context.ConfirmationKeys.Update(key);
            _context.SaveChanges();
        }

        public void DeleteConfirmationKey(ConfirmationKey key)
        {
            _context.ConfirmationKeys.Remove(key);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
