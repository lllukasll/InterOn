using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using InterOn.Api.Helpers;
using InterOn.Data.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using InterOn.Data.ModelsDto;
using InterOn.Service.Interfaces;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userDto == null)
                return BadRequest("Niepoprawne dane");
            

            if (userDto.GrantType == "password")
            {
                if (userDto.Username == null)
                    return BadRequest("Pole nazwa użytkownika jest wymagane.");

                if (userDto.Password == null)
                    return BadRequest("Pole hasło jest wymagane.");

                return DoPassword(userDto);
            }
            else if (userDto.GrantType == "refresh_token")
            {
                if (userDto.UserId == null)
                    return BadRequest("Pole id uzytkownika jest wymagane.");

                if (userDto.RefreshToken == null)
                    return BadRequest("Pole refreshToken jest wymagane.");

                return DoRefreshToken(userDto);
            }
            else
            {
                return BadRequest("Wystąpił problem podczas autentykacji");
            }
        }

        private IActionResult DoPassword(LoginUserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
            {
                return BadRequest("Email i/lub hasło są nie poprawne");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("W celu zalogowania wymagana jest aktywacja konta");
            }

            var refreshToken = GenerateRefreshToken();

            var rToken = new UserTokenDto
            {
                Token = refreshToken,
                UserId = user.Id,
                IsStop = 0
            };

            var mappedRefreshToken = _mapper.Map<UserToken>(rToken);

            if (_userService.AddToken(mappedRefreshToken))
            {
                return Ok(GetJwt(user, refreshToken));
            }

            return BadRequest();
        }

        private IActionResult DoRefreshToken(LoginUserDto userDto)
        {
            var token = _userService.GetUserToken(userDto.RefreshToken, userDto.UserId);

            if (token == null)
                return BadRequest("Nieprawidłowy token");

            if (token.IsStop == 1)
                return BadRequest("Token stracił ważność");

            var refreshToken = GenerateRefreshToken();

            token.IsStop = 1;

            var updateFlag = _userService.ExpireUserToken(token);

            var addFlag = _userService.AddToken(new UserToken
            {
                UserId = userDto.UserId,
                IsStop = 0,
                Token = refreshToken
            });

            var user = _mapper.Map<User>(userDto);

            if (updateFlag && addFlag)
            {
                return Ok(GetJwt(user, refreshToken));
            }

            return BadRequest();
        }

        private LoginResponseData GetJwt(User user, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            //====Moj kod do roli ====
            var roles = _userService.GetUserRoles(user);
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Id.ToString())};
            foreach (var v in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, v.Name));
            }
            //========Koniec===========

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_appSettings.AccessExpireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var response = new LoginResponseData
            {
                ClientId = user.Id,
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };

            return response;
        }

        private string GenerateRefreshToken()
        {
            var token = Guid.NewGuid().ToString().Replace("-", "");

            return token;
        }

        [Authorize]
        [HttpPost("{userId}/changePassword")]
        public IActionResult ChangePassword(int userId, [FromBody]ChangePasswordDto changePasswordDto )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userService.GetUserById(userId) == null)
            {
                return BadRequest("Brak użytkownika o id : " + userId);
            }

            if (changePasswordDto.NewPassword != changePasswordDto.NewPassword2)
            {
                return BadRequest("Hasła nie zgadzają się");
            }

            if (_userService.CheckPassword(changePasswordDto.OldPassword, userId))
            {
                return BadRequest("Złe haslo");
            }

            if (_userService.ChangePassword(userId, changePasswordDto))
            {
                return Ok();
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{userId}/{key}")]
        public IActionResult ConfirmEmail(int userId, string key)
        {
            var confirmationKey = _userService.GetConfirmationKey(userId, key);

            if (confirmationKey == null)
                return BadRequest();

            var confirmEmailFlag = _userService.ConfirmEmail(confirmationKey);
            var revokeConfirmationKeyFlag = _userService.RevokeConfirmationKey(confirmationKey);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.CheckIfLoginUnique(userDto.Username))
                return BadRequest("Login " + userDto.Username + " jest zajęty");

            var user = _mapper.Map<User>(userDto);

            try
            {
                _userService.Create(user, userDto.Password);
                _userService.SendConfirmationEmail(user);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Moze stworzyc kontroler do UserRole ? 
        [HttpPost("assignRole")]
        public IActionResult AssignRoleToUser([FromBody]UserRoleDto userRoleDto)
        {
            if (userRoleDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userRole = _mapper.Map<UserRole>(userRoleDto);

            try
            {
                _userService.AssignRoleToUser(userRole);
                return Ok(userRole);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        [HttpGet("getLoggedUser")]
        public IActionResult GetLoggedUser()
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var user = _userService.GetUserById(userId);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        [HttpGet()]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }
    }
}
