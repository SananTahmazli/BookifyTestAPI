using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities;
using DTOs;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class UserRepository : GenericRepository<UserDTO, User, UserDTO>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public override async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var username = _dbContext.Users.Where(u => u.Username.ToLower() == userDTO.Username.ToLower());
            var role = _dbContext.Roles.Where(r => r.Name == RoleKeywords.UserRole).First();
            userDTO.RoleId = role.Id;

            if (username.Any())
            {
                throw new Exception("Username is already exist!");
            }
            else
            {
                userDTO.Salt = Encryption.GenerateSalt();
                userDTO.Hash = Encryption.GenerateHash(userDTO.Password, userDTO.Salt);
            }
            return await base.CreateAsync(userDTO);
        }

        public UserDTO Login(UserDTO userDTO)
        {
            var username = _dbContext.Users.Where(u => u.Username == userDTO.Username)
                .Include(u => u.Role);

            if (username.Count() == 1)
            {
                var user = username.FirstOrDefault();
                var hash = Encryption.GenerateHash(userDTO.Password, user.Salt);

                if (hash == user.Hash)
                {
                    var model = _mapper.Map<User, UserDTO>(username.First());
                    return model;
                }
                else
                {
                    throw new Exception("Password is incorrect!");
                }
            }
            else
            {
                throw new Exception("Username is not found!");
            }
        }
    }
}