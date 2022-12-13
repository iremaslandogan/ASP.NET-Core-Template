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
    public interface IUserService : IGenericRepository<User>
    {
        Task<CustomResponseDto<User>> AddAsync(UserAddDto userAddDto);
        Task<CustomResponseDto<NoContentDto>> Password(int id, UserUpdatePasswordDto userUpdatePasswordDto);
        Task<CustomResponseDto<UserUpdateImageResponseDto>> Image(int id, UserUpdateImageDto userUpdateImageDto);
        Task<CustomResponseDto<LoginResponseDto>> UpdateAsync(int id,UserUpdateDto userUpdateDto);
        Task<CustomResponseDto<List<UserDto>>> GetAllUser(bool? isAdmin, string search, int limit, int page);
        Task<CustomResponseDto<LoginResponseDto>> Login(LoginDto loginDto);
        Task<CustomResponseDto<User>> Register(UserAddDto userAddDto);
    }
}
