using Core.Models;
using Core.Repositories;
using Core.Utilities.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private readonly IHttpContextAccessor _httpContext;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            _httpContext = httpContext;
        }

        public async Task<PaginatedList<User>> GetAllUser(bool? isAdmin, string search, int limit, int page)
        {
            Expression<Func<User, bool>> adminCond = x => true;
            if (isAdmin != null) adminCond = x => x.IsAdmin == isAdmin;


            Expression<Func<User, bool>> searchCond = x => true;
            if (search != null) searchCond = x => x.Name.Contains(search) || x.Phone.Contains(search);


            var users = _context.Users.Where(adminCond).Where(searchCond).Where(x => x.IsDelete == false);

            return PaginatedListCreator<User>.Create(users, page, limit);

        }


        public async Task<User> Login(string Phone)
        {
            return await _context.Users.Where(x => x.Phone == Phone).SingleOrDefaultAsync();
        }

    }
}
