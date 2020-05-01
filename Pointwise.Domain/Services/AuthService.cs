using Microsoft.IdentityModel.Tokens;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Pointwise.Domain.Enums;

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


            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            var roleClaims = user.Roles.Select(x =>
            new Claim(ClaimTypes.Role, Enum.GetName(typeof(EntityType), x.EntityType) + Enum.GetName(typeof(AccessType), x.AccessType))).ToList();
            var adminRoleClaim = new Claim(ClaimTypes.Role, user.UserType.Name.ToString());
            
            claims.AddRange(roleClaims);
            claims.Add(adminRoleClaim);

            user.Password = string.Empty;
            var expiryDate = DateTime.UtcNow.AddDays(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    claims.ToArray()
                    //new Claim[] {
                    //new Claim(ClaimTypes.Name, user.Id.ToString()),
                    //new Claim(ClaimTypes.Role, user.UserType.Name.ToString())
                    //}
                    ),
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.ExpiryDate = expiryDate;
            return user;
        }

        public bool Logout(string userName)
        {
            return repository.Logout(userName);
        }
    }
}
