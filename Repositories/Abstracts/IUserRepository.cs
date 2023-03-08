using DataAccess.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstracts
{
    public interface IUserRepository : IGenericRepository<UserDTO, User, UserDTO>
    {
        UserDTO Login(UserDTO userDTO);
    }
}