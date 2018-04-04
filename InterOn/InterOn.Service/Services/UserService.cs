using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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
            //if(string.IsNullOrWhiteSpace(password))
            //    throw new Exception("Password is required");

            //if(_userRepository.CheckLogin(user.Username))
            //    throw new Exception("Username " + user.Username + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.CreateUser(user);
            _userRepository.Save();

            return user;
        }

        public bool CheckIfLoginUnique(string login)
        {
            if (_userRepository.CheckLogin(login))
                return false;
            return true;
        }

        public User GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            return users;
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

        public bool SendConfirmationEmail(User user)
        {
            try
            {
                var key = GenerateConfirmationKey(user);

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "mailinteron@gmail.com",
                        Password = "4BsYmh3adsz"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.Email));
                    message.From = new MailAddress("mailinteron@gmail.com");
                    message.Subject = "Link aktywacyjny";
                    message.Body = "http://localhost:58200/users/" + key.UserId + "/" + key.Key + "<br />Link aktywacyjny<br />Klucz : " + key.Key + "<br />UserId : " + key.UserId;
                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ConfirmEmail(ConfirmationKey key)
        {
            try
            {
                if (key == null)
                    return false;

                _userRepository.ConfirmEmail(key);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ConfirmationKey GetConfirmationKey(int userId, string key)
        {
            if (userId == null || key == null)
            {
                throw new Exception("Values can't be null");
            }
            return _userRepository.GetConfirmationKey(userId, key);
        }

        public ConfirmationKey GenerateConfirmationKey(User user)
        {
            var key = Guid.NewGuid().ToString().Replace("-", "");

            var confirmationKey = new ConfirmationKey
            {
                UserId = user.Id,
                Key = key,
                Revoked = false
            };

            _userRepository.AddConfirmationKey(confirmationKey);

            return confirmationKey;
        }

        public bool RevokeConfirmationKey(ConfirmationKey key)
        {
            try
            {
                _userRepository.RevokeConfirmationKey(key);
            }
            catch
            {
                return false;
            }
            return true;

        }

        public bool DeleteConfirmationKey(ConfirmationKey key)
        {
            try
            {
                _userRepository.DeleteConfirmationKey(key);
            }
            catch
            {
                return false;
            }
            return true;

        }

        public bool ChangePassword(int userId, ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = _userRepository.GetUserById(userId);

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _userRepository.UpdateUser(user);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CheckPassword(string password, int userId)
        {
            var user = _userRepository.GetUserById(userId);

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return true;

            return false;
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
