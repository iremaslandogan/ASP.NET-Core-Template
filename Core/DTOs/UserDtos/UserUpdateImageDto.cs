﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.UserDtos
{
    public class UserUpdateImageDto
    {
        public IFormFile Image { get; set; }
    }
}
