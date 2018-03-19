using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterOn.Data.DbModels;
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

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
