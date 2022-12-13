using AutoMapper;
using Core.DTOs.UserDtos;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDto, User>().ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, LoginResponseDto>();
            CreateMap<User, UserUpdateImageResponseDto>();

            CreateMap<UserAddDto, User>().ForMember(dest => dest.Password, opt => opt
                .MapFrom(src => src.Password == null ? PasswordHelper.CryptoPassword("123456") : PasswordHelper.CryptoPassword(src.Password)));

            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
