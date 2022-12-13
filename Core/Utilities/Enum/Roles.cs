using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Enum
{
    public enum Roles
    {
        [Display(Name = "Admin")] Admin = 1
    }

    public static class Role
    {
        public const string Admin = "1";
    }
}
