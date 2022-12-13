using AutoMapper;
using Core.DTOs;
using Core.DTOs.UserDtos;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWorks;
using Core.Utilities.Token;
using Microsoft.AspNetCore.Http;
using Repository.Helpers;
using Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;
        public UserService(Core.Repositories.IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, 
            IHttpContextAccessor httpContext, ITokenService tokenService) : base(repository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _httpContext = httpContext;

        }

        public async Task<CustomResponseDto<UserUpdateImageResponseDto>> Image(int id, UserUpdateImageDto userUpdateImageDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                List<Image> images = _httpContext.HttpContext.Items["FilePaths"] as List<Image>;
                user.Image = images.Count == 0 ? "/Images/defaultUser.jpg" : images.First().Path;

                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
                var userDto = _mapper.Map<UserUpdateImageResponseDto>(user);
                return CustomResponseDto<UserUpdateImageResponseDto>.Success(200, "OK",1,userDto);
            }
            throw new NotFoundException();
        }


        public async Task<CustomResponseDto<NoContentDto>> Password(int id, UserUpdatePasswordDto userUpdatePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            //var password = PasswordHelper.CryptoPassword(userUpdatePasswordDto.OldPassword);
            if (PasswordHelper.VerifyPassword(userUpdatePasswordDto.OldPassword, user.Password) == true)
            {
                user.Password = PasswordHelper.CryptoPassword(userUpdatePasswordDto.Password);
                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
                return CustomResponseDto<NoContentDto>.Success(200, "OK");
            }
            throw new PasswordErrorException();
        }
        public async Task<CustomResponseDto<List<UserDto>>> GetAllUser(bool? isAdmin, string search, int limit, int page)
        {
            var users = await _userRepository.GetAllUser(isAdmin, search, limit, page);

            var usersDto = _mapper.Map<List<UserDto>>(users.Items);
            return CustomResponseDto<List<UserDto>>.Success(200, "OK", users.Count, usersDto);
        }

        public async Task<CustomResponseDto<LoginResponseDto>> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user.Phone != userUpdateDto.Phone)
            {
                var userData = await _userRepository.AnyAsync(u => u.Phone == userUpdateDto.Phone);
                if (userData)
                {
                    throw new RegisteredException();
                }
            }
            var userUpdate = _mapper.Map(userUpdateDto, user);

            _userRepository.Update(userUpdate);
            await _unitOfWork.CommitAsync();
            var userDto = _mapper.Map<LoginResponseDto>(userUpdate);
            userDto.Token = _tokenService.CreateToken(user.Id, user.Phone, user.Name, user.Lastname,Convert.ToInt32(user.IsAdmin));

            return CustomResponseDto<LoginResponseDto>.Success(200, "OK", 1, userDto);
        }


        public async Task<CustomResponseDto<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.Login(loginDto.Phone);
            if (user != null)
            {
                if (PasswordHelper.VerifyPassword(loginDto.Password, user.Password) == true)
                {
                    var userDto = _mapper.Map<LoginResponseDto>(user);
                    userDto.Token = _tokenService.CreateToken(user.Id, user.Phone, user.Name, user.Lastname, Convert.ToInt32(user.IsAdmin));
                    return CustomResponseDto<LoginResponseDto>.Success(200, "OK", 1, userDto);
                }
                throw new LoginErrorException();
            }
            throw new NotFoundException();
        }

        public async Task<CustomResponseDto<User>> AddAsync(UserAddDto userAddDto)
        {
            var userData = await _userRepository.AnyAsync(u => u.Phone == userAddDto.Phone);
            if (userData)
            {
                throw new RegisteredException();
            }
            var user = _mapper.Map<User>(userAddDto);
            List<Image> images = _httpContext.HttpContext.Items["FilePaths"] as List<Image>;
            user.Image = images.Count == 0 ? "/Images/defaultUser.jpg" : images.First().Path;

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<User>.Success(200, "OK", 1, user);

        }

        public async Task<CustomResponseDto<User>> Register(UserAddDto userAddDto)
        {
            var userData = await _userRepository.AnyAsync(u => u.Phone == userAddDto.Phone);
            if (userData)
            {
                throw new RegisteredException();
            }
            var user = _mapper.Map<User>(userAddDto);
            List<Image> images = _httpContext.HttpContext.Items["FilePaths"] as List<Image>;
            user.Image = images.Count == 0 ? "/Images/defaultUser.jpg" : images.First().Path;

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<User>.Success(200, "OK", 1, user);

        }
    }
}
