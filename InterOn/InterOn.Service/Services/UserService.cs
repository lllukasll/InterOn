using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;

namespace InterOn.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserService(IUserRepository userRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetUser(username, password);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if(_userRepository.CheckLogin(user.Username))
                throw new Exception("Username " + user.Username + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.CreateUser(user);
            _userRepository.Save();

            return user;
        }

        public void AssignRoleToUser(UserRole userRole)
        {
            var user = _userRepository.GetUserById(userRole.UserId);

            if(user == null)
                throw new Exception("UserId : " + userRole.UserId + " don't exists");

            var role = _roleRepository.Get(userRole.RoleId);

            if(role == null)
                throw new Exception("RoleId : " + userRole.RoleId + " don't exists");

            _userRepository.AssignRoleToUser(userRole);
            _userRepository.Save();
        }

        public bool AddToken(UserToken userToken)
        {
            if (_userRepository.AddToken(userToken))
                return true;

            return false;
        }

        public IEnumerable<Role> GetUserRoles(User user)
        {
            var userRoles = _userRepository.GetUserRoles(user.Id);

            return userRoles;
        }

        public UserToken GetUserToken(string refreshToken, int userId)
        {
            var token = _userRepository.GetUserToken(refreshToken, userId);

            return token;
        }

        public bool ExpireUserToken(UserToken userToken)
        {
            try
            {
                _userRepository.ExpireUserToken(userToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null) throw new ArgumentException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if(password == null) throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if(storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if(storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
