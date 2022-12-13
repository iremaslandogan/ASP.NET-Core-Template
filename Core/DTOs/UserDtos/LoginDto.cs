using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.UserDtos
{
    public class LoginDto
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}