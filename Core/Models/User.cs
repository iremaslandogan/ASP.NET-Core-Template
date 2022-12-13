using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public bool IsAdmin { get; set; }
    }
}
