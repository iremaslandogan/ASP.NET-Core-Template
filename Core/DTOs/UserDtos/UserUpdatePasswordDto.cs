using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.UserDtos
{
    public class UserUpdatePasswordDto
    {
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
