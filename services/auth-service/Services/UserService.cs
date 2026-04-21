using AuthService.Data;
using AuthService.Models;
using BCrypt.Net;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public interface IUserService
    {
        string Register(RegisterDTO dto);
        string Login(LoginDTO dto);
        string GoogleLogin(string idToken);
        int GetUserCount();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public string Register(RegisterDTO dto)
        {
            var existing = _repo.GetByEmail(dto.Email);
            if (existing != null)
                throw new ArgumentException("User already exists");

            var user = new UserEntity
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _repo.AddUser(user);
            return "User registered successfully";
        }

        public string Login(LoginDTO dto)
        {
            var user = _repo.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new ArgumentException("Invalid credentials");

            return GenerateJwt(user);
        }

        public string GoogleLogin(string idToken)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(idToken).Result;
            var email = payload.Email;
            var name = payload.Name;

            var user = _repo.GetByEmail(email);

            if (user == null)
            {
                user = new UserEntity
                {
                    Name = name,
                    Email = email,
                    PasswordHash = "GOOGLE_USER"
                };
                _repo.AddUser(user);
            }

            return GenerateJwt(user);
        }

        public int GetUserCount()
        {
            return _repo.GetAllUsers().Count();
        }

        private string GenerateJwt(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
