using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeature
{
    public class AuthFeature(
        IConfiguration configuration, 
        IBaseRepository<Users> repository, 
        IBaseRepository<UsersRole> roleRepository, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IPasswordHasher<object> passwordHasher)
    {
        public string GenerateToken(string username)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UsersDTO CreateUser(UsersParamsDTO userParam)
        {
            var userExist = repository.Find(x => x.Username == userParam.Username);
            if (userExist.Count() > 0) {
                throw new Exception("Username sudah ada");
            }

            userParam.Password = passwordHasher.HashPassword(userParam, userParam.Password);

            var user = mapper.Map<Users>(userParam);
            repository.Add(user);
            unitOfWork.SaveChanges();

            var userDTO = mapper.Map<UsersDTO>(user);
            return userDTO;
        }

        public UsersRoleDTO CreateUserRole(UsersRoleParamsDTO userRoleParam)
        {
            var user = repository.Find(x => x.Username == userRoleParam.Username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("Username tidak ditemukan");
            }

            var userRoleExist = roleRepository.Find(x => x.IdUser == user.Id && x.IdRole == userRoleParam.IdRole);
            if (userRoleExist.Count() > 0)
            {
                throw new Exception("Role sudah pernah ditambahkan");
            }

            var userRole = mapper.Map<UsersRole>(userRoleParam);
            userRole.IdUser = user.Id;
            roleRepository.Add(userRole);
            unitOfWork.SaveChanges();

            var userRoleDTO = mapper.Map<UsersRoleDTO>(userRole);
            userRoleDTO.Username = userRoleParam.Username;
            return userRoleDTO;
        }

        public bool Login(LoginParamsDTO loginParams)
        {
            var userExist = repository.Find(x => x.Username == loginParams.Username);
            if (userExist.Count() == 0)
            {
                throw new Exception("Username tidak ditemukan");
            }

            var result = passwordHasher.VerifyHashedPassword(
                loginParams,
                userExist.FirstOrDefault().Password,
                loginParams.Password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("Password salah");
            }

            return result == PasswordVerificationResult.Success;
        }
    }
}
