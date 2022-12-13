using Core.DTOs.UserDtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User> Login(string Phone);
    }
}
