using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoList.Domain;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using TodoList.WebApi.Services.UserService;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
      
        private readonly IUserRepository repository;
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthController(IUserRepository repository, IConfiguration configuration, IUserService userService)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.userService = userService; 
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetEmail()
        {
            var email = userService.GetUserEmail();
            return email;
        }

        [HttpPost("refresh-token")]
        public ActionResult<string> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            User? loggedUser = null;

            var users = repository.GetUsers();
            foreach ( var user in users )
            {
                if (user.RefreshToken.Equals(refreshToken))
                    loggedUser = user;
            }

            if (loggedUser == null)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (loggedUser.RefreshTokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(loggedUser);
            string newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, loggedUser);

            return token;
        }


        [HttpPost("register")]
        public ActionResult<UserDto> Register(InputUserDto request)
        {
            User? user = repository.GetUser(request.Email);
            if (user != null)
                return BadRequest("This Email already registered.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            repository.CreateUser(newUser);

            return newUser.AsDto();
        }

        [HttpPost("login")]
        public ActionResult<string> Login(InputUserDto request)
        {
            User? user = repository.GetUser(request.Email);
            if (user is null)
                return BadRequest("Email not found");


            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            return token;
        }

        private string GenerateRefreshToken()
        {
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return refreshToken;
        }

        private void SetRefreshToken(string newRefreshToken, User user)
        {

            User updatedUser = user with
            {
                RefreshToken = newRefreshToken,
                RefreshTokenCreated = DateTime.UtcNow,
                RefreshTokenExpires = DateTime.UtcNow.AddDays(7)
        };

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                //workaround to avoid error
                Expires = new DateTime(DateTime.UtcNow.Ticks + TimeSpan.FromDays(7).Ticks, DateTimeKind.Utc)
        };
            Response.Cookies.Append("refreshToken", updatedUser.RefreshToken, cookieOptions);

            repository.UpdateUser(updatedUser);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            var secretToken = configuration.GetSection("AppSettings:Token").Value;

            if (secretToken is null)
              throw new Exception("Secret token not provided.");
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));          

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
