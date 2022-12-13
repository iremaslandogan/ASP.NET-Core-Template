using Core.DTOs.UserDtos;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> Login(string Phone)
        {
            return await _context.Users.Where(x => x.Phone == Phone).FirstOrDefaultAsync();
        }
    }
}
