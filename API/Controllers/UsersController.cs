using API.Filters;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.DTOs;
using Core.DTOs.UserDtos;
using Core.Models;
using Core.Services;
using Core.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Mapping;
using System.Collections.Generic;

namespace API.Controllers
{

    [AuthenticationFilter(Roles.Admin)]
    public class UsersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UsersController(IMapper mapper, IUserService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All(bool? isAdmin, string search, int limit, int page)
        {
            return CreateActionResult(await _service.GetAllUser(isAdmin, search, limit, page));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);

            var usersDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, "OK", 1, usersDto));
        }


        [HttpPost]
        public async Task<IActionResult> Save([FromForm] UserAddDto userAddDto)
        {
            return CreateActionResult(await _service.AddAsync(userAddDto));
        }

        [AuthenticationFilter(Roles.Admin)]
        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto userUpdateDto)
        {
            return CreateActionResult(await _service.UpdateAsync(id,userUpdateDto));
        }


        [AuthenticationFilter(Roles.Admin)]
        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);
            user.IsDelete = true; 
            await _service.UpdateAsync(user);
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200, "OK"));
        }


        [AuthenticationFilter(Roles.Admin)]
        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Password(int id, UserUpdatePasswordDto userUpdatePasswordDto)
        {
            return CreateActionResult(await _service.Password(id,userUpdatePasswordDto));
        }


        [AuthenticationFilter(Roles.Admin)]
        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Image(int id,[FromForm] UserUpdateImageDto userUpdateImageDto)
        {
            return CreateActionResult(await _service.Image(id, userUpdateImageDto));
        }
    }
}
