using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Api.Helpers;
using InterOn.Data.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using InterOn.Data.ModelsDto;
using InterOn.Service.Interfaces;
using Newtonsoft.Json;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("api")]
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
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                //mozna udostepnic jakies dane
                return BadRequest();
            }

            if (userDto.GrantType == "password")
            {
                return DoPassword(userDto);
            }
            else if (userDto.GrantType == "refresh_token")
            {
                return DoRefreshToken(userDto);
            }
            else
            {
                //mozna wyswietlic jakis blad
                return BadRequest();
            }
        }

        private IActionResult DoPassword(UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
            {
                return BadRequest();
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

        private IActionResult DoRefreshToken(UserDto userDto)
        {
            var token = _userService.GetUserToken(userDto.RefreshToken, userDto.Id);

            if (token == null)
            {
                return BadRequest();
            }

            if (token.IsStop == 1) //Token stracił ważność
            {
                return BadRequest();
            }

            var refreshToken = GenerateRefreshToken();

            token.IsStop = 1;

            var updateFlag = _userService.ExpireUserToken(token);

            var addFlag = _userService.AddToken(new UserToken
            {
                UserId = userDto.Id,
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
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
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
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                _userService.Create(user, userDto.Password);
                _userService.SendConfirmationEmail(user); // ToDo
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
    }
}
