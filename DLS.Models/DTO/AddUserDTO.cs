using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLS.Models.DTO
{
    public class AddUserDTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
#nullable enable
        public string? Gender { get; set; }
        public int? Age { get; set; }
#nullable disable
    }
}