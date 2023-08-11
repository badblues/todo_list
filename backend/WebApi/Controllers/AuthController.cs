using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoList.Domain;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthController(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }


    //TODO
    [HttpPost("refresh-token")]
    public ActionResult<Response<AuthDTO>> RefreshToken()
    {
        Response<AuthDTO> response = new();
        var refreshToken = Request.Cookies["refreshToken"];

        User? loggedUser = null;

        var users = _repository.GetAll();
        foreach ( var user in users )
        {
            if (user.RefreshToken.Equals(refreshToken))
                loggedUser = user;
        }

        if (loggedUser == null)
        {
            response.Error = "Invalid Refresh Token";
            return Unauthorized(response);
        }
        else if (loggedUser.RefreshTokenExpires < DateTime.Now)
        {
            response.Error = "Token expired";
            return Unauthorized(response);
        }

        string token = CreateToken(loggedUser);
        string newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken, loggedUser);

        response.Data = new AuthDTO {
            Token = token
        };
        return response;
    }


    [HttpPost("register")]
    public ActionResult<Response<UserDto>> Register(InputUserDto request)
    {
        Response<UserDto> response = new();

        User? user = _repository.Get(request.Email);
        if (user != null) {
            response.Error = "This email already registered";
            return BadRequest(response);
        }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User newUser = new()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _repository.Create(newUser);
        response.Data = newUser.AsDto();

        return response;
    }

    [HttpPost("login")]
    public ActionResult<Response<AuthDTO>> Login(InputUserDto request)
    {
        Response<AuthDTO> response = new();
        
        User? user = _repository.Get(request.Email);
        if (user is null) {
            response.Error="Email not found";
            return BadRequest(response);
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            response.Error="Wrong password";
            return BadRequest(response);
        }

        string token = CreateToken(user);

        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, user);

        response.Data = new AuthDTO {
            Token = token,
        };

        return response;
    }

    private static string GenerateRefreshToken()
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
            //workaround to avoid error
            Expires = new DateTime(DateTime.UtcNow.Ticks + TimeSpan.FromDays(7).Ticks, DateTimeKind.Utc)
        };
        Response.Cookies.Append("refreshToken", updatedUser.RefreshToken, cookieOptions);

        _repository.Update(updatedUser);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("email", user.Email),
            new Claim("id", user.Id.ToString())
        };

        var secretToken = _configuration.GetSection("AppSettings:Token").Value;

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

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }


}

