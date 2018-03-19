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

        public bool CheckLogin(string username)
        {
            var user = _context.Users.Any(x => x.Username == username);

            return user;
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
