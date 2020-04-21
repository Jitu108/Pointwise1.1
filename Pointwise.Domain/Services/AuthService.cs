using Microsoft.IdentityModel.Tokens;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pointwise.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repository;

        public AuthService(IUserRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public IUser Authenticate(string userName, string password, byte[] key)
        {
            var user = repository.Authenticate(userName, password);

            if (user == null) return null;

            user.Password = string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserType.Name.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public bool Logout(string userName)
        {
            return repository.Logout(userName);
        }
    }
}
