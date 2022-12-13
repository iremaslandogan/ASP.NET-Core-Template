using Core.Models;
using Core.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PaginatedList<User>> GetAllUser(bool? isAdmin, string search, int limit, int page);
        Task<User> Login(string Phone);
    }
}
