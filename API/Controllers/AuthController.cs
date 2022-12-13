using API.Filters;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.UserDtos;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _service = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return CreateActionResult(await _service.Login(loginDto));
        }

        //[ServiceFilter(typeof(RegisterFilter<User>),Order = 2)]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserAddDto userAddDto)
        {
            return CreateActionResult(await _service.Register(userAddDto));
        }
    }
}
