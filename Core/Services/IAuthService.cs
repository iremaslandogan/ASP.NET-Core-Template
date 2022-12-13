using Core.DTOs;
using Core.DTOs.UserDtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthService : IGenericRepository<User>
    {
        Task<CustomResponseDto<LoginResponseDto>> Login(LoginDto loginDto);
        Task<CustomResponseDto<User>> Register(UserAddDto userAddDto);
    }
}
