using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Repo.Interfaces;

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

        public bool AddToken(UserToken userRole)
        {
            try
            {
                _context.UserTokens.Add(userRole);
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
