using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoList.Domain;
using TodoList.Persistence.Interfaces;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    
    [ApiController]
    public class AuthController : ControllerBase
    {
      
        private readonly IUserRepository repository;
        //private readonly IConfiguration configuration;
        private string token = "qwertyasdfghzxcvbn"; 

        public AuthController(IUserRepository repository)
        {
            this.repository = repository;
            //this.configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(InputUserDto request)
        {
            //TODO: CHECK EXISTING USER BY EMAIL

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            repository.CreateUser(newUser);
            //TODO: FIGURE OUT WHAT DATA NEEDS TO BE RETURNED

            return newUser;
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

            User updatedUser = user with
            {
                TokenCreated = DateTime.UtcNow,
                TokenExpires = DateTime.UtcNow.AddDays(1)
            };
            repository.UpdateUser(updatedUser);

            //TODO REFRESH TOKEN

            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.token));

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
