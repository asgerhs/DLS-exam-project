using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLS.Models.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
#nullable enable
        public TeacherDTO? teacher { get; set; }
        public StudentDTO? student { get; set; }
#nullable disable

    }
}